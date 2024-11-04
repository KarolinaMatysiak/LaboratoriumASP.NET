using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class KomputerController : Controller
    {
        static private Dictionary<int, Komputer> _computers = new Dictionary<int, Komputer>();


        // GET: FormController
        public ActionResult Index()
        {
            return View(_computers);
        }
        
        public ActionResult Form()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Komputer komputer)
        {
            if (ModelState.IsValid)
            {
                int id = _computers.Keys.Count != 0 ? _computers.Keys.Max() : 0;
                komputer.Id = id + 1;
                _computers.Add(komputer.Id, komputer);

                return RedirectToAction("Index");
            }
            else
            {
                return View("Form", komputer); 
            }
            
            
        }
    


}
}
