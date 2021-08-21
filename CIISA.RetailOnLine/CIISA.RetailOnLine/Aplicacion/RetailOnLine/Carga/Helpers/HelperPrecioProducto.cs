using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    public class HelperPrecioProducto
    {

        public StringBuilder insertTablaPrecioProducto(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._precioProducto);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TablePrecioProducto._CODLISTAPRECIO));
            _sb.Append(string.Format("{0}, ", TablePrecioProducto._CODPRODUCTO));
            _sb.Append(TablePrecioProducto._PRECIO);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", pfila["GRUPO_PRE"]));
            _sb.Append(string.Format("'{0}', ", pfila["ARTICULO"]));
            _sb.Append(pfila["PRECIO"]);
            _sb.Append(")");

            return _sb;
        }

    }
}
