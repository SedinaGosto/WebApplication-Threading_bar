using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.ViewModels
{
    public class OcjenaIndexVM
    {

        public List<row> rows { get; set; }
        public class row
        {
            public int OcjenaId { get; set; }

            public string Korisnik { get; set; }

            public string Klijent { get; set; }

            public int OcjenaInt { get; set; }
            public DateTime DatumOcjenjivanja { get; set; }




        }
    }
}
