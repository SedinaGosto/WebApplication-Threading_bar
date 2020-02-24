using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.ViewModels
{
    public class NagradnaIgraIndexVM
    {

        public List<row> rows { get; set; }
        public class row
        {
            public int NagradnaIgraID { get; set; }
            public DateTime DatumPocetka { get; set; }
            public DateTime DatumZavrsetka { get; set; }
            public string Opis { get; set; }
            public string Nagrada { get; set; }
            public string Administrator { get; set; }
            public string Klijent { get; set; }
            public string Slika { get; set; }


        }
    }
}
