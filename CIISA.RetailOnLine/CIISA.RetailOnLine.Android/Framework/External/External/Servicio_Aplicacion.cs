using Android.OS;
using CIISA.RetailOnLine.Droid.Framework.External.External;
using CIISA.RetailOnLine.Framework.External;
using Xamarin.Forms;

[assembly: Dependency(typeof(Servicio_Aplicacion))]
namespace CIISA.RetailOnLine.Droid.Framework.External.External
{
    public class Servicio_Aplicacion: IServicio_Aplicacion
    {
        public void Exit()
        {
            Process.KillProcess(Process.MyPid());
        }
    }
}