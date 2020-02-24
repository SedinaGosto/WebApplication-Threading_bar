using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.ViewModels
{
    public class TerminIndexVM
    {
        public List<row> rows { get; set; }
        public class row
        {
            public int TerminID { get; set; }
            public DateTime VrijemeTermina { get; set; }
            public string Uposlenik { get; set; }
            public int UposlenikId { get; set; }
            public bool Rezervisan { get; set; }


        }
    }
}
