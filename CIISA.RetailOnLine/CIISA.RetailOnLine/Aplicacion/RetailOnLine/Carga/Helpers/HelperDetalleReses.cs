using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    public class HelperDetalleReses
    {
        public StringBuilder insertTablaDetalleReses(DataRow pfila)
        {

            DateTime _fechaMantaza = VarTime.convertStringToDateTime(
                                    pfila["" + TableDetalleReses._FECHA_MATANZA].ToString()
                                    );

            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._DetalleReses);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableDetalleReses._NO_CIA));
            _sb.Append(string.Format("{0}, ", TableDetalleReses._NUM_PEDIDO));
            _sb.Append(string.Format("{0}, ", TableDetalleReses._NO_CLIENTE));
            _sb.Append(string.Format("{0}, ", TableDetalleReses._ARTICULO));
            _sb.Append(string.Format("{0}, ", TableDetalleReses._IND_TIPO));
            _sb.Append(string.Format("{0}, ", TableDetalleReses._FECHA_MATANZA));
            _sb.Append(string.Format("{0}, ", TableDetalleReses._LOTE));
            _sb.Append(string.Format("{0}, ", TableDetalleReses._NO_ANIMAL));
            _sb.Append(string.Format("{0}, ", TableDetalleReses._TIPO_PORCION));
            _sb.Append(TableDetalleReses._PESO);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["" + TableDetalleReses._NO_CIA]));
            _sb.Append(string.Format("{0}, ", pfila["" + TableDetalleReses._NUM_PEDIDO]));
            _sb.Append(string.Format("'{0}', ", pfila["" + TableDetalleReses._NO_CLIENTE]));
            _sb.Append(string.Format("'{0}', ", pfila["" + TableDetalleReses._ARTICULO]));
            _sb.Append(string.Format("'{0}', ", pfila["" + TableDetalleReses._IND_TIPO]));
            _sb.Append(string.Format("'{0}', ", VarTime.getDateTimeSqlite(_fechaMantaza)));
            _sb.Append(string.Format("'{0}', ", pfila["" + TableDetalleReses._LOTE]));
            _sb.Append(string.Format("'{0}', ", pfila["" + TableDetalleReses._NO_ANIMAL]));
            _sb.Append(string.Format("'{0}', ", pfila["" + TableDetalleReses._TIPO_PORCION]));
            _sb.Append(string.Format("{0} ", pfila["" + TableDetalleReses._PESO]));
            _sb.Append(")");

            return _sb;
        }
    }
}
