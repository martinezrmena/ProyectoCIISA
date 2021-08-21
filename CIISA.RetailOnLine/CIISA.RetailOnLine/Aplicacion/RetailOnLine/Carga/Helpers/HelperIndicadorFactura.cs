using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    public class HelperIndicadorFactura
    {

        public StringBuilder insertTablaIndicadorFactura(DataRow pfila)
        {
            string Tramite_Fact = pfila.Table.Columns.Contains("TRAMITE_FACT") ? pfila["TRAMITE_FACT"].ToString() : SQL._No;

            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._indicadorFactura);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableIndicadorFactura._NO_CIA));
            _sb.Append(string.Format("{0}, ", TableIndicadorFactura._NO_CLIENTE));
            _sb.Append(string.Format("{0}, ", TableIndicadorFactura._IND_PED));
            _sb.Append(string.Format("{0}, ", TableIndicadorFactura._IND_FACCONT));
            _sb.Append(string.Format("{0}, ", TableIndicadorFactura._IND_FACCRED));
            _sb.Append(string.Format("{0}, ", TableIndicadorFactura._IND_RESPETA_LIMITE));
            _sb.Append(string.Format("{0}, ", TableIndicadorFactura._IND_CHEQUE));
            _sb.Append(string.Format("{0}, ", TableIndicadorFactura._MONTO_LIMITE));
            _sb.Append(string.Format("{0}, ", TableIndicadorFactura._IND_VENCIMIENTO));
            _sb.Append(string.Format("{0}, ", TableIndicadorFactura._IND_ESTADO));
            _sb.Append(string.Format("{0}, ", TableIndicadorFactura._NO_AGENTE));
            _sb.Append(string.Format("{0}, ", TableIndicadorFactura._COBRADOR));
            _sb.Append(string.Format("{0}, ", TableIndicadorFactura._IND_COBRO));
            //_sb.Append(TableIndicadorFactura._NO_ESTABLECIMIENTO);
            //Validacion 50mts
            _sb.Append(string.Format("{0}, ", TableIndicadorFactura._NO_ESTABLECIMIENTO));
            _sb.Append(string.Format("{0},", TableIndicadorFactura._IND_GEO));
            //Validacion Factura Electronica
            _sb.Append(string.Format("{0},", TableIndicadorFactura._IND_NUM_ORDEN));
            _sb.Append(string.Format("{0},", TableIndicadorFactura._IND_FECHA_ORDEN));
            _sb.Append(string.Format("{0},", TableIndicadorFactura._IND_NUM_RECEPCION));
            _sb.Append(string.Format("{0},", TableIndicadorFactura._IND_FECHA_RECEPCION));
            _sb.Append(string.Format("{0},", TableIndicadorFactura._IND_NUM_RECLAMO));
            _sb.Append(string.Format("{0},", TableIndicadorFactura._IND_FECHA_RECLAMO));
            _sb.Append(string.Format("{0}, ", TableIndicadorFactura._IND_COD_PROVEEDOR));
            _sb.Append(string.Format("{0} ", TableIndicadorFactura._TRAMITE_FACT));
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["NO_CIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["NO_CLIENTE"]));
            _sb.Append(string.Format("'{0}', ", pfila["IND_PED"]));
            _sb.Append(string.Format("'{0}', ", pfila["IND_FACCONT"]));
            _sb.Append(string.Format("'{0}', ", pfila["IND_FACCRED"]));
            _sb.Append(string.Format("'{0}', ", pfila["IND_RESPETA_LIMITE"]));
            _sb.Append(string.Format("'{0}', ", pfila["IND_CHEQUE"]));
            _sb.Append(string.Format("{0}, ", pfila["MONTO_LIMITE"]));
            _sb.Append(string.Format("'{0}', ", pfila["IND_VENCIMIENTO"]));
            _sb.Append(string.Format("'{0}', ", pfila["IND_ESTADO"]));
            _sb.Append(string.Format("'{0}', ", pfila["NO_AGENTE"]));
            _sb.Append(string.Format("'{0}', ", pfila["COBRADOR"]));
            _sb.Append(string.Format("'{0}', ", pfila["IND_COBRO"]));
            //_sb.Append(string.Format("'{0}'", pfila["NO_ESTABLECIMIENTO"]));
            //Validacion 50mts
            _sb.Append(string.Format("'{0}', ", pfila["NO_ESTABLECIMIENTO"]));
            _sb.Append(string.Format("'{0}', ", pfila["IND_GEO"]));
            //Validacion Factura Electronica
            _sb.Append(string.Format("'{0}', ", pfila["IND_NUM_ORDEN"]));
            _sb.Append(string.Format("'{0}', ", pfila["IND_FECHA_ORDEN"]));
            _sb.Append(string.Format("'{0}', ", pfila["IND_NUM_RECEPCION"]));
            _sb.Append(string.Format("'{0}', ", pfila["IND_FECHA_RECEPCION"]));
            _sb.Append(string.Format("'{0}', ", pfila["IND_NUM_RECLAMO"]));
            _sb.Append(string.Format("'{0}', ", pfila["IND_FECHA_RECLAMO"]));
            _sb.Append(string.Format("'{0}', ", pfila["IND_COD_PROVEEDOR"]));
            _sb.Append(string.Format("'{0}' ", pfila["TRAMITE_FACT"]));
            _sb.Append(")");

            return _sb;
        }

    }
}
