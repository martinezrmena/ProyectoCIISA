using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Modelo
{
    public class Descarga_ManagerTramite
    {
        internal void consultaTramiteDetalles(ListView ptrvDocumentos, Color color)
        {
            HelperDetalleTramite _helper = new HelperDetalleTramite();

            _helper.consultaTramiteDetalles(ptrvDocumentos, color);
        }

    }
}
