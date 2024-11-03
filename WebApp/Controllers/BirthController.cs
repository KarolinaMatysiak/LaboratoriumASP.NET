using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class BirthController : Controller
    {
        // GET: BirthController
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Result([FromForm] Age model )
        {
         if (!model.IsValid())
         {
             return View("/Views/Home/Error.cshtml");
         }
         
         model.Birth();
         
         return View(model);
        }

        public IActionResult Form()
        {
            return View();
        }
    }
}
