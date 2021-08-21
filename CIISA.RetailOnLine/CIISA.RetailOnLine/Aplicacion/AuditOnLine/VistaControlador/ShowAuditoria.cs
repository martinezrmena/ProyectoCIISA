using CIISA.RetailOnLine.Aplicacion.AuditOnLine.Vista;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.AuditOnLine.VistaControlador
{
    public class ShowAuditoria
    {
        public void mostrarPantallaAuditoria()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaAuditoria());
        }

    }
}
