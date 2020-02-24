using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class Tretman
    {
        [Key]
        public int TretmanID { get; set; }
        [Required(ErrorMessage ="Polje je obavezno!")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Polje je obavezno!")]
        public int Cijena { get; set; }

    

    }
}
