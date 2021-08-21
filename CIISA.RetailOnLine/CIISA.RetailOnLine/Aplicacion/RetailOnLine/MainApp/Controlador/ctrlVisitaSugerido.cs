using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita;
using CIISA.RetailOnLine.Framework.External.CustomListview;
using CIISA.RetailOnLine.Framework.Handheld.Render;
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
    internal class ctrlVisitaSugerido
    {
        private vistaVisitaSugerido viewSugerido { get; set; }
        private vistaVisita viewVisita { get; set; }
        private bool CERRADO = false;

        internal ctrlVisitaSugerido(vistaVisitaSugerido pviewSugerido, vistaVisita pviewVisita)
        {
            viewSugerido = pviewSugerido;
            viewVisita = pviewVisita;
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(viewSugerido.FindByName<StackLayout>("pnlSugerido").Id))
            {
                viewSugerido.Title = "Sugerido";
            }

            ppanel.IsVisible = true;
        }

        internal void ScreenInicialization()
        {
            RenderWindows.paintWindow(viewSugerido);

            renderPaneles(viewSugerido.FindByName<StackLayout>("pnlSugerido"));

            viewSugerido.v_listaProductos = viewVisita.FindByName<ListView>("pnlTransacciones_ltvProductos");

            viewSugerido.FindByName<ListView>("pnlSugerido_ltvSugerido").ItemsSource = new SelectableObservableCollection<pnlSugerido_ltvSugerido>();

            Logica_ManagerSugerido _manager = new Logica_ManagerSugerido();

            _manager.buscarListaSugeridos(
                viewSugerido.FindByName<ListView>("pnlSugerido_ltvSugerido"),
                viewVisita.controlador.v_objCliente
                );

        }

        internal void pnlSugerido_chkTodas_CheckStateChanged()
        {
            var Source = viewSugerido.FindByName<ListView>("pnlSugerido_ltvSugerido").ItemsSource as SelectableObservableCollection<pnlSugerido_ltvSugerido>;

            foreach (var _lvi in Source)
            {
                _lvi.IsSelected = viewSugerido.FindByName<CheckBox>("pnlSugerido_chkTodas").Checked;
            }
        }

        internal async Task menu_mniAgregar_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (!_validateHH.emptyListView<pnlSugerido_ltvSugerido>(viewSugerido.FindByName<ListView>("pnlSugerido_ltvSugerido")))
            {
                var Source = viewSugerido.FindByName<ListView>("pnlSugerido_ltvSugerido").ItemsSource as SelectableObservableCollection<pnlSugerido_ltvSugerido>;

                foreach (var _lvi in Source)
                {
                    if (_lvi.IsSelected)
                    {
                        string _codProducto = _lvi.Data.CODPRODUCTO;

                        Logica_ManagerProducto _manager = new Logica_ManagerProducto();

                        Producto _objProducto = _manager.buscarProductoPorCodigoProducto(_codProducto);

                        LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(viewVisita);

                        var Source2 = viewVisita.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

                        await _logica.verificarPrecio(_objProducto, Source2.Count - 1, false);
                    }
                }

                CERRADO = true;
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
        }

        internal async Task menu_mniCancelar_Click()
        {
            CERRADO = true;
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        internal void Cerrando()
        {
            if (CERRADO)
            {
                ListView _listaProductos = new ListView();

                _listaProductos = viewSugerido.v_listaProductos;

                LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(viewVisita);

                _logica.actualizarProductoInventario();

                LogicaVisitaRender _logicaRender = new LogicaVisitaRender(viewVisita);

                _logicaRender.renderMenu(false);

                LogicaVisitaActualizar _logicaActualizar = new LogicaVisitaActualizar(viewVisita);

                _logicaActualizar.actualizarColumnas();
                _logicaActualizar.actualizarTotal();
            }
        }
    }
}
