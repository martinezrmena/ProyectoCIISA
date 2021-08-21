using Acr.UserDialogs;
using Android.App;
using Android.App.Admin;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using Plugin.CurrentActivity;
using Plugin.Media;
using Rg.Plugins.Popup.Services;
using System;
using System.Globalization;
using ZXing.Mobile;
using Xamarin.Forms.GoogleMaps.Android;
using CIISA.RetailOnLine.Droid.Framework.External.GpsThings;

namespace CIISA.RetailOnLine.Droid
{
    [Activity(Label = "Retail On Line", Icon = "@drawable/logoCiisa", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal Tipos_Carga tipos_Carga = new Tipos_Carga();

        protected async override void OnCreate(Bundle bundle)
        {
            CultureInfo.CurrentCulture = new CultureInfo("es-US", false);

            DevicePolicyManager devicePolicyManager = (DevicePolicyManager)GetSystemService(Android.Content.Context.DevicePolicyService);
            ComponentName demoDeviceAdmin = new ComponentName(this, Java.Lang.Class.FromType(typeof(DeviceAdmin)));
            Intent intent = new Intent(DevicePolicyManager.ActionAddDeviceAdmin);
            intent.PutExtra(DevicePolicyManager.ExtraDeviceAdmin, demoDeviceAdmin);
            intent.PutExtra(DevicePolicyManager.ExtraAddExplanation, "Dispositivo Administrador");
            StartActivity(intent);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            #region Inicialización de Plugins

            //Plugin para mensajes
            UserDialogs.Init(this);

            //Plugin para PopUp
            Rg.Plugins.Popup.Popup.Init(this, bundle);

            //Plugin para recursos media
            await CrossMedia.Current.Initialize();
            CrossCurrentActivity.Current.Init(this, bundle);

            //Plugin para escaner de código de barras
            MobileBarcodeScanner.Initialize(this.Application);

            //Plugin para Xamarin.Essentials
            Xamarin.Essentials.Platform.Init(this, bundle);

            #endregion

            global::Xamarin.Forms.Forms.Init(this, bundle);

            // Override default BitmapDescriptorFactory by your implementation. 
            var platformConfig = new PlatformConfig
            {
                BitmapDescriptorFactory = new CachingNativeBitmapDescriptorFactory()
            };

            Xamarin.FormsGoogleMaps.Init(this, bundle, platformConfig); // initialize for Xamarin.Forms.GoogleMaps

            LoadApplication(new App());
        }

        protected async override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            String accion = tipos_Carga.AperturaNocturna;

            if (intent.HasExtra(Intent.ExtraText))
            {
                String message = intent.GetStringExtra(Intent.ExtraText);

                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention(message);

                if (message.Contains(accion))
                {
                    ProcesoImpresion _impresion = new ProcesoImpresion();
                    await _impresion.imprimirReporteInventarioTeorico();
                }
            }
        }

        //Manipula el plugin para cerrar el pop up con el boton del dispositivo Hardware
        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Hacer algo si hay alguna página en el `PopupStack`
                //PopupNavigation.Instance.PopAsync(true);
            }
            else
            {
                // Hacer algo si NO hay alguna página en el `PopupStack`
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}

