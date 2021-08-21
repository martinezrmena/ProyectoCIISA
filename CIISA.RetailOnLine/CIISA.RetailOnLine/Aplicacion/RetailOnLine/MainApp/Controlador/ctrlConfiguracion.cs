using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Framework.Handheld.Print;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    public class ctrlConfiguracion
    {
        private vistaConfiguracion view { get; set; }

        internal ctrlConfiguracion(vistaConfiguracion pview)
        {
            view = pview;

        }

        internal void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlConfiguracion"));
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlConfiguracion").Id))
            {
                view.Title = "Configuración";
            }

            ppanel.IsVisible = true;
        }

        internal async void pnlConfiguracion_btnPruebaImpresion_Click()
        {
            Logica_ManagerImpresora _manager = new Logica_ManagerImpresora();

            string _puertoImpresora = _manager.obtenerPuertoImpresora();

            PrintTest _printTest = new PrintTest();

            await _printTest.testPrint(_puertoImpresora);
        }

        internal void menu_mniClose_Click()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}
