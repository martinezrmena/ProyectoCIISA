using CIISA.RetailOnLine.Aplicacion.AuditOnLine.Controlador;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.AuditOnLine.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaAuditoria : ContentPage
    {
        private ctrlAuditoria controlador = null;

        public vistaAuditoria()
        {
            try
            {
                controlador = new ctrlAuditoria(this);

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

        private async Task menu_mniLimpiar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.menu_mniLimpiar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task menu_mniImprimir_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.menu_mniImprimir_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task menu_mniGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.menu_mniConsolidar_Click();
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

        private async Task pnlInventario_btnBorrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInventario_btnBorrar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInventario_btnLimpiar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInventario_btnLimpiar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInventario_btnPuntoDecimal_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInventario_btnPuntoDecimal_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInventario_btnCalculadora_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlInventario_btnCalculadora_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInventario_ltvInventario_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                controlador.pnlInventario_ltvInventario_ItemActivate();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task menu_close_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.vistaAuditoria_Closed();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInventario_chkAjustarColumnas_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            try
            {
                controlador.pnlInventario_chkAjustarColumnas_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInventario_btnOne_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInventario_btnOne_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInventario_btnTwo_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInventario_btnTwo_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInventario_btnThree_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInventario_btnThree_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInventario_btnFour_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInventario_btnFour_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInventario_btnFive_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInventario_btnFive_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInventario_btnSix_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInventario_btnSix_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInventario_btnSeven_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInventario_btnSeven_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInventario_btnEight_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInventario_btnEight_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInventario_btnNine_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInventario_btnNine_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInventario_btnZero_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlInventario_btnZero_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlInventario_btnAgregarCantidad_Clicked(object sender, EventArgs e)
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
    }
}