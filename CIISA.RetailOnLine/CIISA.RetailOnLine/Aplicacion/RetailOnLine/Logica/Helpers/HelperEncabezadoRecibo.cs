using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.GPS.ViewController;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperEncabezadoRecibo
    {
        internal decimal calcularTotalRecibosPagosPorCliente(string pcodCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("SUM(MONTO) AS TOTALRECIBOS ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoRecibo + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_CLIENTE = ");
            _sb.Append("'" + pcodCliente + "' ");
            _sb.Append("AND ");
            _sb.Append("ANULADO = ");
            _sb.Append("'" + SQL._No + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readDecimal(_sb);
        }

        internal void actualizarAnulado(TransaccionEncabezado pobjTransaccion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._encabezadoRecibo + " ");
            _sb.Append("SET ");
            _sb.Append("ANULADO = ");
            _sb.Append("'" + MiscUtils.getVariableStringSQLState(true) + "' ");
            _sb.Append("WHERE ");
            _sb.Append("NO_TRANSA = ");
            _sb.Append("'" + pobjTransaccion.v_codDocumento + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);
        }

        internal decimal calcularTotalFlujoEfectivo(string pcodTipoDocumento)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("SUM(MONTO) AS TOTAL ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoRecibo + " ");
            _sb.Append("WHERE ");
            _sb.Append("ANULADO = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("TIPO_DOC = ");
            _sb.Append("'" + pcodTipoDocumento + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readDecimal(_sb);
        }

        internal async Task guardarReciboEncabezado(Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();
            GPS_Info.initializeGPS();

            _sb.Append("INSERT INTO ");
            _sb.Append(TablesROL._encabezadoRecibo + " ");
            _sb.Append("(");
            _sb.Append("" + TableEncabezadoRecibo._NO_CIA + ", ");
            _sb.Append("" + TableEncabezadoRecibo._NO_TRANSA + ", ");
            _sb.Append("" + TableEncabezadoRecibo._NO_CLIENTE + ", ");
            _sb.Append("" + TableEncabezadoRecibo._NO_ESTABLECIMIENTO + ", ");
            _sb.Append("" + TableEncabezadoRecibo._NO_AGENTE + ", ");
            _sb.Append("" + TableEncabezadoRecibo._OBSERVACION + ", ");
            _sb.Append("" + TableEncabezadoRecibo._MONTO + ", ");
            _sb.Append("" + TableEncabezadoRecibo._FECHA_CREA + ", ");
            _sb.Append("" + TableEncabezadoRecibo._SALDO + ", ");
            _sb.Append("" + TableEncabezadoRecibo._TIPO_DOC + ", ");
            _sb.Append("" + TableEncabezadoRecibo._NO_LINEA + ", ");
            _sb.Append("" + TableEncabezadoRecibo._MONTO_DEVOL + ", ");
            _sb.Append("" + TableEncabezadoRecibo._NUMERO_DEVOL + ", ");
            _sb.Append("" + TableEncabezadoRecibo._ENVIADO + ", ");
            _sb.Append("" + TableEncabezadoRecibo._ANULADO + ", ");
            _sb.Append("" + TableEncabezadoRecibo._CLIENTE_NUEVO + ", ");
            _sb.Append("" + TableEncabezadoRecibo._FECHA_TOMA + ", ");
            _sb.Append("" + TableEncabezadoRecibo._LATITUD + ", ");
            _sb.Append("" + TableEncabezadoRecibo._LONGITUD + "");
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany + "', ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_codDocumento + "', ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "', ");
            _sb.Append("" + pobjCliente.v_objEstablecimiento.v_codEstablecimiento + ", ");
            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "', ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_observacion + "', ");
            _sb.Append("REPLACE('" + pobjCliente.v_objTransaccion.v_total + "',',',''), ");
            _sb.Append("DATETIME('NOW', 'LOCALTIME'), ");
            _sb.Append("REPLACE('" + pobjCliente.v_objTransaccion.v_saldo + "',',',''), ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla() + "', ");
            _sb.Append("" + pobjCliente.v_objTransaccion.v_listaFormaPago.Count + ", ");
            _sb.Append("REPLACE('" + Numeric._zeroInteger + "',',',''), ");
            _sb.Append("'" + string.Empty + "', ");
            _sb.Append("'" + SQL._No + "', ");
            _sb.Append("'" + SQL._No + "', ");
            _sb.Append("'" + Agent.getCodClienteGenerico() + "', ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_fechaTomaFisica.ToString("yyyy-MM-dd HH:mm:ss") + "', ");
            _sb.Append("REPLACE('" + await GPS_Info.v_gpsDevice.GetLatitude() + "',',',''), ");
            _sb.Append("REPLACE('" + await GPS_Info.v_gpsDevice.GetLongitude() + "',',','')");
            _sb.Append(")");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.insertRecord(_sb);
        }
    }
}
