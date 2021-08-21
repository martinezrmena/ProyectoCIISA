using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionReporte;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Print;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion
{
    public class reporteFlujoDinero
    {
        private ProcesoImpresion v_procesoImpresion { get; set; }

        public reporteFlujoDinero(ProcesoImpresion pprocesoImpresion)
        {
            v_procesoImpresion = pprocesoImpresion;
        }

        internal async Task imprimirReporteFlujoDinero(
            string pordenDeVenta,
            string pfacturaContado,
            string pfacturaCredito,
            string pcotizacion,
            string pdevolucion,
            string pregalia,
            string precaudacion,
            string panulacion,
            string preciboDinero,
            string ptramite,
            string ptotalEfectivo,
            string ptotalCheque,
            string ptotalTransferencia,
            string ptotalDeposito,
            string ptotalADepositar
            )
        {
            await v_procesoImpresion.v_impresora.connect();

            if (v_procesoImpresion.ImpresoraFunciona())
            {

                SegmentoTitulo _segmentoTitulo = new SegmentoTitulo();

                _segmentoTitulo.buscarTituloFacturaPorTipoDocumento(
                    v_procesoImpresion.v_listaLineasImpresion,
                    ReportType._genericReport
                    );

                Title _title = new Title();

                _title.titleGenericReport(
                    v_procesoImpresion.v_listaLineasImpresion,
                    "REPORTE FLUJO DINERO"
                    );

                SegmentoDatos _segmentoDatos = new SegmentoDatos();

                _segmentoDatos.datosAgente(v_procesoImpresion.v_listaLineasImpresion);

                TimeDate _timeDate = new TimeDate();

                _timeDate.dateTimeDocument(v_procesoImpresion.v_listaLineasImpresion);

                Line _line = new Line();

                _line.doubleHypenLine(v_procesoImpresion.v_listaLineasImpresion);

                Position _position = new Position();

                string _lineaUno = string.Empty;
                _lineaUno += _position.tabular(
                                _lineaUno.Length,
                                RepFlujoDinero.documentos
                                );
                _lineaUno += "Orden de Venta:";
                _lineaUno += _position.tabular(_lineaUno.Length, RepFlujoDinero.monto);
                _lineaUno += pordenDeVenta;

                _line.printingLinesList(v_procesoImpresion.v_listaLineasImpresion, _lineaUno, 1);

                string _lineaDos = string.Empty;
                _lineaDos += _position.tabular(_lineaDos.Length, RepFlujoDinero.documentos);
                _lineaDos += "Factura Contado:";
                _lineaDos += _position.tabular(_lineaDos.Length, RepFlujoDinero.monto);
                _lineaDos += pfacturaContado;

                _line.printingLinesList(v_procesoImpresion.v_listaLineasImpresion, _lineaDos, 1);

                string _lineaTres = string.Empty;
                _lineaTres += _position.tabular(_lineaTres.Length, RepFlujoDinero.documentos);
                _lineaTres += "Factura Crédito:";
                _lineaTres += _position.tabular(_lineaTres.Length, RepFlujoDinero.monto);
                _lineaTres += pfacturaCredito;

                _line.printingLinesList(v_procesoImpresion.v_listaLineasImpresion, _lineaTres, 1);

                string _lineaCuatro = string.Empty;
                _lineaCuatro += _position.tabular(_lineaCuatro.Length, RepFlujoDinero.documentos);
                _lineaCuatro += "Cotización:";
                _lineaCuatro += _position.tabular(_lineaCuatro.Length, RepFlujoDinero.monto);
                _lineaCuatro += pcotizacion;

                _line.printingLinesList(v_procesoImpresion.v_listaLineasImpresion, _lineaCuatro, 1);

                string _lineaCinco = string.Empty;
                _lineaCinco += _position.tabular(_lineaCinco.Length, RepFlujoDinero.documentos);
                _lineaCinco += "Devolución:";
                _lineaCinco += _position.tabular(_lineaCinco.Length, RepFlujoDinero.monto);
                _lineaCinco += pdevolucion;

                _line.printingLinesList(v_procesoImpresion.v_listaLineasImpresion, _lineaCinco, 1);

                string _lineaSeis = string.Empty;
                _lineaSeis += _position.tabular(_lineaSeis.Length, RepFlujoDinero.documentos);
                _lineaSeis += "Regalía:";
                _lineaSeis += _position.tabular(_lineaSeis.Length, RepFlujoDinero.monto);
                _lineaSeis += pregalia;

                _line.printingLinesList(v_procesoImpresion.v_listaLineasImpresion, _lineaSeis, 1);

                string _lineaSiete = string.Empty;
                _lineaSiete += _position.tabular(_lineaSiete.Length, RepFlujoDinero.documentos);
                _lineaSiete += "Recaudación:";
                _lineaSiete += _position.tabular(_lineaSiete.Length, RepFlujoDinero.monto);
                _lineaSiete += precaudacion;

                _line.printingLinesList(v_procesoImpresion.v_listaLineasImpresion, _lineaSiete, 1);

                string _lineaOcho = string.Empty;
                _lineaOcho += _position.tabular(_lineaOcho.Length, RepFlujoDinero.documentos);
                _lineaOcho += "Anulación:";
                _lineaOcho += _position.tabular(_lineaOcho.Length, RepFlujoDinero.monto);
                _lineaOcho += panulacion;

                _line.printingLinesList(v_procesoImpresion.v_listaLineasImpresion, _lineaOcho, 1);

                string _lineaNueve = string.Empty;
                _lineaNueve += _position.tabular(_lineaNueve.Length, RepFlujoDinero.documentos);
                _lineaNueve += "Recibo Dinero:";
                _lineaNueve += _position.tabular(_lineaNueve.Length, RepFlujoDinero.monto);
                _lineaNueve += preciboDinero;

                _line.printingLinesList(v_procesoImpresion.v_listaLineasImpresion, _lineaNueve, 1);

                string _lineaDiez = string.Empty;
                _lineaDiez += _position.tabular(_lineaDiez.Length, RepFlujoDinero.documentos);
                _lineaDiez += "Trámite:";
                _lineaDiez += _position.tabular(_lineaDiez.Length, RepFlujoDinero.monto);
                _lineaDiez += ptramite;

                _line.printingLinesList(v_procesoImpresion.v_listaLineasImpresion, _lineaDiez, 2);

                string _lineaOnce = string.Empty;
                _lineaOnce += _position.tabular(_lineaOnce.Length, RepFlujoDinero.documentos);
                _lineaOnce += "- Total Efectivo:";
                _lineaOnce += _position.tabular(_lineaOnce.Length, RepFlujoDinero.monto);
                _lineaOnce += ptotalEfectivo;

                _line.printingLinesList(v_procesoImpresion.v_listaLineasImpresion, _lineaOnce, 1);

                string _lineaDoce = string.Empty;
                _lineaDoce += _position.tabular(_lineaDoce.Length, RepFlujoDinero.documentos);
                _lineaDoce += "- Total Cheque:";
                _lineaDoce += _position.tabular(_lineaDoce.Length, RepFlujoDinero.monto);
                _lineaDoce += ptotalCheque;

                _line.printingLinesList(v_procesoImpresion.v_listaLineasImpresion, _lineaDoce, 1);

                string _lineaTrece = string.Empty;
                _lineaTrece += _position.tabular(_lineaTrece.Length, RepFlujoDinero.documentos);
                _lineaTrece += "- Total Transferecia:";
                _lineaTrece += _position.tabular(_lineaTrece.Length, RepFlujoDinero.monto);
                _lineaTrece += ptotalTransferencia;

                _line.printingLinesList(v_procesoImpresion.v_listaLineasImpresion, _lineaTrece, 1);

                string _lineaCatorce = string.Empty;
                _lineaCatorce += _position.tabular(_lineaCatorce.Length, RepFlujoDinero.documentos);
                _lineaCatorce += "- Total Deposito:";
                _lineaCatorce += _position.tabular(_lineaCatorce.Length, RepFlujoDinero.monto);
                _lineaCatorce += ptotalDeposito;

                _line.printingLinesList(v_procesoImpresion.v_listaLineasImpresion, _lineaCatorce, 2);

                _line.simpleHypenLine(v_procesoImpresion.v_listaLineasImpresion);

                string _lineaQuince = string.Empty;
                _lineaQuince += _position.tabular(_lineaQuince.Length, RepFlujoDinero.documentos);
                _lineaQuince += "- TOTAL A DEPOSITAR:";
                _lineaQuince += _position.tabular(_lineaQuince.Length, RepFlujoDinero.monto);
                _lineaQuince += ptotalADepositar;

                _line.printingLinesList(
                    v_procesoImpresion.v_listaLineasImpresion,
                    _lineaQuince,
                    1
                    );

                _line.simpleHypenLine(
                    v_procesoImpresion.v_listaLineasImpresion
                    );

                _line.printLinesSpace(
                    v_procesoImpresion.v_listaLineasImpresion,
                    1
                    );

                Notes _notes = new Notes();

                _notes.noteSection(
                    v_procesoImpresion.v_listaLineasImpresion
                    );

                _line.doubleHypenLine(
                    v_procesoImpresion.v_listaLineasImpresion
                    );

                _line.printLinesSpace(
                    v_procesoImpresion.v_listaLineasImpresion,
                    1
                    );

                Copy _copy = new Copy();

                _copy.copies(
                    v_procesoImpresion.v_listaLineasImpresion,
                    1,
                    1
                    );

                _line.finalSpace(
                    v_procesoImpresion.v_listaLineasImpresion
                    );

                _line.printingLinesList(
                    v_procesoImpresion.v_listaLineasImpresion,
                    Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._name
                        + ". VERSIÓN "
                        + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._version,
                    2
                    );

                Print _print = new Print();

                await _print.print(
                    v_procesoImpresion.v_listaLineasImpresion,
                    v_procesoImpresion.v_impresora
                    );

                await _print.print(
                    v_procesoImpresion.v_listaLineasImpresion,
                    v_procesoImpresion.v_impresora
                    );

                v_procesoImpresion.v_impresora.disconnect();

                Documents _documents = new Documents();

                _documents.printedDocumentsLog(
                    v_procesoImpresion.v_listaLineasImpresion,
                    Directorios.reporteDirectorio,
                    Directorios.reporteFlujoDineroArchivo
                    );

            }

           
        }


    }
}
