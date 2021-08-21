using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Time;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    public class HelperEncabezadoPedido
    {
        public StringBuilder insertTablaEncabezadoPedido(DataRow pfila)
        {

            string PEDIDO_PLANTA = pfila.Table.Columns.Contains(TableEncabezadoPedido._PEDIDO_PLANTA) ? pfila[TableEncabezadoPedido._PEDIDO_PLANTA].ToString() : string.Empty;
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._encabezadoPedido);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableEncabezadoPedido._NO_CIA));
            _sb.Append(string.Format("{0}, ", TableEncabezadoPedido._NO_TRANSA));
            _sb.Append(string.Format("{0}, ", TableEncabezadoPedido._NO_CLIENTE));
            _sb.Append(string.Format("{0}, ", TableEncabezadoPedido._NO_ESTABLECIMIENTO));
            _sb.Append(string.Format("{0}, ", TableEncabezadoPedido._NO_AGENTE));
            _sb.Append(string.Format("{0}, ", TableEncabezadoPedido._FECHA_CREA));
            _sb.Append(string.Format("{0}, ", TableEncabezadoPedido._FECHA_ENTREGA));
            _sb.Append(string.Format("{0}, ", TableEncabezadoPedido._TOTAL));
            _sb.Append(string.Format("{0}, ", TableEncabezadoPedido._TOTAL_IMP));
            _sb.Append(string.Format("{0}, ", TableEncabezadoPedido._IND_AUTOMATICO));
            _sb.Append(string.Format("{0}, ", TableEncabezadoPedido._ENVIADO));
            _sb.Append(string.Format("{0}, ", TableEncabezadoPedido._APLICADO));
            _sb.Append(TableEncabezadoPedido._PEDIDO_PLANTA);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["NO_CIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["NO_TRANSA"]));
            _sb.Append(string.Format("'{0}', ", pfila["NO_CLIENTE"]));
            _sb.Append(string.Format("{0}, ", pfila["NO_ESTABLECIMIENTO"]));
            _sb.Append(string.Format("'{0}', ", pfila["NO_AGENTE"]));
            //_sb.Append(string.Format("'{0}', ", pfila["FECHA_CREA"]));
            _sb.Append(string.Format("'{0}', ", VarTime.convertDateTimeFromServiceToSQLitePlecaVersion(pfila["FECHA_CREA"].ToString())));
            //_sb.Append(string.Format("'{0}', ", pfila["FECHA_ENTREGA"]));
            _sb.Append(string.Format("'{0}', ", VarTime.convertDateTimeFromServiceToSQLitePlecaVersion(pfila["FECHA_ENTREGA"].ToString())));
            _sb.Append(string.Format("{0}, ", pfila["TOTAL"]));
            _sb.Append(string.Format("{0}, ", pfila["TOTAL_IMP"]));
            _sb.Append(string.Format("'{0}', ", pfila["IND_AUTOMATICO"]));
            _sb.Append(string.Format("'{0}', ", SQL._No));
            _sb.Append(string.Format("'{0}', ", SQL._No));
            _sb.Append(string.Format("'{0}' ", PEDIDO_PLANTA));
            _sb.Append(")");

            return _sb;
        }

    }
}
