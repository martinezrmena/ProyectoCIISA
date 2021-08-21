using CIISA.RetailOnLine.Framework.Common.Feedback.Detail;
using CIISA.RetailOnLine.Framework.Common.Feedback.FileSystem;
using CIISA.RetailOnLine.Framework.Common.Feedback.LogFiles;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Common.Time;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Common.Feedback
{
    public class Log
    {
        private StringBuilder v_bodyLog = null;
        private List<string> v_downTextList = null;
        private DateTime v_startTime = VarTime.getNow();
        private StringBuilder v_reportBody = null;
        private string[] v_insertedRowsList = new string[0];
        private string[] v_insertedRowsListWithDetail = new string[0];

        public Log()
        {
            v_bodyLog = new StringBuilder();
            v_downTextList = new List<string>();

            v_startTime = VarTime.getNow();

            v_reportBody = new StringBuilder();

            v_insertedRowsList = new string[0];
            v_insertedRowsListWithDetail = new string[0];
        }

        public void setDetailError(string pdetail)
        {
            LogDetailError _logDetailError = new LogDetailError();

            _logDetailError.setDetailError(
                ref v_bodyLog,
                ref pdetail,
                ref v_startTime
                );
        }

        public void setDetailException(string pdetail)
        {
            LogException _logException = new LogException();

            _logException.setDetailException(ref v_bodyLog,ref pdetail);
        }

        public void setDetailStackTrace(string pdetail)
        {
            LogStackTrace _logStackTrace = new LogStackTrace();

            _logStackTrace.setDetailStackTrace(
                ref v_bodyLog,
                ref pdetail,
                ref v_startTime
                );
        }

        public void generateFileTXTException(SystemCIISA psystemCIISA,string pdirectoryName,string pfileName)
        {
            var _fileTXT = DependencyService.Get<IFileTXT>();
            
            //if (_fileTXT.isHandHeld(ref v_bodyLog, ref v_startTime))
            if(Device.RuntimePlatform.Equals(Device.Android))
            {
                _fileTXT.writeTxt_Handheld(
                    pdirectoryName,
                    pfileName,
                    ref v_startTime,
                    ref v_bodyLog,
                    ref v_reportBody
                    );
            }
            else
            {
                _fileTXT.writeTXT_Server(
                    psystemCIISA,
                    pdirectoryName,
                    pfileName,
                    ref v_startTime,
                    ref v_bodyLog,
                    ref v_reportBody
                    );
            }

        }

        public void addSuccessLine(Editor ptextBox,string pdetail,int pspace)
        {
            LogTextBox _logTextBox = new LogTextBox();

            _logTextBox.addSuccessLine(
                ref ptextBox,
                ref pdetail,
                ref pspace,
                ref v_downTextList,
                ref v_bodyLog,
                ref v_startTime
                );
        }

        public void setDetailValuesParameter(string pdetail)
        {
            LogDetail _logDetail = new LogDetail();

            _logDetail.setDetailValueParameter(
                ref v_bodyLog,
                ref pdetail,
                ref v_startTime
                );
        }

        public void setDetail(string pdetail)
        {
            LogDetail _logDetail = new LogDetail();

            _logDetail.addLine(
                ref v_bodyLog,
                ref pdetail,
                1,
                ref v_startTime
                );
        }

        public void setDetailVarValues(string pdetail)
        {
            LogDetail _logDetail = new LogDetail();

            _logDetail.setDetailVarValues(
                ref v_bodyLog,
                ref pdetail,
                ref v_startTime
                );
        }

        public void generateFileTXTHH(SystemCIISA psystemCIISA,string pdirectoryName,string pfileName)
        {
            var _fileTXT = DependencyService.Get<IFileTXT>();

            _fileTXT.writeTxt_Handheld(
                pdirectoryName,
                pfileName,
                ref v_startTime,
                ref v_bodyLog,
                ref v_reportBody
                );
        }

        public void addLine(Editor ptextBox,string pdetail)
        {
            LogTextBox _logTextBox = new LogTextBox();

            _logTextBox.addLine(
                ref ptextBox,
                ref pdetail,
                ref v_downTextList,
                ref v_bodyLog,
                ref v_startTime
                );
        }

        public void setDetailClass(string pdetail)
        {
            LogDetail _logDetail = new LogDetail();

            _logDetail.setDetailClass(
                ref v_bodyLog,
                ref pdetail,
                ref v_startTime
                );
        }

        public void setDetailMethod(string pdetail)
        {
            LogDetail _logDetail = new LogDetail();

            _logDetail.setDetailMethod(
                ref v_bodyLog,
                ref pdetail,
                ref v_startTime
                );
        }

        public void addLineSuccessWSDownload(Editor ptextBox,string ptable)
        {
            LogTextBox _logTextBox = new LogTextBox();

            _logTextBox.addLineSuccessWSDownload(
                ref ptextBox,
                ref ptable,
                ref v_downTextList,
                ref v_bodyLog,
                ref v_startTime
                );
        }

        public void setDetailDataTableFilled(string pnameDataTable)
        {
            LogDetailDataTable _logDetailDataTable = new LogDetailDataTable();

            _logDetailDataTable.setDetailDataTableFilled(
                ref pnameDataTable,
                ref v_bodyLog,
                ref v_startTime
                );
        }

        public void setDetailDataTableEmpty(string pnameDataTable)
        {
            LogDetailDataTable _logDetailDataTable = new LogDetailDataTable();

            _logDetailDataTable.setDetailDataTableEmpty(
                ref pnameDataTable,
                ref v_bodyLog,
                ref v_startTime
                );
        }

        public void addErrorLineDataTableNull(Editor ptextBox,string ptable)
        {
            LogTextBox _logTextBox = new LogTextBox();

            _logTextBox.addErrorLineDataTableNull(
                ref ptextBox,
                ref ptable,
                ref v_downTextList,
                ref v_bodyLog,
                ref v_startTime
                );
        }

        public void addAlertLineDataTable(Editor ptextBox,string ptable)
        {
            LogTextBox _logTextBox = new LogTextBox();

            _logTextBox.addAlertLineDataTable(
                ref ptextBox,
                ref ptable,
                ref v_downTextList,
                ref v_bodyLog,
                ref v_startTime
                );
        }

        public void addAlertLine(Editor ptextBox,string pdetail)
        {
            LogTextBox _logTextBox = new LogTextBox();

            _logTextBox.addAlertLine(
                ref ptextBox,
                ref pdetail,
                ref v_downTextList,
                ref v_bodyLog,
                ref v_startTime
                );
        }

        public void registrationNumberInserts(Editor ptextBox,int pinserts,int precordAmount,string ptable)
        {
            LogTextBox _logTextBox = new LogTextBox();

            _logTextBox.registrationNumberInserts(
                ref ptextBox,
                ref pinserts,
                ref precordAmount,
                ref ptable,
                ref v_downTextList,
                ref v_bodyLog,
                ref v_startTime
                );
        }

        public void setDetailDataTableNull()
        {
            LogDetailDataTable _logDetailDataTable = new LogDetailDataTable();

            _logDetailDataTable.setDetailDataTableNull(
                ref v_bodyLog,
                ref v_startTime
                );
        }

        public void addErrorLine(Editor ptextBox,string pdetail,int pspace)
        {
            LogTextBox _logTextBox = new LogTextBox();

            _logTextBox.addErrorLine(
                ref ptextBox,
                ref pdetail,
                ref pspace,
                ref v_downTextList,
                ref v_bodyLog,
                ref v_startTime
                );
        }

        public void setDetailSentence(string pdetail)
        {
            LogDetail _logDetail = new LogDetail();

            _logDetail.setDetailSentence(
                ref v_bodyLog,
                ref pdetail,
                ref v_startTime
                );
        }

        public void generateFileTXTHHPrintedDocuments(string pdirectoryName,string pfileName)
        {
            //FileTXT _fileTXT = new FileTXT();
            var _fileTXT = DependencyService.Get<IFileTXT>();

            _fileTXT.writeTxt_Handheld(
                pdirectoryName,
                pfileName,
                ref v_startTime,
                ref v_bodyLog,
                ref v_reportBody
                );
        }
    }
}
