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
        
        [HttpPost]
        public IActionResult Result( [FromForm] Calculator model)
        {
            if (!model.IsValid())
            {
                return View("Error");
            }
            return View(model);
        }

    }
}
