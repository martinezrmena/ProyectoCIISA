using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    internal class ctrlVisitaDevolucion
    {
        private vistaVisitaDevolucion view { get; set; }
        private vistaVisita viewVisita { get; set; }
        private bool CERRADO = false;
        public LogicaVisitaEventos v_LogicaEventos = null;

        internal ctrlVisitaDevolucion(vistaVisitaDevolucion pview,vistaVisita pvisita)
        {
            view = pview;
            viewVisita = pvisita;
        }

        internal ctrlVisitaDevolucion(vistaVisitaDevolucion pview, LogicaVisitaEventos pLogicaEventos)
        {
            view = pview;
            v_LogicaEventos = pLogicaEventos;
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlDevolucion").Id))
            {
                view.Title = "Devolucion";
            }

            ppanel.IsVisible = true;
        }

        private void llenarComboBoxEstadoProducto()
        {
            view.FindByName<Picker>("pnlDevolucion_cbxMotivo").Items.Clear();

            Logica_ManagerMotivo _manager = new Logica_ManagerMotivo();

            DataTable _dt = _manager.buscarMotivoPorCodigoTransaccion(
                                ROLTransactions._devolucionSigla
                                );

            Util _util = new Util();

            _util.fillComboBox(
                _dt,
                view.FindByName<Picker>("pnlDevolucion_cbxMotivo"),
                "Descripcion"
                );
        }

        private void loadData()
        {
            view.FindByName<Label>("pnlDevolucion_lblProducto").Text =
                        view.v_objProducto.v_codProducto
                        + Simbol._hyphenWithSpaces
                        + view.v_objProducto.descripcion();

            view.FindByName<ExtendedEntry>("pnlDevolucion_txtComentario").Text =
                            view.v_objProducto.v_especificacionOV;

            view.FindByName<Picker>("pnlDevolucion_cbxMotivo").SelectedItem =
                view.v_objProducto.v_motivo;

            string _state =
                view.v_objProducto.v_estado;

            if (_state.Contains(Pedido._devolucionBuena))
            {
                view.FindByName<CustomRadioButton>("pnlDevolucion_rdbBueno").Checked = true;
                view.FindByName<CustomRadioButton>("pnlDevolucion_rdbMalo").Checked = false;
            }
            else
            {
                view.FindByName<CustomRadioButton>("pnlDevolucion_rdbBueno").Checked = false;
                view.FindByName<CustomRadioButton>("pnlDevolucion_rdbMalo").Checked = true;
            }
        }

        private void renderComponents()
        {
            view.FindByName<CustomRadioButton>("pnlDevolucion_rdbBueno").Text = Pedido._devolucionBuena;
            view.FindByName<CustomRadioButton>("pnlDevolucion_rdbMalo").Text = Pedido._devolucionMala;

            llenarComboBoxEstadoProducto();

            loadData();

            view.FindByName<Picker>("pnlDevolucion_cbxMotivo").Focus();
        }

        private void renderMenu(bool pmodificarProducto)
        {
            //view.ToolbarItems.Clear();
            ClearMenuBottomBar();

            if (pmodificarProducto)
            {
                //view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniAgregar"));
                view.FindByName<StackLayout>("pnlDevolucion_stkAgregar").IsVisible = true;
                //view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniCancelar"));
                view.FindByName<StackLayout>("pnlDevolucion_stkCancelar").IsVisible = true;
            }
            else
            {
                //view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniAgregar"));
                view.FindByName<StackLayout>("pnlDevolucion_stkAgregar").IsVisible = true;
            }
        }

        internal void ClearMenuBottomBar() {

            view.FindByName<StackLayout>("pnlDevolucion_stkCancelar").IsVisible = false;
            view.FindByName<StackLayout>("pnlDevolucion_stkAgregar").IsVisible = false;
        }

        internal void ScreenInicialization(Producto pobjProducto, bool pmodificarProducto)
        {
            view.v_objProducto = pobjProducto;

            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlDevolucion"));

            renderComponents();

            renderMenu(pmodificarProducto);

            view.FindByName<Picker>("pnlDevolucion_cbxMotivo").SelectedIndex = 0;
        }

        internal void pnlDevolucion_rdbBueno_CheckedChanged()
        {
            view.FindByName<Picker>("pnlDevolucion_cbxMotivo").IsEnabled = false;
            view.FindByName<Picker>("pnlDevolucion_cbxMotivo").SelectedIndex = 0;

            if (view.FindByName<CustomRadioButton>("pnlDevolucion_rdbBueno").Checked)
            {
                view.FindByName<CustomRadioButton>("pnlDevolucion_rdbBueno").Checked = true;
                view.FindByName<CustomRadioButton>("pnlDevolucion_rdbMalo").Checked = false;
            }
        }

        internal void pnlDevolucion_rdbMalo_CheckedChanged()
        {
            view.FindByName<Picker>("pnlDevolucion_cbxMotivo").IsEnabled = true;

            if (view.FindByName<CustomRadioButton>("pnlDevolucion_rdbMalo").Checked)
            {
                view.FindByName<CustomRadioButton>("pnlDevolucion_rdbBueno").Checked = false;
                view.FindByName<CustomRadioButton>("pnlDevolucion_rdbMalo").Checked = true;
            }
        }

        private string obtenerEstadoDevolucion()
        {
            if (view.FindByName<CustomRadioButton>("pnlDevolucion_rdbBueno").Checked)
            {
                return Pedido._devolucionBuena;
            }
            else
            {
                return Pedido._devolucionMala;
            }
        }

        internal void menu_mniAgregar_Click()
        {
            view.v_objProducto.v_especificacionOV = view.FindByName<ExtendedEntry>("pnlDevolucion_txtComentario").Text;

            view.v_objProducto.v_motivo = view.FindByName<Picker>("pnlDevolucion_cbxMotivo").SelectedItem.ToString();

            string _state = obtenerEstadoDevolucion();

            view.v_objProducto.v_estado = _state;

            CERRADO = true;
            Application.Current.MainPage.Navigation.PopModalAsync();
        }

        internal void pnlDevolucion_btnLimpiarBuscar_Click()
        {
            view.FindByName<ExtendedEntry>("pnlDevolucion_txtComentario").Text = string.Empty;
        }

        internal void menu_mniCancelar_Click()
        {
            //view.FindByName<Picker>("pnlDevolucion_cbxMotivo").Focus();

            CERRADO = true;
            Application.Current.MainPage.Navigation.PopModalAsync();
        }

        internal async Task Cerrando()
        {
            if (CERRADO)
            {
                if (viewVisita != null)
                {
                    LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(viewVisita);
                    var Source = viewVisita.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;
                    var _lvi = viewVisita.FindByName<ListView>("pnlTransacciones_ltvProductos").SelectedItem as pnlTransacciones_ltvProductos;

                    await _logica.verificarPrecio(
                        view.v_objProducto,
                        Source.IndexOf(_lvi),
                        true
                        );
                }
                if (v_LogicaEventos != null)
                {
                    await v_LogicaEventos.pnlTransacciones_ltvProductos_ColumnClickParte3(view.v_objProducto, false);
                }
            }
        }
    }
}
