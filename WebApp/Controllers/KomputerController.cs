using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Controllers
{
    
    [Authorize(Roles = "admin,user")]
    public class KomputerController : Controller

    {
   

     //   public Komputer AppDbContext;
             

        private readonly IKomputerService _komputerService;
        public KomputerController(IKomputerService komputerService)
        {
            _komputerService = komputerService;
        }
        


        // GET: FormController
        
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(_komputerService.FindAll());
        }
        
        public ActionResult Form()
        {
            var model = new Komputer();
            model.Organizations = _komputerService.GetAllOrganizations()
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(), 
                    Text = e.Name
                })
                .ToList();
            return View(model);
        }
        
        [HttpGet("{id:int}")]
        public IActionResult Edit(int id)
        {
            var komputer = _komputerService.FindById(id);
            if (komputer is not null)
            {
                komputer.Organizations = _komputerService.GetAllOrganizations()
                    .Select(e => new SelectListItem
                    {
                        Value = e.Id.ToString(), 
                        Text = e.Name
                    })
                    .ToList();
                
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
            
            //Microsoft.EntityFrameworkCore.Design i Sqlite i po prostu core
        }
}
}
