using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adrian.core.Models.CONF
{
    [Table("CONF_Clientes")]
    public class Clientes
    {
        [Key]
        public int ID { get; set; }

        public string Nombre { get; set; }

        public int Cuit { get; set; }

        public int Telefono { get; set; }
        public int ProvinciaID { get; set; }
        public string Localidad { get; set; }
        public string Direccion { get; set; }
        public string Observaciones { get; set; }


        //PADRES

        //HIJOS
    }

}
