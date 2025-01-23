using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models.University;

public class UniversityListItemModel
{
    [Required]
    public int UniversityId { get; set; }

    [Display(Name = "University")] public string? UniversityName { get; set; }

    [Display(Name = "Country")] public string? CountryName { get; set; }
}

public class PaginationModel
{
    public int CurrentPage { get; set; } = 1;
    
    public int TotalPages { get; set; }

    [Display(Name = "Items per page:")] 
    public int PageSize { get; set; } = 20;
    
    public SelectList? PageSizeOptions { get; set; }
}

public class UniversityIndexModel
{
    public IEnumerable<UniversityListItemModel>? UniversitiesList { get; set; }
    
    [Required]
    public PaginationModel Pagination { get; set; }
}