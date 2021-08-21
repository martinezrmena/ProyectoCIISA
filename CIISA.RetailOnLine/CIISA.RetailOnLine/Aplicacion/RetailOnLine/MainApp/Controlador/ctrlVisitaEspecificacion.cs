using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Handheld.Format;
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
    internal class ctrlVisitaEspecificacion
    {
        private vistaVisitaEspecificacion view { get; set; }
        private vistaVisita viewVisita { get; set; }

        private Producto v_objProducto = new Producto();
        private bool CERRADO = false;

        internal ctrlVisitaEspecificacion(vistaVisitaEspecificacion pview,vistaVisita pviewVisita)
        {
            view = pview;
            viewVisita = pviewVisita;
        }

        public void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlEspecificacion").Id))
            {
                view.Title = "Especificación";
            }

            ppanel.IsVisible = true;
        }

        private void llenarComboBoxEspecificacion()
        {
            Logica_ManagerEspecificacion _manager = new Logica_ManagerEspecificacion();

            DataTable _dt = _manager.buscarEspecificacion();

            Util _util = new Util();

            _util.fillComboBox(
                _dt,
                view.FindByName<Picker>("pnlEspecificacion_cbxEspecificacion"),
                "Descripcion"
                );
        }

        private void llenarComboBoxEmbalaje(Picker pcomboBox, string pcodArticulo)
        {
            DataTable _dt = null;

            Logica_ManagerEmbalaje _manager = new Logica_ManagerEmbalaje();

            _dt = _manager.buscarEmbalajePorArticulo(pcodArticulo);


            Util _util = new Util();

            _util.fillComboBox(
                _dt,
                pcomboBox,
                "Embalaje"
                );
        }

        private void especificacionProducto()
        {
            view.FindByName<Label>("pnlEspecificacion_lblProducto").Text =
                v_objProducto.v_codProducto
                + Simbol._hyphenWithSpaces
                + v_objProducto.descripcion();
        }

        private void cargarEspecificacion()
        {
            string _especificacion = v_objProducto.v_especificacionOV;

            Logica_ManagerEspecificacion _manager = new Logica_ManagerEspecificacion();

            bool _existeEspecificacion = _manager.BuscarEspecificacionPorDescripcion(_especificacion);

            if (_existeEspecificacion)
            {
                view.FindByName<Picker>("pnlEspecificacion_cbxEspecificacion").SelectedItem = _especificacion;
                view.FindByName<ExtendedEntry>("pnlEspecificacion_txtEspecificacion").Text = string.Empty;
            }
            else
            {
                if (_especificacion.Equals(string.Empty))
                {
                }
                else
                {
                    view.FindByName<Picker>("pnlEspecificacion_cbxEspecificacion").SelectedItem = "OTRO";
                    view.FindByName<ExtendedEntry>("pnlEspecificacion_txtEspecificacion").Text = _especificacion;
                }
            }
        }

        private void cargarEmbalaje()
        {
            Logica_ManagerEmbalaje _manager = new Logica_ManagerEmbalaje();

            DataTable _dt = _manager.buscarEmbalajePorArticulo(v_objProducto.v_codProducto);

            if (_dt.Rows != null)
            {
                if (_dt.Rows.Count > 0)
                {
                    view.FindByName<Picker>("pnlEspecificacion_cbxEmbalaje").IsEnabled = true;
                }
                else
                {
                    view.FindByName<Picker>("pnlEspecificacion_cbxEmbalaje").IsEnabled = false;
                }
            }
        }

        public void renderComponents()
        {
            llenarComboBoxEspecificacion();

            llenarComboBoxEmbalaje(view.FindByName<Picker>("pnlEspecificacion_cbxEmbalaje"), v_objProducto.v_codProducto);

            especificacionProducto();

            cargarEspecificacion();

            cargarEmbalaje();
        }

        internal void ScreenInicialization(Producto pobjProducto)
        {
            v_objProducto = pobjProducto;

            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlEspecificacion"));

            renderComponents();
        }

        internal void pnlEspecificacion_cbxEspecificacion_SelectedIndexChanged()
        {
            if (view.FindByName<Picker>("pnlEspecificacion_cbxEspecificacion").SelectedItem.ToString().Equals(EspecificacionProducto._otroNombre))
            {
                view.FindByName<ExtendedEntry>("pnlEspecificacion_txtEspecificacion").IsEnabled = true;
                RenderPaint.paintWhiteBackgroundTextBox(view.FindByName<ExtendedEntry>("pnlEspecificacion_txtEspecificacion"));
            }
            else
            {
                view.FindByName<ExtendedEntry>("pnlEspecificacion_txtEspecificacion").IsEnabled = false;
                view.FindByName<ExtendedEntry>("pnlEspecificacion_txtEspecificacion").Text = string.Empty;
                RenderPaint.paintGrayBackgroundTextBox(view.FindByName<ExtendedEntry>("pnlEspecificacion_txtEspecificacion"));
            }
        }

        internal void menu_mniCancelar_Click()
        {
            CERRADO = true;
            Application.Current.MainPage.Navigation.PopModalAsync();
        }

        internal void menu_mniAgregar_Click()
        {
            if (view.FindByName<Picker>("pnlEspecificacion_cbxEspecificacion").SelectedItem.ToString().Equals(EspecificacionProducto._otroNombre))
            {
                ValidateHH _validateHH = new ValidateHH();

                if (!_validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlEspecificacion_txtEspecificacion")))
                {
                    v_objProducto.v_especificacionOV =
                        view.FindByName<ExtendedEntry>("pnlEspecificacion_txtEspecificacion").Text;
                }
            }
            else
            {
                v_objProducto.v_especificacionOV =
                    view.FindByName<Picker>("pnlEspecificacion_cbxEspecificacion").SelectedItem.ToString();
            }

            if (view.FindByName<Picker>("pnlEspecificacion_cbxEmbalaje").IsEnabled)
            {
                decimal _embalaje = FormatUtil.convertStringToDecimal(view.FindByName<Picker>("pnlEspecificacion_cbxEmbalaje").SelectedItem.ToString());

                v_objProducto.v_embalaje = _embalaje;
            }

            view.v_guardar = true;

            CERRADO = true;
            Application.Current.MainPage.Navigation.PopModalAsync();
        }

        internal async Task Cerrando()
        {
            if (CERRADO)
            {
                bool _guardar = false;

                _guardar = view.v_guardar;

                if (_guardar)
                {
                    LogicaVisitaLtvProducto _logicaVisitaProducto = new LogicaVisitaLtvProducto(viewVisita);

                    var Source = viewVisita.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

                    var _lvi = viewVisita.FindByName<ListView>("pnlTransacciones_ltvProductos").SelectedItem as pnlTransacciones_ltvProductos;

                    await _logicaVisitaProducto.verificarPrecio(
                        v_objProducto,
                        Source.IndexOf(_lvi),
                        true
                        );
                }
            }
        }
    }
}
