using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
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
    public partial class vistaTramite : ContentPage
    {
        private ctrlTramite controlador = null;
        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaTramite(Cliente pobjCliente,vistaVisita pviewVisita)
        {
            controlador = new ctrlTramite(this,pviewVisita);

            InitializeComponent();

            controlador.ScreenInicialization(pobjCliente);
        }

        private async Task menu_mniTramitar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlTramite_stkTramitar);
                await controlador.menu_mniTramitar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlTramite_stkTramitar);
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            try
            {
                await controlador.Cerrando();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }            
        }
    }
}