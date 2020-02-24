using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class KorisniciUloge
    {
        [Key]
        public int KorisnikUlogaId { get; set; }

        public int? KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }


        public int? UlogaId { get; set; }
        public Uloga Uloga { get; set; }

        public DateTime? DatumIzmjene { get; set; }
    }
}
