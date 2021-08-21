using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperEmbalaje
    {
        internal DataTable buscarEmbalajePorArticulo(string pcodArticulo)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("NO_CIA, ");
            _sb.Append("ARTICULO, ");
            _sb.Append("EMBALAJE ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._embalaje + " ");
            _sb.Append("WHERE ");
            _sb.Append("ARTICULO = '" + pcodArticulo + "' ");
            _sb.Append("ORDER BY ");
            _sb.Append("ORDEN");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        internal string buscarEmbalajePorDefinicion(string pcodArticulo)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("EMBALAJE ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._embalaje + " ");
            _sb.Append("WHERE ");
            _sb.Append("ARTICULO = '" + pcodArticulo + "' ");
            _sb.Append("AND ");
            _sb.Append("ORDEN = 1");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }
    }
}
