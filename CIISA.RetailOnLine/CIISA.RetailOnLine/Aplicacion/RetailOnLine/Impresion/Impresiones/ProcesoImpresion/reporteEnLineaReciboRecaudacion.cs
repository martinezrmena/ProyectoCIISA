using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.TablesNAF;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Print;
using System.Data;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion
{
    public class reporteEnLineaReciboRecaudacion
    {
        private ProcesoImpresion v_procesoImpresion { get; set; }

        public reporteEnLineaReciboRecaudacion(ProcesoImpresion pprocesoImpresion)
        {
            v_procesoImpresion = pprocesoImpresion;
        }

        internal async Task imprimirReporteEnLineaReciboRecaudacion(DataTable pdt)
        {
            await v_procesoImpresion.v_impresora.connect();

            SegmentoTitulo _segmentoTitulo = new SegmentoTitulo();

            _segmentoTitulo.buscarTituloFacturaPorTipoDocumento(
                v_procesoImpresion.v_listaLineasImpresion,
                ReportType._genericReport
                );

            Title _title = new Title();

            _title.titleGenericReport(v_procesoImpresion.v_listaLineasImpresion, "RECIBO/RECAUDACIÓN TRANSMITIDOS");

            SegmentoDatos _segmentoDatos = new SegmentoDatos();

            _segmentoDatos.datosAgente(v_procesoImpresion.v_listaLineasImpresion);

            TimeDate _timeDate = new TimeDate();

            _timeDate.dateTimeDocument(v_procesoImpresion.v_listaLineasImpresion);

            Line _line = new Line();

            _line.doubleHypenLine(v_procesoImpresion.v_listaLineasImpresion);

            int _cantidadDocumentos = Numeric._zeroInteger;

            string _codDocumento = string.Empty;

            bool _bandera = false;

            foreach (DataRow _fila in pdt.Rows)
            {
                string _fechaHora = _fila[TableDetalleDocumentoNAF.TSTAMP].ToString();

                string _codDocumentoAnterior = _codDocumento;

                string _tipoDoc = _fila[TableDetalleReciboNAF.TIPO_DOC].ToString();

                _codDocumento = _fila[TableDetalleReciboNAF.NO_TRANSA].ToString();

                Logica_ManagerCliente _manager = new Logica_ManagerCliente();

                string _codCliente = _fila[TableEncabezadoReciboNAF.NO_CLIENTE].ToString();

                string _nombre = _manager.buscarNombreClientePorCodigoCliente(_codCliente);

                _codDocumento = _tipoDoc + " / " + _codDocumento + " / " + _codCliente + "-" + _nombre;

                if (_codDocumento != _codDocumentoAnterior)
                {
                    if (!_bandera)
                    {
                        _line.printingLinesList(v_procesoImpresion.v_listaLineasImpresion, _fechaHora, 1);
                        _line.printingLinesList(v_procesoImpresion.v_listaLineasImpresion, _codDocumento, 1);

                        _bandera = true;
                    }
                    else
                    {
                        _line.printLinesSpace(v_procesoImpresion.v_listaLineasImpresion, 1);

                        _line.printingLinesList(v_procesoImpresion.v_listaLineasImpresion, _fechaHora, 1);
                        _line.printingLinesList(v_procesoImpresion.v_listaLineasImpresion, _codDocumento, 1);
                    }

                    _cantidadDocumentos++;
                }

                string _noFactura = _fila[TableDetalleReciboNAF.NO_FACTURA].ToString();

                if (_noFactura.Equals(string.Empty))
                {
                    _noFactura = PaymentForm._notApplyInitials;
                }

                string _monto = _fila[TableDetalleReciboNAF.MONTO].ToString();
                decimal _monto2 = FormatUtil.convertStringToDecimal(_monto);
                _monto = FormatUtil.applyCurrencyFormat(_monto2);

                string _tstamp = _fila[TableEncabezadoReciboNAF.TSTAMP].ToString();

                _line.printingLinesList(
                    v_procesoImpresion.v_listaLineasImpresion,
                    _noFactura + " "
                    + _monto,
                    1
                    );
            }

            _line.simpleHypenLine(
                v_procesoImpresion.v_listaLineasImpresion
                );

            _line.printingLinesList(v_procesoImpresion.v_listaLineasImpresion, "Líneas transmitidas = " + _cantidadDocumentos, 1);

            SegmentoNomenclatura _segmentoNomenclatura = new SegmentoNomenclatura();

            _segmentoNomenclatura.nomenclaturaReporteReciboRecaudacionTransmitidas(v_procesoImpresion.v_listaLineasImpresion);

            _line.printLinesSpace(
                v_procesoImpresion.v_listaLineasImpresion,
                1
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

            v_procesoImpresion.v_impresora.disconnect();

            Documents _documents = new Documents();

            _documents.printedDocumentsLog(
                v_procesoImpresion.v_listaLineasImpresion,
                Directorios.reporteDirectorio,
                Directorios.reporteOVTransmitidas
                );
        }


    }
}
