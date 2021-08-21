using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RadioTelefonico.Controlador;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RadioTelefonico.Vista
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class vistaTelefono : ContentPage
	{
        public ctrlTelefono controlador = null;

        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaTelefono()
        {
            controlador = new ctrlTelefono(this);

            InitializeComponent();

            pnlTelefono_cbxVPN.SelectedIndexChanged -= pnlTelefono_cbxVPN_SelectedIndexChanged;

            controlador.ScreenInicialization();

            pnlTelefono_cbxVPN.SelectedIndexChanged += pnlTelefono_cbxVPN_SelectedIndexChanged;

        }

        private async Task pnlTelefono_btnBorrarBuscar_Clicked(object sender, EventArgs e)
        {

            try
            {
                controlador.pnlTelefono_btnBorrarBuscar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }            
        }

        private async Task pnlTelefono_btnLimpiarBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlTelefono_btnLimpiarBuscar_Click();
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
                await simulateClickGestures.SelectedStack(pnlTelefono_stkCerrar);
                controlador.menu_mniClose_Click();

            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            await simulateClickGestures.SelectedStack(pnlTelefono_stkCerrar);
        }

        private async Task menu_mniCallCenter_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlTelefono_stkCallCenter);
                controlador.menu_mniCallCenter_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlTelefono_stkCallCenter);
        }

        private async Task menu_mniCentralTelefonica_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlTelefono_stkCentralTelfonica);
                controlador.menu_mniCentralTelefonica_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlTelefono_stkCentralTelfonica);
        }

        private async Task pnlTelefono_btnVPNLiquidaciones_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlTelefono_btnVPNLiquidaciones_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlTelefono_btnVPNGerson_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlTelefono_btnVPNGerson_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void pnlTelefono_cbxVPN_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlTelefono_cbxVPN_SelectedIndexChanged();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlTelefono_btnBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlTelefono_btnBuscar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlTelefono_bntLlamar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlTelefono_bntLlamar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

    }
}