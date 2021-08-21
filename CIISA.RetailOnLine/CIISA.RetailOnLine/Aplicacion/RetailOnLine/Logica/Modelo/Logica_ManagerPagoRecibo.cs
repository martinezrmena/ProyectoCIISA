using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerPagoRecibo
    {
        public decimal obtenerMontoPorTipoPago(string ptipoPago)
        {
            HelperPagoRecibo _helper = new HelperPagoRecibo();

            return _helper.obtenerMontoPorTipoPago(ptipoPago);
        }

        public void actualizarAnulado(TransaccionEncabezado pobjTransaccion)
        {
            HelperPagoRecibo _helper = new HelperPagoRecibo();

            _helper.actualizarAnulado(pobjTransaccion);
        }

        public void guardarPagoRecibo(Cliente pobjCliente)
        {
            HelperPagoRecibo _helper = new HelperPagoRecibo();

            _helper.guardarPagoRecibo(pobjCliente);
        }
    }
}
