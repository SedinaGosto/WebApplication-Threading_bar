using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;

namespace WebApplication6.ViewModels
{
    public class TretmanIndexVM
    {
        public List<row> rows { get; set; }
        public Tretman tretman { get; set; }
        public class row
        {
            public int TretmanID { get; set; }
            public string Naziv { get; set; }
            public int Cijena { get; set; }
       




        }

    }
}
