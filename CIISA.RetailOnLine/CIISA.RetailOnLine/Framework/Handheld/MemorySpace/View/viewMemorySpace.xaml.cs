using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.MemorySpace.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Framework.Handheld.MemorySpace.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class viewMemorySpace : ContentPage
    {
        private ctrlMemoryState controller = null;
        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();
        internal SystemCIISA systemCIISA;

        public viewMemorySpace(SystemCIISA psystemCIISA)
        {
            InitializeComponent();
            systemCIISA = psystemCIISA;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                controller = new ctrlMemoryState(this);

                await controller.screenInicialization(systemCIISA);
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

                await simulateClickGestures.SelectedStack(pnlMemory_stkCerrar);
                await controller.menu_mniClose_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlMemory_stkCerrar);
        }
    }
}