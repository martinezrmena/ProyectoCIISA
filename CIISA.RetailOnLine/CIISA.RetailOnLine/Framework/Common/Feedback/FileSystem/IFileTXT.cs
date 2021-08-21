using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using System;
using System.Text;

namespace CIISA.RetailOnLine.Framework.Common.Feedback.FileSystem
{
    public interface IFileTXT
    {
        bool isHandHeld(ref StringBuilder pbodyLog, ref DateTime pstartTime);

        void writeTxt_Handheld(string pdirectoryName,string pfileName,ref DateTime pstartTime,ref StringBuilder pbodyLog,ref StringBuilder preportLog);

        void writeTXT_Server(SystemCIISA psystemCIISA, string pdirectoryName, string pfileName, ref DateTime pstartTime, ref StringBuilder pbodyLog, ref StringBuilder preportLog);
    }
}
