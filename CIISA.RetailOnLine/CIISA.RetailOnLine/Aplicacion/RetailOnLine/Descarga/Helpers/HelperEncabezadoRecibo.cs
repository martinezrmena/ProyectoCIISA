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
    internal class HelperEncabezadoRecibo
    {
        private StringBuilder sentenciaBuscarEncabezadosRecibos()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableEncabezadoRecibo._NO_CIA + ", ");
            _sb.Append(TableEncabezadoRecibo._NO_TRANSA + ", ");
            _sb.Append(TableEncabezadoRecibo._NO_CLIENTE + ", ");
            _sb.Append(TableEncabezadoRecibo._NO_ESTABLECIMIENTO + ", ");
            _sb.Append(TableEncabezadoRecibo._NO_AGENTE + ", ");
            _sb.Append(TableEncabezadoRecibo._OBSERVACION + ", ");
            _sb.Append("REPLACE(" + TableEncabezadoRecibo._MONTO + ",',','.') " + TableEncabezadoRecibo._MONTO + ", ");
            _sb.Append(TableEncabezadoRecibo._FECHA_CREA + ", ");
            _sb.Append("REPLACE(" + TableEncabezadoRecibo._SALDO + ",',','.') " + TableEncabezadoRecibo._SALDO + ", ");
            _sb.Append(TableEncabezadoRecibo._TIPO_DOC + ", ");
            _sb.Append(TableEncabezadoRecibo._NO_LINEA + ", ");
            _sb.Append(TableEncabezadoRecibo._ENVIADO + ", ");
            _sb.Append(TableEncabezadoRecibo._ANULADO + ", ");
            _sb.Append(TableEncabezadoRecibo._CLIENTE_NUEVO + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableEncabezadoRecibo._FECHA_TOMA + " ) " + TableEncabezadoRecibo._FECHA_TOMA + ", ");
            _sb.Append("REPLACE(" + TableEncabezadoRecibo._LATITUD + ",',','.') " + TableEncabezadoRecibo._LATITUD + ", ");
            _sb.Append("REPLACE(" + TableEncabezadoRecibo._LONGITUD + ",',','.') " + TableEncabezadoRecibo._LONGITUD + " ");
            _sb.Append("FROM ");

            return _sb;
        }

        internal DataTable buscarEncabezadosRecibosSinEnviarAutomatico(string ptipoDescarga, TransaccionEncabezado pobjTransaccionEncabezado)
        {
            StringBuilder _sb = sentenciaBuscarEncabezadosRecibos();

            if (ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append(TablesROL._encabezadoReciboBK + " ");
            }
            else
            {
                _sb.Append(TablesROL._encabezadoRecibo + " ");
            }

            _sb.Append("WHERE ");
            _sb.Append(TableEncabezadoRecibo._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoRecibo._ENVIADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append(" AND ");
            _sb.Append(TableEncabezadoRecibo._NO_TRANSA + " != ");
            _sb.Append("'" + pobjTransaccionEncabezado.v_codDocumento + "'");


            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            var DTSpecialFormat = DependencyService.Get<IFillDataSet_Table>();

            return DTSpecialFormat.DataTable_Format(MultiGeneric.uploadDataTable(_sb));
        }


        internal DataTable buscarEncabezadosRecibosSinEnviar(string ptipoDescarga)
        {
            StringBuilder _sb = sentenciaBuscarEncabezadosRecibos();

            if (ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append(TablesROL._encabezadoReciboBK + " ");
            }
            else
            {
                _sb.Append(TablesROL._encabezadoRecibo + " ");
            }

            _sb.Append("WHERE ");
            _sb.Append(TableEncabezadoRecibo._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");

            if (!ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append("AND ");
                _sb.Append(TableEncabezadoRecibo._ENVIADO + " = ");
                _sb.Append("'" + SQL._No + "' ");
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            var DTSpecialFormat = DependencyService.Get<IFillDataSet_Table>();

            return DTSpecialFormat.DataTable_Format(MultiGeneric.uploadDataTable(_sb));
        }
    }
}
