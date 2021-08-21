using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita.Guardar;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.Vista;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita
{
    internal class LogicaVisitaMenu
    {
        private vistaVisita view = null;

        internal LogicaVisitaMenu(vistaVisita pview)
        {
            view = pview;
        }

        internal void menu_mniCambiarCliente_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            LogicaVisitaPedido _logica = new LogicaVisitaPedido(view);
            #region Modificaciones por Carniceros
            view.controlador.Previous_Doc = string.Empty;
            view.controlador.Previous_EsFactura = false;
            bool validar = view.controlador.TipoAgente.Equals(Agent._carniceroSigla) ? true : false;
            LogicaVisitaComboBox _logicaVisitaComboBox = new LogicaVisitaComboBox(view);
            LogicaVisitaPedido _logicaVisitaPedido = new LogicaVisitaPedido(view);
            string Es_Factura = _logicaVisitaComboBox.getTipoDocumento().Contains(ROLTransactions._facturaNombre) ? SQL._Si : SQL._No;
            bool CambiarNumPedidoDR = true;
            #endregion

            if (!_validateHH.emptyListView<pnlTransacciones_ltvProductos>(view.FindByName<ListView>("pnlTransacciones_ltvProductos")) && !view.controlador.v_DevolucionFactura)
            {
                bool EsPedidoBackOffice = view.controlador.TipoAgente.Equals(Agent._carniceroSigla) && Es_Factura.Equals(SQL._Si) && !view.controlador.v_PedidoManual ? _logicaVisitaPedido.ExistePedidoBackOffice() : false;
                //Si es carnicero, hay pedidos backOffice y es factura entonces no se debe poder guardar el pedido de respaldo
                if (!EsPedidoBackOffice)
                {
                    //CambiarNumPedidoDR = Es_Factura.Equals(SQL._Si) ? true : _logicaVisitaPedido.ExistePedidoBackOffice() ? false : true;
                    _logica.guardarPedido(true, validar, CambiarNumPedidoDR);
                }
            }

            vistaCliente _windowsForm = new vistaCliente(view,PantallasSistema._pantallaVisita);
            Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(_windowsForm));
        }

        internal void menu_mniFactura_Click()
        {
            Logica_ManagerTipoTransaccion _managerTipoTransaccion = new Logica_ManagerTipoTransaccion();

            string _codTipoTransaccion = _managerTipoTransaccion.obtenerCodigoTipoTransaccion(view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString());

            view.controlador.v_objCliente.v_objTransaccion.v_objTipoDocumento.SetSigla(_codTipoTransaccion);

            ShowSROL _show = new ShowSROL();

            _show.mostrarPantallaFactura(view.controlador.v_objCliente, PantallasSistema._pantallaVisita, null);
        }

        internal void menu_mniSugerido_Click()
        {
            ShowSROL _show = new ShowSROL();
            _show.mostrarPantallaVisitaSugerido(view);            
        }

        internal async Task menu_mniGuardar_Click()
        {
            try
            {
                LogicaVisitaGuardar _logicaVisitaGuardar = new LogicaVisitaGuardar(view);

                var _objTransaccionPrevio = _logicaVisitaGuardar.CargaInformacionParaGuardar();

                if (await ValidarPesosDR(_objTransaccionPrevio))
                {
                    view.ToolbarItems.Clear();

                    bool _modificarCantidad = false;

                    bool _guardar = await _logicaVisitaGuardar.EsTipoTransaccionCorrecta(_modificarCantidad);

                    if (_guardar)
                    {
                        var _objTransaccion = _logicaVisitaGuardar.CargaInformacionParaGuardar();

                        await _logicaVisitaGuardar.ProcesamientoPorTipoDocumento(_objTransaccion);

                    }
                    else
                    {
                        if (!_modificarCantidad)
                        {
                            LogMessageAttention _logMessageAttention = new LogMessageAttention();

                            await _logMessageAttention.generalAttention("Cambie el tipo de documento y vuelva a presionar el botón guardar");
                        }

                        RenderMenu();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task EvaluarGuardadoDocumento(TransaccionEncabezado _objTransaccion)
        {
            LogicaVisitaGuardar _logicaVisitaGuardar = new LogicaVisitaGuardar(view);
            string OrdenCompra = string.Empty;

            if (_objTransaccion.v_codDocumento.Equals(string.Empty))
            {
                await _logicaVisitaGuardar.GuardoDocumentoFallido();
            }
            else
            {
                await _logicaVisitaGuardar.GuardoDocumentoExito(_objTransaccion, OrdenCompra);
                //Sincronizar???
                await _logicaVisitaGuardar.RestauracionDeDatosYPantalla();
            }

            RenderMenu();
        }

        public void RenderMenu()
        {
            LogicaVisitaRender _logicaRender = new LogicaVisitaRender(view);

            _logicaRender.renderMenu(false);
        }

        internal async Task menu_mniCargaPedido_Click()
        {
            string tipodoc = view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString();
            LogicaVisitaConstructor _logica = new LogicaVisitaConstructor(view);


            if (view.controlador.TipoAgente.Equals(Agent._carniceroSigla))
            {
                LogicaVisitaRender _logicaVisitaRender = new LogicaVisitaRender(view);

                _logicaVisitaRender.RenderVisitaProductos();
                await _logica.Constructores(false, false, true);
            }
            else if (!tipodoc.Equals(ROLTransactions._ordenVentaNombre))
            {
                LogMessageAttention _lma = new LogMessageAttention();
                await _lma.generalAttention("No puede recargar pedidos en este tipo de documento, debe seleccionar Orden de Venta para llevar a cabo este proceso.");
            }
            else
            {
                await _logica.Constructores(false, false, true);
            }
        }

        internal async Task<bool> ValidarPesosDR(TransaccionEncabezado _objTransaccion)
        {
            bool result = true;
            decimal ResultDetalle = Numeric._zeroDecimalInitialize;
            decimal ResultDetallesReses = Numeric._zeroDecimalInitialize;
            Logica_ManagerCarniceria logica = new Logica_ManagerCarniceria();

            if (_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaCreditoSigla) ||
                    _objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaContadoSigla))
            {
                foreach (var _objDetalleDocumento in _objTransaccion?.v_listaDetalles ?? new System.Collections.Generic.List<TransaccionDetalle>())
                {
                    ResultDetalle = Numeric._zeroDecimalInitialize;
                    ResultDetallesReses = Numeric._zeroDecimalInitialize;

                    //Si es detalle de res se validará que coincidan los pesos seleccionados y los asignados
                    if (logica.EsDetalleRes(_objDetalleDocumento?.v_objProducto?.v_codProducto))
                    {
                        ResultDetalle = _objDetalleDocumento.v_objProducto.v_cantTransaccion;

                        foreach (var _objDetalleReses in _objDetalleDocumento?.v_listaDetalleReses ?? new System.Collections.Generic.List<ListviewModels.Carniceria.pnlTransacciones_ltvDetalleReses>())
                        {
                            ResultDetallesReses += _objDetalleReses._vc_peso;
                        }

                        if (ResultDetalle != ResultDetallesReses)
                        {
                            //Si los pesos no coinciden
                            result = false;

                            LogMessageAttention _logMessageAttention = new LogMessageAttention();

                            await _logMessageAttention.generalAttention("Existen inconsistencias entre los pesos asignados de las reses y la cantidad asignada en el pedido para el producto: " + _objDetalleDocumento?.v_objProducto?.v_codProducto + "; revise estos detalles para poder proceder con la operación actual.");
                            break;
                        }
                    }
                }
            }

            return result;
        }
    }
}
