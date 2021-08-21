using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Helpers;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Modelo
{
    public class Descarga_ManagerRecibo
    {

        internal void consultaReciboDetalles(ListView ptrvDocumentos, Color color)
        {
            HelperDetalleRecibo _helper = new HelperDetalleRecibo();

            _helper.consultaReciboDetalles(ptrvDocumentos, color);
        }

    }
}
