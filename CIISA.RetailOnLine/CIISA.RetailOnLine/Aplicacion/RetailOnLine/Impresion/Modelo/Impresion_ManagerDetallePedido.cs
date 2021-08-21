using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo
{
    internal class Impresion_ManagerDetallePedido
    {

        internal string buscarDetallesPedidosRutaImpresion(string pcodTransaction)
        {
            HelperDetallePedido _helper = new HelperDetallePedido();

            return _helper.buscarDetallesPedidosRutaImpresion(
                pcodTransaction
                );
        }

    }
}
