using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Server.FileSystem;
using System.IO;
using System.Linq;
using System.Text;

namespace CIISA.RetailOnLine.Droid.Framework.Common.Feedback.FileSystem
{
    public class Storage_WS
    {
        internal DirectoryInfo createDirectoryWS_Log(SystemCIISA psystemCIISA,string pdirectoryName)
        {
            StringBuilder _directoryPath = new StringBuilder();

            _directoryPath.Append(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).ToString());
            _directoryPath.Append(FolderServer._pathDirectoryLogServer);
            _directoryPath.Append(psystemCIISA._initials);
            _directoryPath.Append(Simbol._slash);
            _directoryPath.Append(pdirectoryName);
            _directoryPath.Append(Simbol._slash);

            Storage _storage = new Storage();

            return _storage.CreateDirectory(_directoryPath.ToString());
        }
    }
}