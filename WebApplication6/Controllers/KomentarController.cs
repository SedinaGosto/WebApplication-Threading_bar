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
    public class KomentarController : Controller
    {
        MojContext _db;


        public KomentarController(MojContext db)
        {
            _db = db;

        }

        //Akcija za odabir uposlenika.
        public IActionResult UposleniciIndex()
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();

            RezervacijaUposlenikVM model = new RezervacijaUposlenikVM()
            {

                rows = _db.Korisnik.Select(i => new RezervacijaUposlenikVM.row
                {
                    UposlenikID = i.KorisnikId,
                    ImePrezime = i.Ime + i.Prezime,

                    Slika = "data:image;base64," + Convert.ToBase64String(i.Slika),


                }
                ).ToList()

            };
            if(k!=null)
            {
                return View("OdaberiKorisnik", model);
            }
            return View("Odaberi",model);
        }
        public IActionResult Index(int id)
        {
            Korisnik k = _db.Korisnik.Find(id);
            List<Ocjena> ocjene = _db.Ocjena.Where(o => o.KorisnikId == id).ToList();

            KomentarIndexVM model = new KomentarIndexVM
            {
                Korisnik = k.Ime + " " + k.Prezime,
                KorisnikId=k.KorisnikId,
                
        


                rows = _db.Komentar.Where(x=>x.SakrijKomentar==false && x.KorisnikId==id).Select(o => new KomentarIndexVM.row
                {
                    KomentarId = o.KomentarId,
                    TekstKomentara = o.TekstKomentara,
                    Klijent = o.Klijent.Ime + " " + o.Klijent.Prezime,
                    KlijentId=o.KlijentId,
              
                    DatumKreiranja = o.DatumKreiranja



                }).ToList()

            };
            if(ocjene.Count>0)
            {
                model.ProsjecnaOcjena = ocjene.Average(x => x.OcjenaInt);
            }
            model.rows = model.rows.OrderByDescending(x => x.DatumKreiranja).ToList();
            return View(model);
        }

        public IActionResult Dodaj(int id)
        {
            KlijentT logirani = HttpContext.GetLogiraniKlijent();
            Korisnik k = _db.Korisnik.Find(id);
            KomentarDodajVM model = new KomentarDodajVM
            {
                KlijentId = logirani.KlijentID,
                KorisnikId = k.KorisnikId,
                SakrijKomentar = false,
                DatumKreiranja = DateTime.Now


            };
            return View(model);
        }

        public IActionResult Snimi(KomentarDodajVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Dodaj", vm);
            }

            Komentar n = new Komentar
            {
                KlijentId = vm.KlijentId,
                KorisnikId = vm.KorisnikId,
                DatumKreiranja = vm.DatumKreiranja,
                SakrijKomentar = vm.SakrijKomentar,
                TekstKomentara=vm.TekstKomentara

            };


            _db.Add(n);
            _db.SaveChanges();
            return Redirect("/Komentar/Index/" + vm.KorisnikId);
        }
        public IActionResult KorisnikIndex(int id)
        {
            Korisnik k = _db.Korisnik.Find(id);
            List<Ocjena> ocjene = _db.Ocjena.Where(o => o.KorisnikId == id).ToList();

            KomentarIndexVM model = new KomentarIndexVM
            {
                Korisnik = k.Ime + " " + k.Prezime,
                KorisnikId = k.KorisnikId,




                rows = _db.Komentar.Where(x => x.SakrijKomentar == false && x.KorisnikId == id).Select(o => new KomentarIndexVM.row
                {
                    KomentarId = o.KomentarId,
                    TekstKomentara = o.TekstKomentara,
                    Klijent = o.Klijent.Ime + " " + o.Klijent.Prezime,

                    DatumKreiranja = o.DatumKreiranja



                }).ToList()

            };
            if (ocjene.Count > 0)
            {
                model.ProsjecnaOcjena = ocjene.Average(x => x.OcjenaInt);
            }
            model.rows = model.rows.OrderByDescending(x => x.DatumKreiranja).ToList();
            return View(model);
        }

        public IActionResult UkloniKomentar(int id)
        {
            KlijentT klijent = HttpContext.GetLogiraniKlijent();
            Komentar k = _db.Komentar.Find(id);
            if (klijent != null)
            {
                k.SakrijKomentar = true;
                _db.SaveChanges();
                return Redirect("/Komentar/Index/" + k.KorisnikId);
            }
            else
            {
                KorisniciUloge list = _db.KorisniciUloge.Include(x => x.Korisnik).Include(x => x.Uloga).Where(x => x.KorisnikId == HttpContext.GetLogiraniKorisnik().KorisnikId).FirstOrDefault();

                if (list.Uloga.Naziv == "Administrator")
                {

                    k.SakrijKomentar = true;
                    _db.SaveChanges();
                }
                else
                {
                    TempData["ErrorMessage"] = "Nemate pravo pristupa";
                }



                return Redirect("/Komentar/KorisnikIndex/" + k.KorisnikId);
            }
        }
    }
}