using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers
{
    internal class SimulateClickGestures
    {
        internal async Task SelectedStack(StackLayout stk)
        {
            stk.IsEnabled = false;

            await Task.Run(() =>
                   Device.BeginInvokeOnMainThread(() =>
                        stk.BackgroundColor = Color.FromHex("#414A4C")
                   )
                ).ConfigureAwait(true);
        }

        internal async Task NotEnabled(Button btn)
        {
            await Task.Run(() =>
                   Device.BeginInvokeOnMainThread(() =>
                        {
                            btn.IsEnabled = false;
                        }
                   )
                ).ConfigureAwait(true);
        }

        internal async Task NoSelectedStack(StackLayout stk)
        {
            await Task.Run(() =>
                   Device.BeginInvokeOnMainThread(() =>
                        stk.BackgroundColor = Color.FromHex("#141414")
                   )
                ).ConfigureAwait(true);

            stk.IsEnabled = true;
        }

        internal async Task Enabled(Button btn)
        {
            await Task.Run(() =>
                   Device.BeginInvokeOnMainThread(() =>
                   {
                       btn.IsEnabled = true;
                   }
                   )
                ).ConfigureAwait(true);
        }

    }
}
