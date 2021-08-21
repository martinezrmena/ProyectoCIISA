using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using CIISA.RetailOnLine.Framework.External.CustomTreeView;
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
    public partial class vistaInventarioPedidos : ContentPage
    {
        private ctrlInventarioPedidos controlador = null;

        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaInventarioPedidos(string pcodProducto)
        {
            controlador = new ctrlInventarioPedidos(this);

            InitializeComponent();

            controlador.ScreenInicialization(pcodProducto);
        }

        private async void pnlInventario_chbExpandir_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            try
            {
                controlador.pnlInventario_chbExpandir_CheckStateChanged();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void menu_mniCerrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlInventario_stkCerrar);
                controlador.menu_mniCerrar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlInventario_stkCerrar);
        }
    }
}