using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion.Carniceria
{
    public class SegmentoLeyendaCarniceria
    {
        internal void pintarLeyendaComprobante(
            List<string> pprintingLinesList,
            string pcodTipoTransaccion,
            string pcodTransaction)
        {
            string _mensajeUno =
                "Aplica Restricciones."
                + Environment.NewLine
                + string.Format("Factura: {0}", pcodTransaction)
                + Environment.NewLine
                + "* Impuesto de Ventas"
                + Environment.NewLine
                + Environment.NewLine;

            Line _line = new Line();

            if (pcodTipoTransaccion.Equals(ROLTransactions._facturaCreditoSigla))
            {
                _line.alignCenterMessage(pprintingLinesList, _mensajeUno);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._facturaContadoSigla))
            {
                _line.alignCenterMessage(pprintingLinesList, _mensajeUno);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._devolucionSigla))
            {
                //pintarLeyendaProductosExentosIVAContenido(pprintingLinesList);
            }

        }

    }
}
