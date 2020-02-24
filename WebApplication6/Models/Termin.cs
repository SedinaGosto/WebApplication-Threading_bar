using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class Termin
    {
        [Key]
        public int Id { get; set; }

        public DateTime VrijemeTermina { get; set; }
        public bool Rezervisan { get; set; }

        [ForeignKey("KorisnikId")]
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
    }
}
