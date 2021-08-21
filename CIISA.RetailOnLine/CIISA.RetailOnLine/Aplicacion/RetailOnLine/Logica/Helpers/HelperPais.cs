using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperPais
    {
        internal DataTable buscarPaises()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("PAIS, DESCRIPCION ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._pais);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        internal string obtenerCodigoPais(string pnomPais)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("PAIS ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._pais + " ");
            _sb.Append("WHERE ");
            _sb.Append("DESCRIPCION = ");
            _sb.Append("'" + pnomPais + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }
    }
}
