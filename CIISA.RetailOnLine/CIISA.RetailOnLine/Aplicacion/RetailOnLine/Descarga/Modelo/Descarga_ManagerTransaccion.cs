using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Modelo
{
    public class Descarga_ManagerTransaccion
    {
        internal void consultaTransaccionDetalles(ListView ptrvDocumentos, Color color)
        {
            HelperDetalleTransaccion _helper = new HelperDetalleTransaccion();

            _helper.consultaTransaccionDetalles(ptrvDocumentos, color);
        }
    }
}
