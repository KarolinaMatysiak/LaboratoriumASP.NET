using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Models;

public class Komputer
{
    [MinLength(length:5, ErrorMessage ="Nazwa musi mieć co najmniej 5 znaków")]
    [Required(ErrorMessage ="Proszę podać nazwe komputera")]
    public string nazwa { get; set; }
    
    [MinLength(length:5, ErrorMessage ="Nazwa musi mieć co najmniej 5 znaków")]
    [Required(ErrorMessage ="Proszę podać model procesora")]
    public string procesor { get; set; }
    
    [Range(1, 512, ErrorMessage = "Wielkość musi być w zakresie 1-512 GB")]
    [Required(ErrorMessage ="Proszę podać wielkość pamięci")]
    public int pamiec { get; set; }
    
    [MinLength(length:5, ErrorMessage ="Nazwa musi mieć co najmniej 5 znaków")]
    [Required(ErrorMessage ="Proszę podać model karty graficznej")]
    public string kartaGraficzna { get; set; }
    
    [MinLength(length:5, ErrorMessage ="Nazwa musi mieć co najmniej 5 znaków")]
    [Required(ErrorMessage ="Proszę podać nazwe producenta")]
    public string producent { get; set; }
    
    [DataType(DataType.Date,ErrorMessage = "Prosze podac date produkcji w odpowiednim formacie")]
    [Required(ErrorMessage ="Proszę podać date produkcji")]
    public string dataProdukcji { get; set; }
    
    [HiddenInput]
    public int Id { get; set; }
    
}