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
    internal class HelperEncabezadoTramite
    {

        internal DataTable buscarEncabezadosTramitesSinEnviar(string ptipoDescarga, TransaccionEncabezado pobjTransaccionEncabezado)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableEncabezadoTramite._NO_CIA + ", ");
            _sb.Append(TableEncabezadoTramite._NO_TRANSA + ", ");
            _sb.Append(TableEncabezadoTramite._NO_CLIENTE + ", ");
            _sb.Append(TableEncabezadoTramite._NO_ESTABLECIMIENTO + ", ");
            _sb.Append(TableEncabezadoTramite._NO_AGENTE + ", ");
            _sb.Append(TableEncabezadoTramite._FECHA_CREA + ", ");
            _sb.Append("REPLACE(" + TableEncabezadoTramite._MONTO + ",',','.') " + TableEncabezadoTramite._MONTO + ", ");
            _sb.Append(TableEncabezadoTramite._ENVIADO + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableEncabezadoTramite._FECHA_TOMA + " ) " + TableEncabezadoTramite._FECHA_TOMA + ", ");
            _sb.Append("REPLACE(" + TableEncabezadoTramite._LATITUD + ",',','.') " + TableEncabezadoTramite._LATITUD + ", ");
            _sb.Append("REPLACE(" + TableEncabezadoTramite._LONGITUD + ",',','.') " + TableEncabezadoTramite._LONGITUD + " ");
            _sb.Append("FROM ");

            if (ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append(TablesROL._encabezadoTramiteBK + " ");
                _sb.Append("WHERE ");
            }
            else
            {
                _sb.Append(TablesROL._encabezadoTramite + " ");
                _sb.Append("WHERE ");
                _sb.Append(TableEncabezadoTramite._ENVIADO + " = ");
                _sb.Append("'" + SQL._No + "' ");
                _sb.Append("AND ");
            }

            _sb.Append(TableEncabezadoTramite._NO_TRANSA + " != ");
            _sb.Append("'" + pobjTransaccionEncabezado.v_codDocumento + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            var DTSpecialFormat = DependencyService.Get<IFillDataSet_Table>();

            return DTSpecialFormat.DataTable_Format(MultiGeneric.uploadDataTable(_sb));
        }

        internal DataTable buscarEncabezadosTramitesSinEnviar(string ptipoDescarga)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableEncabezadoTramite._NO_CIA + ", ");
            _sb.Append(TableEncabezadoTramite._NO_TRANSA + ", ");
            _sb.Append(TableEncabezadoTramite._NO_CLIENTE + ", ");
            _sb.Append(TableEncabezadoTramite._NO_ESTABLECIMIENTO + ", ");
            _sb.Append(TableEncabezadoTramite._NO_AGENTE + ", ");
            _sb.Append(TableEncabezadoTramite._FECHA_CREA + ", ");
            _sb.Append("REPLACE(" + TableEncabezadoTramite._MONTO + ",',','.') " + TableEncabezadoTramite._MONTO + ", ");
            _sb.Append(TableEncabezadoTramite._ENVIADO + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableEncabezadoTramite._FECHA_TOMA + " ) " + TableEncabezadoTramite._FECHA_TOMA + ", ");
            _sb.Append("REPLACE(" + TableEncabezadoTramite._LATITUD + ",',','.') " + TableEncabezadoTramite._LATITUD + ", ");
            _sb.Append("REPLACE(" + TableEncabezadoTramite._LONGITUD + ",',','.') " + TableEncabezadoTramite._LONGITUD + " ");
            _sb.Append("FROM ");

            if (ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append(TablesROL._encabezadoTramiteBK + " ");
            }
            else
            {
                _sb.Append(TablesROL._encabezadoTramite + " ");
                _sb.Append("WHERE ");
                _sb.Append(TableEncabezadoTramite._ENVIADO + " = ");
                _sb.Append("'" + SQL._No + "' ");
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            var DTSpecialFormat = DependencyService.Get<IFillDataSet_Table>();

            return DTSpecialFormat.DataTable_Format(MultiGeneric.uploadDataTable(_sb));
        }
    }
}
