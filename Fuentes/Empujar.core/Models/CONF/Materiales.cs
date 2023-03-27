using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adrian.core.Models.CONF
{
    [Table("CONF_Materiales")]
    public class Materiales
    {
        [Key]
        public int ID { get; set; }

        public string Nombre { get; set; }

        public decimal PrecioCompra { get; set; }

        public decimal PrecioVenta { get; set; }

        //PADRES

        //HIJOS
    }

    [Table("CONF_PreciosHistoricosDeMateriales")]
    public class PreciosHistoricosDeMateriales
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Material")]
        public int MaterialID { get; set; }

        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }

        public decimal Fecha { get; set; }

        public virtual Materiales Material { get; set; }
    }
}
