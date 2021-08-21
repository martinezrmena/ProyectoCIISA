using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperMotivo
    {
        internal DataTable buscarMotivoPorCodigoTransaccion(string pcodTipoTransaccion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("NO_CIA, ");
            _sb.Append("TIPO_DOC, ");
            _sb.Append("CODIGO, ");
            _sb.Append("DESCRIPCION ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._motivo + " ");
            _sb.Append("WHERE ");
            _sb.Append("TIPO_DOC = ");
            _sb.Append("'" + pcodTipoTransaccion + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        internal string obtenerCodigoMotivo(string pcodTipoTransaccion, string pnomMotivo)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("CODIGO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._motivo + " ");
            _sb.Append("WHERE ");
            _sb.Append("TIPO_DOC = ");
            _sb.Append("'" + pcodTipoTransaccion + "' ");
            _sb.Append("AND ");
            _sb.Append("DESCRIPCION = ");
            _sb.Append("'" + pnomMotivo + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal string obtenerDescripcionMotivo(string pcodTipoTransaccion, string pcodMotivo)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("DESCRIPCION ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._motivo + " ");
            _sb.Append("WHERE ");
            _sb.Append("TIPO_DOC = ");
            _sb.Append("'" + pcodTipoTransaccion + "' ");
            _sb.Append("AND ");
            _sb.Append("CODIGO = ");
            _sb.Append("'" + pcodMotivo + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }
    }
}
