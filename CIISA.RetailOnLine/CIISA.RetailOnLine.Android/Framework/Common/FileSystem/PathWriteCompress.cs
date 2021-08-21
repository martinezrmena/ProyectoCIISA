using CIISA.RetailOnLine.Framework.Common.FileSystem;
using System.IO;

namespace CIISA.RetailOnLine.Droid.Framework.Common.FileSystem
{
    public class PathWriteCompress
    {
        private FileInfo _compress_PathWithDateTime = null;
        private FileInfo _compress_PathSimple = null;
        private FileInfo _unCompress_PathWithDateTime = null;
        private FileInfo _unCompress_PathSimple = null;
        private FileInfo _PathLastPath = null;

        public PathWriteCompress(DirectoryInfo pdirectory)
        {
            GeneratePathWrite(pdirectory);

            DeletePathWrite();
        }

        private void GeneratePathWrite(DirectoryInfo pdirectory)
        {
            FileNameHH _fileNameHH = new FileNameHH();

            _compress_PathSimple = new FileInfo(Path.Combine(pdirectory.FullName, _fileNameHH.FileNameDatExtension(TypeCompress._compress, false)));

            _compress_PathWithDateTime = new FileInfo(Path.Combine(pdirectory.FullName, _fileNameHH.FileNameDatExtension(TypeCompress._compress, true)));

            _unCompress_PathSimple = new FileInfo(Path.Combine(pdirectory.FullName, _fileNameHH.FileNameDatExtension(TypeCompress._unCompress, false)));

            _unCompress_PathWithDateTime = new FileInfo(Path.Combine(pdirectory.FullName, _fileNameHH.FileNameDatExtension(TypeCompress._unCompress, true)));
        }

        private void DeletePathWrite()
        {
            DeleteFile.Delete(_compress_PathSimple.FullName);

            DeleteFile.Delete(_compress_PathWithDateTime.FullName);

            DeleteFile.Delete(_unCompress_PathSimple.FullName);

            DeleteFile.Delete(_unCompress_PathWithDateTime.FullName);
        }

        public FileInfo Get_compress_PathWithDateTime()
        {
            _PathLastPath = _compress_PathWithDateTime;

            return _compress_PathWithDateTime;
        }

        public FileInfo Get_compress_PathSimple()
        {
            _PathLastPath = _compress_PathSimple;

            return _compress_PathSimple;
        }

        public FileInfo Get_unCompress_PathSimple()
        {
            _PathLastPath = _unCompress_PathSimple;

            return _unCompress_PathSimple;
        }

        public FileInfo Get_unCompress_PathWithDateTime()
        {
            _PathLastPath = _unCompress_PathWithDateTime;

            return _unCompress_PathWithDateTime;
        }

        public FileInfo Get_PathLastPath()
        {
            return _PathLastPath;
        }
    }
}