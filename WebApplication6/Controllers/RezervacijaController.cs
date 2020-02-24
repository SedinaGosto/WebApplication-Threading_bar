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
    public class RezervacijaController : Controller
    {
        MojContext _db;
        public RezervacijaController(MojContext db)
        {

            _db = db;

        }
        //Akcija za odabir uposlenika.
        public IActionResult UposleniciIndex()
        {
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
            return View(model);
        }

        //Prikaz slobodnih termina za odredjenog uposlenika
        public IActionResult Odaberi(int id)
        {
            DateTime danas = DateTime.Now;
            RezervacijaTerminiVM model = new RezervacijaTerminiVM()
            {
                ImeUposlenika = _db.Korisnik.Where(x => x.KorisnikId == id).Select(x => x.Ime + " "+ x.Prezime).FirstOrDefault(),
                rows = _db.Termin.Where(x => x.KorisnikId == id && x.Rezervisan == false && x.VrijemeTermina>danas).
                Select(i => new RezervacijaTerminiVM.row
                {
                    TerminID = i.Id,
                    VrijemeTermina = i.VrijemeTermina,

                }
                ).ToList()

            };
            return View(model);
        }

        //Dodavanje rezervacije modul klijent
        public IActionResult Dodaj(int id)
        {
            KlijentT k = _db.Klijent.Where(x => x.KlijentID == HttpContext.GetLogiraniKlijent().KlijentID).FirstOrDefault();
            RezervacijaDodajVM model = new RezervacijaDodajVM
            {
                Klijent = k.Ime + " " + k.Prezime,
                KlijentId = k.KlijentID,
                Tretmani = _db.Tretman.Select(i => new SelectListItem { Value = i.TretmanID.ToString(), Text = i.Naziv + " " + i.Cijena + " " + "KM" }).ToList(),
                TerminId = id,
                DatumRezervacije = DateTime.Now,
                VrijemeTermina = _db.Termin.Find(id).VrijemeTermina


            };


            return View(model);
        }
        public IActionResult Snimi(RezervacijaDodajVM vm)
        {
            if (vm.RezervacijaID == 0)
            {


                Rezervacija x = new Rezervacija
                {
                    DatumRezervacije = vm.DatumRezervacije,
                    KlijentId = vm.KlijentId,
                    TerminId = vm.TerminId,
                    TretmanId = vm.TeretmanId,

                };
                _db.Rezervacija.Add(x);
                _db.SaveChanges();
                PoslataNotifikacija n = new PoslataNotifikacija
                {
                    DatumSlanja = DateTime.Now,
                    IsProcitano = false,
                    RezervacijaId = x.Id

                };
                _db.PoslataNotifikacija.Add(n);

            }
            else
            {
                Rezervacija t = _db.Rezervacija.Find(vm.RezervacijaID);
                t.DatumRezervacije = vm.DatumRezervacije;
                t.KlijentId = vm.KlijentId;
                t.TerminId = vm.TerminId;
                t.TretmanId = vm.TeretmanId;
            }
            Termin tr = _db.Termin.Find(vm.TerminId);
            tr.Rezervisan = true;
            _db.SaveChanges();
            return Redirect("/Rezervacija/Index");
        }
      
        //Ponistavanje rezervacije modul klijent
        public IActionResult Obrisi(int ID)
        {
            DateTime danas = DateTime.Now.AddDays(3);


            Rezervacija x = _db.Rezervacija.Find(ID);
            int id = x.KlijentId;
            Termin t = _db.Termin.Find(x.TerminId);
            if(t.VrijemeTermina<danas)
            {
                TempData["ErrorMessage"] = "Rezervacija se moze otkazati samo 3 dana prije!";
                return Redirect("/Rezervacija/Index/" + id);
            }
            KlijentNotifikacija noti = _db.KlijentNotifikacija.Where(y => y.RezervacijaId == x.Id).FirstOrDefault();
            PoslataNotifikacija nofikacija = _db.PoslataNotifikacija.Where(y => y.RezervacijaId == x.Id).FirstOrDefault();
    
         
            if (noti != null)
            {
                _db.KlijentNotifikacija.Remove(noti);
                _db.SaveChanges();
            }
            if (nofikacija != null)
            {
                _db.PoslataNotifikacija.Remove(nofikacija);
                _db.SaveChanges();
            }
            _db.Rezervacija.Remove(x);
            t.Rezervisan = false;
            _db.SaveChanges();


            return Redirect("/Rezervacija/Index/" + id);
        }
        //Pregled rezervacija modul klijent
        public IActionResult Index(int id)
        {

            KlijentT k = HttpContext.GetLogiraniKlijent();
            RezervacijaIndexVM model = new RezervacijaIndexVM();
            var errMsg = TempData["ErrorMessage"] as string;

            if (k != null)
            {

          


                model.rows = _db.Rezervacija.Where(x => x.Klijent.KlijentID == k.KlijentID).Select(o => new RezervacijaIndexVM.row
                {

                    DatumRezervacije = o.DatumRezervacije,
                    Klijent = o.Klijent.Ime + " " + o.Klijent.Prezime,
                    Naziv = o.Tretman.Naziv,
                    Termin = o.Termin.VrijemeTermina,
                    RezervacijaId = o.Id

                }).ToList();


            }
            else if(id!=0)
            {
                model.rows = _db.Rezervacija.Where(x => x.Klijent.KlijentID == id).Select(o => new RezervacijaIndexVM.row
                {

                    DatumRezervacije = o.DatumRezervacije,
                    Klijent = o.Klijent.Ime + " " + o.Klijent.Prezime,
                    Naziv = o.Tretman.Naziv,
                    Termin = o.Termin.VrijemeTermina,
                    RezervacijaId = o.Id,
                    Odobrena = false

                }).ToList();
             
                TempData["Klijent"] = "Odredjeni";
                foreach (var r in model.rows)
                {
                    KlijentNotifikacija n = _db.KlijentNotifikacija.Where(x => x.RezervacijaId == r.RezervacijaId).FirstOrDefault();
                    if (n != null)
                    {
                        r.Odobrena = true;
                    }
                }

                TempData["error"] = errMsg;
                return View("KlijentIndex", model);
            }
            else
            {
                model.rows = _db.Rezervacija.Select(o => new RezervacijaIndexVM.row
                {

                    DatumRezervacije = o.DatumRezervacije,
                    Klijent = o.Klijent.Ime + " " + o.Klijent.Prezime,
                    Naziv = o.Tretman.Naziv,
                    Termin = o.Termin.VrijemeTermina,
                    RezervacijaId = o.Id

                }).ToList();

               
            }
            TempData["error"] = errMsg;
            return View(model);
        


        }
        public IActionResult PregledajRzerevacije(string search)
        {
                Korisnik k = HttpContext.GetLogiraniKorisnik();

            //    //Poziv za search
            if (search != null)
            {

                RezervacijaIndexVM model1 = new RezervacijaIndexVM
                {


                    rows = _db.Rezervacija.Where(x => (x.Klijent.Ime.ToLower().Contains(search.ToLower()) || x.Klijent.Prezime.ToLower().Contains(search.ToLower())) && x.Termin.Korisnik.KorisnikId == k.KorisnikId)
                    .Select(o => new RezervacijaIndexVM.row
                    {

                        DatumRezervacije = o.DatumRezervacije,
                        Klijent = o.Klijent.Ime + " " + o.Klijent.Prezime,
                        Naziv = o.Tretman.Naziv,
                        Termin = o.Termin.VrijemeTermina,
                        RezervacijaId = o.Id,
                        Odobrena = false

                    }).ToList()
                   
                };
                foreach (var r in model1.rows)
                {
                    KlijentNotifikacija n = _db.KlijentNotifikacija.Where(x => x.RezervacijaId == r.RezervacijaId).FirstOrDefault();
                    if(n!=null)
                    r.Odobrena = true;
                }
                return View(model1);
            }



            //Pregled rezervacija za određenog uposlenika
            RezervacijaIndexVM model = new RezervacijaIndexVM
            {


                

                rows = _db.Rezervacija.Where(x => x.Termin.Korisnik.KorisnikId == k.KorisnikId)

              .Select(o => new RezervacijaIndexVM.row
              {

                  DatumRezervacije = o.DatumRezervacije,
                  Klijent = o.Klijent.Ime + " " + o.Klijent.Prezime,
                  Naziv = o.Tretman.Naziv,
                  Termin = o.Termin.VrijemeTermina,
                  RezervacijaId = o.Id,
                  Odobrena=false
                 

              }).ToList()
         
            };
            foreach (var r in model.rows)
            {
                KlijentNotifikacija n = _db.KlijentNotifikacija.Where(x => x.RezervacijaId == r.RezervacijaId).FirstOrDefault();
                if (n != null)
                {
                    r.Odobrena = true;
                }
            }
            return View(model);

        }

    }
    }