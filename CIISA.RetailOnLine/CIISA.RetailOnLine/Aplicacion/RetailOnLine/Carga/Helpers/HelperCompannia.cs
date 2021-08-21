using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    internal class HelperCompannia
    {

        internal StringBuilder insertTablaCompannia(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._compannia);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableCompannia._CODCIA));
            _sb.Append(string.Format("{0}, ", TableCompannia._DESCRIPCION));
            _sb.Append(string.Format("{0}, ", TableCompannia._RAZONSOCIAL));
            _sb.Append(TableCompannia._CEDJURIDICA);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["NO_CIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["NOMBRE"]));
            _sb.Append(string.Format("'{0}', ", pfila["NOMBRE"]));
            _sb.Append(string.Format("'{0}'", pfila["ID_TRIBUTARIO"]));
            _sb.Append(")");

            return _sb;
        }

    }
}
