using System.Text;

namespace CIISA.RetailOnLine.Framework.Common.Feedback.Detail
{
    internal class LogException
    {
        private string v_exceptiontText = "  ■ Excepción: ";

        internal void setDetailException(ref StringBuilder pbodyLog,ref string pdetailLine)
        {
            pbodyLog.Append(v_exceptiontText);
            pbodyLog.Append(pdetailLine);
        }
    }
}
