using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.GPS.ViewController;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    public class HelperEncabezadoTramite
    {
        internal async Task guardarEncabezadoTramite(Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();
            GPS_Info.initializeGPS();

            _sb.Append("INSERT INTO ");
            _sb.Append(TablesROL._encabezadoTramite + " ");
            _sb.Append("(");
            _sb.Append("" + TableEncabezadoTramite._NO_CIA + ", ");
            _sb.Append("" + TableEncabezadoTramite._NO_TRANSA + ", ");
            _sb.Append("" + TableEncabezadoTramite._NO_CLIENTE + ", ");
            _sb.Append("" + TableEncabezadoTramite._NO_ESTABLECIMIENTO + ", ");
            _sb.Append("" + TableEncabezadoTramite._NO_AGENTE + ", ");
            _sb.Append("" + TableEncabezadoTramite._FECHA_CREA + ", ");
            _sb.Append("" + TableEncabezadoTramite._MONTO + ", ");
            _sb.Append("" + TableEncabezadoTramite._ENVIADO + ", ");
            _sb.Append("" + TableEncabezadoTramite._FECHA_TOMA + ", ");
            _sb.Append("" + TableEncabezadoTramite._LATITUD + ", ");
            _sb.Append("" + TableEncabezadoTramite._LONGITUD + "");
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany + "', ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_codDocumento + "', ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "', ");
            _sb.Append("" + pobjCliente.v_objEstablecimiento.v_codEstablecimiento + ", ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_codAgente + "', ");
            _sb.Append("DATETIME('NOW', 'LOCALTIME'), ");
            _sb.Append("REPLACE('" + pobjCliente.v_objTransaccion.v_total + "',',',''), ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_enviado + "', ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_fechaTomaFisica.ToString("yyyy-MM-dd HH:mm:ss") + "', ");
            _sb.Append("REPLACE('" + await GPS_Info.v_gpsDevice.GetLatitude() + "',',',''), ");
            _sb.Append("REPLACE('" + await GPS_Info.v_gpsDevice.GetLongitude() + "',',','')");
            _sb.Append(")");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.insertRecord(_sb);
        }

        internal decimal calcularTotalFlujoEfectivo()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("SUM(" + TableEncabezadoTramite._MONTO + ") AS TOTAL ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoTramite + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readDecimal(_sb);
        }
    }
}
