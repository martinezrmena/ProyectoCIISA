using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Helpers;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Print;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion
{
    public class ProcesoImpresion
    {
        internal string puertoImpresora;

        internal Printer v_impresora = null;

        internal List<string> v_listaLineasImpresion = new List<string>();

        internal ITaskActivity DPB = DependencyService.Get<ITaskActivity>();

        public ProcesoImpresion()
        {
            Logica_ManagerImpresora _manager = new Logica_ManagerImpresora();

            string _puertoImpresora = _manager.obtenerPuertoImpresora();

            _puertoImpresora = "AC3FA4A165F6";
            puertoImpresora = _puertoImpresora;

            v_impresora = new Printer(_puertoImpresora);
        }

        public bool ImpresoraFunciona()
        {
            return v_impresora.FuncionaImpresora();
        }

        public async Task imprimirReporteInventarioTeorico()
        {
            reporteInventarioTeorico _reporte = new reporteInventarioTeorico(this);

            await _reporte.imprimirReporteInventarioTeorico();
        }

        public async Task imprimirReporteCuentasBancarias()
        {
            reporteCuentasBancarias _reporte = new reporteCuentasBancarias(this);

            await _reporte.imprimirReporteCuentasBancarias();
        }

        public async Task imprimirReporteInventarioConExistencias(string ptituloReporte)
        {
            reporteInventarioConExistencias _reporte = new reporteInventarioConExistencias(this);

            await _reporte.imprimirReporteInventarioConExistencias(ptituloReporte);
        }

        public void imprimirTransaccion(Cliente pobjCliente,string pcodTransaction,string pcodTipoTransaccion,string pnomTipoTransaccion,bool preprint, bool DevolucionFactura, bool Tramitar)
        {
            impTransaccion _impresion = new impTransaccion(this, pobjCliente, pcodTransaction, pcodTipoTransaccion, pnomTipoTransaccion, preprint, DevolucionFactura, Tramitar);

            DPB.ImprimirTransaccion(_impresion);
        }

        public async Task imprimirReporteEnLineaTrasladosDiarios(DataTable pdt)
        {
            reporteEnLineaTrasladosDiarios _reporte = new reporteEnLineaTrasladosDiarios(this);

            await _reporte.imprimirReporteEnLineaTrasladosDiarios(pdt);
        }

        public async Task imprimirReporteIndicadoresFacturacion()
        {
            ReporteIndicadoresFacturacion _reporte = new ReporteIndicadoresFacturacion(this);

            await _reporte.imprimirReporteIndicadoresFacturacion();
        }

        public string VistaPreviaReporteIndicadoresFacturacion()
        {
            ReporteIndicadoresFacturacion _reporte = new ReporteIndicadoresFacturacion(this);

            return _reporte.VistaPreviaReporteIndicadoresFacturacion();
        }

        public async Task imprimirReporteVentasPorProducto()
        {
            ReporteVentasProducto _reporte = new ReporteVentasProducto(this);

            await _reporte.imprimirReporteVentasPorProducto();
        }

        public string VistaPreviaReporteVentasPorProducto()
        {
            ReporteVentasProducto _reporte = new ReporteVentasProducto(this);

            return _reporte.VisualizacionReporteVentasPorProducto();
        }

        public async Task imprimirReporteDocumentosRealizados(string pcodTipoDocumento)
        {
            ReporteDocumentosRealizados _reporte = new ReporteDocumentosRealizados(this);

            await _reporte.imprimirReporteDocumentosRealizados(pcodTipoDocumento);
        }

        public string VisualizarReporteDocumentosRealizados(string pcodTipoDocumento)
        {
            ReporteDocumentosRealizados _reporte = new ReporteDocumentosRealizados(this);

            return _reporte.VisualizarReporteDocumentosRealizados(pcodTipoDocumento);
        }

        public async Task imprimirReporteTramites()
        {
            ReporteTramite _reporte = new ReporteTramite(this);

            await _reporte.imprimirReporteTramites();
        }

        public string VisualizarReporteTramites()
        {
            ReporteTramite _reporte = new ReporteTramite(this);

            return _reporte.VisualizarReporteTramites();
        }

        public async Task imprimirReporteAnulaciones()
        {
            ReporteAnulaciones _reporte = new ReporteAnulaciones(this);
            await _reporte.imprimirReporteAnulaciones();
        }

        public string VisualizarReporteAnulaciones()
        {
            ReporteAnulaciones _reporte = new ReporteAnulaciones(this);
            return _reporte.VisualizarReporteAnulaciones();
        }

        public async Task imprimirReporteRecibosDinero(string pcodTipoDocumento)
        {
            ReporteRecibosDinero _reporte = new ReporteRecibosDinero(this);

            await _reporte.imprimirReporteRecibosDinero(pcodTipoDocumento);
        }

        public string VisualizarReporteRecibosDinero(string pcodTipoDocumento)
        {
            ReporteRecibosDinero _reporte = new ReporteRecibosDinero(this);

            return _reporte.VisualizarReporteRecibosDinero(pcodTipoDocumento);
        }

        public async Task imprimirReportePedidosSinAplicar()
        {
            ReportePedidosSinAplicar _reporte = new ReportePedidosSinAplicar(this);

            await _reporte.imprimirReportePedidosSinAplicar();
        }

        public string VisualizarReportePedidosSinAplicar()
        {
            ReportePedidosSinAplicar _reporte = new ReportePedidosSinAplicar(this);

            return _reporte.VisualizarReportePedidosSinAplicar();
        }

        public async Task imprimirReporteConsecutivoDocumentos()
        {
            ReporteConsecutivoDocumentos _reporte = new ReporteConsecutivoDocumentos(this);

            await _reporte.imprimirReporteConsecutivoDocumentos();
        }

        public string VisualizarReporteConsecutivoDocumentos()
        {
            ReporteConsecutivoDocumentos _reporte = new ReporteConsecutivoDocumentos(this);

            return _reporte.VisualizarReporteConsecutivoDocumentos();
        }

        public async Task imprimirReporteCrediticioDeLaRuta()
        {
            reporteCrediticioDeLaRuta _reporte = new reporteCrediticioDeLaRuta(this);

            await _reporte.imprimirReporteCrediticioDeLaRuta();
        }

        public string VisualizarReporteCrediticioDeLaRuta()
        {
            reporteCrediticioDeLaRuta _reporte = new reporteCrediticioDeLaRuta(this);

            return _reporte.VisualizarReporteCrediticioDeLaRuta();
        }

        public async Task imprimirReportePedidosRuta()
        {
            ReportePedidosRuta _reporte = new ReportePedidosRuta(this);

            await _reporte.imprimirReportePedidosRuta();

        }

        public string VisualizarReportePedidosRuta()
        {
            ReportePedidosRuta _reporte = new ReportePedidosRuta(this);

            return _reporte.VisualizarReportePedidosRuta();
        }

        public async Task imprimirReporteCrediticioDelCliente(Cliente pobjCliente)
        {
            reporteCrediticioDelCliente _reporte = new reporteCrediticioDelCliente(this);

            await _reporte.imprimirReporteCrediticioDelCliente(pobjCliente);
        }

        public string VisualizarReporteCrediticioDelCliente(Cliente pobjCliente)
        {
            reporteCrediticioDelCliente _reporte = new reporteCrediticioDelCliente(this);

            return _reporte.VisualizarReporteCrediticioDelCliente(pobjCliente);
        }

        public async Task imprimirReporteFlujoDinero(
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
            string ptotalADepositar)
        {
            reporteFlujoDinero _reporte = new reporteFlujoDinero(this);

            await _reporte.imprimirReporteFlujoDinero(pordenDeVenta, pfacturaContado, pfacturaCredito, pcotizacion, pdevolucion, pregalia, precaudacion, panulacion, preciboDinero, ptramite, ptotalEfectivo, ptotalCheque, ptotalTransferencia, ptotalDeposito, ptotalADepositar);
        }

        public async Task imprimirReporteEnLineaOrdenesVenta(DataTable pdt)
        {
            reporteEnLineaOrdenesVenta _reporte = new reporteEnLineaOrdenesVenta(this);

            await _reporte.imprimirReporteEnLineaOrdenesVenta(pdt);
        }

        public async Task imprimirReporteEnLineaDocumentos(DataTable pdt)
        {
            reporteEnLineaDocumentos _reporte = new reporteEnLineaDocumentos(this);

            await _reporte.imprimirReporteEnLineaDocumentos(pdt);
        }

        public async Task imprimirReporteEnLineaReciboRecaudacion(DataTable pdt)
        {
            reporteEnLineaReciboRecaudacion _reporte = new reporteEnLineaReciboRecaudacion(this);

            await _reporte.imprimirReporteEnLineaReciboRecaudacion(pdt);
        }

        public async Task imprimirReporteInventarioTomaFisica()
        {
            reporteInventarioTomaFisica _reporte = new reporteInventarioTomaFisica(this);

            await _reporte.imprimirReporteInventarioTomaFisica();
        }

        public async Task imprimirTransaccionAntiguedad(
            Cliente pobjCliente,
            string pcodTransaction,
            string pcodTipoTransaccion,
            string pnomTipoTransaccion,
            bool DevolucionFactura)
        {
            impTransaccionAntiguedad _impresion = new impTransaccionAntiguedad(this);

            await _impresion.imprimirTransaccionAntiguedad(
                pobjCliente,
                pcodTransaction,
                pcodTipoTransaccion,
                pnomTipoTransaccion,
                DevolucionFactura
                );
        }

        public async Task imprimirReporteInventarioAuditoria()
        {
            reporteInventarioAuditoria _reporteInventarioAuditoria = new reporteInventarioAuditoria(this);

            await _reporteInventarioAuditoria.imprimirReporteInventarioAuditoria();
        }

        public async Task<bool> HayImpresorasConectadas()
        {
            bool HayImpresora = false;

            HayImpresora = await v_impresora.ValidarImpresorasConectadas();

            return HayImpresora;
        }

        public string GenerarVistaPrevia()
        {
            StringBuilder sb = new StringBuilder();

            foreach (string Linea in v_listaLineasImpresion)
            {
                sb.AppendLine(Linea);
            }

            return sb.ToString();
        }
    }
}
