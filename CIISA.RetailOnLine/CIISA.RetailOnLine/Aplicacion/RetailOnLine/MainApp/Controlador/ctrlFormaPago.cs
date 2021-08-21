using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionDocumento;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita.Guardar;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using CIISA.RetailOnLine.Framework.Handheld.Display.IMainLockScreen;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    internal class ctrlFormaPago
    {
        private vistaFormaPago view { get; set; }
        private string v_pantalla = string.Empty;
        internal Cliente v_objCliente = null;
        private string v_motivoRecaudacion = string.Empty;
        internal bool v_guardar = false;
        private bool v_atomicidadTransaccional = false;
        private bool Cerrando = false;
        private vistaVisita Visita_view { get; set; }

        private LogicaVisitaGuardar v_logicaVisitaGuardar = null;

        private LogicaCarniceriaGuardar v_logicaCarniceriaGuardar = null;

        internal ctrlFormaPago(vistaFormaPago pview)
        {
            view = pview;
        }

        public ctrlFormaPago(vistaFormaPago pview, vistaVisita v_view)
        {
            view = pview;
            Visita_view = v_view;
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlFormaPago").Id))
            {
                view.Title = "Forma Pago";
            }

            ppanel.IsVisible = true;
        }

        private void pintarLabelTotalRestante()
        {
            if (v_objCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._recaudacionSigla))
            {
                view.FindByName<Label>("pnlFormaPago_lblTotal").Text = "Total:";
            }
            else
            {
                view.FindByName<Label>("pnlFormaPago_lblTotal").Text = "Saldo:";
            }
        }

        private void actualizarNumLineasPagos()
        {
            MiscUtils _miscUtils = new MiscUtils();
            _miscUtils.quantityListViewItems<pnlFormaPago_ltvPagos>(view.FindByName<ListView>("pnlFormaPago_ltvPagos"), view.FindByName<Label>("pnlFormaPago_clhFormaPago"), "Forma Pago");
        }

        private void actualizarColumnasMontoTotalPagos()
        {
            Util _util = new Util();

            _util.sumarItemsColumnaLista<pnlFormaPago_ltvPagos>(
                view.FindByName<ListView>("pnlFormaPago_ltvPagos"),
                view.FindByName<Label>("pnlFormaPago_clhMonto"),
                1,
                "Monto"
                );
        }

        private void actualizarTotalPorPagar()
        {
            Util _util = new Util();

            decimal _totalPagos = _util.sumarItemsColumnaLista<pnlFormaPago_ltvPagos>(view.FindByName<ListView>("pnlFormaPago_ltvPagos"),1);

            if (v_objCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._recaudacionSigla))
            {
                view.FindByName<ExtendedEntry>("pnlFormaPago_txtMontoPago").Text = Numeric._zeroDecimal;

                view.FindByName<Label>("pnlFormaPago_lblTotalPago").Text = FormatUtil.applyCurrencyFormat(_totalPagos);
            }
            else
            {
                decimal _totalPorPagar = v_objCliente.v_objTransaccion.v_total - _totalPagos;

                if (_totalPorPagar < 1)
                {
                    view.FindByName<Label>("pnlFormaPago_lblTotalPago").Text = Numeric._zeroDecimal;
                }
                else
                {
                    view.FindByName<ExtendedEntry>("pnlFormaPago_txtMontoPago").Text = FormatUtil.applyCurrencyFormat(_totalPorPagar);

                    view.FindByName<Label>("pnlFormaPago_lblTotalPago").Text = FormatUtil.applyCurrencyFormat(_totalPorPagar);
                }
            }
        }

        private void llenarComboBoxBancos()
        {
            Logica_ManagerBanco _manager = new Logica_ManagerBanco();

            DataTable _dt = _manager.buscarBanco();

            Util _util = new Util();

            _util.fillComboBox(
                _dt,
                view.FindByName<Picker>("pnlFormaPago_cbxBanco"),
                "descripcion"
                );
        }

        private void llenarComboBoxTipoPago()
        {
            Logica_ManagerFormaPago _manager = new Logica_ManagerFormaPago();

            DataTable _dt = _manager.buscarFormaPago();

            Util _util = new Util();

            _util.fillComboBox(
                _dt,
                view.FindByName<Picker>("pnlFormaPago_cbxFormaPago"),
                "Descripcion"
                );

            if (!v_objCliente.v_objEstablecimiento.v_objIndicador.v_condicionCheque)
            {
                _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlFormaPago_cbxFormaPago"),
                    PaymentForm._check
                    );
            }
        }

        private void pintarComponentesPnlTipoPago(bool pvisible, bool pserieVisible)
        {
            view.FindByName<Label>("pnlFormaPago_lblBanco").IsVisible = pvisible;
            view.FindByName<Picker>("pnlFormaPago_cbxBanco").IsVisible = pvisible;
            view.FindByName<Label>("pnlFormaPago_lblNumTransaccion").IsVisible = pvisible;
            view.FindByName<ExtendedEntry>("pnlFormaPago_txtNumTransaccion").IsVisible = pvisible;
            view.FindByName<Label>("pnlFormaPago_lblCuentaDeposito").IsVisible = pvisible;
            view.FindByName<Label>("pnlFormaPago_lblSerie").IsVisible = pserieVisible;
            view.FindByName<ExtendedEntry>("pnlFormaPago_txtSerie").IsVisible = pserieVisible;

            view.FindByName<Button>("pnlFormaPago_btnBorrarNumero").IsVisible = pvisible;
            view.FindByName<Button>("pnlFormaPago_btnLimpiarNumero").IsVisible = pvisible;
            view.FindByName<Button>("pnlFormaPago_btnGuionNumero").IsVisible = pvisible;

            view.FindByName<Button>("pnlFormaPago_btnBorrarSerie").IsVisible = pserieVisible;
            view.FindByName<Button>("pnlFormaPago_btnLimpiarSerie").IsVisible = pserieVisible;
            view.FindByName<Button>("pnlFormaPago_btnGuionSerie").IsVisible = pserieVisible;
        }

        internal void pnlFormaPago_cbxFormaPago_SelectedIndexChanged()
        {
            switch (view.FindByName<Picker>("pnlFormaPago_cbxFormaPago").SelectedIndex)
            {
                case 0:
                    pintarComponentesPnlTipoPago(false, false);
                    break;
                case 1:
                    pintarComponentesPnlTipoPago(true, true);
                    break;
                case 2:
                    pintarComponentesPnlTipoPago(true, false);
                    break;
                case 3:
                    pintarComponentesPnlTipoPago(true, false);
                    break;
            }
        }

        private void renderComponents()
        {
            pintarLabelTotalRestante();

            actualizarNumLineasPagos();
            actualizarColumnasMontoTotalPagos();
            actualizarTotalPorPagar();
            llenarComboBoxBancos();
            llenarComboBoxTipoPago();
            pnlFormaPago_cbxFormaPago_SelectedIndexChanged();
        }

        private void renderMenu(bool pinactivarBotonFinalizar)
        {
            RenderOptions(true);

            string _tp = view.FindByName<Label>("pnlFormaPago_lblTotalPago").Text;
            decimal _totalPago = FormatUtil.convertStringToDecimal(_tp);

            if (v_objCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._recaudacionSigla))
            {
                bool _ltvPagoVacio = false;

                ValidateHH _validateHH = new ValidateHH();

                _ltvPagoVacio = _validateHH.emptyListView<pnlFormaPago_ltvPagos>(view.FindByName<ListView>("pnlFormaPago_ltvPagos"));

                if (_ltvPagoVacio)
                {
                    RenderAgregarPago(true);
                }
                else
                {
                    RenderAgregarPago(true);

                    if (!pinactivarBotonFinalizar)
                    {
                        RenderFinalizar(true);
                    }
                }
            }
            else
            {
                if (_totalPago <= 0)
                {
                    if (!pinactivarBotonFinalizar)
                    {
                        RenderFinalizar(true);
                    }
                }
                else
                {
                    RenderAgregarPago(true);
                }
            }
        }

        private void RenderOptions(bool bandera) {

            if (bandera)
            {
                view.FindByName<Grid>("pnlFormaPago_grdOptions").IsVisible = true;
                RenderAgregarPago(false);
                RenderFinalizar(false);
            }

        }

        private void RenderFinalizar(bool Finalizar) {

            if (Finalizar)
            {

                view.FindByName<StackLayout>("pnlFormaPago_stkFinalizar").IsVisible = true;
            }
            else
            {

                view.FindByName<StackLayout>("pnlFormaPago_stkFinalizar").IsVisible = false;
            }

        }

        private void RenderAgregarPago(bool AgregarPago) {

            if (AgregarPago)
            {
                view.FindByName<StackLayout>("pnlFormaPago_stkAgregarPago").IsVisible = true;
            }
            else {

                view.FindByName<StackLayout>("pnlFormaPago_stkAgregarPago").IsVisible = false;
            }

        }

        private void constructor()
        {
            view.FindByName<ExtendedEntry>("pnlFormaPago_txtNumTransaccion").Text = string.Empty;
            view.FindByName<ExtendedEntry>("pnlFormaPago_txtSerie").Text = string.Empty;
            RenderWindows.paintWindow(view);
            renderPaneles(view.FindByName<StackLayout>("pnlFormaPago"));
            renderComponents();
            renderMenu(false);

            if (!v_motivoRecaudacion.Equals(string.Empty))
            {
                view.FindByName<ExtendedEntry>("pnlFormaPago_txtObservacion").IsEnabled = false;
            }

            view.FindByName<ExtendedEntry>("pnlFormaPago_txtObservacion").Text = v_motivoRecaudacion;
        }

        internal void ScreenInicialization(string ppantalla, Cliente pobjCliente)
        {
            v_pantalla = ppantalla;
            v_objCliente = pobjCliente;

            constructor();
        }

        internal void ScreenInicialization(decimal ptotalSinIV, string ppantalla, Cliente pobjCliente, LogicaCarniceriaGuardar plogica)
        {
            v_logicaCarniceriaGuardar = plogica;
            v_pantalla = ppantalla;
            v_objCliente = pobjCliente;

            constructor();
        }

        internal void ScreenInicialization(decimal ptotalSinIV, string ppantalla, Cliente pobjCliente, LogicaVisitaGuardar plogica)
        {
            v_logicaVisitaGuardar = plogica;
            v_pantalla = ppantalla;
            v_objCliente = pobjCliente;

            constructor();
        }

        internal void ScreenInicialization(string ppantalla,Cliente pobjCliente,string pmotivoRecaudacion, LogicaVisitaGuardar plogica)
        {
            v_logicaVisitaGuardar = plogica;
            v_pantalla = ppantalla;
            v_objCliente = pobjCliente;
            v_motivoRecaudacion = pmotivoRecaudacion;

            constructor();
        }

        private bool validarFormulario()
        {
            bool _formularioCorrecto = false;

            ValidateHH _validateHH = new ValidateHH();

            bool _montoPago = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlFormaPago_txtMontoPago"));

            if (!_montoPago)
            {
                _formularioCorrecto = true;
            }
            else
            {
                _formularioCorrecto = false;
            }

            return _formularioCorrecto;
        }

        private bool validarCantidadPago()
        {
            ValidateHH _validateHH = new ValidateHH();

            bool _monto = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlFormaPago_txtMontoPago"));
            bool _montoMayor = _validateHH.amountGreaterThanZero(view.FindByName<ExtendedEntry>("pnlFormaPago_txtMontoPago"));

            bool _condition = false;

            if (!_monto)
            {
                if (_montoMayor)
                {
                    _condition = true;

                    Logica_ManagerFormaPago _manager = new Logica_ManagerFormaPago();

                    string _codFormaPago = _manager.obtenerCodigoFormaPago(view.FindByName<Picker>("pnlFormaPago_cbxFormaPago").SelectedItem.ToString());

                    if (!_codFormaPago.Equals(PaymentForm._cashInitials))
                    {
                        bool _noTransaccion = false;

                        if (view.FindByName<ExtendedEntry>("pnlFormaPago_txtNumTransaccion").IsVisible)
                        {
                            _noTransaccion = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlFormaPago_txtNumTransaccion"));
                        }

                        if (_noTransaccion)
                        {
                            _condition = false;
                        }
                        else
                        {
                            if (_codFormaPago.Equals(PaymentForm._checkInitials))
                            {
                                bool _noSerie = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlFormaPago_txtSerie"));

                                if (_noSerie)
                                {
                                    _condition = false;
                                }
                            }
                        }
                    }
                }
            }

            return _condition;
        }

        internal void limpiarPnlFormaPago()
        {
            view.FindByName<ExtendedEntry>("pnlFormaPago_txtNumTransaccion").Text = string.Empty;
            view.FindByName<ExtendedEntry>("pnlFormaPago_txtMontoPago").Text = string.Empty;
            view.FindByName<ExtendedEntry>("pnlFormaPago_txtSerie").Text = string.Empty;
        }

        private void agregarPago()
        {
            string _formaPago = view.FindByName<Picker>("pnlFormaPago_cbxFormaPago").SelectedItem.ToString();

            pnlFormaPago_ltvPagos _lvi = new pnlFormaPago_ltvPagos();
            _lvi.FormaPago = _formaPago;

            Util _util = new Util();

            decimal _montoPagos = _util.sumarItemsColumnaLista<pnlFormaPago_ltvPagos>(
                                        view.FindByName<ListView>("pnlFormaPago_ltvPagos"),
                                        1
                                        );

            decimal _abonoFactura = FormatUtil.convertStringToDecimal(view.FindByName<ExtendedEntry>("pnlFormaPago_txtMontoPago").Text);

            decimal _faltaPorAbonar = v_objCliente.v_objTransaccion.v_total - _montoPagos;

            if (_abonoFactura > _faltaPorAbonar)
            {
                if (!v_objCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._recaudacionSigla))
                {
                    _abonoFactura = _faltaPorAbonar;
                }
            }

            _lvi.Monto = FormatUtil.applyCurrencyFormat(_abonoFactura);

            Logica_ManagerFormaPago _manager = new Logica_ManagerFormaPago();

            string _codFormaPago = _manager.obtenerCodigoFormaPago(view.FindByName<Picker>("pnlFormaPago_cbxFormaPago").SelectedItem.ToString());

            if (_codFormaPago.Equals(PaymentForm._cashInitials))
            {
                _lvi.Banco = PaymentForm._notApply;

                _lvi.NumeroTransaccion = PaymentForm._notApply;

                _lvi.Serie = PaymentForm._notApply;
            }
            else
            {
                string _banco = view.FindByName<Picker>("pnlFormaPago_cbxBanco").SelectedItem.ToString();
                _lvi.Banco = _banco;

                string _numTransaccion = view.FindByName<ExtendedEntry>("pnlFormaPago_txtNumTransaccion").Text;
                _lvi.NumeroTransaccion = _numTransaccion;

                if (_codFormaPago.Equals(PaymentForm._checkInitials))
                {
                    string _numSerie = view.FindByName<ExtendedEntry>("pnlFormaPago_txtSerie").Text;
                    _lvi.Serie = _numSerie;
                }
                else
                {
                    _lvi.Serie = PaymentForm._notApply;
                }
            }

            var Source = view.FindByName<ListView>("pnlFormaPago_ltvPagos").ItemsSource as ObservableCollection<pnlFormaPago_ltvPagos>;
            if(Source == null)
            {
                Source = new ObservableCollection<pnlFormaPago_ltvPagos>();
            }

            Source.Add(_lvi);

            view.FindByName<ListView>("pnlFormaPago_ltvPagos").ItemsSource = Source;

            limpiarPnlFormaPago();

            actualizarTotalPorPagar();
            actualizarColumnasMontoTotalPagos();
            actualizarNumLineasPagos();
            renderMenu(false);
        }

        internal void menu_mniAgregarPago_Click()
        {
            bool _formularioCorrecto = validarFormulario();

            if (_formularioCorrecto)
            {
                bool _bandera = validarCantidadPago();

                if (_bandera)
                {
                    agregarPago();
                }
            }
        }

        private void LevantarObjetoFormaDePago()
        {
            List<FormaPago> _listaFormaPago = new List<FormaPago>();

            var Source = view.FindByName<ListView>("pnlFormaPago_ltvPagos").ItemsSource as ObservableCollection<pnlFormaPago_ltvPagos>;

            foreach (var _lvi in Source)
            {
                Logica_ManagerFormaPago _managerFormaPago = new Logica_ManagerFormaPago();
                string _codFormaPago = _managerFormaPago.obtenerCodigoFormaPago(_lvi.FormaPago);

                Logica_ManagerBanco _managerBanco = new Logica_ManagerBanco();
                string _codBanco = _managerBanco.obtenerCodigoBanco(_lvi.Banco);

                string _numeroTransaccion = _lvi.NumeroTransaccion;

                if (_numeroTransaccion.Equals(PaymentForm._notApply))
                {
                    _numeroTransaccion = string.Empty;
                }

                string _serie = _lvi.Serie;

                if (_serie.Equals(PaymentForm._notApply))
                {
                    _serie = string.Empty;
                }

                FormaPago _objFormaPago = new FormaPago();

                _objFormaPago._formaPago = _codFormaPago;
                _objFormaPago._monto = FormatUtil.convertStringToDecimal(_lvi.Monto);
                _objFormaPago._banco = _codBanco;
                _objFormaPago._numTransaccion = _numeroTransaccion;
                _objFormaPago._serie = _serie;

                _listaFormaPago.Add(_objFormaPago);
            }

            v_objCliente.v_objTransaccion.v_listaFormaPago = _listaFormaPago;
        }

        private void guardarRecibo()
        {
            v_objCliente.v_objTransaccion.v_observacion = view.FindByName<ExtendedEntry>("pnlFormaPago_txtObservacion").Text; ;


            Util _util = new Util();

            string _monto = FormatUtil.applyCurrencyFormat(_util.sumarItemsColumnaLista<pnlFormaPago_ltvPagos>(view.FindByName<ListView>("pnlFormaPago_ltvPagos"), 1));

            decimal _montoDecimal = FormatUtil.convertStringToDecimal(_monto);

            v_objCliente.v_objTransaccion.v_total = _montoDecimal;


            v_objCliente.v_objTransaccion.v_saldo = _montoDecimal;

            if (v_objCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._recaudacionSigla))
            {
                v_objCliente.v_objTransaccion.v_saldo = Numeric._zeroDecimalInitialize;
            }

            LevantarObjetoFormaDePago();

            v_guardar = true;
        }

        private async Task guardarTransaccion()
        {
            if (v_objCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._recaudacionSigla))
            {
                decimal _total = FormatUtil.convertStringToDecimal(view.FindByName<Label>("pnlFormaPago_lblTotalPago").Text);

                v_objCliente.v_objTransaccion.v_total = _total;
            }

            Logica_ManagerTransaccion _manager = null;

            _manager = new Logica_ManagerTransaccion(Visita_view);

            await _manager.guardarTransaccion(view.FindByName<ListView>("pnlFormaPago_ltvPagos"), v_objCliente);
        }

        internal async Task menu_mniFinalizar_Click()
        {
            DetallesFormaPago();

            view.FindByName<StackLayout>("pnlFormaPago_stkFinalizar").IsEnabled = false;

            renderMenu(true);
            
            RenderOptions(true);

            bool _esDocumentoDeseado = false;

            if (v_objCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._recaudacionSigla))
            {
                Logica_ManagerTipoTransaccion _managerTipoTransaccion = new Logica_ManagerTipoTransaccion();

                string Mensaje = "¿Seguro que la transacción es un(a) "
                    + _managerTipoTransaccion.obtenerDescripcionTipoTransaccion(v_objCliente.v_objTransaccion.v_objTipoDocumento.GetNombre())
                    + "?"
                    + Environment.NewLine
                    + Environment.NewLine
                    + "* Evite anular un documento.";
                string Titulo = "IMPORTANTE";
                string BotonAceptar = "Aceptar";
                string BotonCancelar = "Cancelar";

                _esDocumentoDeseado = await UserDialogs.Instance.ConfirmAsync(Mensaje, Titulo, BotonAceptar, BotonCancelar, null);

            }
            else
            {
                _esDocumentoDeseado = true;
            }

            if (_esDocumentoDeseado == true)
            {
                if (v_pantalla.Equals(PantallasSistema._pantallaVisita))
                {

                    if (v_objCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._recaudacionSigla))
                    {
                        guardarRecibo();

                        if (v_guardar)
                        {
                            Logica_ManagerRecibo _manager = new Logica_ManagerRecibo();

                            ListView _listView = new ListView();

                            await _manager.guardarRecibo(v_objCliente,_listView,v_atomicidadTransaccional);

                        }
                    }
                    else
                    {
                        await guardarTransaccion();
                    }
                }

                if (v_pantalla.Equals(PantallasSistema._pantallaRecibo))
                {
                    guardarRecibo();
                }
            }
            else
            {
                view.FindByName<StackLayout>("pnlFormaPago_stkFinalizar").IsEnabled = true;
            }

            renderMenu(false);

            Cerrando = true;

            await Application.Current.MainPage.Navigation.PopAsync();

        }

        internal void DetallesFormaPago() {

            HelperDetalleFormaPago HFP = new HelperDetalleFormaPago();
            string Message = string.Empty;
            var Notification = DependencyService.Get<IAndroidMethods>();
            string Title = string.Empty;
            
            var Source = view.FindByName<ListView>("pnlFormaPago_ltvPagos").ItemsSource as ObservableCollection<pnlFormaPago_ltvPagos>;

            if (Source != null)
            {
                if (Source.Count > 0)
                {
                    Title = "Detalle de forma de pago.";
                    Message = HFP.DetalleFormaPago(view.FindByName<ListView>("pnlFormaPago_ltvPagos"));

                    Notification.PushNotification(Message, Title);
                }
            }

        }

        internal void pnlFormaPago_btnLimpiarNumero_Click()
        {
            view.FindByName<ExtendedEntry>("pnlFormaPago_txtNumTransaccion").Text = string.Empty;
        }

        internal void pnlFormaPago_btnLimpiarSerie_Click()
        {
            view.FindByName<ExtendedEntry>("pnlFormaPago_txtSerie").Text = string.Empty;
        }

        internal void pnlFormaPago_btnLimpiarObservacion_Click()
        {
            view.FindByName<ExtendedEntry>("pnlFormaPago_txtObservacion").Text = string.Empty;
        }

        internal void pnlFormaPago_btnLimpiarMonto_Click()
        {
            view.FindByName<ExtendedEntry>("pnlFormaPago_txtMontoPago").Text = string.Empty;
        }

        internal void pnlFormaPago_btnBorrarNumero_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlFormaPago_txtNumTransaccion"));
        }

        internal void pnlFormaPago_btnBorrarSerie_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlFormaPago_txtSerie"));
        }

        internal void pnlFormaPago_btnBorrarObservacion_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlFormaPago_txtObservacion"));
        }

        internal void pnlFormaPago_btnBorrarMonto_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlFormaPago_txtMontoPago"));
        }

        internal void pnlFormaPago_btnGuionNumero_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.guion(view.FindByName<ExtendedEntry>("pnlFormaPago_txtNumTransaccion"));
        }

        internal void pnlFormaPago_btnGuionSerie_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.guion(view.FindByName<ExtendedEntry>("pnlFormaPago_txtSerie"));
        }

        internal void pnlFormaPago_btnPuntoDecimalMonto_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.punto(view.FindByName<ExtendedEntry>("pnlFormaPago_txtMontoPago"));
        }

        internal async Task ctxtMenu_mniEliminar_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlFormaPago_ltvPagos")))
            {
                Util _util = new Util();

                await _util.deleteElementListView<pnlFormaPago_ltvPagos>(view.FindByName<ListView>("pnlFormaPago_ltvPagos"));
                actualizarTotalPorPagar();
                actualizarColumnasMontoTotalPagos();
                actualizarNumLineasPagos();
                renderMenu(false);
            }
        }

        internal async Task pnlFormaPago_btnBorrarLinea_Click()
        {
            await ctxtMenu_mniEliminar_Click();
        }

        internal void ctxtMenu_mniEliminarTodos_Click()
        {
            view.FindByName<ListView>("pnlFormaPago_ltvPagos").ItemsSource = new ObservableCollection<pnlFormaPago_ltvPagos>();
            actualizarTotalPorPagar();
            actualizarColumnasMontoTotalPagos();
            actualizarNumLineasPagos();
            renderMenu(false);
        }

        internal void pnlFormaPago_btnBorrarTodos_Click()
        {
            ctxtMenu_mniEliminarTodos_Click();
        }

        internal void pnlFormaPago_cbxBanco_SelectedIndexChanged()
        {
            Logica_ManagerBanco _manager = new Logica_ManagerBanco();

            string _numCuenta = _manager.obtenerCuentaBanco(view.FindByName<Picker>("pnlFormaPago_cbxBanco").SelectedItem.ToString());

            view.FindByName<Label>("pnlFormaPago_lblCuentaDeposito").Text = _numCuenta;
        }

        public async Task Cerrar()
        {
            if (Cerrando)
            {
                if(v_pantalla == PantallasSistema._pantallaRecibo)
                {
                    await view.viewRecibo.controlador.RespuestaDePagoRecibo(v_guardar);
                }
                if (v_logicaVisitaGuardar != null)
                {
                    await v_logicaVisitaGuardar.SiGuardar(v_objCliente.v_objTransaccion);
                }
            }
        }
    }
}
