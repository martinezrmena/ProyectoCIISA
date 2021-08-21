using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    public class HelperDetallePedido
    {

        public StringBuilder insertTablaDetallePedido(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            string tipo_doc = ROLTransactions._Factura;

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._detallePedido);
            _sb.Append("(");
            _sb.Append("NO_CIA, ");
            _sb.Append(string.Format("{0}, ", TableDetallePedido._NO_TRANSA));
            _sb.Append(string.Format("{0}, ", TableDetallePedido._NO_LINEA));
            _sb.Append(string.Format("{0}, ", TableDetallePedido._ARTI_PED));
            _sb.Append(string.Format("{0}, ", TableDetallePedido._ARTI_DES));
            _sb.Append(string.Format("{0}, ", TableDetallePedido._CAN_PED));
            _sb.Append(string.Format("{0}, ", TableDetallePedido._CAN_DES));
            _sb.Append(string.Format("{0}, ", TableDetallePedido._COMENTARIO));
            _sb.Append(string.Format("{0}, ", TableDetallePedido._TOTAL_LIN));
            _sb.Append(string.Format("{0}, ", TableDetallePedido._TIPO_DOC));
            _sb.Append(string.Format("{0}, ", TableDetallePedido._TOTAL_IMP_LIN));
            _sb.Append(string.Format("{0}, ", TableDetallePedido._NO_AGENTE));
            _sb.Append(string.Format("{0}, ", TableDetallePedido._FECHA_CREA));
            _sb.Append(string.Format("{0}, ", TableDetallePedido._USUARIO_CREA));
            _sb.Append(string.Format("{0}, ", TableDetallePedido._ENVIADO));
            _sb.Append(string.Format("{0}, ", TableDetallePedido._APLICADO));
            _sb.Append(TableDetallePedido._ES_FACTURA);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["NO_CIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["NO_TRANSA"]));
            _sb.Append(string.Format("'{0}', ", pfila["NO_LINEA"]));
            _sb.Append(string.Format("'{0}', ", pfila["ARTI_PED"]));
            _sb.Append(string.Format("'{0}', ", pfila["ARTI_DES"]));
            _sb.Append(string.Format("{0}, ", pfila["CAN_PED"]));
            _sb.Append(string.Format("{0}, ", pfila["CAN_DES"]));
            _sb.Append(string.Format("'{0}', ", pfila["COMENTARIO"]));
            _sb.Append(string.Format("{0}, ", pfila["TOTAL_LIN"]));
            _sb.Append(string.Format("'{0}', ", tipo_doc));
            _sb.Append(string.Format("{0}, ", pfila["TOTAL_IMP_LIN"]));
            _sb.Append(string.Format("'{0}', ", pfila["NO_AGENTE"]));
            _sb.Append(string.Format("'{0}', ", VarTime.convertDateTimeFromServiceToSQLitePlecaVersion(pfila["FECHA_CREA"].ToString())));
            _sb.Append(string.Format("'{0}', ", pfila["USUARIO_CREA"]));
            _sb.Append(string.Format("'{0}', ", SQL._No));
            _sb.Append(string.Format("'{0}', ", SQL._No));

            if (!string.IsNullOrEmpty(tipo_doc) && 
                (tipo_doc.Contains(ROLTransactions._Factura) || (tipo_doc.Contains(ROLTransactions._Pedido))))
            {
                _sb.Append(string.Format("'{0}' ", SQL._Si));
            }
            else
            {
                _sb.Append(string.Format("'{0}' ", SQL._No));
            }

            _sb.Append(")");

            return _sb;
        }

        public decimal ConsultarComprometidoPedido(string cod_producto)
        {
            decimal result = 0;

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            try
            {
                StringBuilder _sb = new StringBuilder();

                _sb.Append("SELECT ");
                _sb.Append("SUM(" + TableDetallePedido._CAN_DES + ") " + TableDetallePedido._CAN_DES + " ");
                _sb.Append("FROM ");
                _sb.Append(TablesROL._detallePedido + " ");
                _sb.Append("WHERE ");
                _sb.Append(TableDetallePedido._ARTI_DES + " = ");
                _sb.Append("'" + cod_producto + "' ");
                _sb.Append("AND ");
                _sb.Append(TableDetallePedido._APLICADO + " = ");
                _sb.Append("'" + SQL._No + "' ");
                _sb.Append(" AND ");
                _sb.Append("ENVIADO = '" + SQL._No + "'");
                _sb.Append(" AND ");
                _sb.Append("ES_FACTURA = '" + SQL._Si + "' ");
                _sb.Append("GROUP BY ");
                _sb.Append(TableDetallePedido._ARTI_DES + " ");

                result = MultiGeneric.readDecimal(_sb);
            }
            catch (Exception ex)
            {
                result = -1;
            }

            return result;

        }

    }
}
