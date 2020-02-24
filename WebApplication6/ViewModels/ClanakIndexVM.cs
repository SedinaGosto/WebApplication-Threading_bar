using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.ViewModels
{
    public class ClanakIndexVM
    {
        public List<row> rows { get; set; }
        public class row
        {
            public int ClanakID { get; set; }
            public string Naslov { get; set; }
            public DateTime DatumObjave { get; set; }
            public string Podnaslov { get; set; }
            public string Kategorija { get; set; }
            public string Korisnik { get; set; }

        }
    }
}
