using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public  class Logica_ManagerPago
    {
        public void actualizarAnulado(TransaccionEncabezado pobjTransaccion)
        {
            HelperPago _helper = new HelperPago();

            _helper.actualizarAnulado(pobjTransaccion);
        }

        public void guardarPagoTransaccion(ListView ppnlFormaPago_ltvPagos, string pcodTransaction)
        {
            HelperPago _helper = new HelperPago();

            _helper.guardarPagoTransaccion(ppnlFormaPago_ltvPagos, pcodTransaction);
        }

        public decimal obtenerMontoPorTipoPago(string ptipoPago)
        {
            HelperPago _helper = new HelperPago();

            return _helper.obtenerMontoPorTipoPago(ptipoPago);
        }
    }
}
