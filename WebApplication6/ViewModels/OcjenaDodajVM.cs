using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.ViewModels
{
    public class OcjenaDodajVM
    {
        public int KorisnkId { get; set; }
        public string Korisnik { get; set; }

        public int KlijentId { get; set; }
        public string Klijent { get; set; }
        public int OcjenaInt { get; set; }
        public DateTime DatumOcjenjivanja { get; set; }

    }
}
