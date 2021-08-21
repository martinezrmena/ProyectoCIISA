using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Helpers
{
    internal class HelperFormaPagoTransaccion
    {
        internal DataTable buscarPagosTransaccionesSinEnviar(string ptipoDescarga, TransaccionEncabezado pobjTransaccionEncabezado)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TablePagos._NO_CIA + ", ");
            _sb.Append(TablePagos._NO_TRANSA + ", ");
            _sb.Append(TablePagos._NO_LINEA + ", ");
            _sb.Append("REPLACE(" + TablePagos._MONTO + ",',','.') " + TablePagos._MONTO + ", ");
            _sb.Append(TablePagos._TIPO + ", ");
            _sb.Append(TablePagos._NO_FISICO + ", ");
            _sb.Append(TablePagos._SERIE + ", ");
            //_sb.Append("STRFTIME('%d/%m/%Y'," + TablePagos._FECHA_CREA + " ) " + TablePagos._FECHA_CREA + ", ");
            _sb.Append(TablePagos._FECHA_CREA + ", ");
            _sb.Append(TablePagos._BANCO + ", ");
            _sb.Append(TablePagos._ENVIADO + ", ");
            _sb.Append(TablePagos._ANULADO + " ");
            _sb.Append("FROM ");

            if (ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append(TablesROL._pagosBK + " ");
            }
            else
            {
                _sb.Append(TablesROL._pagos + " ");
            }

            _sb.Append("WHERE ");
            _sb.Append(TablePagos._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");

            if (!ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append("AND ");
                _sb.Append(TablePagos._ENVIADO + " = ");
                _sb.Append("'" + SQL._No + "' ");
                _sb.Append("AND ");
            }

            _sb.Append(TablePagos._NO_TRANSA + " != ");
            _sb.Append("'" + pobjTransaccionEncabezado.v_codDocumento + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        internal DataTable buscarPagosTransaccionesSinEnviar(string ptipoDescarga)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TablePagos._NO_CIA + ", ");
            _sb.Append(TablePagos._NO_TRANSA + ", ");
            _sb.Append(TablePagos._NO_LINEA + ", ");
            _sb.Append("REPLACE(" + TablePagos._MONTO + ",',','.') " + TablePagos._MONTO + ", ");
            _sb.Append(TablePagos._TIPO + ", ");
            _sb.Append(TablePagos._NO_FISICO + ", ");
            _sb.Append(TablePagos._SERIE + ", ");
            //_sb.Append("STRFTIME('%d/%m/%Y'," + TablePagos._FECHA_CREA + " ) " + TablePagos._FECHA_CREA + ", ");
            _sb.Append(TablePagos._FECHA_CREA + ", ");
            _sb.Append(TablePagos._BANCO + ", ");
            _sb.Append(TablePagos._ENVIADO + ", ");
            _sb.Append(TablePagos._ANULADO + " ");
            _sb.Append("FROM ");

            if (ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append(TablesROL._pagosBK + " ");
            }
            else
            {
                _sb.Append(TablesROL._pagos + " ");
            }

            _sb.Append("WHERE ");
            _sb.Append(TablePagos._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");

            if (!ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append("AND ");
                _sb.Append(TablePagos._ENVIADO + " = ");
                _sb.Append("'" + SQL._No + "' ");
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }
    }
}
