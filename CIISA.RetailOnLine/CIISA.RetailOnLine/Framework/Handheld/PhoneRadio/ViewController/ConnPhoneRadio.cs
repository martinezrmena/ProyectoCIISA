using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Framework.Handheld.PhoneRadio.ViewController
{
    public class ConnPhoneRadio
    {
        public enum CONNECTIONSTATUS : uint
        {
            CONNMGR_STATUS_UNKNOWN = 0x00,
            CONNMGR_STATUS_CONNECTED = 0x10,
            CONNMGR_STATUS_DISCONNECTED = 0x20,
            CONNMGR_STATUS_CONNECTIONFAILED = 0x21,
            CONNMGR_STATUS_CONNECTIONCANCELED = 0x22,
            CONNMGR_STATUS_CONNECTIONDISABLED = 0x23,
            CONNMGR_STATUS_NOPATHTODESTINATION = 0x24,
            CONNMGR_STATUS_WAITINGFORPATH = 0x25,
            CONNMGR_STATUS_WAITINGFORPHONE = 0x26,
            CONNMGR_STATUS_WAITINGCONNECTION = 0x40,
            CONNMGR_STATUS_WAITINGFORRESOURCE = 0x41,
            CONNMGR_STATUS_WAITINGFORNETWORK = 0x42,
            CONNMGR_STATUS_WAITINGDISCONNECTION = 0x80,
            CONNMGR_STATUS_WAITINGCONNECTIONABORT = 0x81
        }

        //public uint CONN_IsConnected()
        //{
        //    uint connectionStatus = (uint)CONNECTIONSTATUS.CONNMGR_STATUS_UNKNOWN;

        //    ConnMgrConnectionStatus(conCurrentDevice.connHandle, out connectionStatus);

        //    return connectionStatus;
        //}
    }
}
