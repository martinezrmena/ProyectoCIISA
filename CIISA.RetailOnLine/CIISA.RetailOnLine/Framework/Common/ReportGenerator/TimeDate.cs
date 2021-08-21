using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Time;
using System;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Framework.Common.ReportGenerator
{
    public class TimeDate
    {
        SplitString s = new SplitString();

        public void dateTimeDocument(List<string> pprintingLinesList)
        {
            string _string = string.Empty;

            _string += "Fecha Emisión: ";
            _string += VarTime.getDateCR();
            _string += ", Hora: ";
            _string += VarTime.getTimeCR();
            _string += Space._one;
            _string += VarTime.getMeridian();

            Line _line = new Line();

            _line.printingLinesList(
                pprintingLinesList,
                _string.ToString(),
                1
                );
        }

        public void dateTimeTransaction(List<string> pprintingLinesList,DateTime pcreationDateTime,bool prePrint)
        {
            if (prePrint)
            {
                string _string = string.Empty;

                _string += "Fecha Emisión: ";
                _string += VarTime.dateCR(pcreationDateTime);
                _string += ", Hora: ";
                _string += VarTime.timeSQLCE(pcreationDateTime);
                _string += Space._one;
                _string += VarTime.meridianSQLCE(pcreationDateTime);

                Line _line = new Line();

                _line.printingLinesList(
                    pprintingLinesList,
                    _string.ToString(),
                    1
                    );

                _string = string.Empty;

                _string += "Fecha Reimpresión: ";
                _string += VarTime.getDateCR();
                _string += ", Hora: ";
                _string += VarTime.getTimeCR();
                _string += Space._one;
                _string += VarTime.getMeridian();

                _line.printingLinesList(
                    pprintingLinesList,
                    _string.ToString(),
                    1
                    );
            }
            else
            {
                dateTimeDocument(
                    pprintingLinesList
                    );
            }
        }
    }
}
