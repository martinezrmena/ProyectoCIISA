using System.Data;
using System.IO;
using System.Text;

namespace CIISA.RetailOnLine.Droid.Framework.Common.FileSystem
{
    public class WriteFile
    {
        public void writeTXTFile(DirectoryInfo pdirectory,string pfileName,string pheader,StringBuilder pbodyLog,StringBuilder preportLog,string pfoot)
        {
            FileNameHH _fileNameHH = new FileNameHH();

            StreamWriter _sw = new StreamWriter(pdirectory + _fileNameHH.fileNameTXTExtension(pfileName));

            _sw.WriteLine(pheader);
            _sw.WriteLine(pbodyLog.ToString());
            _sw.WriteLine(preportLog.ToString());
            _sw.WriteLine(pfoot);

            _sw.Close();
        }

        public PathWriteCompress writeDATFile_ms(DirectoryInfo pdirectory, string pmemoryStream, bool pwithDateTime)
        {
            PathWriteCompress _pathWriteCompress = new PathWriteCompress(pdirectory);

            StreamWriter _file = null;

            if (pwithDateTime)
            {
                _file = new StreamWriter(_pathWriteCompress.Get_compress_PathWithDateTime().FullName);
            }
            else
            {
                _file = new StreamWriter(_pathWriteCompress.Get_compress_PathSimple().FullName);
            }

            _file.WriteLine(pmemoryStream);

            _file.Close();

            return _pathWriteCompress;
        }

        public PathWriteCompress writeDATFile_ds(DirectoryInfo pdirectory, DataSet pds, bool pwithDateTime)
        {
            PathWriteCompress _pathWriteCompress = new PathWriteCompress(pdirectory);

            if (pwithDateTime)
            {
                pds.WriteXml(_pathWriteCompress.Get_unCompress_PathWithDateTime().FullName, XmlWriteMode.WriteSchema);
            }
            else
            {
                pds.WriteXml(_pathWriteCompress.Get_unCompress_PathSimple().FullName, XmlWriteMode.WriteSchema);
            }

            return _pathWriteCompress;
        }
    }
}