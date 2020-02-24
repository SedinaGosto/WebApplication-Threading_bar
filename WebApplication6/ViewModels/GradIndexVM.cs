using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.ViewModels
{
    public class GradIndexVM
    {
        public List<row> rows { get; set; }

        public class row
        {
            public int GradID { get; set; }
            public string Naziv { get; set; }
           


        }

    }
}
