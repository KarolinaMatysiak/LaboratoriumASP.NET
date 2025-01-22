using WebApp.Models.University;

namespace WebApp.Models;

public interface IUniversityService
{
    Task<List<RankingSystemEntity>> GetRankingSystems();

    Task<UniversityEntity?> GetUniversity(int universityId);

    Task<int> GetRankingStartingYear();

    Task<IEnumerable<CriteriaRecord>> GetRankingCriteriaData(int systemId, int universityId);
}