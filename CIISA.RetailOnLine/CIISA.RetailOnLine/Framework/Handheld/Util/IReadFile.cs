using CIISA.RetailOnLine.Framework.Common.Feedback;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.Util
{
    public interface IReadFile
    {
        StringBuilder uploadScriptSQL_sb(string pScriptFileName, string pScriptFolder, Editor ptextBox, Log plog);

        StringBuilder uploadScriptSQL_sb(string pScriptFileName, string pScriptFolder);
    }
}
