using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    public class HelperListaPrecios
    {

        public StringBuilder insertTablaListaPrecios(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._listaPrecios);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableListaPrecios._NO_CIA));
            _sb.Append(string.Format("{0}, ", TableListaPrecios._GRUPO_PRE));
            _sb.Append(TableListaPrecios._DESCRIPCION);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", pfila["NO_CIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["GRUPO_PRE"]));
            _sb.Append(string.Format("'{0}'", pfila["DESCRIPCION"]));
            _sb.Append(")");

            return _sb;
        }

    }
}
