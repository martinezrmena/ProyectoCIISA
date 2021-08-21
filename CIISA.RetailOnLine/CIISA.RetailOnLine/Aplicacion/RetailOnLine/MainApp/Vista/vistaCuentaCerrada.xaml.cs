using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class vistaCuentaCerrada : ContentPage
	{
        private ctrlCuentaCerrada controlador = null;

        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaCuentaCerrada()
        {
            controlador = new ctrlCuentaCerrada(this);

            InitializeComponent();

            controlador.ScreenInicialization();
        }

        private async Task menu_mniBuscar_Clicked(object sender, EventArgs e)
        {
            await simulateClickGestures.SelectedStack(pnlCuentas_stckBuscar);

            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    controlador.menu_btnBuscar_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }

            await simulateClickGestures.NoSelectedStack(pnlCuentas_stckBuscar);
        }

        private async Task menu_mniClose_Clicked(object sender, EventArgs e)
        {
            await simulateClickGestures.SelectedStack(pnlCuentas_stkCerrar);

            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    controlador.menu_btnCerrar_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }

            await simulateClickGestures.NoSelectedStack(pnlCuentas_stkCerrar);
        }

        private async Task pnlCuentas_btnBorrarBuscar_Clicked(object sender, EventArgs e)
        {                  
            try
            {
                controlador.pnlCuentas_btnBorrarBuscar_Click();
            }

            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlCuentas_btnLimpiarBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlCuentas_btnLimpiarBuscar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void pnlCuentas_ltvCuentas_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            try
            {
                controlador.pnlCuentas_txtCodCliente_Focus();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
    }
}