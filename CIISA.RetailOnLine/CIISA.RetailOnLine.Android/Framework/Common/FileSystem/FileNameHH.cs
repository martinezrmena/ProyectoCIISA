using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.FileSystem;
using CIISA.RetailOnLine.Framework.Common.Time;
using System.Text;

namespace CIISA.RetailOnLine.Droid.Framework.Common.FileSystem
{
    public class FileNameHH
    {
        public string fileNameTXTExtension(string pfileName)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append(DateFileName());
            _sb.Append(pfileName);
            _sb.Append(FileExtension._txt);

            return _sb.ToString();
        }

        private string DateFileName()
        {
            StringBuilder _dateFileName = new StringBuilder();

            _dateFileName.Append(VarTime.getLogDate());
            _dateFileName.Append(Simbol._hyphenBajo);
            _dateFileName.Append(VarTime.getNow().Hour);
            _dateFileName.Append(Simbol._hyphen);
            _dateFileName.Append(VarTime.getNow().Minute);
            _dateFileName.Append(Simbol._hyphen);
            _dateFileName.Append(VarTime.getNow().Second);
            _dateFileName.Append(Simbol._hyphen);
            _dateFileName.Append(VarTime.getNow().Millisecond);
            _dateFileName.Append(Simbol._hyphenBajo);

            return _dateFileName.ToString();
        }

        public string FileNameDatExtension(string pfileName, bool pdateTime)
        {
            StringBuilder _sb = new StringBuilder();

            if (pdateTime)
            {
                _sb.Append(DateFileName());
            }

            _sb.Append(pfileName);
            _sb.Append(FileExtension._dat);

            return _sb.ToString();
        }
    }
}