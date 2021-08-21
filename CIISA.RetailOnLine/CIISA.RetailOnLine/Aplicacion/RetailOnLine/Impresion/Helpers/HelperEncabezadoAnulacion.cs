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
    internal class HelperEncabezadoAnulacion
    {
        internal void buscarLineasReporteAnulaciones(List<string> pprintingLinesList)
        {
            #region REPORTES: Anulaciones
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("EA." + TableEncabezadoAnulacion._NO_TRANSA + ", ");
            _sb.Append("EA." + TableEncabezadoAnulacion._NO_CLIENTE + ", ");
            _sb.Append("EA." + TableEncabezadoAnulacion._TOTAL + ", ");
            _sb.Append("C." + TableCliente._NOMBRE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoAnulacion + " EA, ");
            _sb.Append(TablesROL._cliente + " C ");
            _sb.Append("WHERE ");
            _sb.Append("EA." + TableEncabezadoAnulacion._NO_CLIENTE + " = ");
            _sb.Append("C." + TableCliente._NO_CLIENTE + " ");
            _sb.Append("ORDER BY EA." + TableEncabezadoAnulacion._TIPO_DOC + " ASC");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            int _noLinea = 0;
            string _lineaUno = string.Empty;

            foreach (DataRow _fila in _dt.Rows)
            {
                Position _position = new Position();

                string _nomCliente = string.Empty;
                _lineaUno = string.Empty;

                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.codigo);
                _lineaUno += _fila[TableEncabezadoAnulacion._NO_TRANSA].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.tipoDocumento);
                _lineaUno += ROLTransactions._anulacionSigla;

                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.tramite);
                _lineaUno += PaymentForm._notApplyInitials;

                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.anulado);
                _lineaUno += PaymentForm._notApplyInitials;

                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.codCliente);
                _lineaUno += _fila[TableEncabezadoAnulacion._NO_CLIENTE].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.total);
                decimal _total = FormatUtil.convertStringToDecimal(_fila[TableEncabezadoAnulacion._TOTAL].ToString());

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

            _lineaUno = string.Empty;

            _lineaUno += Environment.NewLine;
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
