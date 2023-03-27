using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Adrian.mvc.Models
{
    public class MaterialesViewModel
    {
        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Precio de Compra es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El campo Precio de Compra debe ser mayor que cero.")]
        public decimal PrecioCompra { get; set; }

        [Required(ErrorMessage = "El campo Precio de Venta es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El campo Precio de Venta debe ser mayor que cero.")]
        public decimal PrecioVenta { get; set; }
    }
}