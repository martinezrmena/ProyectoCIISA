using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Vista;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Controlador
{
    public class ctrlClienteInactivo
    {
        private vistaClienteInactivo view { get; set; }
        public bool Cerrar = false;

        public ctrlClienteInactivo(vistaClienteInactivo pview)
        {
            view = pview;
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlCliente").Id))
            {
                view.Title = "Cliente";
            }

            ppanel.IsVisible = true;
        }

        private void renderComponents(bool pmostrar)
        {
            view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar").Focus();
        }

        internal void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);
            renderPaneles(view.FindByName<StackLayout>("pnlCliente"));
            renderComponents(false);
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

        internal void menu_mniClose_Click()
        {
            Cerrar = true;
            Application.Current.MainPage.Navigation.PopModalAsync();
        }

        internal void menu_mniAceptar_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (!_validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar"))) {

                view.v_codCliente = view.FindByName<ExtendedEntry>("pnlClientes_txtBuscar").Text;

                menu_mniClose_Click();
            }

        }
    }
}
