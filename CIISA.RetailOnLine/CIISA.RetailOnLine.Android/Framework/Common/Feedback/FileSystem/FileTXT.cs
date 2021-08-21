using CIISA.RetailOnLine.Droid.Framework.Common.Feedback.FileSystem;
using CIISA.RetailOnLine.Droid.Framework.Common.FileSystem;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Feedback.FileSystem;
using CIISA.RetailOnLine.Framework.Common.Feedback.LogFiles;
using CIISA.RetailOnLine.Framework.Common.FileSystem;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using System;
using System.IO;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileTXT))]
namespace CIISA.RetailOnLine.Droid.Framework.Common.Feedback.FileSystem
{
    internal class FileTXT : IFileTXT
    {
        public bool isHandHeld(ref StringBuilder pbodyLog, ref DateTime pstartTime)
        {
            if (System.Environment.OSVersion.ToString().Contains("CE"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void writeTxt_Handheld(string pdirectoryName,string pfileName,ref DateTime pstartTime,ref StringBuilder pbodyLog,ref StringBuilder preportLog)
        {
            Storage_HH _storageHH = new Storage_HH();

            DirectoryInfo _directory = _storageHH.createDirectoryHH_Log(pdirectoryName);

            Storage _storage = new Storage();
            _storage.cleanLogFiles(pdirectoryName,FolderHH._folderLogHH);

            writeFileOnDisc(
                _directory,
                pfileName,
                ref pstartTime,
                ref pbodyLog,
                ref preportLog
                );
        }

        private void writeFileOnDisc(DirectoryInfo pdirectory,string pfileName,ref DateTime pstartTime,ref StringBuilder pbodyLog,ref StringBuilder preportLog)
        {
            WriteFile _writeFile = new WriteFile();

            LogEstruture _logEstructure = new LogEstruture();

            _writeFile.writeTXTFile(
                pdirectory,
                pfileName,
                _logEstructure.generalHeading(pstartTime),
                pbodyLog,
                preportLog,
                _logEstructure.generalFoot()
                );
        }

        public void writeTXT_Server(SystemCIISA psystemCIISA,string pdirectoryName,string pfileName,ref DateTime pstartTime,ref StringBuilder pbodyLog,ref StringBuilder preportLog)
        {
            DirectoryInfo _directory = null;

            Storage_WS _storage = new Storage_WS();

            _directory = _storage.createDirectoryWS_Log(psystemCIISA, pdirectoryName);

            writeFileOnDisc(
                _directory,
                pfileName,
                ref pstartTime,
                ref pbodyLog,
                ref preportLog
                );
        }
    }
}