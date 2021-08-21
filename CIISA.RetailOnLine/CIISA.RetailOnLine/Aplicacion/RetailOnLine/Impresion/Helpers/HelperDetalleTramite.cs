using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionDocumento;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using System;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers
{
    internal class HelperDetalleTramite
    {
        internal string buscarLineasDetalle(string pcodTransaction, DateTime pdate)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableDetalleTramite._NO_CIA + ", ");
            _sb.Append(TableDetalleTramite._NO_TRANSA + ", ");
            _sb.Append(TableDetalleTramite._NO_LINEA + ", ");
            _sb.Append(TableDetalleTramite._NO_FACTURA + ", ");
            _sb.Append(TableDetalleTramite._MONTO + ", ");
            _sb.Append(TableDetalleTramite._ENVIADO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._detalleTramite + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableDetalleTramite._NO_TRANSA + " = ");
            _sb.Append("'" + pcodTransaction + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            string _lineasImpresion = string.Empty;
            int _numeroLineas = 0;

            decimal _monto = Numeric._zeroDecimalInitialize;

            foreach (DataRow _fila in _dt.Rows)
            {
                Position _position = new Position();

                string _lineaUno = string.Empty;

                _lineaUno += _position.tabular(_lineaUno.Length, Tramite.codDocumento);
                _lineaUno += _fila[TableDetalleTramite._NO_FACTURA].ToString();

                _monto = FormatUtil.convertStringToDecimal(_fila[TableDetalleTramite._MONTO].ToString());

                _lineaUno += _position.tabular(_lineaUno.Length, Tramite.monto);
                _lineaUno += FormatUtil.applyCurrencyFormat(_monto);

                _lineaUno += _position.tabular(_lineaUno.Length, Tramite.fecha);
                _lineaUno += VarTime.dateCR(pdate);

                _lineaUno += Environment.NewLine;

                _lineasImpresion += _lineaUno;

                _numeroLineas++;
            }

            _lineasImpresion += Environment.NewLine;
            _lineasImpresion += "Monto total trámite: ";
            _lineasImpresion += FormatUtil.applyCurrencyFormat(_monto);

            _lineasImpresion += Environment.NewLine;
            _lineasImpresion += Environment.NewLine;

            _lineasImpresion += "No. Líneas: ";
            _lineasImpresion += _numeroLineas;
            _lineasImpresion += Simbol._point;

            return _lineasImpresion;
        }
    }
}
