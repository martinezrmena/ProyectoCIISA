using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using System;
using System.Collections.Generic;
using System.Data;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo.Carniceria
{
    internal class Logica_ManagerCarniceria
    {
        public void CambiarAsignacion(string codproducto, bool variable)
        { 
            HelperDetalleReses _helper = new HelperDetalleReses();

            _helper.CambiarAsignado(codproducto, variable);
        }

        public decimal TotalPesoDisponibleDetalleRes(string codcliente, string articulo) {

            HelperDetalleReses _helper = new HelperDetalleReses();

            return _helper.TotalPesoDisponibleDetalleRes(codcliente, articulo);
        }

        public void ActualizarNumPedido(string old_numpedido, string new_numpedido) {

            HelperDetalleReses _helper = new HelperDetalleReses();

            _helper.ActualizarDetalleResNumPedido(old_numpedido, new_numpedido);

        }

        public void ActualizarNP(Cliente Cliente, string new_numpedido)
        { 
            HelperDetalleReses _helper = new HelperDetalleReses();

            _helper.ActualizarNP(Cliente, new_numpedido);

        }

        public void ActualizarNPReAsignado(Cliente Cliente, string new_numpedido)
        {
            HelperDetalleReses _helper = new HelperDetalleReses();

            _helper.ActualizarNPReAsignado(Cliente, new_numpedido);

        }


        public void ReasignarDetalleRes(string consecutivo, string codcliente)
        {
            HelperDetalleReses _helper = new HelperDetalleReses();

            _helper.ActualizarDetalleResPropietario(consecutivo, codcliente);

        }

        public bool buscarDetalleResExiste(string codproducto, string CodPedido) {

            HelperDetalleReses _helper = new HelperDetalleReses();

            return _helper.ExisteDetalleRes(codproducto, CodPedido);

        }

        public bool EsDetalleRes(string codproducto)
        {
            HelperDetalleReses _helper = new HelperDetalleReses();

            return _helper.EsDetalleRes(codproducto);

        }

        public bool buscarDetalleResComprometidoCliente(string codcliente, string articulo)
        {
            HelperDetalleReses _helper = new HelperDetalleReses();

            return _helper.ExisteDetalleResComprometidoCliente(codcliente, articulo);
        }

        public bool buscarDetalleResPedido(string numpedido, bool vendido) {

            HelperDetalleReses _helper = new HelperDetalleReses();

            return _helper.ExistePedidoDetalleRes(numpedido, vendido);
        }

        public List<pnlTransacciones_ltvDetalleReses> buscarDetalleResesPedido(string no_cliente, string num_pedido, string articulo) {

            HelperDetalleReses _helper = new HelperDetalleReses();

            return _helper.buscarDetalleResesPedido(no_cliente, num_pedido, articulo);
        }

        public List<pnlTransacciones_ltvDetalleReses> buscarDetalleResesProducto(string articulo, string codcliente, string codpedido)
        {
            HelperDetalleReses _helper = new HelperDetalleReses();

            return _helper.buscarDetalleReses(articulo, codcliente, codpedido);

        }

        public List<pnlTransacciones_ltvDetalleReses> buscarDetalleResesFactura(string articulo, string codcliente, string CodFactura)
        {
            HelperDetalleReses _helper = new HelperDetalleReses();

            return _helper.buscarDetalleResesFactura(articulo, codcliente, CodFactura);

        }

        //Desde visita
        public bool buscarDetallesResesProducto(ListView list, string articulo, string codcliente, string codpedido)
        {
            HelperDetalleReses _helper = new HelperDetalleReses();

            return _helper.buscarDetallesReses(list, articulo, codcliente, codpedido);

        }

        //Desde Producto
        public bool buscarDetalleResesProductoDisponible(ListView list, string articulo, string codcliente)
        {
            HelperDetalleReses helper = new HelperDetalleReses();

            return helper.buscarDetallesResesDisponible(list, articulo, codcliente);
        }

        public void CambiarReservado(string consecutivo, bool comp) {

            HelperDetalleReses _helper = new HelperDetalleReses();

            _helper.ActualizarComprometidoDetalleRes(consecutivo, comp);
        }

        public void AnulacionDetalleResesVendido(string codTransaccion, string codcliente) {

            HelperDetalleReses _helper = new HelperDetalleReses();

            _helper.AnulacionVendidoDetalleRes(codTransaccion, codcliente);
        }

        public void AnulacionDetalleResesComprometido(Cliente pobjCliente)
        {
            HelperDetalleReses _helper = new HelperDetalleReses();

            foreach (TransaccionDetalle _objDetalle in pobjCliente.v_objTransaccion.v_listaDetalles)
            {
                if (_objDetalle.v_objProducto.v_estado.Equals(Pedido._devolucionBuena) && EsDetalleRes(_objDetalle.v_objProducto.v_codProducto))
                {
                    _helper.AnulacionComprometidoDetalleResProducto(pobjCliente.v_objTransaccion.v_codFactura, pobjCliente.v_no_cliente, _objDetalle.v_objProducto.v_codProducto);
                }
            }
        }

        public List<pnlTransacciones_ltvDetalleReses> BuscarDetallesResesIndicadoresVisceras(string articulo, string codcliente, bool variable) {

            HelperDetalleReses _helper = new HelperDetalleReses();

            return _helper.BuscarDetallesResesIndicadoresVisceras(articulo, codcliente, variable);

        }

        //Deprecated
        public void buscarListaCajasEscanear(ListView pltvTransacciones, List<pnlTransacciones_ltvProductos> productos)
        {
            HelperScannBox _helper = new HelperScannBox();

            _helper.buscarListaBoxes(pltvTransacciones, productos);
        }
    }
}
