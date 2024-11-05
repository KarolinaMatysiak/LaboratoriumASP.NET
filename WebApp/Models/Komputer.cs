using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models;

public class Komputer
{
    [Required(ErrorMessage ="Proszę uzupełnić pole")]
    [Display(Name = "Kategoria")]
    public Category Category { get; set; }
    
    [Required(ErrorMessage ="Proszę podać nazwe komputera")]
    [MinLength(length:5, ErrorMessage ="Nazwa musi mieć co najmniej 5 znaków")]
    [Display(Name = "Nazwa")]
    public  string? Nazwa { get; set; }
    
    [Required(ErrorMessage ="Proszę podać model procesora")]
    [MinLength(length:5, ErrorMessage ="Nazwa musi mieć co najmniej 5 znaków")]
    [Display(Name = "Procesor")]
    public  string? Procesor { get; set; }
    
    [Required(ErrorMessage ="Proszę podać wielkość pamięci")]
    [Range(1, 512, ErrorMessage = "Wielkość musi być w zakresie 1-512 GB")]
    [Display(Name = "Pamięć")]
    public int? Pamiec { get; set; }
    
    [Required(ErrorMessage ="Proszę podać model karty graficznej")]
    [MinLength(length:5, ErrorMessage ="Nazwa musi mieć co najmniej 5 znaków")]
    [Display(Name = "Karta graficzna")]
    public string? KartaGraficzna { get; set; }
    
    [Required(ErrorMessage ="Proszę podać nazwe producenta")]
    [MinLength(length:5, ErrorMessage ="Nazwa musi mieć co najmniej 5 znaków")]
    [Display(Name = "Producent")]
    public string? Producent { get; set; }
    
    [Required(ErrorMessage ="Proszę podać date produkcji")]
    [DataType(DataType.Date,ErrorMessage = "Prosze podac date produkcji w odpowiednim formacie")]
    [ValidationOfDate(ErrorMessage = "Data produkcji musi być mniejsza lub równa dzisiejszej dacie.")]
    [Display(Name = "Data produkcji")]
    public DateTime? DataProdukcji { get; set; }
    
    [HiddenInput]
    [Display(Name = "ID")]
    public  int Id { get; set; }
    
    [HiddenInput]
    [Display(Name = "Data utworzenia")]
    public DateTime Created { get; set; } 
    
}