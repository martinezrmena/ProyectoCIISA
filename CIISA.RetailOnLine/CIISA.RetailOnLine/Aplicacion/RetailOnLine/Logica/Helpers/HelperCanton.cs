using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperCanton
    {
        internal DataTable buscarCantones(string pcodProvincia)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append("SELECT ");
            _sb.Append(TableCanton._PAIS + ", ");
            _sb.Append(TableCanton._PROVINCIA + ", ");
            _sb.Append(TableCanton._CANTON + ", ");
            _sb.Append(TableCanton._DESCRIPCION + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._canton + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableCanton._PROVINCIA + " = ");
            _sb.Append("'" + pcodProvincia + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        internal string obtenerCodigoCanton(string pcodProvincia, string pnomCanton)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append("SELECT ");
            _sb.Append(TableCanton._CANTON + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._canton + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableCanton._PROVINCIA + " = ");
            _sb.Append("'" + pcodProvincia + "' ");
            _sb.Append("AND ");
            _sb.Append(TableCanton._DESCRIPCION + " = ");
            _sb.Append("'" + pnomCanton + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal string obtenerNombreCanton(string pcodProvincia, string pcodCanton)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append("SELECT ");
            _sb.Append(TableCanton._DESCRIPCION + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._canton + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableCanton._PROVINCIA + " = ");
            _sb.Append("'" + pcodProvincia + "' ");
            _sb.Append("AND ");
            _sb.Append(TableCanton._CANTON + " = ");
            _sb.Append("'" + pcodCanton + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }
    }
}
