using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using WebApplication6.Helper;
using WebApplication6.EF;
using Microsoft.EntityFrameworkCore;

namespace WebApplication6.Helper
{

    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool administrator, bool uposlenik,bool klijent)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { administrator, uposlenik,klijent };
        }
    }


    public class MyAuthorizeImpl : IAsyncActionFilter
    {
        public MyAuthorizeImpl(bool administrator, bool uposlenik,bool klijent)
        {
            _administrator = administrator;
            _uposlenik = uposlenik;
            _klijent = klijent;
        }
        private readonly bool _administrator;
        private readonly bool _uposlenik;

        private readonly bool _klijent;
        public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {

            Korisnik k = filterContext.HttpContext.GetLogiraniKorisnik();
            KlijentT kl = filterContext.HttpContext.GetLogiraniKlijent();

            if (k == null && kl==null)
            {
                if (filterContext.Controller is Controller controller)
                {
                    controller.TempData["error_poruka"] = "Niste logirani";
                }

                filterContext.Result = new RedirectToActionResult("Login", "Autentifikacija", new { @area = "" });
                return;
            }

            //Preuzimamo DbContext preko app services
            MojContext db = filterContext.HttpContext.RequestServices.GetService<MojContext>();

            //zaposlenici mogu pristupiti studenti
            if (_uposlenik && db.Korisnik.Any(s=>s.KorisnikId==k.KorisnikId))
            {
                bool imapristup = false;
                List<KorisniciUloge> uloge = db.KorisniciUloge.Where(s => s.KorisnikId == k.KorisnikId).Include(s => s.Uloga).ToList();
                foreach(var uloga in uloge)
                {
                    if (uloga.Uloga.Naziv == "Uposlenik")
                        imapristup = true;
                }
                if (imapristup)
                {
                    await next(); //ok - ima pravo pristupa
                    return;
                }
            }

           

            if (_administrator && db.Korisnik.Any(s => s.KorisnikId == k.KorisnikId))
            {
                bool imapristup = false;
                List<KorisniciUloge> uloge = db.KorisniciUloge.Where(s => s.KorisnikId == k.KorisnikId).Include(s => s.Uloga).ToList();
                foreach (var uloga in uloge)
                {
                    if (uloga.Uloga.Naziv == "Administrator")
                        imapristup = true;
                }
                if (imapristup)
                {
                    await next(); //ok - ima pravo pristupa
                    return;
                }
            }
            if (_klijent && db.Klijent.Any(s => s.KlijentID == kl.KlijentID))
            {
                await next();//ok - ima pravo pristupa
                return;
            }

            //if (db.Korisnik.All(x => x.KorisnikId != k.KorisnikId) &&  db.klijentT.Any(x => x.KlijentID != klijent.KlijentID))
            //{
            //    filterContext.Result = new RedirectToActionResult("Index", "Home", new { area = "" });
            //    return;
            //}
            if (filterContext.Controller is Controller c1)
            {
                c1.TempData["error_poruka"] = "Nemate pravo pristupa";
            }
            if ( kl!= null && db.Klijent.All(x => x.KlijentID != kl.KlijentID))
            {
                filterContext.Result = new RedirectToActionResult("Privacy", "Home", new { @area = "" });

            }
            else

                filterContext.Result = new RedirectToActionResult("Contact", "Home", new { @area = "" });
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // throw new NotImplementedException();
        }
    }
}