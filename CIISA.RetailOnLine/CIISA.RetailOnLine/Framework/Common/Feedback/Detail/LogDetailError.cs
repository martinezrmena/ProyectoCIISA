using System;
using System.Text;

namespace CIISA.RetailOnLine.Framework.Common.Feedback.Detail
{
    internal class LogDetailError
    {
        internal void setDetailError(ref StringBuilder pbodyLog,ref string pdetailLine,ref DateTime pstartTime)
        {
            string _newLine = "  [X] Error: " + pdetailLine;

            LogDetail _logDetail = new LogDetail();

            _logDetail.addLine(
                ref pbodyLog,
                ref _newLine,
                2,
                ref pstartTime
                );
        }
    }
}
