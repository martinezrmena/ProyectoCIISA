using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.GPS.ViewController;
//using CIISA.RetailOnLine.DatosDePrueba;
using Xamarin.Forms;
using System.Globalization;

namespace CIISA.RetailOnLine
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            string cultura = CultureInfo.CurrentCulture.Name;

            MainPage = new NavigationPage(new vistaIdentificarUsuario());

            GlobalVariables.SystemVariables(ROLSystem.getName(), ROLSystem.getInitials(), VersionROL._versionSROL);

            GPS_Info.initializeGPS();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
