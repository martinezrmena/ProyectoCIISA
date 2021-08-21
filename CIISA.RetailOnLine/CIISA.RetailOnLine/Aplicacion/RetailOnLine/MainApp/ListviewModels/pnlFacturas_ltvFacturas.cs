using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels
{
    public class pnlFacturas_ltvFacturas
    {
        public string NO_FISICO { get; set; }
        public string SALDO { get; set; }
        public string MONTO { get; set; }
        public string PAGOS { get; set; }
        public string FECHA_DOC { get; set; }
        public string FECHA_VENCE { get; set; }
        public string DIAS_VENCIDO { get; set; }
        public string DESCRIPCION { get; set; }
        public string TIPO_DOC { get; set; }
        public string COD_PEDIDO { get; set; }
        public Color ItemColorText { get; set; }


        public string[] ToArray()
        {
            string[] Respuesta = new string[9];
            Respuesta[0] = this.NO_FISICO;
            Respuesta[1] = this.SALDO;
            Respuesta[2] = this.MONTO;
            Respuesta[3] = this.MONTO;
            Respuesta[4] = this.MONTO;
            Respuesta[5] = this.MONTO;
            Respuesta[6] = this.MONTO;
            Respuesta[7] = this.MONTO;
            Respuesta[8] = this.MONTO;
            Respuesta[9] = this.MONTO;

            return Respuesta;
        }
    }
}
