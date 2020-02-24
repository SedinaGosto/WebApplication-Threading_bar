using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication6.EF;
using WebApplication6.Helper;
using WebApplication6.Models;
using WebApplication6.ViewModels;

namespace WebApplication1.Controllers
{
   
    public class ClanakController: Controller
    {
        MojContext _db;
        //Za upload slika
        public readonly IHostingEnvironment _hostingEnvironment;

        public ClanakController(MojContext db, IHostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;

        }
   [HttpPost]
   [ValidateAntiForgeryToken]
        public IActionResult Snimi(ClanakDodaj vm)
        {
            if(!ModelState.IsValid)
            {

                vm.ClanciKategorija = _db.ClanciKategorija.Select(i => new SelectListItem { Value = i.ClanciKategorijaID.ToString(), Text = i.Naziv }).ToList();

                return View("Dodaj", vm);
            }
            string uniqueFileName = null;
        
            if (vm.ClanakID == 0)
            {
                Clanak novi = new Clanak();
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


                novi.Naslov = vm.Naslov;
                novi.TekstClanka = vm.TekstClanka;
                novi.DatumObjave = vm.DatumObjave;
                novi.Podnaslov = vm.Podnaslov;
                novi.ClanciKategorijaID = vm.ClanciKategorijaID;
                novi.KorisnikId = vm.KorisnikId;
              
                    
            
                _db.Clank.Add(novi);
            }
            else
            {
                Clanak t = _db.Clank.Find(vm.ClanakID);
              
            
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


                t.ClanakID = vm.ClanakID;
                t.Naslov = vm.Naslov;
                t.TekstClanka = vm.TekstClanka;
                t.DatumObjave = vm.DatumObjave;
                t.Podnaslov = vm.Podnaslov;
                t.ClanciKategorijaID = vm.ClanciKategorijaID;
                t.KorisnikId = vm.KorisnikId;
                t.Slika = array;
            
            }
       
            _db.SaveChanges();
            return Redirect("/Clanak/Index");
        }
        [Autorizacija(administrator: true, uposlenik: false, klijent: false)]
        public IActionResult Dodaj(int ID)
        {
            Korisnik a = _db.Korisnik.Where(x => x.KorisnikId == HttpContext.GetLogiraniKorisnik().KorisnikId).FirstOrDefault();


            ClanakDodaj model = new ClanakDodaj
            {
                ClanciKategorija = _db.ClanciKategorija.Select(i => new SelectListItem { Value = i.ClanciKategorijaID.ToString(), Text = i.Naziv }).ToList(),
                KorisnikId = a.KorisnikId,
                Korisnik = a.Ime,
                DatumObjave = DateTime.Now,
                

            };

            if (ID != 0)
            {
                Clanak t = _db.Clank.Find(ID);
                model.ClanakID = t.ClanakID;
                model.Naslov = t.Naslov;
                model.TekstClanka = t.TekstClanka;
                model.ClanciKategorijaID = t.ClanciKategorijaID;
                model.KorisnikId = t.KorisnikId;
                model.Podnaslov = t.Podnaslov;
                model.DatumObjave = t.DatumObjave;
            }


            return View(model);
        }
        [Autorizacija(administrator: true, uposlenik: false, klijent: false)]
        public IActionResult Obrisi(int ID)
        {
            Clanak x = _db.Clank.Find(ID);
            _db.Clank.Remove(x);
            _db.SaveChanges();


            return Redirect("/Clanak/Index");
        }
        [Autorizacija(administrator: true, uposlenik: true, klijent: true)]
        public IActionResult Index(int TrenutnaStranica=1, int VelicinaStranice=4)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();

            KlijentT u = HttpContext.GetLogiraniKlijent();
            if (u != null)
            {
                return Redirect("/Clanak/KlijentIndex/");
            }
            ClanakIndexVM model = new ClanakIndexVM
            {


                rows = _db.Clank.Select(o => new ClanakIndexVM.row
                {

                    ClanakID = o.ClanakID,
                    Naslov = o.Naslov,
                    DatumObjave = o.DatumObjave,
                    Podnaslov = o.Podnaslov,
                    Kategorija = o.ClanciKategorija.Naziv,
                    Korisnik = o.Korisnik.Ime,
              }).ToList()
            };
            //Paging
            var items = model.rows.OrderByDescending(x => x.DatumObjave).Skip((TrenutnaStranica - 1) * VelicinaStranice).Take(VelicinaStranice).ToList();
            ViewData["TrenutnaStranica"] = TrenutnaStranica;

            return View(items);
         
        }
        public IActionResult KlijentIndex(int TrenutnaStranica = 1, int VelicinaStranice = 4)
        {
            ClanakIndexVM model = new ClanakIndexVM
            {


                rows = _db.Clank.Select(o => new ClanakIndexVM.row
                {

                    ClanakID = o.ClanakID,
                    Naslov = o.Naslov,
                    DatumObjave = o.DatumObjave,
                    Podnaslov = o.Podnaslov,
                    Kategorija = o.ClanciKategorija.Naziv,
                    Korisnik = o.Korisnik.Ime


                }).ToList()
            };
            //Paging
            var items = model.rows.OrderByDescending(x => x.DatumObjave).Skip((TrenutnaStranica - 1) * VelicinaStranice).Take(VelicinaStranice).ToList();
            ViewData["TrenutnaStranica"] = TrenutnaStranica;

            return View(items);
        }
        public IActionResult Detalji(int id)
        {
            Clanak model = _db.Clank.Where(x => x.ClanakID == id).Include(x => x.Korisnik).Include(x => x.ClanciKategorija).FirstOrDefault();
            

            ViewData["path"] = "data:image;base64," + Convert.ToBase64String(model.Slika);
            return View(model);
        }


    }
}
