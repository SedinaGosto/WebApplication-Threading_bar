using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class Nagrada
    {
        [Key]
        public int NagradaID { get; set; }
        public string Naziv { get; set; }
        public byte[] Slika { get; set; }


    }
}
