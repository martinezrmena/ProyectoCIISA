using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using CIISA.RetailOnLine.Framework.Handheld.Print;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion
{
    public class ReporteAnulaciones
    {
        private ProcesoImpresion v_procesoImpresion { get; set; }

        public ReporteAnulaciones(ProcesoImpresion pprocesoImpresion)
        {
            v_procesoImpresion = pprocesoImpresion;
        }

        internal string VisualizarReporteAnulaciones()
        {
            SegmentoTitulo _segmentoTitulo = new SegmentoTitulo();

            _segmentoTitulo.buscarTituloFacturaPorTipoDocumento(v_procesoImpresion.v_listaLineasImpresion, ReportType._genericReport);

            Title _title = new Title();

            _title.titleGenericReport(v_procesoImpresion.v_listaLineasImpresion, "ANULACIONES");

            TimeDate _timeDate = new TimeDate();

            _timeDate.dateTimeDocument(v_procesoImpresion.v_listaLineasImpresion);

            Line _line = new Line();

            _line.simpleHypenLine(v_procesoImpresion.v_listaLineasImpresion);

            SegmentoDatos _segmentoDatos = new SegmentoDatos();

            _segmentoDatos.datosAgente(v_procesoImpresion.v_listaLineasImpresion);

            _line.simpleHypenLine(v_procesoImpresion.v_listaLineasImpresion);

            SegmentoEncabezado _segmentoEncabezado = new SegmentoEncabezado();

            _segmentoEncabezado.encabezadoReporteDocumentosRealizados(v_procesoImpresion.v_listaLineasImpresion);

            detallesAnulaciones();

            _line.simpleHypenLine(v_procesoImpresion.v_listaLineasImpresion);

            SegmentoNomenclatura _segmentoNomenclatura = new SegmentoNomenclatura();

            _segmentoNomenclatura.nomenclaturaReporteDocumentosRealizados(v_procesoImpresion.v_listaLineasImpresion);

            Copy _copy = new Copy();

            _copy.copies(v_procesoImpresion.v_listaLineasImpresion, 1, 1);

            _line.finalSpace(v_procesoImpresion.v_listaLineasImpresion);

            return v_procesoImpresion.GenerarVistaPrevia();
        }

        internal async Task imprimirReporteAnulaciones()
        {
            await v_procesoImpresion.v_impresora.connect();

            SegmentoTitulo _segmentoTitulo = new SegmentoTitulo();

            _segmentoTitulo.buscarTituloFacturaPorTipoDocumento(v_procesoImpresion.v_listaLineasImpresion, ReportType._genericReport);

            Title _title = new Title();

            _title.titleGenericReport(v_procesoImpresion.v_listaLineasImpresion, "ANULACIONES");

            TimeDate _timeDate = new TimeDate();

            _timeDate.dateTimeDocument(v_procesoImpresion.v_listaLineasImpresion);

            Line _line = new Line();

            _line.simpleHypenLine(v_procesoImpresion.v_listaLineasImpresion);

            SegmentoDatos _segmentoDatos = new SegmentoDatos();

            _segmentoDatos.datosAgente(v_procesoImpresion.v_listaLineasImpresion);

            _line.simpleHypenLine(v_procesoImpresion.v_listaLineasImpresion);

            SegmentoEncabezado _segmentoEncabezado = new SegmentoEncabezado();

            _segmentoEncabezado.encabezadoReporteDocumentosRealizados(v_procesoImpresion.v_listaLineasImpresion);

            detallesAnulaciones();

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
                Directorios.reporteAnulaciones
                );
        }

        private void detallesAnulaciones()
        {
            Impresion_ManagerEncabezadoAnulacion _manager = new Impresion_ManagerEncabezadoAnulacion();

            _manager.buscarLineasReporteAnulaciones(
                v_procesoImpresion.v_listaLineasImpresion
                );
        }
    }
}
