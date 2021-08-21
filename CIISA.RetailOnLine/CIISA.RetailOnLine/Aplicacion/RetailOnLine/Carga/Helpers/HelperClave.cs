using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.TablesNAF;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    internal class HelperClave
    {

        internal StringBuilder insertTablaClave(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._clave);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableClave._NO_CIA));
            _sb.Append(string.Format("{0}, ", TableClave._NO_AGENTE));
            _sb.Append(string.Format("{0}, ", TableClave._PRINCIPAL));
            _sb.Append(string.Format("{0}, ", TableClave._TOMA_FISICA));
            _sb.Append(string.Format("{0}, ", TableClave._FALTANTES));
            _sb.Append(string.Format("{0}, ", TableClave._SMART_CLIENT));
            _sb.Append(string.Format("{0}, ", TableClave._CONSECUTIVO));
            _sb.Append(TableClave._EMERGENCIA);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila[TableClaveNAF.NO_CIA]));
            _sb.Append(string.Format("'{0}', ", pfila[TableClaveNAF.NO_AGENTE]));
            _sb.Append(string.Format("'{0}', ", pfila[TableClaveNAF.PRINCIPAL]));
            _sb.Append(string.Format("'{0}', ", pfila[TableClaveNAF.TOMA_FISICA]));
            _sb.Append(string.Format("'{0}', ", pfila[TableClaveNAF.FALTANTES]));
            _sb.Append(string.Format("'{0}', ", pfila[TableClaveNAF.SMART_CLIENT]));
            _sb.Append(string.Format("'{0}', ", pfila[TableClaveNAF.CONSECUTIVO]));
            _sb.Append(string.Format("'{0}'", pfila[TableClaveNAF.CONSECUTIVO]));
            _sb.Append(")");

            return _sb;
        }

    }
}
