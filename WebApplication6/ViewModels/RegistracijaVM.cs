using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.ViewModels
{
    public class RegistracijaVM
    {
        [Required(ErrorMessage ="Polje je obavezno.")]
        [StringLength(100, ErrorMessage = "Ime ne smije biti prazno.", MinimumLength = 1)]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(100,ErrorMessage ="Ne valja", MinimumLength = 2)]
        public string Prezime { get; set; }

        ////[RegularExpression(@"[0-9]{2}[.]{1}[0-9]{2}[.]{1}[0-9]{4}", ErrorMessage = "Datum je u formatu: dd.mm.yyyy")]
        //[DisplayFormat(DataFormatString = "{0:dd/M/yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime DatumRodjenja { get; set; }
        [Required(ErrorMessage = "Polje je obavezno.")]
        [RegularExpression(@"[0-9]{9}", ErrorMessage = "Kontakt mora sadrzavati 9 znamenki.")]
        public string BrojTelefona { get; set; }
        [Required(ErrorMessage = "Polje je obavezno.")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)", ErrorMessage = "Email treba biti u formatu example@example.net")]
        public   string Email { get; set; }


        public List<SelectListItem> gradovi { get; set; }
        public int GradID { get; set; }
        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(100, ErrorMessage = "Korisničko ime mora sadržavati mininalno 3 karaktera.", MinimumLength = 3)]
        public string korisnicko { get; set; }
        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(100, ErrorMessage = "Lozinka mora sadržavati mininalno 4 karaktera.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string korisnikovalozinka { get; set; }
        
    }
}
