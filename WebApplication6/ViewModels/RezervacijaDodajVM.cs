using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.ViewModels
{
    public class RezervacijaDodajVM
    {
        public int RezervacijaID { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public int TerminId { get; set; }
        public DateTime VrijemeTermina { get; set; }
        public List<SelectListItem> Tretmani { get; set; }
        public int TeretmanId { get; set; }
        public string Klijent { get; set; }
        public int KlijentId { get; set; }

    }
}
