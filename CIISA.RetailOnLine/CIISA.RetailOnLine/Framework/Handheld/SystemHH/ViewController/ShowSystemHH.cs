using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CIISA.RetailOnLine.Framework.Handheld.SystemHH.View;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;

//NECESARIO PARA UTILIZAR EL NAVIGATION
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.SystemHH.ViewController
{
    class ShowSystemHH
    {
        public void showAboutForm(SystemCIISA psystemCIISA)
        {
            Application.Current.MainPage.Navigation.PushAsync(new viewAbout(psystemCIISA));

        }
    }
}
