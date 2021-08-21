using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperListaDePrecios
    {
        internal DataTable buscarListaPrecios()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("GRUPO_PRE, ");
            _sb.Append("DESCRIPCION ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._listaPrecios);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        internal string buscarCodigoListaPrecios(string pnomListaPrecios)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("GRUPO_PRE ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._listaPrecios + " ");
            _sb.Append("WHERE ");
            _sb.Append("DESCRIPCION = ");
            _sb.Append("'" + pnomListaPrecios + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }
    }
}
