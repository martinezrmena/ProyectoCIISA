using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Helpers;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaIdentificarUsuario : ContentPage
    {
        private ctrlIdentificarUsuario controlador = null;
        internal ITaskActivity DPB = DependencyService.Get<ITaskActivity>();
        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaIdentificarUsuario()
        {
            try
            {
                InitializeComponent();

                controlador = new ctrlIdentificarUsuario(this);

                controlador.ScreenInicialization();
            }
            catch (Exception ex)
            {

                ExceptionHandled(ex);
            }
            
        }

        private async void ExceptionHandled(Exception ex) {

            await ExceptionManager.ExceptionHandling(ex);
        }

        #region Menu
        private async Task menu_mniAcceder_Clicked(object sender, EventArgs e)
        {
            try
            {
                pnlIdentificacion_chkRecarga.CheckedChanged -= pnlIdentificacion_chkRecarga_CheckedChanged;
                await simulateClickGestures.SelectedStack(pnlIdentificacion_stkAcceder);
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await controlador.menu_mniAcceder_Click();
                pnlIdentificacion_chkRecarga.CheckedChanged += pnlIdentificacion_chkRecarga_CheckedChanged;

            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }

            await simulateClickGestures.NoSelectedStack(pnlIdentificacion_stkAcceder);
        }

        private async Task menu_mniEnergia_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.menu_mniEnergia_Click();

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

        private void menu_mniPruebaConexion_Clicked(object sender, EventArgs e)
        {
            DPB.PruebaConexion();
        }

        private async Task menu_mniPruebaImpresion_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await controlador.menu_mniPruebaImpresion_Click();
                
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

        private async Task menu_mniLiquidadores_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await simulateClickGestures.SelectedStack(pnlIdentificacion_stkLiquidador);
                await controlador.menu_mniLiquidadores_Click();

            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
            await simulateClickGestures.NoSelectedStack(pnlIdentificacion_stkLiquidador);
        }

        private async Task menu_mniSalir_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlRecargaDiaria_stkSalir);
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
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

            await simulateClickGestures.NoSelectedStack(pnlRecargaDiaria_stkSalir);
        }
        #endregion

        #region checkBox
        private async void pnlIdentificacion_chkAperturaNocturna_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            try
            {
                pnlIdentificacion_chkAperturaNocturna.CheckedChanged -= pnlIdentificacion_chkAperturaNocturna_CheckedChanged;
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await controlador.pnlIdentificacion_chkAperturaNocturna_CheckStateChanged();
                pnlIdentificacion_chkAperturaNocturna.CheckedChanged += pnlIdentificacion_chkAperturaNocturna_CheckedChanged;

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

        private async void pnlIdentificacion_chkRecarga_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            try
            {
                pnlIdentificacion_chkRecarga.CheckedChanged -= pnlIdentificacion_chkRecarga_CheckedChanged;
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await controlador.pnlIdentificacion_chkCargarTablas_CheckStateChanged();
                pnlIdentificacion_chkRecarga.CheckedChanged += pnlIdentificacion_chkRecarga_CheckedChanged;
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

        #region Botones
        private async void pnlIdentificacion_btnBorrarCodigo_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.pnlIdentificacion_btnBorrarCodigo_Click();

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

        private async void pnlIdentificacion_btnLimpiarCodigo_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.pnlIdentificacion_btnLimpiarCodigo_Click();

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

        private async void pnlIdentificacion_btnBorrarContrasenna_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.pnlIdentificacion_btnBorrarContrasenna_Click();

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

        private async void pnlIdentificacion_btnLimpiarContrasenna_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.pnlIdentificacion_btnLimpiarContrasenna_Click();

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

        private async void pnlIdentificacion_btnBorrarRecarga_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.pnlIdentificacion_btnBorrarRecarga_Click();

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

        private async void pnlIdentificacion_btnLimpiarRecarga_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.pnlIdentificacion_btnLimpiarRecarga_Click();

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

    }
}