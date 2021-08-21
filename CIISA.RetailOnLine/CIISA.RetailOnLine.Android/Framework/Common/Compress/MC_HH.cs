using CIISA.RetailOnLine.Droid.Framework.Common.Compress;
using CIISA.RetailOnLine.Droid.Framework.Common.Feedback.FileSystem;
using CIISA.RetailOnLine.Droid.Framework.Common.FileSystem;
using CIISA.RetailOnLine.Framework.Common.Compress;
using System.Data;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(MC_HH))]
namespace CIISA.RetailOnLine.Droid.Framework.Common.Compress
{
    public class MC_HH : IMC_HH
    {
        public DataSet Unzip_HH_DataSet(string pdirectoryName, string ptypeTransaction, string pmemoryStream)
        {
            FileDAT_HH _fileDat = new FileDAT_HH();

            PathWriteCompress _pathWriteCompress = _fileDat.writeDAT_HH_MemoryStream(pdirectoryName, ptypeTransaction, pmemoryStream);

            MemoryCompress _memoryCompress = new MemoryCompress();

            return _memoryCompress.UnzipFromMemoryStream_DataSet(pmemoryStream, _pathWriteCompress.Get_unCompress_PathSimple().FullName);
        }

        public DataTable Unzip_HH_DataTable(string pdirectoryName, string ptypeTransaction, string pmemoryStream, string ptableName)
        {
            DataSet _ds = Unzip_HH_DataSet(pdirectoryName, ptypeTransaction, pmemoryStream);

            DataTable _dt = new DataTable();

            foreach (DataTable _dtTemp in _ds.Tables)
            {
                if (_dtTemp.TableName.Equals(ptableName))
                {
                    _dt = _dtTemp;
                }
            }

            return _dt;
        }

        public string Zip_HH_DataSet(string pdirectoryName, string ptypeTransaction, DataSet pds)
        {
            FileDAT_HH _fileDat = new FileDAT_HH();

            PathWriteCompress _pathWriteCompress = _fileDat.writeDAT_HH_DataSet(pdirectoryName, ptypeTransaction, pds);

            return Zip_HH(pdirectoryName, ptypeTransaction, _pathWriteCompress.Get_PathLastPath());
        }

        private string Zip_HH(string pdirectoryName, string ptypeTransaction, FileInfo pfileInfo)
        {
            MemoryCompress _memoryCompress = new MemoryCompress();

            string _memoryStream = _memoryCompress.ZipToMemoryStream(pfileInfo.FullName);

            FileDAT_HH _fileDat = new FileDAT_HH();

            PathWriteCompress _pathWriteCompress = _fileDat.writeDAT_HH_MemoryStream(pdirectoryName, ptypeTransaction, _memoryStream);

            return _memoryStream;
        }

        //public string Zip_HH_DataTable(string pdirectoryName, string ptypeTransaction, DataTable pdt)
        //{
        //    FileDAT_HH _fileDat = new FileDAT_HH();

        //    PathWriteCompress _pathWriteCompress = _fileDat.writeDAT_HH_DataTable(pdirectoryName, ptypeTransaction, pdt);

        //    return Zip_HH(pdirectoryName, ptypeTransaction, _pathWriteCompress.Get_PathLastPath());
        //}
    }
}