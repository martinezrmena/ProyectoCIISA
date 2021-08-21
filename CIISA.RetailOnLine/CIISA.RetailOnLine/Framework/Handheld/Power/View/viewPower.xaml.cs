using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using CIISA.RetailOnLine.Framework.Handheld.Power.Controller;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Framework.Handheld.Power.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class viewPower : ContentPage
	{
        private ctrlPower controller = null;

        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public viewPower()
        {
            try
            {
                controller = new ctrlPower(this);

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

        private async void menu_mniClose_Click(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlPower_stkCerrar);
                await controller.Close();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlPower_stkCerrar);
        }

        private async void btnSuspend_Click(object sender, EventArgs e)
        {
            try
            {
                controller.btnSuspend_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void pnlPower_btnFullOn_Click(object sender, EventArgs e)
        {
            try
            {
                controller.pnlPower_btnFullOn_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void menu_mniConsultarEstado_Click(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlPower_stkConsultarEstado);
                controller.menu_mniConsultarEstado_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlPower_stkConsultarEstado);
        }

    }
}