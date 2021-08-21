using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita.Guardar;
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
    public partial class vistaFormaPago : ContentPage
    {
        internal ctrlFormaPago controlador = null;
        public vistaRecibo viewRecibo = null;
        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaFormaPago(string ppantalla,Cliente pobjCliente,vistaRecibo pviewRecibo)
        {
            viewRecibo = pviewRecibo;
            InitializeComponent();

            controlador = new ctrlFormaPago(this);

            this.pnlFormaPago_cbxBanco.SelectedIndexChanged -= pnlFormaPago_cbxBanco_SelectedIndexChanged;
            this.pnlFormaPago_cbxFormaPago.SelectedIndexChanged -= pnlFormaPago_cbxFormaPago_SelectedIndexChanged;

            controlador.ScreenInicialization(ppantalla,pobjCliente);

            this.pnlFormaPago_cbxBanco.SelectedIndexChanged += pnlFormaPago_cbxBanco_SelectedIndexChanged;
            this.pnlFormaPago_cbxFormaPago.SelectedIndexChanged += pnlFormaPago_cbxFormaPago_SelectedIndexChanged;

        }

        public vistaFormaPago(decimal ptotalSinIV, string ppantalla, string pcodTipoTransaccion, Cliente pobjCliente, LogicaCarniceriaGuardar logica)
        {
            InitializeComponent();

            controlador = new ctrlFormaPago(this);

            this.pnlFormaPago_cbxBanco.SelectedIndexChanged -= pnlFormaPago_cbxBanco_SelectedIndexChanged;
            this.pnlFormaPago_cbxFormaPago.SelectedIndexChanged -= pnlFormaPago_cbxFormaPago_SelectedIndexChanged;

            controlador.ScreenInicialization(ptotalSinIV, ppantalla, pobjCliente, logica);

            this.pnlFormaPago_cbxBanco.SelectedIndexChanged += pnlFormaPago_cbxBanco_SelectedIndexChanged;
            this.pnlFormaPago_cbxFormaPago.SelectedIndexChanged += pnlFormaPago_cbxFormaPago_SelectedIndexChanged;

        }

        public vistaFormaPago(vistaVisita V_view, decimal ptotalSinIV,string ppantalla,string pcodTipoTransaccion,Cliente pobjCliente, LogicaVisitaGuardar logica)
        {
            InitializeComponent();

            controlador = new ctrlFormaPago(this, V_view);

            this.pnlFormaPago_cbxBanco.SelectedIndexChanged -= pnlFormaPago_cbxBanco_SelectedIndexChanged;
            this.pnlFormaPago_cbxFormaPago.SelectedIndexChanged -= pnlFormaPago_cbxFormaPago_SelectedIndexChanged;

            controlador.ScreenInicialization(ptotalSinIV,ppantalla,pobjCliente,logica);

            this.pnlFormaPago_cbxBanco.SelectedIndexChanged += pnlFormaPago_cbxBanco_SelectedIndexChanged;
            this.pnlFormaPago_cbxFormaPago.SelectedIndexChanged += pnlFormaPago_cbxFormaPago_SelectedIndexChanged;

        }

        public vistaFormaPago(string ppantalla,Cliente pobjCliente,string pmotivoRecaudacion, LogicaVisitaGuardar logica)
        {
            InitializeComponent();

            controlador = new ctrlFormaPago(this);

            this.pnlFormaPago_cbxBanco.SelectedIndexChanged -= pnlFormaPago_cbxBanco_SelectedIndexChanged;
            this.pnlFormaPago_cbxFormaPago.SelectedIndexChanged -= pnlFormaPago_cbxFormaPago_SelectedIndexChanged;

            controlador.ScreenInicialization(ppantalla,pobjCliente,pmotivoRecaudacion,logica);

            this.pnlFormaPago_cbxBanco.SelectedIndexChanged += pnlFormaPago_cbxBanco_SelectedIndexChanged;
            this.pnlFormaPago_cbxFormaPago.SelectedIndexChanged += pnlFormaPago_cbxFormaPago_SelectedIndexChanged;
        }

        private async Task menu_mniAgregarPago_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlFormaPago_stkAgregarPago);
                controlador.menu_mniAgregarPago_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlFormaPago_stkAgregarPago);
        }

        public async void menu_mniFinalizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlFormaPago_stkFinalizar);
                await controlador.menu_mniFinalizar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlFormaPago_stkFinalizar);
        }

        private async Task pnlFormaPago_btnLimpiarNumero_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlFormaPago_btnLimpiarNumero_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlFormaPago_btnLimpiarSerie_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlFormaPago_btnLimpiarSerie_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlFormaPago_btnLimpiarObservacion_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlFormaPago_btnLimpiarObservacion_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlFormaPago_btnLimpiarMonto_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlFormaPago_btnLimpiarMonto_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlFormaPago_btnBorrarNumero_Clicked(object sender, EventArgs e)
        {

            try
            {
                controlador.pnlFormaPago_btnBorrarNumero_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlFormaPago_btnBorrarSerie_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlFormaPago_btnBorrarSerie_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlFormaPago_btnBorrarObservacion_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlFormaPago_btnBorrarObservacion_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlFormaPago_btnBorrarMonto_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlFormaPago_btnBorrarMonto_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlFormaPago_btnGuionNumero_Clicked(object sender, EventArgs e)
        {

            try
            {
                controlador.pnlFormaPago_btnGuionNumero_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlFormaPago_btnGuionSerie_Clicked(object sender, EventArgs e)
        {

            try
            {
                controlador.pnlFormaPago_btnGuionSerie_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlFormaPago_btnPuntoDecimalMonto_Clicked(object sender, EventArgs e)
        {

            try
            {
                controlador.pnlFormaPago_btnPuntoDecimalMonto_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

        }

        private async Task pnlFormaPago_btnBorrarLinea_Clicked(object sender, EventArgs e)
        {

            try
            {
                await controlador.pnlFormaPago_btnBorrarLinea_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlFormaPago_btnBorrarTodos_Clicked(object sender, EventArgs e)
        {

            try
            {
                controlador.pnlFormaPago_btnBorrarTodos_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task ctxtMenu_mniEliminar_Clicked(object sender, EventArgs e)
        {

            try
            {
                await controlador.ctxtMenu_mniEliminar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task ctxtMenu_mniEliminarTodos_Clicked(object sender, EventArgs e)
        {

            try
            {
                controlador.ctxtMenu_mniEliminarTodos_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

        }

        private async void pnlFormaPago_cbxBanco_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                controlador.pnlFormaPago_cbxBanco_SelectedIndexChanged();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

        }

        private async void pnlFormaPago_cbxFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlFormaPago_cbxFormaPago_SelectedIndexChanged();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            
            try
            {
                await controlador.Cerrar();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
    }
}