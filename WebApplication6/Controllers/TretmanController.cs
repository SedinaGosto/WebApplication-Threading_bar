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
    public class TretmanController : Controller
    {

        MojContext _db;

        public TretmanController(MojContext db)
        {

            _db = db;

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Snimi(TretmanIndexVM vm)
        {
            
            if(!ModelState.IsValid)
            {
                vm.rows = _db.Tretman.Select(o => new TretmanIndexVM.row
                {

                    TretmanID = o.TretmanID,
                    Naziv = o.Naziv,
                    Cijena = o.Cijena,



                }).ToList();
                return View("Index", vm);
            }

                Tretman x = new Tretman
                {
                    TretmanID = vm.tretman.TretmanID,
                    Naziv = vm.tretman.Naziv,
                    Cijena=vm.tretman.Cijena
                 


                };
                _db.Tretman.Add(x);
            
          
            _db.SaveChanges();
            return Redirect("/Tretman/Index");
        }
        public IActionResult Uredi(int ID)
        {
            Tretman t = _db.Tretman.Find(ID);


            TretmanUrediVM model = new TretmanUrediVM
            {
                TretmanID = t.TretmanID,
                Naziv = t.Naziv,
                Cijena = t.Cijena
            };


            return PartialView(model);
        }
        public IActionResult Spasi(TretmanUrediVM vm)
        {
            Tretman t = _db.Tretman.Find(vm.TretmanID);
            t.Naziv = vm.Naziv;
            t.Cijena = vm.Cijena;

            _db.SaveChanges();
            return Redirect("/Tretman/Index");
        }

        public IActionResult Obrisi(int ID)
        {
            Tretman x = _db.Tretman.Find(ID);
            _db.Tretman.Remove(x);
            _db.SaveChanges();


            return Redirect("/Tretman/Index");
        }
        public IActionResult Index()
        {
            
            TretmanIndexVM model = new TretmanIndexVM
            {
                tretman=new Tretman(),

                rows = _db.Tretman.Select(o => new TretmanIndexVM.row
                {

                    TretmanID = o.TretmanID,
                    Naziv = o.Naziv,
                    Cijena = o.Cijena,
              


                }).ToList()
            };


            return View(model);
        }
    }
}