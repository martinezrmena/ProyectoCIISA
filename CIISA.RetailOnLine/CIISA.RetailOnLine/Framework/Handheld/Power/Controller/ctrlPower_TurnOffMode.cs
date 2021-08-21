using CIISA.RetailOnLine.Framework.Handheld.Display.IMainLockScreen;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.Power.Controller
{
    public class ctrlPower_TurnOffMode
    {
        
        public void btnTurnOff_Click()
        {
            var LockScreenActivate = DependencyService.Get<IAndroidMethods>();
            LockScreenActivate.LockScreenPhone();
        }
        
        public void btnSuspend_Click()
        { 
            var LockScreenActivate = DependencyService.Get<IAndroidMethods>();
            LockScreenActivate.SaveBattery();

        }
    }
}