using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class AutorizacijskiTokenKlijent
    {

        public int Id { get; set; }
        public string Vrijednost { get; set; }
        [ForeignKey(nameof(Klijent))]
        public int KlijentId { get; set; }
        public KlijentT Klijent { get; set; }
        public DateTime VrijemeEvidentiranja { get; set; }
        public string IpAdresa { get; set; }
    }
}
