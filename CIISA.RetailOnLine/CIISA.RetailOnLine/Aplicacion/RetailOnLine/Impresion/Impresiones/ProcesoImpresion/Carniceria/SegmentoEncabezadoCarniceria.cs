using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Print;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion.Carniceria
{
    public class SegmentoEncabezadoCarniceria
    {
        internal List<string> pprintingLinesList = new List<string>();
        internal SegmentoCopiaCliente copiaCliente = new SegmentoCopiaCliente();

        /// <summary>
        ///Clase encargada de generar e imprimir el encabezado especial en caso
        ///de tratarse de un agente carnicero, encabezado inicial y codigo de barras
        ///superior.
        /// </summary>
        internal async void subEncabezadoTransaccion(
            Print _print,
            ProcesoImpresion v_procesoImpresion,
            string TipoAgente,
            string codTransaccion,
            int pcopy)
        {

            pprintingLinesList.Add(Environment.NewLine);
            pprintingLinesList.Add(Environment.NewLine);

            if (pcopy >= 1)
            {
                copiaCliente.SegmentoCopia(pprintingLinesList, pcopy);

                pprintingLinesList.Add(Environment.NewLine);
            }

            await _print.print(
                    pprintingLinesList, v_procesoImpresion.v_impresora);

            await _print.printBarCode(
                codTransaccion, v_procesoImpresion.v_impresora);
        }

    }
}
