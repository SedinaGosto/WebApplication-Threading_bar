using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class NagradnaIgra
    {
        [Key]
        public int NagradnaIgraID { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumZavrsetka { get; set; }
        public string Opis { get; set; }

        [ForeignKey("NagradaID")]
        public int NagradaID { get; set; }
        public Nagrada Nagrada{ get; set; }

        [ForeignKey("KorisnikId")]
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }

        [ForeignKey("KlijentID")]
        public int KlijentID { get; set; }
        public KlijentT Klijent { get; set; }


    }
}
