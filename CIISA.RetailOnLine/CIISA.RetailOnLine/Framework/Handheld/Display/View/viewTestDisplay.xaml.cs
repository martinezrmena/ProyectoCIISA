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
    public partial class viewTestDisplay : ContentPage
    {
        private ctrlTestDisplay controller = null;
        SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public viewTestDisplay(SystemCIISA psystemCIISA)
        {
            try
            {
                controller = new ctrlTestDisplay(this);

                InitializeComponent();

                controller.screenInicialization(psystemCIISA);

                controller.constructor();
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


        private async Task menu_mniLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                controller.constructor();
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
                await simulateClickGestures.SelectedStack(pnlTestDisplay_stkCerrar);
                await controller.menu_mniClose_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlTestDisplay_stkCerrar);
        }

    }
}