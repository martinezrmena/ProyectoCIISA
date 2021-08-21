using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperPrecioProducto
    {
        internal decimal buscarPrecio(string pcodListaPrecios, string pcodProducto)
        {
            decimal _precio = 0;
            bool _existePrecio = false;

            StringBuilder _sb = new StringBuilder();

            _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("PRECIO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._precioProducto + " ");
            _sb.Append("WHERE ");
            _sb.Append("CODPRODUCTO = ");
            _sb.Append("'" + pcodProducto + "' ");
            _sb.Append("AND ");
            _sb.Append("CODLISTAPRECIO = ");
            _sb.Append("'" + pcodListaPrecios + "'");

            _existePrecio = OperationSQL.thereRecord(_sb, "PRECIO");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            if (_existePrecio)
            {
                _sb = new StringBuilder();

                _sb.Append("SELECT ");
                _sb.Append("PRECIO ");
                _sb.Append("FROM ");
                _sb.Append(TablesROL._precioProducto + " ");
                _sb.Append("WHERE ");
                _sb.Append("CODPRODUCTO = ");
                _sb.Append("'" + pcodProducto + "' ");
                _sb.Append("AND ");
                _sb.Append("CODLISTAPRECIO = ");
                _sb.Append("'" + pcodListaPrecios + "'");

                _precio = MultiGeneric.readDecimal(_sb);
            }
            else
            {
                _sb = new StringBuilder();

                _sb.Append("SELECT ");
                _sb.Append("PRECIO ");
                _sb.Append("FROM ");
                _sb.Append(TablesROL._precioProducto + " ");
                _sb.Append("WHERE ");
                _sb.Append("CODPRODUCTO = ");
                _sb.Append("'" + pcodProducto + "' ");
                _sb.Append("AND ");
                _sb.Append("CODLISTAPRECIO = ");
                _sb.Append("'" + Numeric._oneInteger + "'");

                _precio = MultiGeneric.readDecimal(_sb);
            }

            return _precio;
        }
    }
}
