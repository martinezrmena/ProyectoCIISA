using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperPagoRecibo
    {
        internal void actualizarAnulado(TransaccionEncabezado pobjTransaccion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._pagoRecibo + " ");
            _sb.Append("SET ");
            _sb.Append("ANULADO = ");
            _sb.Append("'" + MiscUtils.getVariableStringSQLState(true) + "' ");
            _sb.Append("WHERE ");
            _sb.Append("NO_TRANSA = ");
            _sb.Append("'" + pobjTransaccion.v_codDocumento + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);
        }

        internal decimal obtenerMontoPorTipoPago(string ptipoPago)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("SUM(MONTO) MONTO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._pagoRecibo + " ");
            _sb.Append("WHERE ");
            _sb.Append("TIPO = ");
            _sb.Append("'" + ptipoPago + "' ");
            _sb.Append("AND ");
            _sb.Append("ANULADO = ");
            _sb.Append("'" + SQL._No + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readDecimal(_sb);
        }

        internal void guardarPagoRecibo(Cliente pobjCliente)
        {
            int _numLinea = 1;

            foreach (FormaPago _formaPago in pobjCliente.v_objTransaccion.v_listaFormaPago)
            {
                StringBuilder _sb = new StringBuilder();

                _sb.Append("INSERT ");
                _sb.Append("INTO ");
                _sb.Append(TablesROL._pagoRecibo + " ");
                _sb.Append("(");
                _sb.Append("NO_CIA, ");
                _sb.Append("NO_TRANSA, ");
                _sb.Append("NO_LINEA, ");
                _sb.Append("MONTO, ");
                _sb.Append("TIPO, ");
                _sb.Append("NO_FISICO, ");
                _sb.Append("SERIE, ");
                _sb.Append("FECHA_CREA, ");
                _sb.Append("BANCO, ");
                _sb.Append("TIPO_DOC, ");
                _sb.Append("ENVIADO, ");
                _sb.Append("ANULADO");
                _sb.Append(") ");
                _sb.Append("VALUES ");
                _sb.Append("(");
                _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany + "', ");
                _sb.Append("'" + pobjCliente.v_objTransaccion.v_codDocumento + "', ");
                _sb.Append("" + _numLinea++ + ", ");
                _sb.Append("REPLACE('" + _formaPago._monto + "',',',''), ");
                _sb.Append("'" + _formaPago._formaPago + "', ");
                _sb.Append("'" + _formaPago._numTransaccion + "', ");
                _sb.Append("'" + _formaPago._serie + "', ");
                _sb.Append("DATE('NOW'), ");
                _sb.Append("'" + _formaPago._banco + "', ");
                _sb.Append("'" + pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla() + "', ");
                _sb.Append("'" + SQL._No + "', ");
                _sb.Append("'" + SQL._No + "'");
                _sb.Append(")");

                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                MultiGeneric.insertRecord(_sb);
            }

        }
    }
}
