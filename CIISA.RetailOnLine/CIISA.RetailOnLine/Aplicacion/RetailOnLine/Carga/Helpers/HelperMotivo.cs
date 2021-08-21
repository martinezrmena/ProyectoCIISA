using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    internal class HelperMotivo
    {

        internal StringBuilder insertTablaMotivo(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._motivo);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableMotivo._NO_CIA));
            _sb.Append(string.Format("{0}, ", TableMotivo._TIPO_DOC));
            _sb.Append(string.Format("{0}, ", TableMotivo._CODIGO));
            _sb.Append(TableMotivo._DESCRIPCION);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["NO_CIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["TIPO_DOC"]));
            _sb.Append(string.Format("'{0}', ", pfila["CODIGO"]));
            _sb.Append(string.Format("'{0}'", pfila["DESCRIPCION"]));
            _sb.Append(")");

            return _sb;
        }

    }
}
