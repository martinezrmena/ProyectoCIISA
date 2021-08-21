using System;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Framework.Common.Feedback.DownTextList
{
    internal class LogDownTextList
    {
        internal void addLineDownTextList(
            ref List<string> pdownTextList,
            ref string pdetail
            )
        {
            pdownTextList.Add(pdetail);
            pdownTextList.Add(Environment.NewLine);
            pdownTextList.Add(Environment.NewLine);
        }
    }
}
