using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CalculatorController : Controller
    {
        // GET: Calculator
        
        public ActionResult Index()
        {
            return View();
        }
        
        public IActionResult Form()
        {
            return View();
        }
        
        public IActionResult Result( Operator? op, double? a, double? b)
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

    }
}
