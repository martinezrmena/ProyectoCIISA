using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Framework.External.DeviceInformation
{
    public interface IService_DeviceInformation
    {
        Task<DeviceInformation> GetDeviceInformation();

        StorageInfo GetInternalStorageInformation();
    }
}
