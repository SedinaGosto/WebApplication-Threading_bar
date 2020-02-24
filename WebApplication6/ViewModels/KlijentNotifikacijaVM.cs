using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.ViewModels
{
    public class KlijentNotifikacijaVM
    {

        public List<Row> lista { get; set; }
        public class Row
        {
            public int NotifikacijaId { get; set; }
            public string Uposlenik { get; set; }
            public DateTime Termin { get; set; }
            public string tretman { get; set; }
        }
    }
}
