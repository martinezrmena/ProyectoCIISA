using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo.Exoneracion
{
    public class Result_Exoneracion
    {
        //Sera el identificador de las consultas
        public decimal Impuesto { get; set; }

        //Sera el total del impuesto aplicado sobre la linea (incluyendo si hay exoneracion o no)
        //y multiplicada por el total de la misma linea para obtener el total global
        public decimal TotalImpuesto { get; set; }

        //Es el total de la linea al cual se le aplico un porcentaje del impuesto
        public decimal Gravado { get; set; }

        //Es el tipo de exoneracion aplicada al impuesto
        public string Tipo { get; set; }
    }
}
