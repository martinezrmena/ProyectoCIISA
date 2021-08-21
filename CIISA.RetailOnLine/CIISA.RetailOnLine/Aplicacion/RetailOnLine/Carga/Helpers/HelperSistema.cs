using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Time;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    public class HelperSistema
    {
        public StringBuilder insertTablaSistema(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._sistema);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableSistema._CODCIA));
            _sb.Append(string.Format("{0}, ", TableSistema._CODAGENTE));
            _sb.Append(string.Format("{0}, ", TableSistema._FECHA));
            _sb.Append(TableSistema._IND_CIERRE);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["NO_CIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["NO_AGENTE"]));
            //_sb.Append(string.Format("'{0}', ", pfila["FECHA"]));
            _sb.Append(string.Format("'{0}', ", VarTime.convertDateTimeFromServiceToSQLite(pfila["FECHA"].ToString())));
            _sb.Append(string.Format("'{0}'", pfila["IND_CIERRE"]));
            _sb.Append(")");

            return _sb;
        }
    }
}
