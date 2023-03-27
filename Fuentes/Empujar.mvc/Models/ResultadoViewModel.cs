using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adrian.mvc.Models
{
    public class ResultadoViewModel
    {
        public ResultadoViewModel()
        {
            returnValue = "";
        }

        public string returnValue { get; set; }

        public string NumeralID { get; set; }
        public string Disponible { get; set; }
    }
}
