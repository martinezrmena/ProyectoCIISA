using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Framework.External
{
    public interface IServicio_Phone
    {
        //string NetworkType();

        string PhoneType();

        string GetSignalStrength();

        string GetSimStatus();

        string GetNetworkOperator();

        string GetNetworkStatus();

        string GetIMEI();

        string GetModel();

        string GetRevision();

        string GetManufacturer();

        string GetSIMID();
    }
}
