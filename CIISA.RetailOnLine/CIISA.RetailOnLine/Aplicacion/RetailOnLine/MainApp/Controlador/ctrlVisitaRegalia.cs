using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    internal class ctrlVisitaRegalia
    {
        private vistaVisitaRegalia view { get; set; }
        private vistaVisita viewVisita { get; set; }
        public LogicaVisitaEventos v_logicaEventos = null;
        private Producto v_objProducto = new Producto();
        private bool CERRADO = false;

        internal ctrlVisitaRegalia(vistaVisitaRegalia pview,vistaVisita pviewVisita)
        {
            view = pview;
            viewVisita = pviewVisita;
        }

        internal ctrlVisitaRegalia(vistaVisitaRegalia pview, LogicaVisitaEventos plogicaEventos)
        {
            view = pview;
            v_logicaEventos = plogicaEventos;
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlRegalia").Id))
            {
                view.Title = "Regalía";
            }

            ppanel.IsVisible = true;
        }

        private void llenarComboBoxRegalia()
        {
            Logica_ManagerMotivo _manager = new Logica_ManagerMotivo();

            DataTable _dt = _manager.buscarMotivoPorCodigoTransaccion(ROLTransactions._regaliaSigla);

            Util _util = new Util();

            _util.fillComboBox(
                _dt,
                view.FindByName<Picker>("pnlRegalia_cbxMotivo"),
                "Descripcion"
                );
        }

        private void renderComponentesPnlRegalia()
        {
            llenarComboBoxRegalia();

            view.FindByName<Label>("pnlRegalia_lblProducto").Text =
                v_objProducto.v_codProducto
                + Simbol._hyphenWithSpaces
                + v_objProducto.descripcion();
        }

        internal void ScreenInicialization(Producto pobjProducto)
        {
            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlRegalia"));

            v_objProducto = pobjProducto;

            renderComponentesPnlRegalia();
        }

        internal void menu_mniCancelar_Click()
        {
            view.v_cancelo = true;
            CERRADO = true;
            Application.Current.MainPage.Navigation.PopModalAsync();
        }

        internal void menu_mniAgregar_Click()
        {
            v_objProducto.v_motivo = view.FindByName<Picker>("pnlRegalia_cbxMotivo").SelectedItem.ToString();
            CERRADO = true;
            Application.Current.MainPage.Navigation.PopModalAsync();
        }

        internal async Task Cerrando()
        {
            if (CERRADO)
            {
                bool _cancelo = false;
                _cancelo = view.v_cancelo;

                if (viewVisita != null)
                {
                    if (!_cancelo)
                    {
                        LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(viewVisita);

                        var Source = viewVisita.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

                        var _lvi = viewVisita.FindByName<ListView>("pnlTransacciones_ltvProductos").SelectedItem as pnlTransacciones_ltvProductos;

                        await _logica.verificarPrecio(
                            v_objProducto,
                            Source.IndexOf(_lvi),
                            true
                            );
                    }
                }

                if (v_logicaEventos != null)
                {
                    await v_logicaEventos.pnlTransacciones_ltvProductos_ColumnClickParte3(v_objProducto, _cancelo);
                }
            }
        }
    }
}
