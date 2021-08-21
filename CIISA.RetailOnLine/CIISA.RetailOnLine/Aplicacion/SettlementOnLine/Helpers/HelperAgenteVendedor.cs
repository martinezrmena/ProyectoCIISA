using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Helpers
{
    internal class HelperAgenteVendedor
    {
        internal string obtenerCodigoCliente()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableAgenteVendedor._CODIGO_CLIENTE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._agenteVendedor + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

    }
}
