using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperDetalleRecibo
    {
        internal void actualizarAnulado(TransaccionEncabezado pobjTransaccion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._detalleRecibo + " ");
            _sb.Append("SET ");
            _sb.Append("ANULADO = ");
            _sb.Append("'" + MiscUtils.getVariableStringSQLState(true) + "' ");
            _sb.Append("WHERE ");
            _sb.Append("NO_TRANSA = ");
            _sb.Append("'" + pobjTransaccion.v_codDocumento + "' ");
            _sb.Append("AND ");
            _sb.Append("TIPO_DOC = ");
            _sb.Append("'" + pobjTransaccion.v_objTipoDocumento.GetSigla() + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);
        }

        internal decimal obtenerPagosFactura(string pcodFactura)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("SUM(MONTO) MONTO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._detalleRecibo + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_FACTURA = ");
            _sb.Append("'" + pcodFactura + "' ");
            _sb.Append("AND ");
            _sb.Append("ANULADO = ");
            _sb.Append("'" + SQL._No + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readDecimal(_sb);
        }

        internal void guardarReciboDetalle(ListView ppnlAbono_ltvAbonos, Cliente pobjCliente)
        {
            int _numeroLinea = 1;

            var Source = ppnlAbono_ltvAbonos.ItemsSource as ObservableCollection<pnlAbono_ltvAbonos>;

            foreach (var _lvi in Source)
            {
                StringBuilder _sb = new StringBuilder();

                _sb.Append("INSERT ");
                _sb.Append("INTO ");
                _sb.Append(TablesROL._detalleRecibo);
                _sb.Append("(");
                _sb.Append(TableDetalleRecibo._NO_CIA + ", ");
                _sb.Append(TableDetalleRecibo._NO_TRANSA + ", ");
                _sb.Append(TableDetalleRecibo._NO_FACTURA + ", ");
                _sb.Append(TableDetalleRecibo._MONTO + ", ");
                _sb.Append(TableDetalleRecibo._TIPO_DOC + ", ");
                _sb.Append(TableDetalleRecibo._ENVIADO + ", ");
                _sb.Append(TableDetalleRecibo._ANULADO + ", ");
                _sb.Append(TableDetalleRecibo._NO_LINEA + ", ");
                _sb.Append(TableDetalleRecibo._FECHA_CREA + ", ");
                _sb.Append(TableDetalleRecibo._TIPO_TRANSA);
                _sb.Append(") ");
                _sb.Append("VALUES ");
                _sb.Append("(");
                _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany + "', ");
                _sb.Append("'" + pobjCliente.v_objTransaccion.v_codDocumento + "', ");
                _sb.Append("'" + _lvi.Factura + "', ");
                _sb.Append("REPLACE('" + _lvi.Abono + "',',',''), ");
                _sb.Append("'" + pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla() + "', ");
                _sb.Append("'" + SQL._No + "', ");
                _sb.Append("'" + SQL._No + "', ");
                _sb.Append("'" + _numeroLinea++ + "', ");
                _sb.Append("DATETIME('NOW', 'LOCALTIME'), ");
                _sb.Append("'" + _lvi.Abono + "'");
                _sb.Append(")");

                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                MultiGeneric.insertRecord(_sb);
            }
        }
    }
}
