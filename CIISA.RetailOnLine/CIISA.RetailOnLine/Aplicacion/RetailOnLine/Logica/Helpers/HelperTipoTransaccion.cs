using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperTipoTransaccion
    {

        internal DataTable buscarTiposTransacciones()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("TIPOTRANSACCION, ");
            _sb.Append("DESCRIPCION ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._tipoTransaccion);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        internal string obtenerCodigoTipoTransaccion(string pnomTipoTransaccion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TablesROL._tipoTransaccion + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._tipoTransaccion + " ");
            _sb.Append("WHERE ");
            _sb.Append("DESCRIPCION = ");
            _sb.Append("'" + pnomTipoTransaccion + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal string obtenerDescripcionTipoTransaccion(string pcodTipoTransaccion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("DESCRIPCION ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._tipoTransaccion + " ");
            _sb.Append("WHERE ");
            _sb.Append("TIPOTRANSACCION = ");
            _sb.Append("'" + pcodTipoTransaccion + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

    }
}
