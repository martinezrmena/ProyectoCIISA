using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Print;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion
{
    public class ReporteConsecutivoDocumentos
    {
        private ProcesoImpresion v_procesoImpresion { get; set; }

        public ReporteConsecutivoDocumentos(ProcesoImpresion pprocesoImpresion)
        {
            v_procesoImpresion = pprocesoImpresion;
        }

        internal string VisualizarReporteConsecutivoDocumentos()
        {
            SegmentoTitulo _segmentoTitulo = new SegmentoTitulo();

            _segmentoTitulo.buscarTituloFacturaPorTipoDocumento(
                v_procesoImpresion.v_listaLineasImpresion,
                ReportType._genericReport
                );

            Title _title = new Title();

            _title.titleGenericReport(
                v_procesoImpresion.v_listaLineasImpresion,
                "CONSECUTIVO DOCUMENTOS"
                );

            TimeDate _timeDate = new TimeDate();

            _timeDate.dateTimeDocument(v_procesoImpresion.v_listaLineasImpresion);

            Line _line = new Line();

            _line.simpleHypenLine(
                v_procesoImpresion.v_listaLineasImpresion
                );

            SegmentoDatos _segmentoDatos = new SegmentoDatos();

            _segmentoDatos.datosAgente(v_procesoImpresion.v_listaLineasImpresion);

            _line.simpleHypenLine(
                v_procesoImpresion.v_listaLineasImpresion
                );

            SegmentoEncabezado _segmentoEncabezado = new SegmentoEncabezado();

            _segmentoEncabezado.encabezadoReporteConsecutivoDocumentos(
                v_procesoImpresion.v_listaLineasImpresion
                );

            detallesConsecutivoDocumentos(
                Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent
                );

            _line.simpleHypenLine(
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

            _line.finalSpace(v_procesoImpresion.v_listaLineasImpresion
                );

            return v_procesoImpresion.GenerarVistaPrevia();
        }

        internal async Task imprimirReporteConsecutivoDocumentos()
        {
            await v_procesoImpresion.v_impresora.connect();

            SegmentoTitulo _segmentoTitulo = new SegmentoTitulo();

            _segmentoTitulo.buscarTituloFacturaPorTipoDocumento(
                v_procesoImpresion.v_listaLineasImpresion,
                ReportType._genericReport
                );

            Title _title = new Title();

            _title.titleGenericReport(
                v_procesoImpresion.v_listaLineasImpresion,
                "CONSECUTIVO DOCUMENTOS"
                );

            TimeDate _timeDate = new TimeDate();

            _timeDate.dateTimeDocument(v_procesoImpresion.v_listaLineasImpresion);

            Line _line = new Line();

            _line.simpleHypenLine(
                v_procesoImpresion.v_listaLineasImpresion
                );

            SegmentoDatos _segmentoDatos = new SegmentoDatos();

            _segmentoDatos.datosAgente(v_procesoImpresion.v_listaLineasImpresion);

            _line.simpleHypenLine(
                v_procesoImpresion.v_listaLineasImpresion
                );

            SegmentoEncabezado _segmentoEncabezado = new SegmentoEncabezado();

            _segmentoEncabezado.encabezadoReporteConsecutivoDocumentos(
                v_procesoImpresion.v_listaLineasImpresion
                );

            detallesConsecutivoDocumentos(
                Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent
                );

            _line.simpleHypenLine(
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

            _line.finalSpace(v_procesoImpresion.v_listaLineasImpresion
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
                Directorios.reporteConsecutivoDocumentos
                );
        }

        private void detallesConsecutivoDocumentos(string pcodAgente)
        {
            Line _line = new Line();

            Impresion_ManagerAgenteVendedor _manager = new Impresion_ManagerAgenteVendedor();

            _line.printingLinesList(
                v_procesoImpresion.v_listaLineasImpresion,
                _manager.buscarLineasReporteConsecutivoDocumentos(
                    pcodAgente
                    ),
                0);
        }

    }
}
