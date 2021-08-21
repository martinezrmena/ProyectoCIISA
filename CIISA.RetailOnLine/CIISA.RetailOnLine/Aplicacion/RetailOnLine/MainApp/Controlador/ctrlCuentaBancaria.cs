using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    public class ctrlCuentaBancaria
    {
        private vistaCuentaBancaria view { get; set; }

        internal ctrlCuentaBancaria(vistaCuentaBancaria pview)
        {
            view = pview;
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlCuentaBancaria"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlCuentaBancaria").Id))
            {
                view.Title = "Cuenta Bancaria";
            }

            ppanel.IsVisible = true;
        }

        internal void buscarCuentaBancaria()
        {
            view.FindByName<ListView>("pnlCuentaBancaria_ltvCuentas").ItemsSource = new ObservableCollection<pnlCuentaBancaria_ltvCuentas>();

            Logica_ManagerBanco _manager = new Logica_ManagerBanco();

            _manager.buscarListaCuentasBancarias(
                view.FindByName<ListView>("pnlCuentaBancaria_ltvCuentas"),
                view.FindByName<Entry>("pnlCuentaBancaria_txtNombreBanco").Text
                );
        }

        internal void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlCuentaBancaria"));

            buscarCuentaBancaria();
        }

        internal void pnlCuentaBancaria_btnBorrarBuscar_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlCuentaBancaria_txtNombreBanco"));
        }

        internal void pnlCuentaBancaria_btnLimpiarBuscar_Click()
        {
            view.FindByName<ExtendedEntry>("pnlCuentaBancaria_txtNombreBanco").Text = string.Empty;
        }

        internal void menu_btnCerrar_Click()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        internal void menu_btnBuscar_Click()
        {
            buscarCuentaBancaria();
        }

        internal async Task pnlCuentaBancaria_btnImprimir_Click()
        {

            if (await LogMessages._dialogResultYes("¿Desea imprimir las cuentas bancarias?", "Imprimir"))
            {
                ProcesoImpresion _impresion = new ProcesoImpresion();

                await _impresion.imprimirReporteCuentasBancarias();
            }
        }

        internal void pnlCuentaBancaria_txtNombreBanco_Focus() {

            view.FindByName<ExtendedEntry>("pnlCuentaBancaria_txtNombreBanco").Focus();
        }
    }
}
