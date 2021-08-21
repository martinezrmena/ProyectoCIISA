using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RadioTelefonico.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Controlador;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.External.Behaviors;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.Controlador
{
    public class ctrlCliente
    {
        private vistaCliente view { get; set; }
        private vistaVisita viewVisita { get; set; }
        private vistaCarniceria viewCarniceria { get; set; }
        private vistaTelefono viewTelefono { get; set; }
        private vistaReporte viewReporte { get; set; }
        private vistaRecibo viewRecibo { get; set; }
        private ctrlCarga_indicadores controladorIndicadores { get; set; }
        private ctrlCarga_descuentos controladorDescuentos { get; set; }
        private bool NewCliente = false;
        private string v_pantallaCarniceria = string.Empty;
        private string v_pantallaVisita = string.Empty;
        private string v_pantallaRecibo = string.Empty;
        private string v_pantallaTelefono = string.Empty;
        private string v_pantallaReporte = string.Empty;
        private string v_pantallaCargaIndicadores = string.Empty;

        public string v_tipoBusqueda = string.Empty;
        public string v_filtro = string.Empty;

        public bool CERRADO = false;

        internal ctrlCliente(vistaCliente pview)
        {
            view = pview;
        }

        public void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlCliente").Id))
            {
                view.Title = "Cliente";
            }

            ppanel.IsVisible = true;
        }

        private void actualizarColumnasCodigo()
        {
            MiscUtils _miscUtils = new MiscUtils();
            _miscUtils.quantityListViewItems<pnlClientes_ltvClientes>(view.FindByName<ListView>("pnlClientes_ltvClientes"), view.FindByName<Label>("pnlClientes_clhCodClienteEstablecimiento"), "Código+Est");
        }

        public void renderComponents(bool pmostrar)
        {
            Util _util = new Util();

            _util.fillComboBoxSearchForCodeAndDescription(view.FindByName<Picker>("pnlClientes_cbxTipoBusqueda"));

            actualizarColumnasCodigo();

            view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar").Focus();
        }

        public void renderMenu() {

            view.ToolbarItems.Clear();
            view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniNuevoCliente"));

        }

        private void ajustarColumnas()
        {
            string _tipoAgente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._typeAgent;                        

            if (_tipoAgente.Equals(Agent._ruteroSigla))
            {
                //view.pnlClientes_clhCodCliente.Width = 60;
                //view.FindByName<ColumnDefinition>("ColumnaCliente").Width = 60;
                //view.pnlClientes_clhCodEstablecimiento.Width = 0;
                view.FindByName<ColumnDefinition>("ColumnaEstablecimiento").Width = 0;
                //view.pnlClientes_clhCodClienteEstablecimiento.Width = 0;
                view.FindByName<ColumnDefinition>("ColumnaClienteEstablecimiento").Width = 0;
            }
            else
            {
                //view.pnlClientes_clhCodCliente.Width = 0;
                view.FindByName<ColumnDefinition>("ColumnaCliente").Width = 0;
                //view.pnlClientes_clhCodEstablecimiento.Width = 0;
                view.FindByName<ColumnDefinition>("ColumnaEstablecimiento").Width = 0;
                //view.pnlClientes_clhCodClienteEstablecimiento.Width = 90;
                //view.FindByName<ColumnDefinition>("ColumnaClienteEstablecimiento").Width = 90;
            }

            //view.pnlClientes_clhDescCliente.Width = 120;
            //view.FindByName<ColumnDefinition>("ColumnaDescripcion").Width = 120;

            if (v_pantallaTelefono.Equals(string.Empty))
            {
                //view.pnlClientes_clhTelefono.Width = 0;
                view.FindByName<ColumnDefinition>("ColumnaTelefono").Width = 0;
                //view.pnlClientes_clhAgente.Width = 60;
                //view.FindByName<ColumnDefinition>("ColumnaAgente").Width = 60;
                //view.pnlClientes_clhCobra.Width = 60;
                //view.FindByName<ColumnDefinition>("ColumnaCobra").Width = 60;
                //view.pnlClientes_clhEsCobrador.Width = 70;
                //view.FindByName<ColumnDefinition>("ColumnaEsCobrador").Width = 70;
            }
            else
            {
                //view.pnlClientes_clhTelefono.Width = 80;
                //view.FindByName<ColumnDefinition>("ColumnaTelefono").Width = 80;
                //view.pnlClientes_clhAgente.Width = 0;
                view.FindByName<ColumnDefinition>("ColumnaAgente").Width = 0;
                //view.pnlClientes_clhCobra.Width = 0;
                view.FindByName<ColumnDefinition>("ColumnaCobra").Width = 0;
                //view.pnlClientes_clhEsCobrador.Width = 0;
                view.FindByName<ColumnDefinition>("ColumnaEsCobrador").Width = 0;
            }

        }

        public void constructor()
        {
            RenderWindows.paintWindow(view);
            renderPaneles(view.FindByName<StackLayout>("pnlCliente"));
            renderComponents(false);
            renderMenu();
        }

        internal void ScreenInicialization(vistaCarniceria ppantallaCarniceria, string pvistaCarniceria)
        {
            v_pantallaCarniceria = pvistaCarniceria;
            viewCarniceria = ppantallaCarniceria;
            constructor();
        }

        internal void ScreenInicialization(string pvistaRecibo,vistaRecibo ppantallaRecibo)
        {
            v_pantallaRecibo = pvistaRecibo;
            viewRecibo = ppantallaRecibo;
            constructor();
        }

        internal void ScreenInicialization(vistaTelefono ppantallaTelefono, string ppantalla)
        {

            if (ppantalla.Equals(PantallasSistema._pantallaVisita))
            {
                v_pantallaVisita = ppantalla;
            }

            if (ppantalla.Equals(PantallasSistema._pantallaPromedioVentas))
            {
                v_pantallaVisita = ppantalla;
            }

            if (ppantalla.Equals(PantallasSistema._pantallaMenuCarga))
            {
                v_pantallaVisita = ppantalla;
            }

            if (ppantalla.Equals(PantallasSistema._pantallaTelefono))
            {
                v_pantallaTelefono = ppantalla;
                viewTelefono = ppantallaTelefono;
            }

            constructor();
        }

        internal void ScreenInicialization(vistaVisita ppantallaVisita,string ppantalla)
        {

            if (ppantalla.Equals(PantallasSistema._pantallaVisita))
            {
                v_pantallaVisita = ppantalla;
                viewVisita = ppantallaVisita;
            }

            if (ppantalla.Equals(PantallasSistema._pantallaPromedioVentas))
            {
                v_pantallaVisita = ppantalla;
            }

            if (ppantalla.Equals(PantallasSistema._pantallaMenuCarga))
            {
                v_pantallaVisita = ppantalla;
            }

            if (ppantalla.Equals(PantallasSistema._pantallaTelefono))
            {
                v_pantallaTelefono = ppantalla;
            }

            constructor();
            
        }

        internal void ScreenInicialization(vistaReporte ppantallaReporte, string ppantalla)
        {

            if (ppantalla.Equals(PantallasSistema._pantallaVisita))
            {
                v_pantallaVisita = ppantalla;
            }

            if (ppantalla.Equals(PantallasSistema._pantallaPromedioVentas))
            {
                v_pantallaVisita = ppantalla;
            }

            if (ppantalla.Equals(PantallasSistema._pantallaMenuCarga))
            {
                v_pantallaVisita = ppantalla;
            }

            if (ppantalla.Equals(PantallasSistema._pantallaTelefono))
            {
                v_pantallaTelefono = ppantalla;
            }

            if (ppantalla.Equals(PantallasSistema._pantallaReporteCDC))
            {
                v_pantallaReporte = ppantalla;
                viewReporte = ppantallaReporte;
            }

            constructor();
        }

        internal void ScreenInicialization(ctrlCarga_indicadores pcontroler, string ppantalla)
        {

            if (ppantalla.Equals(PantallasSistema._pantallaVisita))
            {
                v_pantallaVisita = ppantalla;
            }

            if (ppantalla.Equals(PantallasSistema._pantallaPromedioVentas))
            {
                v_pantallaVisita = ppantalla;
            }

            if (ppantalla.Equals(PantallasSistema._pantallaMenuCarga))
            {
                v_pantallaVisita = ppantalla;
            }

            if (ppantalla.Equals(PantallasSistema._pantallaTelefono))
            {
                v_pantallaTelefono = ppantalla;
            }

            if (ppantalla.Equals(PantallasSistema._pantallaReporteCDC))
            {
                v_pantallaReporte = ppantalla;
            }

            if (ppantalla.Equals(PantallasSistema._pantallaPromedioVentas))
            {
                v_pantallaCargaIndicadores = ppantalla;
                controladorIndicadores = pcontroler;
            }

            constructor();
        }

        internal void ScreenInicialization(ctrlCarga_descuentos pcontroler, string ppantalla)
        {

            if (ppantalla.Equals(PantallasSistema._pantallaVisita))
            {
                v_pantallaVisita = ppantalla;
            }

            if (ppantalla.Equals(PantallasSistema._pantallaPromedioVentas))
            {
                v_pantallaVisita = ppantalla;
            }

            if (ppantalla.Equals(PantallasSistema._pantallaMenuCarga))
            {
                v_pantallaVisita = ppantalla;
            }

            if (ppantalla.Equals(PantallasSistema._pantallaTelefono))
            {
                v_pantallaTelefono = ppantalla;
            }

            if (ppantalla.Equals(PantallasSistema._pantallaReporteCDC))
            {
                v_pantallaReporte = ppantalla;
            }

            if (ppantalla.Equals(PantallasSistema._pantallaPromedioVentas))
            {
                v_pantallaCargaIndicadores = ppantalla;
                controladorDescuentos = pcontroler;
            }

            constructor();
        }

        private async Task seleccionarCliente()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlClientes_ltvClientes")))
            {
                var Seleccionado = view.FindByName<ListView>("pnlClientes_ltvClientes").SelectedItem as pnlClientes_ltvClientes;

                view.v_objCliente.v_no_cliente = Seleccionado.NO_CLIENTE;

                Logica_ManagerEstablecimiento _managerEstablecimiento = new Logica_ManagerEstablecimiento();

                //Evaluar si el cliente posee mas de un establecimiento, para consultar en caso de querer cambiarlo
                if (_managerEstablecimiento.buscarCantidadEstablecimientosPorCodigoCliente(Seleccionado.NO_CLIENTE) > 1)
                {
                    Cliente cliente = new Cliente();

                    cliente.v_no_cliente = Seleccionado.NO_CLIENTE;
                    cliente.v_nombre = Seleccionado.DESCRIPCION;

                    await PopupNavigation.Instance.PushAsync(new vistaAgregarEstablecimiento(this, cliente));
                }
                else {

                    //No posee más de un establecimiento continuamos con el flujo
                    string _no_establecimiento = Seleccionado.NO_ESTABLECIMIENTO;

                    view.v_objCliente.v_objEstablecimiento.v_codEstablecimiento
                        = FormatUtil.convertStringToInt(_no_establecimiento);

                    view.v_objCliente.v_objEstablecimiento.v_objIndicador.v_no_agente = Seleccionado.NO_AGENTE;

                    view.v_objCliente.v_objEstablecimiento.v_objIndicador.v_cobrador = Seleccionado.COBRADOR;

                    bool _esCobrador = MiscUtils.getVariableBooleanSQLStateStringEmptyFalse(Seleccionado.IND_COBRO);

                    view.v_objCliente.v_objEstablecimiento.v_objIndicador.v_esCobrador =
                        _esCobrador;

                    await seleccionarClienteParte3();
                }

            }
        }

        /// <summary>
        /// Metodo encargado de reemplazar datos a un cliente con los datos
        /// con los datos relativos al establecimiento que se haya seleccionado
        /// </summary>
        /// <param name="Establecimiento"></param>
        /// <returns></returns>
        public async Task seleccionarClienteParte2(pnlEstablecimiento_ltvEstablecimientos Establecimiento) {

            string _no_establecimiento = Establecimiento.CODESTABLECIMIENTO;

            view.v_objCliente.v_objEstablecimiento.v_codEstablecimiento
                = FormatUtil.convertStringToInt(_no_establecimiento);

            view.v_objCliente.v_objEstablecimiento.v_objIndicador.v_no_agente = Establecimiento.NO_AGENTE;

            view.v_objCliente.v_objEstablecimiento.v_objIndicador.v_cobrador = Establecimiento.COBRADOR;

            bool _esCobrador = MiscUtils.getVariableBooleanSQLStateStringEmptyFalse(Establecimiento.IND_COBRO);

            view.v_objCliente.v_objEstablecimiento.v_objIndicador.v_esCobrador =
                _esCobrador;

            await seleccionarClienteParte3();

        }

        /// <summary>
        /// Metodo encargado de establecer datos especificos relativos a un cliente
        /// para luego redireccionar a la pantalla visita con los datos especificados
        /// Este metodo es separado de la parte 2 por los casos en los que un cliente solo
        /// tenga un unico establecimiento
        /// </summary>
        /// <returns></returns>
        internal async Task seleccionarClienteParte3() {

            Logica_ManagerCliente _manager = new Logica_ManagerCliente();

            await _manager.buscarClientePorCodigoCliente(view.v_objCliente);

            CERRADO = true;
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        internal async Task menu_mniBuscar_Click()
        {
            view.FindByName<ListView>("pnlClientes_ltvClientes").ItemsSource = new ObservableCollection<pnlClientes_ltvClientes>();

            v_tipoBusqueda = view.FindByName<Picker>("pnlClientes_cbxTipoBusqueda").SelectedItem.ToString();
            v_filtro = view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar").Text;

            Logica_ManagerCliente _manager = new Logica_ManagerCliente();

            _manager.buscarListaClienteRuteros(
                view.FindByName<ListView>("pnlClientes_ltvClientes"),
                v_tipoBusqueda,
                v_filtro
                );

            var Source = view.FindByName<ListView>("pnlClientes_ltvClientes").ItemsSource as ObservableCollection<pnlClientes_ltvClientes>;

            if (Source.Count == 1)
            {
                foreach (var _lvi in Source)
                {
                    var seleccionado = view.FindByName<ListView>("pnlClientes_ltvClientes").SelectedItem as pnlClientes_ltvClientes;

                    //if (_lvi.Focused)
                    if (seleccionado != null)
                    {
                        await seleccionarCliente();
                    }
                }
            }

            actualizarColumnasCodigo();

            ValidateHH _validateHH = new ValidateHH();

            _validateHH.emptyListView<pnlClientes_ltvClientes>(view.FindByName<ListView>("pnlClientes_ltvClientes"));
            view.FindByName<ListView>("pnlClientes_ltvClientes").SelectedItem = null;
            renderMenu();
        }

        internal async Task menu_mniAyuda_Click()
        {
            await HelpFB.closeClientWindow();
        }

        private async Task nuevoCliente()
        {
            ShowPresentacion _show = new ShowPresentacion();
            await _show.mostrarPantallaNuevoCliente();
            NewCliente = false;
            //renderComponents(false);
        }

        internal async Task menu_mniNuevoCliente_Click()
        {
            NewCliente = true;
            await nuevoCliente();
            view.FindByName<ListView>("pnlClientes_ltvClientes").SelectedItem = null;
            renderMenu();
            //await menu_mniBuscar_Click();
        }

        internal void pnlClientes_btnBorrarBuscar_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar"));
        }

        internal void pnlClientes_btnLimpiarBuscar_Click()
        {
            view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar").Text = string.Empty;
        }

        internal void pnlClientes_cbxTipoBusqueda_SelectedIndexChanged()
        {
            view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar").Focus();

            view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar").Text = string.Empty;

            view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar").Behaviors.Clear();

            if (view.FindByName<Picker>("pnlClientes_cbxTipoBusqueda").SelectedItem.ToString().Equals(VarComboBox._cbxCode))
            {
                view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar").Behaviors.Add(new JustNumeric());
                view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar").Keyboard = Keyboard.Numeric;
            }
            else
            {
                view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar").Behaviors.Add(new JustText());
                view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar").Keyboard = Keyboard.Text;
            }
        }

        internal async Task pnlClientes_ltvClientes_ItemActivete()
        {
            await seleccionarCliente();
        }

        public async Task Cerrando()
        {
            if (CERRADO)
            {

                if (v_pantallaVisita.Equals(PantallasSistema._pantallaVisita))
                {
                    await viewVisita.pintarAtributosCliente(view.v_objCliente);

                    LogicaVisitaConstructor _logica = new LogicaVisitaConstructor(viewVisita);
                    LogicaVisitaComboTipoTransaccion logica = new LogicaVisitaComboTipoTransaccion(viewVisita);

                    viewVisita.ConectarComboboxTipoTransaccion(false);
                    await _logica.Constructores(false, true, true);
                    viewVisita.ConectarComboboxTipoTransaccion(true);
                    logica.RenderizarBotonesVisita(false);
                }
                if (v_pantallaTelefono.Equals(PantallasSistema._pantallaTelefono))
                {
                    viewTelefono.controlador._objCliente = view.v_objCliente;

                    await viewTelefono.controlador.CargarDatosCliente();
                }
                if (v_pantallaReporte.Equals(PantallasSistema._pantallaReporteCDC))
                {
                    viewReporte.v_objCliente = view.v_objCliente;
                    await viewReporte.controlador.ReporteCrediticioDelCliente();
                }
                if (v_pantallaRecibo.Equals(PantallasSistema._pantallaRecibo))
                {
                    viewRecibo.establecerVariablesCliente(view.v_objCliente);
                }
                if (v_pantallaCargaIndicadores.Equals(PantallasSistema._pantallaPromedioVentas))
                {
                    if (controladorIndicadores != null)
                    {
                        controladorIndicadores.v_objCliente = view.v_objCliente;
                        controladorIndicadores.informacionIndicadoresIndividualRecargarParte2();
                    }

                    if(controladorDescuentos!= null)
                    {
                        controladorDescuentos.v_objCliente = view.v_objCliente;
                        controladorDescuentos.informacionDescuentoIndividualRecargarParte2();
                    }
                }
            }
            else
            {
                if (!NewCliente)
                {
                    if (v_pantallaVisita.Equals(PantallasSistema._pantallaVisita))
                    {
                        if (view.v_objCliente.v_nombre == string.Empty)
                        {
                            viewVisita.controlador.Cerrar();
                        }
                    }

                }
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
