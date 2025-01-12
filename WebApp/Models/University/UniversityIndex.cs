namespace WebApp.Models.University;

public class UniversityIndexRankingSystem
{
    public int RankingSystemId { get; set; }
    public string RankingSystemName { get; set; }
}

public class UniversityIndex
{
    public int UniversityId { get; set; }

    public string? UniversityName { get; set; }
    
    public string? CountryName { get; set; }
    
    public virtual IEnumerable<UniversityIndexRankingSystem> RankingSystems { get; set; }
}