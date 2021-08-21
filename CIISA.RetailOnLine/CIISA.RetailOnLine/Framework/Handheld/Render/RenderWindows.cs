using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.Render
{
    public static class RenderWindows
    {
        public static void paintWindow(ContentPage pform)
        {
            pform.Title = string.Empty;            
            pform.BackgroundColor = Color.FromHex("414A4C");
            pform.IsVisible = true;
            pform.IsEnabled = true;
        }
    }
}
