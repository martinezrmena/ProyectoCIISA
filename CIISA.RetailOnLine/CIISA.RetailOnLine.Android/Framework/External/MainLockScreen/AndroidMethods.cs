using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.App.Admin;
using Android.Bluetooth;
using Android.Content;
using Android.Net.Wifi;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CIISA.RetailOnLine.Droid.Framework.External.MainLockScreen;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Handheld.Display.IMainLockScreen;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidMethods))]
namespace CIISA.RetailOnLine.Droid.Framework.External.MainLockScreen
{
    public class AndroidMethods:IAndroidMethods
    {
        private BluetoothManager _manager;
        private WifiManager wifi;

        public async void LockScreenPhone() {

            DevicePolicyManager devicePolicyManager = (DevicePolicyManager)Android.App.Application.Context.GetSystemService(Android.Content.Context.DevicePolicyService);
            ComponentName demoDeviceAdmin = new ComponentName(Android.App.Application.Context, Java.Lang.Class.FromType(typeof(DeviceAdmin)));

            if (devicePolicyManager.IsAdminActive(demoDeviceAdmin))
            {
                devicePolicyManager.LockNow();
            }
            else {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention("NO POSEE LOS PERMISOS DE ADMINISTRADOR.");
            }
        }

        public void Reboot()
        {
            //Intent _intent = new Intent(Android.Provider.Settings.);
            //_intent.AddFlags(ActivityFlags.NewTask);
            //Android.App.Application.Context.StartActivity(_intent);
        }

        public async void DisableSaveBattery() {

            PowerManager pm = (PowerManager)Android.App.Application.Context.GetSystemService(Context.PowerService);
            try
            {
                if (pm.IsPowerSaveMode == true)
                {
                    Intent _intent = new Intent(Android.Provider.Settings.ActionBatterySaverSettings);
                    _intent.AddFlags(ActivityFlags.NewTask);
                    Android.App.Application.Context.StartActivity(_intent);
                }
                else
                {
                    LogMessageAttention _logMessageAttention = new LogMessageAttention();
                    await _logMessageAttention.generalAttention("EL DISPOSITIVO YA SE ENCUENTRA A PLENA POTENCIA.");
                }
            }
            catch (Exception)
            {
                Toast.MakeText(Android.App.Application.Context, "ESTÁ VERSIÓN DE ANDROID NO POSEE MODO DE AHORRO DE ENERGÍA", ToastLength.Short).Show();
            }
            
        }

        public async Task<bool> ValueDisableSaveBattery()
        {
            PowerManager pm = (PowerManager)Android.App.Application.Context.GetSystemService(Context.PowerService);
            bool value = false;
            try
            {
                if (pm.IsPowerSaveMode == true)
                {
                    LogMessageAttention _logMessageAttention = new LogMessageAttention();
                    await _logMessageAttention.generalAttention("Debe desactivar el modo ahorro de energía para proceder.");
                    Intent _intent = new Intent(Android.Provider.Settings.ActionBatterySaverSettings);
                    _intent.AddFlags(ActivityFlags.NewTask);
                    Android.App.Application.Context.StartActivity(_intent);
                    value = true;
                }
            }
            catch (Exception)
            {
                Toast.MakeText(Android.App.Application.Context, "ESTÁ VERSIÓN DE ANDROID NO POSEE MODO DE AHORRO DE ENERGÍA", ToastLength.Short).Show();
            }

            return value;

        }

        public async void SaveBattery()
        {
            PowerManager pm = (PowerManager)Android.App.Application.Context.GetSystemService(Context.PowerService);
            try
            {
                if (pm.IsPowerSaveMode == false)
                {
                    Intent _intent = new Intent(Android.Provider.Settings.ActionBatterySaverSettings);
                    _intent.AddFlags(ActivityFlags.NewTask);
                    Android.App.Application.Context.StartActivity(_intent);
                }
                else
                {
                    LogMessageAttention _logMessageAttention = new LogMessageAttention();
                    await _logMessageAttention.generalAttention("MODO AHORRO DE ENERGÍA YA SE ENCUENTRA ACTIVO.");
                }
            }
            catch (Exception)
            {
                Toast.MakeText(Android.App.Application.Context, "ESTÁ VERSIÓN DE ANDROID NO POSEE MODO DE AHORRO DE ENERGÍA", ToastLength.Short).Show();
            }


            try
            {
                Wifi_Manager();
                DisableWifi();
                BluetoothAndroid();
                DisableBluetooth();
            }
            catch (Exception)
            {
                Toast.MakeText(Android.App.Application.Context, "NO SE PUDIERON DESACTIVAR TODAS LAS CARACTERÍSTICAS", ToastLength.Short).Show();
            }
            
        }

        private void Wifi_Manager()
        {
            wifi = (WifiManager)Android.App.Application.Context.GetSystemService(Context.WifiService);
        }

        private void EnableWifi()
        {
            wifi.SetWifiEnabled(true);
        }

        private void DisableWifi()
        {
            wifi.SetWifiEnabled(false);
        }

        private void BluetoothAndroid()
        {
            _manager = (BluetoothManager)Android.App.Application.Context.GetSystemService(Android.Content.Context.BluetoothService);
        }

        private void EnableBluetooth()
        {
            _manager.Adapter.Enable();
        }

        private void DisableBluetooth()
        {
            _manager.Adapter.Disable();
        }

        public void PushNotification(string Message, string Result) {

            //CREAMOS ACTIVIDAD PARA ACCEDER A LA APLICACIÓN DESDE LA NOTIFICACIÓN
            Intent intent = new Intent(Android.App.Application.Context, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.SingleTop);
            intent.PutExtra(Intent.ExtraText, Message);
        
            const int pendingIntentId = 1;
            PendingIntent pendingIntent = PendingIntent.GetActivity(Android.App.Application.Context, pendingIntentId, intent, PendingIntentFlags.OneShot);

            //CREAMOS NOTIFICACIÓN
            Notification.Builder builder = new Notification.Builder(Android.App.Application.Context);
            String NotificationMessage = "Pulse para mostrar más información.";
            builder.SetContentTitle(Result);
            builder.SetAutoCancel(true);
            builder.SetContentIntent(pendingIntent);
            builder.SetContentText(NotificationMessage);
            builder.SetSmallIcon(Resource.Drawable.logoCiisa);

            //PETICIÓN PARA HACER QUE EL DISPOSITIVO VIBRE
            Vibrate_Phone();

            //MOSTRAMOS NOTIFICACIÓN
            Notification notificacion = builder.Build();
            NotificationManager notificationmanager = (NotificationManager)Android.App.Application.Context.GetSystemService(Android.Content.Context.NotificationService);
            notificationmanager.Notify(1, notificacion);

            //PETICIÓN PARA QUE LA PANTALLA SE ENCIENDA
            TurnOnScreen();

        }

        internal void Vibrate_Phone() {

            try
            {
                // Use default vibration length
                //Vibration.Vibrate();

                // Or use specified time
                var duration = TimeSpan.FromSeconds(1);
                Vibration.Vibrate(duration);
            }
            catch (FeatureNotSupportedException)
            {
                // Caracteristica no soportada en el dispositivo
            }
            catch (Exception)
            {
                //Otro error ocurrio
            }
        }

        internal void TurnOnScreen() {

            var powerManager = (PowerManager)Android.App.Application.Context.GetSystemService(Context.PowerService);
            var wakeLock = powerManager.NewWakeLock(WakeLockFlags.ScreenDim | WakeLockFlags.AcquireCausesWakeup, "StackOverflow");
            wakeLock.Acquire(3000);
            wakeLock.Release();
        }


    }
}