using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Constantes;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers
{
    public class HelperTipoImpresion
    {
        internal void InsertDefaultValue()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._TipoImpresora);
            _sb.Append("( ");
            _sb.Append(TableTipoImpresion._TIPO);
            _sb.Append(")");
            _sb.Append(" VALUES");
            _sb.Append("( ");
            _sb.Append("'" + TipoImpresionConst.ZPL + "'");
            _sb.Append(")");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.insertRecord(_sb);

        }

        internal int UpdateValue(string TipoImpresion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._TipoImpresora);
            _sb.Append(" SET ");
            _sb.Append(TableTipoImpresion._TIPO + "=");
            _sb.Append("'" + TipoImpresion + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.updateRecord(_sb);

        }

        internal string GetValue()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(string.Format("{0} ", TableTipoImpresion._TIPO));
            _sb.Append("FROM ");
            _sb.Append(TablesROL._TipoImpresora + " ");
            _sb.Append("LIMIT 1 ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);

        }
    }
}
