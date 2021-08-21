using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers
{
    public class BrowserView
    {
        public const int TimeOutLimit = 10;

        public async Task OpenBrowser(Uri uri)
        {
            await Browser.OpenAsync(uri);
        }
    }
}
