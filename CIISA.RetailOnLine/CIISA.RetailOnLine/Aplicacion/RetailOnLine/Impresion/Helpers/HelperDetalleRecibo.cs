using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionDocumento;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using System;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers
{
    internal class HelperDetalleRecibo
    {
        internal string buscarLineasDetalle(string pcodTransaction, string pcodTipoTransaccion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(string.Format("DR.{0}, ", TableDetalleRecibo._NO_FACTURA));
            _sb.Append("DR." + TableDetalleRecibo._MONTO + ", ");
            _sb.Append("F." + TableFactura._SALDO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._detalleRecibo + " DR, ");
            _sb.Append(TablesROL._factura + " F ");
            _sb.Append("WHERE ");
            _sb.Append("DR." + TableDetalleRecibo._NO_TRANSA + " = ");
            _sb.Append("'" + pcodTransaction + "' ");
            _sb.Append("AND ");
            _sb.Append("DR." + TableDetalleRecibo._TIPO_DOC + " = ");
            _sb.Append("'" + pcodTipoTransaccion + "' ");
            _sb.Append("AND ");
            _sb.Append("DR." + TableDetalleRecibo._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("DR." + TableDetalleRecibo._NO_FACTURA + " = ");
            _sb.Append("F." + TableFactura._NO_FISICO + " ");
            _sb.Append("ORDER BY DR." + TableDetalleRecibo._NO_TRANSA + " ASC");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            StringBuilder _lineasImpresion = new StringBuilder();

            foreach (DataRow _fila in _dt.Rows)
            {
                Position _position = new Position();

                StringBuilder _lineaUno = new StringBuilder();

                _lineaUno.Append(_position.tabular(_lineaUno.Length, PosicionRD.factura));
                _lineaUno.Append(_fila[string.Format("{0}", TableDetalleRecibo._NO_FACTURA)].ToString());

                decimal _saldoPendiente = FormatUtil.convertStringToDecimal(_fila[string.Format("{0}", TableFactura._SALDO)].ToString());

                Logica_ManagerDetalleRecibo _manager = new Logica_ManagerDetalleRecibo();

                decimal _abonos = _manager.obtenerPagosFactura(_fila[string.Format("{0}", TableDetalleRecibo._NO_FACTURA)].ToString());
                decimal _esteAbono = FormatUtil.convertStringToDecimal(_fila[string.Format("{0}", TableDetalleRecibo._MONTO)].ToString());

                _lineaUno.Append(_position.tabular(_lineaUno.Length, PosicionRD.saldoFactura));

                DataTable _cantidadAbonos = buscarCantidadAbonosAFactura(
                                        _fila[string.Format("{0}",
                                        TableDetalleRecibo._NO_FACTURA)].ToString()
                                        );

                if (_cantidadAbonos.Rows.Count > 1)
                {
                    _lineaUno.Append(FormatUtil.applyCurrencyFormat(_saldoPendiente - (_abonos - _esteAbono)));
                }
                else
                {
                    _lineaUno.Append(FormatUtil.applyCurrencyFormat(_saldoPendiente));
                }

                _lineaUno.Append(_position.tabular(_lineaUno.Length, PosicionRD.abono));

                _lineaUno.Append(FormatUtil.applyCurrencyFormat(_esteAbono));

                _lineaUno.Append(_position.tabular(_lineaUno.Length, PosicionRD.saldoAbono));

                _lineaUno.Append(FormatUtil.applyCurrencyFormat(_saldoPendiente - _abonos));

                _lineasImpresion.Append(_lineaUno);

                _lineasImpresion.Append(Environment.NewLine);
            }

            return _lineasImpresion.ToString();
        }

        internal decimal buscarAbonosAnteriores(string pcodTransaction, string pcodFactura)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("SUM(" + TableDetalleRecibo._MONTO + ") " + TableDetalleRecibo._MONTO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._detalleRecibo + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableDetalleRecibo._NO_TRANSA + " = ");
            _sb.Append("'" + pcodTransaction + "' ");
            _sb.Append("AND ");
            _sb.Append(TableDetalleRecibo._NO_FACTURA + " = ");
            _sb.Append("'" + pcodFactura + "' ");
            _sb.Append("AND ");
            _sb.Append(TableDetalleRecibo._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readDecimal(_sb);
        }

        internal DataTable buscarCantidadAbonosAFactura(string pcodFactura)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableDetalleRecibo._MONTO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._detalleRecibo + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableDetalleRecibo._NO_FACTURA + " = ");
            _sb.Append("'" + pcodFactura + "' ");
            _sb.Append("AND ");
            _sb.Append(TableDetalleRecibo._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

    }
}
