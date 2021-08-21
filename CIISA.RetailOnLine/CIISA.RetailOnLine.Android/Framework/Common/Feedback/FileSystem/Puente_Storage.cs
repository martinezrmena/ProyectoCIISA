using CIISA.RetailOnLine.Droid.Framework.Common.Feedback.FileSystem;
using CIISA.RetailOnLine.Framework.Common.Feedback.FileSystem;
using Xamarin.Forms;

[assembly: Dependency(typeof(Puente_Storage))]
namespace CIISA.RetailOnLine.Droid.Framework.Common.Feedback.FileSystem
{
    public class Puente_Storage: IStorage
    {
        private Storage _storage = new Storage();

        public void hideDirectories()
        {
            _storage.hideDirectories();
        }

        public void hideFiles(string pfileToExclude)
        {
            _storage.hideFiles(pfileToExclude);
        }

        public string executableFile()
        {
            return _storage.executableFile();
        }
    }
}