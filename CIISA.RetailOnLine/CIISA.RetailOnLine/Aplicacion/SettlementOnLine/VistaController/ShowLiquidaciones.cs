using CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Vista;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.SettlementOnLine.VistaController
{
    public class ShowLiquidaciones
    {
        public void mostrarPantallaLiquidadores()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaMenuSettlementOnLine());
        }

        public void mostrarPantallaLiquidadoresReimpresion()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaReimpresion());
        }

        public void mostrarPantallaLiquidadoresReimpresionAntiguedad()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaReimpresionAntiguedad());
        }
    }
}
