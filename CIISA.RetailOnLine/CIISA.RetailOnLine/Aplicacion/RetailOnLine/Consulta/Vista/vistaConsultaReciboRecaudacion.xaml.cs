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
    public partial class vistaConsultaReciboRecaudacion : ContentPage
    {
        private ctrlConsultaReciboRecaudacion controlador = null;
        internal ITaskActivity DPB = DependencyService.Get<ITaskActivity>();
        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaConsultaReciboRecaudacion()
        {
            controlador = new ctrlConsultaReciboRecaudacion(this);

            InitializeComponent();

            DPB.StartScreenInicializationReciboRecaudacion(controlador);
        }

        private void pnlConsultaDocumentos_nudConsulta_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            VisualStepper.Text = e.NewValue.ToString();
        }

        private async void menu_mniConsultar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlConsultaDocumentos_stkConsultar);
                DPB.StartConsultaReciboRecaudacion(controlador);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlConsultaDocumentos_stkConsultar);
        }

        private async Task menu_mniClose_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlConsultaDocumentos_stkCerrar);
                await controlador.menu_mniClose_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlConsultaDocumentos_stkCerrar);
        }

        private async Task pnlConsultaDocumentos_btnImprimir_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await controlador.pnlConsultaReciboRecaudacion_btnImprimir_Click();
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

        private void pnlConsultaDocumentos_btnPruebaConexion_Clicked(object sender, EventArgs e)
        {
            DPB.PruebaConexion();
        }

        private async Task pnlConsultaDocumentos_btnNomenclatura_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlConsultaReciboRecaudacion_btnNomenclatura_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
    }
}