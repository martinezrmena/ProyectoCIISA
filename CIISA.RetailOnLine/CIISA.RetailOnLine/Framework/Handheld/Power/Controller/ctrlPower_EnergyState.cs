using CIISA.RetailOnLine.Framework.Handheld.Display.IMainLockScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.Power.Controller
{
    public class ctrlPower_EnergyState
    {
        internal string v_error = null;

        public ctrlPower_EnergyState()
        {
            var bat = DependencyService.Get<IBattery>();
            switch (bat.PowerSource)
            {
                case PowerSource.Battery:
                    v_error = "Bateria - ";
                    break;
                case PowerSource.Ac:
                    v_error = "AC - ";
                    break;
                case PowerSource.Usb:
                    v_error = "USB - ";
                    break;
                case PowerSource.Wireless:
                    v_error = "Inalámbrico - ";
                    break;
                case PowerSource.Other:
                default:
                    v_error = "Otro - ";
                    break;
            }
            switch (bat.Status)
            {
                case BatteryStatus.Charging:
                    v_error += "Cargando";
                    break;
                case BatteryStatus.Discharging:
                    v_error += "Descargandose";
                    break;
                case BatteryStatus.NotCharging:
                    v_error += "Sin cargar";
                    break;
                case BatteryStatus.Full:
                    v_error += "Completo";
                    break;
                case BatteryStatus.Unknown:
                default:
                    v_error += "Desconocido";
                    break;
            }

            v_error += " ";
            v_error += bat.RemainingChargePercent + "%";

        }

        public void pnlPower_btnFullOn_Click()
        {
            var LockScreenActivate = DependencyService.Get<IAndroidMethods>();
            LockScreenActivate.DisableSaveBattery();
        }
    }
}
