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
    internal class HelperFormaPagoRecibo
    {
        private StringBuilder sentenciaBuscarPagosRecibos()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TablePagoRecibo._NO_CIA + ", ");
            _sb.Append(TablePagoRecibo._NO_TRANSA + ", ");
            _sb.Append(TablePagoRecibo._NO_LINEA + ", ");
            _sb.Append("REPLACE(" + TablePagoRecibo._MONTO + ",',','.') " + TablePagoRecibo._MONTO + ", ");
            _sb.Append(TablePagoRecibo._TIPO + ", ");
            _sb.Append(TablePagoRecibo._NO_FISICO + ", ");
            _sb.Append(TablePagoRecibo._SERIE + ", ");
            _sb.Append(TablePagoRecibo._FECHA_CREA + ", ");
            _sb.Append(TablePagoRecibo._BANCO + ", ");
            _sb.Append(TablePagoRecibo._TIPO_DOC + " ");
            _sb.Append("FROM ");

            return _sb;
        }

        internal DataTable buscarPagosRecibosSinEnviar(string ptipoDescarga, TransaccionEncabezado pobjTransaccionEncabezado)
        {
            StringBuilder _sb = sentenciaBuscarPagosRecibos();

            if (ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append(TablesROL._pagoReciboBK + " ");
            }
            else
            {
                _sb.Append(TablesROL._pagoRecibo + " ");
            }

            _sb.Append("WHERE ");
            _sb.Append(TablePagoRecibo._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");

            if (!ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append("AND ");
                _sb.Append(TablePagoRecibo._ENVIADO + " = ");
                _sb.Append("'" + SQL._No + "' ");
            }

            _sb.Append(" AND ");
            _sb.Append(TablePagoRecibo._NO_TRANSA + " != ");
            _sb.Append("'" + pobjTransaccionEncabezado.v_codDocumento + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            var DTSpecialFormat = DependencyService.Get<IFillDataSet_Table>();

            return DTSpecialFormat.DataTable_Format(MultiGeneric.uploadDataTable(_sb));
        }

        internal DataTable buscarPagosRecibosSinEnviar(string ptipoDescarga)
        {
            StringBuilder _sb = sentenciaBuscarPagosRecibos();

            if (ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append(TablesROL._pagoReciboBK + " ");
            }
            else
            {
                _sb.Append(TablesROL._pagoRecibo + " ");
            }

            _sb.Append("WHERE ");
            _sb.Append(TablePagoRecibo._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");

            if (!ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append("AND ");
                _sb.Append(TablePagoRecibo._ENVIADO + " = ");
                _sb.Append("'" + SQL._No + "' ");
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            var DTSpecialFormat = DependencyService.Get<IFillDataSet_Table>();

            return DTSpecialFormat.DataTable_Format(MultiGeneric.uploadDataTable(_sb));
        }
    }
}
