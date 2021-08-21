using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using System;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaVisita : ContentPage
    {
        internal ctrlVisita controlador = null;
        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaVisita()
        {
            InitializeComponent();

            controlador = new ctrlVisita(this);

            ConectarComboboxTipoTransaccion(false);

            controlador.ScreenInicialization();

            ConectarComboboxTipoTransaccion(true);
        }

        public void ConectarComboboxTipoTransaccion(bool Conectar)
        {
            if (Conectar)
            {
                pnlTransacciones_cbxTipoTransaccion.SelectedIndexChanged += pnlTransacciones_cbxTipoTransaccion_SelectedIndexChanged;
            }
            else
            {
                pnlTransacciones_cbxTipoTransaccion.SelectedIndexChanged -= pnlTransacciones_cbxTipoTransaccion_SelectedIndexChanged;
            }
        }

        public vistaVisita(Cliente pobjCliente, bool pcambiarCliente)
        {
            InitializeComponent();

            controlador = new ctrlVisita(this);

            ConectarComboboxTipoTransaccion(false);

            controlador.ScreenInicialization(pobjCliente, pcambiarCliente);

            ConectarComboboxTipoTransaccion(true);
        }

        public vistaVisita(Cliente pobjCliente, bool pcambiarCliente, bool FacturacionFaltantes)
        {
            InitializeComponent();

            controlador = new ctrlVisita(this);

            ConectarComboboxTipoTransaccion(false);

            controlador.ScreenInicialization(pobjCliente, pcambiarCliente,FacturacionFaltantes);

            ConectarComboboxTipoTransaccion(true);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                NavigationPage.SetHasBackButton(this, false);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            
        }

        public async Task pintarAtributosCliente(Cliente pobjCliente)
        {
            try
            {
                controlador.pintarAtributosCliente(pobjCliente);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task menu_mniCambiarCliente_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    controlador.menu_mniCambiarCliente_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
        }

        private async Task menu_mniFactura_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    controlador.menu_mniFactura_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
        }

        private async Task menu_mniPruebaImpresion_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await controlador.menu_mniPruebaImpresion_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
        }

        private async Task menu_mniCoordenadas_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await controlador.menu_mniCoordenadas_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
        }

        private async Task menu_mniSugerido_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    controlador.menu_mniSugerido_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
        }

        private async Task pnlTransacciones_btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlTransacciones_btnEliminar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlTransacciones_btnEliminarTodos_Clicked(object sender, EventArgs e)
        {

            try
            {
                await controlador.pnlTransacciones_btnEliminarTodos_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

        }

        private async Task pnlTransacciones_btnCalcularMontoLinea_Clicked(object sender, EventArgs e)
        {

            try
            {
                await controlador.pnlTransacciones_btnCalcularMontoLinea_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

        }

        private async Task pnlTransacciones_btnEspecificacion_Clicked(object sender, EventArgs e)
        {

            try
            {
                await controlador.pnlTransacciones_btnEspecificacion_Click();
            }            
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlTransacciones_btnMotivo_Clicked(object sender, EventArgs e)
        {

            try
            {
                await controlador.pnlTransacciones_btnMotivo_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

        }

        public async void pnlTransacciones_cbxTipoTransaccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ConectarComboboxTipoTransaccion(false);

                await controlador.pnlTransacciones_cbxTipoTransaccion_SelectedIndexChanged();

                ConectarComboboxTipoTransaccion(true);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task ctxMenu_cliente_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.ctxMenu_cliente_clicked();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task menu_mniCargaPedido_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.menu_mniCargaPedido_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task menu_mniBloquear_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlTransacciones_stkLock);
                controlador.menu_mniBloquear_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlTransacciones_stkLock);
        }

        private async Task menu_mniGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlTransacciones_stkGuardar);
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);

                if (await controlador.ValidateGPS())
                {
                    await controlador.menu_GuardarInicialization();
                }
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }

            await simulateClickGestures.NoSelectedStack(pnlTransacciones_stkGuardar);
        }

        private async void pnlTransacciones_ltvProductos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            try
            {
                controlador.pnlTransacciones_ltvProductos_ItemActivate();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlTransacciones_ltvProductos_ColumnClick();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task Cerrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.vistaVisita_Closing(true);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            try
            {
                controlador.vistaVisita_Closing(true);
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
                );
            }

            return base.OnBackButtonPressed();
        }

        private async void pnlTransacciones_ltvProductos_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                await controlador.pnlTransacciones_ltvProductos_Tapped();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlTransacciones_btnDetalleReses_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlTrasacciones_DetalleReses();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void menu_GoogleMaps_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.GoogleMapsView();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void menu_Waze_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.WazeView();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void menu_mniDevolucionFactura_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.DevolucionFacturaMenu();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void menu_mniFacturaManual_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.FacturaManual();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void menu_mniTipoDevolucion_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.CambiarTipoDevolucion();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
    }
}