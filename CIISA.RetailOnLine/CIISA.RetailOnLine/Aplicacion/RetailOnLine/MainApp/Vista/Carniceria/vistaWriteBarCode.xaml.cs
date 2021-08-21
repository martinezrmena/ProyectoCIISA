using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador.Carniceria;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using Rg.Plugins.Popup.Pages;
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
    public partial class vistaWriteBarCode : PopupPage
    {
        internal ctrlWriteBarCode controlador = null;

        //DEPRACATED: AL FINAL EL CLIENTE PIDIO NO REALIZARLO
        /// <summary>
        ///Pantalla encargada de realizar la escritura manual del codigo de barras
        /// </summary>
        /// return Codigo de barra para agregar en vistaCarniceria
        public vistaWriteBarCode(ctrlCarniceria ctrl)
        {
            InitializeComponent();

            controlador = new ctrlWriteBarCode(this, ctrl);

            controlador.ScreenInicialization();
        }

        private async Task btnAceptar_Clicked(object sender, EventArgs e)
        {
            try
            {

                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.Aceptar_Click();
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
    }
}