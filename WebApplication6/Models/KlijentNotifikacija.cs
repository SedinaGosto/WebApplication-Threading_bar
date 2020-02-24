using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class KlijentNotifikacija
    {
        [Key]

        public int NotifikacijaId { get; set; }
        public int RezervacijaId { get; set; }
        public Rezervacija Rezervacija { get; set; }
        public int KlijentId { get; set; }
        public KlijentT Klijent { get; set; }

        public DateTime DatumSlanja { get; set; }
        public bool IsProcitano { get; set; }
    }
}
