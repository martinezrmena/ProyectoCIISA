using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CIISA.RetailOnLine.Droid.Framework.External.External;
using CIISA.RetailOnLine.Framework.External.DeviceInformation;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidBrightnessService))]
namespace CIISA.RetailOnLine.Droid.Framework.External.External
{

    public class AndroidBrightnessService : IBrightnessService
    {
        public void SetBrightness(float brightness)
        {
            try
            {
                //Versiones mas recientes
                var window = CrossCurrentActivity.Current.Activity.Window;
                WindowManagerLayoutParams windowManagerLayoutParams = new WindowManagerLayoutParams();
                windowManagerLayoutParams.CopyFrom(window.Attributes);
                windowManagerLayoutParams.ScreenBrightness = brightness; //set screen to full brightness
                window.Attributes = windowManagerLayoutParams;
            }
            catch {
                //Versiones anteriores de android
                var windows = ((Activity)Android.App.Application.Context).Window;
                WindowManagerLayoutParams windowManagerLayoutParams = new WindowManagerLayoutParams();
                windowManagerLayoutParams.CopyFrom(windows.Attributes);
                windowManagerLayoutParams.ScreenBrightness = brightness;
                windows.Attributes = windowManagerLayoutParams;
            }
        }

        public float GetBrightness() {

            try
            {
                //Versiones mas recientes
                var window = CrossCurrentActivity.Current.Activity.Window;
                WindowManagerLayoutParams windowManagerLayoutParams = new WindowManagerLayoutParams();
                windowManagerLayoutParams.CopyFrom(window.Attributes);
                return windowManagerLayoutParams.ScreenBrightness;
            }
            catch
            {
                //Versiones anteriores de android
                var windows = ((Activity)Android.App.Application.Context).Window;
                WindowManagerLayoutParams windowManagerLayoutParams = new WindowManagerLayoutParams();
                windowManagerLayoutParams.CopyFrom(windows.Attributes);
                return windowManagerLayoutParams.ScreenBrightness;
            }

        }



    }
}