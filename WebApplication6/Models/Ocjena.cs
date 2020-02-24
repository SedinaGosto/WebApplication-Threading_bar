using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class Ocjena
    {
        [Key]
        public int OcjenaId { get; set; }

        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }

        public int KlijentId { get; set; }
        public KlijentT Klijent { get; set; }

        public int OcjenaInt { get; set; }
        public DateTime DatumOcjenjivanja { get; set; }
    }
}
