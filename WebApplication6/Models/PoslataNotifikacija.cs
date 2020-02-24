using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebApplication6.Models;

namespace WebApplication6.Models
{
    public class PoslataNotifikacija
    {
        public int Id { get; set; }

   
        public int RezervacijaId { get; set; }
        public Rezervacija rezervacija { get; set; }

        public DateTime DatumSlanja { get; set; }
        public bool IsProcitano { get; set; }
    }
}
