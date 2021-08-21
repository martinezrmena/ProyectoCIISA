using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RadioTelefonico.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RadioTelefonico.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RadioTelefonico.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RadioTelefonico.VistaControlador;
using CIISA.RetailOnLine.Framework.External;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.VistaControlador;
using System.Collections.ObjectModel;
using System.Data;
using Xamarin.Forms;
using XLabs.Forms.Controls;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Character;
using System.Threading.Tasks;
using Plugin.Messaging;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RadioTelefonico.Controlador
{
    public class ctrlTelefono
    {
        private vistaTelefono view { get; set; }

        internal ctrlTelefono(vistaTelefono pview)
        {
            view = pview;
        }
        
        public void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlTelefono").Id))
            {
                view.Title = "Teléfono";
            }

            ppanel.IsVisible = true;
        }

        private void llenarComboVPN()
        {
            RadioTelefonico_ManagerInformacionRuta _manager = new RadioTelefonico_ManagerInformacionRuta();

            DataTable _dt = _manager.buscarInformacionRuta();

            Util _util = new Util();

            _util.fillComboBox(
                _dt,
                view.FindByName<Picker>("pnlTelefono_cbxVPN"),
                "no_agente"
                );
        }

        internal void cargarConfiguracionSistema(bool pavanzado)
        {
            var Info = DependencyService.Get<IServicio_Phone>();

            view.FindByName<ListView>("pnlTelefono_ltvConfiguracion").ItemsSource = new ObservableCollection<pnlTelefono_ltvConfiguracion>();

            ObservableCollection<pnlTelefono_ltvConfiguracion> Source = new ObservableCollection<pnlTelefono_ltvConfiguracion>();

            pnlTelefono_ltvConfiguracion _lvi01 = new pnlTelefono_ltvConfiguracion();
            _lvi01.Configuracion = "Tipo Sistema";

            //uint _tipoSistema = v_objTapi.TAPI_GetSystemType();

            //if (_tipoSistema == (uint)TapiPhoneRadio.LINESYSTEMTYPE.LINESYSTEMTYPE_IDEN)
            //{
            //    _lvi01.SubItems.Add("iDEN");
            //}

            //else if (((_tipoSistema & ((uint)TapiPhoneRadio.LINESYSTEMTYPE.LINESYSTEMTYPE_IS95A)) != 0)
            //             || ((_tipoSistema & ((uint)TapiPhoneRadio.LINESYSTEMTYPE.LINESYSTEMTYPE_IS95B)) != 0)
            //             || ((_tipoSistema & ((uint)TapiPhoneRadio.LINESYSTEMTYPE.LINESYSTEMTYPE_1XRTTPACKET)) != 0)
            //             || ((_tipoSistema & ((uint)TapiPhoneRadio.LINESYSTEMTYPE.LINESYSTEMTYPE_1XEVDOPACKET)) != 0)
            //             || ((_tipoSistema & ((uint)TapiPhoneRadio.LINESYSTEMTYPE.LINESYSTEMTYPE_1XEVDVPACKET)) != 0))
            //{
            //    _lvi01.SubItems.Add("CDMA");
            //}

            //else if (((_tipoSistema & ((uint)TapiPhoneRadio.LINESYSTEMTYPE.LINESYSTEMTYPE_GSM)) != 0)
            //            || ((_tipoSistema & ((uint)TapiPhoneRadio.LINESYSTEMTYPE.LINESYSTEMTYPE_GPRS)) != 0)
            //            || ((_tipoSistema & ((uint)TapiPhoneRadio.LINESYSTEMTYPE.LINESYSTEMTYPE_EDGE)) != 0)
            //            || ((_tipoSistema & ((uint)TapiPhoneRadio.LINESYSTEMTYPE.LINESYSTEMTYPE_UMTS)) != 0)
            //            || ((_tipoSistema & ((uint)TapiPhoneRadio.LINESYSTEMTYPE.LINESYSTEMTYPE_HSDPA)) != 0))
            //{
            //    _lvi01.SubItems.Add("GSM");
            //}

            //else
            //{
            //    _lvi01.SubItems.Add("Unknown!");
            //}
            
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
                if(Numero <= 74)
                {
                    _lvi02.TextColor = Color.Red;
                }
                if(Numero >= 75)
                {
                    _lvi02.TextColor = Color.Blue;
                }
            }
            else
            {
                _lvi02.TextColor = Color.Red;
            }

            //if (view.FindByName<CheckBox>("pnlTelefono_chkRadioTelefonico").Checked)
            //{
            //    _lvi02.Valor = _lvi02.Valor + " %";
            //}
            //else
            //{
            //    _lvi02.Valor = "Apagado";
            //}

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

            //ListViewItem _lvi09;
            pnlTelefono_ltvConfiguracion _lvi09 = new pnlTelefono_ltvConfiguracion();
            _lvi09.Configuracion = "SIM ID";
            _lvi09.Valor = Info.GetSIMID();
            //if (_tipoSistema == (uint)TapiPhoneRadio.LINESYSTEMTYPE.LINESYSTEMTYPE_IDEN)
            //{
            //    _lvi09 = new ListViewItem("SIM ID");
            //}
            //else
            //{
            //    _lvi09 = new ListViewItem("IMSI");
            //    _lvi09.SubItems.Add(v_objTapi.lineGeneralInfo.IMSIorSIMId);
            //}
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

            view.FindByName<ListView>("pnlTelefono_ltvConfiguracion").ItemsSource = Source;
        }

        internal void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlTelefono"));

            llenarComboVPN();

            //v_objTapi = new TapiPhoneRadio();
            //v_objCommon = new CommonPhoneRadio();
            //v_objConn = new ConnPhoneRadio();
            //v_objLogica = new LogicPhoneRadio();
            //v_objTapi.tapiMessageEvent += new EventHandler(v_objLogica.activateTapiMessageEvent);
            //v_objTapi.TAPI_Open();

            cargarConfiguracionSistema(false);
            //view.temporizador.Enabled = true;
            //v_objLogica.renderCheckBoxes(view.pnlTelefono_chkRadioTelefonico, v_objTapi, v_objConn);

            renderBotonesCheck();

            //view.pnlTelefono_ltvConfiguracion.Focus();

            view.Title = "Teléfono";
        }

        internal void pnlTelefono_btnLimpiarBuscar_Click()
        {
            view.FindByName<ExtendedEntry>("pnlTelefono_txtNumeroTelefonico").Text = string.Empty;
        }

        internal void pnlTelefono_btnBorrarBuscar_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlTelefono_txtNumeroTelefonico"));
        }

        internal void menu_mniClose_Click()
        {
            //view.FindByName<ExtendedEntry>("pnlTelefono_ltbEstadoConexion").Items.Clear();

            //view.temporizador.Enabled = false;

            Application.Current.MainPage.Navigation.PopAsync();
        }

        internal void menu_mniCallCenter_Click()
        {
            view.FindByName<ExtendedEntry>("pnlTelefono_txtNumeroTelefonico").Text = NumeroTelefonicos.callCenter;
        }

        internal void menu_mniCentralTelefonica_Click()
        {
            view.FindByName<ExtendedEntry>("pnlTelefono_txtNumeroTelefonico").Text = NumeroTelefonicos.centralTelefonica;
        }

        internal void pnlTelefono_btnVPNLiquidaciones_Click()
        {
            view.FindByName<ExtendedEntry>("pnlTelefono_txtNumeroTelefonico").Text = VPNMovistar.liquidacionesVPN;
            view.FindByName<Label>("pnlTelefono_lblNombreCliente").Text = VPNMovistar.liquidacionesNombre;
        }

        internal void pnlTelefono_btnVPNGerson_Click()
        {
            view.FindByName<ExtendedEntry>("pnlTelefono_txtNumeroTelefonico").Text = VPNMovistar.gersonGonzalezVPN;
            view.FindByName<Label>("pnlTelefono_lblNombreCliente").Text = VPNMovistar.gersonGonzalezNombre;
        }

        internal void pnlTelefono_cbxVPN_SelectedIndexChanged()
        {
            view.FindByName<ExtendedEntry>("pnlTelefono_txtNumeroTelefonico").Text = view.FindByName<Picker>("pnlTelefono_cbxVPN").SelectedItem.ToString();

            RadioTelefonico_ManagerInformacionRuta _manager = new RadioTelefonico_ManagerInformacionRuta();

            view.FindByName<Label>("pnlTelefono_lblNombreCliente").Text = _manager.buscarNombreAgente(
                                                    view.FindByName<Picker>("pnlTelefono_cbxVPN").SelectedItem.ToString()
                                                    );
        }        

        private void renderBotonesCheck()
        {
            //view.FindByName<ToolbarItem>("menu_mniClose").IsEnabled = true;
            view.FindByName<StackLayout>("pnlTelefono_stkCerrar").IsVisible = true;
        }

        private void renderBotones(bool pbloquear)
        {
            view.FindByName<CheckBox>("pnlTelefono_chkRadioTelefonico").IsEnabled = pbloquear;

            view.FindByName<ToolbarItem>("menu_mniClose").IsEnabled = pbloquear;
        }

        public Cliente _objCliente = null;

        internal async Task pnlTelefono_btnBuscar_Click()
        {
            view.FindByName<ExtendedEntry>("pnlTelefono_txtNumeroTelefonico").Text = string.Empty;            

            ShowPresentacion _show = new ShowPresentacion();

            await _show.mostrarPantallaCliente(view,PantallasSistema._pantallaTelefono);            
        }

        public async Task CargarDatosCliente()
        {
            if (_objCliente == null)
            {
            }
            else
            {
                if (_objCliente.v_telefono.Equals(string.Empty))
                {
                }
                else
                {
                    ValidateHH _validateHH = new ValidateHH();

                    view.FindByName<ExtendedEntry>("pnlTelefono_txtNumeroTelefonico").Text = await _validateHH.validarSoloNumerosREGEX(_objCliente.v_telefono);
                    view.FindByName<Label>("pnlTelefono_lblNombreCliente").Text = _objCliente.v_no_cliente + Simbol._hyphenWithSpaces + _objCliente.v_nombre;
                }
            }
        }

        internal void pnlTelefono_bntLlamar_Click()
        {
            if (view.FindByName<ExtendedEntry>("pnlTelefono_txtNumeroTelefonico").Text == null)
            {
                view.FindByName<ExtendedEntry>("pnlTelefono_txtNumeroTelefonico").Text = string.Empty;
            }
            else {
                view.FindByName<ExtendedEntry>("pnlTelefono_txtNumeroTelefonico").Text = view.FindByName<ExtendedEntry>("pnlTelefono_txtNumeroTelefonico").Text.Replace(" ", "");
            }

            string Numero = view.FindByName<ExtendedEntry>("pnlTelefono_txtNumeroTelefonico").Text;
            if (Numero.Length > 0)
            {
                // Make Phone Call
                var phoneDialer = CrossMessaging.Current.PhoneDialer;
                if (phoneDialer.CanMakePhoneCall)
                    phoneDialer.MakePhoneCall(Numero);

                view.FindByName<ExtendedEntry>("pnlTelefono_txtNumeroTelefonico").Text = string.Empty;
            }

            //uint uRet = 0;

            //view.pnlTelefono_ltbEstadoConexion.Items.Clear();

            //if (v_objTapi.TAPI_IsRadioEnabled() == false)
            //{
            //    MessageBox.Show(Resources.radioMustBeEnabled);
            //}
            //else
            //{
            //    if (view.pnlTelefono_txtNumeroTelefonico.Text.Length > 0)
            //    {
            //        view.pnlTelefono_ltbEstadoConexion.Items.Add(Resources.dialing);

            //        //Application.DoEvents();

            //        uRet = v_objTapi.TAPI_MakeCall(view.pnlTelefono_txtNumeroTelefonico.Text);

            //        if (uRet == (uint)TapiPhoneRadio.LINECALLRETURN.LINEERR_OK)
            //        {
            //            view.pnlTelefono_bntLlamar.Enabled = false;
            //            view.pnlTelefono_btnColgar.Enabled = true;
            //        }
            //    }
            //}
        }
    }
}
