using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperDistrito
    {
        internal DataTable buscarDistritos(string pcodProvincia, string pcodCanton)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("PAIS, ");
            _sb.Append("PROVINCIA, ");
            _sb.Append("CANTON, ");
            _sb.Append("DISTRITO, ");
            _sb.Append("DESCRIPCION ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._distrito + " ");
            _sb.Append("WHERE ");
            _sb.Append("PROVINCIA = ");
            _sb.Append("'" + pcodProvincia + "' ");
            _sb.Append("AND ");
            _sb.Append("CANTON = ");
            _sb.Append("'" + pcodCanton + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        internal string buscarCodigoDistrito(string pcodProvincia, string pcodCanton, string pnomDistrito)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("DISTRITO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._distrito + " ");
            _sb.Append("WHERE ");
            _sb.Append("PROVINCIA = ");
            _sb.Append("'" + pcodProvincia + "' ");
            _sb.Append("AND ");
            _sb.Append("CANTON = ");
            _sb.Append("'" + pcodCanton + "' ");
            _sb.Append("AND ");
            _sb.Append("DESCRIPCION = ");
            _sb.Append("'" + pnomDistrito + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal string buscarNombreDistrito(string pcodProvincia, string pcodCanton, string pcodDistrito)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("DESCRIPCION ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._distrito + " ");
            _sb.Append("WHERE ");
            _sb.Append("PROVINCIA = ");
            _sb.Append("'" + pcodProvincia + "' ");
            _sb.Append("AND ");
            _sb.Append("CANTON = ");
            _sb.Append("'" + pcodCanton + "' ");
            _sb.Append("AND ");
            _sb.Append("DISTRITO = ");
            _sb.Append("'" + pcodDistrito + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }
    }
}
