using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.MemorySpace.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.MemorySpace.ViewController
{
    public class ShowMemorySpace
    {
        public void showMemorySpaceForm(SystemCIISA psystemCIISA)
        {
            Application.Current.MainPage.Navigation.PushAsync(new viewMemorySpace(psystemCIISA));
        }
    }
}
