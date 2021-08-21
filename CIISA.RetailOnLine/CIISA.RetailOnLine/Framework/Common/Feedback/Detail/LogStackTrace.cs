using System;
using System.Text;

namespace CIISA.RetailOnLine.Framework.Common.Feedback.Detail
{
    internal class LogStackTrace
    {
        internal void setDetailStackTrace(ref StringBuilder pbodyLog,ref string pdetailLine,ref DateTime pstartTime)
        {
            string _newLine = "  ■ StackTrace: "
                                    + pdetailLine;

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
