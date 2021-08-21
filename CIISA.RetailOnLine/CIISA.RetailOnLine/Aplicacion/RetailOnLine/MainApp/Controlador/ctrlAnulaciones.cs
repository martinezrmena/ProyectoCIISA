using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Anulacion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    public class ctrlAnulaciones
    {
        private vistaAnulaciones view { get; set; }

        public TransaccionEncabezado v_objTransaccion = new TransaccionEncabezado();

        public string Cod_Transaccion { get; set; }

        public string Cod_Agente { get; set; }

        public Cliente v_objCliente = new Cliente();

        public string TipoAgente { get; set; }

        public pnlAnulacion_ltvTransacciones AnulacionSeleccionado { get; set; }

        private bool cancelarRecuperacion { get; set; }

        internal ctrlAnulaciones(vistaAnulaciones pview)
        {
            view = pview;
            TipoAgente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._typeAgent;
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlAnulacion").Id))
            {
                view.Title = "Anulaciones";
            }

            ppanel.IsVisible = true;
        }

        private void llenarComboBoxMotivo(Picker pcomboBox)
        {
            pcomboBox.Items.Clear();

            Logica_ManagerMotivo _manager = new Logica_ManagerMotivo();

            DataTable _dt = _manager.buscarMotivoPorCodigoTransaccion(ROLTransactions._anulacionSigla);

            Util _util = new Util();

            _util.fillComboBox(
                _dt,
                pcomboBox,
                "Descripcion"
                );
        }

        private void renderComponents()
        {
            llenarComboBoxMotivo(view.FindByName<Picker>("pnlAnulacion_cbxMotivo"));
        }

        private void buscarTransacciones()
        {
            view.FindByName<ListView>("pnlAnulacion_ltvTransacciones").ItemsSource = new ObservableCollection<pnlAnulacion_ltvTransacciones>();

            Logica_ManagerAnulacion _manager = new Logica_ManagerAnulacion();

            DateTime _fechaDocumento = _manager.buscarUltimaTransaccionRealizada();

            _manager.buscarListaTransaccionEncabezadosParaAnulaciones(
                view.FindByName<ListView>("pnlAnulacion_ltvTransacciones"),
                _fechaDocumento
                );
        }

        private void buscarDocumentosAnulados()
        {
            view.FindByName<ListView>("pnlAnulacion_ltvAnulaciones").ItemsSource = new ObservableCollection<pnlAnulacion_ltvAnulaciones>();

            Logica_ManagerEncabezadoAnulacion _manager = new Logica_ManagerEncabezadoAnulacion();

            _manager.buscarListaDeDocumentosAnulados(view.FindByName<ListView>("pnlAnulacion_ltvAnulaciones"));
        }

        private void refrescarPantalla()
        {
            buscarTransacciones();
            buscarDocumentosAnulados();

            Logica_ManagerInventario _manager = new Logica_ManagerInventario();

            _manager.recalcularProductoDisponibleEnInventario();
        }

        private void renderMenu() {

            //view.ToolbarItems.Clear();
            RenderMenuAnulacion(false);

            ValidateHH _validateHH = new ValidateHH();

            if (!_validateHH.emptyListView<pnlAnulacion_ltvTransacciones>(view.FindByName<ListView>("pnlAnulacion_ltvTransacciones"))) {

                //view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniBuscar"));
                //view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniAnular"));
                RenderMenuAnulacion(true);

            }

        }

        internal void RenderMenuAnulacion(bool render) {

            view.FindByName<Grid>("pnlAnulacion_grdOptions").IsVisible = render;

        }

        internal void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlAnulacion"));

            renderComponents();

            refrescarPantalla();

            renderMenu();
        }

        internal void menu_mniBuscar_Click()
        {
            refrescarPantalla();
        }

        private void actualizarAnuladoEncabezadoTransaccion()
        {
            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._reciboDineroSigla)
            || v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._recaudacionSigla))
            {
                Logica_ManagerEncabezadoRecibo _managerEncabezadoRecibo = new Logica_ManagerEncabezadoRecibo();

                _managerEncabezadoRecibo.actualizarAnulado(v_objTransaccion);
            }
            else
            {
                Logica_ManagerEncabezadoTransaccion _managerEncabezadoTransaccion = new Logica_ManagerEncabezadoTransaccion();

                _managerEncabezadoTransaccion.actualizarAnulado(v_objTransaccion);
            }
        }

        private void actualizarAnuladoDetalleTransaccion()
        {
            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._reciboDineroSigla)
            || v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._recaudacionSigla))
            {
                Logica_ManagerDetalleRecibo _managerDetalleRecibo = new Logica_ManagerDetalleRecibo();

                _managerDetalleRecibo.actualizarAnulado(v_objTransaccion);
            }
            else
            {
                Logica_ManagerDetalleTransaccion _managerDetalleTransaccion = new Logica_ManagerDetalleTransaccion();

                _managerDetalleTransaccion.actualizarAnulado(v_objTransaccion);
            }
        }

        private void anularRevertirInventario()
        {
            Logica_ManagerInventario _manager = new Logica_ManagerInventario();

            _manager.actualizarVentasDocumentosAnuladosConVentas(v_objTransaccion);
        }

        private void anularRevertirInventarioRegalia()
        {
            Logica_ManagerInventario _manager = new Logica_ManagerInventario();

            _manager.actualizarRegaliasDocumentosAnuladosConRegalias(v_objTransaccion);
        }

        private void actualizarInventario()
        {

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._ordenVentaSigla))
            {

            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaCreditoSigla))
            {
                Logica_ManagerInventario _manager = new Logica_ManagerInventario();

                _manager.actualizarInventarioAnulacion(v_objTransaccion);

                anularRevertirInventario();
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaContadoSigla))
            {
                Logica_ManagerInventario _manager = new Logica_ManagerInventario();

                _manager.actualizarInventarioAnulacion(v_objTransaccion);

                anularRevertirInventario();
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._cotizacionSigla))
            {

            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._devolucionSigla))
            {
                Logica_ManagerInventario _manager = new Logica_ManagerInventario();

                _manager.actualizarInventarioAnulacion(v_objTransaccion);

                //anularRevertirInventario();
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._regaliaSigla))
            {
                Logica_ManagerInventario _manager = new Logica_ManagerInventario();

                _manager.actualizarInventarioAnulacion(v_objTransaccion);

                anularRevertirInventarioRegalia();
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._recaudacionSigla))
            {
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._reciboDineroSigla))
            {
            }
        }

        private void anularFactura()
        {
            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._ordenVentaSigla))
            {
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaCreditoSigla))
            {
                Logica_ManagerFactura _manager = new Logica_ManagerFactura();

                _manager.anularFactura(v_objTransaccion);

                actualizarInventario();
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaContadoSigla))
            {
                actualizarInventario();
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._cotizacionSigla))
            {
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._devolucionSigla))
            {
                actualizarInventario();
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._regaliaSigla))
            {
                actualizarInventario();
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._recaudacionSigla))
            {
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._reciboDineroSigla))
            {
            }
        }

        private void actualizarAnuladoFormaPagoTransaccion()
        {
            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._ordenVentaSigla))
            {
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaCreditoSigla))
            {
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaContadoSigla))
            {
                Logica_ManagerPago _managerPago = new Logica_ManagerPago();
                _managerPago.actualizarAnulado(v_objTransaccion);
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._cotizacionSigla))
            {
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._devolucionSigla))
            {
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._regaliaSigla))
            {
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._recaudacionSigla))
            {
                Logica_ManagerPagoRecibo _manager = new Logica_ManagerPagoRecibo();
                _manager.actualizarAnulado(v_objTransaccion);
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._reciboDineroSigla))
            {
                Logica_ManagerPagoRecibo _manager = new Logica_ManagerPagoRecibo();
                _manager.actualizarAnulado(v_objTransaccion);
            }
        }

        private void anularPago()
        {
            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._ordenVentaSigla))
            {
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaCreditoSigla))
            {
                anularFactura();
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaContadoSigla))
            {
                actualizarAnuladoFormaPagoTransaccion();

                anularFactura();
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._cotizacionSigla))
            {
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._devolucionSigla))
            {
                anularFactura();
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._regaliaSigla))
            {
                anularFactura();
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._recaudacionSigla))
            {
                actualizarAnuladoFormaPagoTransaccion();
            }

            if (v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._reciboDineroSigla))
            {
                actualizarAnuladoFormaPagoTransaccion();
            }
        }

        private void anularDetalle()
        {
            actualizarAnuladoDetalleTransaccion();

            Logica_ManagerDetalleAnulacion _manager = new Logica_ManagerDetalleAnulacion();

            _manager.guardarDetalleAnulacion(v_objTransaccion);

            anularPago();
        }

        private async Task anularEncabezado(bool pactualizarInventario)
        {
            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            try
            {
                MultiGeneric.BeginTransaction();

                actualizarAnuladoEncabezadoTransaccion();

                Logica_ManagerEncabezadoAnulacion _manager = new Logica_ManagerEncabezadoAnulacion();

                _manager.guardarEncabezadoAnulacion(v_objTransaccion);

                anularDetalle();

                if (pactualizarInventario)
                {
                    Logica_ManagerInventario _managerInventario = new Logica_ManagerInventario();

                    _managerInventario.recalcularProductoDisponibleEnInventario();
                }

                EliminarIndicadorAnulacion(Cod_Agente, Cod_Transaccion);

                await anularDetalleReses();

                MultiGeneric.Commit();
            }
            catch (Exception ex)
            {
                MultiGeneric.Rollback();

                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        internal async Task pnlAnulacion_ltvTransacciones_ItemActivate()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (!_validateHH.emptyListView<pnlAnulacion_ltvTransacciones>(view.FindByName<ListView>("pnlAnulacion_ltvTransacciones")))
            {
                if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlAnulacion_ltvTransacciones")))
                {
                    var Seleccionado = view.FindByName<ListView>("pnlAnulacion_ltvTransacciones").SelectedItem as pnlAnulacion_ltvTransacciones;
                    string _codTransaccion = Seleccionado.CODDOCUMENTO;
                    Logica_ManagerAgenteVendedor logica_ManagerAgenteVendedor = new Logica_ManagerAgenteVendedor();

                    string _codTipoTransaccion = Seleccionado.CODTIPODOCUMENTO;
                    string _TipoTransaccion = Seleccionado.DESCRIPCION;
                    v_objCliente.v_no_cliente = Seleccionado.CODCLIENTE;
                    AnulacionSeleccionado = Seleccionado;
                    Cod_Transaccion = _codTransaccion;
                    Cod_Agente = logica_ManagerAgenteVendedor.obtenerCodigoAgente();
                    v_objTransaccion = null;

                    if (ValidarAnulacion(Cod_Agente, Cod_Transaccion))
                    {
                        //Esta validación viene indicada vía web service al recargar la tabla indicadoranulación
                        #region Validar Anulación

                        Logica_ManagerTransaccion _manager = new Logica_ManagerTransaccion();

                        if (_codTipoTransaccion.Equals(ROLTransactions._reciboDineroSigla)
                        || _codTipoTransaccion.Equals(ROLTransactions._recaudacionSigla))
                        {
                            v_objTransaccion = _manager.buscarReciboParaAnulaciones(_codTransaccion, _codTipoTransaccion);
                        }
                        else
                        {
                            v_objTransaccion = _manager.buscarTransaccionParaAnulaciones(_codTransaccion, _codTipoTransaccion);
                        }

                        v_objTransaccion.v_motivoAnulacion = view.FindByName<Picker>("pnlAnulacion_cbxMotivo").SelectedItem.ToString();

                        if (await LogMessages._dialogResultYes("¿Desea anular la transacción "
                            + v_objTransaccion.v_codDocumento
                            + "?", "Anular"))
                        {

                            if (_codTipoTransaccion.Equals(ROLTransactions._reciboDineroSigla)
                                || _codTipoTransaccion.Equals(ROLTransactions._recaudacionSigla))
                            {
                                v_objTransaccion.v_recuperarDocumento = false;

                                await anularEncabezado(false);
                                refrescarPantalla();
                            }
                            else
                            {
                                if (await LogMessages._dialogResultYes("¿Desea recuperar el documento?", "Anular"))
                                {
                                    v_objTransaccion.v_recuperarDocumento = true;

                                    foreach (TransaccionDetalle _objDetalle in v_objTransaccion.v_listaDetalles)
                                    {
                                        _objDetalle.v_objProducto.v_precioConsultado = false;
                                        _objDetalle.v_objProducto.v_porcentajeDescuentoConsultado = false;
                                        _objDetalle.v_objProducto.v_descripcionConsultado = false;
                                        _objDetalle.v_objProducto.v_exentoConsultado = false;
                                        _objDetalle.v_objProducto.v_unidadConsultado = false;
                                    }

                                    await anularEncabezado(true);

                                    await Application.Current.MainPage.Navigation.PopAsync();
                                }
                                else
                                {

                                    v_objTransaccion.v_recuperarDocumento = false;

                                    await anularEncabezado(false);

                                    cancelarRecuperacion = true;

                                    refrescarPantalla();

                                    await Application.Current.MainPage.Navigation.PopAsync();

                                }
                            }

                            Logica_ManagerCliente _managerCliente = new Logica_ManagerCliente();

                            Cliente _objCliente = new Cliente();

                            _objCliente.v_no_cliente = v_objTransaccion.v_codCliente;

                            await _managerCliente.buscarClientePorCodigoCliente(_objCliente);

                            renderMenu();
                        }
                        #endregion
                    }
                    else
                    {
                        LogMessageAttention _logMessageAttention = new LogMessageAttention();
                        await _logMessageAttention.generalAttention("No esta autorizado a anular este documento, comuniquese con BackOffice.");
                    }
                }
            }
        }

        /// <summary>
        /// Metodo encargado de validar si es posible realizar la anulacion dependiendo de los
        /// datos provenientes de indicador anulacion
        /// </summary>
        /// <param name="cod_agente"></param>
        /// <param name="cod_transaccion"></param>
        /// <returns>bool que indica si es posible anular el documento</returns>
        internal bool ValidarAnulacion(string cod_agente, string cod_transaccion) {

            Logica_ManagerIndicadorAnulacion logicaInd = new Logica_ManagerIndicadorAnulacion();

            return logicaInd.ValidarAnulacion(cod_agente, cod_transaccion);
        }

        /// <summary>
        /// Luego de utilizar el indicador anulacion es necesario eliminarlo de la base de datos
        /// </summary>
        /// <param name="cod_agente"></param>
        /// <param name="cod_transaccion"></param>
        internal void EliminarIndicadorAnulacion(string cod_agente, string cod_transaccion) {

            Logica_ManagerIndicadorAnulacion logica_ManagerIndicadorAnulacion = new Logica_ManagerIndicadorAnulacion();

            logica_ManagerIndicadorAnulacion.eliminarIndicadorAnulacion(cod_agente, cod_transaccion);
        }

        internal async Task menu_mniAnular_Click()
        {
            await pnlAnulacion_ltvTransacciones_ItemActivate();
        }

        /// <summary>
        /// En caso de que un pedido posea detalle reses
        /// </summary>
        internal async Task anularDetalleReses() {

            Logica_ManagerCarniceria logica = new Logica_ManagerCarniceria();

            //Validamos que el pedido posea detalle reses
            if (logica.buscarDetalleResPedido(Cod_Transaccion, true)) {

                //Si hay detalles reses con ese codigo de pedido
                logica.AnulacionDetalleResesVendido(
                    Cod_Transaccion,
                    v_objCliente.v_no_cliente
                    );

                v_objCliente.v_objEstablecimiento.v_codEstablecimiento = Int32.Parse(AnulacionSeleccionado.CODESTABLECIMIENTO);

                await CrearEncabezadoPedido();

                //Actualizar numpedido al nuevo
                logica.ActualizarNumPedido(
                    Cod_Transaccion, 
                    v_objCliente.v_objTransaccion.v_codDocumento);

                v_objCliente.v_objTransaccion.Cod_RecuperarPedido = v_objCliente.v_objTransaccion.v_codDocumento;
            }

        }

        /// <summary>
        /// Si existen detalle reses en el documento anulado entonces es necesario
        /// asignar estos detalles a un pedido nuevo, independientemente de si se recupero
        /// o no el documento en el proceso de anulación
        /// </summary>
        internal async Task CrearEncabezadoPedido() {

            //Buscamos al cliente
            Logica_ManagerCliente _managerCliente = new Logica_ManagerCliente();
            await _managerCliente.buscarClientePorCodigoCliente(v_objCliente);

            //Armamos el encabezado pedido y procedemos a guardarlo
            LogicaAnulacionLevantarObjetos logica = new LogicaAnulacionLevantarObjetos(view);
            logica.guardarNuevoPedido(logica.levantarObjetoTransaccionEncabezado());

        }

        internal async Task Cerrando()
        {
            ShowSROL _showVisitaAnulacion = new ShowSROL();

            if (v_objTransaccion != null)
            {
                if (v_objTransaccion.v_recuperarDocumento)
                {
                    Cliente _objClienteAnulaciones = new Cliente();

                    _objClienteAnulaciones.v_no_cliente = v_objTransaccion.v_codCliente;

                    _objClienteAnulaciones.v_objEstablecimiento.v_codEstablecimiento = v_objTransaccion.v_objEstablecimiento.v_codEstablecimiento;

                    Logica_ManagerCliente _manager = new Logica_ManagerCliente();

                    await _manager.buscarClientePorCodigoCliente(_objClienteAnulaciones);

                    _objClienteAnulaciones.v_objTransaccion = v_objTransaccion;

                    _objClienteAnulaciones.v_objTransaccion.Cod_RecuperarPedido = v_objCliente.v_objTransaccion.Cod_RecuperarPedido;

                    _showVisitaAnulacion.v_objCliente = _objClienteAnulaciones;

                    _showVisitaAnulacion.mostrarPantallaVisitaDesdeAnulacion();
                }
            }
            
            if (cancelarRecuperacion)
            {
                Cliente _objClienteAnulaciones = new Cliente();

                _objClienteAnulaciones.v_no_cliente = v_objTransaccion.v_codCliente;

                _objClienteAnulaciones.v_objEstablecimiento.v_codEstablecimiento = v_objTransaccion.v_objEstablecimiento.v_codEstablecimiento;

                Logica_ManagerCliente _manager = new Logica_ManagerCliente();

                await _manager.buscarClientePorCodigoCliente(_objClienteAnulaciones);

                _showVisitaAnulacion.v_objCliente = _objClienteAnulaciones;

                _showVisitaAnulacion.mostrarPantallaVisitaDesdeAnulacion();
            }
        }

    }
}
