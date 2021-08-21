using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    internal class HelperCanton
    {

        internal StringBuilder insertTablaCanton(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._canton);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableCanton._PAIS));
            _sb.Append(string.Format("{0}, ", TableCanton._PROVINCIA));
            _sb.Append(string.Format("{0}, ", TableCanton._CANTON));
            _sb.Append(TableCanton._DESCRIPCION);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["PAIS"]));
            _sb.Append(string.Format("'{0}', ", pfila["PROVINCIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["CANTON"]));
            _sb.Append(string.Format("'{0}'", pfila["DESCRIPCION"]));
            _sb.Append(")");

            return _sb;
        }

    }
}
