using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models;

[Table("computers")]
public class KomputerEntity
{
    
   // public int Id { get; set; }
    
    
    [Microsoft.Build.Framework.Required]

    public Category Category { get; set; }
    
    [Microsoft.Build.Framework.Required]

    public  string? Nazwa { get; set; }
    
    [Microsoft.Build.Framework.Required]

    public  string? Procesor { get; set; }
    
    [Microsoft.Build.Framework.Required]

    public int? Pamiec { get; set; }
    
    [Microsoft.Build.Framework.Required]
    [Column("karta graficzna")]
    public string? KartaGraficzna { get; set; }
    
    [Microsoft.Build.Framework.Required]

    public string? Producent { get; set; }
    
    [Microsoft.Build.Framework.Required]
    [DataType(DataType.Date)]
    [ValidationOfDate]
    [Column("data produkcji")]
    public DateTime? DataProdukcji { get; set; }
    
    [HiddenInput]

    public  int Id { get; set; }
    
    [HiddenInput]

    public DateTime Created { get; set; } 
}