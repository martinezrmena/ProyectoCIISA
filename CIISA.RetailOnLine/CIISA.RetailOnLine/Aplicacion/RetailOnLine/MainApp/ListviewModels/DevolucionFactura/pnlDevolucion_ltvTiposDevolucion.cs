using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.DevolucionFactura
{
    public class pnlDevolucion_ltvTiposDevolucion
    {
        public string TipoDevolucion { get; set; }

        public string CODDOCUMENTO { get; set; }
        public string FECHACREACION { get; set; }
        public string FECHAENTREGA { get; set; }
        public string CODTIPODOCUMENTO { get; set; }
        public string DIAS_VENCIDO { get; set; }
        public string CODPEDIDO { get; set; }
        public Color ItemColorText { get; set; }

        public pnlDevolucion_ltvTiposDevolucion()
        {

        }

        public pnlDevolucion_ltvTiposDevolucion(string tipo)
        {
            this.TipoDevolucion = tipo;
        }
    }
}
