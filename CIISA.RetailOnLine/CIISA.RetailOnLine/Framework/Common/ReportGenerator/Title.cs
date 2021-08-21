using CIISA.RetailOnLine.Framework.Common.Render;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Framework.Common.ReportGenerator
{
    public class Title
    {
        public void titleGenericReport(List<string> pprintingLinesList,string preportName)
        {
            string _reportName = string.Empty;

            _reportName += "REPORTE ";
            _reportName += preportName;

            Line _line = new Line();

            Position _position = new Position();

            _line.printingLinesList(
                pprintingLinesList,
                _position.center(_reportName.Length) + _reportName, 1)
                ;
        }
    }
}
