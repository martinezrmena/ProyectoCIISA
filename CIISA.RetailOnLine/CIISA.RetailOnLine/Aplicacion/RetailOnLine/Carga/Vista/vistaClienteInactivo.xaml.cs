using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Vista;
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
    public partial class vistaClienteInactivo : ContentPage
    {
        private ctrlClienteInactivo controlador = null;
        private ctrlCarga_cliente v_ctrlCarga_Cliente = null;
        public string v_codCliente = string.Empty;
        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaClienteInactivo(ctrlCarga_cliente pctrlCarga_Cliente)
        {
            controlador = new ctrlClienteInactivo(this);
            v_ctrlCarga_Cliente = pctrlCarga_Cliente;

            InitializeComponent();

            controlador.ScreenInicialization();
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

        private async Task menu_mniClose_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlCliente_stkCerrar);
                controlador.menu_mniClose_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlCliente_stkCerrar);
        }

        private async Task menu_mniAceptar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlCliente_stkAceptar);
                controlador.menu_mniAceptar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlCliente_stkAceptar);
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            try
            {
                if (controlador.Cerrar)
                {
                    if (v_ctrlCarga_Cliente != null)
                    {
                        v_ctrlCarga_Cliente.pcodCliente = v_codCliente;
                        v_ctrlCarga_Cliente.informacionClienteIndividualRecargarParte2();
                    }
                }
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

        }
    }
}