using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class Rezervacija
    {
        public int Id { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public int KlijentId { get; set; }
        public KlijentT Klijent { get; set; }

        public int TerminId { get; set; }
        public Termin Termin { get; set; }
        public int TretmanId { get; set; }
        public Tretman Tretman { get; set; }
    }
}
