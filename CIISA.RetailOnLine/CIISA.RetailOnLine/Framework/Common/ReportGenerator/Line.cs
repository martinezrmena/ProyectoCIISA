using CIISA.RetailOnLine.Framework.Common.Render;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CIISA.RetailOnLine.Framework.Common.ReportGenerator
{
    public class Line
    {
        public static readonly string _noLinea = "No. Línea";

        public void printingLinesList(List<string> pprintingLinesList,string pprintingLines,int pspace)
        {
            foreach (string singleline in Regex.Split(pprintingLines, Environment.NewLine))
            {
                pprintingLinesList.Add(singleline + Environment.NewLine);
            }

            pspace--;

            if (pspace > 0)
            {
                for (int i = 0; i < pspace; i++)
                {
                    pprintingLinesList.Add(Environment.NewLine);
                }
            }
        }

        public void printLinesSpace(List<string> pprintingLinesList, int pquantity)
        {
            Line _line = new Line();

            for (int i = 0; i < pquantity; i++)
            {
                _line.printingLinesList(
                    pprintingLinesList,
                    Environment.NewLine,
                    0)
                    ;
            }
        }

        public void printLineSpace(List<string> pprintingLinesList, int pquantity) {

            for (int i = 0; i < pquantity; i++)
            {
                pprintingLinesList.Add(Environment.NewLine);
            }
        }

        public void printLineSpaceText(string line, int pquantity) {

            for (int i = 0; i < pquantity; i++)
            {
                line += Environment.NewLine;
            }
        }

        public void simpleHypenLine(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printingLinesList(
                pprintingLinesList,
                "-----------------------------------------------",
                1)
                ;
        }

        public void doubleHypenLine(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printingLinesList(
                pprintingLinesList,
                "================================================",
                1)
                ;
        }

        public void finalSpace(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printLineSpace(pprintingLinesList, 4);
        }
        
        public void dottedLine(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printingLinesList(
                pprintingLinesList,
                "................................................",
                2)
                ;
        }

        public void alignCenterMessage(List<string> pprintingLinesList, string text) {

            string _lineaPrint = string.Empty;

            Position _position = new Position();            

            foreach (string singleline in Regex.Split(text, Environment.NewLine))
            {
                _lineaPrint = string.Empty;

                _lineaPrint = _position.center(singleline.Length);

                _lineaPrint += singleline;                

                pprintingLinesList.Add(_lineaPrint + Environment.NewLine);
            }

        }

    }
}
