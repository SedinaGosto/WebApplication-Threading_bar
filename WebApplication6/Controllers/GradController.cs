using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication6.EF;
using WebApplication6.Helper;
using WebApplication6.Models;
using WebApplication6.ViewModels;

namespace WebApplication1.Controllers
{
   
    public class GradController : Controller
    {
        MojContext db;
        public GradController(MojContext _db)
        {
            db = _db;
        }

        [Autorizacija(administrator: true, uposlenik: true, klijent: true)]
        public IActionResult Index()
        {

            GradIndexVM model = new GradIndexVM
            {

                rows = db.Grad.Select(o => new GradIndexVM.row
                {
                    GradID = o.Id,
                    Naziv = o.Naziv,
                   


                }).ToList()

            };


            return PartialView(model);
        }
        [Autorizacija(administrator: true, uposlenik: false, klijent: false)]
        public IActionResult Obrisi(int ID)
        {
            Grad t = db.Grad.Find(ID);
            db.Remove(t);
            db.SaveChanges();

            return Redirect("/Grad/Index");


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Snimi(GradDodajVM vm)
        {
            if(!ModelState.IsValid)
            {
                return View("Dodaj", vm);
            }
            Grad n = new Grad
            {
                Id = vm.GradID,
                Naziv = vm.Naziv

            };


            db.Add(n);
            db.SaveChanges();
            return Redirect("/Klijent/Dodaj");
        }
        [Autorizacija(administrator: true, uposlenik: true, klijent: true)]
        public IActionResult Dodaj()
        {
            GradDodajVM model = new GradDodajVM();



            return PartialView(model);
        }

    }
}