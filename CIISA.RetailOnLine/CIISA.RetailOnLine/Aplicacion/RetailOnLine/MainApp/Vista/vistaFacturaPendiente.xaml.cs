using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
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
    public partial class vistaFacturaPendiente : ContentPage
    {
        private ctrlFacturaPendiente controlador = null;

        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaFacturaPendiente(Cliente pobjCliente, string ppantallaInvoca, vistaRecibo pviewRecibo)
        {
            controlador = new ctrlFacturaPendiente(this);

            InitializeComponent();

            controlador.ScreenInicialization(pobjCliente, ppantallaInvoca, pviewRecibo);

        }

        private async Task menu_mniTramitar_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await controlador.menu_mniTramitar_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
        }

        private async Task menu_mniVerificar_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {

                    await simulateClickGestures.SelectedStack(pnlFacturaPendiente_stckCheck);
                    await controlador.menu_mniVerificar_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }

                await simulateClickGestures.NoSelectedStack(pnlFacturaPendiente_stckCheck);
            }
        }

        private async Task menu_mniSumar_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await simulateClickGestures.SelectedStack(pnlFacturaPendiente_stckSumar);
                    controlador.menu_mniSumar_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }

                await simulateClickGestures.NoSelectedStack(pnlFacturaPendiente_stckSumar);
            }
        }

        private async Task menu_mniAgregar_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await simulateClickGestures.SelectedStack(pnlFacturaPendiente_stkAgregar);
                    await controlador.menu_mniAgregar_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }

                await simulateClickGestures.NoSelectedStack(pnlFacturaPendiente_stkAgregar);
            }
        }

        private async Task pnlFacturas_chkTodas_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            try
            {
                controlador.pnlFacturas_chkTodos_CheckStateChanged();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlFacturas_ltvFacturas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                controlador.pnlFacturas_ltvFacturas_ItemCheck();
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
                await controlador.Cerrando();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
    }
}