using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionDocumento;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using System;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion.Carniceria
{
    public class SegmentoDatosCarnicero
    {
        internal SplitString s = new SplitString();

        internal void datosCliente(List<string> pprintingLinesList, Cliente pobjCliente)
        {
            Line _line = new Line();

            _line.printingLinesList(pprintingLinesList, "Condicion de pago: " + "CONTRA ENTREGA", 1);

        }

        internal void datosAgente(List<string> pprintingLinesList)
        {
            Line _line = new Line();

            _line.printingLinesList(pprintingLinesList, "Ruta: " + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent, 1);

            s.Split_String(pprintingLinesList, Agent.getNombreAgente());

            _line.printingLinesList(pprintingLinesList, "Cedula: " + "0204690277", 1);

            //??otra vez datos del agente

            _line.simpleHypenLine(pprintingLinesList);
        }

        internal void encabezadoTransaccion(List<string> pprintingLinesList, string pcodTipoTransaccion)
        {
            string _lineaUno = string.Empty;
            string _lineaDos = string.Empty;

            Position _position = new Position();

            Line _line = new Line();

            _lineaUno += _position.tabular(_lineaDos.Length, PosicionCR._cr_Codigo);
            _lineaUno += "Cód";
            _lineaUno += _position.tabular(_lineaDos.Length, PosicionCR._cr_Descripcion);
            _lineaUno += "Descripción";
            _lineaUno += _position.tabular(_lineaDos.Length, PosicionCR._cr_Unidad);
            _lineaUno += "Unidad";
            _lineaDos += _position.tabular(_lineaDos.Length, PosicionCR._cr_Un);
            _lineaDos += "Un.";
            _lineaDos += _position.tabular(_lineaDos.Length, PosicionCR._cr_Cantidad);
            _lineaDos += "Cantidad";
            _lineaDos += _position.tabular(_lineaDos.Length, PosicionCR._cr_PUnitario);
            _lineaDos += "P/Unitario";
            _lineaDos += _position.tabular(_lineaDos.Length, PosicionCR._cr_Totales);
            _lineaDos += "Totales";

            if (pcodTipoTransaccion.Equals(ROLTransactions._devolucionSigla))
            {
                _lineaDos += Environment.NewLine;
                _lineaDos += "Estado/Motivo/Comentario";
            }

            _line.printingLinesList(pprintingLinesList, _lineaUno, 1);
            _line.printingLinesList(pprintingLinesList, _lineaDos, 1);

        }
    }
}
