using CIISA.RetailOnLine.Framework.Handheld.GPS.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.GPS.ViewController
{
    public class ShowGPS
    {

        public void showGpsForm()
        {
            Application.Current.MainPage.Navigation.PushAsync(new viewGPS());
        }

    }
}
