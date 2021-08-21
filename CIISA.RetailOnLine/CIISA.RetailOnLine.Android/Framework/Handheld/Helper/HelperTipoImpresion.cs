using System.Text;
using CIISA.RetailOnLine.Droid.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;

namespace CIISA.RetailOnLine.Droid.Framework.Handheld.Helper
{
    public class HelperTipoImpresion
    {
        internal string GetValue()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(string.Format("{0} ", TableTipoImpresion._TIPO));
            _sb.Append("FROM ");
            _sb.Append(TablesROL._TipoImpresora + " ");
            _sb.Append("LIMIT 1 ");

            return MultiGeneric.readStringText(_sb);

        }
    }
}