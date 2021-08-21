using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Time;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    public class HelperInventario
    {
        public StringBuilder insertTablaInventario(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._inventario);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableInventario._CODCIA));
            _sb.Append(string.Format("{0}, ", TableInventario._CODAGENTE));
            _sb.Append(string.Format("{0}, ", TableInventario._FECHATOMA));
            _sb.Append(string.Format("{0}, ", TableInventario._FECHATOMACONSOLIDA));
            _sb.Append(string.Format("{0}, ", TableInventario._CODPRODUCTO));
            _sb.Append(string.Format("{0}, ", TableInventario._CANTIDAD));
            _sb.Append(string.Format("{0}, ", TableInventario._VENTAS));
            _sb.Append(string.Format("{0}, ", TableInventario._DEVOLUCIONESBUENAS));
            _sb.Append(string.Format("{0}, ", TableInventario._DEVOLUCIONESMALAS));
            _sb.Append(string.Format("{0}, ", TableInventario._REGALIAS));
            _sb.Append(string.Format("{0}, ", TableInventario._ANULACIONES));
            _sb.Append(string.Format("{0}, ", TableInventario._ANULACIONESBUENAS));
            _sb.Append(string.Format("{0}, ", TableInventario._ANULACIONESMALAS));
            _sb.Append(string.Format("{0}, ", TableInventario._DISPONIBLE));
            _sb.Append(string.Format("{0}, ", TableInventario._FECHACREA));
            _sb.Append(string.Format("{0}, ", TableInventario._TOMA_FISICA));
            _sb.Append(string.Format("{0}, ", TableInventario._ENVIADO));
            _sb.Append(TableInventario._CONSOLIDADO);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["NO_CIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["NO_AGENTE"]));
            _sb.Append(string.Format("'{0}', ", VarTime.convertDateTimeFromServiceToSQLite(pfila["FECHA_TOMA"].ToString())));
            _sb.Append("DATE('NOW'), ");
            _sb.Append(string.Format("'{0}', ", pfila["NO_ARTI"]));
            _sb.Append(string.Format("'{0}', ", pfila["CANTIDAD"]));
            _sb.Append(string.Format("'{0}', ", Numeric._zeroInteger));
            _sb.Append(string.Format("'{0}', ", Numeric._zeroInteger));
            _sb.Append(string.Format("'{0}', ", Numeric._zeroInteger));
            _sb.Append(string.Format("'{0}', ", Numeric._zeroInteger));
            _sb.Append(string.Format("'{0}', ", Numeric._zeroInteger));
            _sb.Append(string.Format("'{0}', ", Numeric._zeroInteger));
            _sb.Append(string.Format("'{0}', ", Numeric._zeroInteger));
            _sb.Append(string.Format("'{0}', ", pfila["CANTIDAD"]));
            _sb.Append(string.Format("'{0}', ", VarTime.convertDateTimeFromServiceToSQLite(pfila["FECHA_CREA"].ToString())));
            _sb.Append(string.Format("'{0}', ", Numeric._zeroInteger));
            _sb.Append(string.Format("'{0}', ", SQL._No));
            _sb.Append(string.Format("'{0}' ", SQL._No));
            _sb.Append(")");

            return _sb;
        }

        public StringBuilder updateTablaInventario(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._inventario + " ");
            _sb.Append("SET ");
            _sb.Append("CANTIDAD = ");
            _sb.Append(string.Format("'{0}', ", pfila["CANTIDAD"]));
            _sb.Append("FECHACREA = ");
            _sb.Append(string.Format("'{0}', ", VarTime.convertDateTimeFromServiceToSQLite(pfila["FECHA_CREA"].ToString())));
            _sb.Append("ENVIADO = ");
            _sb.Append(string.Format("'{0}' ", SQL._No));
            _sb.Append("WHERE ");
            _sb.Append("CODCIA = ");
            _sb.Append(string.Format("'{0}' ", pfila["NO_CIA"]));
            _sb.Append("AND ");
            _sb.Append("CODAGENTE = ");
            _sb.Append(string.Format("'{0}' ", pfila["NO_AGENTE"]));
            _sb.Append("AND ");
            _sb.Append("CODPRODUCTO = ");
            _sb.Append(string.Format("'{0}'", pfila["NO_ARTI"]));

            return _sb;
        }
    }
}
