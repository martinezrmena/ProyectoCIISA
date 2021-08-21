using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Time;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    internal class HelperFechaServidor
    {

        internal StringBuilder insertTablaFechaServidor(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._fechaServidor);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableFechaServidor._FECHA_SERVIDOR));
            _sb.Append(string.Format("{0}, ", TableFechaServidor._ANNO));
            _sb.Append(string.Format("{0}, ", TableFechaServidor._MES));
            _sb.Append(string.Format("{0}, ", TableFechaServidor._DIA));
            _sb.Append(string.Format("{0}, ", TableFechaServidor._HORA));
            _sb.Append(string.Format("{0}, ", TableFechaServidor._MINUTOS));
            _sb.Append(string.Format("{0}, ", TableFechaServidor._AMPM));
            _sb.Append(TableFechaServidor._ACTUALIZADA);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            //_sb.Append(string.Format("'{0}', ", pfila["FECHASERVIDOR"]));
            _sb.Append(string.Format("'{0}', ", VarTime.convertDateTimeFromServiceToSQLite(pfila["FECHASERVIDOR"].ToString())));
            _sb.Append(string.Format("'{0}', ", pfila["ANNO"]));
            _sb.Append(string.Format("'{0}', ", pfila["MES"]));
            _sb.Append(string.Format("'{0}', ", pfila["DIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["HORA"]));
            _sb.Append(string.Format("'{0}', ", pfila["MINUTOS"]));
            _sb.Append(string.Format("'{0}', ", pfila["AMPM"]));
            _sb.Append(string.Format("'{0}'", SQL._Si));
            _sb.Append(")");

            return _sb;
        }

    }
}
