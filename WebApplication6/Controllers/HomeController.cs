using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication6.EF;
using WebApplication6.Helper;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class HomeController : Controller
    {
        private MojContext _db;

        public HomeController(MojContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            //KorisnickiNalog k = HttpContext.GetLogiraniKorisnik();
            //Admin pk = _db.Administrator.Where(s => s.KorisnickiNalogId == k.Id).FirstOrDefault();
            //Uposlenik z = _db.Uposlenik.Where(s => s.KorisnickiNalogId == k.Id).FirstOrDefault();
            //Klijent klijent = _db.Klijent.Where(s => s.KorisnickiNalogId == k.Id).FirstOrDefault();

            //if (pk != null)
            //{
            //    return View();
            //}
            //if (z != null)
            //{
            //    return RedirectToAction("Notifikacije", "Notifikacija");
            //}
            //else
            //{
            return View();
            //}


        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
