using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.ViewModels
{
    public class TretmanUrediVM
    {
        public int TretmanID { get; set; }
        [Required(ErrorMessage = "Polje je obavezno!")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Polje je obavezno!")]
        [Range(0, 1000,ErrorMessage ="Cijena ne smije biti veca od 1000KM!")]
        public int Cijena { get; set; }

   
    }
}
