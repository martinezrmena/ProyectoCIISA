using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Controlador;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.VistaControlador
{
    public class ShowCarga
    {
        public void mostrarPantallaCarga(int ptipoCarga)
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaCarga(ptipoCarga));
        }

        public void mostrarPantallaClienteInactivo(ctrlCarga_cliente pctrlCarga_Cliente)
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new vistaClienteInactivo(pctrlCarga_Cliente)));
        }
    }
}
