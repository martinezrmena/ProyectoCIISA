using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionDocumento;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionReporte;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers
{
    internal class HelperEncabezadoRecibo
    {
        internal string buscarLineasEncabezado(string pcodTransaction,string pcodTipoTransaccion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableEncabezadoRecibo._OBSERVACION + ", ");
            _sb.Append(TableEncabezadoRecibo._MONTO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoRecibo + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableEncabezadoRecibo._NO_TRANSA + " = ");
            _sb.Append("'" + pcodTransaction + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoRecibo._TIPO_DOC + " = ");
            _sb.Append("'" + pcodTipoTransaccion + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoRecibo._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            string _lineaUno = string.Empty;

            foreach (DataRow _fila in _dt.Rows)
            {
                _lineaUno = string.Empty;

                Position _position = new Position();

                _lineaUno += _position.tabular(_lineaUno.Length, PosicionER._er_Concepto);
                _lineaUno += _fila[TableEncabezadoRecibo._OBSERVACION].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, PosicionER.monto);

                decimal _monto = FormatUtil.convertStringToDecimal(_fila[TableEncabezadoRecibo._MONTO].ToString());

                _lineaUno += FormatUtil.applyCurrencyFormat(_monto);
            }

            return _lineaUno;
        }

        internal void buscarLineasReporteRecibosDeDinero(string pcodTipoDocumento,List<string> pprintingLinesList)
        {
            #region REPORTES: Recaudación, Recibos de dinero
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableEncabezadoRecibo._NO_TRANSA + ", ");
            _sb.Append(TableEncabezadoRecibo._NO_CLIENTE + ", ");
            _sb.Append(TableEncabezadoRecibo._MONTO + ", ");
            _sb.Append(TableEncabezadoRecibo._ANULADO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoRecibo + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableEncabezadoRecibo._TIPO_DOC + " = ");
            _sb.Append("'" + pcodTipoDocumento + "' ");
            _sb.Append("ORDER BY " + TableEncabezadoRecibo._NO_TRANSA + " ASC");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            string _lineaUno = string.Empty;

            int _noLinea = 0;

            foreach (DataRow _fila in _dt.Rows)
            {
                Position _position = new Position();

                _lineaUno = string.Empty;

                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.codigo);
                _lineaUno += _fila[TableEncabezadoRecibo._NO_TRANSA].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.tipoDocumento);
                _lineaUno += pcodTipoDocumento;

                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.tramite);
                _lineaUno += PaymentForm._notApplyInitials;

                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.anulado);
                string _state = _fila[TableEncabezadoRecibo._ANULADO].ToString();

                if (_state.Equals(Indicators._S))
                {
                    _lineaUno += Indicators._Si;
                }
                else
                {
                    _lineaUno += Indicators._No;
                }

                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.codCliente);
                _lineaUno += _fila[TableEncabezadoRecibo._NO_CLIENTE].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.total);
                decimal _total = FormatUtil.convertStringToDecimal(_fila[TableEncabezadoRecibo._MONTO].ToString());

                _lineaUno += FormatUtil.applyCurrencyFormat(_total);

                _lineaUno += Environment.NewLine;

                Logica_ManagerCliente _manager = new Logica_ManagerCliente();
                string _lineaDos = string.Empty;

                _lineaDos += _position.tabular(_lineaDos.Length, RepFacturas.cliente);
                _lineaDos += _manager.buscarNombreClientePorCodigoCliente(
                                _fila[TableEncabezadoRecibo._NO_CLIENTE].ToString()
                                );

                _lineaDos += Environment.NewLine;

                _noLinea++;

                foreach (string singleline in Regex.Split(_lineaUno + _lineaDos, Environment.NewLine))
                {
                    pprintingLinesList.Add(singleline + Environment.NewLine);
                }

            }

            _lineaUno = string.Empty;

            _lineaUno += "No. Líneas: ";
            _lineaUno += _noLinea;
            _lineaUno += Simbol._point;
            _lineaUno += Environment.NewLine;

            foreach (string singleline in Regex.Split(_lineaUno, Environment.NewLine))
            {
                pprintingLinesList.Add(singleline + Environment.NewLine);
            }
            #endregion
        }
    }
}
