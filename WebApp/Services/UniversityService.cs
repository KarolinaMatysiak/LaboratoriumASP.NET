using Microsoft.EntityFrameworkCore;
using WebApp.Models.University;

namespace WebApp.Models;

public class UniversityService : IUniversityService
{
    private readonly UniversityDbContext _context;

    public UniversityService(UniversityDbContext context)
    {
        _context = context;
    }

    public async Task<List<RankingSystemEntity>> GetRankingSystems()
    {
        return await _context
            .RankingSystems
            .ToListAsync();
    }

    public async Task<UniversityEntity?> GetUniversity(int universityId)
    {
        return await _context.Universities
            .Include(u => u.Country)
            .FirstOrDefaultAsync(m => m.Id == universityId);
    }

    public async Task<int> GetRankingStartingYear()
    {
        var oldestCriterion = await _context.UniversityRankingYears
            .OrderBy(x => x.Year)
            .FirstOrDefaultAsync();

        return oldestCriterion?.Year ?? DateTime.Now.Year;
    }

    public async Task<IEnumerable<CriteriaRecord>> GetRankingCriteriaData(int systemId, int universityId)
    {
        var criteriaList = await _context.RankingCriteria
            .Where(criteria => criteria.RankingSystemId == systemId)
            .Select(criteria => new
            {
                RankingCriteria = criteria,
                FilteredUniversityRankingYears = criteria.UniversityRankingYearEntity
                    .Where(universityRanking => universityRanking.UniversityId == universityId)
                    .ToList()
            })
            .ToListAsync();

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

        return criteriaList.Select(element => new CriteriaRecord
        {
            Name = element.RankingCriteria.CriteriaName,
            Data = data.GetValueOrDefault(element.RankingCriteria.CriteriaName, new Dictionary<int, int?>())
        });
    }
}