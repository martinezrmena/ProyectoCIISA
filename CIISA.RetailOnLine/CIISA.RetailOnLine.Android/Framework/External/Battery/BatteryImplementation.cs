using System;
using Android.App;
using Android.Content;
using Android.OS;
using CIISA.RetailOnLine.Droid.Framework.External.Battery;
using CIISA.RetailOnLine.Framework.Handheld.Power.Controller;

[assembly: Xamarin.Forms.Dependency(typeof(BatteryImplementation))]
namespace CIISA.RetailOnLine.Droid.Framework.External.Battery
{
    public class BatteryImplementation: IBattery
    {
        //private BatteryBroadcastReceiver batteryReceiver;
        public BatteryImplementation() { }

        public int RemainingChargePercent
        {
            get
            {
                try
                {
                    using (var filter = new IntentFilter(Intent.ActionBatteryChanged))
                    {
                        using (var battery = Application.Context.RegisterReceiver(null, filter))
                        {
                            var level = battery.GetIntExtra(BatteryManager.ExtraLevel, -1);
                            var scale = battery.GetIntExtra(BatteryManager.ExtraScale, -1);

                            return (int)Math.Floor(level * 100D / scale);
                        }
                    }
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Ensure you have android.permission.BATTERY_STATS");
                    throw;
                }

            }
        }

        public CIISA.RetailOnLine.Framework.Handheld.Power.Controller.BatteryStatus Status
        {
            get
            {
                try
                {
                    using (var filter = new IntentFilter(Intent.ActionBatteryChanged))
                    {
                        using (var battery = Application.Context.RegisterReceiver(null, filter))
                        {
                            int status = battery.GetIntExtra(BatteryManager.ExtraStatus, -1);
                            var isCharging = status == (int)CIISA.RetailOnLine.Framework.Handheld.Power.Controller.BatteryStatus.Charging || status == (int)CIISA.RetailOnLine.Framework.Handheld.Power.Controller.BatteryStatus.Full;

                            var chargePlug = battery.GetIntExtra(BatteryManager.ExtraPlugged, -1);
                            var usbCharge = chargePlug == (int)BatteryPlugged.Usb;
                            var acCharge = chargePlug == (int)BatteryPlugged.Ac;
                            bool wirelessCharge = false;
                            wirelessCharge = chargePlug == (int)BatteryPlugged.Wireless;

                            isCharging = (usbCharge || acCharge || wirelessCharge);
                            if (isCharging)
                                return CIISA.RetailOnLine.Framework.Handheld.Power.Controller.BatteryStatus.Charging;

                            switch (status)
                            {
                                case (int)CIISA.RetailOnLine.Framework.Handheld.Power.Controller.BatteryStatus.Charging:
                                    return CIISA.RetailOnLine.Framework.Handheld.Power.Controller.BatteryStatus.Charging;
                                case (int)CIISA.RetailOnLine.Framework.Handheld.Power.Controller.BatteryStatus.Discharging:
                                    return CIISA.RetailOnLine.Framework.Handheld.Power.Controller.BatteryStatus.Discharging;
                                case (int)CIISA.RetailOnLine.Framework.Handheld.Power.Controller.BatteryStatus.Full:
                                    return CIISA.RetailOnLine.Framework.Handheld.Power.Controller.BatteryStatus.Full;
                                case (int)CIISA.RetailOnLine.Framework.Handheld.Power.Controller.BatteryStatus.NotCharging:
                                    return CIISA.RetailOnLine.Framework.Handheld.Power.Controller.BatteryStatus.NotCharging;
                                default:
                                    return CIISA.RetailOnLine.Framework.Handheld.Power.Controller.BatteryStatus.Unknown;
                            }
                        }
                    }
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Ensure you have android.permission.BATTERY_STATS");
                    throw;
                }
            }
        }

        public PowerSource PowerSource
        {
            get
            {
                try
                {
                    using (var filter = new IntentFilter(Intent.ActionBatteryChanged))
                    {
                        using (var battery = Application.Context.RegisterReceiver(null, filter))
                        {
                            int status = battery.GetIntExtra(BatteryManager.ExtraStatus, -1);
                            var isCharging = status == (int)CIISA.RetailOnLine.Framework.Handheld.Power.Controller.BatteryStatus.Charging || status == (int)CIISA.RetailOnLine.Framework.Handheld.Power.Controller.BatteryStatus.Full;

                            var chargePlug = battery.GetIntExtra(BatteryManager.ExtraPlugged, -1);
                            var usbCharge = chargePlug == (int)BatteryPlugged.Usb;
                            var acCharge = chargePlug == (int)BatteryPlugged.Ac;

                            bool wirelessCharge = false;
                            wirelessCharge = chargePlug == (int)BatteryPlugged.Wireless;

                            isCharging = (usbCharge || acCharge || wirelessCharge);

                            if (!isCharging)
                                return CIISA.RetailOnLine.Framework.Handheld.Power.Controller.PowerSource.Battery;
                            else if (usbCharge)
                                return CIISA.RetailOnLine.Framework.Handheld.Power.Controller.PowerSource.Usb;
                            else if (acCharge)
                                return CIISA.RetailOnLine.Framework.Handheld.Power.Controller.PowerSource.Ac;
                            else if (wirelessCharge)
                                return CIISA.RetailOnLine.Framework.Handheld.Power.Controller.PowerSource.Wireless;
                            else
                                return CIISA.RetailOnLine.Framework.Handheld.Power.Controller.PowerSource.Other;
                        }
                    }
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("Ensure you have android.permission.BATTERY_STATS");
                    throw;
                }
            }
        }
    }
}