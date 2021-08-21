using CIISA.RetailOnLine.Framework.Common.Character;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Framework.Common.ReportGenerator
{
    public class Copy
    {
        public void copies(List<string> pprintingLinesList, int pcopy, int pcopyNumber)
        {
            Line _line = new Line();

            _line.printingLinesList(
                pprintingLinesList,
                "Copia "
                    + pcopy
                    + Simbol._slash
                    + pcopyNumber
                    + Simbol._point,
                1);
        }

        public string copies(string pprintingLines, int pcopy, int pcopyNumber)
        {
            pprintingLines +=
                "Copia "
                + pcopy
                + Simbol._slash
                + pcopyNumber
                + " Cliente.";

            return pprintingLines;
        }
    }
}
