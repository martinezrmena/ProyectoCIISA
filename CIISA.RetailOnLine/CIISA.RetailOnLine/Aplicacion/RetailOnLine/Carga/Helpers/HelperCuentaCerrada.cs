using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Time;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    internal class HelperCuentaCerrada
    {

        internal StringBuilder insertTablaCuentaCerrada(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._cuentaCerrada);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableCuentaCerrada._NO_CIA));
            _sb.Append(string.Format("{0}, ", TableCuentaCerrada._NO_AGENTE));
            _sb.Append(string.Format("{0}, ", TableCuentaCerrada._NO_CLIENTE));
            _sb.Append(string.Format("{0}, ", TableCuentaCerrada._NOMBRE));
            _sb.Append(string.Format("{0}, ", TableCuentaCerrada._F_CIERRE));
            _sb.Append(TableCuentaCerrada._MOTIVO);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["NO_CIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["NO_AGENTE"]));
            _sb.Append(string.Format("'{0}', ", pfila["NO_CLIENTE"]));
            _sb.Append(string.Format("'{0}', ", pfila["NOMBRE"]));
            //_sb.Append(string.Format("'{0}', ", pfila["F_CIERRE"]));
            _sb.Append(string.Format("'{0}', ", VarTime.convertDateTimeFromServiceToSQLite(pfila["F_CIERRE"].ToString())));
            _sb.Append(string.Format("'{0}'", pfila["MOTIVO"]));
            _sb.Append(")");

            return _sb;
        }

    }
}
