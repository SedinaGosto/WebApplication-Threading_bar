using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.EF;
using WebApplication6.Helper;
using WebApplication6.Models;
using WebApplication6.ViewModels;

namespace WebApplication6.Controllers
{
  
    public class KlijentController: Controller
    {
        MojContext _db;

        public KlijentController(MojContext db)
        {
            _db = db;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Snimi(KlijentDodajVM vm)
        {
            if (vm.KlijentID == 0)
            {


                KlijentT x = new KlijentT
                {
                    KlijentID = vm.KlijentID,
                    Ime = vm.Ime,
                    Prezime=vm.Prezime,
                    Email=vm.Email,
                    BrojTelefona=vm.BrojTelefona,
                    GradID = vm.GradID,
                    
                  


                };
                _db.Klijent.Add(x);
            }
            else
            {
                KlijentT t = _db.Klijent.Find(vm.KlijentID);
             
                t.Ime = vm.Ime;
                t.Prezime = vm.Prezime;
             
                t.Email = vm.Email;
                t.BrojTelefona = vm.BrojTelefona;
                t.GradID = vm.GradID;
                _db.SaveChanges();
            }
            _db.SaveChanges();
            TempData["error"] = "Uspjesno pohranjeno";

            return Redirect("/Klijent/Dodaj");
        }

   


        public IActionResult Dodaj()
        {
            KlijentT t = HttpContext.GetLogiraniKlijent();
            KlijentDodajVM model = new KlijentDodajVM();
            if(t!=null)
            {
                model.KlijentID = t.KlijentID;
                model.Ime = t.Ime;
                model.Prezime = t.Prezime;

                model.Email = t.Email;
                model.BrojTelefona = t.BrojTelefona;
                model.GradID = t.GradID;
                TempData["Succes"] = TempData["error"];
                return PartialView(model);
            }



            model.gradovi = _db.Grad.Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Naziv }).ToList();
                model.DatumRodjenja = DateTime.Now;
            return PartialView(model);






        }
        [Autorizacija(administrator: true, uposlenik: false, klijent: false)]
        public IActionResult Obrisi(int ID)
        {
            KlijentT x = _db.Klijent.Find(ID);
            _db.Klijent.Remove(x);
            _db.SaveChanges();


            return Redirect("/Klijent/Index");
        }
        [Autorizacija(administrator: true, uposlenik: true, klijent: false)]
        public IActionResult Index()
        {
            KlijentIndexVM model = new KlijentIndexVM
            {


                rows = _db.Klijent.Select(o => new KlijentIndexVM.row
                {

                    KlijentID = o.KlijentID,
                    Ime = o.Ime,
                    Prezime = o.Prezime,
                KorisnickoIme=o.KorisnickoIme,
                    Email = o.Email,
                    BrojTelefona = o.BrojTelefona,
                    Grad = o.Grad.Naziv


                }).ToList()
            };


            return PartialView(model);
        }

        public IActionResult PromijeniLozinku()
        {
            LozinkaVM model = new LozinkaVM();
            return View(model);
        }

        public IActionResult Promijena(LozinkaVM model)
        {
             if(model.NovaLozinka!=model.PotvrdaLozinke)
            {
                return View(model);
            }

            KlijentT k = HttpContext.GetLogiraniKlijent();
            k.LozinkaSalt = GenerateHashClass.GenerateSalt();
            k.LozinkaHash = GenerateHashClass.GenerateHash(k.LozinkaSalt, model.NovaLozinka);
            _db.SaveChanges();
            return Redirect("/KlijentNotifikacija/Notifikacije");
        }

    }
}
