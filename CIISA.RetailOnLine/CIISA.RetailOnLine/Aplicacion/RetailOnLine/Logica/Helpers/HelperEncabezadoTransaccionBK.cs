using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using System;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperEncabezadoTransaccionBK
    {

        internal DateTime buscarFechaHoraDocumento(string pcodTransaction, string pcodTipoTransaccion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableEncabezadoDocumento._FECHACREACION + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoDocumentoBK + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableEncabezadoDocumento._CODDOCUMENTO + " = ");
            _sb.Append("'" + pcodTransaction + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("'" + pcodTipoTransaccion + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            string _fechaHora = MultiGeneric.readStringText(_sb);

            return FormatUtil.convertStringToDateTimeWithTime(_fechaHora);
        }

    }
}
