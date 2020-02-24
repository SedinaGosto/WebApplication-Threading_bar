using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.ViewModels
{
    public class KlijentIndexVM
    {

        public List<row> rows { get; set; }
        public class row
        {
            public int KlijentID { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Email { get; set; }
            public string BrojTelefona { get; set; }
            public string KorisnickoIme { get; set; }
            public string Grad { get; set; }




        }
    }
}
