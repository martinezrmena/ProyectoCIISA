using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador.DevolucionFactura;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista.DevolucionFactura
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaDevolucionFactura : ContentPage
    {

        private ctrlDevolucionFactura controlador = null;

        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaDevolucionFactura(Cliente pobjCliente, vistaVisita v_view)
        {
            controlador = new ctrlDevolucionFactura(this, v_view);

            InitializeComponent();

            controlador.ScreenInicialization(pobjCliente);
        }

        private async Task menu_mniAgregar_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await simulateClickGestures.SelectedStack(pnlDevolucionFactura_stkAgregar);
                    await controlador.menu_mniAgregar_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }

                await simulateClickGestures.NoSelectedStack(pnlDevolucionFactura_stkAgregar);
            }
        }
    }
}