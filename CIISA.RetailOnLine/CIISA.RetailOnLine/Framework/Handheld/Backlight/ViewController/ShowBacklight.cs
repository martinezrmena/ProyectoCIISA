using CIISA.RetailOnLine.Framework.Handheld.Backlight.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.Backlight.ViewController
{
    public class ShowBacklight
    {

        public void showBacklightForm()
        {
            Application.Current.MainPage.Navigation.PushAsync(new viewBackLight());
        }

    }
}
