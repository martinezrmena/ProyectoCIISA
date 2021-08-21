using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista.Carniceria
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaPedidos : ContentPage
    {
        private ctrlPedidos controlador = null;

        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaPedidos(Cliente pobjCliente, vistaVisita v_view)
        {
            controlador = new ctrlPedidos(this, v_view);

            InitializeComponent();

            controlador.ScreenInicialization(pobjCliente);
        }

        private async Task menu_mniAgregar_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await simulateClickGestures.SelectedStack(pnlPedidos_stkAgregar);
                    await controlador.menu_mniAgregar_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }

                await simulateClickGestures.NoSelectedStack(pnlPedidos_stkAgregar);
            }
        }
    }
}