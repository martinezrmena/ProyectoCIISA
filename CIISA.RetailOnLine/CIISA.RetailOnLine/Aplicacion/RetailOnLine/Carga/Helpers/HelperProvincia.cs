using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    internal class HelperProvincia
    {

        internal StringBuilder insertTablaProvincia(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._provincia);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableProvincia._PAIS));
            _sb.Append(string.Format("{0}, ", TableProvincia._PROVINCIA));
            _sb.Append(TableProvincia._DESCRIPCION);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["PAIS"]));
            _sb.Append(string.Format("'{0}', ", pfila["PROVINCIA"]));
            _sb.Append(string.Format("'{0}'", pfila["DESCRIPCION"]));
            _sb.Append(")");

            return _sb;
        }

    }
}
