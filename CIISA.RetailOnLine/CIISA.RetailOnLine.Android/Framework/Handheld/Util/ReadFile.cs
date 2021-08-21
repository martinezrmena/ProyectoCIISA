using CIISA.RetailOnLine.Droid.Framework.Handheld.DataBase;
using CIISA.RetailOnLine.Droid.Framework.Handheld.Util;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using System.IO;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReadFile))]
namespace CIISA.RetailOnLine.Droid.Framework.Handheld.Util
{
    public class ReadFile:IReadFile
    {
        private string uploadScriptSQL(string pScriptFileName, string pScriptFolder, Editor ptextBox, Log plog)
        {
            //ConnectionStringDB _objStringConnection = new ConnectionStringDB();
            //string _pathSystemDirectory = _objStringConnection.getDirectoryPath();

            string _scriptSQL = string.Empty;

            using (StreamReader sr = new StreamReader(Android.App.Application.Context.Assets.Open(pScriptFileName)))
            {
                _scriptSQL = sr.ReadToEnd();
            }

            //bool _fileExist = false;

            //_fileExist = File.Exists(
            //    _pathSystemDirectory
            //    + pScriptFolder
            //    + pScriptFileName
            //    );

            //if (_fileExist)
            //{
            //    FileInfo file = new FileInfo(
            //        _pathSystemDirectory
            //        + pScriptFolder
            //        + pScriptFileName
            //        );

            //    _scriptSQL = file.OpenText().ReadToEnd();
            //}
            //else
            //{
            //    plog.addErrorLine(ptextBox, "no encontró el archivo de script sql: " + pScriptFileName + Simbol._point, 1);
            //}

            return _scriptSQL;
        }

        private string uploadScriptSQL(string pScriptFileName, string pScriptFolder)
        {
            string _scriptSQL = string.Empty;

            using (StreamReader sr = new StreamReader(Android.App.Application.Context.Assets.Open(pScriptFileName)))
            {
                _scriptSQL = sr.ReadToEnd();
            }

            return _scriptSQL;
        }

        public StringBuilder uploadScriptSQL_sb(string pScriptFileName, string pScriptFolder, Editor ptextBox, Log plog)
        {
            string _scriptSQL = uploadScriptSQL(pScriptFileName, pScriptFolder, ptextBox, plog);

            StringBuilder _sb = new StringBuilder();

            _sb.Append(_scriptSQL);

            return _sb;
        }

        public StringBuilder uploadScriptSQL_sb(string pScriptFileName, string pScriptFolder)
        {
            string _scriptSQL = uploadScriptSQL(pScriptFileName, pScriptFolder);

            StringBuilder _sb = new StringBuilder();

            _sb.Append(_scriptSQL);

            return _sb;
        }
    }
}