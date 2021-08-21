using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperDetalleTramite
    {
        internal void guardarDetalleTramite(Cliente pobjCliente)
        {
            int _noLinea = 1;

            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT INTO ");
            _sb.Append(TablesROL._detalleTramite + " ");
            _sb.Append("(");
            _sb.Append("NO_CIA, ");
            _sb.Append("NO_TRANSA, ");
            _sb.Append("NO_LINEA, ");
            _sb.Append("NO_FACTURA, ");
            _sb.Append("MONTO, ");
            _sb.Append("ENVIADO");
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany + "', ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_codDocumento + "', ");
            _sb.Append("" + _noLinea + ", ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_facturaTramitar + "', ");
            _sb.Append("REPLACE('" + pobjCliente.v_objTransaccion.v_total + "',',',''), ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_enviado + "'");
            _sb.Append(")");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.insertRecord(_sb);
        }
    }
}
