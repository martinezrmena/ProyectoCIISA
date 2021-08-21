using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Framework.External.DeviceInformation
{
    public class DeviceInformation
    {
        /// <summary>
        /// Current battery level 0 - 100
        /// </summary>
        public int BatteryRemainingChargePercent { get; set; }
        /// <summary>
        /// Current battery status like Charging, Discharging, etc.
        /// </summary>
        public string BatteryStatus { get; set; }
        /// <summary>
        /// Available RAM memory (in bytes).
        /// </summary>
        public long AvailableMainMemory { get; set; }
        /// <summary>
        /// Total RAM memory (in bytes).
        /// </summary>
        public long TotalMainMemory { get; set; }
        /// <summary>
        /// If <c>true</c> indicates that the system is low in memory.
        /// </summary>
        public bool IsLowMainMemory { get; set; }
        /// <summary>
        /// Total size (in bytes) of the internal storage.
        /// </summary>
        public long TotalInternalStorage { get; set; }
        /// <summary>
        /// Free size (in bytes) in the internal storage.
        /// It might be different than available size.
        /// </summary>
        public long FreeInternalStorage { get; set; }
        /// <summary>
        /// Available size (in bytes) in the internal storage.
        /// It might be different than free size.
        /// </summary>
        public long AvailableInternalStorage { get; set; }
        /// <summary>
        /// If <c>true</c> indicates that the device has a removable storage.
        /// </summary>
        public bool HasRemovableExternalStorage { get; set; }
        /// <summary>
        /// If <c>true</c> indicates that the app can write in the removable storage.
        /// </summary>
        public bool CanWriteRemovableExternalStorage { get; set; }
        /// <summary>
        /// Total size (in bytes) of the removable external storage.
        /// </summary>
        public long TotalRemovableExternalStorage { get; set; }
        /// <summary>
        /// Available size (in bytes) of the removable external storage.
        /// </summary>
        public long AvailableRemovableExternalStorage { get; set; }
        /// <summary>
        /// Free size (in bytes) of the removable external storage.
        /// </summary>
        public long FreeRemovableExternalStorage { get; set; }
    }
}
