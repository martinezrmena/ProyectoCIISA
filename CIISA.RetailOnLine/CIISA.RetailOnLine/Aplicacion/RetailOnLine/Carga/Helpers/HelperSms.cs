using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.TablesNAF;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    public class HelperSms
    {

        public StringBuilder insertTablaSms(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._sms);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableSms._NO_CIA));
            _sb.Append(string.Format("{0}, ", TableSms._NO_AGENTE));
            _sb.Append(string.Format("{0}, ", TableSms._TIPO_DOC));
            _sb.Append(string.Format("{0}, ", TableSms._COORDENADAS));
            _sb.Append(string.Format("{0}, ", TableSms._SMS));
            _sb.Append(string.Format("{0}, ", TableSms._TELEFONO_1));
            _sb.Append(string.Format("{0}, ", TableSms._TELEFONO_2));
            _sb.Append(string.Format("{0}", TableSms._TELEFONO_3));
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["" + TableSmsNAF.NO_CIA + ""]));
            _sb.Append(string.Format("'{0}', ", pfila["" + TableSmsNAF.NO_AGENTE + ""]));
            _sb.Append(string.Format("'{0}', ", pfila["" + TableSmsNAF.TIPO_DOC + ""]));
            _sb.Append(string.Format("'{0}', ", pfila["" + TableSmsNAF.COORDENADAS + ""]));
            _sb.Append(string.Format("'{0}', ", pfila["" + TableSmsNAF.SMS + ""]));
            _sb.Append(string.Format("'{0}', ", pfila["" + TableSmsNAF.TELEFONO_1 + ""]));
            _sb.Append(string.Format("'{0}', ", pfila["" + TableSmsNAF.TELEFONO_2 + ""]));
            _sb.Append(string.Format("'{0}'", pfila["" + TableSmsNAF.TELEFONO_3 + ""]));
            _sb.Append(")");

            return _sb;
        }

    }
}
