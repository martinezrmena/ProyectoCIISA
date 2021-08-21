using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.TablesNAF;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Print;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion
{
    public class reporteEnLineaDocumentos
    {
        private ProcesoImpresion v_procesoImpresion { get; set; }

        public reporteEnLineaDocumentos(ProcesoImpresion pprocesoImpresion)
        {
            v_procesoImpresion = pprocesoImpresion;
        }

        internal async Task imprimirReporteEnLineaDocumentos(DataTable pdt)
        {
            await v_procesoImpresion.v_impresora.connect();

            SegmentoTitulo _segmentoTitulo = new SegmentoTitulo();

            _segmentoTitulo.buscarTituloFacturaPorTipoDocumento(
                v_procesoImpresion.v_listaLineasImpresion,
                ReportType._genericReport
                );

            Title _title = new Title();

            _title.titleGenericReport(v_procesoImpresion.v_listaLineasImpresion, "DOCUMENTOS TRANSMITIDOS");

            SegmentoDatos _segmentoDatos = new SegmentoDatos();

            _segmentoDatos.datosAgente(v_procesoImpresion.v_listaLineasImpresion);

            TimeDate _timeDate = new TimeDate();

            _timeDate.dateTimeDocument(v_procesoImpresion.v_listaLineasImpresion);

            Line _line = new Line();

            _line.dottedLine(v_procesoImpresion.v_listaLineasImpresion);

            SegmentoEncabezado _segmentoEncabezado = new SegmentoEncabezado();
            _segmentoEncabezado.encabezadoReporteEnLineaOrdenesVenta(v_procesoImpresion.v_listaLineasImpresion);

            _line.doubleHypenLine(v_procesoImpresion.v_listaLineasImpresion);

            int _cantidadDocumentos = Numeric._zeroInteger;

            string _codDocumentoAnterior = string.Empty;

            bool _bandera = false;

            foreach (DataRow _fila in pdt.Rows)
            {
                string _fechaHora = _fila[TableDetalleDocumentoNAF.TSTAMP].ToString();

                string _codDocumento = string.Empty;

                _codDocumentoAnterior = _codDocumento;

                string _tipoDoc = _fila[TableDetalleDocumentoNAF.TIPO_DOC].ToString();

                _codDocumento = _fila[TableDetalleDocumentoNAF.NO_TRANSA].ToString();

                string _codCliente = _fila[TableEncabezadoDocumentoNAF.NO_CLIENTE].ToString();

                Logica_ManagerCliente _manager = new Logica_ManagerCliente();

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

                string _linea = _fila[TableDetalleDocumentoNAF.NO_LINEA].ToString();
                string _codProducto = _fila[TableDetalleDocumentoNAF.NO_ARTI].ToString();

                string _cantidad = _fila[TableDetalleDocumentoNAF.CANTIDAD].ToString();
                decimal _cantidadTemp = FormatUtil.convertStringToDecimal(_cantidad);
                _cantidad = FormatUtil.applyCurrencyFormat(_cantidadTemp);

                string _especificacion = _fila[TableDetalleDocumentoNAF.COMENTARIO].ToString();
                string _embalaje = _fila[TableDetalleDocumentoNAF.EMBALAGE].ToString();

                if (_embalaje.Equals(string.Empty))
                {
                    _embalaje = PaymentForm._notApplyInitials;
                }

                _line.printingLinesList(
                    v_procesoImpresion.v_listaLineasImpresion,
                    _linea + " "
                    + _codProducto + " "
                    + _cantidad + " "
                    + _especificacion + " "
                    + _embalaje,
                    1
                    );
            }

            _line.simpleHypenLine(
                v_procesoImpresion.v_listaLineasImpresion
                );

            _line.printingLinesList(v_procesoImpresion.v_listaLineasImpresion, "Líneas transmitidos = " + _cantidadDocumentos, 1);

            SegmentoNomenclatura _segmentoNomenclatura = new SegmentoNomenclatura();

            _segmentoNomenclatura.nomenclaturaReporteDocumentosTransmitidos(v_procesoImpresion.v_listaLineasImpresion);

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
