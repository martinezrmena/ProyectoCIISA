using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
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
	public partial class vistaIndicadoresFacturacion : ContentPage
	{
        private ctrlIndicadoresFacturacion controlador = null;

        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaIndicadoresFacturacion()
        {
            controlador = new ctrlIndicadoresFacturacion(this);

            InitializeComponent();

            controlador.ScreenInicialization();
        }

        private async Task menu_btnBuscar_Clicked(object sender, EventArgs e)
        {
            await simulateClickGestures.SelectedStack(pnlIndicadores_stckBuscar);

            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    controlador.menu_btnBuscar_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }

            await simulateClickGestures.NoSelectedStack(pnlIndicadores_stckBuscar);
        }

        private async Task menu_btnCerrar_Clicked(object sender, EventArgs e)
        {

            await simulateClickGestures.SelectedStack(pnlIndicadores_stkCerrar);

            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    controlador.menu_btnCerrar_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }

            await simulateClickGestures.NoSelectedStack(pnlIndicadores_stkCerrar);
        }

        private async void pnlIndicadores_ltvIndicadores_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            try
            {
                controlador.pnlIndicadores_txtCodCliente_Focus();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        
        }
    }
}