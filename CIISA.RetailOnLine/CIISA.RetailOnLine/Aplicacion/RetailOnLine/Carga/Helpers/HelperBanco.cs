using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    internal class HelperBanco
    {

        internal StringBuilder insertTablaBanco(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._banco);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableBanco._BANCO));
            _sb.Append(string.Format("{0}, ", TableBanco._DESCRIPCION));
            _sb.Append(string.Format("{0}, ", TableBanco._DESCRIP_RUTERO));
            _sb.Append(TableBanco._SIGLA);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["BANCO"]));
            _sb.Append(string.Format("'{0}', ", pfila["DESCRIPCION"]));
            _sb.Append(string.Format("'{0}', ", pfila["DESCRIP_RUTERO"]));
            _sb.Append(string.Format("'{0}'", pfila["ALIAS"]));
            _sb.Append(")");

            return _sb;
        }

    }
}
