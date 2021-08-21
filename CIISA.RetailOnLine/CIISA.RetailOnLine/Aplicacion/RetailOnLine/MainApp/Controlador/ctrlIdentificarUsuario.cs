using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.IdentificarUsuario;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.SettlementOnLine.VistaController;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Display.ViewController;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Power.ViewController;
using CIISA.RetailOnLine.Framework.Handheld.Print;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.SystemHH.ViewController;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System;
using System.Data;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    public class ctrlIdentificarUsuario
    {
        internal vistaIdentificarUsuario view { get; set; }
        private string v_reporteCargaDiaria { get; set; }

        internal ctrlIdentificarUsuario(vistaIdentificarUsuario pview)
        {
            view = pview;
            v_reporteCargaDiaria = string.Empty;
        }

        internal async void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);

            LogicaIdentificarUsuario_IURender _liu_iur = new LogicaIdentificarUsuario_IURender(this);

            _liu_iur.renderPaneles(view.FindByName<StackLayout>("pnlIdentificacion"));

            _liu_iur.renderComponents();

            var _establishDatabase = DependencyService.Get<IEstablishDatabase>();

            _establishDatabase.thereDataBase();

            if (!Variable._thereDataBase)
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention(
                    "El Sistema se inicializará por primera vez (recuerde verificar los consecutivos)."
                    + Environment.NewLine
                    + Environment.NewLine
                    + "Ingrese el código de la ruta deseado"
                    );

                view.FindByName<CheckBox>("pnlIdentificacion_chkAperturaNocturna").IsEnabled = false;

                view.FindByName<CheckBox>("pnlIdentificacion_chkRecarga").IsEnabled = false;
            }
            else
            {
                view.FindByName<CheckBox>("pnlIdentificacion_chkRecarga").IsEnabled = true;

                Logica_ManagerSistema _manager = new Logica_ManagerSistema();

                if (_manager.buscarEstado().Equals(SQL._close))
                {
                    view.FindByName<CheckBox>("pnlIdentificacion_chkAperturaNocturna").IsEnabled = true;
                    view.FindByName<CheckBox>("pnlIdentificacion_chkAperturaNocturna").IsVisible = true;
                }
                else
                {
                    view.FindByName<CheckBox>("pnlIdentificacion_chkAperturaNocturna").IsVisible = false;
                }
            }
            _liu_iur.renderMenu();
        }

        #region Menu
        internal async Task menu_mniAcceder_Click()
        {
            view.FindByName<ExtendedEntry>("pnlIdentificacion_txtIdentificacion").Text = view.FindByName<ExtendedEntry>("pnlIdentificacion_txtIdentificacion").Text.Replace(" ", "");

            LogicaIdentificarUsuario_IURender _liu_iur = new LogicaIdentificarUsuario_IURender(this);

            if (view.FindByName<CheckBox>("pnlIdentificacion_chkRecarga").Checked)
            {
                LogicaIdentificarUsuario_Verificar _liu_v = new LogicaIdentificarUsuario_Verificar(this);

                await _liu_v.verificarCodigoSeguridad();
            }
            else if (view.FindByName<CheckBox>("pnlIdentificacion_chkAperturaNocturna").Checked)
            {
                LogicaIdentificarUsuario_Verificar _liu_v = new LogicaIdentificarUsuario_Verificar(this);

                await _liu_v.verificarCodigoSeguridad();

            }
            else
            {
                LogicaIdentificarUsuario_IdentificarUsuario _liu_iu = new LogicaIdentificarUsuario_IdentificarUsuario(this);

                await _liu_iu.identificarUsuario();
            }

            var _establishDatabase = DependencyService.Get<IEstablishDatabase>();

            _establishDatabase.thereDataBase();

            if (LanzadorMenu._aperturaNocturna)
            {
                crtPostCarga _controladorPostcarga = new crtPostCarga();
                _controladorPostcarga.BorradoTablasPost_aperturaNoctura();

                Logica_ManagerSistema _manager = new Logica_ManagerSistema();
                _manager.actualizarEstado(false);
            }

            if (LanzadorMenu._cargaDesdeCero)
            {
                crtPostCarga _controladorPostcarga = new crtPostCarga();
                _controladorPostcarga.BorradoTablasPost_cargaDeCero();

                Logica_ManagerSistema _manager = new Logica_ManagerSistema();
                _manager.actualizarEstado(true);

                LogMessageAttention _lma = new LogMessageAttention();

                await _lma.generalAttention("Se cargó de cero la máquina."
                    + Environment.NewLine
                    + Environment.NewLine
                    + " * Se borró el inventario y se cerró la máquina");

                ScreenInicialization();
            }

            if (LanzadorMenu._mostrarMenuPrincipal)
            {
                ShowSROL _show = new ShowSROL();

                _show.mostrarPantallaMenuPrincipal();
            }

            _liu_iur.renderComponents();

            LanzadorMenu.inicializarVariables();
        }

        internal void menu_mniEnergia_Click()
        {
            ShowPower _show = new ShowPower();

            _show.showPowerForm();
        }

        internal async Task menu_mniSalir_Click()
        {
            LogicaIdentificarUsuario_IURender _liu_iur = new LogicaIdentificarUsuario_IURender(this);

            _liu_iur.renderPaneles(view.FindByName<StackLayout>("pnlIdentificacion"));

            _liu_iur.renderPaneles(view.FindByName<StackLayout>("pnlIdentificacion"));

            _liu_iur.renderMenu();

            ApplicationExit _applicationExit = new ApplicationExit();

            await _applicationExit.LogOut();
        }

        internal void menu_mniPruebaConexion_Click()
        {

            var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

            _testConnectionSROL.testConnectionString(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true);

        }

        internal async Task menu_mniPruebaImpresion_Click()
        {
            Logica_ManagerImpresora _manager = new Logica_ManagerImpresora();

            string _puertoImpresora = _manager.obtenerPuertoImpresora();

            PrintTest _printTest = new PrintTest();

            await _printTest.testPrint(_puertoImpresora);
        }

        internal async Task menu_mniLiquidadores_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (!_validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlIdentificacion_txtIdentificacion")))
            {
                Logica_ManagerAgenteVendedor _managerAgenteVendedor = new Logica_ManagerAgenteVendedor();
                string _codAgente = _managerAgenteVendedor.obtenerCodigoAgente();

                if (_codAgente.Equals(view.FindByName<ExtendedEntry>("pnlIdentificacion_txtIdentificacion").Text))
                {
                    DataTable _dt = _managerAgenteVendedor.buscarAgenteVendedor(_codAgente);

                    LogicaIdentificarUsuario _liu = new LogicaIdentificarUsuario(this);

                    _liu.establecerVariableDeEntorno_Agente(_dt);

                    string _codCompannia = _liu.obtenerCodigoCompannia(view.FindByName<Picker>("pnlIdentificacion_cbxCompannia").SelectedIndex);

                    GlobalVariables.AgentVariables_Temporal(_codCompannia, _codAgente);

                    ShowDisplay _showDisplay = new ShowDisplay();

                    Logica_ManagerClave _managerClave = new Logica_ManagerClave();

                    _showDisplay.showAccessCodeForm2(
                                                    _managerClave.obtenerCodigoPrincipal(),
                                                    "Ingrese el código para mostrar las opciones de liquidador.",
                                                    this,
                                                    "IdentificarUsuario"
                                                    );

                }
                else
                {
                    LogMessageAttention _logMessageAttention = new LogMessageAttention();
                    await _logMessageAttention.generalAttention("Código de la ruta incorrecto."
                        + Environment.NewLine
                        + Environment.NewLine
                        + "* Verifique no haber ingresado espacios en blanco");
                }
            }
            else
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention("Ingrese el código de la ruta");
            }
        }

        public void menu_mniLiquidadores_ClickParte2(bool _codigoCorrecto)
        {
            if (_codigoCorrecto)
            {
                ShowLiquidaciones _showLiquidaciones = new ShowLiquidaciones();

                _showLiquidaciones.mostrarPantallaLiquidadores();
            }
        }
        #endregion

        #region Checkbox
        internal async Task pnlIdentificacion_chkCargarTablas_CheckStateChanged()
        {
            LogicaIdentificarUsuario_IURender _liu_iur = new LogicaIdentificarUsuario_IURender(this);

            string _caption = "Carga Manual";

            if (view.FindByName<CheckBox>("pnlIdentificacion_chkRecarga").Checked)
            {                
                if (await LogMessages._dialogResultYes("¿Desea cargar manualmente la información?", _caption))
                {
                    view.FindByName<CheckBox>("pnlIdentificacion_chkAperturaNocturna").Checked = false;
                    view.FindByName<CheckBox>("pnlIdentificacion_chkRecarga").Checked = true;

                    _liu_iur.pintarTecladoRecarga(true);
                }
                else
                {
                    view.FindByName<CheckBox>("pnlIdentificacion_chkRecarga").Checked = false;


                    if (!view.FindByName<CheckBox>("pnlIdentificacion_chkAperturaNocturna").Checked)
                    {
                        _liu_iur.pintarTecladoRecarga(false);
                    }
                }
            }
            else {

                if (!view.FindByName<CheckBox>("pnlIdentificacion_chkAperturaNocturna").Checked)
                {
                    _liu_iur.pintarTecladoRecarga(false);
                }
            }
            
        }

        internal async Task pnlIdentificacion_chkAperturaNocturna_CheckStateChanged()
        {
            LogicaIdentificarUsuario_IURender _liu_iur = new LogicaIdentificarUsuario_IURender(this);

            string _caption = "Apertura Nocturna";

            if (view.FindByName<CheckBox>("pnlIdentificacion_chkAperturaNocturna").Checked)
            {
                if (await LogMessages._dialogResultYes("Este proceso debe ejecutar después de las 8 p.m. ¿Desea continuar?", _caption))
                {
                    view.FindByName<CheckBox>("pnlIdentificacion_chkAperturaNocturna").Checked = true;
                    view.FindByName<CheckBox>("pnlIdentificacion_chkRecarga").Checked = false;

                    _liu_iur.pintarTecladoRecarga(true);
                }
                else
                {
                    view.FindByName<CheckBox>("pnlIdentificacion_chkAperturaNocturna").Checked = false;

                    if (!view.FindByName<CheckBox>("pnlIdentificacion_chkRecarga").Checked)
                    {
                        _liu_iur.pintarTecladoRecarga(false);
                    }
                }
            }
            else {

                if (!view.FindByName<CheckBox>("pnlIdentificacion_chkRecarga").Checked)
                {
                    _liu_iur.pintarTecladoRecarga(false);
                }
            }
        }
        #endregion

        #region Botones
        internal void pnlIdentificacion_btnBorrarCodigo_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlIdentificacion_txtIdentificacion"));
        }

        internal void pnlIdentificacion_btnLimpiarCodigo_Click()
        {
            view.FindByName<ExtendedEntry>("pnlIdentificacion_txtIdentificacion").Text = string.Empty;
        }

        internal void pnlIdentificacion_btnLimpiarContrasenna_Click()
        {
            view.FindByName<ExtendedEntry>("pnlIdentificacion_txtContrasenna").Text = string.Empty;
        }

        internal void pnlIdentificacion_btnBorrarContrasenna_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlIdentificacion_txtContrasenna"));
        }

        internal void pnlIdentificacion_btnBorrarRecarga_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlIdentificacion_txtRecarga"));
        }

        internal void pnlIdentificacion_btnLimpiarRecarga_Click()
        {
            view.FindByName<ExtendedEntry>("pnlIdentificacion_txtRecarga").Text = string.Empty;
        }
        #endregion
    }
}
