using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Respaldo.Modelo
{
    public class Respaldo_ManagerRespaldar
    {
        private void respaldarInformacionDocumentoEncabezado(Editor ptextBox, Log plog)
        {
            plog.addLine(ptextBox, "1. DOCUMENTO ENCABEZADO");

            Respaldo_ManagerEncabezadoTransaccion _managerEncabezadoTransaccion = new Respaldo_ManagerEncabezadoTransaccion();

            _managerEncabezadoTransaccion.RespaldarEncabezadoTransaccion();

            //_managerEncabezadoTransaccion.RespaldarEncabezadoTransaccion();

            //_managerEncabezadoTransaccion.RespaldarEncabezadoTransaccion();

            OperationSQL.deleteTableFeedbackTextBox(
                TablesROL._encabezadoDocumento,
                ptextBox,
                plog
                );

            plog.addLine(ptextBox, "Se respaldo los documentos (OV, FT, FC, DV, P, RG).");
        }
        private void respaldarInformacionDocumentoDetalle(Editor ptextBox, Log plog)
        {
            plog.addLine(ptextBox, "1. DOCUMENTO DETALLE");

            Respaldo_ManagerDetalleTransaccion _managerDetalleTransaccion = new Respaldo_ManagerDetalleTransaccion();

            _managerDetalleTransaccion.RespaldarDetalleTransaccion();

            OperationSQL.deleteTableFeedbackTextBox(
                TablesROL._detalleDocumento,
                ptextBox,
                plog
                );

            plog.addLine(ptextBox, "Se respaldo los documentos (OV, FT, FC, DV, P, RG).");
        }
        private void respaldarInformacionReciboEncabezado(Editor ptextBox, Log plog)
        {
            plog.addLine(ptextBox, "2. RECIBO ENCABEZADO");

            Respaldo_ManagerEncabezadoRecibo _managerEncabezadoRecibo = new Respaldo_ManagerEncabezadoRecibo();

            _managerEncabezadoRecibo.RespaldarEncabezadoRecibo();

            OperationSQL.deleteTableFeedbackTextBox(
                 TablesROL._encabezadoRecibo,
                 ptextBox,
                 plog
                 );

            plog.addLine(ptextBox, "Se respaldo los documentos (RJ, RC).");
        }
        private void respaldarInformacionReciboDetalle(Editor ptextBox, Log plog)
        {
            plog.addLine(ptextBox, "2. RECIBO DETALLE");

            Respaldo_ManagerDetalleRecibo _managerDetalleRecibo = new Respaldo_ManagerDetalleRecibo();

            _managerDetalleRecibo.RespaldarDetalleRecibo();

            OperationSQL.deleteTableFeedbackTextBox(
                TablesROL._detalleRecibo,
                ptextBox,
                plog
                );

            plog.addLine(ptextBox, "Se respaldo los documentos (RJ, RC).");
        }
        private void respaldarInformacionTramiteEncabezado(Editor ptextBox, Log plog)
        {
            plog.addLine(ptextBox, "3. TRAMITE ENCABEZADO");

            Respaldo_ManagerEncabezadoTramite _managerEncabezadoTramite = new Respaldo_ManagerEncabezadoTramite();

            _managerEncabezadoTramite.RespaldarEncabezadoTramite();

            OperationSQL.deleteTableFeedbackTextBox(
                TablesROL._encabezadoTramite,
                ptextBox,
                plog
                );

            plog.addLine(ptextBox, "Se respaldo los documentos (TR).");
        }
        private void respaldarInformacionTramiteDetalle(Editor ptextBox, Log plog)
        {
            plog.addLine(ptextBox, "3. TRAMITE DETALLE");

            Respaldo_ManagerDetalleTramite _managerDetalleTramite = new Respaldo_ManagerDetalleTramite();

            _managerDetalleTramite.RespaldarDetalleTramite();

            OperationSQL.deleteTableFeedbackTextBox(
                TablesROL._detalleTramite,
                ptextBox,
                plog
                );

            plog.addLine(ptextBox, "Se respaldo los documentos (TR).");
        }
        private void respaldarInformacionAnulacionEncabezado(Editor ptextBox, Log plog)
        {
            plog.addLine(ptextBox, "4. ANULACION ENCABEZADO");

            Respaldo_ManagerEncabezadoAnulacion _managerEncabezadoAnulacion = new Respaldo_ManagerEncabezadoAnulacion();

            _managerEncabezadoAnulacion.RespaldarEncabezadoAnulacion();

            OperationSQL.deleteTableFeedbackTextBox(
                TablesROL._encabezadoAnulacion,
                ptextBox,
                plog
                );

            plog.addLine(ptextBox, "Se respaldo los documentos (AN).");
        }
        private void respaldarInformacionAnulacionDetalle(Editor ptextBox, Log plog)
        {
            plog.addLine(ptextBox, "4. ANULACION DETALLE");

            Respaldo_ManagerDetalleAnulacion _managerDetalleAnulacion = new Respaldo_ManagerDetalleAnulacion();

            _managerDetalleAnulacion.RespaldarDetalleAnulacion();

            OperationSQL.deleteTableFeedbackTextBox(
                TablesROL._detalleAnulacion,
                ptextBox,
                plog
                );

            plog.addLine(ptextBox, "Se respaldo los documentos (AN).");
        }
        private void respaldarInformacionPagoDocumento(Editor ptextBox, Log plog)
        {
            plog.addLine(ptextBox, "5. PAGO DOCUMENTO");

            Respaldo_ManagerPago _managerPago = new Respaldo_ManagerPago();

            _managerPago.RespaldarPagos();

            OperationSQL.deleteTableFeedbackTextBox(
                TablesROL._pagos,
                ptextBox,
                plog
                );

            plog.addLine(ptextBox, "Se respaldo las formas de pago (FC, FT).");
        }
        private void respaldarInformacionPagoRecibo(Editor ptextBox, Log plog)
        {
            plog.addLine(ptextBox, "5. PAGO RECIBO");

            Respaldo_ManagerPagoRecibo _managerPagoRecibo = new Respaldo_ManagerPagoRecibo();

            _managerPagoRecibo.RespaldarPagosRecibo();

            OperationSQL.deleteTableFeedbackTextBox(
                TablesROL._pagoRecibo,
                ptextBox,
                plog
                );

            plog.addLine(ptextBox, "Se respaldo las formas de pago (RJ, RC).");
        }
        private void respaldarInformacionRazonesNoVenta(Editor ptextBox, Log plog)
        {
            plog.addLine(ptextBox, "6. RAZONES NO VENTA");

            Respaldo_ManagerEncabezadoRazonesNV _managerEncabezadoRazoneNV = new Respaldo_ManagerEncabezadoRazonesNV();

            _managerEncabezadoRazoneNV.RespaldarEncabezadoRazonesNoVenta();

            OperationSQL.deleteTableFeedbackTextBox(
                TablesROL._encabezadoRazonesNV,
                ptextBox,
                plog
                );

            plog.addLine(ptextBox, "Se respaldo los documentos (RNV).");
        }
        private void respaldarInformacionInventario(Editor ptextBox, Log plog)
        {
            plog.addLine(ptextBox, "7. INVENTARIO");

            Respaldo_ManagerInventario _managerInventario = new Respaldo_ManagerInventario();

            _managerInventario.RespaldarInventario();

            OperationSQL.deleteTableFeedbackTextBox(
                TablesROL._inventario,
                ptextBox,
                plog
                );

            plog.addLine(ptextBox, "Se respaldo el inventario.");
        }
        private void respaldarInformacionDetalleReses(Editor ptextBox, Log plog)
        {
            plog.addLine(ptextBox, "7. DETALLE RESES");

            Respaldo_ManagerDetalleReses _managerDetalleReses= new Respaldo_ManagerDetalleReses();

            _managerDetalleReses.RespaldarDetalleReses();

            OperationSQL.deleteTableFeedbackTextBox(
                TablesROL._DetalleReses,
                ptextBox,
                plog
                );

            plog.addLine(ptextBox, "Se respaldo el detalle de reses.");
        }
        private void respaldarInformacionBitacora(Editor ptextBox, Log plog)
        {
            plog.addLine(ptextBox, "7. BITACORA");

            Respaldo_ManagerBitacora _managerBitacora = new Respaldo_ManagerBitacora();

            _managerBitacora.RespaldarBitacora();

            OperationSQL.deleteTableFeedbackTextBox(
                TablesROL._bitacora,
                ptextBox,
                plog
                );

            plog.addLine(ptextBox, "Se respaldo la bitacora.");
        }
        public void respaldarInformacion(Editor ptextBox, Log plog)
        {
            plog.addLine(ptextBox, "PROCESO DE RESPALDO DE INFORMACIÓN");

            respaldarInformacionDocumentoEncabezado(ptextBox, plog);

            respaldarInformacionDocumentoDetalle(ptextBox, plog);

            respaldarInformacionReciboEncabezado(ptextBox, plog);

            respaldarInformacionReciboDetalle(ptextBox, plog);

            respaldarInformacionTramiteEncabezado(ptextBox, plog);

            respaldarInformacionTramiteDetalle(ptextBox, plog);

            respaldarInformacionAnulacionEncabezado(ptextBox, plog);

            respaldarInformacionAnulacionDetalle(ptextBox, plog);

            respaldarInformacionPagoDocumento(ptextBox, plog);

            respaldarInformacionPagoRecibo(ptextBox, plog);

            respaldarInformacionRazonesNoVenta(ptextBox, plog);

            respaldarInformacionInventario(ptextBox, plog);

            respaldarInformacionDetalleReses(ptextBox, plog);

            respaldarInformacionBitacora(ptextBox, plog);

            plog.addLine(ptextBox, "FIN DEL PROCESO DE RESPALDO DE INFORMACIÓN");
        }
    }
}
