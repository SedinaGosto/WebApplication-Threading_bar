using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.EF;
using WebApplication6.Helper;
using WebApplication6.Models;
using WebApplication6.ViewModels;

namespace WebApplication1.Controllers
{
    [Autorizacija(administrator: true, uposlenik: false, klijent: false)]
    public class NagradaController : Controller
    {

        MojContext _db;
        //Za upload slika
        public readonly IHostingEnvironment _hostingEnvironment;
        public NagradaController(MojContext db, IHostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {

            NagradaIndexVM model = new NagradaIndexVM
            {

                rows = _db.Nagrada.Select(o => new NagradaIndexVM.row
                {
                    NagradaID = o.NagradaID,
                    Naziv = o.Naziv,



                }).ToList()

            };


            return PartialView(model);
        }
        public IActionResult Obrisi(int ID)
        {
            Nagrada t = _db.Nagrada.Find(ID);
            _db.Remove(t);
            _db.SaveChanges();

            return Redirect("/Nagrada/Index");


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Snimi(NagradaDodajVM vm)
        {
            if(!ModelState.IsValid)
            {
                return View("Dodaj",vm);
            }

            string uniqueFileName = null;
            Nagrada n = new Nagrada();
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
                        n.Slika = fileBytes;
                    }
                }
            }

            n.NagradaID = vm.NagradaID;
            n.Naziv = vm.Naziv;

          


            _db.Add(n);
            _db.SaveChanges();
            return Redirect("/NagradnaIgra/Dodaj");
        }

        public IActionResult Dodaj()
        {
            NagradaDodajVM model = new NagradaDodajVM();



            return PartialView(model);
        }
    }
}
