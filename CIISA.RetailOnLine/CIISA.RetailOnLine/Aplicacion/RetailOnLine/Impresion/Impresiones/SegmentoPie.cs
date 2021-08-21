using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones
{
    internal class SegmentoPie
    {
        internal void pieTransaccion(List<string> pprintingLinesList, string pcodTipoTransaccion, string pcodTransaction, string pnomTipoTransaccion, bool preprint)
        {
            string _mensajeUno =
                "Factura Original o Recibo de Dinero son los"
                + Environment.NewLine
                + "únicos comprobantes de pago, exíjalo al"
                + Environment.NewLine
                + "cancelar esta factura. Esta factura es título"
                + Environment.NewLine
                + "ejecutivo, Art. 460 Código de Comercio. "
                + Environment.NewLine
                + "A partir del vencimiento devengará 3.5%"
                + Environment.NewLine
                + "mensual.";

            string _mensajeDos =
                "La validez de este documento está condicionado"
                + Environment.NewLine
                + "a que los cheques recibidos en pago parcial o"
                + Environment.NewLine
                + "total sean pagados por el banco respectivo.";

            string _mensajeTres =
                "Autorizado mediante resolución N. 11-97 de"
                + Environment.NewLine
                + "Agosto de 1997. D.G.T.D.";

            string _mensajeCuatro =
                "Renuncio a mi domicilio y notificación así "
                + Environment.NewLine
                + "como cualquier trámite de juicio ejecutivo."
                + Environment.NewLine
                + "Al mismo tiempo doy por aceptadas las "
                + Environment.NewLine
                + "condiciones del código de comercio según"
                + Environment.NewLine
                + "artículo 460. El deudor señala para "
                + Environment.NewLine
                + "notificaciones el domicilio que indica arriba."
                + Environment.NewLine
                + "A partir del vencimiento devengara interés "
                + Environment.NewLine
                + "del 3.5% mensual.";

            string _mensajeCinco = "Recibido conforme.";

            Line _line = new Line();

            if (pcodTipoTransaccion.Equals(ROLTransactions._facturaCreditoSigla))
            {
                string _tipoCliente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._typeAgent;

                if (_tipoCliente.Equals(Agent._ruteroSigla))
                {
                    _line.printingLinesList(pprintingLinesList, _mensajeTres, 1);

                    _line.printingLinesList(pprintingLinesList, _mensajeCuatro, 1);
                }

                if (_tipoCliente.Equals(Agent._supermercadoSigla) || _tipoCliente.Equals(Agent._carniceroSigla) || _tipoCliente.Equals(Agent._cobradorSigla))
                {
                    Position _position = new Position();

                    StringBuilder _mensaje = new StringBuilder("RECIBO DE DINERO ES EL ÚNICO COMPROBANTE");

                    _line.printingLinesList(
                        pprintingLinesList,
                        _position.center(_mensaje.Length) + _mensaje,
                        1);

                    _mensaje = new StringBuilder("DE PAGO EXÍJALO AL CANCELAR ESTA FACTURA");

                    _line.printingLinesList(
                        pprintingLinesList,
                        _position.center(_mensaje.Length) + _mensaje,
                        1);

                    _mensaje = new StringBuilder("ESTA FACTURA DEVENGA EL 3% DE INTERÉS");

                    _line.printingLinesList(
                        pprintingLinesList,
                        _position.center(_mensaje.Length) + _mensaje,
                        1);

                    _mensaje = new StringBuilder("MENSUAL SI ES CANCELADA DESPUÉS DEL");

                    _line.printingLinesList(
                        pprintingLinesList,
                        _position.center(_mensaje.Length) + _mensaje,
                        1);

                    _mensaje = new StringBuilder("VENCIMIENTO");

                    _line.printingLinesList(
                        pprintingLinesList,
                        _position.center(_mensaje.Length) + _mensaje,
                        1);
                }
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._facturaContadoSigla))
            {
                _line.printingLinesList(pprintingLinesList, _mensajeTres, 1);

                _line.printingLinesList(pprintingLinesList, _mensajeDos, 1);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._recaudacionSigla))
            {
                _line.printingLinesList(pprintingLinesList, _mensajeDos, 1);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._reciboDineroSigla))
            {
                _line.printingLinesList(pprintingLinesList, _mensajeUno, 1);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._tramiteSigla))
            {
                _line.printingLinesList(pprintingLinesList, _mensajeCinco, 1);
            }

        }

        internal void pieCrediticioDelCliente(List<string> pprintingLinesList)
        {
            string _mensajeUno = "Nulo sin firma y sello.";

            Line _line = new Line();

            _line.printingLinesList(pprintingLinesList, _mensajeUno, 1);
        }
    }
}
