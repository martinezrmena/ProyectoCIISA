using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using CIISA.RetailOnLine.Framework.Handheld.Print;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion
{
    public class ReporteRecibosDinero
    {
        private ProcesoImpresion v_procesoImpresion { get; set; }

        public ReporteRecibosDinero(ProcesoImpresion pprocesoImpresion)
        {
            v_procesoImpresion = pprocesoImpresion;
        }

        public string VisualizarReporteRecibosDinero(string pcodTipoDocumento)
        {
            SegmentoTitulo _segmentoTitulo = new SegmentoTitulo();

            _segmentoTitulo.buscarTituloFacturaPorTipoDocumento(v_procesoImpresion.v_listaLineasImpresion, ReportType._genericReport);

            _segmentoTitulo.tituloReporte(v_procesoImpresion.v_listaLineasImpresion, pcodTipoDocumento);

            TimeDate _timeDate = new TimeDate();

            _timeDate.dateTimeDocument(v_procesoImpresion.v_listaLineasImpresion);

            Line _line = new Line();

            _line.simpleHypenLine(v_procesoImpresion.v_listaLineasImpresion);

            SegmentoDatos _segmentoDatos = new SegmentoDatos();

            _segmentoDatos.datosAgente(v_procesoImpresion.v_listaLineasImpresion);

            _line.simpleHypenLine(v_procesoImpresion.v_listaLineasImpresion);

            SegmentoEncabezado _segmentoEncabezado = new SegmentoEncabezado();

            _segmentoEncabezado.encabezadoReporteDocumentosRealizados(v_procesoImpresion.v_listaLineasImpresion);

            detallesRecibosDeDinero(pcodTipoDocumento);

            _line.printLinesSpace(v_procesoImpresion.v_listaLineasImpresion, 1);

            _line.simpleHypenLine(v_procesoImpresion.v_listaLineasImpresion);

            SegmentoNomenclatura _segmentoNomenclatura = new SegmentoNomenclatura();

            _segmentoNomenclatura.nomenclaturaReporteDocumentosRealizados(v_procesoImpresion.v_listaLineasImpresion);

            Copy _copy = new Copy();

            _copy.copies(v_procesoImpresion.v_listaLineasImpresion, 1, 1);

            _line.finalSpace(v_procesoImpresion.v_listaLineasImpresion);

            return v_procesoImpresion.GenerarVistaPrevia();
        }

        public async Task imprimirReporteRecibosDinero(string pcodTipoDocumento)
        {
            await v_procesoImpresion.v_impresora.connect();

            SegmentoTitulo _segmentoTitulo = new SegmentoTitulo();

            _segmentoTitulo.buscarTituloFacturaPorTipoDocumento(v_procesoImpresion.v_listaLineasImpresion, ReportType._genericReport);

            _segmentoTitulo.tituloReporte(v_procesoImpresion.v_listaLineasImpresion, pcodTipoDocumento);

            TimeDate _timeDate = new TimeDate();

            _timeDate.dateTimeDocument(v_procesoImpresion.v_listaLineasImpresion);

            Line _line = new Line();

            _line.simpleHypenLine(v_procesoImpresion.v_listaLineasImpresion);

            SegmentoDatos _segmentoDatos = new SegmentoDatos();

            _segmentoDatos.datosAgente(v_procesoImpresion.v_listaLineasImpresion);

            _line.simpleHypenLine(v_procesoImpresion.v_listaLineasImpresion);

            SegmentoEncabezado _segmentoEncabezado = new SegmentoEncabezado();

            _segmentoEncabezado.encabezadoReporteDocumentosRealizados(v_procesoImpresion.v_listaLineasImpresion);

            detallesRecibosDeDinero(pcodTipoDocumento);

            _line.printLinesSpace(v_procesoImpresion.v_listaLineasImpresion, 1);

            _line.simpleHypenLine(v_procesoImpresion.v_listaLineasImpresion);

            SegmentoNomenclatura _segmentoNomenclatura = new SegmentoNomenclatura();

            _segmentoNomenclatura.nomenclaturaReporteDocumentosRealizados(v_procesoImpresion.v_listaLineasImpresion);

            Copy _copy = new Copy();

            _copy.copies(v_procesoImpresion.v_listaLineasImpresion, 1, 1);

            _line.finalSpace(v_procesoImpresion.v_listaLineasImpresion);

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
                Directorios.reporteRecibosDinero
                );
        }

        private void detallesRecibosDeDinero(string pcodTipoDocumento)
        {
            Impresion_ManagerEncabezadoRecibo _manager = new Impresion_ManagerEncabezadoRecibo();

            _manager.buscarLineasReporteRecibosDeDinero(
               pcodTipoDocumento,
               v_procesoImpresion.v_listaLineasImpresion
               );
        }

    }
}
