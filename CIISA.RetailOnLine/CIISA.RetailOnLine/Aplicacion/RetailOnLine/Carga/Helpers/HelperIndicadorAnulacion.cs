using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    public class HelperIndicadorAnulacion
    {
        internal StringBuilder insertTablaIndicadorAnulacion(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._indAnulacion);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableIndicadorAnulacion._NO_CIA));
            _sb.Append(string.Format("{0}, ", TableIndicadorAnulacion._NO_AGENTE));
            _sb.Append(string.Format("{0} ", TableIndicadorAnulacion._NO_TRANSA));

            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("( ");
            _sb.Append(string.Format("'{0}', ", pfila[TableIndicadorAnulacion._NO_CIA]));
            _sb.Append(string.Format("'{0}', ", pfila[TableIndicadorAnulacion._NO_AGENTE]));
            _sb.Append(string.Format("'{0}' ", pfila[TableIndicadorAnulacion._NO_TRANSA]));

            _sb.Append(")");

            return _sb;
        }
    }
}