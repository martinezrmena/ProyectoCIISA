using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion.Carniceria
{
    public class SegmentoCopiaCliente
    {
        internal void SegmentoCopia(List<string> pprintingLinesList, int NCopia)
        {
            string _lineaUno = "COPIA No " + NCopia + " PARA EL CLIENTE";
            string _lineaPrint = string.Empty;

            Position _position = new Position();
            Line _line = new Line();

            _line.simpleHypenLine(pprintingLinesList);

            _lineaPrint = _position.center(_lineaUno.Length);
            _lineaPrint += _lineaUno;

            _line.printingLinesList(pprintingLinesList, _lineaPrint, 1);

            _line.simpleHypenLine(pprintingLinesList);

        }

    }
}
