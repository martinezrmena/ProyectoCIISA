using CIISA.RetailOnLine.Framework.External.DeviceInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.MemorySpace.ViewController
{
    public class DeviceMemoryInfo
    {
        private DeviceInformation DI = null;
        private long v_freeBytesAvailable = 0;
        private long v_totalBytes = 0;
        private string v_directoryName = string.Empty;
        private long v_totalFreeBytes = 0;

        public long totalFreeBytes
        {
            get
            {
                getStoreageSize();
                return v_totalFreeBytes;
            }
        }

        public string directoryName
        {
            get
            {
                return v_directoryName;
            }
            set
            {
                v_directoryName = value;
            }
        }

        public long freeBytesAvailable
        {
            get
            {
                getStoreageSize();

                return v_freeBytesAvailable;
            }

        }

        public void getStoreageSize()
        {
            DiskFreeSpace _diskFreeSpace = new DiskFreeSpace();          
            var InternalMemory = DependencyService.Get<IService_DeviceInformation>().GetInternalStorageInformation();

            _diskFreeSpace._freeBytesAvailable = InternalMemory.AvailableSpace;
            _diskFreeSpace._totalBytes = InternalMemory.TotalSpace;
            _diskFreeSpace._totalFreeBytes = InternalMemory.FreeSpace;

            v_freeBytesAvailable = _diskFreeSpace._freeBytesAvailable;
            v_totalBytes = _diskFreeSpace._totalBytes;
            v_totalFreeBytes = _diskFreeSpace._totalFreeBytes;
            
        }

        public long totalBytes
        {
            get
            {
                getStoreageSize();

                return v_totalBytes;
            }

        }

        public async Task TomarInformacion()
        {
            if (DI == null)
            {
                var Servicio = DependencyService.Get<IService_DeviceInformation>();
                DI = await Servicio.GetDeviceInformation();
                v_freeBytesAvailable = DI.FreeInternalStorage;
                v_totalBytes = DI.TotalInternalStorage;
            }            
        }

        public async Task<int> percentajeFreeMemory()
        {
            await TomarInformacion();

            long _percentage = Math.Abs((freeBytesAvailable * 100) / totalBytes);
            
            return (int)_percentage;
        }

        public async Task<bool> maintenanceRequired()
        {
            bool _maintenanceReq = false;

            int _percentage = await percentajeFreeMemory();

            if (_percentage < 10)
            {
                _maintenanceReq = true;
            }
            else
            {
                _maintenanceReq = false;
            }

            return _maintenanceReq;
        }

        public async Task<bool> hasStorageCardPresent()
        {
            return await isStorageCard();
        }

        private async Task<bool> isStorageCard()
        {
            bool _hasStorageCard = false;
            await TomarInformacion();

            if (DI.HasRemovableExternalStorage)
            {
                _hasStorageCard = true;
            }
            else
            {
                _hasStorageCard = false;
            }

            return _hasStorageCard;
        }


    }
}
