namespace CIISA.RetailOnLine.Droid.Framework.Handheld.DataBase
{
    public class ConnectionStringDB
    {
        private string _directoryPath = string.Empty;
        private string _pahtDB = string.Empty;
        private string _stringConnection = string.Empty;

        public ConnectionStringDB()
        {
            stringConnection();
        }

        private void stringConnection()
        {
            _directoryPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).ToString();
            _pahtDB = System.IO.Path.Combine(_directoryPath, DBCF._dataBaseName);
            _stringConnection = "Data Source=" + _pahtDB + ";Max Database Size=" + DBCF._maximunSizeDB;
        }

        public string getDataBasePath()
        {
            return _pahtDB;
        }

        public string getStringConnection()
        {
            return _stringConnection;
        }

        public string getDataBase()
        {
            return DBCF._dataBaseName;
        }

        public string getDirectoryPath()
        {
            return _directoryPath;
        }
    }
}