using System.Collections.Generic;

namespace CIISA.RetailOnLine.Framework.Common.ReportGenerator
{
    public class Notes
    {
        public void noteSection(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printingLinesList(
                pprintingLinesList,
                "* Anotaciones:",
                2
                );

            _line.dottedLine(pprintingLinesList);
            _line.dottedLine(pprintingLinesList);
            _line.dottedLine(pprintingLinesList);
        }

    }
}
