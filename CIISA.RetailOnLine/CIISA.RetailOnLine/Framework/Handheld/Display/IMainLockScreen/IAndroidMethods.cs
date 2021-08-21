using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Framework.Handheld.Display.IMainLockScreen
{
    public interface IAndroidMethods
    {
        void LockScreenPhone();

        void SaveBattery();

        void DisableSaveBattery();

        Task<bool> ValueDisableSaveBattery();

        void PushNotification(string Message, string Result);
    }
}
