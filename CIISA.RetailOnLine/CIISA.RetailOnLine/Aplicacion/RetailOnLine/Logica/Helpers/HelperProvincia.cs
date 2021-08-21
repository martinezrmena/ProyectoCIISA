using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperProvincia
    {
        internal DataTable buscarProvincias()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableProvincia._PAIS + ", ");
            _sb.Append(TableProvincia._PROVINCIA + ", ");
            _sb.Append(TableProvincia._DESCRIPCION + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._provincia);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        internal string obtenerCodigoProvincia(string pnomProvincia)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableProvincia._PROVINCIA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._provincia + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableProvincia._DESCRIPCION + " = ");
            _sb.Append("'" + pnomProvincia + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal string obtenerNombreProvincia(string pcodProvincia)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableProvincia._DESCRIPCION + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._provincia + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableProvincia._PROVINCIA + " = ");
            _sb.Append("'" + pcodProvincia + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }
    }
}
