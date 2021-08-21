using CIISA.RetailOnLine.Framework.Common.Feedback;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Framework.Handheld.Print
{
    public class Documents
    {
        public void printedDocumentsLog(List<string> pprintingLinesList,string pdirectory,string pdocumentName)
        {
            Log _log = new Log();

            _log.setDetail("Número líneas: " + pprintingLinesList.Count);

            foreach (string _line in pprintingLinesList)
            {
                _log.setDetail(_line);
            }

            _log.generateFileTXTHHPrintedDocuments(
                pdirectory,
                pdocumentName
                );
        }
    }
}
