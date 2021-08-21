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
    public partial class vistaBitacora : ContentPage
    {

        public ctrlBitacora controlador = null;

        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaBitacora()
        {
            controlador = new ctrlBitacora(this);

            InitializeComponent();

            controlador.ScreenInicialization();
        }


        private async Task Salir_Tapped(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlBitacora_Salir);
                await controlador.Close_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlBitacora_Salir);
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await controlador.ProcesarInformacion();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }
    }
}