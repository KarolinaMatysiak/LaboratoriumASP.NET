using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public enum Category
{
    [Display(Name = "Desktop")]Low = 1, 
    [Display(Name = "All-in-One")]Normal = 2, 
    [Display(Name = "Nettop/Mini-PC")]High = 3,
    [Display(Name = "Gaming")]Urgent = 4 
}