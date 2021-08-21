using CIISA.RetailOnLine.Framework.Common.Character;
using System;
using System.Text;

namespace CIISA.RetailOnLine.Framework.Common.Feedback.Detail
{
    internal class LogDetail
    {
        internal void addLine(ref StringBuilder pbodyLog,ref string pdetailLine,int pspace,ref DateTime pstartTime)
        {
            LogDetailUtils _logDetailsUtils = new LogDetailUtils();

            string _newLine = Simbol._squareBracketLeft
                    + _logDetailsUtils.setDurationTime(pstartTime)
                    + Simbol._squareBracketRight
                    + Space._two;

            pbodyLog.Append(_newLine);
            pbodyLog.Append(pdetailLine);

            LogDetailUtils _logDetailUtils = new LogDetailUtils();

            _logDetailUtils.setDetailJumpLine(ref pbodyLog, pspace);
        }

        internal void setDetailValueParameter(ref StringBuilder pbodyLog,ref string pdetail,ref DateTime pstartTime)
        {
            string _newLine = "      [Parámetro] " + pdetail;

            addLine(
                ref pbodyLog,
                ref _newLine,
                1,
                ref pstartTime
                );
        }

        internal void setDetailVarValues(ref StringBuilder pbodyLog,ref string pdetail,ref DateTime pstartTime)
        {
            string _newLine = "         [Variable] " + pdetail;

            addLine(
                ref pbodyLog,
                ref _newLine,
                1,
                ref pstartTime
                );
        }

        internal void setDetailClass(ref StringBuilder pbodyLog,ref string pdetail,ref DateTime pstartTime)
        {
            string _newLine = " ◄ Clase: " + pdetail;

            addLine(
                ref pbodyLog,
                ref _newLine,
                1,
                ref pstartTime
                );
        }

        internal void setDetailMethod(ref StringBuilder pbodyLog,ref string pdetail,ref DateTime pstartTime)
        {
            string _newLine = " ► Método: ";
            _newLine += pdetail;

            addLine(
                ref pbodyLog,
                ref _newLine,
                2,
                ref pstartTime
                );
        }

        internal void setDetailSentence(ref StringBuilder pbodyLog,ref string pdetail,ref DateTime pstartTime)
        {
            string _newLine = "     • Consulta: " + pdetail + Environment.NewLine;

            addLine(
                ref pbodyLog,
                ref _newLine,
                1,
                ref pstartTime
                );
        }
    }
}
