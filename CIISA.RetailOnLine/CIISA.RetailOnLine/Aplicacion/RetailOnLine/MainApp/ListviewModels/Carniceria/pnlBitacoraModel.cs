using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria
{
    public class pnlBitacoraModel
    {
        public const string codcliente = "Cod_Cliente";
        public const string fechasvisita = "FechaVisita";
        public const string volcompra = "Vol_Compra";
        public const string porcentajecompra = "Porcentaje_Compra";
        public const string quejas = "Quejas";
        public const string oportunidades = "Oportunidades";
        public const string competencias = "Competencias";

        public string Cod_Cliente { get; set; }
        public string FechaVisita { get; set; }
        public string Vol_Compra { get; set; }
        public string Porcentaje_Compra { get; set; }
        public string SituacionNegocio { get; set; }
        public string Quejas { get; set; }
        public string Oportunidades { get; set; }
        public string Competencias { get; set; }
    }
}
