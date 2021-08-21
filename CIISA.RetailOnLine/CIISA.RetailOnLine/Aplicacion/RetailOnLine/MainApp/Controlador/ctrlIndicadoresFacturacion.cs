using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Framework.Handheld.Render;
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
    public class ctrlIndicadoresFacturacion
    {
        private vistaIndicadoresFacturacion view { get; set; }

        internal ctrlIndicadoresFacturacion(vistaIndicadoresFacturacion pview)
        {
            view = pview;

        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlIndicadores"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlIndicadores").Id))
            {
                view.Title = "Indicadores Facturación";
            }

            ppanel.IsVisible = true;
        }

        private void buscarIndicadores()
        {
            view.FindByName<ListView>("pnlIndicadores_ltvIndicadores").ItemsSource = new ObservableCollection<pnlIndicadores_ltvIndicadores>();

            Logica_ManagerIndicador _manager = new Logica_ManagerIndicador();

            _manager.buscarListaIndicadoresFacturacion(
                view.FindByName<ListView>("pnlIndicadores_ltvIndicadores"),
                view.FindByName<Entry>("pnlIndicadores_txtCodCliente").Text
                );
        }

        internal void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlIndicadores"));

            buscarIndicadores();
        }

        internal void menu_btnCerrar_Click()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        internal void menu_btnBuscar_Click()
        {
            buscarIndicadores();
        }

        internal void pnlIndicadores_txtCodCliente_Focus() {

            view.FindByName<ExtendedEntry>("pnlIndicadores_txtCodCliente").Focus();
        }
    }
}
