using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Controlador;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaCarga : ContentPage
    {
        private ctrlCarga controlador = null;

        internal bool v_cerrado = false;

        public vistaCarga(int ptipoCarga)
        {
            controlador = new ctrlCarga(this);

            InitializeComponent();

            controlador.ScreenInicialization(ptipoCarga);
        }

        private async Task menu_mniSi_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.menu_mniSi_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task menu_mniNo_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.menu_mniNo_Click();
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

        private async Task pnlTablas_chkTodas_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            try
            {
                controlador.pnlSeleccionTablas_chkTodas_CheckStateChanged();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            try
            {
                if (v_cerrado)
                {
                    controlador.vistaCargaMaquina_Closing();
                }
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            
        }

    }
}