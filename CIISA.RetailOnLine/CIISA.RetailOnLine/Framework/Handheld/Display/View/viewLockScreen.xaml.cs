using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Display.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Framework.Handheld.Display.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class viewLockScreen : ContentPage
    {
        private ctrlLookScreen controller = null;
        SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public viewLockScreen(SystemCIISA psystemCIISA)
        {

            try
            {
                controller = new ctrlLookScreen(this);

                InitializeComponent();

                controller.screenInicialization();

                controller.turnOff();
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

        private async Task menu_mniUnlock_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlLockScreen_stkLock);
                controller.menu_mniUnlock_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlLockScreen_stkLock);
        }

        private async Task pnlLockScreen_btnGreen_Clicked(object sender, EventArgs e)
        {
            try
            {
                controller.pnlLockScreen_btnGreen_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlLockScreen_btnYellow_Clicked(object sender, EventArgs e)
        {
            try
            {
                controller.pnlLockScreen_btnYellow_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlLockScreen_btnRed_Clicked(object sender, EventArgs e)
        {
            try
            {
                controller.pnlLockScreen_btnRed_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasBackButton(this, false);
        }
    }
}