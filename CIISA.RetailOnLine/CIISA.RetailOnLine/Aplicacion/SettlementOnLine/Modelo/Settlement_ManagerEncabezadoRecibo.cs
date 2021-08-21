using CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Helpers;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Modelo
{
    internal class Settlement_ManagerEncabezadoRecibo
    {
        internal void consultaReciboEncabezados(ListView pltvTransacciones)
        {
            HelperEncabezadoRecibo _helper = new HelperEncabezadoRecibo();

            _helper.consultarReciboEncabezados(pltvTransacciones);
        }

    }
}
