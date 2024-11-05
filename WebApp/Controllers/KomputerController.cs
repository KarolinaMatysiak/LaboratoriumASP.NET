using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class KomputerController : Controller
    
    {
             

        private readonly IKomputerService _komputerService;
        public KomputerController(IKomputerService komputerService)
        {
            _komputerService = komputerService;
        }
        


        // GET: FormController
        public ActionResult Index()
        {
            return View(_komputerService.FindAll());
        }
        
        public ActionResult Form()
        {
            return View();
        }
        
        [HttpGet("{id:int}")]
        public IActionResult Edit(int id)
        {

            var komputer = _komputerService.FindById(id);
            if (komputer is not null)
            {
                return View("EditForm", komputer);
            }
            else
            {
                Console.WriteLine("NIE ZNALEZIONO TAKIEGO ID");
                return NotFound();
            };
        }

        [HttpPost]
        public IActionResult Save(Komputer komputer)
        {
            if (ModelState.IsValid)
            {
                _komputerService.Update(komputer);
                return RedirectToAction("Index");
            }
            else
            {
                return View("EditForm", komputer); 
            }
        }
        public IActionResult Delete(int id)
        {
           _komputerService.Delete(id);
            return View("Index", _komputerService.FindAll());
        }
        
        
        public IActionResult Details(int id)
        {
            return View(_komputerService.FindById(id));
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
                _komputerService.Add(komputer);
                return RedirectToAction("Index");
            }
            else
            {
                return View(komputer); 
            }
            
            
        }
    


}
}
