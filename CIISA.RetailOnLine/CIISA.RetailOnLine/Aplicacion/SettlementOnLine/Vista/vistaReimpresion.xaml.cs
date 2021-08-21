using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
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
	public partial class vistaReimpresion : ContentPage
	{
        private ctrlReimpresion controlador = null;

        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaReimpresion()
        {
            controlador = new ctrlReimpresion(this);

            InitializeComponent();

            controlador.ScreenInicialization();
        }

        private async Task menu_mniReimprimir_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlTransacciones_stkImprimir);
                await controlador.menu_mniReimprimir_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlTransacciones_stkImprimir);
        }

        private async Task menu_mniClose_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlTransacciones_stkCerrar);
                controlador.menu_mniClose_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlTransacciones_stkCerrar);
        }

        private async Task pnlTransacciones_ltvTransacciones_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                await controlador.pnlTransacciones_ltvTransacciones_ItemActivate();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
    }
}