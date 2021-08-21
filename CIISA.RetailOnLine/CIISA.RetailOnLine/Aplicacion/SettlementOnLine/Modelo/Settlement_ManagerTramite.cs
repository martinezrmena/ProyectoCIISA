using Xamarin.Forms;
using CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Helpers;

namespace CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Modelo
{
    internal class Settlement_ManagerTramite
    {
        internal void consultarTramiteEncabezados(ListView pltvTransacciones)
        {
            HelperEncabezadoTramite _helper = new HelperEncabezadoTramite();

            _helper.consultarTramiteEncabezados(pltvTransacciones);
        }

    }
}
