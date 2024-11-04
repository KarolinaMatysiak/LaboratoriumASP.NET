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
            return View("Index", _computers);
        }
        
        public ActionResult Form()
        {
            return View();
        }
        
        [HttpGet("{id:int}")]
        public IActionResult Edit(int id)
        {
    
            if (_computers.Keys.Contains(id))
            {
                return View("EditForm", _computers[id]);
            }
            else
            {
                return NotFound();
            };
        }

        [HttpPost]
        public IActionResult Save(Komputer komputer)
        {
            if (ModelState.IsValid)
            {
                _computers[komputer.Id] = komputer;
                return RedirectToAction("Index");
            }
            else
            {
                return View("EditForm", komputer); 
            }
        }
        public IActionResult Delete(int id)
        {
            _computers.Remove(id);
            return View("Index", _computers);
        }
        
        
        public IActionResult Details(int id)
        {
            return View(_computers[id]);
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
