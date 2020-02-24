using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication6.EF;
using WebApplication6.Helper;
using WebApplication6.Models;
using WebApplication6.ViewModels;

namespace WebApplication6.Controllers
{
    [Autorizacija(administrator: true, uposlenik: true, klijent: false)]
    public class TerminController : Controller
    {
        MojContext _db;

        public TerminController(MojContext db)
        {
            _db = db;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Snimi(TerminDodajVM vm)
        {
            if (vm.TerminId == 0)
            {


                Termin x = new Termin
                {
                    
                  
                KorisnikId = vm.UposlenikID,
                Rezervisan = vm.Rezervisan,
                VrijemeTermina = vm.VrijemeTermina
               
            };
                _db.Termin.Add(x);
            }
            else
            {
                Termin t = _db.Termin.Find(vm.TerminId);
               t.Id=vm.TerminId;
              t.KorisnikId=vm.UposlenikID;
               t.Rezervisan=vm.Rezervisan;
               t.VrijemeTermina=vm.VrijemeTermina;
            }
            _db.SaveChanges();
            return Redirect("/Uposlenik/PregledTermina/" + vm.UposlenikID);
        }
        public IActionResult Dodaj(int ID)
        {

            TerminDodajVM model = new TerminDodajVM
            {
                Uposlenici = _db.Korisnik.Select(i => new SelectListItem { Value = i.KorisnikId.ToString(), Text = i.Ime + " "+ i.Prezime    }).ToList(),
                Rezervisan = false,
                 VrijemeTermina=DateTime.Now
            


            };

            if (ID != 0)
            {
                Termin t = _db.Termin.Find(ID);
                model.TerminId = t.Id;
                model.UposlenikID = t.KorisnikId;
                model.Rezervisan = t.Rezervisan;
                model.VrijemeTermina = t.VrijemeTermina;
            


            }


            return PartialView(model);
        }
        public IActionResult DodajZaUposlenika(int ID)
        {
            Korisnik u = _db.Korisnik.Find(ID);
            TerminDodajVM model = new TerminDodajVM
            {
                UposlenikID = ID,
                Rezervisan = false,
                VrijemeTermina = DateTime.Now



            };
            TempData["ImeUposlenika"] =u.Ime + " "+ u.Prezime;





            return PartialView(model);
        }


        public IActionResult Obrisi(int ID)
        {
            Termin x = _db.Termin.Find(ID);
            int id = x.KorisnikId;
            _db.Termin.Remove(x);
            _db.SaveChanges();


            return Redirect("/Termin/TerminiZaUposlenika/" + id);
        }
        //modul administrator
        public IActionResult Remove(int ID)
        {
            Termin x = _db.Termin.Find(ID);
            int id = x.KorisnikId;
            _db.Termin.Remove(x);
            _db.SaveChanges();


            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
        
            if (k != null)
            {
                TerminIndexVM model1 = new TerminIndexVM
                {


                    rows = _db.Termin.Select(o => new TerminIndexVM.row
                    {

                        TerminID = o.Id,
                        Uposlenik = o.Korisnik.Ime + " " + o.Korisnik.Prezime,
                        VrijemeTermina = o.VrijemeTermina,
                        Rezervisan = o.Rezervisan,
                        UposlenikId = o.KorisnikId


                    }).ToList()
                };
                return View(model1);
            }
            TerminIndexVM model = new TerminIndexVM
            {


                rows = _db.Termin.Select(o => new TerminIndexVM.row
                {

                    TerminID = o.Id,
                    Uposlenik = o.Korisnik.Ime + " " + o.Korisnik.Prezime,
                    VrijemeTermina = o.VrijemeTermina,
                    Rezervisan = o.Rezervisan,
                    UposlenikId = o.KorisnikId


                }).ToList()
            };


            return View(model);
         
        }
        public IActionResult TerminiZaUposlenika(int id)
        {
            TerminIndexVM model = new TerminIndexVM
            {


                rows = _db.Termin.Where(x=>x.KorisnikId==id).Select(o => new TerminIndexVM.row
                {
                    UposlenikId=id,
                    TerminID = o.Id,
                    Uposlenik = o.Korisnik.Ime+ " " + o.Korisnik.Prezime,
                    VrijemeTermina = o.VrijemeTermina,
                    Rezervisan = o.Rezervisan


                }).ToList()
            };

            TempData["UposlenikId"] = id;
            return View(model);
        }
        public IActionResult Promijeni(int id)
        {
            Termin t = _db.Termin.Find(id);
            if (t.Rezervisan)
            {
                t.Rezervisan = false;
            }
            else
                t.Rezervisan = true;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        //modul uposlenik
        public IActionResult PromijeniRezervaciju(int id)
        {
            Termin t = _db.Termin.Find(id);
            if (t.Rezervisan)
            {
                t.Rezervisan = false;
            }
            else
                t.Rezervisan = true;
            _db.SaveChanges();

            return Redirect("/Uposlenik/PregledTermina/" + t.KorisnikId);
        }

    }
}