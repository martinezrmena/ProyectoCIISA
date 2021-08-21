using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Helpers
{
    internal class HelperEncabezadoTransaccion
    {

        private StringBuilder sentenciaBuscarEncabezadoDocumento()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableEncabezadoDocumento._CODCIA + ", ");
            _sb.Append(TableEncabezadoDocumento._CODDOCUMENTO + ", ");
            _sb.Append(TableEncabezadoDocumento._CODAGENTE + ", ");
            _sb.Append(TableEncabezadoDocumento._CODCLIENTE + ", ");
            _sb.Append(TableEncabezadoDocumento._CODESTABLECIMIENTO + ", ");
            _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + ", ");
            _sb.Append(TableEncabezadoDocumento._FECHACREACION + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableEncabezadoDocumento._FECHAENTREGA + " ) " + TableEncabezadoDocumento._FECHAENTREGA + ", ");
            _sb.Append("REPLACE(" + TableEncabezadoDocumento._TOTAL + ",',','.') " + TableEncabezadoDocumento._TOTAL + ", ");
            _sb.Append("REPLACE(" + TableEncabezadoDocumento._TOTAL_IMP + ",',','.') " + TableEncabezadoDocumento._TOTAL_IMP + ", ");
            _sb.Append(TableEncabezadoDocumento._CEDULA + ", ");
            _sb.Append(TableEncabezadoDocumento._NOMBRE + ", ");
            _sb.Append("REPLACE(" + TableEncabezadoDocumento._MONTO_DEVOL + ",',','.') " + TableEncabezadoDocumento._MONTO_DEVOL + ", ");
            _sb.Append(TableEncabezadoDocumento._NUMERO_DEVOL + ", ");
            _sb.Append("REPLACE(" + TableEncabezadoDocumento._MONTO_DESCUENTO + ",',','.') " + TableEncabezadoDocumento._MONTO_DESCUENTO + ", ");
            _sb.Append(TableEncabezadoDocumento._CLIENTE_NUEVO + ", ");
            _sb.Append(TableEncabezadoDocumento._NUEVO_CLIENTE + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableEncabezadoDocumento._FECHA_TOMA + " ) " + TableEncabezadoDocumento._FECHA_TOMA + ", ");
            _sb.Append("REPLACE(" + TableEncabezadoDocumento._LATITUD + ",',','.') " + TableEncabezadoDocumento._LATITUD + ", ");
            _sb.Append("REPLACE(" + TableEncabezadoDocumento._LONGITUD + ",',','.') " + TableEncabezadoDocumento._LONGITUD + ", ");

            #region Factura Electronica
            _sb.Append(TableEncabezadoDocumento._NUM_ORDEN + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableEncabezadoDocumento._FECHA_ORDEN + " ) " + TableEncabezadoDocumento._FECHA_ORDEN + ", ");
            _sb.Append(TableEncabezadoDocumento._NUM_RECEPCION + ", ");
            _sb.Append(TableEncabezadoDocumento._NUM_RECLAMO + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableEncabezadoDocumento._FECHA_RECLAMO + " ) " + TableEncabezadoDocumento._FECHA_RECLAMO + ", ");
            _sb.Append(TableEncabezadoDocumento._COD_PROVEEDOR + ", ");
            _sb.Append(TableEncabezadoDocumento._CODFACTURA + ", ");
            _sb.Append(TableEncabezadoDocumento._CODPEDIDO + " ");
            #endregion

            _sb.Append("FROM ");

            return _sb;
        }

        internal DataTable buscarEncabezadosDocumentosSinEnviar_SincronizacionAutomatica(TransaccionEncabezado pobjTransaccion, string TipoTransaccion)
        {
            StringBuilder _sb = sentenciaBuscarEncabezadoDocumento();

            _sb.Append(TablesROL._encabezadoDocumento + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableEncabezadoDocumento._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoDocumento._ENVIADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoDocumento._CODDOCUMENTO + " != ");
            _sb.Append("'" + pobjTransaccion.v_codDocumento + "'");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("'" + TipoTransaccion + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            var DTSpecialFormat = DependencyService.Get<IFillDataSet_Table>();

            return DTSpecialFormat.DataTable_Format_EncDoc(MultiGeneric.uploadDataTable(_sb));
        }

        internal DataTable buscarEncabezadosDocumentosSinEnviar(bool ptodosDocumentos, string ptipoDescarga)
        {
            StringBuilder _sb;

            _sb = sentenciaBuscarEncabezadoDocumento();

            if (ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append(TablesROL._encabezadoDocumentoBK + " ");
            }
            else
            {
                _sb.Append(TablesROL._encabezadoDocumento + " ");
            }

            _sb.Append("WHERE ");
            _sb.Append(TableEncabezadoDocumento._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");

            if (!ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append("AND ");
                _sb.Append(TableEncabezadoDocumento._ENVIADO + " = ");
                _sb.Append("'" + SQL._No + "' ");

                if (!ptodosDocumentos)
                {
                    _sb.Append("AND ");
                    _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
                    _sb.Append("'" + ROLTransactions._ordenVentaSigla + "'");
                }
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            var DTSpecialFormat = DependencyService.Get<IFillDataSet_Table>();

            return DTSpecialFormat.DataTable_Format_EncDoc(MultiGeneric.uploadDataTable(_sb));
        }
    }
}
