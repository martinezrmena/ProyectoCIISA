using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones
{
    internal class SegmentoNomenclatura
    {
        internal void nomenclaturaInventarioTeorico(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printLinesSpace(pprintingLinesList, 1);
            _line.printingLinesList(pprintingLinesList, "NOMENCLATURA:", 2);
            _line.printingLinesList(pprintingLinesList, "  Cant: Cantidad (Inventario inicial).", 1);
            _line.printingLinesList(pprintingLinesList, "  Vent: Ventas.", 1);
            _line.printingLinesList(pprintingLinesList, "  Vent: Ventas.", 1);
            _line.printingLinesList(pprintingLinesList, "  Devo: Devoluciones.", 1);
            _line.printingLinesList(pprintingLinesList, "  Rega: Regalías.", 1);
            _line.printingLinesList(pprintingLinesList, "  Anul: Anulaciones", 1);
            _line.printingLinesList(pprintingLinesList, "  Disp: Disponible (Inventario final).", 2);
        }

        internal void nomenclaturaIndicadoresFacturacion(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printLinesSpace(pprintingLinesList, 1);
            _line.printingLinesList(pprintingLinesList, "NOMENCLATURA:", 2);
            _line.printingLinesList(pprintingLinesList, IndFacturacionNS._codigo + " : Código.", 1);
            _line.printingLinesList(pprintingLinesList, IndFacturacionNS._pedido + "  : Factura contado.", 1);
            _line.printingLinesList(pprintingLinesList, IndFacturacionNS._facturaContado + "  : Factura crédito.", 1);
            _line.printingLinesList(pprintingLinesList, IndFacturacionNS._facturaCredito + "  : Respeta límite.", 1);
            _line.printingLinesList(pprintingLinesList, IndFacturacionNS._respetaLimite + "  : Cheque.", 1);
            _line.printingLinesList(pprintingLinesList, IndFacturacionNS._cheque + "  : Monto límite.", 1);
            _line.printingLinesList(pprintingLinesList, IndFacturacionNS._vencimiento + "  : Vencimiento.", 1);
            _line.printingLinesList(pprintingLinesList, IndFacturacionNS._estado + "  : Estado.", 1);
            _line.printingLinesList(pprintingLinesList, IndFacturacionNS._cobrador + ": Cobrador.", 1);
            _line.printingLinesList(pprintingLinesList, IndFacturacionNS._cobro + "  : Cobro.", 2);
        }

        internal void nomenclaturaReporteCrediticioDelCliente(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printLinesSpace(pprintingLinesList, 1);
            _line.printingLinesList(pprintingLinesList, "NOMENCLATURA:", 2);
            _line.printingLinesList(pprintingLinesList, "  No. Docu:    Número documento.", 1);
            _line.printingLinesList(pprintingLinesList, "  M. Original: Monto original.", 1);
            _line.printingLinesList(pprintingLinesList, "  Fecha Venc:  Fecha vence.", 1);
            _line.printingLinesList(pprintingLinesList, "  Dias Venc:   Días vencida.", 1);
            _line.printingLinesList(pprintingLinesList, "  No. Líneas:  Número de líneas.", 2);
        }

        internal void nomenclaturaReporteDocumentosRealizados(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printLinesSpace(pprintingLinesList, 1);
            _line.printingLinesList(pprintingLinesList, "NOMENCLATURA:", 2);
            _line.printingLinesList(pprintingLinesList, "  No. Docu:    Número documento.", 1);
            _line.printingLinesList(pprintingLinesList, "  T.D.:        Tipo documento.", 1);
            _line.printingLinesList(pprintingLinesList, "  Tr.:         Traslado.", 1);
            _line.printingLinesList(pprintingLinesList, "  An.:         Anulación.", 1);
            _line.printingLinesList(pprintingLinesList, "  Cod. C.:     Código Cliente.", 1);
            _line.printingLinesList(pprintingLinesList, "  M. Original: Monto original.", 1);
            _line.printingLinesList(pprintingLinesList, "  N/A:         No aplica.", 1);
            _line.printingLinesList(pprintingLinesList, "  No. Líneas:  Número de líneas.", 2);
        }

        internal void nomenclaturaReporteTrasladosDiarios(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printLinesSpace(pprintingLinesList, 1);
            _line.printingLinesList(pprintingLinesList, "NOMENCLATURA:", 2);
            _line.printingLinesList(pprintingLinesList, "  Estado L:    Listo.", 1);
            _line.printingLinesList(pprintingLinesList, "  Estado A:    Aplicado.", 1);
            _line.printLinesSpace(pprintingLinesList, 1);
        }

        internal void nomenclaturaReporteDocumentosTransmitidos(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printLinesSpace(pprintingLinesList, 1);
            _line.printingLinesList(pprintingLinesList, "NOMENCLATURA:", 2);
            _line.printingLinesList(pprintingLinesList, "  " + ROLTransactions._cotizacionSigla + ": " + ROLTransactions._cotizacionNombre, 1);
            _line.printingLinesList(pprintingLinesList, "  " + ROLTransactions._devolucionSigla + ": " + ROLTransactions._devolucionNombre, 1);
            _line.printingLinesList(pprintingLinesList, "  " + ROLTransactions._facturaContadoSigla + ": " + ROLTransactions._facturaContadoNombre, 1);
            _line.printingLinesList(pprintingLinesList, "  " + ROLTransactions._facturaCreditoSigla + ": " + ROLTransactions._facturaCreditoNombre, 1);
            _line.printingLinesList(pprintingLinesList, "  " + ROLTransactions._regaliaSigla + ": " + ROLTransactions._regaliaNombre, 2);
        }

        internal void nomenclaturaReporteReciboRecaudacionTransmitidas(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printLinesSpace(pprintingLinesList, 1);
            _line.printingLinesList(pprintingLinesList, "NOMENCLATURA:", 2);
            _line.printingLinesList(pprintingLinesList, "  " + ROLTransactions._reciboDineroSigla + ": " + ROLTransactions._reciboDineroNombre, 1);
            _line.printingLinesList(pprintingLinesList, "  " + ROLTransactions._recaudacionSigla + ": " + ROLTransactions._recaudacionNombre, 2);
        }    
    }
}
