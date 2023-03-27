using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Adrian.mvc.Models
{
    public class GalponesViewModel
    {

        [Required(ErrorMessage = "Requerido.")]
        public string Nombre { get; set; }

    }
}
