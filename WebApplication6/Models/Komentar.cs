using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class Komentar
    {

        public int KomentarId { get; set; }

        public string TekstKomentara { get; set; }
        public DateTime  DatumKreiranja { get; set; }

        public int KlijentId { get; set; }
        public KlijentT Klijent { get; set; }

        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }

        public bool SakrijKomentar { get; set; }

    }
}
