using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using System.Collections.Generic;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones
{
    internal class SegmentoTitulo
    {

        internal void buscarTituloFacturaPorTipoDocumento(List<string> pprintingLinesList, string pcodTipoTransaccion)
        {
            
            if (!pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla)
                && !pcodTipoTransaccion.Equals(ROLTransactions._cotizacionSigla))
            {
                Line _line = new Line();

                _line.printLinesSpace(pprintingLinesList, 1);

                Impresion_ManagerTituloComprobante _manager = new Impresion_ManagerTituloComprobante();

                _manager.buscarTituloFacturaPorTipoDocumento(pcodTipoTransaccion, pprintingLinesList);

                _line.simpleHypenLine(pprintingLinesList);
            }
        }

        internal void tituloReporte(List<string> pprintingLinesList, string pcodTipoTransaccion)
        {
            StringBuilder _reportName = new StringBuilder();
            _reportName.Append("REPORTE ");

            if (pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla))
            {
                _reportName.Append(ROLTransactions._ordenVentaNombre);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._facturaContadoSigla))
            {
                _reportName.Append(ROLTransactions._facturaContadoNombre);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._facturaCreditoSigla))
            {
                _reportName.Append(ROLTransactions._facturaCreditoNombre);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._cotizacionSigla))
            {
                _reportName.Append(ROLTransactions._cotizacionNombre);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._devolucionSigla))
            {
                _reportName.Append(ROLTransactions._devolucionNombre);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._regaliaSigla))
            {
                _reportName.Append(ROLTransactions._regaliaNombre);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._recaudacionSigla))
            {
                _reportName.Append(ROLTransactions._recaudacionNombre);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._reciboDineroSigla))
            {
                _reportName.Append(ROLTransactions._reciboDineroNombre);
            }

            Line _line = new Line();

            Position _position = new Position();

            _line.printingLinesList(
                pprintingLinesList,
                _position.center(_reportName.Length) + _reportName.ToString().ToUpper(),
                1);
        }
    }
}
