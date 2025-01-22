using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Models.University;

namespace WebApp.Controllers
{
    public class UniversityController : Controller
    {
        private readonly UniversityDbContext _context;
        private readonly IUniversityService _universityService;

        public UniversityController(UniversityDbContext context, IUniversityService universityService)
        {
            _context = context;
            _universityService = universityService;
        }

        // GET: University
        //stronicowanie
        public async Task<IActionResult> Index(int page = 1, int size = 20)
        {
            var data = await _context
                .Universities
                .Include(c => c.Country)
                .OrderByDescending((m => m.Country))
                .Skip(size * (page - 1))
                .Take(size)
                .AsNoTracking()
                .ToListAsync();

            var model = data.Select((universityEntity) => new UniversityIndex
            {
                UniversityId = universityEntity.Id,
                UniversityName = universityEntity.UniversityName,
                CountryName = universityEntity.Country?.CountryName,
            });
            return View(model);
        }

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

        // GET: University/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id");
            return View();
        }

        // POST: University/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CountryId,UniversityName")] UniversityEntity universityEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(universityEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", universityEntity.CountryId);
            return View(universityEntity);
        }

        // GET: University/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var university = await _context.Universities.FindAsync(id);
            if (university == null)
            {
                return NotFound();
            }

            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", university.CountryId);
            return View(university);
        }

        // POST: University/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,CountryId,UniversityName")] UniversityEntity universityEntity)
        {
            if (id != universityEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(universityEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UniversityExists(universityEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", universityEntity.CountryId);
            return View(universityEntity);
        }

        // GET: University/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var university = await _context.Universities
                .Include(u => u.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (university == null)
            {
                return NotFound();
            }

            return View(university);
        }

        // POST: University/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var university = await _context.Universities.FindAsync(id);
            if (university != null)
            {
                _context.Universities.Remove(university);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UniversityExists(int id)
        {
            return _context.Universities.Any(e => e.Id == id);
        }

        public IActionResult AddRanking(int universityId)
        {
            var dbRankingSystems = _context.RankingSystems.ToList();
            var dbCriteria = _context.RankingCriteria.ToList();
            var years = Enumerable.Range(2017, DateTime.Now.Year - 2016).ToList();

            var rankingSystemsOptions = new SelectList(dbRankingSystems, "Id", "SystemName");
            var criteriaOptions = new SelectList(dbCriteria, "Id", "CriteriaName");
            var yearsOptions =
                new SelectList(years.Select(x => new { Value = x, Text = x }), "Value", "Text").Reverse();

            var model = new AddRankingModel
            {
                UniversityId = universityId,
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
            var dbCriteria = _context.RankingCriteria.ToList();
            var years = Enumerable.Range(2017, DateTime.Now.Year - 2016).ToList();

            model.RankingSystemsOptions = new SelectList(dbRankingSystems, "Id", "SystemName");
            model.CriteriaOptions = new SelectList(dbCriteria, "Id", "CriteriaName");
            model.YearsOptions = new SelectList(years.Select(x => new { Value = x, Text = x }), "Value", "Text").Reverse();

            return View(model);
        }
    }
}