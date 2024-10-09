using System.Diagnostics;
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
    public IActionResult About()
    {
        return View();
    }
    public IActionResult Calculator(Operator? op, double? x, double? y)
    {
        //https//localhost:7226/Home/Calculator?op=add&x=4&y=1,5
        //var op= Request.Query["op"];
        //var x= double.Parse(Request.Query["x"]);
        //var y = double.Parse(Request.Query["y"]);
        if (x is null || y is null)
        {
            ViewBag.ErrorMessage = "Niepoprawny format";
            return View("CalculaterError");
        }

        if (op is null)
        {
           
            ViewBag.ErrorMessage = "Nieznany operator";
            return View("CalculaterError");
        }
        double? result = 0.0;
        switch (op)
        {
            case Operator.Add:
                result = x + y;
                ViewBag.Operator = "+";
                break;
            case Operator.Sub:
                result = x - y;
                ViewBag.Operator = "-";
                break;
                case Operator.Mul:
                result = x * y;
                ViewBag.Operator = "*";
                break;
                case Operator.Div:
                result = x / y;
                ViewBag.Operator = ":";
                break;
              
        }

        ViewBag.Result = result;
        ViewBag.X = x;
        ViewBag.Y = y;
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

public enum Operator
{
    Add,Sub,Mul,Div
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
