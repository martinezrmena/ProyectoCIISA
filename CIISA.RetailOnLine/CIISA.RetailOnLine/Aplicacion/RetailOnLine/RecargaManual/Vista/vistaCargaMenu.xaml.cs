using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Helpers;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaCargaMenu : ContentPage
    {
        private ctrlCargaMenu controlador = null;

        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaCargaMenu()
        {
            controlador = new ctrlCargaMenu(this);

            InitializeComponent();

            controlador.ScreenInicialization();
        }

        private void pnlMenu_btnPruebaConexion_Clicked(object sender, EventArgs e)
        {
            var DPB = DependencyService.Get<ITaskActivity>();
            DPB.PruebaConexion();
        }

        private async Task pnlMenu_btnAnulacion_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlMenu_btnAnulacion_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnCliente_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlMenu_btnCliente_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnEstablecimiento_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlMenu_btnEstablecimiento_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnDescuentos_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlMenu_btnDescuentos_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnDescuentosGeneral_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlMenu_btnDescuentosGeneral_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnFactura_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlMenu_btnFactura_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnIndicadorFactura_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlMenu_btnIndicadorFactura_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnInventario_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlMenu_btnInventario_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnListaPrecios_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlMenu_btnListaPrecios_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnPrecioProducto_Clicked(object sender, EventArgs e)
        {

            try
            {
                await controlador.pnlMenu_btnPrecioProducto_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnProducto_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlMenu_btnProducto_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnVisita_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlMenu_btnVisita_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnImpresora_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlMenu_btnImpresora_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnPedidos_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlMenu_btnPedidos_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnClienteIndividual_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlMenu_btnClienteIndividual_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnIndicadorClienteFactura_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlMenu_btnIndicadorClienteFactura_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnDescuentosCliente_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlMenu_btnDescuentosCliente_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        #region MenuBarButtom
        private async Task menu_mniClose_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlMenu_stkCerrar);
                controlador.menu_mniClose_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlMenu_stkCerrar);
        }

        private async Task menu_mniAbortar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlMenu_stkAbortar);
                await controlador.menu_mniAbortar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlMenu_stkAbortar);
        }
        #endregion

        #region Carniceria
        private async Task pnlMenu_btnDetalleReses_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlMenu_btnDetalleReses_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnMensajeFactura_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlMenu_btnMensajeFactura_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
        #endregion
    }
}