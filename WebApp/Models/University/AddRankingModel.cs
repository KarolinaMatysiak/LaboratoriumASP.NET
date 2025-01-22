using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models.University;

public class AddRankingModel
{
    [HiddenInput]
    [Required]
    public int UniversityId { get; set; }
    
    [Display(Name = "System")]
    [Required]
    public int RankingSystemId { get; set; }
    
    [Display(Name = "Criteria")]
    [Required]
    public int RankingCriteriaId { get; set; }
    
    [Display(Name = "Year")]
    [Required]
    [Range(2017, int.MaxValue)]
    public int Year { get; set; }
    
    [Display(Name = "Score")]
    [Required]
    [Range(0, 100)]
    public int Score { get; set; }
    
    [HiddenInput]
    [ValidateNever]
    public IEnumerable<SelectListItem> RankingSystemsOptions { get; set; }
    
    [HiddenInput]
    [ValidateNever]
    public IEnumerable<SelectListItem> CriteriaOptions { get; set; }
    
    [HiddenInput]
    [ValidateNever]
    public IEnumerable<SelectListItem> YearsOptions { get; set; }
}