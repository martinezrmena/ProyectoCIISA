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
    public partial class vistaAnulaciones : ContentPage
    {
        public ctrlAnulaciones controlador = null;

        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaAnulaciones()
        {
            controlador = new ctrlAnulaciones(this);

            InitializeComponent();

            controlador.ScreenInicialization();
        }

        private async Task menu_mniBuscar_Clicked(object sender, EventArgs e)
        {            
            try
            {

                await simulateClickGestures.SelectedStack(pnlAnulacion_stkBuscar);
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.menu_mniBuscar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }

            await simulateClickGestures.NoSelectedStack(pnlAnulacion_stkBuscar);
        }

        private async Task menu_mniAnular_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlAnulacion_stkAnular);
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await controlador.menu_mniAnular_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }

            await simulateClickGestures.NoSelectedStack(pnlAnulacion_stkAnular);
        }

        private async Task pnlAnulacion_ltvTransacciones_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await controlador.pnlAnulacion_ltvTransacciones_ItemActivate();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            try
            {
                await controlador.Cerrando();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
    }
}