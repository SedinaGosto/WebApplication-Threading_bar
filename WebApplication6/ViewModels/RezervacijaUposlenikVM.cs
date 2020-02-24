using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.ViewModels
{
    public class RezervacijaUposlenikVM
    {
        public List<row> rows { get; set; }
       
        public class row
        {
            public int UposlenikID { get; set; }
            public string ImePrezime { get; set; }

            public string Slika { get; set; }

        }
    }
}
