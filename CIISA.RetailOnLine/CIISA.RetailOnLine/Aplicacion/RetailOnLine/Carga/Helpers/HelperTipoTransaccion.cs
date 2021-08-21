using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    internal class HelperTipoTransaccion
    {

        internal StringBuilder insertTablaTipoTransaccion(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._tipoTransaccion);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableTipoTransaccion._TIPOTRANSACCION));
            _sb.Append(TableTipoTransaccion._DESCRIPCION);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["TIPO"]));
            _sb.Append(string.Format("'{0}'", pfila["DESCRIPCION"]));
            _sb.Append(")");

            return _sb;
        }

    }
}
