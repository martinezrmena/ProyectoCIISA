using System.IO;

namespace CIISA.RetailOnLine.Droid.Framework.Common.FileSystem
{
    public static class DeleteFile
    {
        public static void Delete(string ppath)
        {
            if (File.Exists(ppath))
            {
                File.Delete(ppath);
            }
        }

    }
}