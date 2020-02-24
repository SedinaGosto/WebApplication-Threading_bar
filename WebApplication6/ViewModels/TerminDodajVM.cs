using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.ViewModels
{
    public class TerminDodajVM
    {

        public int TerminId { get; set; }
        [Required(ErrorMessage = "Polje je obavezno!")]
        public DateTime VrijemeTermina { get; set; }
        public bool Rezervisan { get; set; }

        public List<SelectListItem> Uposlenici { get; set; }
        public int UposlenikID { get; set; }


    }
}
