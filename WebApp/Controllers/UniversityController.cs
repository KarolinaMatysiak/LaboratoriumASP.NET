using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.University;

namespace WebApp.Controllers
{
    public class UniversityController : Controller
    {
        private readonly UniversityDbContext _context;

        public UniversityController(UniversityDbContext context)
        {
            _context = context;
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

            var rankingSystems = await _context
                .RankingSystems
                .ToListAsync();

            var model = data.Select((universityEntity) => new UniversityIndex
            {
                UniversityId = universityEntity.Id,
                UniversityName = universityEntity.UniversityName,
                CountryName = universityEntity.Country?.CountryName,
                RankingSystems = rankingSystems.Select((rankingSystem) => new UniversityIndexRankingSystem
                    {
                        RankingSystemId = rankingSystem.Id,
                        RankingSystemName = rankingSystem.SystemName
                    }
                )
            });
            return View(model);
        }

        // GET: University/Details/5
        public async Task<IActionResult> Details(int? systemId, int? universityId)
        {
            if (systemId == null || universityId == null)
            {
                return NotFound();
            }

            var criteriaList = await _context.RankingCriteria
                .Where(e => e.RankingSystemId == systemId)
                .Select(e => new
                {
                    RankingCriteria = e,
                    FilteredUniversityRankingYears = e.UniversityRankingYearEntity
                        .Where(ury => ury.UniversityId == universityId)
                        .ToList()
                })
                .ToListAsync();

            var university = await _context.Universities
                .Include(u => u.Country)
                .FirstOrDefaultAsync(m => m.Id == universityId);
            if (university == null)
            {
                return NotFound();
            }

            var oldestCriterion = await _context.UniversityRankingYears
                .Where(x => x.UniversityId == universityId)
                .Where(x => x.RankingCriteria.RankingSystemId == systemId)
                .FirstOrDefaultAsync();
            if (oldestCriterion == null)
            {
                return NotFound();
            }
            
            var data = new Dictionary<string, Dictionary<int, int?>>();
            foreach (var criterion in criteriaList)
            {
                data.Add(criterion.RankingCriteria.CriteriaName, new Dictionary<int, int?>());

                foreach (var ranking in criterion.FilteredUniversityRankingYears)
                {
                    if (ranking.Year is not null && ranking.Score is not null)
                    {
                        data[criterion.RankingCriteria.CriteriaName].Add((int)ranking.Year, (int)ranking.Score);
                    }
                }
            }


            return View(new CriteriaModel
            {
                CriteriaUniversity = new CriteriaUniversityModel
                {
                    UniversityName = university.UniversityName,
                    Country = university.Country.CountryName
                },
                CriteriaRecords = criteriaList.Select(element => new CriteriaRecord
                {
                    Name = element.RankingCriteria.CriteriaName,
                    Data = data.GetValueOrDefault(element.RankingCriteria.CriteriaName, new Dictionary<int, int?>())
                }),
                StartYear = oldestCriterion.Year,
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
    }
}