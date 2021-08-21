using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    internal class HelperEmbalaje
    {

        internal StringBuilder insertTablaEmbalaje(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._embalaje);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableEmbalaje._NO_CIA));
            _sb.Append(string.Format("{0}, ", TableEmbalaje._ARTICULO));
            _sb.Append(string.Format("{0}, ", TableEmbalaje._EMBALAJE));
            _sb.Append(TableEmbalaje._ORDEN);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["CIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["ARTICULO"]));
            _sb.Append(string.Format("{0}, ", pfila["EMBALAGE"]));
            _sb.Append(pfila["ORDEN"]);
            _sb.Append(")");

            return _sb;
        }

    }
}
