using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionDocumento;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers
{
    internal class HelperPagoRecibo
    {
        internal string buscarLineasFormaPago(string pcodTransaction, string pcodTipoTransaccion)
        {
            int _numLinea = 1;

            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("PR." + TablePagoRecibo._NO_CIA + ", ");
            _sb.Append("PR." + TablePagoRecibo._NO_TRANSA + ", ");
            _sb.Append("PR." + TablePagoRecibo._NO_LINEA + ", ");
            _sb.Append("PR." + TablePagoRecibo._MONTO + ", ");
            _sb.Append("PR." + TablePagoRecibo._TIPO + ", ");
            _sb.Append("PR." + TablePagoRecibo._NO_FISICO + ", ");
            _sb.Append("PR." + TablePagoRecibo._SERIE + ", ");
            _sb.Append("PR." + TablePagoRecibo._FECHA_CREA + ", ");
            _sb.Append("PR." + TablePagoRecibo._BANCO + ", ");
            _sb.Append("PR." + TablePagoRecibo._TIPO_DOC + ", ");
            _sb.Append("PR." + TablePagoRecibo._ENVIADO + ", ");
            _sb.Append("PR." + TablePagoRecibo._ANULADO + ", ");
            _sb.Append("FP." + TableFormaPago._DESCRIPCION + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._pagoRecibo + " PR, ");
            _sb.Append(TablesROL._formaPago + " FP ");
            _sb.Append("WHERE ");
            _sb.Append("PR." + TablePagoRecibo._NO_TRANSA + " = ");
            _sb.Append("'" + pcodTransaction + "' ");
            _sb.Append("AND ");
            _sb.Append("PR." + TablePagoRecibo._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("PR." + TablePagoRecibo._TIPO_DOC + " = ");
            _sb.Append("'" + pcodTipoTransaccion + "' ");
            _sb.Append("AND ");
            _sb.Append("PR." + TablePagoRecibo._TIPO + " = ");
            _sb.Append("FP." + TableFormaPago._TIPO);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            Position _position = new Position();

            string _lineas = string.Empty;

            if (_dt.Rows.Count > 0)
            {
                decimal _total = Numeric._zeroDecimalInitialize;

                foreach (DataRow _fila in _dt.Rows)
                {
                    string _lineaUno = string.Empty;

                    _lineaUno += _position.tabular(_lineaUno.Length, PosicionFP.linea);
                    _lineaUno += _numLinea++;

                    _lineaUno += _position.tabular(_lineaUno.Length, PosicionFP.formaPago);
                    _lineaUno += _fila[TableFormaPago._DESCRIPCION].ToString();

                    _lineaUno += _position.tabular(_lineaUno.Length, PosicionFP.banco);
                    Logica_ManagerBanco _manager = new Logica_ManagerBanco();
                    _lineaUno += _manager.buscarSigla(_fila[TablePagoRecibo._BANCO].ToString());

                    _lineaUno += _position.tabular(_lineaUno.Length, PosicionFP.numero);
                    _lineaUno += _fila[TablePagoRecibo._NO_FISICO].ToString();

                    _lineaUno += _position.tabular(_lineaUno.Length, PosicionFP.monto);
                    decimal _monto = FormatUtil.convertStringToDecimal(_fila[TablePagoRecibo._MONTO].ToString());

                    _lineaUno += FormatUtil.applyCurrencyFormat(_monto);

                    _lineaUno += Environment.NewLine;

                    _lineas += _lineaUno;

                    _total = _total + _monto;
                }

                string _lineaDos = string.Empty;

                _lineaDos += _position.tabular(_lineaDos.Length, PosicionFP.numero);
                _lineaDos += "Total:";
                _lineaDos += _position.tabular(_lineaDos.Length, PosicionFP.monto);
                _lineaDos += FormatUtil.applyCurrencyFormat(_total);

                _lineaDos += Environment.NewLine;

                _lineas += _lineaDos;
            }
            else
            {
                string _lineaUno = string.Empty;

                _lineaUno += _position.tabular(_lineaUno.Length, PosicionFP.linea);
                _lineaUno += _numLinea;

                _lineaUno += _position.tabular(_lineaUno.Length, PosicionFP.formaPago);
                _lineaUno += PaymentForm._notApplyInitials;

                _lineaUno += _position.tabular(_lineaUno.Length, PosicionFP.banco);
                _lineaUno += PaymentForm._notApplyInitials;

                _lineaUno += _position.tabular(_lineaUno.Length, PosicionFP.numero);
                _lineaUno += PaymentForm._notApplyInitials;

                _lineaUno += _position.tabular(_lineaUno.Length, PosicionFP.monto);
                _lineaUno += PaymentForm._notApplyInitials;

                _lineaUno += Environment.NewLine;

                _lineas += _lineaUno;
            }

            return _lineas;
        }

        internal bool buscarTipoFormaPago(string pcodTransaction)
        {
            bool _copiaAdicional = false;

            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TablePagoRecibo._TIPO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._pagoRecibo + " ");
            _sb.Append("WHERE ");
            _sb.Append(TablePagoRecibo._NO_TRANSA + " = ");
            _sb.Append("'" + pcodTransaction + "' ");
            _sb.Append("AND ");
            _sb.Append(TablePagoRecibo._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            if (_dt.Rows.Count > 0)
            {
                foreach (DataRow _fila in _dt.Rows)
                {
                    string _tipoDocumento = _fila[TablePagoRecibo._TIPO].ToString();

                    if (_tipoDocumento.Equals(PaymentForm._checkInitials)
                        || _tipoDocumento.Equals(PaymentForm._depositInitials)
                        || _tipoDocumento.Equals(PaymentForm._transferInitials))
                    {
                        _copiaAdicional = true;
                    }
                }
            }

            return _copiaAdicional;
        }
    }
}
