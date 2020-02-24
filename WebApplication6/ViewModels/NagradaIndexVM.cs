using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.ViewModels
{
    public class NagradaIndexVM
    {

        public List<row> rows { get; set; }

        public class row
        {
            public int NagradaID { get; set; }
            public string Naziv { get; set; }
        }
    }
}
