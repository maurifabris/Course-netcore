using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Adrian.core.Models.CONF
{
    [Table("CONF_TiposDeCentroDeCosto")]
    public class TiposDeCentroDeCosto
    {

        [Key]
        public int ID { get; set; }

        public string Nombre { get; set; }


        //PADRES
        public virtual ICollection<CentrosDeCostos> CentrosDeCosto { get; set; }
        //HIJOS
    }
    [Table("CONF_CentrosDeCosto")]
    public class CentrosDeCostos
    {
        [Key]
        public int ID { get; set; }

        public string Nombre { get; set; }
        public string Identificacion { get; set; }

        // Clave foránea hacia la tabla padre
        public int TipoDeCentroDeCostoID { get; set; }
        public virtual CentrosDeCostos CentroDeCosto { get; set; }
    }
}
