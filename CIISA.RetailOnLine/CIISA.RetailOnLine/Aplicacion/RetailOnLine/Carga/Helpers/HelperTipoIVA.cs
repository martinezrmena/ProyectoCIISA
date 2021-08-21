using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    public class HelperTipoIVA
    {
        internal StringBuilder insertTablaTipoIVA(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._TiposIVA);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableTiposIVA._CLAVE_IVA));
            _sb.Append(string.Format("{0}, ", TableTiposIVA._PORCENTAJE));
            _sb.Append(TableTiposIVA._SIMBOLO);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["CLAVE_IVA"]));
            _sb.Append(string.Format("'{0}', ", pfila["PORCENTAJE"]));
            _sb.Append(string.Format("'{0}'", pfila["SIMBOLO"]));
            _sb.Append(")");

            return _sb;
        }
    }
}
