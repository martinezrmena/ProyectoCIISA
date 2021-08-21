using CIISA.RetailOnLine.Droid.Framework.Handheld.DataBase;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Common.FileSystem;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using System.IO;

namespace CIISA.RetailOnLine.Droid.Framework.Common.Feedback.FileSystem
{
    public class Storage
    {
        public DirectoryInfo CreateDirectory(string pdirectoryPath)
        {
            DirectoryInfo _directory = new DirectoryInfo(pdirectoryPath);

            if (!_directory.Exists)
            {
                _directory.Create();
            }

            _directory.Attributes = FileAttributes.Directory;
            _directory.Attributes = FileAttributes.Hidden;

            return _directory;
        }

        internal void cleanLogFiles(string pdirectory, string ppathSystemDirectory)
        {
            try
            {
                string _path = string.Empty;
                _path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).ToString();
                _path += ppathSystemDirectory;
                _path += Simbol._slash;

                DirectoryInfo dir = new DirectoryInfo(_path);

#pragma warning disable CS0219 // La variable 'respuesta' está asignada pero su valor nunca se usa
                bool respuesta = false;
#pragma warning restore CS0219 // La variable 'respuesta' está asignada pero su valor nunca se usa

                if (!dir.Exists)
                {
                    dir.Create();
                }

                string[] filePaths = Directory.GetFiles(
                                            //ppathSystemDirectory.ToString(),
                                            dir.ToString(),
                                            FileExtension._asteriskTxt
                                            );

                if (filePaths.Length > VarLog.v_quantityLogFiles)
                {
                    foreach (string _file in filePaths)
                    {
                        File.Delete(_file);
                    }
                }
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
            
        }

        public void hideDirectories()
        {
            try
            {
                DirectoryInfo _dirInfo = new DirectoryInfo(FolderHH._folderLogHH);

                bool existe = _dirInfo.Exists;

                if (existe)
                {
                    DirectoryInfo[] _allDirectories = _dirInfo.GetDirectories();

                    foreach (DirectoryInfo _directory in _allDirectories)
                    {
                        _directory.Attributes = FileAttributes.Hidden;
                    }
                }                
            }
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            catch (System.Exception ex)
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            {

                throw;
            }
            
        }

        public void hideFiles(string pfileToExclude)
        {
            ConnectionStringDB _objStringConnection = new ConnectionStringDB();

            string _pathSystemDirectory = _objStringConnection.getDirectoryPath();

            DirectoryInfo _dirInfo = new DirectoryInfo(_pathSystemDirectory);

            FileInfo[] _allFiles = _dirInfo.GetFiles(FileExtension._asteriskPointAsterisk);

            foreach (FileInfo _file in _allFiles)
            {
                if (!_file.Name.Equals(pfileToExclude))
                {
                    if (!_file.Extension.Equals(FileExtension._asteriskExe))
                    {
                        _file.Attributes = FileAttributes.Hidden;
                    }
                }
            }
        }

        public string executableFile()
        {
            string _executableFile = string.Empty;

            SystemCIISA sc = Sistema.establecerObjetoSystemCIISA();

            if (Sistema.establecerObjetoSystemCIISA()._initials.Equals(ROLSystem.getInitials()))
            {
                _executableFile = "CIISA.RetailOnLine.Movil.MainApp.exe";
            }

            if (Sistema.establecerObjetoSystemCIISA()._initials.Equals(COLSystem.getInitials()))
            {
                _executableFile = "CIISA.ChargingOnLine.Movil.MainApp.exe";
            }

            if (Sistema.establecerObjetoSystemCIISA()._initials.Equals(SCSystem.getInitials()))
            {
                _executableFile = "CIISA.Movil.Common.SmartClient.exe";
            }

            return _executableFile;
        }
    }
}