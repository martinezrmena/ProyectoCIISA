using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones
{
    internal class SegmentoFormaPago
    {
        internal void formaPago(List<string> pprintingLinesList, string pcodTransaction, string pcodTipoTransaccion)
        {
            Line _line = new Line();

            if (pcodTipoTransaccion.Equals(ROLTransactions._reciboDineroSigla))
            {
                Impresion_ManagerPagoRecibo _manager = new Impresion_ManagerPagoRecibo();

                _line.printingLinesList(pprintingLinesList, _manager.buscarLineasFormaPago(pcodTransaction, pcodTipoTransaccion), 1);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._recaudacionSigla))
            {
                Impresion_ManagerPagoRecibo _manager = new Impresion_ManagerPagoRecibo();

                _line.printingLinesList(pprintingLinesList, _manager.buscarLineasFormaPago(pcodTransaction, pcodTipoTransaccion), 1);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._facturaContadoSigla))
            {
                Impresion_ManagerPago _manager = new Impresion_ManagerPago();

                _line.printingLinesList(pprintingLinesList, _manager.buscarLineasFormaPago(pcodTransaction), 1);
            }
        }
    }
}
