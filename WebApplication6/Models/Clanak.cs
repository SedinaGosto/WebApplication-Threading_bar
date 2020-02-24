using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class Clanak
    {

        [Key]
        public int ClanakID { get; set; }
        public string Naslov { get; set; }
        public string TekstClanka { get; set; }
        public DateTime DatumObjave { get; set; }
        public string Podnaslov { get; set; }
     
        [ForeignKey("ClanciKategorijaID")]
        public int ClanciKategorijaID { get; set; }
        public ClanciKategorija ClanciKategorija { get; set;}

        [ForeignKey("KorisnikId")]
        public int KorisnikId{ get; set; }
        public Korisnik Korisnik { get; set; }
        public byte[] Slika { get; set; }

    }
}
