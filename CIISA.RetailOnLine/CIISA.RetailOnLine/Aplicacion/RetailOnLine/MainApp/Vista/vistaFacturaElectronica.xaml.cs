using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using Rg.Plugins.Popup.Pages;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaFacturaElectronica : PopupPage
    {
        internal ctrlFacturaElectronica controlador = null;
        internal bool guardar = false;
        internal ctrlVisita ctrlVisita = null;
        internal ctrlCarniceria ctrlCarniceria = null;


        internal vistaFacturaElectronica(ctrlCarniceria carniceria, string TipoDocumento)
        {
            InitializeComponent();
            ctrlCarniceria = carniceria;
            controlador = new ctrlFacturaElectronica(this, TipoDocumento, PantallasSistema._pantallaCarniceria);
        }

        internal vistaFacturaElectronica(ctrlVisita visita, string TipoDocumento)
        {
            InitializeComponent();
            ctrlVisita = visita;
            controlador = new ctrlFacturaElectronica(this, TipoDocumento, PantallasSistema._pantallaVisita);
        }

        private async Task pnlFacturaElectronicabtnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                guardar = true;
                await controlador.Guardar();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        //Al desaparecer el pop up
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (!guardar)
            {
                //Cuando no se guarda debemos devolver los parametros a una forma anterior

            }

        }

        private async void pnlFacturaElectronica_btnEliminarOrdenCompra_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlFacturaElectronica_EliminarOrdenCompra();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void pnlFacturaElectronica_btnEliminarTodosOrdenCompra_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlFacturaElectronica_EliminarTodosOrdenCompra();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void pnlFacturaElectronica_btnEliminarNumeroRecibo_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlFacturaElectronica_EliminarRecibo();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void pnlFacturaElectronica_btnEliminarTodosNumeroRecibo_Clicked(object sender, EventArgs e)
        {

            try
            {
                controlador.pnlFacturaElectronica_EliminarTodosRecibo();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

        }

        private async void pnlFacturaElectronica_btnEliminarNumeroReclamo_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlFacturaElectronica_EliminarReclamo();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void pnlFacturaElectronica_btnEliminarTodosNumeroReclamo_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlFacturaElectronica_EliminarTodosReclamo();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
    }
}