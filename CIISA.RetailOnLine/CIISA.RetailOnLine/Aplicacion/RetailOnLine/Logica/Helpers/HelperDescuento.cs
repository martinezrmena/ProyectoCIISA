using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperDescuento
    {
        internal decimal obtenerDescuento(string pcodCliente, string pcodProducto, decimal pcantidad)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("SUM(PORCENTAJE) ");
            _sb.Append("PORCENTAJE ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._descuentos + " ");
            _sb.Append("WHERE ");
            _sb.Append("CODCLIENTE = ");
            _sb.Append("'" + pcodCliente + "' ");
            _sb.Append("AND ");
            _sb.Append("CODPRODUCTO = ");
            _sb.Append("'" + pcodProducto + "' ");
            _sb.Append("AND ");
            _sb.Append("CANTIDAD <= ");
            _sb.Append("REPLACE('" + pcantidad + "','.',',') ");
            _sb.Append("AND ");
            _sb.Append("DATE(FECHA_INICIA) <= DATE('now') ");
            _sb.Append("AND ");
            _sb.Append("DATE(FECHA_VENCE, '+1 day') >= DATE('now') ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readDecimal(_sb);
        }

        internal string obtenerTipoDescuento(string pcodCliente, string pcodProducto)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("TIPO_DESC ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._descuentos + " ");
            _sb.Append("WHERE ");
            _sb.Append("CODCLIENTE = ");
            _sb.Append("'" + pcodCliente + "' ");
            _sb.Append("AND ");
            _sb.Append("CODPRODUCTO = ");
            _sb.Append("'" + pcodProducto + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal string obtenerFechaInicio(string pcodCliente, string pcodProducto)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("STRFTIME('%d/%m/%Y', FECHA_INICIA ) FECHA_INICIA ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._descuentos + " ");
            _sb.Append("WHERE ");
            _sb.Append("CODCLIENTE = ");
            _sb.Append("'" + pcodCliente + "' ");
            _sb.Append("AND ");
            _sb.Append("CODPRODUCTO = ");
            _sb.Append("'" + pcodProducto + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

        internal string getDateExpires(string pcodCliente, string pcodProducto)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("STRFTIME('%d/%m/%Y', FECHA_VENCE ) FECHA_VENCE ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._descuentos + " ");
            _sb.Append("WHERE ");
            _sb.Append("CODCLIENTE = ");
            _sb.Append("'" + pcodCliente + "' ");
            _sb.Append("AND ");
            _sb.Append("CODPRODUCTO = ");
            _sb.Append("'" + pcodProducto + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }
    }
}
