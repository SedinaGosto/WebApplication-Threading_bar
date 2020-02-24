using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.EF;
using WebApplication6.Models;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication6.Helper
{
    public static class Autentifikacija
    {
        public static readonly string LogiraniKorisnik = "logirani_korisnik";
       

        public static void SetLogiraniKorisnik(this HttpContext context, Korisnik korisnik, bool snimiUCookie=false)
        {

           MojContext db = context.RequestServices.GetService<MojContext>();

            string stariToken = context.Request.GetCookieJson<string>(LogiraniKorisnik);
            if (stariToken != null)
            {
                AutorizacijskiTokenKorisnik obrisati = db.AutorizacijskiTokenKorisnik.FirstOrDefault(x => x.Vrijednost == stariToken);
                if (obrisati != null)
                {
                    db.AutorizacijskiTokenKorisnik.Remove(obrisati);
                    db.SaveChanges();
                }
            }

            if (korisnik != null)
            {

                string token = Guid.NewGuid().ToString();
                db.AutorizacijskiTokenKorisnik.Add(new AutorizacijskiTokenKorisnik
                {
                    Vrijednost = token,
                    KorisnikId = korisnik.KorisnikId,
                    VrijemeEvidentiranja = DateTime.Now
                });
                db.SaveChanges();
                context.Response.SetCookieJson(LogiraniKorisnik, token);
            }
        }
        public static void SetLogiraniKlijent(this HttpContext context, KlijentT korisnik, bool snimiUCookie = false)
        {

            MojContext db = context.RequestServices.GetService<MojContext>();

            string stariToken = context.Request.GetCookieJson<string>(LogiraniKorisnik);
            if (stariToken != null)
            {
                AutorizacijskiTokenKlijent obrisati = db.AutorizacijskiTokenKlijent.FirstOrDefault(x => x.Vrijednost == stariToken);
                if (obrisati != null)
                {
                    db.AutorizacijskiTokenKlijent.Remove(obrisati);
                    db.SaveChanges();
                }
            }

            if (korisnik != null)
            {

                string token = Guid.NewGuid().ToString();
                db.AutorizacijskiTokenKlijent.Add(new AutorizacijskiTokenKlijent
                {
                    Vrijednost = token,
                    KlijentId = korisnik.KlijentID,
                    VrijemeEvidentiranja = DateTime.Now
                });
                db.SaveChanges();
                context.Response.SetCookieJson(LogiraniKorisnik, token);
            }
        }

        public static string GetTrenutniToken(this HttpContext context)
        {
            return context.Request.GetCookieJson<string>(LogiraniKorisnik);
        }

        public static Korisnik GetLogiraniKorisnik(this HttpContext context)
        {
            MojContext db = context.RequestServices.GetService<MojContext>();

            string token = context.Request.GetCookieJson<string>(LogiraniKorisnik);
            if (token == null)
                return null;

            return db.AutorizacijskiTokenKorisnik
                .Where(x => x.Vrijednost == token)
                .Select(s => s.Korisnik)
                .SingleOrDefault();

        }
        public static KlijentT GetLogiraniKlijent(this HttpContext context)
        {
            MojContext db = context.RequestServices.GetService<MojContext>();

            string token = context.Request.GetCookieJson<string>(LogiraniKorisnik);
            if (token == null)
                return null;

            return db.AutorizacijskiTokenKlijent
                .Where(x => x.Vrijednost == token)
                .Select(s => s.Klijent)
                .SingleOrDefault();

        }

    }
}
