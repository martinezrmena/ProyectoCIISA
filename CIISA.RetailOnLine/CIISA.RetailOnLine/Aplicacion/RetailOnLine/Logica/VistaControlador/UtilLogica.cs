using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.VistaControlador
{
    public class UtilLogica
    {
        public decimal obtenerCantidadProductoComprometido(string pcodProducto, List<Producto> plistaProductoComprometido)
        {
            decimal _cantProductoComprometido = Numeric._zeroDecimalInitialize;

            foreach (Producto _objProducto in plistaProductoComprometido)
            {
                if (_objProducto.v_codProducto.Equals(pcodProducto))
                {
                    _cantProductoComprometido = _objProducto.v_cantTransaccion;
                }
            }

            return _cantProductoComprometido;
        }


        public List<TransaccionDetalle> actualizarAplicadosPedidoBackOffice(string CodPedido, Cliente pobjCliente, bool Validar, string Es_Factura)
        {
            List<TransaccionDetalle> DetallesPedidoSinModificar = new List<TransaccionDetalle>();

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            List<string> Cod_Documento = new List<string>();

            try
            {
                MultiGeneric.BeginTransaction();

                Logica_ManagerEncabezadoPedido _managerEncabezado = new Logica_ManagerEncabezadoPedido();

                _managerEncabezado.actualizarAplicadoPedidoBackOffice(CodPedido, pobjCliente);

                List<TransaccionDetalle> _listaDocumentos = new List<TransaccionDetalle>();

                foreach (TransaccionEncabezado _objTransaccionE in pobjCliente.v_objListaPedidos)
                {
                    Cod_Documento.Add(_objTransaccionE.v_codDocumento);
                    _listaDocumentos.AddRange(_objTransaccionE.v_listaDetalles);
                }

                Logica_ManagerDetallePedido _managerDetalle = new Logica_ManagerDetallePedido();

                //Regresa aquellos elementos que no han sido actualizados por no formar parte del pedido
                //Todo por el nuevo indicador
                DetallesPedidoSinModificar = _managerDetalle.actualizarAplicadoListaDetallesPedido(_listaDocumentos, Validar, Es_Factura, Cod_Documento, pobjCliente.v_no_cliente);

                MultiGeneric.Commit();
            }
            catch (Exception ex)
            {
                MultiGeneric.Rollback();

                throw new Exception("actualizarAplicadosPedidosCliente(...)", ex);
            }

            return DetallesPedidoSinModificar;
        }

        /// <summary>
        /// Permite actualizar los pedidos, marca como aplicados a los anteriores e ingresa los nuevos
        /// </summary>
        /// <param name="pobjCliente">Objeto que contiene información del cliente y documentos</param>
        /// <param name="Validar">Define si se validará si es factura o no</param>
        /// <param name="Es_Factura">Contiene un S en caso de que el documento sea facturao una N en caso de que no</param>
        /// <returns></returns>
        public List<TransaccionDetalle> actualizarAplicadosPedidosCliente(Cliente pobjCliente, bool Validar, string Es_Factura)
        {
            List<TransaccionDetalle> DetallesPedidoSinModificar = new List<TransaccionDetalle>();

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            List<string> Cod_Documento = new List<string>();

            try
            {
                MultiGeneric.BeginTransaction();

                Logica_ManagerEncabezadoPedido _managerEncabezado = new Logica_ManagerEncabezadoPedido();

                if (_managerEncabezado.ExistePedidoCliente(pobjCliente, Validar, Es_Factura))
                {
                    _managerEncabezado.actualizarAplicado(pobjCliente, Validar);

                    List<TransaccionDetalle> _listaDocumentos = new List<TransaccionDetalle>();

                    foreach (TransaccionEncabezado _objTransaccionE in pobjCliente.v_objListaPedidos)
                    {
                        Cod_Documento.Add(_objTransaccionE.v_codDocumento);
                        _listaDocumentos.AddRange(_objTransaccionE.v_listaDetalles);
                    }

                    Logica_ManagerDetallePedido _managerDetalle = new Logica_ManagerDetallePedido();

                    //Regresa aquellos elementos que no han sido actualizados por no formar parte del pedido
                    //Todo por el nuevo indicador
                    DetallesPedidoSinModificar = _managerDetalle.actualizarAplicadoListaDetallesPedido(_listaDocumentos, Validar, Es_Factura, Cod_Documento, pobjCliente.v_no_cliente);
                }

                MultiGeneric.Commit();
            }
            catch (Exception ex)
            {
                MultiGeneric.Rollback();

                throw new Exception("actualizarAplicadosPedidosCliente(...)", ex);
            }

            return DetallesPedidoSinModificar;

        }
    }
}
