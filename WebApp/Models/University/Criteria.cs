using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models.University;

public class CriteriaRankingSystem
{
    [Display(Name = "Criteria Ranking System Id")]
    public int Id { get; set; }
    
    [Display(Name = "Criteria Ranking System Name")]
    public string Name { get; set; }
}

public class CriteriaUniversityModel
{
    [Display(Name = "University Id")]
    [Required]
    public int UniversityId { get; set; }
    
    [Display(Name = "University")]
    [Required]
    public string UniversityName { get; set; }
    
    [Display(Name = "Country")]
    [Required]
    public string Country { get; set; }
}

public class CriteriaRecord
{
    [Display(Name = "Criteria Name")]
    public string? Name { get; set; }
    
    [Display(Name = "Criteria Data")]
    public Dictionary<int, int?> Data { get; set; }
}

public class CriteriaModel
{
    [Display(Name = "Criteria University")]
    public CriteriaUniversityModel? CriteriaUniversity { get; set; }
    
    [Display(Name = "Criteria Records")]
    [Required]
    public IEnumerable<CriteriaRecord> CriteriaRecords { get; set; }
    
    [Display(Name = "Ranking System")]
    [Required]
    public IEnumerable<CriteriaRankingSystem> RankingSystems { get; set; }
    
    [Display(Name = "Selected Ranking Id")]
    public int? SelectedRankingId { get; set; }
    
    [Display(Name = "Start Year")]
    public int? StartYear { get; set; }
    
    [Display(Name = "University Id")]
    [Required]
    public int UniversityId { get; set; }
}