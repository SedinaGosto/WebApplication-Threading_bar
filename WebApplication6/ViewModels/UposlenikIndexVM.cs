using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;

namespace WebApplication6.ViewModels
{
    public class UposlenikIndexVM
    {
        public List<row> rows { get; set; }
        public Korisnik uposlenik { get; set; }
        [Required(ErrorMessage ="Polje je obavezno!")]
        public string KorisnickoIme { get; set; }
        [Required(ErrorMessage = "Polje je obavezno!")]
        [StringLength(100, ErrorMessage = "Lozinka mora sadržavati mininalno 4 karaktera.", MinimumLength = 4)]

        public string Pasword { get; set; }
        public class row
        {
            public int KorisnikId { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Email { get; set; }
            public string Telefon { get; set; }
            public string KorisnickoIme { get; set; }
            public string LozinkaHash { get; set; }
            public string LozinkaSalt { get; set; }
            public bool? Status { get; set; }
            public byte[] Slika { get; set; }




        }
    }
}
