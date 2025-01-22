using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.University;

public class UniversityIndex
{
    public int UniversityId { get; set; }

    [Display(Name = "University")]
    public string? UniversityName { get; set; }
    
    [Display(Name = "Country")]
    public string? CountryName { get; set; }
}