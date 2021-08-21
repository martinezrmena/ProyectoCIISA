using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CIISA.RetailOnLine.Droid.Aplicacion.Helpers;
using CIISA.RetailOnLine.Framework.External;
using Xamarin.Forms;

[assembly: Dependency(typeof(Version_Android))]
namespace CIISA.RetailOnLine.Droid.Aplicacion.Helpers
{
    public class Version_Android: IAppVersion
    {

        public string GetVersion()
        {
            var context = Android.App.Application.Context;

            PackageManager manager = context.PackageManager;
            PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionName;
        }

        public int GetBuild()
        {
            var context = Android.App.Application.Context;
            PackageManager manager = context.PackageManager;
            PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionCode;
        }

    }
}