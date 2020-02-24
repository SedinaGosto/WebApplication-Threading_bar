using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication6.EF;
using WebApplication6.Helper;
using WebApplication6.Models;
using WebApplication6.ViewModels;

namespace WebApplication6.Controllers
{
    public class OcjenaController : Controller
    {
        MojContext _db;
   
     
        public OcjenaController(MojContext db)
        {
            _db = db;
          
        }
        public IActionResult Index()
        {

            OcjenaIndexVM model = new OcjenaIndexVM
            {

                rows = _db.Ocjena.Select(o => new OcjenaIndexVM.row
                {
                   OcjenaId=o.OcjenaId,
                   DatumOcjenjivanja=o.DatumOcjenjivanja,
                   Klijent=o.Klijent.Ime + " "+ o.Klijent.Prezime,
                   Korisnik=o.Korisnik.Ime+ " "+o.Korisnik.Prezime,
                   OcjenaInt=o.OcjenaInt



                }).ToList()

            };


            return View(model);
        }
        public IActionResult Dodaj(int id)
        {
            KlijentT logirani = HttpContext.GetLogiraniKlijent();
            Korisnik k = _db.Korisnik.Find(id);
            OcjenaDodajVM model = new OcjenaDodajVM { 
            KlijentId= logirani.KlijentID,
            Klijent=logirani.Ime + " "+ logirani.Prezime,
            Korisnik=k.Ime + " "+ k.Prezime,
            KorisnkId=id,
           DatumOcjenjivanja=DateTime.Now
            

        };
            return View(model);
        }
        public IActionResult Snimi(OcjenaDodajVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Dodaj", vm);
            }

            Ocjena n = new Ocjena
            {
                KlijentId = vm.KlijentId,
                KorisnikId = vm.KorisnkId,
                DatumOcjenjivanja = vm.DatumOcjenjivanja,
                OcjenaInt=vm.OcjenaInt

            };


            _db.Add(n);
            _db.SaveChanges();
            return Redirect("/Komentar/Index/"+ vm.KorisnkId);
        }
    }
}