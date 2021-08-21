using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using System.Collections.Generic;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones
{
    internal class SegmentoSupermercados
    {
        internal void facturaOriginalParaCobro(List<string> pprintingLinesList, string pcodTipoTransaccion)
        {
            string _tipoCliente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._typeAgent;

            if (_tipoCliente.Equals(Agent._supermercadoSigla))
            {
                if (pcodTipoTransaccion.Equals(ROLTransactions._facturaCreditoSigla))
                {
                    StringBuilder _fopc = new StringBuilder();
                    _fopc.Append("FACTURA ORIGINAL PARA COBRO");

                    Line _line = new Line();

                    _line.simpleHypenLine(pprintingLinesList);

                    Position _position = new Position();

                    _line.printingLinesList(
                        pprintingLinesList,
                        _position.center(_fopc.Length) + _fopc,
                        1);

                    _line.simpleHypenLine(pprintingLinesList);

                }
            }

        }

        internal void atencionCambiosPrecios(List<string> pprintingLinesList, string pcodTipoTransaccion)
        {
            string _tipoCliente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._typeAgent;

            if (_tipoCliente.Equals(Agent._supermercadoSigla))
            {
                if (pcodTipoTransaccion.Equals(ROLTransactions._facturaCreditoSigla))
                {
                    StringBuilder _mensaje = new StringBuilder("**ATENCIÓN*");

                    Line _line = new Line();

                    Position _position = new Position();

                    _line.printingLinesList(
                        pprintingLinesList,
                        _position.center(_mensaje.Length) + _mensaje,
                        1);

                    _mensaje = new StringBuilder("*CAMBIOS DE PRECIOS*");

                    _line.printingLinesList(
                        pprintingLinesList,
                        _position.center(_mensaje.Length) + _mensaje,
                        1);

                    _mensaje = new StringBuilder("*SIN PREVIO*");

                    _line.printingLinesList(
                        pprintingLinesList,
                        _position.center(_mensaje.Length) + _mensaje,
                        1);

                    _mensaje = new StringBuilder("*AVISO*");

                    _line.printingLinesList(
                        pprintingLinesList,
                        _position.center(_mensaje.Length) + _mensaje,
                        1);

                    _mensaje = new StringBuilder("*APLICAN RESTRICCIONES*");

                    _line.printingLinesList(
                        pprintingLinesList,
                        _position.center(_mensaje.Length) + _mensaje,
                        2);

                }
            }

        }

        internal void tipoTransaccionCodigoTransaccion(List<string> pprintingLinesList,string pcodTransaction,string pcodTipoTransaccion,string pnomTipoTransaccion)
        {
            string _tipoCliente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._typeAgent;

            if (_tipoCliente.Equals(Agent._supermercadoSigla))
            {
                if (pcodTipoTransaccion.Equals(ROLTransactions._facturaCreditoSigla))
                {
                    StringBuilder _mensaje = new StringBuilder(pnomTipoTransaccion);
                    _mensaje.Append(": ");
                    _mensaje.Append(pcodTransaction);

                    Line _line = new Line();

                    Position _position = new Position();

                    _line.printingLinesList(
                        pprintingLinesList,
                        _position.center(_mensaje.Length) + _mensaje,
                        1);

                    _mensaje = new StringBuilder("SUPERMERCADOS");

                    _line.printingLinesList(
                        pprintingLinesList,
                        _position.center(_mensaje.Length) + _mensaje,
                        1);
                }
            }

        }
    }
}
