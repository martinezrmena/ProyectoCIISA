using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using CIISA.RetailOnLine.Framework.Handheld.Backlight.Controller;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Framework.Handheld.Backlight.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class viewBackLight : ContentPage
	{
        private ctrlBacklight controller = null;
        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public viewBackLight()
        {
            try
            {
                controller = new ctrlBacklight(this);

                InitializeComponent();

                controller.screenInicialization();
            }
            catch (Exception ex)
            {
                ExceptionHandled(ex);
            }
        }

        private async void ExceptionHandled(Exception ex)
        {

            await ExceptionManager.ExceptionHandling(ex);
        }

        private async Task chkBacklight_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                controller.chkBacklight_CheckStateChanged();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task trbBacklightLevel_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                controller.trbBacklightLevel_ValueChanged();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

        }

        private async Task menu_mniClose_Click(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlBackLight_stkCerrar);
                await controller.menu_mniClose_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlBackLight_stkCerrar);
        }

    }
}