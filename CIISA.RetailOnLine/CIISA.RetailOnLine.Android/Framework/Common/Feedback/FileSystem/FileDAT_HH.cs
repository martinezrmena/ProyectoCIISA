using CIISA.RetailOnLine.Droid.Framework.Common.FileSystem;
using System.Data;
using System.IO;

namespace CIISA.RetailOnLine.Droid.Framework.Common.Feedback.FileSystem
{
    public class FileDAT_HH
    {
        public PathWriteCompress writeDAT_HH_MemoryStream(string pdirectoryName, string ptypeTransaction, string pmemoryStream)
        {
            Storage_HH _storage = new Storage_HH();

            DirectoryInfo _directory = _storage.createDirectoryHH_Data(pdirectoryName, ptypeTransaction);

            WriteFile _writeFile = new WriteFile();

            return _writeFile.writeDATFile_ms(_directory, pmemoryStream, false);
        }

        public PathWriteCompress writeDAT_HH_DataSet(string pdirectoryName, string ptypeTransaction, DataSet pds)
        {
            Storage_HH _storage = new Storage_HH();

            DirectoryInfo _directory = _storage.createDirectoryHH_Data(pdirectoryName, ptypeTransaction);

            WriteFile _writeFile = new WriteFile();

            return _writeFile.writeDATFile_ds(_directory, pds, false);
        }
    }
}