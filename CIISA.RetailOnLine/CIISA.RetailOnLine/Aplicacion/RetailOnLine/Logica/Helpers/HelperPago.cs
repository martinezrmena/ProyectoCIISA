using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperPago
    {
        internal void actualizarAnulado(TransaccionEncabezado pobjTransaccion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._pagos + " ");
            _sb.Append("SET ");
            _sb.Append("ANULADO = ");
            _sb.Append("'" + MiscUtils.getVariableStringSQLState(true) + "' ");
            _sb.Append("WHERE ");
            _sb.Append("NO_TRANSA = ");
            _sb.Append("'" + pobjTransaccion.v_codDocumento + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);
        }

        internal void guardarPagoTransaccion(ListView ppnlFormaPago_ltvPagos, string pcodTransaction)
        {
            int _numeroLinea = 1;

            var Source = ppnlFormaPago_ltvPagos.ItemsSource as ObservableCollection<pnlFormaPago_ltvPagos>;

            if (Source.Count > 0)
            {
                foreach (var _lvi in Source)
                {
                    string _codBanco = string.Empty;
                    
                    if (!_lvi.Banco.Equals(PaymentForm._notApply))
                    {
                        Logica_ManagerBanco _managerBanco = new Logica_ManagerBanco();

                        _codBanco = _managerBanco.obtenerCodigoBanco(_lvi.Banco);
                    }

                    Logica_ManagerFormaPago _manager = new Logica_ManagerFormaPago();                    

                    string _codTipoPago = _manager.obtenerCodigoFormaPago(_lvi.FormaPago);
                    
                    if (_lvi.NumeroTransaccion.Equals(PaymentForm._notApply))
                    {
                        _lvi.NumeroTransaccion = string.Empty;
                    }                    

                    if (_lvi.Serie.Equals(PaymentForm._notApply))
                    {
                        _lvi.Serie = string.Empty;
                    }

                    StringBuilder _sb = new StringBuilder();

                    _sb.Append("INSERT ");
                    _sb.Append("INTO ");
                    _sb.Append(TablesROL._pagos + " ");
                    _sb.Append("(");
                    _sb.Append("NO_CIA, ");
                    _sb.Append("NO_TRANSA, ");
                    _sb.Append("NO_LINEA, ");
                    _sb.Append("MONTO, ");
                    _sb.Append("TIPO, ");
                    _sb.Append("NO_FISICO, ");
                    _sb.Append("SERIE, ");
                    _sb.Append("FECHA_CREA, ");
                    _sb.Append("BANCO, ");
                    _sb.Append("ENVIADO, ");
                    _sb.Append("ANULADO");
                    _sb.Append(") ");
                    _sb.Append("VALUES ");
                    _sb.Append(" (");
                    _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany + "', ");
                    _sb.Append("'" + pcodTransaction + "', ");
                    _sb.Append("'" + _numeroLinea + "', ");
                    _sb.Append("REPLACE('" + FormatUtil.convertStringToDecimal(_lvi.Monto) + "',',',''), ");
                    _sb.Append("'" + _codTipoPago + "', ");
                    _sb.Append("'" + _lvi.NumeroTransaccion + "', ");
                    _sb.Append("'" + _lvi.Serie + "', ");
                    _sb.Append("DATETIME('NOW', 'LOCALTIME'), ");
                    _sb.Append("'" + _codBanco + "', ");
                    _sb.Append("'" + SQL._No + "', ");
                    _sb.Append("'" + SQL._No + "'");
                    _sb.Append(")");

                    var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                    MultiGeneric.insertRecord(_sb);

                    _numeroLinea++;
                }
            }
            else
            {
                StringBuilder _sb = new StringBuilder();

                _sb.Append("INSERT ");
                _sb.Append("INTO ");
                _sb.Append(TablesROL._pagos + " ");
                _sb.Append("(");
                _sb.Append("NO_CIA, ");
                _sb.Append("NO_TRANSA, ");
                _sb.Append("NO_LINEA, ");
                _sb.Append("MONTO, ");
                _sb.Append("TIPO, ");
                _sb.Append("NO_FISICO, ");
                _sb.Append("SERIE, ");
                _sb.Append("FECHA_CREA, ");
                _sb.Append("BANCO, ");
                _sb.Append("ENVIADO, ");
                _sb.Append("ANULADO");
                _sb.Append(") ");
                _sb.Append("VALUES ");
                _sb.Append(" (");
                _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany + "', ");
                _sb.Append("'" + pcodTransaction + "', ");
                _sb.Append("'" + _numeroLinea + "', ");
                _sb.Append("REPLACE('" + 0 + "',',',''), ");
                _sb.Append("'EF', ");
                _sb.Append("'" + string.Empty + "', ");
                _sb.Append("'" + string.Empty + "', ");
                _sb.Append("DATE('NOW'), ");
                _sb.Append("'" + string.Empty + "', ");
                _sb.Append("'" + SQL._No + "', ");
                _sb.Append("'" + SQL._No + "'");
                _sb.Append(")");

                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                MultiGeneric.insertRecord(_sb);

                _numeroLinea++;
            }
        }

        internal decimal obtenerMontoPorTipoPago(string ptipoPago)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("SUM(MONTO) MONTO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._pagos + " ");
            _sb.Append("WHERE ");
            _sb.Append("TIPO = ");
            _sb.Append("'" + ptipoPago + "' ");
            _sb.Append("AND ");
            _sb.Append("ANULADO = ");
            _sb.Append("'" + SQL._No + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readDecimal(_sb);
        }
    }
}
