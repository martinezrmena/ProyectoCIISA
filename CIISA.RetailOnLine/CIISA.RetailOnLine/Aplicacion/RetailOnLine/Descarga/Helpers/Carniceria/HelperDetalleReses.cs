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

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Helpers.Carniceria
{
    internal class HelperDetalleReses
    {
        internal DataTable buscarDetallesResesSinEnviar()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableDetalleReses._NO_CIA + ", ");
            _sb.Append(TableDetalleReses._NUM_PEDIDO + ", ");
            _sb.Append(TableDetalleReses._NO_CLIENTE + ", ");
            _sb.Append(TableDetalleReses._ARTICULO + ", ");
            _sb.Append(TableDetalleReses._IND_TIPO + ", ");
            _sb.Append(TableDetalleReses._FECHA_MATANZA + ", ");
            _sb.Append(TableDetalleReses._LOTE + ", ");
            _sb.Append(TableDetalleReses._NO_ANIMAL + ", ");
            _sb.Append(TableDetalleReses._TIPO_PORCION + ", ");
            _sb.Append(TableDetalleReses._COMPROMETIDO + ", ");
            _sb.Append(TableDetalleReses._VENDIDO + ", ");
            _sb.Append(TableDetalleReses._ENVIADO + ", ");
            _sb.Append(TableDetalleReses._PESO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._DetalleReses + " ");
            _sb.Append("WHERE ");
            _sb.Append("ENVIADO ='" + SQL._No + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            var DTSpecialFormat = DependencyService.Get<IFillDataSet_Table>();

            return DTSpecialFormat.DataTable_Format(MultiGeneric.uploadDataTable(_sb));
        }

    }
}
