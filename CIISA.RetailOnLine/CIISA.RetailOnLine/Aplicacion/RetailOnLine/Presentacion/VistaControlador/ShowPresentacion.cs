using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RadioTelefonico.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Controlador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.VistaControlador
{
    public class ShowPresentacion
    {
        //Cliente v_objeto_cliente = null;

        public async Task mostrarPantallaNuevoCliente()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new vistaNuevoCliente()));
        }

        public async Task mostrarPantallaCliente(vistaTelefono pview, string ppantalla)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new vistaCliente(pview,ppantalla)));
        }

        public async Task mostrarPantallaReporte()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new vistaReporte());
        }

        public async Task mostrarPantallaCliente(vistaReporte ppantalla, bool pshowControlBox)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new vistaCliente(ppantalla,PantallasSistema._pantallaReporteCDC)));
        }

        public async Task mostrarPantallaCliente(ctrlCarga_indicadores pcontroler, string ppantalla)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new vistaCliente(pcontroler, ppantalla)));
        }

        public async Task mostrarPantallaCliente(ctrlCarga_descuentos pcontroler, string ppantalla)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new vistaCliente(pcontroler, ppantalla)));
        }
    }
}
