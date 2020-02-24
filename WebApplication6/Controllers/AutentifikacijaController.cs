using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MimeKit;
using WebApplication6.EF;
using WebApplication6.Helper;
using WebApplication6.Models;
using WebApplication6.ViewModels;

namespace WebApplication6.Controllers
{
    public class AutentifikacijaController : Controller
    {
        private MojContext _db;
        public AutentifikacijaController(MojContext db)
        {
            _db = db;
        }

        public static string GenerateSalt()
        {
            var buf = new byte[16];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }
        public static string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];

            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }

        public IActionResult Index()
        {
            return View(new LoginVM()
            {
                ZapamtiPassword = true,
            });
        }

        public IActionResult Login(LoginVM input)
        {


            Korisnik korisnik = _db.Korisnik
                .SingleOrDefault(x => x.KorisnickoIme == input.username);
            KlijentT klijent = _db.Klijent.SingleOrDefault(x => x.KorisnickoIme == input.username);
           

            if (korisnik == null)
            {
                if (klijent == null)
                {
                    TempData["error_poruka"] = "Pogrešan username";
                    input.username = " ";
                    return View("Index", input);
                }
            }
           
          if(korisnik!=null)
            {
                string lozinkahash = GenerateHashClass.GenerateHash(korisnik.LozinkaSalt, input.password);
                if (lozinkahash != korisnik.LozinkaHash)
                {
                    TempData["error_poruka"] = "Pogrešan password";
                    input.username = " ";
                    return View("Index", input);
                }
            }
          if(klijent!=null)
            {
                string lozinkahash = GenerateHashClass.GenerateHash(klijent.LozinkaSalt, input.password);
                if (lozinkahash != klijent.LozinkaHash)
                {
                    TempData["error_poruka"] = "Pogrešan password";
                    input.username = " ";
                    return View("Index", input);
                }
            }

          
           

            if (korisnik != null)
            {
                HttpContext.SetLogiraniKorisnik(korisnik);
                return RedirectToAction("Index", "Home");
               
            }
        
            else
            {
                HttpContext.SetLogiraniKlijent(klijent);
                return RedirectToAction("About", "Home");
            }
        }

        public IActionResult Registracija()
        {
            RegistracijaVM model = new RegistracijaVM();
            model.korisnicko = "";
            model.korisnikovalozinka = "";
            model.gradovi = _db.Grad.Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Naziv
            }).ToList();

            return View(model);
            }
      
        public IActionResult SnimiRegistraciju(RegistracijaVM input)
        {
            TryValidateModel(input);
            if (!ModelState.IsValid)
            {
                input.gradovi = _db.Grad.Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Naziv
                }).ToList();
                return View("Registracija", input);
            }
            if (input != null)
            {
                var n = _db.Klijent
                    .Where((x) => x.KorisnickoIme == input.korisnicko)
                    .FirstOrDefault();

                if (n != null)
                {
                    throw new Exception("Korisnicki nalog postoji");
                }

                //if (n != null)
                //{
                //    TempData["error_poruka"] = "Vec postoji korisnik sa tim korisničkim imenom";
                //    return View("Index"
                //        );
                //}
                //KorisnickiNalog kn = new KorisnickiNalog
                //{
                //    KorisnickoIme = input.korisnicko,
                //    Lozinka = input.korisnikovalozinka
                //};
                //_db.KorisnickiNalog.Add(kn);
                //_db.SaveChanges();
                KlijentT k = new KlijentT
                {
                    Ime = input.Ime,
                    Prezime = input.Prezime,
                    KorisnickoIme=input.korisnicko,
                    BrojTelefona = input.BrojTelefona,
                    Email = input.Email,
                    GradID = input.GradID,
                    LozinkaSalt = GenerateHashClass.GenerateSalt(),
                   
             
                };
                k.LozinkaHash = GenerateHashClass.GenerateHash(k.LozinkaSalt, input.korisnikovalozinka);
                _db.Klijent.Add(k);
                _db.SaveChanges();

                //Slanje maila za uspjesnu registraciju.
                var message = new MimeMessage();
                message.To.Add(new MailboxAddress(input.Email));
                message.From.Add(new MailboxAddress("threadingbard.o.o@outlook.com"));

                message.Subject = "Dobro dosli u threading bar Lejla Habibija";
                message.Body = new TextPart("plain")
                {
                    Text = "Uspjesno ste se registrovali" +" "+ input.Ime+ " " + input.Prezime +"!"
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.outlook.com", 587, false);

                    client.Authenticate("threadingbard.o.o@outlook.com", "Mostar.2016");
                    client.Send(message);

                    client.Disconnect(true);
                }

            }


            return RedirectToAction("Index");
        }
        public IActionResult Logout()
        {
            Korisnik nalog = HttpContext.GetLogiraniKorisnik();
            KlijentT k = HttpContext.GetLogiraniKlijent();
            if(nalog != null)
            {
                AutorizacijskiTokenKorisnik token = _db.AutorizacijskiTokenKorisnik.FirstOrDefault(x => x.KorisnikId == nalog.KorisnikId);
                _db.AutorizacijskiTokenKorisnik.Remove(token);
                _db.SaveChanges();
                HttpContext.Response.RemoveCookie(Autentifikacija.LogiraniKorisnik);
            }
            if (k != null)
            {
                AutorizacijskiTokenKlijent token = _db.AutorizacijskiTokenKlijent.FirstOrDefault(x => x.KlijentId == k.KlijentID);
                _db.AutorizacijskiTokenKlijent.Remove(token);
                _db.SaveChanges();
                HttpContext.Response.RemoveCookie(Autentifikacija.LogiraniKorisnik);
            }
            return RedirectToAction("Index");
        }
    }
}