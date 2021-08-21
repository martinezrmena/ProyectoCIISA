using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers.DevolucionFactura;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista.DevolucionFactura;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.DevolucionFactura;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita.ComboTipoTransaccion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Display.ViewController;
using CIISA.RetailOnLine.Framework.Handheld.GPS.ViewController;
using CIISA.RetailOnLine.Framework.Handheld.Print;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    public class ctrlVisita
    {
        #region Properties
        public BrowserView browserView = new BrowserView();
        public vistaVisita view { get; set; }

        internal string v_motivoRecaudacion = string.Empty;

        internal bool v_finalizoConstructor2 = false;

        public Cliente v_objCliente = new Cliente();

        public LogMessageAttention _lma = new LogMessageAttention();

        public int TappedN = 0;

        internal bool v_cambiarCliente = true;

        internal bool v_FacturacionFaltante = false;

        public string COD_DOCUMENTO { get; set; }

        public string TipoAgente = null;

        public bool Previous_EsFactura { get; set; }

        public bool Current_EsFactura { get; set; }

        public string Previous_Doc { get; set; }

        //Devolucion de factura
        internal bool v_DevolucionFactura { get; set; } = false;

        public string COD_FACTURA { get; set; }

        public bool valid { get; set; }

        public string COD_PEDIDO { get; set; }

        //Pedido desde BackOffice
        public bool v_PedidoBackOffice { get; set; }

        public bool v_PedidoManual { get; set; } = false;

        #endregion

        internal ctrlVisita(vistaVisita pview)
        {
            view = pview;
            TipoAgente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._typeAgent;
            Previous_EsFactura = false;
            Previous_Doc = string.Empty;
        }

        #region Inicialización
        internal void ScreenInicialization()
        {
            LogicaVisitaConstructor _logica = new LogicaVisitaConstructor(view);
            _logica.constructor();

            ShowSROL _show = new ShowSROL();
            _show.mostrarPantallaCliente(view);
        }

        //Desde Anulaciones
        internal async void ScreenInicialization(Cliente pobjCliente, bool pcambiarCliente)
        {
            string TD = pobjCliente.v_objTransaccion.v_objTipoDocumento.GetNombre();

            LogicaVisitaConstructor _logica = new LogicaVisitaConstructor(view);

            LogicaVisitaComboTipoTransaccion logica = new LogicaVisitaComboTipoTransaccion(view);

            v_cambiarCliente = pcambiarCliente;

            await view.pintarAtributosCliente(pobjCliente);

            await _logica.Constructores(true, true, false);

            if (!string.IsNullOrEmpty(TD))
            {
                view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem = TD;

                //Guardamos el indicador actual correspondiente al tipo de documento
                if (TD.Contains(ROLTransactions._Factura))
                {
                    Previous_EsFactura = true;
                    logica.RenderizarBotonesVisita(false);
                }
            }

            await _logica.Constructores(false, false, true);
        }

        internal async void ScreenInicialization(Cliente pobjCliente, bool pcambiarCliente, bool FacturacionFaltantes)
        {
            v_cambiarCliente = pcambiarCliente;
            v_FacturacionFaltante = FacturacionFaltantes;

            LogicaVisitaConstructor _logica = new LogicaVisitaConstructor(view);

            await view.pintarAtributosCliente(pobjCliente);

            await _logica.Constructores(true, true, true);
        }

        /// <summary>
        /// Metodo encargado corregir los detalle reses para poder asignar su indicador
        /// a una viscera.
        /// </summary>
        public void ScreenInicializationVisceras()
        {
            FacturacionVisceras facturacionVisceras = new FacturacionVisceras(view, v_objCliente);

            do
            {
                if (facturacionVisceras.CodProductosVisita.Count > 0)
                {
                    facturacionVisceras.CorregirIndicadores(facturacionVisceras.CodProductosVisita.First());
                }
            } while (facturacionVisceras.CodProductosVisita.Count > 0);

        }

        public void pintarAtributosCliente(Cliente pobjCliente)
        {
            v_objCliente = pobjCliente;

            if (TipoAgente.Equals(Agent._ruteroSigla))
            {
                view.FindByName<Label>("pnlTransacciones_lblNombreCliente").Text =
                    v_objCliente.v_no_cliente
                    + Simbol._hyphenWithSpaces
                    + v_objCliente.v_nombre;
            }

            if (TipoAgente.Equals(Agent._carniceroSigla) || TipoAgente.Equals(Agent._supermercadoSigla) || TipoAgente.Equals(Agent._cobradorSigla))
            {
                view.FindByName<Label>("pnlTransacciones_lblNombreCliente").Text =
                    v_objCliente.v_no_cliente
                    + Simbol._point
                    + v_objCliente.v_objEstablecimiento.v_codEstablecimiento
                    + Simbol._hyphenWithSpaces
                    + v_objCliente.v_objEstablecimiento.v_descEstablecimiento;
            }
        }

        private void renderComponents(bool pcarga)
        {
            LogicaVisitaRender _logica = new LogicaVisitaRender(view);

            _logica.renderComponentesPnlTransacciones(true);
        }

        private void renderComponentesPnlTransacciones(bool pcarga)
        {
            LogicaVisitaRender _logica = new LogicaVisitaRender(view);

            _logica.renderComponentesPnlTransacciones(pcarga);
        }
        #endregion

        #region Menu conceptual y opciones secundarias
        internal void menu_mniCambiarCliente_Click()
        {
            LogicaVisitaMenu _logica = new LogicaVisitaMenu(view);

            _logica.menu_mniCambiarCliente_Click();
        }

        internal void menu_mniFactura_Click()
        {
            LogicaVisitaMenu _logica = new LogicaVisitaMenu(view);

            _logica.menu_mniFactura_Click();
        }

        internal async Task menu_mniPruebaImpresion_Click()
        {
            Logica_ManagerImpresora _manager = new Logica_ManagerImpresora();

            string _puertoImpresora = _manager.obtenerPuertoImpresora();

            PrintTest _printTest = new PrintTest();

            await _printTest.testPrint(_puertoImpresora);
        }

        internal async Task menu_mniCoordenadas_Click()
        {
            await GPS_Info.v_gpsDevice.ShowCoordinates();
        }

        internal void menu_mniSugerido_Click()
        {
            LogicaVisitaMenu _logica = new LogicaVisitaMenu(view);

            _logica.menu_mniSugerido_Click();
        }

        internal async Task pnlTransacciones_btnEliminar_Click()
        {
            LogicaVisitaBotones _logicaBotones = new LogicaVisitaBotones(view);

            await _logicaBotones.pnlTransacciones_btnEliminar_Click();

            view.FindByName<ListView>("pnlTransacciones_ltvProductos").SelectedItem = null;
        }

        internal async Task pnlTransacciones_btnEliminarTodos_Click()
        {
            LogicaVisitaBotones _logicaBotones = new LogicaVisitaBotones(view);

            await _logicaBotones.pnlTransacciones_btnEliminarTodos_Click();

            view.FindByName<ListView>("pnlTransacciones_ltvProductos").SelectedItem = null;
        }

        internal async Task pnlTransacciones_btnCalcularMontoLinea_Click()
        {
            LogicaVisitaBotones _logicaBotones = new LogicaVisitaBotones(view);

            await _logicaBotones.pnlTransacciones_btnCalcularMontoLinea_Click();
        }

        internal async Task pnlTransacciones_btnEspecificacion_Click()
        {
            LogicaVisitaBotones _logicaBotones = new LogicaVisitaBotones(view);

            await _logicaBotones.pnlTransacciones_btnEspecificacion_Click();
        }

        internal async Task pnlTransacciones_btnMotivo_Click()
        {
            LogicaVisitaBotones _logicaBotones = new LogicaVisitaBotones(view);

            await _logicaBotones.pnlTransacciones_btnMotivo_Click();
        }
        #endregion

        #region Cambiar Documento
        /// <summary>
        /// Metodo encargado de validar si es necesario renderizar la lista principal
        /// dependiendo de los indicadores
        /// </summary>
        /// <returns></returns>
        public async Task pnlTransacciones_cbxTipoTransaccion_SelectedIndexChanged()
        {
            LogicaVisitaComboTipoTransaccion _logica = new LogicaVisitaComboTipoTransaccion(view);
            LogicaVisitaComboBox _logicaVisitaComboBox = new LogicaVisitaComboBox(view);

            valid = false;

            if (!_logicaVisitaComboBox.getTipoDocumento().Contains(ROLTransactions._facturaNombre))
            {
                v_PedidoBackOffice = false;
                v_PedidoManual = false;
            }

            if (TipoAgente.Equals(Agent._carniceroSigla))
            {
                //Si hay un cambio entre tipos de documentos (factura o no) se recargan los pedidos
                //Ademas verificar entre recaudacion, tramite, recibo de dinero
                if (EqualDocuments())
                {
                    //Es necesario vaciar el listview y recargar de acuerdo al tipo documento
                    await _logica.ReemplazarElementosListViewPorTipoDocumento();
                    await _logica.pnlTransacciones_cbxTipoTransaccion_SelectedIndexChanged(false);
                }
                else
                {
                    valid = true;
                    await _logica.pnlTransacciones_cbxTipoTransaccion_SelectedIndexChanged(true);
                }
            }
            else
            {
                valid = true;
                await _logica.pnlTransacciones_cbxTipoTransaccion_SelectedIndexChanged(true);
            }

        }

        /// <summary>
        /// Metodo encargado de ayudar a decidir si se pedira una regarga del pedido
        /// </summary>
        /// <returns>bool que especifica si será necesario renderizar los elementos de la lista</returns>
        internal bool EqualDocuments()
        {

            bool iguales = false;

            string TD = view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString();

            if (TD.Contains(ROLTransactions._Factura))
                Current_EsFactura = true;
            else
                Current_EsFactura = false;

            if ((Current_EsFactura != Previous_EsFactura))
            {
                if (!TD.Equals(ROLTransactions._reciboDineroNombre))
                    if (!TD.Equals(ROLTransactions._tramiteNombre))
                        if (!TD.Equals(ROLTransactions._recaudacionNombre))
                            iguales = true;
            }
            else
            {
                if (Previous_Doc.Equals(ROLTransactions._reciboDineroNombre))
                    iguales = true;
                else if (Previous_Doc.Equals(ROLTransactions._tramiteNombre))
                    iguales = true;
                else if (Previous_Doc.Equals(ROLTransactions._recaudacionNombre))
                    iguales = true;
            }

            if (TD.Contains(ROLTransactions._Factura))
                Previous_EsFactura = true;
            else
                Previous_EsFactura = false;

            Previous_Doc = TD;

            return iguales;
        }
        #endregion

        internal async Task ctxMenu_cliente_clicked()
        {
            string action = await view.DisplayActionSheet(
                null, 
                null, 
                null, 
                "Información General", 
                "Información Financiera", 
                "Autorizado Firmar", 
                "Ubicación", 
                "Contacto", 
                "Indicadores");

            LogicaVisitaContextMenu _logica = new LogicaVisitaContextMenu(view);
            if (action != null)
            {
                if (action.Equals("Información General"))
                {
                    await _logica.infoGeneral();
                }
                if (action.Equals("Información Financiera"))
                {
                    await _logica.infoFinanciera();
                }
                if (action.Equals("Autorizado Firmar"))
                {
                    await _logica.autorizadoFirmar();
                }
                if (action.Equals("Ubicación"))
                {
                    await _logica.ubicacion();
                }
                if (action.Equals("Contacto"))
                {
                    await _logica.contacto();
                }
                if (action.Equals("Indicadores"))
                {
                    await _logica.indicadores();
                }
            }
        }

        internal async Task menu_mniCargaPedido_Click()
        {
            v_PedidoManual = false;
            LogicaVisitaMenu _logica = new LogicaVisitaMenu(view);

            await _logica.menu_mniCargaPedido_Click();
        }

        internal void menu_mniBloquear_Click()
        {
            ShowDisplay _show = new ShowDisplay();

            _show.showLockScreenForm(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));
        }

        #region Guardar
        internal async Task menu_GuardarInicialization()
        {

            LogicaVisitaComboTipoTransaccion _logica = new LogicaVisitaComboTipoTransaccion(view);

            if (_logica.pnlTransacciones_Validar_TipoDocumento_Factura_Devolucion())
            {
                if (_logica.validarIndicadoresFacturaElectronica(this))
                {
                    await PopupNavigation.Instance.PushAsync(new vistaFacturaElectronica(this, _logica.GetTipoDocumento()));
                }
                else
                {
                    await menu_GuardarInicializationParte2(new FacturaElectronica());
                }
            }
            else
            {
                await menu_mniGuardar_Click();
            }

        }

        internal async Task menu_GuardarInicializationParte2(FacturaElectronica facturaElectronica)
        {

            v_objCliente.v_objTransaccion.v_objFacturaElectronica = facturaElectronica;

            LogicaVisitaMenu _logica = new LogicaVisitaMenu(view);

            await _logica.menu_mniGuardar_Click();
        }

        internal async Task menu_mniGuardar_Click()
        {
            LogicaVisitaMenu _logica = new LogicaVisitaMenu(view);

            await _logica.menu_mniGuardar_Click();
        }
        #endregion

        internal void pnlTransacciones_ltvProductos_ItemActivate()
        {
            TappedN = 0;
        }

        internal async Task pnlTransacciones_ltvProductos_Tapped()
        {
            if (TappedN != 1)
            {
                TappedN++;
            }
            else {

                if (!v_PedidoBackOffice)
                {
                    //Si es detalle res redireccionar a su pantalla
                    LogicaCarniceriaActualizar logica = new LogicaCarniceriaActualizar(view);
                    Logica_ManagerCarniceria plogica = new Logica_ManagerCarniceria();
                    pnlTransacciones_ltvProductos _lvi = view.FindByName<ListView>("pnlTransacciones_ltvProductos").SelectedItem as pnlTransacciones_ltvProductos;

                    if (plogica.EsDetalleRes(_lvi._pt_codigo) && logica.ValidarActualizar())
                    {
                        await pnlTrasacciones_DetalleReses();
                    }
                    else
                    {
                        LogicaVisitaEventos _logica = new LogicaVisitaEventos(view);
                        await _logica.pnlTransacciones_ltvProductos_ItemActivate();
                    }
                }

                TappedN = 0;

            }

        }

        internal async Task pnlTransacciones_ltvProductos_ColumnClick()
        {
            LogicaVisitaEventos _logica = new LogicaVisitaEventos(view);

            await _logica.pnlTransacciones_ltvProductos_ColumnClick();

            InterruptSelection();
        }

        protected void InterruptSelection() {

            view.FindByName<ListView>("pnlTransacciones_ltvProductos").SelectedItem = null;

            TappedN = 0;

        }

        #region Cerrar
        internal void vistaVisita_Closing(bool renderList, bool CambiarAsignadoDR = false, bool pantallaProducto = false, List<pnlTransacciones_ltvDetalleReses> ListaDetallesResesProductos = null)
        {
            LogicaVisitaEventos _logica = new LogicaVisitaEventos(view);

            _logica.vistaVisita_Closing(renderList, CambiarAsignadoDR, pantallaProducto, ListaDetallesResesProductos);
        }

        internal void Cerrar()
        {
            view.Navigation.PopAsync();
        }
        #endregion

        #region Detalle Reses

        internal async Task pnlTrasacciones_DetalleReses() {

            ValidateHH _validateHH = new ValidateHH();

            if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlTransacciones_ltvProductos")))
            {
                pnlTransacciones_ltvProductos _lvi = view.FindByName<ListView>("pnlTransacciones_ltvProductos").SelectedItem as pnlTransacciones_ltvProductos;

                LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(view);

                Producto _objProducto = _logica.levantarProductoSeleccionado(_lvi, false);

                Logica_ManagerCarniceria logica = new Logica_ManagerCarniceria();

                //ES NECESARIO VALIDAR SI EL PRODUCTO POSEE DETALLE RESES
                if (logica.buscarDetalleResExiste(_objProducto.v_codProducto, this.COD_PEDIDO))
                {
                    var Source = view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

                    int v_fila = Source.IndexOf(_lvi) + 1;

                    LogicaCarniceriaEventos plogica = new LogicaCarniceriaEventos(view);

                    await plogica.pnlTransacciones_ltvProductos_ItemActivate(
                        _lvi,
                        _logica,
                        _objProducto,
                        v_fila,
                        COD_DOCUMENTO);
                }
                else
                {
                    await _lma.generalAttention("El producto no posee inventario comprometido.");
                }
            }
        }

        #endregion

        #region Google Maps
        public async Task GoogleMapsView()
        {
            double longitud = v_objCliente.v_longitud;
            double latitud = v_objCliente.v_latitud;

            if (longitud != 0 && latitud != 0)
            {
                var request = string.Format("http://maps.google.com/?daddr="+latitud+","+longitud+"");
                await browserView.OpenBrowser(new Uri(request));
            }
            else
            {
                await _lma.generalAttention("Estimado usuario, las coordenadas del usuario no poseen el formato correcto.");
            }
        }
        #endregion

        #region Waze
        public async Task WazeView()
        {
            double longitud = v_objCliente.v_longitud;
            double latitud = v_objCliente.v_latitud;

            if (longitud != 0 && latitud != 0)
            {
                var request = string.Format("https://waze.com/ul?q=your address&ll="+latitud+", "+longitud+"&navigate=yes");
                await browserView.OpenBrowser(new Uri(request));
            }
            else
            {
                await _lma.generalAttention("Estimado usuario, las coordenadas del usuario no poseen el formato correcto.");
            }
        }
        #endregion

        #region Devolucion de factura
        /// <summary>
        /// Permite controlar el tipo de devoluciones que vayan a realizarse según el usuario lo desee
        /// </summary>
        /// <returns></returns>
        internal async Task CambiarTipoDevolucion()
        {
            await PopupNavigation.Instance.PushAsync(new vistaTipoDevolucion(view, valid));
        }

        /// <summary>
        /// Parte dos del proceso de seleccionar el tipo de devolución
        /// su ejecución tiene lugar luego de que el usuario selecciona el tipo de devolución que requiere
        /// </summary>
        /// <returns></returns>
        internal async Task seleccionarTipoDevolucionParte2()
        {
            Helper_DevolucionFactura helper_DevolucionFactura = new Helper_DevolucionFactura(view);

            await helper_DevolucionFactura.ProcesarTipoDevolucion();
        }

        /// <summary>
        /// Metodo que permite obtener los detalle documento y colocarlos en la lista
        /// de productos en base al código de una factura con la finalidad de mostrarle al usuario
        /// que productos pertenecian a esa factura antes de crear un nuevo documento para devolución
        /// y anular lo referente a esa factura: recibo, detalle documento y encabezado documento.
        /// </summary>
        public void DevolucionFacturaInitialize()
        {
            RenderPaint.paintWhiteBackgroundListView(view.FindByName<ListView>("pnlTransacciones_ltvProductos"));

            LogicaDevolucionFactura logicaDevolucionFactura = new LogicaDevolucionFactura(view);

            logicaDevolucionFactura.cargarProductosFacturados();

            v_objCliente.v_objTransaccion.v_codFactura = COD_FACTURA;

            view.FindByName<StackLayout>("pnlTransacciones_stkGuardar").IsVisible = true;

        }

        /// <summary>
        /// Método del menu conceptual que permite seleccionar una factura diferente para anular
        /// </summary>
        public void DevolucionFacturaMenu()
        {
            LogicaVisitaComboTT_DF _logicaDevolucionFactura = new LogicaVisitaComboTT_DF(view);

            _logicaDevolucionFactura.devolucionFactura();
        }
        #endregion

        #region Validar GPS
        internal async Task<bool> ValidateGPS()
        {
            bool result = true;

            result = await GPS_Info.v_gpsDevice.GetPosition() == null ? false : true;

            if (!result)
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention("No se encuentra disponible la posición proporcionada por el GPS, por favor verifique el estado de la máquina antes de continuar.");
            }

            return result;
        }
        #endregion

        #region Pedidos BackOffice
        public async Task GenerarPedidosBackOffice(string CodPedido)
        {
            LogicaVisitaConstructor _logica = new LogicaVisitaConstructor(view);

            await _logica.ConstructorPedidosBackOffice(CodPedido);

            v_PedidoManual = false;
        }
        #endregion

        #region Factura Manual
        /// <summary>
        /// Control que permite manipular la factura manual
        /// </summary>
        /// <returns></returns>
        public async Task FacturaManual()
        {
            if (await LogMessages._dialogResultYes("El siguiente proceso reiniciará la lista de productos, ¿está seguro que desea continuar?","Atención"))
            {
                LogicaVisitaRender _logicaVisitaRender = new LogicaVisitaRender(view);
                LogicaVisitaConstructor _logica = new LogicaVisitaConstructor(view);
                LogicaVisitaPedido _logicaVisitaPedido = new LogicaVisitaPedido(view);

                //Se limpia el menu
                _logicaVisitaRender.RenderVisitaProductos();
                LimpiarFacturaBackOffice();
                await _logica.ConstructorPedidosManual(_logicaVisitaPedido);
                v_PedidoManual = true;
            }
        }

        /// <summary>
        /// Permite reinicializar los controles que manipular la factura de Back Office
        /// </summary>
        private void LimpiarFacturaBackOffice()
        {
            //Se limpia los elementos de la factura de Back Office
            v_objCliente.v_objTransaccion = new TransaccionEncabezado();
            v_objCliente.v_objTransaccionPedido = new TransaccionEncabezado();
            COD_PEDIDO = string.Empty;
            COD_DOCUMENTO = string.Empty;
            v_DevolucionFactura = false;
            v_PedidoBackOffice = false;
            v_PedidoManual = false;
            view.FindByName<Label>("pnlTransacciones_clhCodigo").IsEnabled = true;
        }
        #endregion
    }
}
