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
    
    public IActionResult Calculator(double? a, Operator? op, double? b)
    {
        
        ViewBag.Op = op;
        ViewBag.a = a;
        ViewBag.b = b;
        
        double result = 0;
        if (a is not null & b is not null)
        {
            switch (op)
            {
                case Operator.Unknown:
                    return View("Error");

                case Operator.Add:
                    result = (double)(a + b);
                    ViewBag.Op = "+";
                    break;
                case Operator.Mul:
                    result = (double)(a * b);
                    ViewBag.Op = "*";
                    break;
                case Operator.Sub:
                    result = (double)(a - b);
                    ViewBag.Op = "-";
                    break;
                case Operator.Div:
                    result = (double)(a / b);
                    ViewBag.Op = ":";
                    break;
                default:
                    return View("Error");
            }
        }
        else
        {
            return View("Error");
        }

        ViewBag.result = result;
        
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
