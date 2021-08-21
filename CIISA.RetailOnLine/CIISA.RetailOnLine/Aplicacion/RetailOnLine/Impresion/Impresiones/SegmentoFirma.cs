using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionReporte;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones
{
    internal class SegmentoFirma
    {
        internal void firma(List<string> pprintingLinesList, string pcodTipoTransaccion)
        {
            if (pcodTipoTransaccion.Equals(ROLTransactions._facturaCreditoSigla))
            {
                firmaTransacciones(pprintingLinesList);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._facturaContadoSigla))
            {
                firmaTransacciones(pprintingLinesList);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._reciboDineroSigla))
            {
                firmaTransacciones(pprintingLinesList);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._recaudacionSigla))
            {
                firmaTransacciones(pprintingLinesList);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._regaliaSigla))
            {
                firmaRegalia(pprintingLinesList);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._devolucionSigla))
            {
                firmaDevolucion(pprintingLinesList);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._tramiteSigla))
            {
                firmaTramite(pprintingLinesList);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._anulacionSigla))
            {
                if (pcodTipoTransaccion.Equals(ROLTransactions._facturaContadoSigla))
                {
                    firmaAnulacion(pprintingLinesList);
                }

                if (pcodTipoTransaccion.Equals(ROLTransactions._facturaCreditoSigla))
                {
                    firmaAnulacion(pprintingLinesList);
                }

                if (pcodTipoTransaccion.Equals(ROLTransactions._reciboDineroSigla))
                {
                    firmaAnulacion(pprintingLinesList);
                }

                if (pcodTipoTransaccion.Equals(ROLTransactions._recaudacionSigla))
                {
                    firmaAnulacion(pprintingLinesList);
                }

            }

            Line _line = new Line();

            _line.printLineSpace(pprintingLinesList, 1);

        }

        internal void firmaTransacciones(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printLineSpace(pprintingLinesList, 2);

            _line.printingLinesList(pprintingLinesList, "Firma Cliente ó Autorizado: _________________", 3);

            _line.printingLinesList(pprintingLinesList, "                    Cédula: _________________", 2);
        }

        internal void firmaInventarioAuditoria(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printLineSpace(pprintingLinesList, 2);

            _line.printingLinesList(pprintingLinesList, "  Nombre/Firma Auditor: ______________________", 3);

            _line.printingLinesList(pprintingLinesList, "  Nombre/Firma Agente:  ______________________", 2);
        }

        internal void firmaInventarioTomaFisica(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printLineSpace(pprintingLinesList, 2);

            _line.printingLinesList(pprintingLinesList, "  Nombre/Firma Liquidador Inventarios: ________", 3);

            _line.printingLinesList(pprintingLinesList, "  Nombre/Firma Agente:  _______________________", 2);
        }

        internal void firmaDevolucion(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printLineSpace(pprintingLinesList, 2);

            _line.printingLinesList(pprintingLinesList, "Firma Vendedor: ____________________", 1);
        }

        internal void firmaRegalia(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printLineSpace(pprintingLinesList, 2);

            _line.printingLinesList(pprintingLinesList, "Recibido Conforme: _________________", 3);

            _line.printingLinesList(pprintingLinesList, "           Cédula: _________________", 1);
        }

        internal void firmaTramite(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printLineSpace(pprintingLinesList, 2);

            _line.printingLinesList(pprintingLinesList, "Nombre: ____________________________", 3);

            _line.printingLinesList(pprintingLinesList, "Cédula: ____________________________", 3);

            _line.printingLinesList(pprintingLinesList, "Firma: _____________________________", 1);
        }

        internal void firmaAnulacion(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printLineSpace(pprintingLinesList, 2);

            _line.printingLinesList(pprintingLinesList, "Nombre: ____________________________", 3);

            _line.printingLinesList(pprintingLinesList, "Cédula: ____________________________", 3);

            _line.printingLinesList(pprintingLinesList, "Firma: _____________________________", 1);
        }

        internal void firmaCrediticioDelCliente(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printLineSpace(pprintingLinesList, 2);

            Position _position = new Position();

            string _linea = "Firma: _________________";
            _linea += _position.tabular(_linea.Length, RepCrediticioCliente.vencimiento);
            _linea += "Sello";

            _line.printingLinesList(pprintingLinesList, _linea, 2);
        }

    }
}
