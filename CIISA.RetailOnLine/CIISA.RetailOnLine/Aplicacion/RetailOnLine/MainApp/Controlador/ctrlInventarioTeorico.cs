using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    internal class ctrlInventarioTeorico
    {
        private vistaInventarioTeorico view { get; set; }

        internal ctrlInventarioTeorico(vistaInventarioTeorico pview)
        {
            view = pview;
        }

        public void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlInventario").Id))
            {
                view.Title = "Inventario Teórico";
            }

            ppanel.IsVisible = true;
        }

        internal async void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlInventario"));

            Logica_ManagerInventario _manager = new Logica_ManagerInventario();

            _manager.recalcularProductoDisponibleEnInventario();

            _manager.buscarListaInventarioTeorico(view.FindByName<ListView>("pnlInventario_ltvProducto"));

            var Source = view.FindByName<ListView>("pnlInventario_ltvProducto").ItemsSource as ObservableCollection<pnlInventario_ltvProducto>;

            if (Source.Count > 0)
            {
            }
            else
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();

                await _logMessageAttention.generalAttention(
                    "Debe cargar el inventario."
                    + Environment.NewLine
                    + Environment.NewLine
                    + "Vaya al menú principal, seleccione la opción carga, ahí presione el botón 6.Inventario"
                    );
            }
        }

        internal async Task menu_mniClose_Click()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        internal async Task menu_mniMenu_mniInventarioDisponible_Click()
        {

            if (await LogMessages._dialogResultYes("¿Desea imprimir el inventario disponible?", "Imprimir"))
            {
                ProcesoImpresion _impresion = new ProcesoImpresion();

                await _impresion.imprimirReporteInventarioConExistencias("INVENTARIO CON EXISTENCIAS");
            }
        }

        internal async Task menu_mniMenu_mniInventarioTeorico_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea imprimir el inventario teórico?", "Imprimir"))
            {
                ProcesoImpresion _impresion = new ProcesoImpresion();

                await _impresion.imprimirReporteInventarioTeorico();
            }
        }
    }
}
