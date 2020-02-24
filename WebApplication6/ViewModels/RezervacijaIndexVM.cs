using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.ViewModels
{
    public class RezervacijaIndexVM
    {
        public List<row> rows { get; set; }
        public class row
        {
            public int RezervacijaId { get; set; }
            public DateTime DatumRezervacije { get; set; }
            public string Klijent { get; set; }
            public DateTime Termin { get; set; }
            public string Naziv { get; set; }
            public bool Odobrena { get; set; }

        }
    }
}
