using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerDetalleRecibo
    {
        public void actualizarAnulado(TransaccionEncabezado pobjTransaccion)
        {
            HelperDetalleRecibo _manager = new HelperDetalleRecibo();

            _manager.actualizarAnulado(pobjTransaccion);
        }

        public decimal obtenerPagosFactura(string pcodFactura)
        {
            HelperDetalleRecibo _manager = new HelperDetalleRecibo();

            return _manager.obtenerPagosFactura(pcodFactura);
        }

        public void guardarReciboDetalle(ListView ppnlAbono_ltvAbonos, Cliente pobjCliente)
        {
            HelperDetalleRecibo _manager = new HelperDetalleRecibo();

            _manager.guardarReciboDetalle(ppnlAbono_ltvAbonos, pobjCliente);
        }
    }
}
