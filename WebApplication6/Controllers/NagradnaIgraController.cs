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

    public class NagradnaIgraController: Controller
    {
        MojContext _db;

        public NagradnaIgraController(MojContext db)
        {
            _db = db;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Snimi(NagradnaIgraDodajVM vm)
        {
            if(!ModelState.IsValid)
            {
                NagradnaIgraDodajVM model = new NagradnaIgraDodajVM
                {
                    DatumPocetka = DateTime.Now,
                    DatumZavrsetka = DateTime.Now,

                    Nagrade = _db.Nagrada.Select(i => new SelectListItem { Value = i.NagradaID.ToString(), Text = i.Naziv }).ToList(),
                    Administrator = vm.Administrator,
                    AdministratorID = vm.AdministratorID,
                    Klijenti = _db.Klijent.Select(i => new SelectListItem { Value = i.KlijentID.ToString(), Text = i.Ime + " " + i.Prezime }).ToList()



                };

                return View("Dodaj", model);
            }
            if (vm.NagradnaIgraID == 0)
            {


                NagradnaIgra x = new NagradnaIgra
                {
                    NagradnaIgraID = vm.NagradnaIgraID,
                    DatumPocetka = vm.DatumPocetka,
                    DatumZavrsetka = vm.DatumZavrsetka,
                    Opis = vm.Opis,
                    NagradaID = vm.NagradaID,
                    KorisnikId = vm.AdministratorID,
                    KlijentID = vm.KlijentID
                };
                _db.NagradnaIgra.Add(x);
            }
            else
            {
                NagradnaIgra t = _db.NagradnaIgra.Find(vm.NagradnaIgraID);
                t.NagradnaIgraID = vm.NagradnaIgraID;
                t.DatumPocetka = vm.DatumPocetka;
                t.DatumZavrsetka = vm.DatumZavrsetka;
                t.Opis = vm.Opis;
                t.NagradaID = vm.NagradaID;
                t.KorisnikId = vm.AdministratorID;
                t.KlijentID = vm.KlijentID;
            }
            _db.SaveChanges();
            return Redirect("/NagradnaIgra/Index");
        }
        [Autorizacija(administrator: true, uposlenik: false, klijent: false)]
        public IActionResult Dodaj(int ID)
        {
            Korisnik a = _db.Korisnik.Where(x => x.KorisnikId == HttpContext.GetLogiraniKorisnik().KorisnikId).FirstOrDefault();

            NagradnaIgraDodajVM model = new NagradnaIgraDodajVM
            {
                DatumPocetka=DateTime.Now,
                DatumZavrsetka=DateTime.Now,

                Nagrade = _db.Nagrada.Select(i => new SelectListItem { Value = i.NagradaID.ToString(), Text = i.Naziv }).ToList(),
                Administrator=a.Ime+ " "+ a.Prezime,
                AdministratorID=a.KorisnikId,
                Klijenti = _db.Klijent.Select(i => new SelectListItem { Value = i.KlijentID.ToString(), Text = i.Ime+" "+ i.Prezime }).ToList()

               

            };

            if (ID != 0)
            {
                NagradnaIgra t = _db.NagradnaIgra.Find(ID);
                model.NagradnaIgraID = t.NagradnaIgraID;
                model.DatumPocetka = t.DatumPocetka;
                model.DatumZavrsetka = t.DatumZavrsetka;
                model.Opis = t.Opis;

                model.NagradaID = t.NagradaID;
                model.AdministratorID = t.KorisnikId;
                model.KlijentID = t.KlijentID;



            }


            return View(model);
        }
        [Autorizacija(administrator: true, uposlenik: false, klijent: false)]
        public IActionResult Obrisi(int ID)
        {
            NagradnaIgra x = _db.NagradnaIgra.Find(ID);
            _db.NagradnaIgra.Remove(x);
            _db.SaveChanges();


            return Redirect("/NagradnaIgra/Index");
        }
        [Autorizacija(administrator: true, uposlenik: true, klijent: true)]
        public IActionResult Index()
        {
            NagradnaIgraIndexVM model = new NagradnaIgraIndexVM
            {


                rows = _db.NagradnaIgra.Select(o => new NagradnaIgraIndexVM.row
                {

                    NagradnaIgraID = o.NagradnaIgraID,
                    DatumPocetka = o.DatumPocetka,
                    DatumZavrsetka = o.DatumZavrsetka,
                    Opis = o.Opis,
                    Nagrada = o.Nagrada.Naziv,
                    Slika= "data:image;base64," + Convert.ToBase64String(o.Nagrada.Slika),
                    Administrator = o.Korisnik.Ime,
                    Klijent = o.Klijent.Ime +" "+o.Klijent.Prezime,



                }).ToList()
            };


            return View(model);
        }

        //modul klijent
        public IActionResult KlijentIndex()
        {
            NagradnaIgraIndexVM model = new NagradnaIgraIndexVM
            {


                rows = _db.NagradnaIgra.Select(o => new NagradnaIgraIndexVM.row
                {

                    NagradnaIgraID = o.NagradnaIgraID,
                    DatumPocetka = o.DatumPocetka,
                    DatumZavrsetka = o.DatumZavrsetka,
                    Opis = o.Opis,
                    Nagrada = o.Nagrada.Naziv,
                    Slika = "data:image;base64," + Convert.ToBase64String(o.Nagrada.Slika),
                    Administrator = o.Korisnik.Ime,
                    Klijent = o.Klijent.Ime + " " + o.Klijent.Prezime,




                }).ToList()
            };


            return View(model);
        }

    }
}
