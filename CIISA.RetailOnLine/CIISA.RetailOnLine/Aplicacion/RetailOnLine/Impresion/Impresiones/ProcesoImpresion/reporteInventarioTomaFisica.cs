using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using CIISA.RetailOnLine.Framework.Handheld.Print;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion
{
    internal class reporteInventarioTomaFisica
    {
        private ProcesoImpresion v_procesoImpresion { get; set; }

        internal reporteInventarioTomaFisica(ProcesoImpresion pprocesoImpresion)
        {
            v_procesoImpresion = pprocesoImpresion;
        }

        internal async Task imprimirReporteInventarioTomaFisica()
        {
            await v_procesoImpresion.v_impresora.connect();

            SegmentoTitulo _segmentoTitulo = new SegmentoTitulo();

            _segmentoTitulo.buscarTituloFacturaPorTipoDocumento(
                v_procesoImpresion.v_listaLineasImpresion,
                ReportType._genericReport
                );

            Title _title = new Title();

            _title.titleGenericReport(v_procesoImpresion.v_listaLineasImpresion, "TOMA FISICA");

            Line _line = new Line();

            _line.simpleHypenLine(v_procesoImpresion.v_listaLineasImpresion);

            TimeDate _timeDate = new TimeDate();

            _timeDate.dateTimeDocument(v_procesoImpresion.v_listaLineasImpresion);

            SegmentoDatos _segmentoDatos = new SegmentoDatos();

            _segmentoDatos.datosAgente(v_procesoImpresion.v_listaLineasImpresion);

            _line.simpleHypenLine(v_procesoImpresion.v_listaLineasImpresion);

            SegmentoEncabezado _segmentoEncabezado = new SegmentoEncabezado();

            _segmentoEncabezado.encabezadoInventarioTomaFisica(v_procesoImpresion.v_listaLineasImpresion);

            _line.doubleHypenLine(v_procesoImpresion.v_listaLineasImpresion);

            Impresion_ManagerInventario _manager = new Impresion_ManagerInventario();

            _manager.buscarLineasInventarioTomaFisica(v_procesoImpresion.v_listaLineasImpresion);

            Copy _copy = new Copy();

            _copy.copies(v_procesoImpresion.v_listaLineasImpresion, 1, 1);

            _line.finalSpace(v_procesoImpresion.v_listaLineasImpresion);

            Print _print = new Print();

            await _print.print(
                v_procesoImpresion.v_listaLineasImpresion,
                v_procesoImpresion.v_impresora
                );

            v_procesoImpresion.v_impresora.disconnect();

            v_procesoImpresion.v_listaLineasImpresion = new List<string>();

            await LogMessageSuccess.printed();
        }
    }
}
