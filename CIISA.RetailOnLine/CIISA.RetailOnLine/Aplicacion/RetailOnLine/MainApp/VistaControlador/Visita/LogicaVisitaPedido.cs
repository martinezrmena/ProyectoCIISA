using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita
{
    internal class LogicaVisitaPedido
    {
        private vistaVisita view = null;

        internal LogicaVisitaPedido(vistaVisita pview)
        {
            view = pview;
        }

        private async Task extraerDetalle(Producto pobjProducto,LogicaVisitaLtvProducto plogica,TransaccionEncabezado pobjTransaccionEncabezado)
        {
            var Source = view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

            foreach (TransaccionDetalle _objDetalle in pobjTransaccionEncabezado.v_listaDetalles)
            {
                pobjProducto = _objDetalle.v_objProducto;

                await plogica.verificarPrecio(
                    pobjProducto,
                    Source.Count - 1,
                    false
                    );
            }
        }

        internal async Task cargaPedidosRecuperar(bool validar, string Es_Factura)
        {
            Producto _objProducto = new Producto();

            if (view.controlador.v_objCliente.v_objTransaccion.v_recuperarDocumento)
            {
                var Source = view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

                view.controlador.COD_PEDIDO = view.controlador.v_objCliente.v_objTransaccion.Cod_RecuperarPedido;

                foreach (TransaccionDetalle _objDetalle in view.controlador.v_objCliente.v_objTransaccion.v_listaDetalles)
                {
                    _objProducto = _objDetalle.v_objProducto;

                    LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(view);                    

                    await _logica.verificarPrecio(
                        _objProducto,
                        Source.Count - 1,
                        false
                        );
                }
            }
            else
            {
                Logica_ManagerEncabezadoPedido _managerEncabezadoPedido = new Logica_ManagerEncabezadoPedido();
                bool _existePedido = _managerEncabezadoPedido.ExistePedidoCliente(view.controlador.v_objCliente, validar, Es_Factura);

                if (_existePedido)
                {
                    foreach (TransaccionEncabezado _objTransaccionEncabezado in view.controlador.v_objCliente.v_objListaPedidos)
                    {
                        LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(view);

                        view.controlador.COD_PEDIDO = _objTransaccionEncabezado.v_codDocumento;

                        await extraerDetalle(_objProducto, _logica, _objTransaccionEncabezado);
                    }
                }
                else
                {
                    view.controlador.v_PedidoBackOffice = false;
                }
            }
        }

        internal bool ExistePedidoBackOffice()
        {
            Logica_ManagerEncabezadoPedido _managerEncabezadoPedido = new Logica_ManagerEncabezadoPedido();
            bool _existePedido = _managerEncabezadoPedido.ExistePedidoBackOffice(view.controlador.v_objCliente);

            return _existePedido;
        } 

        internal async Task cargaPedidoRecuperarBackOffice(string CodPedido, bool validar, string Es_Factura)
        {
            Producto _objProducto = new Producto();
            Logica_ManagerEncabezadoPedido _managerEncabezadoPedido = new Logica_ManagerEncabezadoPedido();
            bool _existePedido = _managerEncabezadoPedido.ExistePedidoClienteBackOffice(CodPedido, view.controlador.v_objCliente, validar, Es_Factura);

            if (_existePedido)
            {
                foreach (TransaccionEncabezado _objTransaccionEncabezado in view.controlador.v_objCliente.v_objListaPedidos)
                {
                    LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(view);

                    await extraerDetalle(_objProducto, _logica, _objTransaccionEncabezado);

                    //guardamos el codigo de la transaccion
                    view.controlador.COD_DOCUMENTO = _objTransaccionEncabezado.v_codDocumento;
                    view.controlador.COD_PEDIDO = _objTransaccionEncabezado.v_codDocumento;

                    if (_objTransaccionEncabezado.v_listaDetalles.Count > 0)
                    {
                        view.controlador.v_PedidoBackOffice = _objTransaccionEncabezado.v_Pedido_Planta.Equals(SQL._Si) && view.controlador.TipoAgente.Equals(Agent._carniceroSigla) ? true : false;
                    }
                }
            }
        }

        public void guardarNuevoPedido(TransaccionEncabezado pobjTransaccion, bool CambiarNumPedidoDR = false, bool pantallaProducto = false, List<pnlTransacciones_ltvDetalleReses> ListaDetallesResesProductos = null)
        {
            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            try
            {
                MultiGeneric.BeginTransaction();

                Logica_ManagerAgenteVendedor _managerAgenteVendedor = new Logica_ManagerAgenteVendedor();
                _managerAgenteVendedor.BuscarConsecutivo_Pedido(view.controlador.v_objCliente);

                pobjTransaccion.v_codDocumento = view.controlador.v_objCliente.v_objTransaccion.v_codDocumento;

                view.controlador.v_objCliente.v_objTransaccion = pobjTransaccion;

                Logica_ManagerEncabezadoPedido _managerEncabezadoPedido = new Logica_ManagerEncabezadoPedido();
                _managerEncabezadoPedido.guardarEncabezadoPedido(view.controlador.v_objCliente);

                Logica_ManagerDetallePedido _managerDetallePedido = new Logica_ManagerDetallePedido();
                _managerDetallePedido.guardarDetallePedido(view.controlador.v_objCliente, view.controlador.TipoAgente);

                if (CambiarNumPedidoDR)
                {
                    //Es necesario actualzar el numero de pedido de los detalle reses
                    //que hayan sido asignados si proviene desde la pantalla de productos porque aun no estan asignados a este cliente
                    //con los parametros requeridos
                    //ActualizarDR(view.controlador.v_objCliente, pobjTransaccion.v_codDocumento);
                    if (pantallaProducto &&
                        ListaDetallesResesProductos != null &&
                        view.controlador.v_objCliente.v_objTransaccion.v_listaDetalles != null &&
                        ListaDetallesResesProductos.Count > 0 &&
                        view.controlador.v_objCliente.v_objTransaccion.v_listaDetalles.Any(x => x.v_objProducto?.v_codProducto ==
                        ListaDetallesResesProductos.FirstOrDefault()._vc_articulo))
                    {
                        view.controlador.v_objCliente.v_objTransaccion.v_listaDetalles
                            .Where(x => x.v_objProducto.v_codProducto == ListaDetallesResesProductos.FirstOrDefault()._vc_articulo)
                            .FirstOrDefault().v_listaDetalleReses = ListaDetallesResesProductos;
                    }

                    //Es necesario actualizar los detalles de reses que si hayan sido utilizados
                    //principalmente los que provienen desde productos
                    ActualizarDRAsignado(view.controlador.v_objCliente, pobjTransaccion.v_codDocumento);

                    //Actualizamos el codigo del pedido en visita para poder hacer referencia correcta a los detalles reses principalmente (actualmente no sirve para nada más en esta parte)
                    view.controlador.COD_PEDIDO = pobjTransaccion.v_codDocumento;
                }

                _managerAgenteVendedor.AumentarConsecutivo_Pedido();

                MultiGeneric.Commit();
            }
            catch (Exception ex)
            {
                MultiGeneric.Rollback();

                view.controlador.v_objCliente.v_objTransaccion.v_codDocumento = string.Empty;

                throw new Exception("guardarNuevoPedido(...)", ex);
            }

        }

        public void ActualizarDR(Cliente cliente, string Cod_Documento) {

            Logica_ManagerCarniceria logica = new Logica_ManagerCarniceria();

            logica.ActualizarNP(cliente, Cod_Documento);

        }

        public void ActualizarDRAsignado(Cliente cliente, string Cod_Documento)
        {
            Logica_ManagerCarniceria logica = new Logica_ManagerCarniceria();

            logica.ActualizarNPReAsignado(cliente, Cod_Documento);
        }

        internal void guardarPedido(bool renderList, bool Validar, bool CambiarNumPedidoDR = false, bool pantallaProducto = false, List<pnlTransacciones_ltvDetalleReses> ListaDetallesResesProductos = null)
        {
            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            try
            {
                var Source = view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;
                string Es_Factura = view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Contains("Factura") && Validar ? SQL._Si : SQL._No;
                List<TransaccionDetalle> DetallesSinModificar = new List<TransaccionDetalle>();

                if (Source.Count > 0)
                {
                    MultiGeneric.BeginTransaction();

                    UtilLogica _util = new UtilLogica();

                    DetallesSinModificar = _util.actualizarAplicadosPedidosCliente(view.controlador.v_objCliente, Validar, Es_Factura);

                    LogicaVisitaLevantarObjetos _logicaLevantarObjetos = new LogicaVisitaLevantarObjetos(view);

                    TransaccionEncabezado _objTransaccion = _logicaLevantarObjetos.levantarObjetoTransaccionEncabezado();

                    _logicaLevantarObjetos.levantarObjetoTransaccionDetalle(_objTransaccion);

                    _objTransaccion.v_objTipoDocumento.SetSigla(ROLTransactions._pedidoSigla);

                    int v_linea = _objTransaccion.v_listaDetalles.Count;

                    if (Validar)
                    {
                        foreach (var item in DetallesSinModificar)
                        {
                            v_linea++;
                            item.v_numLinea = v_linea;
                            _objTransaccion.v_listaDetalles.Add(item);
                        }
                    }

                    guardarNuevoPedido(_objTransaccion, CambiarNumPedidoDR, pantallaProducto, ListaDetallesResesProductos);

                    view.controlador.v_objCliente.v_objTransaccion.v_codDocumento = string.Empty;

                    view.controlador.v_objCliente.v_objListaPedidos.Clear();

                    if (renderList)
                    {
                        view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource = new ObservableCollection<pnlTransacciones_ltvProductos>();
                    }

                    MultiGeneric.Commit();
                }
            }
            catch (Exception ex)
            {
                MultiGeneric.Rollback();

                view.controlador.v_objCliente.v_objTransaccion.v_codDocumento = string.Empty;

                throw new Exception("guardarPedido(...)", ex);
            }

        }
    }
}
