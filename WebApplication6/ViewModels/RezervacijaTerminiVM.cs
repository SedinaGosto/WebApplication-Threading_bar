using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.ViewModels
{
    public class RezervacijaTerminiVM
    {
        public string ImeUposlenika { get; set; }
        public List<row> rows { get; set; }
        public class row
        {
            public int TerminID { get; set; }
            public DateTime VrijemeTermina { get; set; }

     


        }
    }
}
