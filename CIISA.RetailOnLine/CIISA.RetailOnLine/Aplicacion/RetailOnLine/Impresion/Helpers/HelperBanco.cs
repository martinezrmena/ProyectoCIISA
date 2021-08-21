using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionReporte;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers
{
    internal class HelperBanco
    {

        internal void buscarLineasCuentasBancarias(
            List<string> pprintingLinesList
            )
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableBanco._BANCO + ", ");
            _sb.Append(TableBanco._DESCRIPCION + ", ");
            _sb.Append(TableBanco._DESCRIP_RUTERO + ", ");
            _sb.Append(TableBanco._SIGLA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._banco);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            int _cantLineas = 0;

            foreach (DataRow _fila in _dt.Rows)
            {
                Position _position = new Position();

                string _lineaUno = string.Empty;
                _lineaUno += _position.tabular(_lineaUno.Length, RepCuentasBancarias.sigla);
                _lineaUno += _fila[TableBanco._SIGLA].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepCuentasBancarias.banco);
                _lineaUno += _fila[TableBanco._DESCRIPCION].ToString();
                _lineaUno += Environment.NewLine;
                //_lineaUno += "\\&";

                string _lineaDos = string.Empty;
                _lineaDos += _position.tabular(_lineaDos.Length, RepCuentasBancarias.banco);
                _lineaDos += _fila[TableBanco._DESCRIP_RUTERO].ToString();
                _lineaDos += Environment.NewLine;
                //_lineaDos += "\\&";

                _cantLineas++;

                pprintingLinesList.Add(_lineaUno);
                pprintingLinesList.Add(_lineaDos);
                pprintingLinesList.Add(Environment.NewLine);
                //pprintingLinesList.Add("\\&");
            }

            pprintingLinesList.Add(Environment.NewLine +
                "No. Líneas: " +
                _cantLineas +
                Environment.NewLine);
                //"\\&");
        }

    }
}
