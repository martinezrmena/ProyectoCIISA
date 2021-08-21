using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using System;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones
{
    internal class SegmentoLeyenda
    {
        internal void pintarLeyendaProductosExentosIVA(List<string> pprintingLinesList,string pcodTipoTransaccion,string pcodTransaction,string pnomTipoTransaccion)
        {
            if (pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla))
            {
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._facturaContadoSigla))
            {
                pintarLeyendaProductosExentosIVAContenido(pprintingLinesList);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._facturaCreditoSigla))
            {
                pintarLeyendaProductosExentosIVAContenido(pprintingLinesList);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._cotizacionSigla))
            {
                pintarLeyendaProductosExentosIVAContenido(pprintingLinesList);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._devolucionSigla))
            {
                pintarLeyendaProductosExentosIVAContenido(pprintingLinesList);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._regaliaSigla))
            {
                pintarLeyendaProductosExentosIVAContenido(pprintingLinesList);
            }
        }

        private void pintarLeyendaProductosExentosIVAContenido(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printingLinesList(
                pprintingLinesList,
                Simbol._asterisk
                    + " Productos exentos de impuesto.",
                2
                );
        }

        internal void pintarLeyendaRevisaProducto(List<string> pprintingLinesList,string pcodTipoTransaccion,string pcodTransaction,string pnomTipoTransaccion)
        {
            if (pcodTipoTransaccion.Equals(ROLTransactions._facturaContadoSigla))
            {
                pintarLeyendaRevisaProductoContenido(pprintingLinesList);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._facturaCreditoSigla))
            {
                pintarLeyendaRevisaProductoContenido(pprintingLinesList);
            }
        }

        private void pintarLeyendaRevisaProductoContenido(List<string> pprintingLinesList)
        {
            string _tipoCliente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._typeAgent;

            if (_tipoCliente.Equals(Agent._ruteroSigla))
            {
                Line _line = new Line();

                _line.printingLinesList(
                    pprintingLinesList,
                    "REVISE BIEN EL PRODUCTO ESPECIFICADO EN LA",
                    1
                    );

                _line.printingLinesList(
                    pprintingLinesList,
                    "FACTURA, CIISA NO SE HACE RESPONSABLE POR",
                    1
                    );

                _line.printingLinesList(
                    pprintingLinesList,
                     "RECLAMOS POSTERIORES.",
                    2
                    );

            }

        }
    }
}
