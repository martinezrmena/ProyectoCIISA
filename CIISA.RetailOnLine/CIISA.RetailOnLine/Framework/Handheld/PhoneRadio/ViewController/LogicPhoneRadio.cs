using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.PhoneRadio.ViewController
{
    public class LogicPhoneRadio
    {
        public string convertToString(object pobjet)
        {
            return (pobjet == null) ? "<null>" : pobjet.ToString();
        }

        //public void chkWiFi_Click(ListView pltbState, TapiPhoneRadio pobjTapi, ConnPhoneRadio pobjConn)
        //{
        //    pobjConn.CONN_Disconnect();

        //    var Source = pltbState.ItemsSource as ObservableCollection<string>;
        //    Source.Add("Desconectado!");
        //    //pltbState.Items.Add(Resources.disconnected);

        //    pobjTapi.TAPI_SetRadioState(false);
        //    Source.Add("Apago el radio telefónico.");
        //    //pltbState.Items.Add(Resources.radioOff);

        //}
    }
}
