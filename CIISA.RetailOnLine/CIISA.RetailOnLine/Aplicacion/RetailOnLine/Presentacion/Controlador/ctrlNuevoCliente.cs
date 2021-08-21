using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Consulta.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.Vista;
using CIISA.RetailOnLine.Framework.Common.Compress;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.External.GpsThings;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.GPS.ViewController;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using Geolocator.Plugin;
using Geolocator.Plugin.Abstractions;
using System;
using System.Data;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.Controlador
{
    internal class ctrlNuevoCliente
    {
        private vistaNuevoCliente view { get; set; }

        private string v_cedula = string.Empty;
        private string v_fechaNacimiento = string.Empty;
        private string v_primerApellido = string.Empty;
        private string v_segundoApellido = string.Empty;
        private string v_nombre = string.Empty;
        private string v_email = string.Empty;
        private double v_latitud = 0;
        private double v_longitud = 0;
        IGeolocator locator = null;

        private string v_codigoTipoIdentificacion = string.Empty;

        internal ctrlNuevoCliente(vistaNuevoCliente pview)
        {
            view = pview;
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlInformacion"));

            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlConsulta"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlInformacion").Id))
            {
                view.Title = "Nuevo Cliente";
            }

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlConsulta").Id))
            {
                view.Title = "Consulta Cliente";
            }

            ppanel.IsVisible = true;
        }

        private void RenderMenu()
        {
            RenderMenuBottom();

            if (view.FindByName<StackLayout>("pnlConsulta").IsVisible)
            {
                view.FindByName<StackLayout>("pnlCliente_stkConsultar").IsVisible = true;
                view.FindByName<StackLayout>("pnlCliente_stkCerrar").IsVisible = true;
            }

            if (view.FindByName<StackLayout>("pnlInformacion").IsVisible)
            {
                view.FindByName<StackLayout>("pnlCliente_stkGuardar").IsVisible = true;
                view.FindByName<StackLayout>("pnlCliente_stkCerrar").IsVisible = true;
            }
        }

        private void RenderMenuBottom() {

            view.FindByName<StackLayout>("pnlCliente_stkGuardar").IsVisible = false;
            view.FindByName<StackLayout>("pnlCliente_stkConsultar").IsVisible = false;
            view.FindByName<StackLayout>("pnlCliente_stkCerrar").IsVisible = false;
        }

        private void llenarComboPais()
        {
            Logica_ManagerPais _manager = new Logica_ManagerPais();

            DataTable _dt = _manager.buscarPaises();

            Util _util = new Util();

            _util.fillComboBox(
                _dt,
                view.FindByName<Picker>("pnlInformacion_cbxPais"),
                "descripcion"
                );
        }

        private void llenarComboProvincia()
        {
            Logica_ManagerProvincia _manager = new Logica_ManagerProvincia();

            DataTable _dt = _manager.buscarProvincias();

            Util _util = new Util();

            _util.fillComboBox(
                _dt,
                view.FindByName<Picker>("pnlInformacion_cbxProvincia"),
                "descripcion"
                );

            _util.fillComboBox(
                _dt,
                view.FindByName<Picker>("pnlInformacion_cbxProvinciaApo"),
                "descripcion"
                );
        }

        private void llenarComboListaPrecios()
        {
            Logica_ManagerListaPrecios _manager = new Logica_ManagerListaPrecios();

            DataTable _dt = _manager.buscarListaPrecios();

            Util _util = new Util();

            _util.fillComboBox(
                _dt,
                view.FindByName<Picker>("pnlInformacion_cbxListaPrecios"),
                "descripcion"
                );
        }

        private void llenarComboClasificacionCliente()
        {
            Logica_ManagerClasificacionCliente _manager = new Logica_ManagerClasificacionCliente();

            DataTable _dt = _manager.buscarClasificacionCliente();

            Util _util = new Util();

            _util.fillComboBox(
                _dt,
                view.FindByName<Picker>("pnlInformacion_cbxClasificacionCliente"),
                "nombre"
                );
        }

        private void renderComponents()
        {
            view.FindByName<CheckBox>("pnlConsulta_rdbFisico").Checked = true;

            view.FindByName<ExtendedEntry>("pnlInformacion_txtNomLocal").Focus();

            llenarComboPais();
            llenarComboProvincia();

            llenarComboListaPrecios();

            llenarComboClasificacionCliente();
        }

        internal async Task<bool> LoadLocation() {
            bool Validar = false;
            locator = CrossGeolocator.Current;
            Validar = locator.IsGeolocationEnabled;

            if (Validar)
            {
                Position position = await locator.GetPositionAsync();
                v_latitud = Math.Round(position.Latitude, 8);
                v_longitud = Math.Round(position.Longitude, 8);
                view.FindByName<Label>("pnlInformacion_txtLatitud").Text = v_latitud.ToString();
                view.FindByName<Label>("pnlInformacion_txtLongitud").Text = v_longitud.ToString();
            }
            else
            {
                LogMessageAttention _lm = new LogMessageAttention();
                await _lm.generalAttention("El GPS debe encontrarse activo para proceder.");
                GPS_Device Gps = new GPS_Device();
                await Gps.StartGps();
            }

            return Validar;

        }

        internal void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);
            renderPaneles(view.FindByName<StackLayout>("pnlConsulta"));
            RenderMenu();
            renderComponents();
        }

        internal void pnlInformacion_btnBorrarTelefono_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlInformacion_txtTelefono"));
        }

        internal void pnlInformacion_btnLimpiarTelefono_Click()
        {
            view.FindByName<ExtendedEntry>("pnlInformacion_txtTelefono").Text = string.Empty;
        }

        internal void pnlInformacion_btnLimpiarNL_Click()
        {
            view.FindByName<ExtendedEntry>("pnlInformacion_txtNomLocal").Text = string.Empty;
        }

        internal void pnlInformacion_btnLimpiarNA_Click()
        {
            view.FindByName<ExtendedEntry>("pnlInformacion_txtNomApo").Text = string.Empty;
        }

        internal void pnlInformacion_btnLimpiarCA_Click()
        {
            view.FindByName<ExtendedEntry>("pnlInformacion_txtCedApo").Text = string.Empty;
        }

        internal void pnlInformacion_btnLimpiarRS_Click()
        {
            view.FindByName<ExtendedEntry>("pnlInformacion_txtRazonSocial").Text = string.Empty;
        }

        internal void renderTipoDocumento(bool pfisico, bool pjuridico, bool ppasaporte, bool pdimex)
        {
            bool _fisico = false;
            bool _juridico = false;
            bool _pasaporte = false;
            bool _dimex = false;

            if (pfisico)
            {
                _fisico = true;
            }

            if (pjuridico)
            {
                _juridico = true;
            }

            if (ppasaporte)
            {
                _pasaporte = true;
            }

            if (pdimex)
            {
                _dimex = true;
            }

            view.FindByName<CheckBox>("pnlConsulta_rdbFisico").Checked = _fisico;
            view.FindByName<CheckBox>("pnlConsulta_rdbJuridico").Checked = _juridico;
            view.FindByName<CheckBox>("pnlConsulta_rdbPasaporte").Checked = _pasaporte;
            view.FindByName<CheckBox>("pnlConsulta_rdbDimex").Checked = _dimex;

            view.FindByName<ExtendedEntry>("pnlConsulta_txtFisico1").IsEnabled = _fisico;
            view.FindByName<ExtendedEntry>("pnlConsulta_txtFisico02").IsEnabled = _fisico;
            view.FindByName<ExtendedEntry>("pnlConsulta_txtFisico03").IsEnabled = _fisico;
            if (!_fisico)
            {
                view.FindByName<ExtendedEntry>("pnlConsulta_txtFisico1").Text = string.Empty;
                view.FindByName<ExtendedEntry>("pnlConsulta_txtFisico02").Text = string.Empty;
                view.FindByName<ExtendedEntry>("pnlConsulta_txtFisico03").Text = string.Empty;
            }

            view.FindByName<ExtendedEntry>("pnlConsulta_txtJuridico1").IsEnabled = _juridico;
            view.FindByName<ExtendedEntry>("pnlConsulta_txtJuridico2").IsEnabled = _juridico;
            view.FindByName<ExtendedEntry>("pnlConsulta_txtJuridico3").IsEnabled = _juridico;
            if (!_juridico)
            {
                view.FindByName<ExtendedEntry>("pnlConsulta_txtJuridico1").Text = string.Empty;
                view.FindByName<ExtendedEntry>("pnlConsulta_txtJuridico2").Text = string.Empty;
                view.FindByName<ExtendedEntry>("pnlConsulta_txtJuridico3").Text = string.Empty;
            }

            view.FindByName<ExtendedEntry>("pnlConsulta_txtPasaporte").IsEnabled = _pasaporte;
            if (!_pasaporte)
            {
                view.FindByName<ExtendedEntry>("pnlConsulta_txtPasaporte").Text = string.Empty;
            }

            view.FindByName<ExtendedEntry>("pnlConsulta_txtDimex").IsEnabled = _dimex;
            if (!_dimex)
            {
                view.FindByName<ExtendedEntry>("pnlConsulta_txtDimex").Text = string.Empty;
            }
        }

        private void ResetearColorFondoCamposTextoFormularioConsulta()
        {
            RenderPaint.paintWhiteBackgroundTextBox(view.FindByName<ExtendedEntry>("pnlConsulta_txtFisico1"));
            RenderPaint.paintWhiteBackgroundTextBox(view.FindByName<ExtendedEntry>("pnlConsulta_txtFisico02"));
            RenderPaint.paintWhiteBackgroundTextBox(view.FindByName<ExtendedEntry>("pnlConsulta_txtFisico03"));

            RenderPaint.paintWhiteBackgroundTextBox(view.FindByName<ExtendedEntry>("pnlConsulta_txtJuridico1"));
            RenderPaint.paintWhiteBackgroundTextBox(view.FindByName<ExtendedEntry>("pnlConsulta_txtJuridico2"));
            RenderPaint.paintWhiteBackgroundTextBox(view.FindByName<ExtendedEntry>("pnlConsulta_txtJuridico3"));

            RenderPaint.paintWhiteBackgroundTextBox(view.FindByName<ExtendedEntry>("pnlConsulta_txtPasaporte"));

            RenderPaint.paintWhiteBackgroundTextBox(view.FindByName<ExtendedEntry>("pnlConsulta_txtDimex"));
        }

        private async Task<bool> ValidarFormularioConsultaDocumento()
        {
            ResetearColorFondoCamposTextoFormularioConsulta();
            LogMessageAttention _logMessageAttention = new LogMessageAttention();

            bool _existeCampoVacio = false;
            bool _formatoCorrecto = false;

            ValidateHH _validateHH = new ValidateHH();

            if (view.FindByName<CheckBox>("pnlConsulta_rdbFisico").Checked)
            {
                _existeCampoVacio = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlConsulta_txtFisico1"));
                _formatoCorrecto = _validateHH.validarLength(view.FindByName<ExtendedEntry>("pnlConsulta_txtFisico1"));

                if (!_existeCampoVacio && _formatoCorrecto)
                {
                    _existeCampoVacio = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlConsulta_txtFisico02"));
                    _formatoCorrecto = _validateHH.validarLength(view.FindByName<ExtendedEntry>("pnlConsulta_txtFisico02"));
                }

                if (!_existeCampoVacio && _formatoCorrecto)
                {
                    _existeCampoVacio = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlConsulta_txtFisico03"));
                    _formatoCorrecto = _validateHH.validarLength(view.FindByName<ExtendedEntry>("pnlConsulta_txtFisico03"));
                }

                if (!_formatoCorrecto)
                {
                    _existeCampoVacio = true;
                }

            }

            if (view.FindByName<CheckBox>("pnlConsulta_rdbJuridico").Checked)
            {
                _existeCampoVacio = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlConsulta_txtJuridico1"));
                _formatoCorrecto = _validateHH.validarLength(view.FindByName<ExtendedEntry>("pnlConsulta_txtJuridico1"));

                if (!_existeCampoVacio && _formatoCorrecto)
                {
                    _existeCampoVacio = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlConsulta_txtJuridico2"));
                    _formatoCorrecto = _validateHH.validarLength(view.FindByName<ExtendedEntry>("pnlConsulta_txtJuridico2"));
                }

                if (!_existeCampoVacio && _formatoCorrecto)
                {
                    _existeCampoVacio = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlConsulta_txtJuridico3"));
                    _formatoCorrecto = _validateHH.validarLength(view.FindByName<ExtendedEntry>("pnlConsulta_txtJuridico3"));
                }

                if (!_formatoCorrecto)
                {
                    _existeCampoVacio = true;
                }
            }

            if (view.FindByName<CheckBox>("pnlConsulta_rdbPasaporte").Checked)
            {
                _existeCampoVacio = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlConsulta_txtPasaporte"));
                _formatoCorrecto = _validateHH.validarLength(view.FindByName<ExtendedEntry>("pnlConsulta_txtPasaporte"));

                if (!_formatoCorrecto)
                {
                    _existeCampoVacio = true;
                    await _logMessageAttention.generalAttention("El formato requerido es: XXXXXXXXXXXXXXXX (16 digitos)");
                }
            }

            if (view.FindByName<CheckBox>("pnlConsulta_rdbDimex").Checked)
            {
                _existeCampoVacio = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlConsulta_txtDimex"));
                _formatoCorrecto = _validateHH.validarLength(view.FindByName<ExtendedEntry>("pnlConsulta_txtDimex"));

                if (!_formatoCorrecto)
                {
                    _existeCampoVacio = true;
                    await _logMessageAttention.generalAttention("El formato requerido es: XXXXXXXXXXXX (12 digitos).");
                }
            }

            if (_existeCampoVacio)
            {
                await _logMessageAttention.generalAttention("Existen campos sin completar. Revise los formatos requeridos.");
            }

            return _existeCampoVacio;
        }

        private void ExtraerInformacionClientePadronNacional(DataTable pdt)
        {
            foreach (DataRow _fila in pdt.Rows)
            {
                v_cedula = _fila["NUM_CED"].ToString();
                v_fechaNacimiento = _fila["FEC_NAC"].ToString();
                v_primerApellido = _fila["PRI_APE"].ToString();
                v_segundoApellido = _fila["SEG_APE"].ToString();
                v_nombre = _fila["NOM"].ToString();
            }

            Logica_ManagerTipoIdentificacion _managerTipoIdentificacion = new Logica_ManagerTipoIdentificacion();

            if (view.FindByName<CheckBox>("pnlConsulta_rdbFisico").Checked)
            {
                v_codigoTipoIdentificacion = _managerTipoIdentificacion.buscarCodigoTipoIdentificacion("CEDULA");
            }

            if (view.FindByName<CheckBox>("pnlConsulta_rdbJuridico").Checked)
            {
                v_codigoTipoIdentificacion = _managerTipoIdentificacion.buscarCodigoTipoIdentificacion("JURIDICA");
            }

            if (view.FindByName<CheckBox>("pnlConsulta_rdbPasaporte").Checked)
            {
                v_codigoTipoIdentificacion = _managerTipoIdentificacion.buscarCodigoTipoIdentificacion("PASAPORTE");
            }

            if (view.FindByName<CheckBox>("pnlConsulta_rdbDimex").Checked)
            {
                v_codigoTipoIdentificacion = _managerTipoIdentificacion.buscarCodigoTipoIdentificacion("DIMEX");
            }
        }

        internal async Task menu_mniConsultar_Click()
        {
            if (await LoadLocation())
            {
                bool _validarFormulario = await ValidarFormularioConsultaDocumento();

                if (!_validarFormulario)
                {
                    string _cedula = string.Empty;

                    string _cedFormatoUsuario = string.Empty;

                    if (view.FindByName<CheckBox>("pnlConsulta_rdbFisico").Checked)
                    {
                        _cedula = view.FindByName<ExtendedEntry>("pnlConsulta_txtFisico1").Text
                                    + view.FindByName<ExtendedEntry>("pnlConsulta_txtFisico02").Text
                                    + view.FindByName<ExtendedEntry>("pnlConsulta_txtFisico03").Text;

                        _cedFormatoUsuario = view.FindByName<ExtendedEntry>("pnlConsulta_txtFisico1").Text + " " +
                                   view.FindByName<ExtendedEntry>("pnlConsulta_txtFisico02").Text + " " +
                                   view.FindByName<ExtendedEntry>("pnlConsulta_txtFisico03").Text;
                    }

                    if (view.FindByName<CheckBox>("pnlConsulta_rdbJuridico").Checked)
                    {
                        _cedula = view.FindByName<ExtendedEntry>("pnlConsulta_txtJuridico1").Text
                                    + view.FindByName<ExtendedEntry>("pnlConsulta_txtJuridico2").Text
                                    + view.FindByName<ExtendedEntry>("pnlConsulta_txtJuridico3").Text;
                    }

                    if (view.FindByName<CheckBox>("pnlConsulta_rdbPasaporte").Checked)
                    {
                        _cedula = view.FindByName<ExtendedEntry>("pnlConsulta_txtPasaporte").Text;
                    }

                    if (view.FindByName<CheckBox>("pnlConsulta_rdbDimex").Checked)
                    {
                        _cedula = view.FindByName<ExtendedEntry>("pnlConsulta_txtDimex").Text;
                    }

                    Consulta_ModeloCliente _modeloCliente = new Consulta_ModeloCliente();

                    bool _condition = _modeloCliente.ExisteCliente(_cedula);

                    bool encontrado = true;

                    var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

                    LogMessageAttention _lma = new LogMessageAttention();

                    if (!_condition)
                    {
                        if (await _testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
                        {
                            _condition = _modeloCliente.ExisteClienteEnNAF(_cedula);

                            if (_condition)
                            {
                                await _lma.generalAttention("El documento de identificación se encuentra registrado en la compañía."
                                    + Environment.NewLine
                                    + Environment.NewLine
                                    + "Comuníquese con el Departamento de Liquidaciones");
                            }
                            else
                            {


                                if (view.FindByName<CheckBox>("pnlConsulta_rdbFisico").Checked)
                                {
                                    var ServicioConsulta = DependencyService.Get<IService_WebServiceConsultation>();
                                    string _memoryStream = ServicioConsulta.Get_consultaClientePadronNacional(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), _cedula);

                                    var _memoryCompress = DependencyService.Get<IMC_HH>();

                                    DataTable _dt = _memoryCompress.Unzip_HH_DataTable("pnlConsulta_btnConsulta_Click", TypeTransaction._select, _memoryStream, TablesROL._cliente);

                                    if (_dt.Rows.Count > 0)
                                    {
                                        ExtraerInformacionClientePadronNacional(_dt);

                                        await _lma.generalAttention("Se obtuvo la informacion del nuevo cliente:"
                                            + Environment.NewLine
                                            + Environment.NewLine
                                            + v_cedula
                                            + Environment.NewLine
                                            + v_nombre
                                            + Environment.NewLine
                                            + v_primerApellido
                                            + Environment.NewLine
                                            + v_segundoApellido
                                            );
                                    }
                                    else
                                    {
                                        encontrado = false;
                                        await _lma.generalAttention("El número de documento no se encuentra en el Padrón Nacional de la empresa");
                                        await _lma.generalAttention("Verifique que la cédula ingresada \"" + _cedFormatoUsuario + "\" sea correcta y que cumpla con el formato establecido XX XXXX XXXX");

                                    }
                                }
                                else
                                {
                                    v_cedula = _cedula;
                                    v_fechaNacimiento = string.Empty;
                                    v_primerApellido = string.Empty;
                                    v_segundoApellido = string.Empty;
                                    v_nombre = string.Empty;
                                }

                                if (encontrado)
                                {
                                    if (await LogMessages._dialogResultYes("¿Desea ingresar los datos para el nuevo cliente\n" + "ced: " + _cedFormatoUsuario + "?", "Agregar Nuevo Cliente"))
                                    {
                                        renderPaneles(view.FindByName<StackLayout>("pnlInformacion"));

                                        RenderMenu();

                                        view.FindByName<Label>("pnlInformacion_lblCedulaInfo").Text = v_cedula;
                                        view.FindByName<ExtendedEntry>("pnlInformacion_txtNombre").Text = v_nombre;
                                        view.FindByName<ExtendedEntry>("pnlInformacion_txt1erApellido").Text = v_primerApellido;
                                        view.FindByName<ExtendedEntry>("pnlInformacion_txt2doApellido").Text = v_segundoApellido;

                                        view.FindByName<ExtendedEntry>("pnlInformacion_txtNomLocal").Text = v_nombre + " " + v_primerApellido + " " + v_segundoApellido;
                                        view.FindByName<ExtendedEntry>("pnlInformacion_txtRazonSocial").Text = v_nombre + " " + v_primerApellido + " " + v_segundoApellido;

                                        if (view.FindByName<CheckBox>("pnlConsulta_rdbFisico").Checked)
                                        {
                                            view.FindByName<ExtendedEntry>("pnlInformacion_txtNombre").IsEnabled = !true;
                                            view.FindByName<ExtendedEntry>("pnlInformacion_txt1erApellido").IsEnabled = !true;
                                            view.FindByName<ExtendedEntry>("pnlInformacion_txt2doApellido").IsEnabled = !true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        await _lma.generalAttention("El Cliente se encuentra en la lista de clientes del Sistema ROL");
                    }
                }
            }
        }

        internal void menu_mniClose_Click()
        {
            Application.Current.MainPage.Navigation.PopModalAsync();
        }

        private void llenarComboCanton(string pcodProvincia, Picker Canton)
        {
            Logica_ManagerCanton _manager = new Logica_ManagerCanton();

            DataTable _dt = _manager.buscarCantones(pcodProvincia);

            Util _util = new Util();

            _util.fillComboBox(
                _dt,
                Canton,
                "descripcion"
                );
        }

        internal void pnlInformacion_cbxProvincia_SelectedIndexChanged()
        {
            string _nomProvincia = view.FindByName<Picker>("pnlInformacion_cbxProvincia").SelectedItem.ToString();

            Logica_ManagerProvincia _manager = new Logica_ManagerProvincia();

            string _codProvincia = _manager.obtenerCodigoProvincia(_nomProvincia);

            llenarComboCanton(_codProvincia, view.FindByName<Picker>("pnlInformacion_cbxCanton"));
        }

        internal void pnlInformacion_cbxProvinciaApo_SelectedIndexChanged()
        {
            string _nomProvincia = view.FindByName<Picker>("pnlInformacion_cbxProvinciaApo").SelectedItem.ToString();

            Logica_ManagerProvincia _manager = new Logica_ManagerProvincia();

            string _codProvincia = _manager.obtenerCodigoProvincia(_nomProvincia);

            llenarComboCanton(_codProvincia, view.FindByName<Picker>("pnlInformacion_cbxCantonApo"));
        }

        private void llenarComboDistrito(string pcodProvincia, string pcodCanton, Picker Distrito)
        {
            Logica_ManagerDistrito _manager = new Logica_ManagerDistrito();

            DataTable _dt = _manager.buscarDistritos(pcodProvincia, pcodCanton);

            Util _util = new Util();

            _util.fillComboBox(
                _dt,
                Distrito,
                "descripcion"
                );
        }

        internal void pnlInformacion_cbxCanton_SelectedIndexChanged()
        {
            string _nomProvincia = view.FindByName<Picker>("pnlInformacion_cbxProvincia").SelectedItem.ToString();

            Logica_ManagerProvincia _manager = new Logica_ManagerProvincia();

            string _codProvincia = _manager.obtenerCodigoProvincia(_nomProvincia);

            if (view.FindByName<Picker>("pnlInformacion_cbxCanton").SelectedItem != null)
            {
                string _nomCanton = view.FindByName<Picker>("pnlInformacion_cbxCanton").SelectedItem.ToString();

                Logica_ManagerCanton _managerCanton = new Logica_ManagerCanton();
                string _codCanton = _managerCanton.obtenerCodigoCanton(_codProvincia, _nomCanton);

                llenarComboDistrito(_codProvincia, _codCanton, view.FindByName<Picker>("pnlInformacion_cbxDistrito"));
            }
        }

        internal void pnlInformacion_cbxCantonApo_SelectedIndexChanged()
        {
            string _nomProvincia = view.FindByName<Picker>("pnlInformacion_cbxProvinciaApo").SelectedItem.ToString();

            Logica_ManagerProvincia _manager = new Logica_ManagerProvincia();

            string _codProvincia = _manager.obtenerCodigoProvincia(_nomProvincia);

            if (view.FindByName<Picker>("pnlInformacion_cbxCantonApo").SelectedItem != null)
            {
                string _nomCanton = view.FindByName<Picker>("pnlInformacion_cbxCantonApo").SelectedItem.ToString();

                Logica_ManagerCanton _managerCanton = new Logica_ManagerCanton();
                string _codCanton = _managerCanton.obtenerCodigoCanton(_codProvincia, _nomCanton);

                llenarComboDistrito(_codProvincia, _codCanton, view.FindByName<Picker>("pnlInformacion_cbxDistritoApo"));
            }
        }

        private async  Task<bool> validarCamposVacios()
        {
            bool _existeCampoVacio = false;

            ValidateHH _validateHH = new ValidateHH();

            _existeCampoVacio = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlInformacion_txtNomLocal"));

            if (!_existeCampoVacio)
            {
                _existeCampoVacio = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlInformacion_txtRazonSocial"));
            }

            if (!_existeCampoVacio)
            {
                _existeCampoVacio = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlInformacion_txtNombre"));
            }

            if (!_existeCampoVacio)
            {
                _existeCampoVacio = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlInformacion_txt1erApellido"));
            }

            if (!_existeCampoVacio)
            {
                _existeCampoVacio = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlInformacion_txt2doApellido"));
            }

            if (!_existeCampoVacio)
            {
                _existeCampoVacio = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlInformacion_txtDireccion"));
            }

            if (!_existeCampoVacio)
            {
                _existeCampoVacio = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlInformacion_txtTelefono"));
            }

            if (!_existeCampoVacio)
            {
                _existeCampoVacio = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlInformacion_txtNomApo"));
            }

            if (!_existeCampoVacio)
            {
                _existeCampoVacio = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlInformacion_txtCedApo"));
            }

            if (!_existeCampoVacio)
            {
                _existeCampoVacio = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlInformacion_txtDireccionApo"));
            }

            if (!_existeCampoVacio)
            {
                _existeCampoVacio = _validateHH.emptyEditorBox(view.FindByName<ExtendedEditor>("pnlInformacion_txtObservaciones"));
            }

            if (!_existeCampoVacio)
            {
                _existeCampoVacio = string.IsNullOrEmpty(ObtenerDiasAtencion()) ? true : false;

                if (_existeCampoVacio)
                {
                    LogMessageAttention _logMessageAttention = new LogMessageAttention();
                    await _logMessageAttention.generalAttention("No ha seleccionado ningún día de atención.");
                }

            }

            return _existeCampoVacio;
        }

        private async Task<bool> validarFormulario()
        {
            bool _formularioCorrecto = false;

            _formularioCorrecto = await validarCamposVacios();

            if (!_formularioCorrecto)
            {
                _formularioCorrecto = true;
            }
            else
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention("Existen campos sin completar");

                _formularioCorrecto = false;
            }

            return _formularioCorrecto;
        }

        private void establecerValoresDireccion(Cliente pobjCliente)
        {
            Logica_ManagerPais _managerPais = new Logica_ManagerPais();
            pobjCliente.v_objDireccion.v_pais = _managerPais.obtenerCodigoPais(view.FindByName<Picker>("pnlInformacion_cbxPais").SelectedItem.ToString());

            Logica_ManagerProvincia _managerProvincia = new Logica_ManagerProvincia();
            string _codProvincia = _managerProvincia.obtenerCodigoProvincia(view.FindByName<Picker>("pnlInformacion_cbxProvincia").SelectedItem.ToString());
            pobjCliente.v_objDireccion.v_provincia = _codProvincia;

            string _codProvinciaApo = _managerProvincia.obtenerCodigoProvincia(view.FindByName<Picker>("pnlInformacion_cbxProvinciaApo").SelectedItem.ToString());
            pobjCliente.v_objDireccion.v_provinciaApo = _codProvinciaApo;

            Logica_ManagerCanton _managerCanton = new Logica_ManagerCanton();
            string _codCanton = _managerCanton.obtenerCodigoCanton(
                                    _codProvincia,
                                    view.FindByName<Picker>("pnlInformacion_cbxCanton").SelectedItem.ToString()
                                    );

            pobjCliente.v_objDireccion.v_canton = _codCanton;

            string _codCantonApo = _managerCanton.obtenerCodigoCanton(
                                    _codProvinciaApo,
                                    view.FindByName<Picker>("pnlInformacion_cbxCantonApo").SelectedItem.ToString()
                                    );

            pobjCliente.v_objDireccion.v_cantonApo = _codCantonApo;


            Logica_ManagerDistrito _managerDistrito = new Logica_ManagerDistrito();
            pobjCliente.v_objDireccion.v_distrito = _managerDistrito.buscarCodigoDistrito(
                                                        _codProvincia,
                                                        _codCanton,
                                                        view.FindByName<Picker>("pnlInformacion_cbxDistrito").SelectedItem.ToString()
                                                        );

            pobjCliente.v_objDireccion.v_distritoApo = _managerDistrito.buscarCodigoDistrito(
                                                        _codProvinciaApo,
                                                        _codCantonApo,
                                                        view.FindByName<Picker>("pnlInformacion_cbxDistritoApo").SelectedItem.ToString()
                                                        );

            pobjCliente.v_objDireccion.v_direccion = view.FindByName<ExtendedEntry>("pnlInformacion_txtDireccion").Text;

            pobjCliente.v_objDireccion.v_direccionApo = view.FindByName<ExtendedEntry>("pnlInformacion_txtDireccionApo").Text;
        }

        private void establecerValoresEstablecimiento(Cliente pobjCliente)
        {
            Establecimiento _objEstablecimiento = new Establecimiento();

            pobjCliente.v_objEstablecimiento.v_codEstablecimiento = Agent._codigoEstablecimientoRuteros;

            pobjCliente.v_objEstablecimiento.v_codLocalizacion =
                pobjCliente.v_objDireccion.v_pais
                + pobjCliente.v_objDireccion.v_provincia
                + pobjCliente.v_objDireccion.v_canton;

            pobjCliente.v_objEstablecimiento.v_descEstablecimiento = view.FindByName<ExtendedEntry>("pnlInformacion_txtNomLocal").Text;

            pobjCliente.v_objEstablecimiento.v_direccion = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent;

            pobjCliente.v_objEstablecimiento.v_direccionExacta = view.FindByName<ExtendedEntry>("pnlInformacion_txtDireccion").Text;
        }

        private void establecerValoresCliente(Cliente pobjCliente)
        {
            Logica_ManagerAgenteVendedor _managerAgenteVendedor = new Logica_ManagerAgenteVendedor();

            pobjCliente.v_no_cliente = _managerAgenteVendedor.buscarConsecutivoCliente();

            pobjCliente.v_codClienteGenerico = Agent.getCodClienteGenerico();

            pobjCliente.v_no_cia = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany;


            pobjCliente.v_nombre = view.FindByName<ExtendedEntry>("pnlInformacion_txtNomLocal").Text;

            pobjCliente.v_nombre_largo = view.FindByName<ExtendedEntry>("pnlInformacion_txtRazonSocial").Text;

            string _nomEncargado =
                view.FindByName<ExtendedEntry>("pnlInformacion_txt1erApellido").Text
                + " "
                + view.FindByName<ExtendedEntry>("pnlInformacion_txt2doApellido").Text
                + " "
                + view.FindByName<ExtendedEntry>("pnlInformacion_txtNombre").Text;

            pobjCliente.v_nombre_enc = _nomEncargado;

            pobjCliente.v_telefono = view.FindByName<ExtendedEntry>("pnlInformacion_txtTelefono").Text;

            pobjCliente.v_tipoCedula = v_codigoTipoIdentificacion;

            pobjCliente.v_cedula = v_cedula;

            Logica_ManagerListaPrecios _managerListaPrecios = new Logica_ManagerListaPrecios();

            pobjCliente.v_lista_precios = _managerListaPrecios.buscarCodigoListaPrecios(view.FindByName<Picker>("pnlInformacion_cbxListaPrecios").SelectedItem.ToString());

            Logica_ManagerClasificacionCliente _managerClasificacionCliente = new Logica_ManagerClasificacionCliente();
            pobjCliente.v_clasificacion = _managerClasificacionCliente.buscarCodigoClasificacion(
                                                view.FindByName<Picker>("pnlInformacion_cbxClasificacionCliente").SelectedItem.ToString()
                                                );

            establecerValoresDireccion(pobjCliente);

            establecerValoresEstablecimiento(pobjCliente);

            pobjCliente.v_email = view.FindByName<ExtendedEntry>("pnlInformacion_txtEmail").Text;

            pobjCliente.v_latitud = v_latitud;
            pobjCliente.v_longitud = v_longitud;

            pobjCliente.v_nombre_apo = view.FindByName<ExtendedEntry>("pnlInformacion_txtNomApo").Text;
            pobjCliente.v_cedula_apo = view.FindByName<ExtendedEntry>("pnlInformacion_txtCedApo").Text;

            pobjCliente.v_observaciones = view.FindByName<ExtendedEditor>("pnlInformacion_txtObservaciones").Text;

            pobjCliente.v_dias_atencion = ObtenerDiasAtencion();
        }

        private string ObtenerDiasAtencion()
        {
            string dias = string.Empty;

            if (view.FindByName<CheckBox>("pnlInformacion_cbxDomingo").Checked)
            {
                dias = "Domingo";
            }

            if (view.FindByName<CheckBox>("pnlInformacion_cbxLunes").Checked)
            {
                dias += ",Lunes";
            }

            if (view.FindByName<CheckBox>("pnlInformacion_cbxMartes").Checked)
            {
                dias += ",Martes";
            }

            if (view.FindByName<CheckBox>("pnlInformacion_cbxMiercoles").Checked)
            {
                dias += ",Miércoles";
            }

            if (view.FindByName<CheckBox>("pnlInformacion_cbxJueves").Checked)
            {
                dias += ",Jueves";
            }

            if (view.FindByName<CheckBox>("pnlInformacion_cbxViernes").Checked)
            {
                dias += ",Viernes";
            }

            if (view.FindByName<CheckBox>("pnlInformacion_cbxSabado").Checked)
            {
                dias += ",Sábado";
            }

            return dias;
        }

        internal void SeleccionarDias()
        {
            var seleccionado = view.FindByName<CheckBox>("pnlInformacion_cbxTodos").Checked;

            view.FindByName<CheckBox>("pnlInformacion_cbxDomingo").Checked = seleccionado;
            view.FindByName<CheckBox>("pnlInformacion_cbxLunes").Checked = seleccionado;
            view.FindByName<CheckBox>("pnlInformacion_cbxMartes").Checked = seleccionado;
            view.FindByName<CheckBox>("pnlInformacion_cbxMiercoles").Checked = seleccionado;
            view.FindByName<CheckBox>("pnlInformacion_cbxJueves").Checked = seleccionado;
            view.FindByName<CheckBox>("pnlInformacion_cbxViernes").Checked = seleccionado;
            view.FindByName<CheckBox>("pnlInformacion_cbxSabado").Checked = seleccionado;
        }

        internal async Task menu_mniGuardar_Click()
        {
            if (await LoadLocation())
            {
                if (await validarFormulario())
                {
                    if (await LogMessages._dialogResultYes("¿Es la información introducida correcta?", "Agregar Nuevo Cliente"))
                    {
                        var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                        MultiGeneric.BeginTransaction();

                        Cliente _objCliente = new Cliente();

                        establecerValoresCliente(_objCliente);

                        Logica_ManagerCliente _managerCliente = new Logica_ManagerCliente();

                        _managerCliente.nuevoCliente(_objCliente);

                        Logica_ManagerEstablecimiento _managerEstablecimiento = new Logica_ManagerEstablecimiento();

                        _managerEstablecimiento.nuevoEstablecimiento(_objCliente);

                        Logica_ManagerIndicador _managerIndicador = new Logica_ManagerIndicador();

                        _managerIndicador.nuevoIndicador(_objCliente);

                        Logica_ManagerAgenteVendedor _managerAgenteVendedor = new Logica_ManagerAgenteVendedor();

                        _managerAgenteVendedor.aumentarConsecutivoNuevoCliente();

                        MultiGeneric.Commit();

                        await LogMessageSuccess._generalSuccess("Se creó el Cliente."
                            + Environment.NewLine
                            + Environment.NewLine
                            + "   Código: "
                            + _objCliente.v_no_cliente);

                        menu_mniClose_Click();
                    }
                    else
                    {
                        LogMessageAttention _logMessageAttention = new LogMessageAttention();
                        await _logMessageAttention.generalAttention("Verifique la información");
                    }
                }
            }

        }
    }
}
