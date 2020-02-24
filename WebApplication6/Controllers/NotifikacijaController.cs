using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication6.EF;
using WebApplication6.Helper;
using WebApplication6.Models;
using WebApplication6.ViewModels;

namespace WebApplication6.Controllers
{

    public class NotifikacijaController : Controller
    {

        MojContext _db;
        public NotifikacijaController(MojContext db)
        {

            _db = db;

        }
        public IActionResult Notifikacije(int TrenutnaStranica = 1, int VelicinaStranice = 5)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
     
            NotifVM model = new NotifVM()
            {
                lista = _db.PoslataNotifikacija.Where(x => x.IsProcitano == false && x.rezervacija.Termin.KorisnikId == k.KorisnikId).Select(a => new NotifVM.Row
                {
                    NotifikacijaId = a.Id,
                    Termin = a.rezervacija.Termin.VrijemeTermina,
                    tretman = a.rezervacija.Tretman.Naziv,
                    NazivKlijenta = a.rezervacija.Klijent.Ime + " " + a.rezervacija.Klijent.Prezime


                }).ToList()
            };
            //Paging
            var items = model.lista.OrderByDescending(x => x.Termin).Skip((TrenutnaStranica - 1) * VelicinaStranice).Take(VelicinaStranice).ToList();
            ViewData["TrenutnaStranica"] = TrenutnaStranica;

            return View(items);
      
        }
        public IActionResult OznaciProcitano(int id)
        {
            PoslataNotifikacija n = _db.PoslataNotifikacija.Find(id);
            Rezervacija r = _db.Rezervacija.Find(n.RezervacijaId);
            n.IsProcitano = true;
            KlijentNotifikacija nova = new KlijentNotifikacija
            {
                DatumSlanja = DateTime.Now,
                IsProcitano = false,
                RezervacijaId = n.RezervacijaId,
                KlijentId = r.KlijentId,


            };
            _db.KlijentNotifikacija.Add(nova);
            _db.SaveChanges();

            return RedirectToAction("Notifikacije");
        }
        public IActionResult Odobri (int id)
        {
            PoslataNotifikacija n = _db.PoslataNotifikacija.Where(x => x.RezervacijaId == id).FirstOrDefault();
            Rezervacija r = _db.Rezervacija.Find(id);
            n.IsProcitano = true;
            KlijentNotifikacija nova = new KlijentNotifikacija
            {
                DatumSlanja = DateTime.Now,
                IsProcitano = false,
                RezervacijaId = n.RezervacijaId,
                KlijentId = r.KlijentId,


            };
            _db.KlijentNotifikacija.Add(nova);
            _db.SaveChanges();

            return Redirect("/Rezervacija/PregledajRzerevacije");
        }
   
        public IActionResult OdobriKlijent(int id)
        {
            PoslataNotifikacija n = _db.PoslataNotifikacija.Where(x => x.RezervacijaId == id).FirstOrDefault();
            Rezervacija r = _db.Rezervacija.Find(id);
            
        
            KorisniciUloge list = _db.KorisniciUloge.Include(x=>x.Korisnik).Include(x=>x.Uloga).Where(x => x.KorisnikId == HttpContext.GetLogiraniKorisnik().KorisnikId).FirstOrDefault();

            if (list.Uloga.Naziv == "Administrator")
            {


                n.IsProcitano = true;
                KlijentNotifikacija nova = new KlijentNotifikacija
                {
                    DatumSlanja = DateTime.Now,
                    IsProcitano = false,
                    RezervacijaId = n.RezervacijaId,
                    KlijentId = r.KlijentId,


                };
                _db.KlijentNotifikacija.Add(nova);
                _db.SaveChanges();
            }
            else{
                TempData["ErrorMessage"] = "Nemate pravo pristupa";
            }

            return Redirect("/Rezervacija/Index/"+ r.KlijentId);
        }
    }
}