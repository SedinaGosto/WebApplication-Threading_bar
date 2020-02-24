using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.ViewModels
{
    public class NagradnaIgraDodajVM
    {

        public int NagradnaIgraID { get; set; }
        [Required(ErrorMessage = "Polje je obavezno!")]
        public DateTime DatumPocetka { get; set; }
        [Required(ErrorMessage = "Polje je obavezno!")]
        public DateTime DatumZavrsetka { get; set; }
        [Required(ErrorMessage = "Polje je obavezno!")]
        public string Opis { get; set; }
        public List<SelectListItem> Nagrade { get; set; }
        public int NagradaID { get; set; }
        public string Administrator { get; set; }
        public int AdministratorID { get; set; }
        public List<SelectListItem> Klijenti { get; set; }
        public int KlijentID { get; set; }
    }
}
