using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    internal class HelperSugerido
    {

        internal StringBuilder insertTablaSugerido(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._sugerido);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableSugerido._NO_CIA));
            _sb.Append(string.Format("{0}, ", TableSugerido._NO_AGENTE));
            _sb.Append(string.Format("{0}, ", TableSugerido._NO_CLIENTE));
            _sb.Append(string.Format("{0}, ", TableSugerido._CODPRODUCTO));
            _sb.Append(TableSugerido._CANTIDAD);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["CIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["COD_AGENTE"]));
            _sb.Append(string.Format("'{0}', ", pfila["COD_CLIENTE"]));
            _sb.Append(string.Format("'{0}', ", pfila["ARTICULO"]));
            _sb.Append(string.Format("'{0}'", pfila["CANTIDAD"]));
            _sb.Append(")");

            return _sb;
        }

    }
}
