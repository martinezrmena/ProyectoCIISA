using CIISA.RetailOnLine.Aplicacion.RetailOnLine.InventoryOnLine.Controller;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.InventoryOnLine.ListViewMoldels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Helpers;
using Acr.UserDialogs;

namespace CIISA.RetailOnLine.Aplicacion.InventoryOnLine._View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaTomaFisica : ContentPage
    {
        public ctrlTomaFisica controlador = null;
        internal ITaskActivity DPB = DependencyService.Get<ITaskActivity>();

        public vistaTomaFisica()
        {
            try
            {
                controlador = new ctrlTomaFisica(this);

                InitializeComponent();

                DPB.StartScreenInicializationTomaFisica(controlador);

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

        private async Task pnlTomaFisica_ltvInventario_ItemActivate(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlTomaFisica_ltvInventario_ItemActivate();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlTomaFisica_btnPuntoDecimal_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlTomaFisica_btnPuntoDecimal_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlTomaFisica_btnBorrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlTomaFisica_btnBorrar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

        }

        private async Task menu_mniAgregarCantidad_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.mniAgregarCantidad_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private void menu_mniConsolidar_Clicked(object sender, EventArgs e)
        {
            DPB.ConsolidarTomaFisica(controlador);
        }

        private void menu_mniContinuar_Clicked(object sender, EventArgs e)
        {
            DPB.pnlFinalizar_Continuar(controlador);
        }

        private void menu_mniProcedimiento_Clicked(object sender, EventArgs e)
        {
            DPB.pnlProcedimiento(controlador);
        }

        private async Task menu_mniRecalcularInventario_Clicked(object sender, EventArgs e)
        {
            try {

                await controlador.menu_mniRecalcularInventario_Click();
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

        private async Task pnlTomaFisica_btnCalculadora_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlTomaFisica_btnCalculadora_Click();
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
                await controlador.menu_mniClose_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlTomaFisica_btnLimpiar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlTomaFisica_btnLimpiar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private void pnlFinalizar_btnImprimir_Clicked(object sender, EventArgs e)
        {
            DPB.pnlFinalizar_Imprimir(controlador);
        }

        private void pnlFinalizar_btnPruebaConexion_Clicked(object sender, EventArgs e)
        {
            DPB.PruebaConexion();
        }

        private void pnlFinalizar_btnSincronizar_Clicked(object sender, EventArgs e)
        {
            DPB.pnlFinalizarSincronizarTomaFisica(controlador);
        }

        private void pnlFinalizar_btnCerrarMaquina_Clicked(object sender, EventArgs e)
        {
            DPB.pnlFinalizarCerrarMaquina(controlador);
        }

        private async Task pnlTomaFisica_chkAjustarColumnas_CheckStateChanged(object sender, XLabs.EventArgs<bool> e)
        {
            try
            {
                controlador.pnlTomaFisica_chkAjustarColumnas_CheckStateChanged();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

        }

        private async Task pnlTomaFisica_btnAgregarCantidad_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.mniAgregarCantidad_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

        }

        private async Task pnlTomaFisica_btnUno_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlTomaFisica_btnOne_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlTomaFisica_btnDos_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlTomaFisica_btnTwo_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlTomaFisica_btnTres_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlTomaFisica_btnThree_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlTomaFisica_btnCuatro_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlTomaFisica_btnFour_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlTomaFisica_btnCinco_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlTomaFisica_btnFive_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlTomaFisica_btnSeis_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlTomaFisica_btnSix_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlTomaFisica_btnSiete_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlTomaFisica_btnSeven_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlTomaFisica_btnOcho_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlTomaFisica_btnEight_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlTomaFisica_btnNueve_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlTomaFisica_btnNine_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlTomaFisica_btnCero_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlTomaFisica_btnZero_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
    }
}