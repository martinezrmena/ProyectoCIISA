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
	public partial class vistaCuentaBancaria : ContentPage
	{
        private ctrlCuentaBancaria controlador = null;

        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaCuentaBancaria()
        {
            controlador = new ctrlCuentaBancaria(this);

            InitializeComponent();

            controlador.ScreenInicialization();
        }

        private async Task menu_mniBuscar_Clicked(object sender, EventArgs e)
        {
            await simulateClickGestures.SelectedStack(pnlCuentaBancaria_stckBuscar);

            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    controlador.buscarCuentaBancaria();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }

            await simulateClickGestures.NoSelectedStack(pnlCuentaBancaria_stckBuscar);
        }

        private async Task menu_mniClose_Clicked(object sender, EventArgs e)
        {

            await simulateClickGestures.SelectedStack(pnlCuentaBancaria_stkCerrar);

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

            await simulateClickGestures.SelectedStack(pnlCuentaBancaria_stkCerrar);
        }

        private async Task pnlCuentaBancaria_btnBorrarBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlCuentaBancaria_btnBorrarBuscar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlCuentaBancaria_btnLimpiarBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlCuentaBancaria_btnLimpiarBuscar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlCuentaBancaria_btnImprimir_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await controlador.pnlCuentaBancaria_btnImprimir_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
        }

        private async void pnlCuentaBancaria_ltvCuentas_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            try
            {
                controlador.pnlCuentaBancaria_txtNombreBanco_Focus();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
    }
}