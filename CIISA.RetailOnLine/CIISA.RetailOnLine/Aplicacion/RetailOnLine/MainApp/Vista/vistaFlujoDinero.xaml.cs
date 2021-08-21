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
    public partial class vistaFlujoDinero : ContentPage
    {
        private ctrlFlujoDinero controlador = null;

        SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaFlujoDinero()
        {
            controlador = new ctrlFlujoDinero(this);

            InitializeComponent();

            controlador.ScreenInicialization();
        }

        private async Task menu_mniImprimir_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlFlujoDinero_stkImprimir);
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await controlador.menu_mniImprimir_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally
            {

                UserDialogs.Instance.HideLoading();
            }

            await simulateClickGestures.NoSelectedStack(pnlFlujoDinero_stkImprimir);
        }
   

        private async Task menu_mniClose_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlFlujoDinero_stkCerrar);
                controlador.menu_mniClose_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlFlujoDinero_stkCerrar);
        }
    }
}