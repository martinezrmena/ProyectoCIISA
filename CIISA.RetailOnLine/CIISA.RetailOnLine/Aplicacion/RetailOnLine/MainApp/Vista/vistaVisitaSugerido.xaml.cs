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
    public partial class vistaVisitaSugerido : ContentPage
    {
        private ctrlVisitaSugerido controlador = null;

        public ListView v_listaProductos = new ListView();

        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaVisitaSugerido(vistaVisita pviewVisita)
        {
            controlador = new ctrlVisitaSugerido(this, pviewVisita);

            InitializeComponent();

            controlador.ScreenInicialization();
        }

        private async Task pnlSugerido_chkTodas_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            try
            {
                controlador.pnlSugerido_chkTodas_CheckStateChanged();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task menu_mniCancelar_Clicked(object sender, EventArgs e)
        {
            await simulateClickGestures.SelectedStack(pnlSugerido_stkCancelar);

            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await controlador.menu_mniCancelar_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }

            await simulateClickGestures.NoSelectedStack(pnlSugerido_stkCancelar);
        }

        private async Task menu_mniAgregar_Clicked(object sender, EventArgs e)
        {
            await simulateClickGestures.SelectedStack(pnlSugerido_stkAgregar);

            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await controlador.menu_mniAgregar_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }

            await simulateClickGestures.NoSelectedStack(pnlSugerido_stkAgregar);
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            try
            {
                controlador.Cerrando();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            
        }
    }
}