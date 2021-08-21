using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador.DevolucionFactura;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using Rg.Plugins.Popup.Pages;
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
	public partial class vistaTipoDevolucion : PopupPage
    {
        internal ctrlTipoDevolucion controlador = null;

        public vistaTipoDevolucion (vistaVisita vistaVisita,  bool valid)
		{
            InitializeComponent();

            controlador = new ctrlTipoDevolucion(this, vistaVisita, valid);

            controlador.ScreenInicialization();
        }

        private async Task btnSeleccionar_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await controlador.SeleccionarTipoDevolucion();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}