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
    public class KlijentNotifikacijaController : Controller
    {

        MojContext _db;
        public KlijentNotifikacijaController(MojContext db)
        {

            _db = db;

        }
        public IActionResult Notifikacije(int TrenutnaStranica = 1, int VelicinaStranice = 5)
        {
            KlijentT k = HttpContext.GetLogiraniKlijent();

            KlijentNotifikacijaVM model = new KlijentNotifikacijaVM()
            {
                lista = _db.KlijentNotifikacija.Where(x => x.IsProcitano == false && x.KlijentId == k.KlijentID).Select(a => new KlijentNotifikacijaVM.Row
                {
                    NotifikacijaId = a.NotifikacijaId,
                    Termin = a.Rezervacija.Termin.VrijemeTermina,
                    tretman = a.Rezervacija.Tretman.Naziv,
                    Uposlenik=a.Rezervacija.Termin.Korisnik.Ime + " " + a.Rezervacija.Termin.Korisnik.Prezime



                }).ToList()
            };
            //Paging
            var items = model.lista.OrderByDescending(x => x.Termin).Skip((TrenutnaStranica - 1) * VelicinaStranice).Take(VelicinaStranice).ToList();
            ViewData["TrenutnaStranica"] = TrenutnaStranica;

            return View(items);

        }
        public IActionResult OznaciProcitano(int id)
        {
            KlijentNotifikacija n = _db.KlijentNotifikacija.Find(id);
            n.IsProcitano = true;
            _db.SaveChanges();
         

            return RedirectToAction("Notifikacije");
        }
    }
}