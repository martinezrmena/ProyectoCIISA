using CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Helpers;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Modelo
{
    internal class Settlement_ManagerTransaccionBK
    {

        internal void consultarTransaccionEncabezadosBK(ListView pltvTransacciones)
        {
            HelperEncabezadoTransaccionBK _helper = new HelperEncabezadoTransaccionBK();

            _helper.consultarTransaccionEncabezadosBK(pltvTransacciones);
        }

    }
}
