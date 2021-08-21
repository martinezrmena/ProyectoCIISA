using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaInventarioTeorico : ContentPage
    {
        private ctrlInventarioTeorico controlador = null;

        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaInventarioTeorico()
        {
            controlador = new ctrlInventarioTeorico(this);

            InitializeComponent();

            controlador.ScreenInicialization();
        }

        private async Task menu_mniMenu_mniInventarioTeorico_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlInventario_stkInventarioTeorico);
                await controlador.menu_mniMenu_mniInventarioTeorico_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlInventario_stkInventarioTeorico);
        }

        private async Task menu_mniMenu_mniInventarioDisponible_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlInventario_stkInventarioDisponible);
                await controlador.menu_mniMenu_mniInventarioDisponible_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlInventario_stkInventarioDisponible);
        }

        private async Task menu_mniClose_Clicked(object sender, EventArgs e)
        {            
            try
            {
                await simulateClickGestures.SelectedStack(pnlInventario_stkCerrar);
                await controlador.menu_mniClose_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlInventario_stkCerrar);
        }
    }
}