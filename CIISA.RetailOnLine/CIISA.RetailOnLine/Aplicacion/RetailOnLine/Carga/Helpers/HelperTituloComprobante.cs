using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    internal class HelperTituloComprobante
    {

        internal StringBuilder insertTablaTituloComprobante(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._tituloComprobante);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableTituloComprobante._NO_CIA));
            _sb.Append(string.Format("{0}, ", TableTituloComprobante._TIPO_DOC));
            _sb.Append(TableTituloComprobante._TITULO);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["NO_CIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["TIPO_DOC"]));
            _sb.Append(string.Format("'{0}'", pfila["TITULO"]));
            _sb.Append(")");

            return _sb;
        }

    }
}
