using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperSms
    {

        internal bool enviarCoordenadas(string ptipoDocumento)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableSms._COORDENADAS + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._sms + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableSms._TIPO_DOC + " = ");
            _sb.Append("'" + ptipoDocumento + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            string _envia = MultiGeneric.readStringText(_sb);

            if (_envia.Equals(SQL._Si))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool enviarSms(string ptipoDocumento)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableSms._SMS + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._sms + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableSms._TIPO_DOC + " = ");
            _sb.Append("'" + ptipoDocumento + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            string _envia = MultiGeneric.readStringText(_sb);

            if (_envia.Equals(SQL._Si))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal string buscarTelefono_1(string ptipoDocumento)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableSms._TELEFONO_1 + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._sms + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableSms._TIPO_DOC + " = ");
            _sb.Append("'" + ptipoDocumento + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal string buscarTelefono_2(string ptipoDocumento)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableSms._TELEFONO_2 + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._sms + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableSms._TIPO_DOC + " = ");
            _sb.Append("'" + ptipoDocumento + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal string buscarTelefono_3(string ptipoDocumento)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableSms._TELEFONO_3 + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._sms + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableSms._TIPO_DOC + " = ");
            _sb.Append("'" + ptipoDocumento + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal DataTable buscarSms_DT(string ptipoDocumento)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableSms._NO_AGENTE + ", ");
            _sb.Append(TableSms._NO_AGENTE + ", ");
            _sb.Append(TableSms._TIPO_DOC + ", ");
            _sb.Append(TableSms._COORDENADAS + ", ");
            _sb.Append(TableSms._SMS + ", ");
            _sb.Append(TableSms._TELEFONO_1 + ", ");
            _sb.Append(TableSms._TELEFONO_2 + ", ");
            _sb.Append(TableSms._TELEFONO_3 + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._sms + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableSms._TIPO_DOC + " = ");
            _sb.Append("'" + ptipoDocumento + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

    }
}
