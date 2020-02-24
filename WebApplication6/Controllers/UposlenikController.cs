using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication6.EF;
using WebApplication6.Helper;
using WebApplication6.Models;
using WebApplication6.ViewModels;

namespace WebApplication6.Controllers
{
  
    public class UposlenikController : Controller
    {
        MojContext _db;
        //Za upload slika
        public readonly IHostingEnvironment _hostingEnvironment;
        public UposlenikController(MojContext db, IHostingEnvironment hostingEnvironment)
        {

            _db = db;
            _hostingEnvironment = hostingEnvironment;

        }
        [Autorizacija(administrator: true, uposlenik: true, klijent: false)]
        public IActionResult Uredi(int id)
        {
            Korisnik novi = _db.Korisnik.Find(id);
            UposlenikUrediVM model = new UposlenikUrediVM
            {
                KorisnikId = novi.KorisnikId,
             Ime=novi.Ime,
             Prezime=novi.Prezime,
             KorisnickoIme=novi.KorisnickoIme,
             Email=novi.Email,
             Slika= "data:image;base64," + Convert.ToBase64String(novi.Slika),
            Telefon =novi.Telefon
    
            };


            return View(model);

        }

        public IActionResult Snimi(UposlenikUrediVM vm)
        {
            if (!ModelState.IsValid)
            {

                return View("Dodaj", vm);
            }
            string uniqueFileName = null;

                Korisnik novi = new Korisnik();
                if (vm.Photo != null)
                {
                    //Upload slike

                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");


                    uniqueFileName = Guid.NewGuid().ToString() + "_" + vm.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    if (vm.Photo.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            vm.Photo.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            string s = Convert.ToBase64String(fileBytes);
                            novi.Slika = fileBytes;
                        }
                    }
                }


                novi.Ime = vm.Ime;
                novi.Prezime = vm.Prezime;
                novi.Email = vm.Email;
                novi.KorisnickoIme = vm.KorisnickoIme;
                novi.Telefon = vm.Telefon;
                novi.LozinkaSalt = GenerateHashClass.GenerateSalt();
                novi.LozinkaHash = GenerateHashClass.GenerateHash(novi.LozinkaSalt,vm.Lozinka);
                novi.KorisnikId = vm.KorisnikId;



                _db.Korisnik.Add(novi);
            _db.SaveChanges();
       
            return Redirect("/Uposlenik/Index");
      


        }
        public IActionResult Spasi(UposlenikUrediVM vm)
        {


            Korisnik t = _db.Korisnik.Find(vm.KorisnikId);

            string uniqueFileName = null;
            IFormFile photo = vm.Photo;
            byte[] array = t.Slika;
            if (vm.Photo != null)
            {

                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + vm.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);



                if (vm.Photo.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        vm.Photo.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string s = Convert.ToBase64String(fileBytes);
                        array = fileBytes;
                    }
                }

            }


            t.KorisnikId = vm.KorisnikId;
            t.Ime = vm.Ime;
            t.Prezime = vm.Prezime;
            t.Email = vm.Email;
            t.Telefon = vm.Telefon;
            t.KorisnickoIme = vm.KorisnickoIme;
            t.LozinkaHash = t.LozinkaHash;
            t.LozinkaSalt = t.LozinkaSalt;
            t.Slika = array;

        

        _db.SaveChanges();
            return Redirect("/Uposlenik/Index");

        }

        [Autorizacija(administrator: true, uposlenik: false, klijent: false)]
        public IActionResult Obrisi(int ID)
        {
            Korisnik x = _db.Korisnik.Find(ID);
            _db.Remove(x);
            _db.SaveChanges();


            return Redirect("/Uposlenik/Index");
        }
        [Autorizacija(administrator: true, uposlenik: true, klijent: false)]
        public IActionResult Index()
        {

            UposlenikIndexVM model = new UposlenikIndexVM()
            {
                uposlenik = new Korisnik(),
                rows = _db.Korisnik.Select(i => new UposlenikIndexVM.row
                {
                    KorisnikId = i.KorisnikId,
                    Ime = i.Ime,
                    Prezime = i.Prezime,
                    Telefon = i.Telefon,
                    KorisnickoIme = i.KorisnickoIme,
                    Email=i.Email,
                   
                    Status=i.Status
                   


                }
                ).ToList()

            };
            return View(model);

        }
        public IActionResult Dodaj(int ID)
        {
            

            UposlenikUrediVM model = new UposlenikUrediVM
            {

            };

            if (ID != 0)
            {
                Korisnik t = _db.Korisnik.Find(ID);
                model.KorisnikId = t.KorisnikId;
                model.Ime = t.Ime;
                model.Prezime = t.Prezime;
                model.Email = t.Email;

                model.KorisnickoIme = t.KorisnickoIme;
                model.Telefon = t.Telefon;
    



            }


            return View(model);
        }
    
        [Autorizacija(administrator: true, uposlenik: true, klijent: true)]
        public IActionResult PregledTermina(int id)
        {

            Korisnik novi = _db.Korisnik.Find(id);
            UposlenikUrediVM model = new UposlenikUrediVM
            {

                KorisnikId = novi.KorisnikId,
                Ime = novi.Ime,
                Prezime = novi.Prezime,
                KorisnickoIme = novi.KorisnickoIme,
                //Slika = novi.Slika,
                Telefon = novi.Telefon
            };


            return View(model);

        }
        public IActionResult PromijeniLozinku()
        {
            LozinkaVM model = new LozinkaVM();
            return View(model);
        }

        public IActionResult Promijena(LozinkaVM model)
        {
            if (model.NovaLozinka != model.PotvrdaLozinke)
            {
                return View(model);
            }

            Korisnik k = HttpContext.GetLogiraniKorisnik();
            k.LozinkaSalt = GenerateHashClass.GenerateSalt();
            k.LozinkaHash = GenerateHashClass.GenerateHash(k.LozinkaSalt, model.NovaLozinka);
            _db.SaveChanges();
            return Redirect("/Notifikacija/Notifikacije");
        }
    }
}