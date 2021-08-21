using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Print;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion.Carniceria
{
    public class SegmentoPieCarniceria
    {
        internal List<string> pprintingLinesList = new List<string>();
        internal SegmentoCopiaCliente copiaCliente = new SegmentoCopiaCliente();
        internal SegmentoLeyendaCarniceria SLC = new SegmentoLeyendaCarniceria();
        internal SegmentoMensajeEspecial segmentoMensaje = new SegmentoMensajeEspecial();

        internal async void pieTransaccion(
            Print _print,
            ProcesoImpresion v_procesoImpresion,
            string TipoAgente,
            string pcodTipoTransaccion,
            string codTransaction,
            int nCopy)
        {

            if (nCopy >= 1)
            {
                //Copia N° Cliente
                copiaCliente.SegmentoCopia(pprintingLinesList, nCopy);

                pprintingLinesList.Add(Environment.NewLine);
            }

            //Leyenda especial
            //SLC.pintarLeyendaComprobante(pprintingLinesList, pcodTipoTransaccion, codTransaction);

            //Segmento mensaje
            segmentoMensaje.Mensaje(pprintingLinesList);

            //Sección de impresión
            await _print.print(
                    pprintingLinesList, v_procesoImpresion.v_impresora);

            await _print.printBarCode(
                    codTransaction, v_procesoImpresion.v_impresora);

            //Es necesario reiniciar las lineas de impresion para los espacios finales
            pprintingLinesList = new List<string>();

            pprintingLinesList.Add(Environment.NewLine);
            pprintingLinesList.Add(Environment.NewLine);
            pprintingLinesList.Add(Environment.NewLine);

            await _print.print(
                        pprintingLinesList, v_procesoImpresion.v_impresora);


        }
    }
}
