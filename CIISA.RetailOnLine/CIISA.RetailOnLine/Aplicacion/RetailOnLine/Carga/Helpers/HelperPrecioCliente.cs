using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.TablesNAF;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    public class HelperPrecioCliente
    {

        public StringBuilder insertTablaPrecioCliente(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._precioCliente);
            _sb.Append("(");

            _sb.Append(string.Format("{0}, ", TablePrecioCliente._NO_CIA));
            _sb.Append(string.Format("{0}, ", TablePrecioCliente._NO_CLIENTE));
            _sb.Append(string.Format("{0}, ", TablePrecioCliente._ARTICULO));
            _sb.Append(string.Format("{0}, ", TablePrecioCliente._PRECIO_ESPECIAL));
            _sb.Append(string.Format("{0}", TablePrecioCliente._TIPO_MTO));
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["" + TablePrecioClienteNAF.NO_CIA + ""]));
            _sb.Append(string.Format("'{0}', ", pfila["" + TablePrecioClienteNAF.NO_CLIENTE + ""]));
            _sb.Append(string.Format("'{0}', ", pfila["" + TablePrecioClienteNAF.ARTICULO + ""]));
            _sb.Append(string.Format("'{0}', ", pfila["" + TablePrecioClienteNAF.PRECIO_ESPECIAL + ""]));
            _sb.Append(string.Format("'{0}' ", pfila["" + TablePrecioClienteNAF.TIPO_MTO + ""]));
            _sb.Append(")");

            return _sb;
        }

    }
}
