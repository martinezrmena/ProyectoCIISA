using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Common.FileSystem;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.Connection
{
    public class EstablishTable
    {
        public void createTable(
            string pScriptFileName,
            string ptable,
            Editor ptextBox,
            Log plog
            )
        {
            var _readFile = DependencyService.Get<IReadFile>();

            StringBuilder _sentence = _readFile.uploadScriptSQL_sb(
                                            pScriptFileName,
                                            FolderHH._folderScriptsSROL,
                                            ptextBox,
                                            plog
                                            );

            plog.setDetailVarValues("Nombre script SQL: " + pScriptFileName);

            if (!_sentence.Equals(string.Empty))
            {
                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                int _create = MultiGeneric.establishTable(_sentence);

                plog.addSuccessLine(ptextBox, "creó tabla " + ptable + Simbol._point, 1);
            }
            else
            {
                plog.addErrorLine(ptextBox, "CommandText vacío.", 0);
            }

            plog.setDetail(string.Empty);
            plog.setDetail(string.Empty);
            plog.setDetail(string.Empty);
        }

        public void createTable(
            string pScriptFileName,
            string ptable
            )
        {
            var _readFile = DependencyService.Get<IReadFile>();

            StringBuilder _sentence = _readFile.uploadScriptSQL_sb(
                                            pScriptFileName,
                                            FolderHH._folderScriptsSROL
                                            );

            if (!_sentence.Equals(string.Empty))
            {
                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                int _create = MultiGeneric.establishTable(_sentence);
            }
        }


        public void AlterTable(
            string pScriptFileName,
            string ptable
            )
        {
            var _readFile = DependencyService.Get<IReadFile>();

            StringBuilder _sentence = _readFile.uploadScriptSQL_sb(
                                            pScriptFileName,
                                            FolderHH._folderScriptsSROL
                                            );

            if (!_sentence.Equals(string.Empty))
            {
                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                int _create = MultiGeneric.establishTable(_sentence);
            }
        }
    }
}
