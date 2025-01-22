using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models.University;

public class CriteriaRankingSystem
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class CriteriaUniversityModel
{
    public int UniversityId { get; set; }
    
    [Display(Name = "University")]
    public string UniversityName { get; set; }
    
    [Display(Name = "Country")]
    public string Country { get; set; }
}

public class CriteriaRecord
{
    public string? Name { get; set; }
    public Dictionary<int, int?> Data { get; set; }
}

public class CriteriaModel
{
    public CriteriaUniversityModel? CriteriaUniversity { get; set; }
    public IEnumerable<CriteriaRecord> CriteriaRecords { get; set; }
    
    [Display(Name = "Ranking System")]
    public IEnumerable<CriteriaRankingSystem> RankingSystems { get; set; }
    public int? SelectedRankingId { get; set; }
    public int? StartYear { get; set; }
    
    public int UniversityId { get; set; }
}