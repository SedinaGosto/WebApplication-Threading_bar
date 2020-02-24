using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication6.Helper;
using WebApplication6.Models;
using WebApplication6.ViewModels;
using Microsoft.AspNetCore.Mvc;
using WebApplication6.EF;

namespace WebApplication6Controllers
{
    [Autorizacija(administrator: true, uposlenik: true,klijent:false)]
    public class SesijaController : Controller
    {
        private MojContext _db;

        public SesijaController(MojContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            SesijaIndexVM model = new SesijaIndexVM();
            model.Rows = _db.AutorizacijskiTokenKorisnik.Select(s => new SesijaIndexVM.Row
            {
                VrijemeLogiranja = s.VrijemeEvidentiranja,
                token = s.Vrijednost,
                IpAdresa = s.IpAdresa
            }).ToList();
            model.TrenutniToken = HttpContext.GetTrenutniToken();
            return View(model);
        }

        public IActionResult Obrisi(string token)
        {
            AutorizacijskiTokenKorisnik obrisati = _db.AutorizacijskiTokenKorisnik.FirstOrDefault(x => x.Vrijednost == token);
            if (obrisati != null)
            {
                _db.AutorizacijskiTokenKorisnik.Remove(obrisati);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}