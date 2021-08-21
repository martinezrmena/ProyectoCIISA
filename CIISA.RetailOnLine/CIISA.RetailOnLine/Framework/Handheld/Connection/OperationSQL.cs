using Acr.UserDialogs;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.Connection
{
    public static class OperationSQL
    {
        public static void deleteTableFeedbackTextBox(string ptable,Editor ptextBox,Log plog)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("DELETE FROM ");
            _sb.Append("" + ptable + "");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.deleteTable(_sb);

            plog.addSuccessLine(ptextBox, "borró la tabla " + ptable + Simbol._point, 1);
        }

        public static bool thereRecord(StringBuilder psentence, string prow)
        {
            bool _thereRecord = false;

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(psentence);

            if (_dt.Rows.Count > 0)
            {
                string _textField = string.Empty;

                foreach (DataRow _row in _dt.Rows)
                {
                    _textField = _row[prow].ToString();
                }

                if (!_textField.Equals(string.Empty))
                {
                    _thereRecord = true;
                }
            }

            return _thereRecord;
        }

        public static bool checkSent(string ptable)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("ENVIADO ");
            _sb.Append("FROM ");
            _sb.Append(Space._one);
            _sb.Append(ptable);
            _sb.Append(Space._one);
            _sb.Append("WHERE ");
            _sb.Append("ENVIADO = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("ANULADO = ");
            _sb.Append("'" + SQL._No + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            string _send = MultiGeneric.readStringText(_sb);

            return MiscUtils.getVariableBooleanSQLStateStringEmptyTrue(_send);
        }

        public static int updateAsNoSent(string pclassInvoke,string pmethodInvoke,string ptable)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(ptable);
            _sb.Append(" SET ENVIADO = '");
            _sb.Append(SQL._No);
            _sb.Append("'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.updateRecord(_sb);
        }

        public static int updateAsNoSentSpecificTable(string ptable, string pconditionalField, string plist)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(ptable);
            _sb.Append(" SET ENVIADO = '");
            _sb.Append(SQL._No);
            _sb.Append("' ");
            _sb.Append("WHERE ");
            _sb.Append("" + pconditionalField + "");
            _sb.Append(Space._one);
            _sb.Append("NOT IN ");
            _sb.Append("(");
            _sb.Append("" + plist + "");
            _sb.Append(")");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.updateRecord(_sb);
        }

        public static void deleteSpecificTable(
            string ptable,
            string pconditionalField,
            string pconditionalValue,
            Editor ptextBox,
            Log plog
            )
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("DELETE FROM ");
            _sb.Append("" + ptable + "");
            _sb.Append(Space._one);
            _sb.Append("WHERE ");
            _sb.Append("" + pconditionalField + " = ");
            _sb.Append("'" + pconditionalValue + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            try
            {
                MultiGeneric.deleteTable(_sb);

                plog.addSuccessLine(ptextBox, "borró la tabla " + ptable + Simbol._point, 1);
            }
            catch (Exception ex)
            {
                plog.addErrorLine(ptextBox, "no borró la tabla " + ptable + Simbol._point, 1);

                UserDialogs.Instance.AlertAsync("no borró la tabla " + ptable + Simbol._point + "Excepcion: "+ex.Message);

                //throw new ApplicationException("no borró la tabla " + ptable + Simbol._point, ex);
            }
        }
    }
}
