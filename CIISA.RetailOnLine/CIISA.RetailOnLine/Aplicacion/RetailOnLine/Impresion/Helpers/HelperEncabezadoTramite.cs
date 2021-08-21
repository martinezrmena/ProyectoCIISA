using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionReporte;
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
    internal class HelperEncabezadoTramite
    {
        internal void buscarLineasReporteTramites(List<string> pprintingLinesList)
        {
            #region REPORTES: Tramites
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("ET." + TableEncabezadoTramite._NO_TRANSA + ", ");
            _sb.Append("ET." + TableEncabezadoTramite._NO_CLIENTE + ", ");
            _sb.Append("ET." + TableEncabezadoTramite._MONTO + ", ");
            _sb.Append("C." + TableCliente._NOMBRE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoTramite + " ET, ");
            _sb.Append(TablesROL._cliente + " C ");
            _sb.Append("WHERE ");
            _sb.Append("ET." + TableEncabezadoTramite._NO_CLIENTE + " = ");
            _sb.Append("C." + TableCliente._NO_CLIENTE + " ");
            _sb.Append("ORDER BY ET." + TableEncabezadoTramite._NO_TRANSA + " ASC");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            int _noLinea = 0;

            foreach (DataRow _fila in _dt.Rows)
            {
                Position _position = new Position();

                string _lineaUno = string.Empty;
                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.codigo);
                _lineaUno += _fila[TableEncabezadoTramite._NO_TRANSA].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.tipoDocumento);
                _lineaUno += ROLTransactions._tramiteSigla;

                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.tramite);
                _lineaUno += PaymentForm._notApplyInitials;

                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.anulado);
                _lineaUno += PaymentForm._notApplyInitials;

                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.codCliente);
                _lineaUno += _fila[TableEncabezadoTramite._NO_CLIENTE].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.total);
                decimal _total = FormatUtil.convertStringToDecimal(_fila[TableEncabezadoTramite._MONTO].ToString());

                _lineaUno += FormatUtil.applyCurrencyFormat(_total);

                _lineaUno += Environment.NewLine;

                string _lineaDos = string.Empty;
                _lineaDos += _position.tabular(_lineaDos.Length, RepFacturas.cliente);
                _lineaDos += _fila[TableCliente._NOMBRE].ToString();

                _lineaDos += Environment.NewLine;

                _noLinea++;


                foreach (string singleline in Regex.Split(_lineaUno + _lineaDos, Environment.NewLine))
                {
                    pprintingLinesList.Add(singleline + Environment.NewLine);
                }

            }

            string _lineaTres = string.Empty;

            _lineaTres += "No. Líneas: ";
            _lineaTres += _noLinea;
            _lineaTres += Simbol._point;
            _lineaTres += Environment.NewLine;

            foreach (string singleline in Regex.Split(_lineaTres, Environment.NewLine))
            {
                pprintingLinesList.Add(singleline + Environment.NewLine);
            }
            #endregion
        }

        internal DateTime buscarLineasDetalle(string pcodTransaction)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableEncabezadoTramite._FECHA_CREA + " ) " + TableEncabezadoTramite._FECHA_CREA + " ");
            //_sb.Append("CONVERT(NCHAR(10), " + TableEncabezadoTramite._FECHA_CREA + ", 103) " + TableEncabezadoTramite._FECHA_CREA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoTramite + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableEncabezadoTramite._NO_TRANSA + " = ");
            _sb.Append("'" + pcodTransaction + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            DateTime _fecha = new DateTime();

            foreach (DataRow _fila in _dt.Rows)
            {
                _fecha = FormatUtil.covertStringToDateTimeWithoutTime(_fila[TableEncabezadoTramite._FECHA_CREA].ToString());
            }

            return _fecha;
        }
    }
}
