using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.IdentificarUsuario;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Display.View;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.Display.ViewController
{
    public class ShowDisplay
    {
        public async Task showAccessCodeForm2(string paccessCode, string pmessage, ctrlMenu pctrl,string ppantalla)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(
                new NavigationPage(
                    new viewAccessCode(
                        paccessCode, 
                        pmessage,
                        pctrl,
                        ppantalla)));
        }

        public void showAccessCodeForm2(string paccessCode, string pmessage, ctrlIdentificarUsuario pctrl, string ppantalla)
        {
            Application.Current.MainPage.Navigation.PushModalAsync(
                new NavigationPage(
                    new viewAccessCode(
                        paccessCode,
                        pmessage,
                        pctrl,
                        ppantalla)));
        }
        public void showAccessCodeForm2(string paccessCode, string pmessage, LogicaIdentificarUsuario_Verificar LIUV, string ppantalla)
        {
            Application.Current.MainPage.Navigation.PushModalAsync(
                new NavigationPage(
                    new viewAccessCode(
                        paccessCode,
                        pmessage,
                        LIUV,
                        ppantalla)));
        }

        public void showLockScreenForm(SystemCIISA psystemCIISA)
        {
            Application.Current.MainPage.Navigation.PushAsync(new viewLockScreen(psystemCIISA));
        }
    }
}
