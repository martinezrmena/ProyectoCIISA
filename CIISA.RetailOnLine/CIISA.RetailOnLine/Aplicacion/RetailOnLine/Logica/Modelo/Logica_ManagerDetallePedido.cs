using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerDetallePedido
    {
        public List<TransaccionDetalle> buscarDetallePedidoPorCodigoPedido(string pcodPedido, bool Validar, string Es_Factura, string codcliente)
        {
            HelperDetallePedido _helper = new HelperDetallePedido();

            return _helper.buscarDetallePedidoPorCodigoPedido(pcodPedido, Validar, Es_Factura, codcliente);
        }

        public List<TransaccionDetalle> actualizarAplicadoListaDetallesPedido(List<TransaccionDetalle> plistaDocumentos, bool Validar, string Es_Factura, List<string> Cod_Documento, string codcliente)
        {
            HelperDetallePedido _helper = new HelperDetallePedido();

            return _helper.actualizarAplicadoListaDetallesPedido(plistaDocumentos, Validar, Es_Factura, Cod_Documento, codcliente);
        }

        public void guardarDetallePedido(Cliente pobjCliente, string TipoAgente)
        {
            HelperDetallePedido _helper = new HelperDetallePedido();

            _helper.guardarDetallePedido(pobjCliente, TipoAgente);
        }

        public bool BuscarDetallePedido(string codpedido, string codarticulo, string Es_Factura)
        {
            HelperDetallePedido _helper = new HelperDetallePedido();

            return _helper.BuscarDetallePedido(codpedido, codarticulo, Es_Factura);
        }

        public void actualizarDetallePedido(Producto v_objProducto, string Cod_Document)
        {
            HelperDetallePedido _helper = new HelperDetallePedido();

            _helper.actualizarDetallePedido(v_objProducto, Cod_Document);
        }

        public bool DetallePedido_Vacio()
        {
            HelperDetallePedido _helper = new HelperDetallePedido();

            return _helper.DetallePedido_Vacio();
        }
    }
}
