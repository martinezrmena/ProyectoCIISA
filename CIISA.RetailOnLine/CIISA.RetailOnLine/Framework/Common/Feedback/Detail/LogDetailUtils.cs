using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Time;
using System;
using System.Text;

namespace CIISA.RetailOnLine.Framework.Common.Feedback.Detail
{
    internal class LogDetailUtils
    {
        internal string setDurationTime(DateTime pstartTime)
        {
            TimeSpan _timeSpan = new TimeSpan(VarTime.getNow().Ticks - pstartTime.Ticks);

            string _string = _timeSpan.Hours.ToString();
            _string += ":";
            _string += _timeSpan.Minutes;
            _string += ":";
            _string += _timeSpan.Seconds;
            _string += ":";
            _string += _timeSpan.Milliseconds;
            _string += Space._one;
            _string += VarTime._timeFormatWithMiliseconds;

            return _string;
        }

        internal void setDetailJumpLine(ref StringBuilder pbodyLog,int pspace)
        {
            if (pspace > 0)
            {
                for (int i = 0; i < pspace; i++)
                {
                    pbodyLog.Append(Environment.NewLine);
                }
            }

        }
    }
}
