using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Helpers;
using CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Controlador;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Vista
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class vistaMenuSettlementOnLine : ContentPage
	{
        private ctrlMenuSettlementOnLine controlador = null;
        internal ITaskActivity DPB = DependencyService.Get<ITaskActivity>();

        public vistaMenuSettlementOnLine()
        {
            controlador = new ctrlMenuSettlementOnLine(this);

            InitializeComponent();

            controlador.ScreenInicialization();
        }

        private async Task menu_mniClose_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.menu_mniClose_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlLiquidadores_btnDescarga_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlLiquidadores_btnDescarga_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlLiquidadores_btnSincronizarRespaldo_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlLiquidadores_btnSincronizarRespaldo_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlLiquidadores_btnCarga_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlLiquidadores_btnCarga_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlLiquidadores_btnCerradoManual_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlLiquidadores_btnCerradoManual_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlLiquidadores_btnAperturaManual_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlLiquidadores_btnAperturaManual_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlLiquidadores_btnDescargaConsecutivos_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await controlador.pnlLiquidadores_btnDescargaConsecutivos_Click();
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

        private async Task pnlLiquidadores_btnCargaConsecutivos_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await controlador.pnlLiquidadores_btnCargaConsecutivos_Click();
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

        private async Task pnlLiquidadores_btnReporteConsecutivos_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlLiquidadores_btnReporteConsecutivos_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlLiquidadores_btnCargaPedidos_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlLiquidadores_btnCargaPedidos_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlLiquidadores_btnReimpresion_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlLiquidadores_btnReimpresion_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlLiquidadores_btnReimpresionAntiguedad_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlLiquidadores_btnReimpresionAntiguedad_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlLiquidadores_btnCargarAgente_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlLiquidadores_btnCargarAgente_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlLiquidadores_btnConsultarAgente_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlLiquidadores_btnConsultarAgente_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlLiquidadores_btnAuditoria_Clicked(object sender, EventArgs e)
        {      
            try
            {
                controlador.pnlLiquidadores_btnAuditoria_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlLiquidadores_btnCargarClaves_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await controlador.pnlLiquidadores_btnCargarClaves_Click();
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

        private async Task pnlLiquidadores_btnMostrarClaves_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlLiquidadores_btnMostrarClaves_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlLiquidadores_btnRespaldo_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlLiquidadores_btnRespaldo_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlLiquidadores_btnCargaClienteFaltantes_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlLiquidadores_btnCargaClienteFaltantes_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnFactFaltantes_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlMenu_btnFactFaltantes_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
    }
}