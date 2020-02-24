using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebApplication6.Models;
using WebApplication6.ViewModels;
using WebApplication6.EF;
using WebApplication6.Helper;

namespace WebApplication1.Controllers
{
    [Autorizacija(administrator: true, uposlenik: false, klijent: false)]
    public class ClanciKategorijaController: Controller
    {

        MojContext _db;

        public ClanciKategorijaController(MojContext db)
        {
            _db = db;

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Snimi(ClanciKategorijaDodaj vm)
        {
            if(!ModelState.IsValid)
            {
                return View("Dodaj", vm);
            }

            ClanciKategorija n = new ClanciKategorija
            {
                ClanciKategorijaID = vm.ClanakKategorijaID,
                Naziv = vm.Naziv,
                DatumKreiranja=vm.DatumKreiranja

            };


            _db.Add(n);
            _db.SaveChanges();
            return Redirect("/Clanak/Dodaj");
        }

        public IActionResult Dodaj()
        {
            ClanciKategorijaDodaj model = new ClanciKategorijaDodaj();
            return View(model);
        }
    }
}
