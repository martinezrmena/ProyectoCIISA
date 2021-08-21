using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using Xamarin.Forms;
using CIISA.RetailOnLine.Aplicacion.InventoryOnLine._View;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.InventoryOnLine.ViewController
{
    class ShowTomaFisica
    {
        public async Task mostrarPantallaTomaFisica()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new vistaTomaFisica());
        }

    }
}
