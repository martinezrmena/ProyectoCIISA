using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.Controlador;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.Vista
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class vistaReporte : ContentPage
	{
        public ctrlReporte controlador = null;

        public Cliente v_objCliente = null;

        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaReporte()
        {
            controlador = new ctrlReporte(this);

            InitializeComponent();

            controlador.ScreenInicialization();
        }

        private async Task pnlReporte_btnIndicadores_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlReporte_btnIndicadores_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlReporte_btnVentasProducto_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlReporte_btnVentasProducto_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlReporte_btnFacturaCredito_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlReporte_btnFacturaCredito_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlReporte_btnTramite_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlReporte_btnTramite_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

        }

        private async Task pnlReporte_btnAnulacion_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlReporte_btnAnulacion_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlReporte_btnRecaudacion_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlReporte_btnRecaudacion_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlReporte_btnRegalia_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlReporte_btnRegalia_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlReporte_btnDevolucion_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlReporte_btnDevolucion_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlReporte_btnCotizacion_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlReporte_btnCotizacion_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlReporte_btnOrdenesVenta_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlReporte_btnOrdenesVenta_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlReporte_btnRecibosDinero_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlReporte_btnRecibosDinero_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlReporte_btnFacturaContado_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlReporte_btnFacturaContado_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlReporte_btnPedidosSinAplicar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlReporte_btnPedidosSinAplicar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlReporte_btnConsecutivos_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlReporte_btnConsecutivos_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlReporte_btnCrediticioDeLaRuta_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlReporte_btnCrediticioDeLaRuta_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlReporte_btnPedidos_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlReporte_btnPedidos_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlReporte_btnCrediticioDelCliente_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlReporte_btnCrediticioDelCliente_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task menu_mniCerrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlReporte_stkCerrar);
                controlador.menu_mniCerrar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlReporte_stkCerrar);
        }
    }
}