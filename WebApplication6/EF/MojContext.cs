using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Models;

namespace WebApplication6.EF
{
    public class MojContext:DbContext
    {


        public MojContext(DbContextOptions<MojContext> options) : base(options)
            {
            }



        public DbSet<ClanciKategorija> ClanciKategorija { get; set; }
        public DbSet<Clanak> Clank { get; set; }
        public DbSet<Grad> Grad { get; set; }

        public DbSet<Nagrada> Nagrada { get; set; }
        public DbSet<NagradnaIgra> NagradnaIgra { get; set; }
        public DbSet<Tretman> Tretman { get; set; }


        public DbSet<Termin> Termin { get; set; }
        public DbSet<Rezervacija> Rezervacija { get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Uloga> Uloga { get; set; }
        public DbSet<KorisniciUloge> KorisniciUloge { get; set; }
        public DbSet<KlijentT> Klijent { get; set; }




      
        public DbSet<AutorizacijskiTokenKlijent> AutorizacijskiTokenKlijent { get; set; }
        public DbSet<AutorizacijskiTokenKorisnik> AutorizacijskiTokenKorisnik { get; set; }


        public DbSet<PoslataNotifikacija> PoslataNotifikacija { get; set; }
        public DbSet<KlijentNotifikacija> KlijentNotifikacija { get; set; }
        public DbSet<Ocjena> Ocjena { get; set; }
        public DbSet<Komentar> Komentar { get; set; }
      




















    }
}
