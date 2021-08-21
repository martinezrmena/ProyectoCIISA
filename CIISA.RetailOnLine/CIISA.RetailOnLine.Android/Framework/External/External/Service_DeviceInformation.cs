using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using CIISA.RetailOnLine.Droid.Framework.External.External;
using CIISA.RetailOnLine.Framework.External.DeviceInformation;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;


[assembly: Dependency(typeof(Service_DeviceInformation))]
namespace CIISA.RetailOnLine.Droid.Framework.External.External
{
    public class Service_DeviceInformation : IService_DeviceInformation
    {
        public async Task<DeviceInformation> GetDeviceInformation()
        {
            DeviceInformation devInfo = new DeviceInformation();

            //* Gets the main memory (RAM) information.
            var activityManager = (ActivityManager)Android.App.Application.Context.GetSystemService(Context.ActivityService);

            ActivityManager.MemoryInfo memInfo = new ActivityManager.MemoryInfo();
            activityManager.GetMemoryInfo(memInfo);

            System.Diagnostics.Debug.WriteLine("GetDeviceInfo - Avail {0} - {1} MB", memInfo.AvailMem, memInfo.AvailMem / 1024 / 1024);
            System.Diagnostics.Debug.WriteLine("GetDeviceInfo - Low {0}", memInfo.LowMemory);
            System.Diagnostics.Debug.WriteLine("GetDeviceInfo - Total {0} - {1} MB", memInfo.TotalMem, memInfo.TotalMem / 1024 / 1024);

            devInfo.AvailableMainMemory = memInfo.AvailMem;
            devInfo.IsLowMainMemory = memInfo.LowMemory;
            devInfo.TotalMainMemory = memInfo.TotalMem;

            //* Gets the internal storage information.
            StorageInfo internalStorageInfo = this.GetStorageInformation(Android.OS.Environment.GetExternalStoragePublicDirectory("").ToString());

            devInfo.TotalInternalStorage = internalStorageInfo.TotalSpace;
            devInfo.AvailableInternalStorage = internalStorageInfo.AvailableSpace;
            devInfo.FreeInternalStorage = internalStorageInfo.FreeSpace;

            string extStorage = await this.RemovableStoragePath();

            devInfo.HasRemovableExternalStorage = !String.IsNullOrEmpty(extStorage);

            if (devInfo.HasRemovableExternalStorage)
            {
                bool canWrite = await this.IsWriteable(extStorage);
                devInfo.CanWriteRemovableExternalStorage = canWrite;

                //* Gets the external removable storage information.
                StorageInfo removableStorageInfo = this.GetStorageInformation(Android.OS.Environment.GetExternalStoragePublicDirectory("").ToString());
                devInfo.TotalRemovableExternalStorage = removableStorageInfo.TotalSpace;
                devInfo.FreeRemovableExternalStorage = removableStorageInfo.FreeSpace;
                devInfo.AvailableRemovableExternalStorage = removableStorageInfo.AvailableSpace;

            }
            else
            {
                devInfo.CanWriteRemovableExternalStorage = false;
                devInfo.TotalRemovableExternalStorage = 0;
                devInfo.FreeRemovableExternalStorage = 0;
                devInfo.AvailableRemovableExternalStorage = 0;
            }
            return devInfo;
        }

