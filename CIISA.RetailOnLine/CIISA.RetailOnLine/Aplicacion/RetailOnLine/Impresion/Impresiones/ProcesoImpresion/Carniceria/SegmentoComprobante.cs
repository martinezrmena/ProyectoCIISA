using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion.Carniceria
{
    public class SegmentoComprobante
    {
        public void Resolucion(List<string> plistaLineasImpresion)
        {
            Line _line = new Line();

            string _mensajeUno =
                "RECIBO DE DINERO ES EL UNICO COMPROBANTE"
                + Environment.NewLine
                + "DE PAGO, EXIJALO AL CANCELAR ESTA FACTURA,"
                + Environment.NewLine
                + "ESTA FACTURA DEVENGA EL 3% DE INTERES"
                + Environment.NewLine
                + "MENSUAL, SI ES CANCELADA DESPUES DEL"
                + Environment.NewLine
                + "VENCIMIENTO."
                + Environment.NewLine
                + Environment.NewLine
                + Environment.NewLine;

            _line.alignCenterMessage(
                        plistaLineasImpresion,
                        _mensajeUno);

        }
    }
}
