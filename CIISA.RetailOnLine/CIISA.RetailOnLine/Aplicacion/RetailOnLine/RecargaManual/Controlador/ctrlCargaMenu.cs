using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Consulta.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RadioTelefonico.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.VistaControlador;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.External;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Controlador
{
    public class ctrlCargaMenu
    {
        internal vistaCargaMenu view { get; set; }
        internal bool v_hiloCorriendo = false;
        internal DateTime v_startTime = VarTime.getNow();
        private string v_cargaActual = string.Empty;
        public static CancellationTokenSource tokenSource = new CancellationTokenSource();
        private CancellationToken token = tokenSource.Token;
        internal bool v_cargaSistema = false;
        internal bool v_cargaPedido = false;
        internal ITaskActivity DPB = DependencyService.Get<ITaskActivity>();

        public ctrlCargaMenu(vistaCargaMenu pview)
        {
            view = pview;
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlMenu").Id))
            {
                view.Title = "Carga Información";
            }

            ppanel.IsVisible = true;
        }

        internal void cargarConfiguracionSistema(bool pavanzado)
        {
            var Info = DependencyService.Get<IServicio_Phone>();

            view.FindByName<ListView>("pnlMenu_ltvConfiguracion").ItemsSource = new ObservableCollection<pnlTelefono_ltvConfiguracion>();

            ObservableCollection<pnlTelefono_ltvConfiguracion> Source = new ObservableCollection<pnlTelefono_ltvConfiguracion>();

            pnlTelefono_ltvConfiguracion _lvi01 = new pnlTelefono_ltvConfiguracion();
            _lvi01.Configuracion = "Tipo Sistema";

            _lvi01.Valor = Info.PhoneType();

            if (pavanzado)
            {
                Source.Add(_lvi01);
            }

            pnlTelefono_ltvConfiguracion _lvi02 = new pnlTelefono_ltvConfiguracion();
            _lvi02.Configuracion = "Intensidad Señal";
            _lvi02.Valor = Info.GetSignalStrength();

            int Numero = 0;

            bool IsNumeric = int.TryParse(_lvi02.Valor, out Numero);

            if (IsNumeric)
            {
                if (Numero <= 74)
                {
                    _lvi02.TextColor = Color.Red;
                }
                if (Numero >= 75)
                {
                    _lvi02.TextColor = Color.Blue;
                }
            }
            else
            {
                _lvi02.TextColor = Color.Red;
            }

            Source.Add(_lvi02);

            pnlTelefono_ltvConfiguracion _lvi03 = new pnlTelefono_ltvConfiguracion();
            _lvi03.Configuracion = "Estado Red";
            _lvi03.Valor = Info.GetNetworkStatus();
            if (_lvi03.Valor.Equals(string.Empty))
            {
                _lvi03.Valor = "Sin Red";
            }
            if (pavanzado)
            {
                Source.Add(_lvi03);
            }

            pnlTelefono_ltvConfiguracion _lvi04 = new pnlTelefono_ltvConfiguracion();
            _lvi04.Configuracion = "Operador";
            _lvi04.Valor = Info.GetNetworkOperator();
            if (pavanzado)
            {
                Source.Add(_lvi04);
            }

            pnlTelefono_ltvConfiguracion _lvi05 = new pnlTelefono_ltvConfiguracion();
            _lvi05.Configuracion = "Radio Mfg";
            _lvi05.Valor = Info.GetManufacturer();
            if (pavanzado)
            {
                Source.Add(_lvi05);
            }

            pnlTelefono_ltvConfiguracion _lvi06 = new pnlTelefono_ltvConfiguracion();
            _lvi06.Configuracion = "Radio FW";
            _lvi06.Valor = Info.GetRevision();
            if (pavanzado)
            {
                Source.Add(_lvi06);
            }

            pnlTelefono_ltvConfiguracion _lvi07 = new pnlTelefono_ltvConfiguracion();
            _lvi07.Configuracion = "Radio Model";
            _lvi07.Valor = Info.GetModel();
            if (pavanzado)
            {
                Source.Add(_lvi07);
            }

            pnlTelefono_ltvConfiguracion _lvi08 = new pnlTelefono_ltvConfiguracion();
            _lvi08.Configuracion = "IMEI";
            _lvi08.Valor = Info.GetIMEI();
            if (pavanzado)
            {
                Source.Add(_lvi08);
            }

            pnlTelefono_ltvConfiguracion _lvi09 = new pnlTelefono_ltvConfiguracion();
            _lvi09.Configuracion = "SIM ID";
            _lvi09.Valor = Info.GetSIMID();

            if (pavanzado)
            {
                Source.Add(_lvi09);
            }

            pnlTelefono_ltvConfiguracion _lvi10 = new pnlTelefono_ltvConfiguracion();
            _lvi10.Configuracion = "Estado SIM";
            _lvi10.Valor = Info.GetSimStatus();
            if (_lvi10.Valor.Equals(string.Empty))
            {
                _lvi10.Valor = "Apagado";
                _lvi10.TextColor = Color.Red;
            }
            if (_lvi10.Valor == "Ready")
            {
                _lvi10.Valor = "Listo";
            }
            Source.Add(_lvi10);

            //pnlTelefono_ltvConfiguracion _lvi11 = new pnlTelefono_ltvConfiguracion();
            ////ListViewItem _lvi11 = new ListViewItem("Conexión a CIISA");
            //_lvi11.Configuracion = "Conexión a CIISA";
            //string _conexion = v_objLogica.convertToString(SystemState.ConnectionsCellularDescriptions);
            //if (_conexion.Equals(string.Empty))
            //{
            //    _conexion = "Sin Conexión";
            //    _lvi11.TextColor = Color.Red;
            //}
            //_lvi11.Valor = _conexion;
            //Source.Add(_lvi11);

            //pnlTelefono_ltvConfiguracion _lvi12 = new pnlTelefono_ltvConfiguracion();
            ////ListViewItem _lvi12 = new ListViewItem("Estado Conexión");
            //_lvi12.Configuracion = "Estado Conexión";
            //uint _uRet = v_objConn.CONN_IsConnected();
            //string _estadoConexion = (ConnPhoneRadio.CONNECTIONSTATUS)_uRet + string.Empty;
            //_lvi12.SubItems.Add(v_objLogica.getConnectionMessageError(_estadoConexion));
            //view.pnlTransacciones_ltvConfiguracion.Items.Add(_lvi12);

            view.FindByName<ListView>("pnlMenu_ltvConfiguracion").ItemsSource = Source;
        }

        internal void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);
            renderPaneles(view.FindByName<StackLayout>("pnlMenu"));

            //view.pnlMenu_txtBitacora.ScrollBars = 0;

            //v_objTapi = new TapiPhoneRadio();
            //v_objCommon = new CommonPhoneRadio();
            //v_objConn = new ConnPhoneRadio();
            //v_objLogica = new LogicPhoneRadio();

            //v_objTapi.tapiMessageEvent += new EventHandler(v_objLogica.activateTapiMessageEvent);

            //v_objTapi.TAPI_Open();

            cargarConfiguracionSistema(false);
            //view.temporizador.Enabled = true;
            //view.tmrDuracion.Enabled = true;

            //bool _activar = v_objLogica.renderCheckBoxes(
            //                    view.pnlMenu_chkRadioTelefonico,
            //                    view.pnlMenu_chkAdministradorConexion,
            //                    view.pnlMenu_chkWiFi,
            //                    v_objTapi,
            //                    v_objConn
            //                    );
            //El Telefono siempre estara activo
            bool _activar = true;

            GUIRecargaManual _guiCarga = new GUIRecargaManual(this);

            _guiCarga.actualizarGUIdesdeHiloBoton(_activar, !_activar, true);

            view.FindByName<ListView>("pnlMenu_ltvConfiguracion").Focus();
        }

        private async Task pnlMenu_btnInventario(bool pcargaSistema, bool pcargaPedido)
        {
            if (await LogMessages._dialogResultYes("¿Desea cargar el inventario?", "Carga Información"))
            {
                v_cargaSistema = pcargaSistema;
                v_cargaPedido = pcargaPedido;

                GUIRecargaManual _guiCarga = new GUIRecargaManual(this);

                _guiCarga.actualizarGUIdesdeHiloBoton(false, true, false);

                v_hiloCorriendo = true;

                v_startTime = VarTime.getNow();

                v_cargaActual = TablesROL._inventario;

                ctrlCarga_inventario _controlador = new ctrlCarga_inventario(this);

                //v_hilo = new Thread(new ThreadStart(_controlador.informacionInventarioRecargar));
                //v_hilo.Start();
                //v_hilo = Task.Factory.StartNew(_controlador.informacionInventarioRecargar, token);
                DPB.StartNewTaskInventario(_controlador);
            }
        }

        internal void renderBotones(bool pbtnCargar, bool pbtnAbortar, bool pbtnCerrar)
        {
            view.FindByName<Button>("pnlMenu_btnAnulacion").IsEnabled = pbtnCargar;

            view.FindByName<Button>("pnlMenu_btnCliente").IsEnabled = pbtnCargar;

            view.FindByName<Button>("pnlMenu_btnEstablecimiento").IsEnabled = pbtnCargar;

            view.FindByName<Button>("pnlMenu_btnDescuentos").IsEnabled = pbtnCargar;

            view.FindByName<Button>("pnlMenu_btnDescuentosGeneral").IsEnabled = pbtnCargar;

            view.FindByName<Button>("pnlMenu_btnFactura").IsEnabled = pbtnCargar;

            view.FindByName<Button>("pnlMenu_btnIndicadorFactura").IsEnabled = pbtnCargar;

            view.FindByName<Button>("pnlMenu_btnInventario").IsEnabled = pbtnCargar;

            view.FindByName<Button>("pnlMenu_btnListaPrecios").IsEnabled = pbtnCargar;

            view.FindByName<Button>("pnlMenu_btnPrecioProducto").IsEnabled = pbtnCargar;

            view.FindByName<Button>("pnlMenu_btnProducto").IsEnabled = pbtnCargar;

            view.FindByName<Button>("pnlMenu_btnVisita").IsEnabled = pbtnCargar;

            view.FindByName<Button>("pnlMenu_btnDetalleReses").IsEnabled = pbtnCargar;

            view.FindByName<Button>("pnlMenu_btnMensajeFactura").IsEnabled = pbtnCargar;

            view.FindByName<Button>("pnlMenu_btnClienteIndividual").IsEnabled = pbtnCargar;

            view.FindByName<Button>("pnlMenu_btnIndicadorClienteFactura").IsEnabled = pbtnCargar;

            view.FindByName<Button>("pnlMenu_btnDescuentosCliente").IsEnabled = pbtnCargar;

            view.FindByName<Button>("pnlMenu_btnImpresora").IsEnabled = pbtnCargar;

            view.FindByName<Button>("pnlMenu_btnPedidos").IsEnabled = pbtnCargar;

            view.FindByName<StackLayout>("pnlMenu_stkAbortar").IsVisible = pbtnAbortar;

            view.FindByName<StackLayout>("pnlMenu_stkCerrar").IsVisible = pbtnCerrar;

            view.FindByName<Button>("pnlMenu_btnPruebaConexion").IsEnabled = pbtnCargar;
        }

        internal async Task pnlMenu_btnPruebaConexion_Click()
        {
            //view.pnlMenu_ltbEstado.Items.Clear();
            //view.pnlMenu_ltbEstado.Items.Add(Resources.attempting);

            var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

            if (await _testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
            {
                await LogMessageSuccess._generalSuccess(LogMessages.withConnection());
                //view.pnlMenu_ltbEstado.Items.Add(Resources.connected);
            }
            else
            {
                await LogMessageSuccess._generalSuccess(LogMessages.withoutConnection());
                //v_objConn.CONN_Disconnect();
                //view.pnlMenu_ltbEstado.Items.Add(Resources.disconnected);
            }

            //v_objLogica.renderCheckBoxes(
            //    view.pnlMenu_chkRadioTelefonico,
            //    view.pnlMenu_chkAdministradorConexion,
            //    view.pnlMenu_chkWiFi,
            //    v_objTapi,
            //    v_objConn);

            cargarConfiguracionSistema(false);

            //Cursor.Current = Cursors.Default;
        }

        internal async Task pnlMenu_btnAnulacion_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea cargar las autorizaciones de anulación?", "Carga Información"))
            {
                GUIRecargaManual _guiCarga = new GUIRecargaManual(this);

                _guiCarga.actualizarGUIdesdeHiloBoton(false, true, false);

                v_hiloCorriendo = true;

                v_startTime = VarTime.getNow();

                v_cargaActual = TablesROL._cliente;

                ctrlCarga_indAnulacion _controlador = new ctrlCarga_indAnulacion(this);

                DPB.StartNewTaskIndAnulacion(_controlador);

            }
        }

        internal async Task pnlMenu_btnCliente_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea cargar los Clientes?", "Carga Información"))
            {
                GUIRecargaManual _guiCarga = new GUIRecargaManual(this);

                _guiCarga.actualizarGUIdesdeHiloBoton(false, true, false);

                v_hiloCorriendo = true;

                v_startTime = VarTime.getNow();

                v_cargaActual = TablesROL._cliente;

                ctrlCarga_cliente _controlador = new ctrlCarga_cliente(this);

                //v_hilo = new Thread(new ThreadStart(_controlador.informacionClienteRecargar));
                //v_hilo.Start();
                //v_hilo = Task.Factory.StartNew(_controlador.informacionClienteRecargar, token);
                DPB.StartNewTaskClientes(_controlador);

            }
        }

        internal async Task pnlMenu_btnEstablecimiento_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea cargar los Establecimientos?", "Carga Información"))
            {
                GUIRecargaManual _guiCarga = new GUIRecargaManual(this);

                _guiCarga.actualizarGUIdesdeHiloBoton(false, true, false);

                v_hiloCorriendo = true;

                v_startTime = VarTime.getNow();

                v_cargaActual = TablesROL._establecimiento;

                ctrlCarga_establecimiento _controlador = new ctrlCarga_establecimiento(this);

                //v_hilo = new Thread(new ThreadStart(_controlador.informacionEstablecimientoRecargar));
                //v_hilo.Start();
                //v_hilo = Task.Factory.StartNew(_controlador.informacionEstablecimientoRecargar, token);
                DPB.StartNewTaskEstablecimientos(_controlador);
            }
        }

        internal async Task pnlMenu_btnDescuentos_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea cargar los Descuentos?", "Carga Información"))
            {
                GUIRecargaManual _guiCarga = new GUIRecargaManual(this);

                _guiCarga.actualizarGUIdesdeHiloBoton(false, true, false);

                v_hiloCorriendo = true;

                v_startTime = VarTime.getNow();

                v_cargaActual = TablesROL._descuentos;

                ctrlCarga_descuentos _controlador = new ctrlCarga_descuentos(this);

                DPB.StartNewTaskDescuentos(_controlador);
            }
        }

        internal async Task pnlMenu_btnDescuentosGeneral_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea cargar los Descuentos Generales?", "Carga Información"))
            {
                GUIRecargaManual _guiCarga = new GUIRecargaManual(this);

                _guiCarga.actualizarGUIdesdeHiloBoton(false, true, false);

                v_hiloCorriendo = true;

                v_startTime = VarTime.getNow();

                v_cargaActual = TablesROL._descuentos;

                ctrlCarga_descuentosGeneral _controlador = new ctrlCarga_descuentosGeneral(this);

                DPB.StartNewTaskDescuentosGeneral(_controlador);
            }
        }

        internal async Task pnlMenu_btnFactura_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea cargar las Facturas?", "Carga Información"))
            {
                GUIRecargaManual _guiCarga = new GUIRecargaManual(this);

                _guiCarga.actualizarGUIdesdeHiloBoton(false, true, false);

                v_hiloCorriendo = true;

                v_startTime = VarTime.getNow();

                v_cargaActual = TablesROL._factura;

                ctrlCarga_facturas _controlador = new ctrlCarga_facturas(this);

                DPB.StartNewTaskFacturas(_controlador);
            }
        }

        internal async Task pnlMenu_btnIndicadorFactura_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea cargar los Indicadores de Facturas?", "Carga Información"))
            {
                GUIRecargaManual _guiCarga = new GUIRecargaManual(this);

                _guiCarga.actualizarGUIdesdeHiloBoton(false, true, false);

                v_hiloCorriendo = true;

                v_startTime = VarTime.getNow();

                v_cargaActual = TablesROL._indicadorFactura;

                ctrlCarga_indicadores _controlador = new ctrlCarga_indicadores(this);
                
                DPB.StartNewTaskIndicadorFacturas(_controlador);
            }
        }

        internal async Task pnlMenu_btnInventario_Click()
        {
            Logica_ManagerInventario _manager = new Logica_ManagerInventario();

            bool _inventarioVacio = _manager.InventarioVacio();

            if (_inventarioVacio)
            {
                ctrConsultaTrasladosDiarios _consulta = new ctrConsultaTrasladosDiarios();

                DataTable _dt = await _consulta.ConsultaTrasladosDiarios();

                if (_dt.Rows.Count > 0)
                {
                    await pnlMenu_btnInventario(true, true);
                }
                else
                {
                    LogMessageAttention _lma = new LogMessageAttention();

                    await _lma.generalAttention("Sin traslados aplicados."
                        + Environment.NewLine
                        + Environment.NewLine
                        + "* Sin continua generará el inventaro incompleto, y deberá esperar que se realice el ajuste manualmente"
                        );

                    if (await LogMessages._dialogResultYes("¿Desea realmente cargar el inventario?", "Carga Información"))
                    {
                        await pnlMenu_btnInventario(true, true);
                    }
                }
            }
            else
            {
                await pnlMenu_btnInventario(false, false);
            }
        }

        internal async Task pnlMenu_btnListaPrecios_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea cargar la Lista de Precios?", "Carga Información"))
            {
                GUIRecargaManual _guiCarga = new GUIRecargaManual(this);

                _guiCarga.actualizarGUIdesdeHiloBoton(false, true, false);

                v_hiloCorriendo = true;

                v_startTime = VarTime.getNow();

                v_cargaActual = TablesROL._listaPrecios;

                ctrlCarga_listaPrecios _controlador = new ctrlCarga_listaPrecios(this);

                DPB.StartNewTaskListaPrecios(_controlador);
            }
        }

        internal async Task pnlMenu_btnPrecioProducto_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea cargar los Precios de Producto?", "Carga Información"))
            {
                GUIRecargaManual _guiCarga = new GUIRecargaManual(this);

                _guiCarga.actualizarGUIdesdeHiloBoton(false, true, false);

                v_hiloCorriendo = true;

                v_startTime = VarTime.getNow();

                v_cargaActual = TablesROL._precioProducto;

                ctrlCarga_precioProducto _controlador = new ctrlCarga_precioProducto(this);

                DPB.StartNewTaskPrecioProductos(_controlador);
            }
        }

        internal async Task pnlMenu_btnProducto_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea cargar los Productos?", "Carga Información"))
            {
                GUIRecargaManual _guiCarga = new GUIRecargaManual(this);

                _guiCarga.actualizarGUIdesdeHiloBoton(false, true, false);

                v_hiloCorriendo = true;

                v_startTime = VarTime.getNow();

                v_cargaActual = TablesROL._producto;

                ctrlCarga_producto _controlador = new ctrlCarga_producto(this);
                
                DPB.StartNewTaskProductos(_controlador);
            }
        }

        internal async Task pnlMenu_btnVisita_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea cargar la Visita?", "Carga Información"))
            {
                GUIRecargaManual _guiCarga = new GUIRecargaManual(this);

                _guiCarga.actualizarGUIdesdeHiloBoton(false, true, false);

                v_hiloCorriendo = true;

                v_startTime = VarTime.getNow();

                v_cargaActual = TablesROL._visitas;

                ctrlCarga_visita _controlador = new ctrlCarga_visita(this);
                
                DPB.StartNewTaskVisita(_controlador);
            }
        }

        internal async Task pnlMenu_btnDetalleReses_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea cargar los detalles de reses?", "Carga Información"))
            {
                GUIRecargaManual _guiCarga = new GUIRecargaManual(this);

                _guiCarga.actualizarGUIdesdeHiloBoton(false, true, false);

                v_hiloCorriendo = true;

                v_startTime = VarTime.getNow();

                v_cargaActual = TablesROL._DetalleReses;

                ctrlCarga_DetalleReses _controlador = new ctrlCarga_DetalleReses(this);

                DPB.StartNewTaskDetalleReses(_controlador);
            }
        }

        internal async Task pnlMenu_btnMensajeFactura_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea cargar el mensaje de la factura?", "Carga Información"))
            {
                GUIRecargaManual _guiCarga = new GUIRecargaManual(this);

                _guiCarga.actualizarGUIdesdeHiloBoton(false, true, false);

                v_hiloCorriendo = true;

                v_startTime = VarTime.getNow();

                v_cargaActual = TablesROL._MensajeFactura;

                ctrlCarga_MensajeFactura _controlador = new ctrlCarga_MensajeFactura(this);

                DPB.StartNewTaskMensajeFactura(_controlador);
            }
        }

        internal async Task pnlMenu_btnImpresora_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea cargar la Impresora?", "Carga Información"))
            {
                GUIRecargaManual _guiCarga = new GUIRecargaManual(this);

                _guiCarga.actualizarGUIdesdeHiloBoton(false, true, false);

                v_hiloCorriendo = true;

                v_startTime = VarTime.getNow();

                v_cargaActual = TablesROL._impresora;

                ctrlCarga_impresora _controlador = new ctrlCarga_impresora(this);
                
                DPB.StartNewTaskImpresora(_controlador);
            }
        }

        internal async Task pnlMenu_btnPedidos_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea cargar los pedidos?", "Carga Información"))
            {
                GUIRecargaManual _guiCarga = new GUIRecargaManual(this);

                _guiCarga.actualizarGUIdesdeHiloBoton(false, true, false);

                v_hiloCorriendo = true;

                v_startTime = VarTime.getNow();

                v_cargaActual = TablesROL._encabezadoPedido;

                ctrlCarga_encabezadoPedido _controlador = new ctrlCarga_encabezadoPedido(this);

                DPB.StartNewTaskPedidos(_controlador);
            }
        }

        internal async Task pnlMenu_btnClienteIndividual_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea cargar un Cliente?", "Carga Información"))
            {
                GUIRecargaManual _guiCarga = new GUIRecargaManual(this);

                await Task.Run(() =>_guiCarga.actualizarGUIdesdeHiloBoton(false, true, false));
               
                v_startTime = VarTime.getNow();

                v_cargaActual = TablesROL._factura;

                ctrlCarga_cliente _controlador = new ctrlCarga_cliente(this);

                _controlador.informacionClienteIndividualRecargarParte1();
            }
        }

        public void informacionClienteIndividualRecargarParte3(ctrlCarga_cliente _controlador)
        {
            v_hiloCorriendo = true;
            
            DPB.StartNewTaskinformacionClienteIndividualRecargarParte3(_controlador);
        }

        internal async Task pnlMenu_btnIndicadorClienteFactura_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea cargar los Indicadores de Cliente?", "Carga Información"))
            {
                GUIRecargaManual _guiCarga = new GUIRecargaManual(this);

                _guiCarga.actualizarGUIdesdeHiloBoton(false, true, false);

                v_startTime = VarTime.getNow();

                v_cargaActual = TablesROL._factura;

                ctrlCarga_indicadores _controlador = new ctrlCarga_indicadores(this);

                await _controlador.informacionIndicadoresIndividualRecargarParte1();
            }
        }

        public void informacionIndicadoresIndividualRecargarParte3(ctrlCarga_indicadores _controlador)
        {
            v_hiloCorriendo = true;
            
            DPB.informacionIndicadoresIndividualRecargarParte3(_controlador);
        }

        internal async Task pnlMenu_btnDescuentosCliente_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea cargar los Descuentos de Cliente?", "Carga Información"))
            {
                GUIRecargaManual _guiCarga = new GUIRecargaManual(this);

                _guiCarga.actualizarGUIdesdeHiloBoton(false, true, false);

                v_startTime = VarTime.getNow();

                v_cargaActual = TablesROL._factura;

                ctrlCarga_descuentos _controlador = new ctrlCarga_descuentos(this);

                await _controlador.informacionDescuentoIndividualRecargarParte1();
            }
        }

        public void informacionDescuentoIndividualRecargarParte3(ctrlCarga_descuentos _controlador)
        {
            v_hiloCorriendo = true;
            
            DPB.informacionDescuentoIndividualRecargarParte3(_controlador);
        }

        internal void menu_mniClose_Click()
        {
            if (v_hiloCorriendo)
            {
                DPB.AbortNewTask();
                v_cargaActual = null;
                v_hiloCorriendo = false;
            }
            
            Application.Current.MainPage.Navigation.PopAsync();
        }

        internal async Task menu_mniAbortar_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea abortar la carga de información?", "Abortar"))
            {
                GUIRecargaManual _guiCarga = new GUIRecargaManual(this);

                Log _log = new Log();

                Editor _textBox = new Editor();

                if (v_hiloCorriendo)
                {

                    v_hiloCorriendo = false;
                    DPB.AbortNewTask();

                    if (v_cargaActual.Equals(TablesROL._descuentos))
                    {
                        _guiCarga.actualizarGUIdesdeHiloBoton(false, false, true);

                        v_startTime = VarTime.getNow();

                        v_cargaActual = TablesROL._descuentos;

                        OperationSQL.deleteTableFeedbackTextBox(
                            TablesROL._descuentos,
                            _textBox,
                            _log
                            );

                        ctrlCarga_descuentos _controlador = new ctrlCarga_descuentos(this);

                        _controlador.informacionDescuentoRespaldo(_textBox, _log);

                    }
                }

                _guiCarga.actualizarGUIdesdeHilo("CANCELO LA CARGA - 0%", Color.FromRgb(158, 4, 4));

                _guiCarga.actualizarGUIdesdeHiloResultadoCarga(
                    "La operación al cargar " + v_cargaActual + " se canceló.",
                    Color.White
                    );

                v_cargaActual = null;

                _guiCarga.actualizarGUIdesdeHiloBoton(true, false, true);
            }
        }
    }
}
