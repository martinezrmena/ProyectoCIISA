using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Consulta.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Consulta.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.InventoryOnLine.ViewController;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RadioTelefonico.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.SettlementOnLine.VistaController;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Backlight.ViewController;
using CIISA.RetailOnLine.Framework.Handheld.Display.ViewController;
using CIISA.RetailOnLine.Framework.Handheld.GPS.ViewController;
using CIISA.RetailOnLine.Framework.Handheld.MemorySpace.ViewController;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.SystemHH.ViewController;
using System;
using System.Data;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    public class ctrlMenu
    {
        private vistaMenu view { get; set; }

        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        internal ctrlMenu(vistaMenu pview)
        {
            view = pview;
        }

        internal OnUpdateDataBase DataBase = new OnUpdateDataBase();

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlMenu"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlMenu").Id))
            {
                view.Title = "Menú "
                    + Simbol._squareBracketLeft
                    + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent
                    + Simbol._squareBracketRight;
            }

            ppanel.IsVisible = true;
        }

        internal async void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlMenu"));

            DeviceMemoryInfo _deviceMemoryInfo = new DeviceMemoryInfo();

            if (await _deviceMemoryInfo.maintenanceRequired())
            {
                ShowMemorySpace _show = new ShowMemorySpace();

                _show.showMemorySpaceForm(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));
            }

            DataBase.UpdateDatabase();

        }

        #region Menu        

        internal void menu_mniBloquear_Click()
        {
            ShowDisplay _show = new ShowDisplay();

            _show.showLockScreenForm(
                Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)
                );
        }

        internal async Task menu_mniSalir_Click()
        {
            ApplicationExit _applicationExit = new ApplicationExit();

            await _applicationExit.LogOut();
        }

        internal async void SalirBackButton() {

            try
            {
                Device.BeginInvokeOnMainThread(async () => {
                    var result = await view.DisplayAlert("Salir del sistema", "¿Está seguro que desea salir del sistema?", "Aceptar", "Cancelar");

                    if (result)
                    {
                        ApplicationExit _applicationExit = new ApplicationExit();

                        await _applicationExit.DirectExit();
                    }
                });
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

        }

        #endregion

        #region GESTIÓN DE VENTA
        internal async void pnlMenu_btnBitacora_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnBitacora"));

            ShowSROL _showBitacora = new ShowSROL();

            _showBitacora.mostrarPantallaBitcora();

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnBitacora"));
        }

        internal async void pnlMenu_btnRuta_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnRuta"));

            ShowSROL _showRuta = new ShowSROL();

            _showRuta.mostrarPantallaRuta();

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnRuta"));
        }

        internal async void pnlMenu_btnVisita_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnVisita"));

            ShowSROL _showVisita = new ShowSROL();

            _showVisita.mostrarPantallaVisita();

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnVisita"));
        }

        internal async void pnlMenu_btnAnulaciones_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnAnulaciones"));
            
            ShowSROL _showAnulaciones = new ShowSROL();

            _showAnulaciones.mostrarPantallaAnulaciones();

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnAnulaciones"));

        }

        internal async void pnlMenu_btnNuevoCliente_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnNuevoCliente"));

            ShowPresentacion _showNuevoCliente = new ShowPresentacion();

            await _showNuevoCliente.mostrarPantallaNuevoCliente();

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnNuevoCliente"));
        }

        internal async void pnlMenu_btnEntregaPedidos_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnEntregaPedidos"));

            ShowSROL _showEntregaPedidos = new ShowSROL();

            _showEntregaPedidos.mostrarPantallaEntregaPedidos();

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnEntregaPedidos"));
        }

        internal async void pnlMenu_btnCuentaCerrada_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnCuentaCerrada"));

            ShowSROL _showCuentaCerrada = new ShowSROL();

            _showCuentaCerrada.mostrarPantallaCuentaCerrada();

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnCuentaCerrada"));
        }

        internal async void pnlMenu_btnIndicadoresFacturacion_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnIndicadoresFacturacion"));

            ShowSROL _showIndicadoresFacturacion = new ShowSROL();

            _showIndicadoresFacturacion.mostrarPantallaIndicadoresFacturacion();

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnIndicadoresFacturacion"));
        }

        internal async void pnlMenu_btnCuentasBancarias_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnCuentasBancarias"));

            ShowSROL _showCuentaBancaria = new ShowSROL();

            _showCuentaBancaria.mostrarPantallaCuentaBancaria();

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnCuentasBancarias"));
        }

        internal async void pnlMenu_btnTelefono_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnTelefono"));

            ShowRadioTelefonico _showTelefono = new ShowRadioTelefonico();

            _showTelefono.mostrarPantallaTelefono();

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnTelefono"));
        }
        #endregion

        #region INVENTARIO
        internal async void pnlMenu_btnInventarioTeorico_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnInventarioTeorico"));

            ShowSROL _showInventarioTeorico = new ShowSROL();

            _showInventarioTeorico.mostrarPantallaInventarioTeorico();

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnInventarioTeorico"));
        }

        internal async void pnlMenu_btnInventarioPedidos_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnInventarioPedidos"));

            ShowSROL _showInventarioPedidos = new ShowSROL();

            _showInventarioPedidos.mostrarPantallaInventarioPedidos(string.Empty);

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnInventarioPedidos"));
        }

        internal async Task pnlMenu_btnTraslados_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnTraslados"));

            ctrConsultaTrasladosDiarios _consulta = new ctrConsultaTrasladosDiarios();

            DataTable _dt = await _consulta.ConsultaTrasladosDiarios();

            if (_dt.TableName != String.Empty)
            {
                if (_dt.Rows.Count > 0)
                {
                    ProcesoImpresion _impresion = new ProcesoImpresion();

                    await _impresion.imprimirReporteEnLineaTrasladosDiarios(_dt);
                }
                else
                {
                    LogMessageAttention _lma = new LogMessageAttention();

                    Logica_ManagerInventario _manager = new Logica_ManagerInventario();

                    if (_manager.InventarioVacio())
                    {
                        await _lma.generalAttention("Sin traslados aplicados."
                            + Environment.NewLine
                            + Environment.NewLine
                            + "* No se debe cargar el inventario");
                    }
                    else
                    {
                        await _lma.generalAttention("Sin traslados aplicados");
                    }
                }
            }
            else
            {
                LogMessageAttention _lma = new LogMessageAttention();

                await _lma.generalAttention("No se obtuvo información.");
            }

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnTraslados"));
        }
        #endregion

        #region REPORTES
        internal async Task pnlMenu_btnReportes_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnReportes"));

            ShowPresentacion _showReporte = new ShowPresentacion();

            await _showReporte.mostrarPantallaReporte();

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnReportes"));
        }

        internal async void pnlMenu_btnFlujoContable_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnFlujoContable"));

            ShowSROL _showFlujoDinero = new ShowSROL();

            _showFlujoDinero.mostrarPantallaFlujoDinero();

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnFlujoContable"));
        }
        #endregion

        #region SINCRONIZAR
        internal async void pnlMenu_btnDescarga_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnDescarga"));

            ShowDescarga _showDescarga = new ShowDescarga();

            _showDescarga.mostrarPantallaDescarga(PantallasSistema._pantallaMenu, TipoDescarga._normal, false);

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnDescarga"));
        }

        internal async void pnlMenu_btnCarga_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnCarga"));

            ShowRecargaManual _showMenuCarga = new ShowRecargaManual();

            _showMenuCarga.mostrarPantallaMenuCarga();

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnCarga"));
        }

        internal async void pnlMenu_btnConsultaOV_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnConsultaOV"));

            ShowConsulta _showConsultaOV = new ShowConsulta();

            _showConsultaOV.mostrarPantallaConsultaOV();

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnConsultaOV"));
        }

        internal async void pnlMenu_btnConsultaDocs_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnConsultaDocs"));

            ShowConsulta _showConsultaDocumentos = new ShowConsulta();

            _showConsultaDocumentos.mostrarPantallaConsultaDocumentos();

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnConsultaDocs"));
        }

        internal async void pnlMenu_btnConsultaRecibos_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnConsultaRecibos"));

            ShowConsulta _showReciboRecaudacion = new ShowConsulta();

            _showReciboRecaudacion.mostrarPantallaConsultaReciboRecaudacion();

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnConsultaRecibos"));
        }
        #endregion

        #region UTILES
        internal async void pnlMenu_btnEstadoMemoria_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnEstadoMemoria"));

            ShowMemorySpace _showMemorySpace = new ShowMemorySpace();

            _showMemorySpace.showMemorySpaceForm(
                    Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)
                    );

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnEstadoMemoria"));
        }

        internal async void pnlMenu_btnIluminacion_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnIluminacion"));

            ShowBacklight _showBacklight = new ShowBacklight();

            _showBacklight.showBacklightForm();

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnIluminacion"));
        }

        internal async void pnlMenu_btnPruebaPantalla_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnPruebaPantalla"));

            ShowSROL _showPantallaPrueba = new ShowSROL();

            _showPantallaPrueba.mostrarPantallaPruebaPantalla();

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnPruebaPantalla"));
        }

        internal async void pnlMenu_btnUbicacionGPS_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnUbicacionGPS"));

            ShowGPS _showGPS = new ShowGPS();

            _showGPS.showGpsForm();

            await GPS_Info.v_gpsDevice.StartGps();

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnUbicacionGPS"));
        }

        internal async void pnlMenu_btTipoImpresion_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnTipoImpresion"));

            ShowSROL _showPantallaPrueba = new ShowSROL();

            _showPantallaPrueba.mostrarPantallaTipoImpresion();

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnTipoImpresion"));
        }

        #endregion

        #region LIQUIDADORES
        internal async Task pnlMenu_btnFactFaltantes_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnFactFaltantes"));

            ShowDisplay _showDisplay2 = new ShowDisplay();

            Logica_ManagerClave _managerClave2 = new Logica_ManagerClave();

            await _showDisplay2.showAccessCodeForm2(
                _managerClave2.obtenerCodigoFaltantes(),
                "Ingrese el código para ingresar a la Facturación de Faltantes.",
                this,
                "FacturacionFaltantes");

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnFactFaltantes"));
        }

        public async Task pnlMenu_btnFactFaltantes_ClickParte2(bool _accesoFacturacionFaltantes)
        {
            ShowSROL _showVisitaFacturacionFaltantes = new ShowSROL();

            if (_accesoFacturacionFaltantes)
            {
                Logica_ManagerAgenteVendedor _managerAgenteVendedor = new Logica_ManagerAgenteVendedor();

                string _codCliente = _managerAgenteVendedor.obtenerCodigoClienteAgenteVendedor();

                Cliente _objClienteFacturacionFaltantes = new Cliente { v_no_cliente = _codCliente };

                Logica_ManagerCliente _manager = new Logica_ManagerCliente();

                await _manager.buscarClientePorCodigoCliente(_objClienteFacturacionFaltantes);

                _showVisitaFacturacionFaltantes.v_objCliente = _objClienteFacturacionFaltantes;
            }

            if (_accesoFacturacionFaltantes)
            {
                _showVisitaFacturacionFaltantes.mostrarPantallaVisitaDesdeFacturacionFaltantes();
            }
        }

        internal async Task pnlMenu_btnConsolidarCerrar_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnConsolidarCerrar"));

            if (await LogMessages._dialogResultYes("¿Desea Cerrar y Consolidar?", "Cerrado Consolidado"))
            {                
                Logica_ManagerClave _managerClaveConsolidacion = new Logica_ManagerClave();

                ShowDisplay _showDisplayConsolidacion = new ShowDisplay();

                await _showDisplayConsolidacion.showAccessCodeForm2(
                                                _managerClaveConsolidacion.obtenerCodigoTomaFisica(),
                                                 "Ingrese el código para acceder a la Consolidación Automática y Cierre.",
                                                this,
                                                "ConsolidarCerrar");
            }

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnConsolidarCerrar"));
        }

        internal void pnlMenu_btnConsolidarCerrar_ClickParte2(bool _codigoCorrectoConsolidacion)
        {

            if (_codigoCorrectoConsolidacion)
            {
               var DPB = DependencyService.Get<ITaskActivity>();

                DPB.StartConsolidar_Cerrar();
            }
        }

        internal async Task pnlMenu_btnTomaFisica_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnTomaFisica"));

            ShowDisplay _showDisplay = new ShowDisplay();
            //bool _codigoCorrecto = false;

            Logica_ManagerClave _managerClave = new Logica_ManagerClave();

            await _showDisplay.showAccessCodeForm2(
                                             _managerClave.obtenerCodigoTomaFisica(),
                                             "Ingrese el código para acceder a la Toma Física.",
                                              this,
                                              "TomaFisica");

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnTomaFisica"));

        }

        internal async Task pnlMenu_btnTomaFisica_ClickParte2(bool _codigoCorrecto) {

            if (_codigoCorrecto)
            {
                ShowTomaFisica _showTomaFisica = new ShowTomaFisica();

                await _showTomaFisica.mostrarPantallaTomaFisica();
            }
        }

        internal async Task pnlMenu_btnLiquidadores_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnLiquidadores"));

            ShowDisplay _showLiquidadore = new ShowDisplay();

            Logica_ManagerClave _managerClaveLiq = new Logica_ManagerClave();

            await _showLiquidadore.showAccessCodeForm2(
                                            _managerClaveLiq.obtenerCodigoPrincipal(),
                                            "Ingrese el código para mostrar las opciones de liquidador.",
                                            this,
                                            "Liquidadores");

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnLiquidadores"));


        }

        internal void pnlMenu_btnLiquidadores_ClickParte2(bool _codigoCorrectoLiq)
        {
            if (_codigoCorrectoLiq)
            {
                ShowLiquidaciones _showLiquidaciones = new ShowLiquidaciones();

                _showLiquidaciones.mostrarPantallaLiquidadores();
            }
        }

        internal async void pnlMenu_btnAcercaDe_Click()
        {
            await simulateClickGestures.NotEnabled(view.FindByName<Button>("pnlMenu_btnAcercaDe"));

            ShowSystemHH _showAbout = new ShowSystemHH();

            _showAbout.showAboutForm(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));

            await simulateClickGestures.Enabled(view.FindByName<Button>("pnlMenu_btnAcercaDe"));
        }
        #endregion

    }
}
