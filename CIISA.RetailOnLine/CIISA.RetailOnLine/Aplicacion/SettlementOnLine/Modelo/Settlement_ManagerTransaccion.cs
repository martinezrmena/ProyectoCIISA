using CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Helpers;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Modelo
{
    internal class Settlement_ManagerTransaccion
    {
        internal void consultarTransaccionEncabezados(ListView pltvTransacciones)
        {
            HelperEncabezadoTransaccion _helper = new HelperEncabezadoTransaccion();

            _helper.consultarTransaccionEncabezados(pltvTransacciones);
        }

    }
}
