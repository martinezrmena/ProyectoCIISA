using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    public class HelperVisita
    {

        public StringBuilder insertTablaVisita(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._visitas);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableVisitas._CODRUTA));
            _sb.Append(string.Format("{0}, ", TableVisitas._CODCIA));
            _sb.Append(string.Format("{0}, ", TableVisitas._DIA));
            _sb.Append(string.Format("{0}, ", TableVisitas._CODCLIENTE));
            _sb.Append(string.Format("{0}, ", TableVisitas._CODESTABLECIMIENTO));
            _sb.Append(TableVisitas._ORDEN);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["COD_RUTA"]));
            _sb.Append(string.Format("'{0}', ", pfila["NO_CIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["DIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["NO_CLIENTE"]));
            _sb.Append(string.Format("'{0}', ", pfila["NO_ESTABLECIMIENTO"]));
            _sb.Append(string.Format("'{0}'", pfila["ORDEN"]));
            _sb.Append(")");

            return _sb;
        }

    }
}
