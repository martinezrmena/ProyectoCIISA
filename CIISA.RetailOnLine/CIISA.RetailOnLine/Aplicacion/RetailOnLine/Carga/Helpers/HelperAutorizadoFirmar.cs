using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    internal class HelperAutorizadoFirmar
    {

        internal StringBuilder insertTablaAutorizadoFirmar(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._autorizadoFirmar);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableAutorizadoFirmar._NO_CIA));
            _sb.Append(string.Format("{0}, ", TableAutorizadoFirmar._GRUPO));
            _sb.Append(string.Format("{0}, ", TableAutorizadoFirmar._CLIENTE));
            _sb.Append(string.Format("{0}, ", TableAutorizadoFirmar._CEDULA_AUTORIZADO));
            _sb.Append(TableAutorizadoFirmar._NOMBRE_AUTORIZADO);
            _sb.Append(") ");
            _sb.Append("VALUES");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["NO_CIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["GRUPO"]));
            _sb.Append(string.Format("'{0}', ", pfila["CLIENTE"]));
            _sb.Append(string.Format("'{0}', ", pfila["CEDULA_AUTORIZADO"]));
            _sb.Append(string.Format("'{0}'", pfila["NOMBRE_AUTORIZADO"]));
            _sb.Append(")");

            return _sb;
        }

    }
}
