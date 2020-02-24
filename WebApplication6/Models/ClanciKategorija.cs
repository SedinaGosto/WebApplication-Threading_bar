using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class ClanciKategorija
    {
        [Key]
        public int ClanciKategorijaID { get; set; }
        public string Naziv { get; set; }
        public DateTime DatumKreiranja { get; set; }
    }
}
