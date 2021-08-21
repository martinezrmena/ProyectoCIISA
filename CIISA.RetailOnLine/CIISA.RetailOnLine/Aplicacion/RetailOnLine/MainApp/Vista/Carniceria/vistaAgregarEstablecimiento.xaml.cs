using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.Controlador;
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
    public partial class vistaAgregarEstablecimiento : PopupPage
    {

        internal ctrlAgregarEstablecimiento controlador = null;
        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        /// <summary>
        ///Pantalla encargada de permitir seleccionar el establecimiento correcto de un cliente
        ///en caso de que posea mas de uno
        /// </summary>
        /// return Establecimiento seleccionado para continuar el flujo en seleccion del cliente
        public vistaAgregarEstablecimiento()
        {
            InitializeComponent();

            controlador = new ctrlAgregarEstablecimiento(this);

            controlador.ScreenInicialization();
        }

        public vistaAgregarEstablecimiento(ctrlCliente ctrl, Cliente pobjCliente)
        {
            InitializeComponent();

            controlador = new ctrlAgregarEstablecimiento(this);

            controlador.ScreenInicialization(ctrl, pobjCliente);
        }

        private async Task btnSeleccionar_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await controlador.SeleccionarEstablecimiento();
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