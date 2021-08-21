using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.FileSystem;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Time;
using System.IO;
using System.Text;

namespace CIISA.RetailOnLine.Droid.Framework.Common.Feedback.FileSystem
{
    public class Storage_HH
    {
        internal DirectoryInfo createDirectoryHH_Log(string pdirectoryName)
        {
            string _path = string.Empty;

            //_path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            _path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).ToString();

            _path += FolderHH._folderLogHH;
            _path += Simbol._slash;
            _path += pdirectoryName;
            _path += Simbol._slash;

            Storage _storage = new Storage();

            DirectoryInfo _directory = _storage.CreateDirectory(_path.ToString());

            _path += VarTime.getLogDate();
            _path += Simbol._slash;

            _directory = new DirectoryInfo(_path.ToString());

            if (!_directory.Exists)
            {
                _directory = _storage.CreateDirectory(_path.ToString());
            }

            return _directory;
        }

        public DirectoryInfo createDirectoryHH_Data(string pdirectory, string ptypeTransaction)
        {
            StringBuilder _directoryPath = new StringBuilder();

            _directoryPath.Append(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).ToString());

            if (ptypeTransaction.Equals(TypeTransaction._select))
            {
                _directoryPath.Append(FolderHH._folderCompressSelect);
            }

            if (ptypeTransaction.Equals(TypeTransaction._download))
            {
                _directoryPath.Append(FolderHH._folderCompressDownload);
            }

            if (ptypeTransaction.Equals(TypeTransaction._upload))
            {
                _directoryPath.Append(FolderHH._folderCompressUpload);
            }

            _directoryPath.Append(pdirectory);

            Storage _storage = new Storage();

            return _storage.CreateDirectory(_directoryPath.ToString());
        }
    }
}