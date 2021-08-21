using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
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
    internal class HelperInventario
    {
        internal DataTable BuscarInventario()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableInventario._CODCIA + ", ");
            _sb.Append(TableInventario._CODAGENTE + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableInventario._FECHATOMA + " ) " + TableInventario._FECHATOMA + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableInventario._FECHATOMACONSOLIDA + " ) " + TableInventario._FECHATOMACONSOLIDA + ", ");
            _sb.Append(TableInventario._CODPRODUCTO + ", ");
            _sb.Append("REPLACE(" + TableInventario._CANTIDAD + ",',','.') " + TableInventario._CANTIDAD + ", ");
            _sb.Append("REPLACE(" + TableInventario._VENTAS + ",',','.') " + TableInventario._VENTAS + ", ");
            _sb.Append("REPLACE(" + TableInventario._DEVOLUCIONESBUENAS + ",',','.') " + TableInventario._DEVOLUCIONESBUENAS + ", ");
            _sb.Append("REPLACE(" + TableInventario._DEVOLUCIONESMALAS + ",',','.') " + TableInventario._DEVOLUCIONESMALAS + ", ");
            _sb.Append("REPLACE(" + TableInventario._REGALIAS + ",',','.') " + TableInventario._REGALIAS + ", ");
            _sb.Append("REPLACE(" + TableInventario._ANULACIONES + ",',','.') " + TableInventario._ANULACIONES + ", ");
            _sb.Append("REPLACE(" + TableInventario._ANULACIONESBUENAS + ",',','.') " + TableInventario._ANULACIONESBUENAS + ", ");
            _sb.Append("REPLACE(" + TableInventario._ANULACIONESMALAS + ",',','.') " + TableInventario._ANULACIONESMALAS + ", ");
            _sb.Append("REPLACE(" + TableInventario._DISPONIBLE + ",',','.') " + TableInventario._DISPONIBLE + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableInventario._FECHACREA + " ) " + TableInventario._FECHACREA + ", ");
            _sb.Append("REPLACE(" + TableInventario._TOMA_FISICA + ",',','.') " + TableInventario._TOMA_FISICA + ", ");
            _sb.Append("REPLACE(" + TableInventario._AUDITORIA + ",',','.') " + TableInventario._AUDITORIA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._inventario);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            var DTSpecialFormat = DependencyService.Get<IFillDataSet_Table>();

            return DTSpecialFormat.DataTable_Format(MultiGeneric.uploadDataTable(_sb));
        }
    }
}
