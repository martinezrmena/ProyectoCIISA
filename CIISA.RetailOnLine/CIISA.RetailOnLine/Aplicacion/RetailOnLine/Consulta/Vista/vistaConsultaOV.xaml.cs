using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Consulta.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Helpers;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Consulta.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaConsultaOV : ContentPage
    {
        private ctrlConsultaOV controlador = null;
        internal ITaskActivity DPB = DependencyService.Get<ITaskActivity>();
        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaConsultaOV()
        {
            controlador = new ctrlConsultaOV(this);

            InitializeComponent();

            DPB.StartScreenInicializationConsultarOV(controlador);
        }

        private void pnlConsultaOV_nudConsulta_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            VisualStepper.Text = e.NewValue.ToString();
        }

        private async Task menu_mniConsultar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlConsultaOV_stkConsultar);
                DPB.StartConsultaOV(controlador);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlConsultaOV_stkConsultar);
        }

        private async Task menu_mniClose_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlConsultaOV_stkCerrar);
                controlador.menu_mniClose_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlConsultaOV_stkCerrar);
        }

        private async Task pnlConsultaOV_btnImprimir_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await controlador.pnlConsultaOV_btnImprimir_Click();
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

        private async Task pnlConsultaOV_btnPruebaConexion_Clicked(object sender, EventArgs e)
        {
            try
            {
                DPB.PruebaConexion();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
    }
}