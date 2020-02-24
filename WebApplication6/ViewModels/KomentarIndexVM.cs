using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.ViewModels
{
    public class KomentarIndexVM
    {
        public List<row> rows { get; set; }
        public string Korisnik { get; set; }
        public int KorisnikId { get; set; }
        public double ProsjecnaOcjena { get; set; }
        public class row
        {
            public int KomentarId { get; set; }

            public string TekstKomentara { get; set; }
            public DateTime DatumKreiranja { get; set; }


            public string Klijent { get; set; }
            public int KlijentId { get; set; }



        }

     
    }
}
