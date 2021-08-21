using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
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
    public class ctrlCuentaCerrada
    {
        private vistaCuentaCerrada view { get; set; }

        internal ctrlCuentaCerrada(vistaCuentaCerrada pview)
        {
            view = pview;
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlCuentas"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlCuentas").Id))
            {
                view.Title = "Cuenta Cerrada";
            }

            ppanel.IsVisible = true;
        }

        private void buscarCliente()
        {
            view.FindByName<ListView>("pnlCuentas_ltvCuentas").ItemsSource = new ObservableCollection<pnlCuentas_ltvCuentas>();

            Logica_ManagerCuentaCerrada _manager = new Logica_ManagerCuentaCerrada();

            _manager.buscarListaCuentasCerradas(
                view.FindByName<ListView>("pnlCuentas_ltvCuentas"),
                view.FindByName<ExtendedEntry>("pnlCuentas_txtCodCliente").Text
                );
        }

        internal void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlCuentas"));

            buscarCliente();
        }

        internal void menu_btnCerrar_Click()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        internal void menu_btnBuscar_Click()
        {
            buscarCliente();
        }

        internal void pnlCuentas_btnBorrarBuscar_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlCuentas_txtCodCliente"));
        }

        internal void pnlCuentas_btnLimpiarBuscar_Click()
        {
            view.FindByName<ExtendedEntry>("pnlCuentas_txtCodCliente").Text = string.Empty;
        }

        internal void pnlCuentas_txtCodCliente_Focus() {

            view.FindByName<ExtendedEntry>("pnlCuentas_txtCodCliente").Focus();
        }
    }
}
