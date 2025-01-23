using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Models.University;

namespace WebApp.Controllers
{
    [Authorize(Roles = "admin,user")]
    public class UniversityController : Controller
    {
        private readonly UniversityDbContext _context;
        private readonly IUniversityService _universityService;

        public UniversityController(UniversityDbContext context, IUniversityService universityService)
        {
            _context = context;
            _universityService = universityService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] PaginationModel pagination)
        {
            var recordsCount = await _context
                .Universities
                .AsNoTracking()
                .CountAsync();

            var data = await _context
                .Universities
                .Include(c => c.Country)
                .OrderByDescending((m => m.Country))
                .Skip(pagination.PageSize * (pagination.CurrentPage - 1))
                .Take(pagination.PageSize)
                .AsNoTracking()
                .ToListAsync();

            var totalPages = recordsCount / pagination.PageSize;
            var pageSizeOptions = new SelectList(new List<int> { 5, 20, 50 }, pagination.PageSize);

            var model = new UniversityIndexModel
            {
                UniversitiesList = data.Select((universityEntity) => new UniversityListItemModel
                {
                    UniversityId = universityEntity.Id,
                    UniversityName = universityEntity.UniversityName,
                    CountryName = universityEntity.Country?.CountryName,
                }),
                Pagination = new PaginationModel
                {
                    CurrentPage = pagination.CurrentPage,
                    TotalPages = totalPages,
                    PageSizeOptions = pageSizeOptions,
                    PageSize = pagination.PageSize
                }
            };
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int? universityId, int? systemId)
        {
            if (universityId == null)
            {
                return NotFound();
            }

            var rankingSystems = await _universityService.GetRankingSystems();

            var university = await _universityService.GetUniversity((int)universityId);
            if (university == null)
            {
                return NotFound();
            }

            var startingYear = await _universityService.GetRankingStartingYear();

            IEnumerable<CriteriaRecord> data;
            if (systemId != null)
            {
                data = await _universityService.GetRankingCriteriaData((int)systemId, (int)universityId);
            }
            else
            {
                data = new List<CriteriaRecord>();
            }

            return View(new CriteriaModel
            {
                CriteriaUniversity = new CriteriaUniversityModel
                {
                    UniversityId = university.Id,
                    UniversityName = university.UniversityName,
                    Country = university.Country.CountryName
                },
                CriteriaRecords = data,
                RankingSystems = rankingSystems.Select(x => new CriteriaRankingSystem
                {
                    Id = x.Id,
                    Name = x.SystemName
                }),
                StartYear = startingYear,
            });
        }

      
        [HttpGet]
        public async Task<ViewResult> AddRanking(int universityId, int? rankingSystemId)
        {
            var dbRankingSystems = _context.RankingSystems.ToList();

            var years = Enumerable.Range(2017, DateTime.Now.Year - 2016).ToList();

            var criteria = await _context.RankingCriteria.Where(x => x.RankingSystemId == rankingSystemId)
                .ToListAsync();

            var rankingSystemsOptions = dbRankingSystems
                .Select(rs => new SelectListItem
                {
                    Value = rs.Id.ToString(),
                    Text = rs.SystemName
                })
                .ToList();
            rankingSystemsOptions.Insert(0, new SelectListItem { Value = "", Text = "-", Selected = true });

            var criteriaOptions = new SelectList(criteria, "Id", "CriteriaName");
            var yearsOptions =
                new SelectList(years.Select(x => new { Value = x, Text = x }), "Value", "Text").Reverse();

            var model = new AddRankingModel
            {
                UniversityId = universityId,
                RankingSystemId = rankingSystemId,
                RankingSystemsOptions = rankingSystemsOptions,
                CriteriaOptions = criteriaOptions,
                YearsOptions = yearsOptions,
            };
            return View(model);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRanking(AddRankingModel model)
        {
            if (ModelState.IsValid)
            {
                var ranking = new UniversityRankingYearEntity
                {
                    UniversityId = model.UniversityId,
                    RankingCriteriaId = model.RankingCriteriaId,
                    Year = model.Year,
                    Score = model.Score
                };

                _context.UniversityRankingYears.Add(ranking);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", new { universityId = model.UniversityId });
            }

            var dbRankingSystems = _context.RankingSystems.ToList();
            var criteria = await _context.RankingCriteria.Where(x => x.RankingSystemId == model.RankingSystemId)
                .ToListAsync();
            var rankingSystemsOptions = dbRankingSystems
                .Select(rs => new SelectListItem
                {
                    Value = rs.Id.ToString(),
                    Text = rs.SystemName
                })
                .ToList();
            rankingSystemsOptions.Insert(0, new SelectListItem { Value = "", Text = "-", Selected = true });
            
            var years = Enumerable.Range(2017, DateTime.Now.Year - 2016).ToList();

            model.RankingSystemsOptions = rankingSystemsOptions;
            model.CriteriaOptions = new SelectList(criteria, "Id", "CriteriaName");
            model.YearsOptions =
                new SelectList(years.Select(x => new { Value = x, Text = x }), "Value", "Text").Reverse();

            return View(model);
        }
    }
}