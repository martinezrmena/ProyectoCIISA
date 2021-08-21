using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    internal class HelperDistrito
    {

        internal StringBuilder insertTablaDistrito(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._distrito);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableDistrito._PAIS));
            _sb.Append(string.Format("{0}, ", TableDistrito._PROVINCIA));
            _sb.Append(string.Format("{0}, ", TableDistrito._CANTON));
            _sb.Append(string.Format("{0}, ", TableDistrito._DISTRITO));
            _sb.Append(TableDistrito._DESCRIPCION);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["PAIS"]));
            _sb.Append(string.Format("'{0}', ", pfila["PROVINCIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["CANTON"]));
            _sb.Append(string.Format("'{0}', ", pfila["DISTRITO"]));
            _sb.Append(string.Format("'{0}'", pfila["DESCRIPCION"]));
            _sb.Append(")");

            return _sb;
        }

    }
}
