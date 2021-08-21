using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Framework.Handheld.Print
{
    public class Print
    {

        public async Task print(List<string> pprintingLinesList,Printer pprint)
        {
            string impresion = string.Empty;

            foreach (string _line in pprintingLinesList)
            {
                impresion += _line;
                await pprint.print(_line);
            }

            //LogMessageAttention _logMessageAttention = new LogMessageAttention();
            //await _logMessageAttention.generalAttention(impresion);
        }

        public async Task printLine(string pprintingLine, Printer pprint)
        {
            await pprint.print(pprintingLine);
        }

        public async Task printBarCode(string code, Printer pprint)
        {
            await pprint.printBarCode(code);
        }

    }
}
