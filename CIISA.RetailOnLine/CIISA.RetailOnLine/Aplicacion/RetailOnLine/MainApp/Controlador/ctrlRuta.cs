using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.External.Behaviors;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    internal class ctrlRuta
    {
        private vistaRuta view { get; set; }

        public Cliente v_objCliente = new Cliente();

        private string v_pantallaVisita = string.Empty;
        private string v_pantallaPromedioVentas = string.Empty;

        internal ctrlRuta(vistaRuta pview)
        {
            view = pview;
        }

        public void renderPaneles(StackLayout ppanel)
        {

            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlCliente"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlCliente").Id))
            {
                view.Title = "Ruta";
            }

            ppanel.IsVisible = true;
        }

        private void actualizarColumnasCodigo()
        {
            MiscUtils _miscUtils = new MiscUtils();
            _miscUtils.quantityListViewItems<pnlClientes_ltvClientes>(view.FindByName<ListView>("pnlClientes_ltvClientes"), view.FindByName<Label>("pnlClientes_clhCodCliente"), "Código");
        }

        public void renderComponents(bool pmostrar)
        {
            Util _util = new Util();

            _util.fillComboBoxTodayAll(
                view.FindByName<Picker>("pnlClientes_cbxSegmento")
                );

            _util.fillComboBoxSearchForCodeAndDescription(
                view.FindByName<Picker>("pnlClientes_cbxTipoBusqueda")
                );

            Logica_ManagerRazonesNV _manager = new Logica_ManagerRazonesNV();

            DataTable _dt = _manager.buscarRazonesNV();

            _util.fillComboBox(
                _dt,
                view.FindByName<Picker>("pnlClientes_cbxMotivo"),
                "Descripcion"
                );

            actualizarColumnasCodigo();
        }

        private void renderMenu()
        {
            if (view.FindByName<StackLayout>("pnlCliente").IsVisible)
            {
                view.ToolbarItems.Clear();
                //view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniBuscar"));

                ValidateHH _validateHH = new ValidateHH();

                //if (!_validateHH.emptyListView<pnlClientes_ltvClientes>(view.FindByName<ListView>("pnlClientes_ltvClientes")))
                //{
                //    if (_validateHH.JustValidatelistViewItemSelected(view.FindByName<ListView>("pnlClientes_ltvClientes")))
                //    {
                //        view.ToolbarItems.Add(view.FindByName<ToolbarItem>("context_Visita"));
                //        view.ToolbarItems.Add(view.FindByName<ToolbarItem>("context_AgregarMotivo"));
                //    }
                //}
            }
        }

        internal void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);
            renderPaneles(view.FindByName<StackLayout>("pnlCliente"));
            renderComponents(false);
            renderMenu();
        }

        private async Task buscar(string ptipoBusquedaSegmento)
        {
            view.FindByName<ListView>("pnlClientes_ltvClientes").ItemsSource = new ObservableCollection<pnlClientes_ltvClientes>();

            Logica_ManagerVisita _manager = new Logica_ManagerVisita();

            _manager.buscarListaClienteRuteros(
                view.FindByName<ListView>("pnlClientes_ltvClientes"),
                ptipoBusquedaSegmento,
                view.FindByName<Picker>("pnlClientes_cbxTipoBusqueda").SelectedItem.ToString(),
                view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar").Text);

            var Source = view.FindByName<ListView>("pnlClientes_ltvClientes").ItemsSource as ObservableCollection<pnlClientes_ltvClientes>;

            if (Source.Count == 1)
            {
                foreach (var _lvi in Source)
                {
                    await seleccionarCliente();
                }
            }

            actualizarColumnasCodigo();

            ValidateHH _validateHH = new ValidateHH();

            _validateHH.emptyListView<pnlClientes_ltvClientes>(view.FindByName<ListView>("pnlClientes_ltvClientes"));
        }

        private async Task<bool> seleccionarCliente()
        {
            bool validar = false;
            ValidateHH _validateHH = new ValidateHH();

            if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlClientes_ltvClientes")))
            {
                pnlClientes_ltvClientes Seleccionado = view.FindByName<ListView>("pnlClientes_ltvClientes").SelectedItem as pnlClientes_ltvClientes;

                v_objCliente.v_no_cliente = Seleccionado.CODCLIENTE;

                string _no_establecimiento = Seleccionado.CODESTABLECIMIENTO;

                v_objCliente.v_objEstablecimiento.v_codEstablecimiento = FormatUtil.convertStringToInt(_no_establecimiento);

                Logica_ManagerCliente _manager = new Logica_ManagerCliente();

                await _manager.buscarClientePorCodigoCliente(v_objCliente);

                validar = true;
            }

            return (bool)validar;
        }

        internal async Task menu_mniBuscar_Click()
        {
            await buscar(view.FindByName<Picker>("pnlClientes_cbxSegmento").SelectedItem.ToString());
            view.FindByName<ListView>("pnlClientes_ltvClientes").SelectedItem = null;
            renderMenu();
        }

        internal void menu_mniCerrarOrdenAtencion_Click()
        {
            renderPaneles(view.FindByName<StackLayout>("pnlCliente"));
            renderMenu();
        }

        internal async Task menu_mniAgregarMotivo_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (_validateHH.emptyListView<pnlClientes_ltvClientes>(view.FindByName<ListView>("pnlClientes_ltvClientes")))
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention("La lista esta vacía");
            }
            else
            {
                if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlClientes_ltvClientes")))
                {
                    pnlClientes_ltvClientes Seleccionado = view.FindByName<ListView>("pnlClientes_ltvClientes").SelectedItem as pnlClientes_ltvClientes;

                    string _codCliente = Seleccionado.CODCLIENTE;

                    string _desRazonNV = view.FindByName<Picker>("pnlClientes_cbxMotivo").SelectedItem.ToString();

                    Logica_ManagerRazonesNV _managerRazonesNV = new Logica_ManagerRazonesNV();

                    string _codRazonNV = _managerRazonesNV.obtenerCodigoTipoTransaccion(_desRazonNV);

                    Logica_ManagerEncabezadoRazonesNV _managerEncabezadoRazonesNV = new Logica_ManagerEncabezadoRazonesNV();

                    _managerEncabezadoRazonesNV.guardarRazonNoVenta(_codCliente, _codRazonNV);

                    view.FindByName<ListView>("pnlClientes_ltvClientes").SelectedItem = null;

                    await menu_mniBuscar_Click();
                }
            }

            renderMenu();
        }

        internal void pnlClientes_btnLimpiarBuscar_Click()
        {
            view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar").Text = string.Empty;
        }

        internal void pnlClientes_btnBorrarBuscar_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar"));
        }

        private void calcularCoberturaEfectiva()
        {
            view.FindByName<Label>("pnlClientes_txtCobertura").Text = string.Empty;

            Logica_ManagerEncabezadoTransaccion _managerEncabezadoTransaccion = new Logica_ManagerEncabezadoTransaccion();

            int _numeroClientesFacturados = _managerEncabezadoTransaccion.calcularTotalClientesFacturadosParaHoy();

            Logica_ManagerVisita _managerVisita = new Logica_ManagerVisita();

            int _totalClientesSegmento = _managerVisita.calcularTotalClientesSegmento();

            decimal _cobertura = 0;

            if (_totalClientesSegmento > 0)
            {
                _cobertura = (_numeroClientesFacturados * 100) / _totalClientesSegmento;
            }

            view.FindByName<Label>("pnlClientes_txtCobertura").Text = _cobertura + " " + Simbol._percentaje;
        }

        internal void pnlClientes_btnCobertura_Click()
        {
            calcularCoberturaEfectiva();
        }

        internal void pnlClientes_cbxTipoBusqueda_SelectedIndexChanged()
        {
            view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar").Text = string.Empty;
            view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar").Focus();
            view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar").Behaviors.Clear();

            if (view.FindByName<Picker>("pnlClientes_cbxTipoBusqueda").SelectedItem.ToString().Equals(VarComboBox._cbxCode))
            {
                view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar").Keyboard = Keyboard.Numeric;
                view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar").Behaviors.Add(new JustNumeric());

                renderPaneles(view.FindByName<StackLayout>("pnlCliente"));
            }
            else
            {
                view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar").Keyboard = Keyboard.Default;
                view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar").Behaviors.Clear();

                renderPaneles(view.FindByName<StackLayout>("pnlCliente"));
            }
        }

        internal async Task pnlClientes_ltvClientes_ItemActivete()
        {
            if (await seleccionarCliente())
            {
                ShowSROL _showRuta = new ShowSROL();

                _showRuta.v_objCliente = v_objCliente;

                _showRuta.mostrarPantallaVisitaDesdeRuta();

                await menu_mniBuscar_Click();

                view.FindByName<Label>("pnlClientes_txtCobertura").Text = string.Empty;

                //pnlClientes_cbxTipoBusqueda_SelectedIndexChanged();
                view.FindByName<ListView>("pnlClientes_ltvClientes").SelectedItem = null;
                renderMenu();
            }
            
        }

        internal void pnlClientes_ltvClientes_ItemSeleccionado()
        {
            renderMenu();
        }

        internal void pnlClientes_txtBuscar_Focus() {
            view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar").Focus();
        }
    }
}
