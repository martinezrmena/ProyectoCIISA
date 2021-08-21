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
    public partial class vistaEntregaPedidos : ContentPage
    {
        private ctrlEntregaPedidos controlador = null;

        public vistaEntregaPedidos()
        {
            controlador = new ctrlEntregaPedidos(this);

            InitializeComponent();

            controlador.ScreenInicialization();
        }

        private async void pnlTransaccionDetalles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlTransaccionDetalles_SelectedIndexChanged();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
    }
}