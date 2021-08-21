using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
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
    public partial class vistaConfiguracion : ContentPage
    {
        private ctrlConfiguracion controlador = null;

        public vistaConfiguracion()
        {
            controlador = new ctrlConfiguracion(this);

            InitializeComponent();

            controlador.ScreenInicialization();
        }

        private async void menu_mniClose_Clicked(object sender, EventArgs e)
        {

            try
            {
                controlador.menu_mniClose_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void pnlConfiguracion_btnPruebaImpresion_Clicked(object sender, EventArgs e)
        {

            try
            {
                controlador.pnlConfiguracion_btnPruebaImpresion_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
    }
}