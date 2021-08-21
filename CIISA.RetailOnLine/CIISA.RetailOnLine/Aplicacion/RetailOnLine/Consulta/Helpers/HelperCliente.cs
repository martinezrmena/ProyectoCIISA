using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Consulta.Helpers
{
    internal class HelperCliente
    {
        internal bool existeCliente(string pcedula)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableCliente._CEDULA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._cliente + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableCliente._CEDULA + " = ");
            _sb.Append("'" + pcedula + "'");

            return OperationSQL.thereRecord(_sb, TableCliente._CEDULA);
        }
    }
}
