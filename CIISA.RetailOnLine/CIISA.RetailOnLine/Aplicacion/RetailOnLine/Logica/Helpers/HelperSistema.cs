using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperSistema
    {
        internal string buscarMaximaFecha()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableSistema._FECHA + " ) " + TableSistema._FECHA + " ");
            _sb.Append("FROM ");
            _sb.Append("(");
            _sb.Append("SELECT MAX(DATETIME(" + TableSistema._FECHA + ")) AS " + TableSistema._FECHA + " ");
            _sb.Append("FROM " + TablesROL._sistema);
            _sb.Append(") " + TableSistema._FECHA);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal string buscarEstado(DateTime pdate)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableSistema._IND_CIERRE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._sistema + " ");
            _sb.Append("WHERE ");
            _sb.Append("DATE("+TableSistema._FECHA + ") = ");
            _sb.Append("DATE('" + VarTime.dateSQLCE(pdate) + "') ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal void actualizarEstado(bool pbloqueado)
        {
            string _bloqueado = string.Empty;

            if (pbloqueado)
            {
                _bloqueado = SQL._close;
            }
            else
            {
                _bloqueado = SQL._pending;
            }

            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._sistema + " ");
            _sb.Append("SET ");
            _sb.Append(TableSistema._IND_CIERRE + " = ");
            _sb.Append("'" + _bloqueado + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);
        }
    }
}
