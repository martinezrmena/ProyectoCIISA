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
    public partial class vistaRuta : ContentPage
    {
        private ctrlRuta controlador = null;
        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaRuta()
        {       
            controlador = new ctrlRuta(this);

            InitializeComponent();

            pnlClientes_cbxTipoBusqueda.SelectedIndexChanged -= pnlClientes_cbxTipoBusqueda_SelectedIndexChanged;

            controlador.ScreenInicialization();

            pnlClientes_cbxTipoBusqueda.SelectedIndexChanged += pnlClientes_cbxTipoBusqueda_SelectedIndexChanged;

        }

        private async Task pnlClientes_btnBorrarBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlClientes_btnBorrarBuscar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlClientes_btnLimpiarBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlClientes_btnLimpiarBuscar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task menu_mniCerrarOrdenAtencion_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    controlador.menu_mniCerrarOrdenAtencion_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
        }

        private async void pnlClientes_cbxTipoBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    controlador.pnlClientes_cbxTipoBusqueda_SelectedIndexChanged();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
        }

        private async Task pnlClientes_btnCobertura_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    controlador.pnlClientes_btnCobertura_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
        }

        private async Task pnlClientes_ltvClientes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                controlador.pnlClientes_ltvClientes_ItemSeleccionado();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void pnlClientes_cbxTipoBusqueda_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                controlador.pnlClientes_txtBuscar_Focus();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlClientes_stkBuscar_Clicked(object sender, EventArgs e)
        {

            await simulateClickGestures.SelectedStack(pnlClientes_stckBuscar);

            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await controlador.menu_mniBuscar_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }

            await simulateClickGestures.NoSelectedStack(pnlClientes_stckBuscar);

        }

        private async Task pnlClientes_stkVisita_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await simulateClickGestures.SelectedStack(pnlClientes_stkVisita);
                    await controlador.pnlClientes_ltvClientes_ItemActivete();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }

            await simulateClickGestures.NoSelectedStack(pnlClientes_stkVisita);
        }

        private async Task pnlClientes_stkMotive_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await simulateClickGestures.SelectedStack(pnlClientes_stckMotive);
                    await controlador.menu_mniAgregarMotivo_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }

            await simulateClickGestures.NoSelectedStack(pnlClientes_stckMotive);
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            try
            {
                pnlClientes_cbxTipoBusqueda.PropertyChanged -= pnlClientes_cbxTipoBusqueda_PropertyChanged;
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
    }
}