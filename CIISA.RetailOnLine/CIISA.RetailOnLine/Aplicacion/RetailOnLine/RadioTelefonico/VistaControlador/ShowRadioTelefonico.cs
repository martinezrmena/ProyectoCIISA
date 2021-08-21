using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RadioTelefonico.Vista;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RadioTelefonico.VistaControlador
{
    public class ShowRadioTelefonico
    {

        public void mostrarPantallaTelefono()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaTelefono());
        }

    }
}
