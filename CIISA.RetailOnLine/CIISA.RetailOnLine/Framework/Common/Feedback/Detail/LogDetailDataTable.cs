using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Framework.Common.Feedback.Detail
{
    internal class LogDetailDataTable
    {
        private string v_dataTable = "     <> DataTable ";

        internal void setDetailDataTableFilled(ref string pnameDataTable,ref StringBuilder pbodyLog,ref DateTime pstartTime)
        {
            string _newLine = v_dataTable + pnameDataTable + " lleno.";

            LogDetail _logDetail = new LogDetail();

            _logDetail.addLine(
                ref pbodyLog,
                ref _newLine,
                2,
                ref pstartTime
                );
        }

        internal void setDetailDataTableEmpty(ref string pnameDataTable,ref StringBuilder pbodyLog,ref DateTime pstartTime)
        {
            string _newLine = v_dataTable + pnameDataTable + " vacío.";

            LogDetail _logDetail = new LogDetail();

            _logDetail.addLine(
                ref pbodyLog,
                ref _newLine,
                1,
                ref pstartTime
                );
        }

        internal void setDetailDataTableNull(ref StringBuilder pbodyLog,ref DateTime pstartTime)
        {
            string _newLine = v_dataTable + "nulo.";

            LogDetail _logDetail = new LogDetail();

            _logDetail.addLine(
                ref pbodyLog,
                ref _newLine,
                1,
                ref pstartTime
                );
        }
    }
}
