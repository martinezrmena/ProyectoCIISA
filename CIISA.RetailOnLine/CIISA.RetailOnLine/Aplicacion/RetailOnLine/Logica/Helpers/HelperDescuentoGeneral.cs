using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    public class HelperDescuentoGeneral
    {

        internal decimal obtenerDescuentoGeneral(string pcodCliente, string pcodProducto, decimal pcantidad)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("SUM(porcentaje) ");
            _sb.Append("PORCDESCUENTO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._descuentoGeneral + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_CLIENTE = ");
            _sb.Append("'" + pcodCliente + "' ");
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

        internal string obtenerTipoDescuentoGeneral(string pcodCliente, string pcodProducto)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("TIPO_DESC ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._descuentoGeneral + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_CLIENTE = ");
            _sb.Append("'" + pcodCliente + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }

    }
}
