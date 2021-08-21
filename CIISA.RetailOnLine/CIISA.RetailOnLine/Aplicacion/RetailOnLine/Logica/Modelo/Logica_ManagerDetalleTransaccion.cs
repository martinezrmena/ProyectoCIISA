using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerDetalleTransaccion
    {
        public void actualizarAnulado(TransaccionEncabezado pobjTransaccion)
        {
            HelperDetalleTransaccion _helper = new HelperDetalleTransaccion();

            _helper.actualizarAnulado(pobjTransaccion);
        }

        public void guardarDetalleTransaccion(Cliente pobjCliente, bool v_DevolucionFactura = false)
        {
            HelperDetalleTransaccion _helper = new HelperDetalleTransaccion();

            _helper.guardarDetalleTransaccion(pobjCliente, v_DevolucionFactura);
        }

        public decimal buscarMontoTransaccion(Cliente pobjCliente)
        {
            HelperDetalleTransaccion _helper = new HelperDetalleTransaccion();

            return _helper.buscarMontoTransaccion(pobjCliente);
        }

        public bool BuscarDocumentosSinEnviar()
        {
            return OperationSQL.checkSent(TablesROL._detalleDocumento);
        }
    }
}
