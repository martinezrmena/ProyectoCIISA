using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.SystemHH.controller;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Framework.Handheld.SystemHH.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class viewAbout : ContentPage
    {
        private ctrlAbout controller = null;
        SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public viewAbout(SystemCIISA psystemCIISA)
        {
            try
            {
                controller = new ctrlAbout(this);

                InitializeComponent();

                controller.screenInicialization(psystemCIISA);
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

        private async void menu_mniClose_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlAbout_stkCerrar);
                controller.menu_mniClose_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlAbout_stkCerrar);

        }
    }
}