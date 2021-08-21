using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using CIISA.RetailOnLine.Framework.Handheld.Print;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion
{
    public class ReportePedidosSinAplicar
    {
        private ProcesoImpresion v_procesoImpresion { get; set; }

        public ReportePedidosSinAplicar(ProcesoImpresion pprocesoImpresion)
        {
            v_procesoImpresion = pprocesoImpresion;
        }

        internal string VisualizarReportePedidosSinAplicar()
        {
            SegmentoTitulo _segmentoTitulo = new SegmentoTitulo();

            _segmentoTitulo.buscarTituloFacturaPorTipoDocumento(
                v_procesoImpresion.v_listaLineasImpresion,
                ReportType._genericReport
                );

            Title _title = new Title();

            _title.titleGenericReport(
                v_procesoImpresion.v_listaLineasImpresion,
                "PEDIDOS SIN APLICAR"
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

            _segmentoEncabezado.encabezadoReportePedidosRuta(
                v_procesoImpresion.v_listaLineasImpresion
                );

            detallesPedidosSinAplicar();

            _line.printLinesSpace(
                v_procesoImpresion.v_listaLineasImpresion,
                1
                );

            _line.simpleHypenLine(
                v_procesoImpresion.v_listaLineasImpresion
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

            return v_procesoImpresion.GenerarVistaPrevia();
        }

        internal async Task imprimirReportePedidosSinAplicar()
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
                "PEDIDOS SIN APLICAR"
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

            _segmentoEncabezado.encabezadoReportePedidosRuta(
                v_procesoImpresion.v_listaLineasImpresion
                );

            detallesPedidosSinAplicar();

            _line.printLinesSpace(
                v_procesoImpresion.v_listaLineasImpresion,
                1
                );

            _line.simpleHypenLine(
                v_procesoImpresion.v_listaLineasImpresion
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
                Directorios.reportePedidosSinAplicarArchivo
                );
        }

        private void detallesPedidosSinAplicar()
        {
            Impresion_ManagerEncabezadoPedido _manager = new Impresion_ManagerEncabezadoPedido();

            _manager.buscarEncabezadosPedidosSinAplicar(
                v_procesoImpresion.v_listaLineasImpresion
                );
        }
    
    }
}
