using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerTransaccion
    {
        public vistaVisita v_view = null;

        public TransaccionEncabezado v_objTransaccionPedido = new TransaccionEncabezado();

        public Logica_ManagerTransaccion() { }

        public Logica_ManagerTransaccion(vistaVisita vista)
        {
            v_view = vista;
        }

        public bool buscarIndicadorTramite()
        {
            HelperIndicador helperIndicador = new HelperIndicador();

            return helperIndicador.buscarIndicadorTramite(v_view.controlador.v_objCliente.v_no_cliente, v_view.controlador.v_objCliente.v_objEstablecimiento.v_codEstablecimiento);
        }

        public TransaccionEncabezado buscarReciboParaAnulaciones(string pcodTransaction,string pcodTipoTransaccion)
        {
            HelperTransaccion _helper = new HelperTransaccion();

            TransaccionEncabezado _objTransaccion = _helper.buscarReciboEncabezadoParaAnulaciones(pcodTransaction,pcodTipoTransaccion);

            _helper.buscarReciboDetalleParaAnulaciones(_objTransaccion);

            return _objTransaccion;
        }

        public TransaccionEncabezado buscarTransaccionParaAnulaciones(string pcodTransaction,string pcodTipoTransaccion)
        {
            HelperTransaccion _helper = new HelperTransaccion();

            var _objTransaccion = _helper.buscarTransaccionEncabezadoParaAnulaciones(pcodTransaction,pcodTipoTransaccion);

            return _helper.buscarTransaccionDetalleParaAnulaciones(_objTransaccion);
        }

        /// <summary>
        /// Metodo encargado de guardar transacciones, restablecer parametros relativos a detalles reses
        /// y para guardar pedidos.
        /// </summary>
        /// <param name="ppnlFormaPago_ltvPagos"></param>
        /// <param name="pobjCliente"></param>
        /// <returns></returns>
        public async Task guardarTransaccion(ListView ppnlFormaPago_ltvPagos, Cliente pobjCliente)
        {
            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            try
            {
                MultiGeneric.BeginTransaction();

                Logica_ManagerAgenteVendedor _managerAgenteVendedor = new Logica_ManagerAgenteVendedor();

                _managerAgenteVendedor.buscarConsecutivoTransaccion(pobjCliente);

                //Es necesario validar si el consecutivo actual ya existe en la BD
                await _managerAgenteVendedor.verificarConsecutivoRepetido(pobjCliente);

                if (!pobjCliente.v_objTransaccion.v_codDocumento.Equals(string.Empty))
                {
                    Logica_ManagerEncabezadoTransaccion _managerEncabezado = new Logica_ManagerEncabezadoTransaccion();

                    await _managerEncabezado.guardarTransaccionEncabezado(pobjCliente);

                    Logica_ManagerDetalleTransaccion _managerDetalle = new Logica_ManagerDetalleTransaccion();

                    _managerDetalle.guardarDetalleTransaccion(pobjCliente, v_view.controlador.v_DevolucionFactura);
                }

                //DetalleResesCarniceria
                Logica_ManagerDetalleReses _managerDetalleReses = new Logica_ManagerDetalleReses();

                if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaCreditoSigla) ||
                    pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaContadoSigla))
                {
                    _managerDetalleReses.CambiarVendido(pobjCliente, true, pobjCliente.v_objTransaccion.v_codPedido, pobjCliente.v_objTransaccion.v_codDocumento);
                }
                //Si es devolucion por factura
                else if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._devolucionSigla) && pobjCliente.v_objTransaccion.v_DevolucionFactura)
                {
                    Logica_ManagerCarniceria logica = new Logica_ManagerCarniceria();

                    //Validamos que el pedido posea detalle reses
                    if (logica.buscarDetalleResPedido(pobjCliente.v_objTransaccion.v_codFactura, true))
                    {
                        //Si hay detalles reses con ese codigo de pedido:
                        //indicamos que ya no esta vendido, lo liberamos para cualquier otro cliente
                        //eliminamos cualquier relación con visceras que tenga
                        logica.AnulacionDetalleResesComprometido(
                            pobjCliente
                            );
                    }
                }
                //Si no es ninguna factura entonces reiniciar su vasignado
                else
                {
                    _managerDetalleReses.CambiarAsignado(pobjCliente, true);
                }

                if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaCreditoSigla))
                {
                    Logica_ManagerDetalleTransaccion _managerDetalleTransaccion = new Logica_ManagerDetalleTransaccion();

                    decimal _total = _managerDetalleTransaccion.buscarMontoTransaccion(pobjCliente);

                    Logica_ManagerFactura _managerFactura = new Logica_ManagerFactura();

                    _managerFactura.guardarFactura(pobjCliente, _total);
                }

                if (!pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._ordenVentaSigla))
                {
                    if (!pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._cotizacionSigla))
                    {
                        if (!pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._recaudacionSigla))
                        {
                            Logica_ManagerInventario _manager = new Logica_ManagerInventario();

                            _manager.actualizarInventarioTransaccion(pobjCliente);
                        }
                    }
                }

                if (ppnlFormaPago_ltvPagos != null)
                {
                    Logica_ManagerPago _managerPago = new Logica_ManagerPago();

                    _managerPago.guardarPagoTransaccion(ppnlFormaPago_ltvPagos, pobjCliente.v_objTransaccion.v_codDocumento);
                }

                _managerAgenteVendedor.actualizarConsecutivoTransaccion(pobjCliente);

                //Guardar datos pedido
                if (!pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._cotizacionSigla))
                {
                    if (!pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._devolucionSigla))
                    {
                        if (!pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._recaudacionSigla))
                        {
                            if (!pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._reciboDineroSigla))
                            {
                                if (!pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._regaliaSigla))
                                {
                                    UtilLogica _util = new UtilLogica();

                                    if (v_view.controlador.TipoAgente.Equals(Agent._carniceroSigla))
                                    {
                                        string Es_Factura = pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaCreditoSigla) || pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaContadoSigla) ? SQL._Si : SQL._No;

                                        v_objTransaccionPedido = new TransaccionEncabezado(pobjCliente.v_objTransaccion);

                                        //Es necesario verificar si se esta procesando un pedido que es desde BackOffice
                                        if (v_view.controlador.v_PedidoBackOffice)
                                        {
                                            _util.actualizarAplicadosPedidoBackOffice(pobjCliente.v_objTransaccion.v_codPedido, pobjCliente, true, Es_Factura);
                                        }
                                        else
                                        {
                                            CrearNuevoPedido(_util.actualizarAplicadosPedidosCliente(pobjCliente, true, Es_Factura), pobjCliente);
                                        }
                                    }
                                    else
                                    {
                                        //Si no importa cambiar entre documentos entonces todos se guardaran como no
                                        _util.actualizarAplicadosPedidosCliente(pobjCliente, false, SQL._No);
                                    }

                                }
                            }
                        }
                    }
                }

                MultiGeneric.Commit();
            }
            catch (Exception ex)
            {
                MultiGeneric.Rollback();

                pobjCliente.v_objTransaccion.v_codDocumento = string.Empty;

                throw new Exception("guardarTransaccion(Cliente pobjCliente)", ex);
            }
        }

        /// <summary>
        /// Permite crear un nuevo pedido con los detalles que no hayan sido aplicados
        /// al guardar un documento dependiendo del indicador Es_factura y del tipo de documento guardado
        /// </summary>
        /// <param name="DetallesPedidoSinModificar"></param>
        /// <param name="pobjCliente"></param>
        private void CrearNuevoPedido(List<TransaccionDetalle> DetallesPedidoSinModificar, Cliente pobjCliente)
        {
            if (v_view != null)
            {
                LogicaVisitaPedido _logica = new LogicaVisitaPedido(v_view);

                if (DetallesPedidoSinModificar != null)
                {
                    if (DetallesPedidoSinModificar.Count > 0)
                    {
                        pobjCliente.v_objTransaccion.v_listaDetalles = DetallesPedidoSinModificar;

                        _logica.guardarNuevoPedido(pobjCliente.v_objTransaccion, true);

                        v_view.controlador.v_objCliente.v_objTransaccion = v_objTransaccionPedido;
                    }
                }
            }
        }
    }
}
