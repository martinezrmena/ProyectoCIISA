using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Consulta.Vista;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Consulta.VistaControlador
{
    public class ShowConsulta
    {
        public void mostrarPantallaConsultaOV()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaConsultaOV());
        }

        public void mostrarPantallaConsultaDocumentos()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaConsultaDocumentos());
        }

        public void mostrarPantallaConsultaReciboRecaudacion()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaConsultaReciboRecaudacion());
        }
    }
}
