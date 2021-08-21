using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones
{
    internal class SegmentoReimpresion
    {
        SplitString s = new SplitString();
    
        internal void tipoTransaccionCodigoTransaccion(List<string> pprintingLinesList,string pcodTransaction,string pnomTipoTransaccion,bool preprint, string numero_agente)
        {
            Line _line = new Line();

            if (preprint)
            {
                _line.printingLinesList(pprintingLinesList,
                    "ATENCIÓN: REIMPRESIÓN"
                    + Environment.NewLine
                    + Simbol._squareBracketLeft
                    + "Departamento Liquidaciones"
                    + Simbol._squareBracketRight
                    + Environment.NewLine, 1);
            }

            if (pprintingLinesList.Count == 0) {

                pnomTipoTransaccion = Environment.NewLine + pnomTipoTransaccion;
            }

            if (pnomTipoTransaccion.Equals(ROLTransactions._facturaContadoNombre) || pnomTipoTransaccion.Equals(ROLTransactions._facturaCreditoNombre))
            {
                /**************************************************************************************/
                _line.printingLinesList(pprintingLinesList, "Comprobante Electrónico: " + pcodTransaction, 1);

                /**************************************************************************************/
                StringBuilder sb_facturaElectronica = new StringBuilder();
                sb_facturaElectronica.Append("001"); //3
                sb_facturaElectronica.Append("0" + numero_agente); //8
                sb_facturaElectronica.Append("01"); //10
                sb_facturaElectronica.Append(Convert.ToInt32(pcodTransaction).ToString("D10")); //20

                _line.printingLinesList(pprintingLinesList, "Factura Electrónica: " + sb_facturaElectronica.ToString(), 1);

                /**************************************************************************************/
                var pcodTransactionSinCodRuta = pcodTransaction.Substring(4, pcodTransaction.Length - 4);

                StringBuilder sb_claveNumerica = new StringBuilder();
                sb_claveNumerica.Append("506"); //3
                sb_claveNumerica.Append(DateTime.Now.ToString("dd")); //5
                sb_claveNumerica.Append(DateTime.Now.ToString("MM")); //7
                sb_claveNumerica.Append(DateTime.Now.ToString("yy")); //9
                sb_claveNumerica.Append("003101008150"); //21
                sb_claveNumerica.Append(sb_facturaElectronica); //41
                sb_claveNumerica.Append("3"); //42                                                
                sb_claveNumerica.Append(Convert.ToInt32(pcodTransactionSinCodRuta).ToString("D8")); //50

                s.Split_String(pprintingLinesList, "Clave Numérica: " + sb_claveNumerica.ToString());
            }
            else
            {
                _line.printingLinesList(pprintingLinesList, pnomTipoTransaccion
                + ": "
                + pcodTransaction, 1);
            }

        }

        

    }
}