        protected StorageInfo GetStorageInformation(string path)
        {
            StorageInfo storageInfo = new StorageInfo();

            StatFs stat = new StatFs(path); //"/storage/sdcard1"
            long totalSpaceBytes = 0;
            long freeSpaceBytes = 0;
            long availableSpaceBytes = 0;

            /*
              We have to do the check for the Android version, because the OS calls being made have been deprecated for older versions. 
              The ‘old style’, pre Android level 18 didn’t use the Long suffixes, so if you try and call use those on 
              anything below Android 4.3, it’ll crash on you, telling you that that those methods are unavailable. 
              http://blog.wislon.io/posts/2014/09/28/xamarin-and-android-how-to-use-your-external-removable-sd-card/
             */
            if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr2)
            {
                long blockSize = stat.BlockSizeLong;
                totalSpaceBytes = stat.BlockCountLong * stat.BlockSizeLong;
                availableSpaceBytes = stat.AvailableBlocksLong * stat.BlockSizeLong;
                freeSpaceBytes = stat.FreeBlocksLong * stat.BlockSizeLong;
            }
            else
            {
                //Android establece que se debe apuntar a trabajar arriba del api19, según estadares
                //totalSpaceBytes = (long)stat.BlockCount * (long)stat.BlockSize;
                //availableSpaceBytes = (long)stat.AvailableBlocks * (long)stat.BlockSize;
                //freeSpaceBytes = (long)stat.FreeBlocks * (long)stat.BlockSize;
            }

            storageInfo.TotalSpace = totalSpaceBytes;
            storageInfo.AvailableSpace = availableSpaceBytes;
            storageInfo.FreeSpace = freeSpaceBytes;
            return storageInfo;

        }

        private Task<string> RemovableStoragePath()
        {
            return Task.Run(() => {
                //* Tries to detect if there is a removable storage.
                //* http://blog.wislon.io/posts/2014/09/28/xamarin-and-android-how-to-use-your-external-removable-sd-card/
                string procMounts = System.IO.File.ReadAllText("/proc/mounts");
                System.Diagnostics.Debug.WriteLine("begin /proc/mounts");
                System.Diagnostics.Debug.WriteLine(procMounts);
                System.Diagnostics.Debug.WriteLine("end /proc/mounts");
                var candidateProcMountEntries = procMounts.Split('\n', '\r').ToList();
                candidateProcMountEntries.RemoveAll(s => s.IndexOf("storage", StringComparison.OrdinalIgnoreCase) < 0);
                var bestCandidate = candidateProcMountEntries
                    .FirstOrDefault(s => s.IndexOf("ext", StringComparison.OrdinalIgnoreCase) >= 0
                        && s.IndexOf("sd", StringComparison.OrdinalIgnoreCase) >= 0
                        && s.IndexOf("vfat", StringComparison.OrdinalIgnoreCase) >= 0);

                // e.g. /dev/block/vold/179:9 /storage/extSdCard vfat rw,dirsync,nosuid, blah
                if (!string.IsNullOrWhiteSpace(bestCandidate))
                {
                    var sdCardEntries = bestCandidate.Split(' ');
                    var sdCardEntry = sdCardEntries.FirstOrDefault(s => s.IndexOf("/storage/", System.StringComparison.OrdinalIgnoreCase) >= 0);
                    System.Diagnostics.Debug.WriteLine("It has removable storage {0}", !string.IsNullOrWhiteSpace(sdCardEntry) ? string.Format("{0}", sdCardEntry) : string.Empty);
                    return !string.IsNullOrWhiteSpace(sdCardEntry) ? string.Format("{0}", sdCardEntry) : string.Empty;
                }
                return string.Empty;
            });
        }

        private Task<bool> IsWriteable(string path)
        {

            return Task.Run(() => {
                bool result = false;
                try
                {

                    const string someTestText = "some test text";
                    string testFile = string.Format("{0}/{1}.txt", path, Guid.NewGuid());
                    System.IO.File.WriteAllText(testFile, someTestText);
                    System.IO.File.Delete(testFile);
                    result = true;
                }
                catch (Exception ex)
                { // it's not writeable
                    System.Diagnostics.Debug.WriteLine("ExternalSDStorageHelper", string.Format("Exception: {0}\r\nMessage: {1}\r\nStack Trace: {2}", ex, ex.Message, ex.StackTrace));
                }

                return result;
            });
        }

        public StorageInfo GetInternalStorageInformation() {
            StorageInfo Internal = GetStorageInformation(Android.OS.Environment.GetExternalStoragePublicDirectory("").ToString());
            return Internal;
        }

    }
}