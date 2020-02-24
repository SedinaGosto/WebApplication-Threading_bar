using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.ViewModels
{
    public class ClanakDodaj
    {

        public int ClanakID { get; set; }
        [Required(ErrorMessage ="Polje je obavezno!")]
        [StringLength(100, ErrorMessage = "Naslov  mora sadržavati mininalno 3 karaktera.", MinimumLength = 3)]
        public string Naslov { get; set; }
        [Required(ErrorMessage = "Polje je obavezno!")]
        public string TekstClanka { get; set; }
        [Required(ErrorMessage = "Polje je obavezno!")]
        public DateTime DatumObjave { get; set; }
        [Required(ErrorMessage = "Polje je obavezno!")]
        public string Podnaslov { get; set; }
        public List<SelectListItem> ClanciKategorija { get; set; }
       public int ClanciKategorijaID { get; set; }
      public string Korisnik { get; set; }
        public int KorisnikId { get; set; }
        public IFormFile Photo { get; set; }

    }
}
