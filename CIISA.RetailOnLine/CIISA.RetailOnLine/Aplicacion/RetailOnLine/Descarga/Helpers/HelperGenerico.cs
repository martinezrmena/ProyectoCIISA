using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Helpers
{
    public class HelperGenerico
    {
        internal StringBuilder MarcarComoEnviado(string ptable)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(ptable + " ");
            _sb.Append("SET ");
            _sb.Append("ENVIADO = ");
            _sb.Append("'" + SQL._Si + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.insertRecord(_sb);

            return _sb;
        }

        internal StringBuilder MarcarComoEnviado_Documentos(string ptable, TransaccionEncabezado pobjTransaccionEncabezado, string TipoTransaccion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(ptable + " ");
            _sb.Append("SET ");
            _sb.Append("ENVIADO = ");
            _sb.Append("'" + SQL._Si + "' ");
            _sb.Append("WHERE ");
            _sb.Append(TableEncabezadoDocumento._CODDOCUMENTO + " != ");
            _sb.Append("'" + pobjTransaccionEncabezado.v_codDocumento + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("'" + TipoTransaccion + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.insertRecord(_sb);

            return _sb;
        }

        internal StringBuilder MarcarComoEnviado_Tramites(string ptable, TransaccionEncabezado pobjTransaccionEncabezado)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(ptable + " ");
            _sb.Append("SET ");
            _sb.Append("ENVIADO = ");
            _sb.Append("'" + SQL._Si + "' ");
            _sb.Append("WHERE ");
            _sb.Append(TableEncabezadoTramite._NO_TRANSA + " != ");
            _sb.Append("'" + pobjTransaccionEncabezado.v_codDocumento + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.insertRecord(_sb);

            return _sb;
        }

        internal StringBuilder MarcarComoEnviado_DetallesTramites(string ptable, TransaccionEncabezado pobjTransaccionEncabezado)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(ptable + " ");
            _sb.Append("SET ");
            _sb.Append("ENVIADO = ");
            _sb.Append("'" + SQL._Si + "' ");
            _sb.Append("WHERE ");
            _sb.Append(TableDetalleTramite._NO_TRANSA + " != ");
            _sb.Append("'" + pobjTransaccionEncabezado.v_codDocumento + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.insertRecord(_sb);

            return _sb;
        }

        internal StringBuilder MarcarComoEnviado_Recibos(string ptable, TransaccionEncabezado pobjTransaccionEncabezado)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(ptable + " ");
            _sb.Append("SET ");
            _sb.Append("ENVIADO = ");
            _sb.Append("'" + SQL._Si + "' ");
            _sb.Append("WHERE ");
            _sb.Append(TableEncabezadoRecibo._NO_TRANSA + " != ");
            _sb.Append("'" + pobjTransaccionEncabezado.v_codDocumento + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.insertRecord(_sb);

            return _sb;
        }

        internal StringBuilder MarcarComoEnviado_DetallesRecibos(string ptable, TransaccionEncabezado pobjTransaccionEncabezado)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(ptable + " ");
            _sb.Append("SET ");
            _sb.Append("ENVIADO = ");
            _sb.Append("'" + SQL._Si + "' ");
            _sb.Append("WHERE ");
            _sb.Append(TableDetalleRecibo._NO_TRANSA + " != ");
            _sb.Append("'" + pobjTransaccionEncabezado.v_codDocumento + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.insertRecord(_sb);

            return _sb;
        }

        internal StringBuilder MarcarComoEnviado_Pagos(string ptable, TransaccionEncabezado pobjTransaccionEncabezado)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(ptable + " ");
            _sb.Append("SET ");
            _sb.Append("ENVIADO = ");
            _sb.Append("'" + SQL._Si + "' ");
            _sb.Append("WHERE ");
            _sb.Append(TablePagos._NO_TRANSA + " != ");
            _sb.Append("'" + pobjTransaccionEncabezado.v_codDocumento + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.insertRecord(_sb);

            return _sb;
        }

        internal StringBuilder MarcarComoEnviado_PagosRecibos(string ptable, TransaccionEncabezado pobjTransaccionEncabezado)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(ptable + " ");
            _sb.Append("SET ");
            _sb.Append("ENVIADO = ");
            _sb.Append("'" + SQL._Si + "' ");
            _sb.Append("WHERE ");
            _sb.Append(TablePagoRecibo._NO_TRANSA + " != ");
            _sb.Append("'" + pobjTransaccionEncabezado.v_codDocumento + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.insertRecord(_sb);

            return _sb;
        }

    }
}
