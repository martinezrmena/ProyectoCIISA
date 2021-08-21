using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperClasificacionCliente
    {
        internal DataTable buscarClasificacionCliente()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("NO_CIA, ");
            _sb.Append("CODIGO, ");
            _sb.Append("NOMBRE ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._clasificacionCliente);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        internal string buscarCodigoClasificacion(string pnombreClasificacion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("CODIGO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._clasificacionCliente + " ");
            _sb.Append("WHERE ");
            _sb.Append("NOMBRE = ");
            _sb.Append("'" + pnombreClasificacion + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }
    }
}
