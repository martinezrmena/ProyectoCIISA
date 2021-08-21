using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita.Guardar;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    public class ctrlRecibo
    {
        private vistaRecibo view { get; set; }
        private bool Cerrar = false;
        public Cliente v_objCliente = new Cliente();
        private LogicaVisitaGuardar v_logicaVisitaGuardar = null;

        internal bool v_atomicidadTransaccional = false;

        internal ctrlRecibo(vistaRecibo pview)
        {
            view = pview;
        }

        private void actualizarNumLineasFacturas()
        {
            MiscUtils _miscUtils = new MiscUtils();
            _miscUtils.quantityListViewItems<pnlAbono_ltvFacturas>(view.FindByName<ListView>("pnlAbono_ltvFacturas"), view.FindByName<Label>("pnlAbono_ltvFacturas_clhNoFisico"), "Documento");
        }

        private void actualizarNumLineasAbonos()
        {
            MiscUtils _miscUtils = new MiscUtils();
            _miscUtils.quantityListViewItems<pnlAbono_ltvAbonos>(view.FindByName<ListView>("pnlAbono_ltvAbonos"), view.FindByName<Label>("pnlAbono_ltvPagos_clhFactura"), "Documento");
        }

        private void actualizarColumnasMontoTotalFacturas()
        {
            Util _util = new Util();

            _util.sumarItemsColumnaLista<pnlAbono_ltvFacturas>(
                view.FindByName<ListView>("pnlAbono_ltvFacturas"),
                view.FindByName<Label>("pnlAbono_ltvFacturas_clhSaldo"),
                3,
                "Saldo"
                );
        }

        private void actualizarColumnasMontoTotalAbonos()
        {
            Util _util = new Util();

            _util.sumarItemsColumnaLista<pnlAbono_ltvAbonos>(
                view.FindByName<ListView>("pnlAbono_ltvAbonos"),
                view.FindByName<Label>("pnlAbono_ltvPagos_clhAbono"),
                1,
                "Monto"
                );
        }

        private void renderMenu()
        {
            ValidateHH _validateHH = new ValidateHH();

            bool _ltvAbonosVacio = _validateHH.emptyListView<pnlAbono_ltvAbonos>(view.FindByName<ListView>("pnlAbono_ltvAbonos"));
            bool _ltvFacturasVacio = _validateHH.emptyListView<pnlAbono_ltvFacturas>(view.FindByName<ListView>("pnlAbono_ltvFacturas"));
            if (!_ltvFacturasVacio)
            {
                var SourceAbonos = view.FindByName<ListView>("pnlAbono_ltvAbonos").ItemsSource as ObservableCollection<pnlAbono_ltvAbonos>;
                if (SourceAbonos == null)
                {
                    SourceAbonos = new ObservableCollection<pnlAbono_ltvAbonos>();
                }
                var SourceFacturas = view.FindByName<ListView>("pnlAbono_ltvFacturas").ItemsSource as ObservableCollection<pnlAbono_ltvFacturas>;
                if (SourceFacturas == null)
                {
                    SourceFacturas = new ObservableCollection<pnlAbono_ltvFacturas>();
                }

                int _cantAbonos = SourceAbonos.Count;

                int _cantFacturas = SourceFacturas.Count;

                if (_cantAbonos != _cantFacturas)
                {
                    RenderMenuParte2(false);

                }

                if (!_ltvAbonosVacio)
                {
                    RenderMenuParte2(true);
                }
            }
            else
            {
                if (!v_objCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._reciboDineroSigla)
                    && !v_objCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._recaudacionSigla))
                {
                    view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniCliente"));
                }
            }
        }

        private void RenderOptions(bool bandera)
        {

            if (bandera)
            {
                view.FindByName<Grid>("pnlRecibo_grdOptions").IsVisible = true;
            }
            else
            {
                view.FindByName<Grid>("pnlRecibo_grdOptions").IsVisible = false;
            }

        }

        private void RenderMenuParte2(bool bandera)
        {

            RenderOptions(true);

            if (bandera)
            {
                view.FindByName<StackLayout>("pnlRecibo_stkPagar").IsVisible = true;
                view.FindByName<StackLayout>("pnlRecibo_stkAbonar").IsVisible = false;
            }
            else
            {

                view.FindByName<StackLayout>("pnlRecibo_stkPagar").IsVisible = false;
                view.FindByName<StackLayout>("pnlRecibo_stkAbonar").IsVisible = true;
            }

        }

        private void renderComponents()
        {
            actualizarNumLineasFacturas();
            actualizarNumLineasAbonos();
            actualizarColumnasMontoTotalFacturas();
            actualizarColumnasMontoTotalAbonos();

            renderMenu();
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlAbono").Id))
            {
                view.Title = "Abono Facturas";
            }

            ppanel.IsVisible = true;
        }

        private void pintarInformacionCliente()
        {
            view.FindByName<Label>("pnlAbono_lblNombreCliente").Text = v_objCliente.v_no_cliente
                                                        + Simbol._hyphenWithSpaces
                                                        + v_objCliente.v_nombre
                                                        ;
        }

        internal void ScreenInicialization(Cliente pobjCliente, LogicaVisitaGuardar plogica)
        {
            v_objCliente = pobjCliente;
            v_logicaVisitaGuardar = plogica;

            RenderWindows.paintWindow(view);
            renderComponents();
            renderPaneles(view.FindByName<StackLayout>("pnlAbono"));
            renderMenu();

            pintarInformacionCliente();
        }

        internal void pnlAbono_btnBorrar_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlAbono_txtMontoAbono"));
        }

        internal void pnlAbono_btnLimpiar_Click()
        {
            view.FindByName<ExtendedEntry>("pnlAbono_txtMontoAbono").Text = string.Empty;
        }
        
        internal async Task ctxMenu_abono_mniEliminar_Click()
        {
            Util _util = new Util();

            await _util.deleteElementListView<pnlAbono_ltvAbonos>(view.FindByName<ListView>("pnlAbono_ltvAbonos"));
            actualizarColumnasMontoTotalAbonos();
            actualizarNumLineasAbonos();
            renderMenu();

        }

        internal void ctxMenu_abono_mniEliminarTodos_Click()
        {
            view.FindByName<ListView>("pnlAbono_ltvAbonos").ItemsSource = new ObservableCollection<pnlAbono_ltvAbonos>();
            renderMenu();
            actualizarColumnasMontoTotalAbonos();
            actualizarNumLineasAbonos();
        }

        internal async Task ctxMenu_factura_mniEliminar_Click()
        {
            bool _existe = false;

            var _lviUno = view.FindByName<ListView>("pnlAbono_ltvFacturas").SelectedItem as pnlAbono_ltvFacturas;

            if (_lviUno != null)
            {
                var Source = view.FindByName<ListView>("pnlAbono_ltvFacturas").ItemsSource as ObservableCollection<pnlAbono_ltvFacturas>;
                int IndiceUno = Source.IndexOf(_lviUno);

                foreach (var _lviDos in Source)
                {
                    if (IndiceUno == Source.IndexOf(_lviDos))
                    {
                        _existe = true;
                    }
                }
            }

            if (!_existe)
            {
                Util _util = new Util();

                await _util.deleteElementListView<pnlAbono_ltvFacturas>(view.FindByName<ListView>("pnlAbono_ltvFacturas"));
                actualizarColumnasMontoTotalFacturas();
                actualizarNumLineasFacturas();
                renderMenu();
            }
            else
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention(
                    "Debe eliminar el abono a la factura");
            }
        }

        internal async Task ctxMenu_factura_mniEliminarTodas_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (_validateHH.emptyListView<pnlAbono_ltvAbonos>(view.FindByName<ListView>("pnlAbono_ltvAbonos")))
            {
                view.FindByName<ListView>("pnlAbono_ltvFacturas").ItemsSource = new ObservableCollection<pnlAbono_ltvFacturas>();
                renderMenu();
                actualizarColumnasMontoTotalFacturas();
                actualizarNumLineasFacturas();
            }
            else
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention(
                    "Debe eliminar el/los abono(s) a la(s) factura(s)");
            }

        }

        internal void menu_mniPagar_Click()
        {
            Util _util = new Util();

            decimal _total = _util.sumarItemsColumnaLista<pnlAbono_ltvAbonos>(view.FindByName<ListView>("pnlAbono_ltvAbonos"), 1);

            v_objCliente.v_objTransaccion.v_total = _total;

            ShowSROL _show = new ShowSROL();

            _show.mostrarPantallaFormaPagoRecibo(PantallasSistema._pantallaRecibo, v_objCliente,view);
        }

        public async Task RespuestaDePagoRecibo(bool _guardar)
        {
            if (_guardar)
            {
                Logica_ManagerRecibo _manager = new Logica_ManagerRecibo();

                await _manager.guardarRecibo(
                    v_objCliente,
                    view.FindByName<ListView>("pnlAbono_ltvAbonos"),
                    v_atomicidadTransaccional
                    );
            }

            if (!v_objCliente.v_objTransaccion.v_codDocumento.Equals(string.Empty))
            {
                Cerrar = true;
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                renderComponents();

                await LogMessageError.generalError("No realizó el pago");
            }
        }

        internal void establecerVariablesCliente(Cliente pobjCliente)
        {
            v_objCliente = pobjCliente;
            pintarInformacionCliente();
        }

        private bool validarFormulario()
        {
            ValidateHH _validateHH = new ValidateHH();

            bool _txtVacio = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlAbono_txtMontoAbono"));
            bool _cantMayorQueCero = _validateHH.amountGreaterThanZero(view.FindByName<ExtendedEntry>("pnlAbono_txtMontoAbono"));

            bool _estadoCorrecto = false;

            if (!_txtVacio)
            {
                if (_cantMayorQueCero)
                {
                    _estadoCorrecto = true;
                }
            }

            return _estadoCorrecto;
        }

        private void cargarFacturas(List<string> plistaFacturas)
        {
            Logica_ManagerFactura _manager = new Logica_ManagerFactura();

            _manager.buscarListaFactura(view.FindByName<ListView>("pnlAbono_ltvFacturas"), v_objCliente, plistaFacturas);
        }

        internal void pnlAbono_btnPuntoDecimal_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.punto(view.FindByName<ExtendedEntry>("pnlAbono_txtMontoAbono"));
        }

        internal void menu_mniCliente_Click()
        {
            renderComponents();

            ShowSROL _show = new ShowSROL();

            _show.mostrarPantallaClienteRecibo(view);

        }

        internal void menu_mniAbonar_Click()
        {
            bool _bandera = validarFormulario();

            if (_bandera)
            {
                Util _util = new Util();

                decimal _abonoFacturas = FormatUtil.convertStringToDecimal(view.FindByName<ExtendedEntry>("pnlAbono_txtMontoAbono").Text);

                var SourceFacturas = view.FindByName<ListView>("pnlAbono_ltvFacturas").ItemsSource as ObservableCollection<pnlAbono_ltvFacturas>;

                var SourceAbonos = view.FindByName<ListView>("pnlAbono_ltvAbonos").ItemsSource as ObservableCollection<pnlAbono_ltvAbonos>;

                foreach (var _lviFactura in SourceFacturas)
                {
                    if (_abonoFacturas > 0)
                    {
                        string _factura = _lviFactura.NoFisico;

                        pnlAbono_ltvAbonos _lviAbono = new pnlAbono_ltvAbonos();

                        _lviAbono.Factura = _factura;

                        string _sldFactura = _lviFactura.Monto;

                        decimal _saldoFactura = FormatUtil.convertStringToDecimal(_sldFactura);

                        decimal _montoAbonar = _abonoFacturas - _saldoFactura;

                        if (_montoAbonar < 0)
                        {
                            _montoAbonar = _abonoFacturas;
                        }
                        else
                        {
                            _montoAbonar = _saldoFactura;
                        }

                        _lviAbono.Abono = FormatUtil.applyCurrencyFormat(_montoAbonar);

                        _lviAbono.SaldoAnterior = FormatUtil.applyCurrencyFormat(_saldoFactura);

                        decimal _saldoActual = _saldoFactura - _abonoFacturas;

                        if (_saldoActual < 0)
                        {
                            _abonoFacturas = _saldoActual * -1;
                            _saldoActual = Numeric._zeroDecimalInitialize;

                        }
                        else
                        {
                            _abonoFacturas = Numeric._zeroDecimalInitialize;
                        }

                        string _sldActual = FormatUtil.applyCurrencyFormat(_saldoActual);

                        _lviAbono.SaldoActual = _sldActual;

                        _lviAbono.TipoDocumentoDescripcion = _lviFactura.TipoDocumentoDescripcion;

                        _lviAbono.TipoDocumento = _lviFactura.TipoDocumento;

                        SourceAbonos.Add(_lviAbono);
                    }

                    view.FindByName<ListView>("pnlAbono_ltvAbonos").ItemsSource = SourceAbonos;
                }

                renderMenu();
                actualizarNumLineasAbonos();
                actualizarColumnasMontoTotalAbonos();
                view.FindByName<ExtendedEntry>("pnlAbono_txtMontoAbono").Text = string.Empty;
            }

        }

        internal async Task pnlAbono_ltvFacturas_Columna0Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            bool _existeAbono = _validateHH.emptyListView<pnlAbono_ltvAbonos>(view.FindByName<ListView>("pnlAbono_ltvAbonos"));            

            if(_existeAbono)
            {
                view.FindByName<ListView>("pnlAbono_ltvAbonos").ItemsSource = new ObservableCollection<pnlAbono_ltvAbonos>();

                ShowSROL _show = new ShowSROL();

                _show.mostrarPantallaFactura(v_objCliente, PantallasSistema._pantallaRecibo,view);                
            }
            else
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention("No puede agregar facturas, existen abonos en la lista");
            }
        }

        internal void pnlAbono_ltvFacturas_Columna1Click()
        {
            Util _util = new Util();

            _util.sumarItemsColumnaLista<pnlAbono_ltvFacturas>(
                    view.FindByName<ListView>("pnlAbono_ltvFacturas"),
                    view.FindByName<Label>("pnlAbono_ltvFacturas_clhSaldo"),
                    1,
                    "Monto"
                    );
        }

        public async Task RespuestaDeFacturasPendientes()
        {
            Util _util = new Util();

            if (v_objCliente.v_listaFacturas.Count > 0)
            {
                cargarFacturas(v_objCliente.v_listaFacturas);

                decimal _totalFacturas = _util.sumarItemsColumnaLista<pnlAbono_ltvFacturas>(view.FindByName<ListView>("pnlAbono_ltvFacturas"), 1);

                view.FindByName<ExtendedEntry>("pnlAbono_txtMontoAbono").Text = FormatUtil.applyCurrencyFormat(_totalFacturas);

                actualizarColumnasMontoTotalFacturas();
                actualizarNumLineasFacturas();
            }
            else
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention("No se seleccionó ninguna factura");
            }

            renderMenu();
        }

        public async Task Cerrando()
        {
            if (Cerrar)
            {
                if (view.v_pantallaInvoca.Equals(PantallasSistema._pantallaVisita))
                {

                }

                if (v_logicaVisitaGuardar != null)
                {
                    await v_logicaVisitaGuardar.SiGuardar(v_objCliente.v_objTransaccion);
                }
            }
        }
    }
}
