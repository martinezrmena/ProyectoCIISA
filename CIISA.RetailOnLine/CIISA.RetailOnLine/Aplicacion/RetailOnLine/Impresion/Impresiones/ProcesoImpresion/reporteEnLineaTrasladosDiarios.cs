using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionDocumento;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Print;
using System.Data;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion
{
    public class reporteEnLineaTrasladosDiarios
    {
        private ProcesoImpresion v_procesoImpresion { get; set; }

        public reporteEnLineaTrasladosDiarios(ProcesoImpresion pprocesoImpresion)
        {
            v_procesoImpresion = pprocesoImpresion;
        }

        internal async Task imprimirReporteEnLineaTrasladosDiarios(DataTable pdt)
        {
            await v_procesoImpresion.v_impresora.connect();

            SegmentoTitulo _segmentoTitulo = new SegmentoTitulo();

            _segmentoTitulo.buscarTituloFacturaPorTipoDocumento(
                v_procesoImpresion.v_listaLineasImpresion,
                ReportType._genericReport
                );

            Title _title = new Title();

            _title.titleGenericReport(v_procesoImpresion.v_listaLineasImpresion, "TRASLADOS DIARIOS");

            SegmentoDatos _segmentoDatos = new SegmentoDatos();

            _segmentoDatos.datosAgente(v_procesoImpresion.v_listaLineasImpresion);

            TimeDate _timeDate = new TimeDate();

            _timeDate.dateTimeDocument(v_procesoImpresion.v_listaLineasImpresion);

            Line _line = new Line();

            _line.dottedLine(v_procesoImpresion.v_listaLineasImpresion);

            SegmentoEncabezado _segmentoEncabezado = new SegmentoEncabezado();
            _segmentoEncabezado.encabezadoReporteEnLineaTrasladosDiarios(v_procesoImpresion.v_listaLineasImpresion, pdt);

            _line.doubleHypenLine(v_procesoImpresion.v_listaLineasImpresion);

            int _cantidadPedidos = Numeric._zeroInteger;

            foreach (DataRow _fila in pdt.Rows)
            {
                string _linea = string.Empty;

                Position _position = new Position();

                _linea += _position.tabular(_linea.Length, PosicionTD.linea);
                _linea += _cantidadPedidos++;
                _linea += _position.tabular(_linea.Length, PosicionTD.pedido);
                _linea += _fila["NUM_PEDIDO_RUTERO"].ToString();
                _linea += _position.tabular(_linea.Length, PosicionTD.articulo);
                _linea += _fila["ARTICULO"].ToString();
                _linea += _position.tabular(_linea.Length, PosicionTD.cantidad);

                decimal _cantidad = FormatUtil.convertStringToDecimal(_fila["UDS_PES"].ToString());
                _linea += FormatUtil.applyCurrencyFormat(_cantidad);

                _linea += _position.tabular(_linea.Length, PosicionTD.estado);
                _linea += _fila["ESTADO"].ToString();

                _line.printingLinesList(v_procesoImpresion.v_listaLineasImpresion, _linea, 1);
            }

            _line.simpleHypenLine(v_procesoImpresion.v_listaLineasImpresion);

            _line.printingLinesList(v_procesoImpresion.v_listaLineasImpresion, "Líneas = " + _cantidadPedidos, 1);

            _line.printLinesSpace(v_procesoImpresion.v_listaLineasImpresion, 1);

            SegmentoNomenclatura _segmentoNomenclatura = new SegmentoNomenclatura();

            _segmentoNomenclatura.nomenclaturaReporteTrasladosDiarios(v_procesoImpresion.v_listaLineasImpresion);

            _line.printLinesSpace(v_procesoImpresion.v_listaLineasImpresion, 1);

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
