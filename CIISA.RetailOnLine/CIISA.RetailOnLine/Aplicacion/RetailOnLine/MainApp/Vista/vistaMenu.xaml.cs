using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaMenu : ContentPage
    {
        private ctrlMenu controlador = null;
        SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaMenu()
        {
            try
            {

                controlador = new ctrlMenu(this);

                InitializeComponent();

                controlador.ScreenInicialization();
            }
            catch (Exception ex)
            {
                ExceptionHandled(ex);
            }
        }

        private async void ExceptionHandled(Exception ex)
        {
            await ExceptionManager.ExceptionHandling(ex);
        }

        #region Menu
        private async Task menu_mniSalir_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await simulateClickGestures.SelectedStack(pnlMenu_stkSalir);
                await controlador.menu_mniSalir_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }

            await simulateClickGestures.NoSelectedStack(pnlMenu_stkSalir);
        }

        private async Task menu_mniBloquear_Clicked(object sender, EventArgs e)
        {

            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await simulateClickGestures.SelectedStack(pnlMenu_stkBloquear);
                controlador.menu_mniBloquear_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }

            await simulateClickGestures.NoSelectedStack(pnlMenu_stkBloquear);

        }

        protected override bool OnBackButtonPressed()
        {
            controlador.SalirBackButton();

            return true;
        }

        #endregion

        #region GESTIÓN DE VENTA
        private async Task Bitacora_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.pnlMenu_btnBitacora_Click();
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


        private async Task pnlMenu_btnRuta_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.pnlMenu_btnRuta_Click();
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

        private async Task pnlMenu_btnVisita_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.pnlMenu_btnVisita_Click();
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

        private async Task pnlMenu_btnAnulaciones_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.pnlMenu_btnAnulaciones_Click();
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

        private async Task pnlMenu_btnNuevoCliente_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.pnlMenu_btnNuevoCliente_Click();
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

        private async Task pnlMenu_btnEntregaPedidos_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.pnlMenu_btnEntregaPedidos_Click();
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

        private async Task pnlMenu_btnCuentaCerrada_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.pnlMenu_btnCuentaCerrada_Click();
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

        private async Task pnlMenu_btnIndicadoresFacturacion_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.pnlMenu_btnIndicadoresFacturacion_Click();
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

        private async Task pnlMenu_btnCuentasBancarias_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.pnlMenu_btnCuentasBancarias_Click();
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

        private async Task pnlMenu_btnTelefono_Clicked(object sender, EventArgs e)
        {

            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.pnlMenu_btnTelefono_Click();
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

        #endregion

        #region INVENTARIO
        private async Task pnlMenu_btnInventarioTeorico_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    controlador.pnlMenu_btnInventarioTeorico_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
        }

        private async Task pnlMenu_btnInventarioPedidos_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    controlador.pnlMenu_btnInventarioPedidos_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
        }

        private async Task pnlMenu_btnTraslados_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await controlador.pnlMenu_btnTraslados_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
        }
        #endregion

        #region REPORTES
        private async Task pnlMenu_btnReportes_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await controlador.pnlMenu_btnReportes_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
        }

        private async Task pnlMenu_btnFlujoContable_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlMenu_btnFlujoContable_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        #endregion

        #region SINCRONIZACION
        private async Task pnlMenu_btnDescarga_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlMenu_btnDescarga_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnCarga_Clicked(object sender, EventArgs e)
        {

            try
            {
                controlador.pnlMenu_btnCarga_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnConsultaOV_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    controlador.pnlMenu_btnConsultaOV_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
        }

        private async Task pnlMenu_btnConsultaDocs_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlMenu_btnConsultaDocs_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnConsultaRecibos_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlMenu_btnConsultaRecibos_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
        #endregion

        #region UTILES
        private async Task pnlMenu_btnEstadoMemoria_Click(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlMenu_btnEstadoMemoria_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnIluminacion_Click(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlMenu_btnIluminacion_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }


        private async Task pnlMenu_btnPruebaPantalla_Click(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlMenu_btnPruebaPantalla_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnUbicacionGPS_Click(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlMenu_btnUbicacionGPS_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void pnlMenu_btnTipoImpresion_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlMenu_btTipoImpresion_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        #endregion

        #region LIQUIDADORES
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

        private async Task pnlMenu_btnConsolidarCerrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlMenu_btnConsolidarCerrar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

        }

        private async Task pnlMenu_btnTomaFisica_Clicked(object sender, EventArgs e)
        {
            try
            {

                var v_testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

                if (await v_testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
                {
                    await controlador.pnlMenu_btnTomaFisica_Click();
                }
                    
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnLiquidadores_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlMenu_btnLiquidadores_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlMenu_btnAcercaDe_Clicked(object sender, EventArgs e)
        {

            try
            {
                controlador.pnlMenu_btnAcercaDe_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }


        #endregion
    }
}