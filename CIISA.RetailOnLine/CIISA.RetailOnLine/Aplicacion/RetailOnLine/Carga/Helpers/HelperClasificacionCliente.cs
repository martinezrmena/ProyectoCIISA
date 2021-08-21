using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    internal class HelperClasificacionCliente
    {

        internal StringBuilder insertTablaClasificacionCliente(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._clasificacionCliente);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableClasificacionCliente._NO_CIA));
            _sb.Append(string.Format("{0}, ", TableClasificacionCliente._CODIGO));
            _sb.Append(TableClasificacionCliente._NOMBRE);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["NO_CIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["CODIGO"]));
            _sb.Append(string.Format("'{0}'", pfila["NOMBRE"]));
            _sb.Append(")");

            return _sb;
        }

    }
}
