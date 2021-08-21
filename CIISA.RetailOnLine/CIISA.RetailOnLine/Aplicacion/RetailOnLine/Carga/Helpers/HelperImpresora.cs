using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    public class HelperImpresora
    {

        public StringBuilder insertTablaImpresora(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._impresora);
            _sb.Append("(");
            _sb.Append("" + TableImpresora._NO_CIA + ", ");
            _sb.Append("" + TableImpresora._NO_AGENTE + ", ");
            _sb.Append("" + TableImpresora._PUERTO + "");
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append("'" + pfila["" + TableImpresora._NO_CIA + ""] + "', ");
            _sb.Append("'" + pfila["" + TableImpresora._NO_AGENTE + ""] + "', ");
            _sb.Append("'" + pfila["" + TableImpresora._PUERTO + ""] + "'");
            _sb.Append(")");

            return _sb;
        }

    }
}
