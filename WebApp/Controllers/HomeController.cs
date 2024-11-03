using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
 
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult About()
    {
        return View();
    }
    
    public IActionResult Calculator(Operator op)
    {
        ViewBag.Op = op;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

/*ZADANIE 1
  Utworz metode Calcultor oraz widok w nim wyswietl tylko napis Kalkulator
  Dodaj link w nawigacji aplikacji do metody Calculator
  Wykonaj commit i wyslij do repozyterium przez push
  */
    
/*ZADANIE 2
 Napisz metode Age, ktora przyjmuje parametr z data urodzenia i wywietla wiek
 w latach miesiacach i dniach
 */
