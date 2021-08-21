using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using CIISA.RetailOnLine.Framework.Common.Time;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones
{
    internal class SegmentoPlazo
    {
        internal void plazo(Cliente pobjCliente,List<string> plistaLineasImpresion)
        {
            Line _line = new Line();

            _line.printingLinesList(plistaLineasImpresion,
                "Plazo: "
                + pobjCliente.v_plazo
                + ", Vence: "
                + VarTime.getDateExpiresCR(
                        pobjCliente.v_plazo),
                        1
                        );
        }        
    }
}
