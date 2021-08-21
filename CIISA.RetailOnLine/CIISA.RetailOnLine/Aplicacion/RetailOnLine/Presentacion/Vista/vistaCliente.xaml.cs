using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RadioTelefonico.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Controlador;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaCliente : ContentPage
    {
        private ctrlCliente controlador = null;

        public Cliente v_objCliente = new Cliente();

        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaCliente(vistaCarniceria ppantallaCarniceria, string ppantalla)
        {
            controlador = new ctrlCliente(this);

            InitializeComponent();

            controlador.ScreenInicialization(ppantallaCarniceria, ppantalla);
        }

        public vistaCliente(vistaVisita ppantallaVisita, string ppantalla)
        {
            controlador = new ctrlCliente(this);

            InitializeComponent();

            controlador.ScreenInicialization(ppantallaVisita, ppantalla);
        }

        public vistaCliente(vistaTelefono ppantallaTelefono, string ppantalla)
        {
            controlador = new ctrlCliente(this);

            InitializeComponent();

            controlador.ScreenInicialization(ppantallaTelefono, ppantalla);
        }

        public vistaCliente(vistaReporte ppantallaReporte, string ppantalla)
        {
            controlador = new ctrlCliente(this);

            InitializeComponent();

            controlador.ScreenInicialization(ppantallaReporte, ppantalla);
        }

        public vistaCliente(ctrlCarga_indicadores pcontroler, string ppantalla)
        {
            controlador = new ctrlCliente(this);

            InitializeComponent();

            controlador.ScreenInicialization(pcontroler, ppantalla);
        }

        public vistaCliente(ctrlCarga_descuentos pcontroler, string ppantalla)
        {
            controlador = new ctrlCliente(this);

            InitializeComponent();

            controlador.ScreenInicialization(pcontroler, ppantalla);
        }

        public vistaCliente(string pVistaRecibo, vistaRecibo ppantallaRecibo)
        {
            controlador = new ctrlCliente(this);

            InitializeComponent();

            controlador.ScreenInicialization(pVistaRecibo, ppantallaRecibo);
        }

        private async Task menu_mniNuevoCliente_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await controlador.menu_mniNuevoCliente_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
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

        private async Task pnlClientes_cbxTipoBusqueda_SelectedIndexChanged(object sender, EventArgs e)
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

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            try
            {
                pnlClientes_cbxTipoBusqueda.PropertyChanged -= pnlClientes_cbxTipoBusqueda_PropertyChanged;
                await controlador.Cerrando();

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

        private async Task pnlClientes_stkBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {

                await simulateClickGestures.SelectedStack(pnlClientes_stckBuscar);
                await controlador.menu_mniBuscar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlClientes_stckBuscar);
        }

        private async Task pnlClientes_stckAyuda_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await simulateClickGestures.SelectedStack(pnlClientes_stckAyuda);
                    await controlador.menu_mniAyuda_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }

                await simulateClickGestures.NoSelectedStack(pnlClientes_stckAyuda);
            }
        }

        private async Task pnlClientes_stckSeleccionar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlClientes_stkSeleccionar);
                await controlador.pnlClientes_ltvClientes_ItemActivete();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlClientes_stkSeleccionar);
        }
    }
}