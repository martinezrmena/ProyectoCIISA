using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Framework.Common.Feedback.FileSystem
{
    public interface IStorage
    {
        void hideDirectories();

        void hideFiles(string pfileToExclude);

        string executableFile();
    }
}
