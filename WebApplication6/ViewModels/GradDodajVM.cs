using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.ViewModels
{
    public class GradDodajVM
    {

        public int GradID { get; set; }
        [Required(ErrorMessage = "Polje je obavezno!")]
        public string Naziv { get; set; }
    }
}
