using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    public class HelperEstablecimiento
    {

        public StringBuilder insertTablaEstablecimiento(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._establecimiento);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableEstablecimiento._CODCLIENTE));
            _sb.Append(string.Format("{0}, ", TableEstablecimiento._CODESTABLECIMIENTO));
            _sb.Append(string.Format("{0}, ", TableEstablecimiento._DESCESTABLECIMIENTO));
            _sb.Append(string.Format("{0}, ", TableEstablecimiento._DIRECCION));
            _sb.Append(string.Format("{0}, ", TableEstablecimiento._CODLOCALIZACION));
            _sb.Append(TableEstablecimiento._DIRECCIONEXACTA);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["NO_CLIENTE"]));
            _sb.Append(string.Format("'{0}', ", pfila["NO_ESTABLECIMIENTO"]));
            _sb.Append(string.Format("'{0}', ", pfila["NOMBRE"]));
            _sb.Append(string.Format("'{0}', ", pfila["NO_AGENTE"]));
            _sb.Append(string.Format("'{0}', ", pfila["LOC"]));
            _sb.Append(string.Format("'{0}'", pfila["DIRECCION"]));
            _sb.Append(")");

            return _sb;
        }

    }
}
