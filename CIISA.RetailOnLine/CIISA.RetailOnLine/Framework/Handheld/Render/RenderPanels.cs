using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.Render
{
    public static class RenderPanels
    {
        public static void paintPanel(StackLayout ppanel)
        {
            ppanel.IsVisible = false;
            ppanel.BackgroundColor = Color.White;
            ppanel.IsEnabled = true;
            ppanel.Focus();
        }
    }
}
