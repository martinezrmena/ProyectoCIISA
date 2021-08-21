using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Respaldo.Helpers.Carniceria
{
    public class HelperDetalleReses
    {
        internal void RespaldarDetalleReses()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT INTO ");
            _sb.Append(TablesROL._DetalleResesBK);
            _sb.Append(" (NO_CIA, ");
            _sb.Append(" NUM_PEDIDO, ");
            _sb.Append(" NO_CLIENTE, ");
            _sb.Append(" ARTICULO, ");
            _sb.Append(" IND_TIPO, ");
            _sb.Append(" FECHA_MATANZA, ");
            _sb.Append(" LOTE, ");
            _sb.Append(" NO_ANIMAL, ");
            _sb.Append(" TIPO_PORCION, ");
            _sb.Append(" PESO, ");
            _sb.Append(" COMPROMETIDO, ");
            _sb.Append(" VENDIDO, ");
            _sb.Append(" ENVIADO, ");
            _sb.Append(" VASIGNADO) ");
            _sb.Append("SELECT ");
            _sb.Append(" NO_CIA, ");
            _sb.Append(" NUM_PEDIDO, ");
            _sb.Append(" NO_CLIENTE, ");
            _sb.Append(" ARTICULO, ");
            _sb.Append(" IND_TIPO, ");
            _sb.Append(" FECHA_MATANZA, ");
            _sb.Append(" LOTE, ");
            _sb.Append(" NO_ANIMAL, ");
            _sb.Append(" TIPO_PORCION, ");
            _sb.Append(" PESO, ");
            _sb.Append(" COMPROMETIDO, ");
            _sb.Append(" VENDIDO, ");
            _sb.Append(" ENVIADO, ");
            _sb.Append(" VASIGNADO ");
            _sb.Append(" FROM ");
            _sb.Append(TablesROL._DetalleReses);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.InsertRecordBackUp(_sb);
        }
    }
}
