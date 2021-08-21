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
    internal class HelperPago
    {
        internal string buscarLineasFormaPago(string pcodTransaction)
        {
            StringBuilder _lineas = new StringBuilder();

            int _numLinea = 1;

            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("P." + TablePagos._NO_CIA + ", ");
            _sb.Append("P." + TablePagos._NO_TRANSA + ", ");
            _sb.Append("P." + TablePagos._NO_LINEA + ", ");
            _sb.Append("P." + TablePagos._MONTO + ", ");
            _sb.Append("P." + TablePagos._TIPO + ", ");
            _sb.Append("P." + TablePagos._NO_FISICO + ", ");
            _sb.Append("P." + TablePagos._SERIE + ", ");
            _sb.Append("P." + TablePagos._FECHA_CREA + ", ");
            _sb.Append("P." + TablePagos._BANCO + ", ");
            _sb.Append("P." + TablePagos._ENVIADO + ", ");
            _sb.Append("P." + TablePagos._ANULADO + ", ");
            _sb.Append("FP." + TableFormaPago._DESCRIPCION + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._pagos + " P, ");
            _sb.Append(TablesROL._formaPago + " FP ");
            _sb.Append("WHERE ");
            _sb.Append("P." + TablePagos._NO_TRANSA + " = ");
            _sb.Append("'" + pcodTransaction + "' ");
            _sb.Append("AND ");
            _sb.Append("P." + TablePagos._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("P." + TablePagos._TIPO + " = ");
            _sb.Append("FP." + TableFormaPago._TIPO);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            Position _position = new Position();

            if (_dt.Rows.Count > 0)
            {
                foreach (DataRow _fila in _dt.Rows)
                {
                    StringBuilder _line = new StringBuilder();

                    _line.Append(_position.tabular(_line.Length, PosicionFP.linea));
                    _line.Append(_numLinea);

                    _line.Append(_position.tabular(_line.Length, PosicionFP.formaPago));
                    _line.Append(_fila[string.Format("{0}", TableFormaPago._DESCRIPCION)].ToString());

                    if (_fila[string.Format("{0}", TablePagos._TIPO)].ToString().Equals(PaymentForm._cashInitials))
                    {
                        _line.Append(_position.tabular(_line.Length, PosicionFP.banco));
                        _line.Append(PaymentForm._notApplyInitials);

                        _line.Append(_position.tabular(_line.Length, PosicionFP.numero));
                        _line.Append(PaymentForm._notApplyInitials);
                    }
                    else
                    {
                        _line.Append(_position.tabular(_line.Length, PosicionFP.banco));
                        Logica_ManagerBanco _manager = new Logica_ManagerBanco();
                        _line.Append(_manager.buscarSigla(_fila[string.Format("{0}", TablePagos._BANCO)].ToString()));

                        _line.Append(_position.tabular(_line.Length, PosicionFP.numero));
                        _line.Append(_fila[string.Format("{0}", TablePagos._NO_FISICO)].ToString());
                    }

                    _line.Append(_position.tabular(_line.Length, PosicionFP.monto));
                    decimal _monto = FormatUtil.convertStringToDecimal(_fila[TablePagos._MONTO].ToString());

                    _line.Append(FormatUtil.applyCurrencyFormat(_monto));

                    _line.Append(Environment.NewLine);

                    _numLinea++;
                    _lineas.Append(_line);
                }
            }
            else
            {
                StringBuilder _line = new StringBuilder();

                _line.Append(_position.tabular(_line.Length, PosicionFP.linea));
                _line.Append(Numeric._oneInteger);

                _line.Append(_position.tabular(_line.Length, PosicionFP.formaPago));
                _line.Append(PaymentForm._notApplyInitials);

                _line.Append(_position.tabular(_line.Length, PosicionFP.banco));
                _line.Append(PaymentForm._notApplyInitials);

                _line.Append(_position.tabular(_line.Length, PosicionFP.numero));
                _line.Append(PaymentForm._notApplyInitials);

                _line.Append(_position.tabular(_line.Length, PosicionFP.monto));
                _line.Append(PaymentForm._notApplyInitials);

                _line.Append(Environment.NewLine);

                _lineas.Append(_line);
            }

            return _lineas.ToString();
        }

        internal bool buscarTipoFormaPago(string pcodTransaction)
        {
            bool _copiaAdicional = false;

            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TablePagos._TIPO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._pagos + " ");
            _sb.Append("WHERE ");
            _sb.Append(TablePagos._NO_TRANSA + " = ");
            _sb.Append("'" + pcodTransaction + "' ");
            _sb.Append("AND ");
            _sb.Append(TablePagos._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            if (_dt.Rows.Count > 0)
            {
                foreach (DataRow _fila in _dt.Rows)
                {
                    string _tipoDocumento = _fila[TablePagos._TIPO].ToString();

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
