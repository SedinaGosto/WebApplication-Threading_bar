using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.ViewModels
{
    public class KomentarDodajVM
    {
        public int KomentarId { get; set; }

        public string TekstKomentara { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public int KlijentId { get; set; }
        public int KorisnikId { get; set; }
        public bool SakrijKomentar { get; set; }
    }
}
