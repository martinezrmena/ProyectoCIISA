using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    public class HelperFactura
    {
        public DataTable buscarFacturas()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableFactura._NO_FISICO + ", ");
            _sb.Append(TableFactura._NO_CLIENTE + ", ");
            _sb.Append(TableFactura._SALDO + ", ");
            _sb.Append(TableFactura._MONTOORIGINAL + ", ");
            _sb.Append(TableFactura._FECHA_DOCUMENTO + ", ");
            _sb.Append(TableFactura._FECHA_VENCE + ", ");
            _sb.Append(TableFactura._ENVIADO + ", ");
            _sb.Append(TableFactura._ANULADO + ", ");
            _sb.Append(TableFactura._TRAMITE + ", ");
            _sb.Append(TableFactura._CREADA + ", ");
            _sb.Append(TableFactura._TIPO_DOC + ", ");
            _sb.Append(TableFactura._NUM_ESTABLECIMIENTO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._factura);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        public StringBuilder insertTablaFactura(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._factura);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableFactura._NO_FISICO));
            _sb.Append(string.Format("{0}, ", TableFactura._NO_CLIENTE));
            _sb.Append(string.Format("{0}, ", TableFactura._SALDO));
            _sb.Append(string.Format("{0}, ", TableFactura._FECHA_DOCUMENTO));
            _sb.Append(string.Format("{0}, ", TableFactura._FECHA_VENCE));
            _sb.Append(string.Format("{0}, ", TableFactura._ENVIADO));
            _sb.Append(string.Format("{0}, ", TableFactura._ANULADO));
            _sb.Append(string.Format("{0}, ", TableFactura._TRAMITE));
            _sb.Append(string.Format("{0}, ", TableFactura._CREADA));
            _sb.Append(string.Format("{0}, ", TableFactura._MONTOORIGINAL));
            _sb.Append(string.Format("{0}, ", TableFactura._TIPO_DOC));
            _sb.Append(string.Format("{0}, ", TableFactura._NUM_ESTABLECIMIENTO));
            _sb.Append(TableFactura._FECHA_INSERT);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["NO_FISICO"]));
            _sb.Append(string.Format("'{0}', ", pfila["NO_CLIENTE"]));
            _sb.Append(string.Format("{0}, ", pfila["SALDO"]));
            //_sb.Append(string.Format("'{0}', ", pfila["FECHA_DOCUMENTO"]));
            _sb.Append(string.Format("'{0}', ", VarTime.convertDateTimeFromServiceToSQLite(pfila["FECHA_DOCUMENTO"].ToString())));
            //_sb.Append(string.Format("'{0}', ", pfila["FECHA_VENCE"]));
            _sb.Append(string.Format("'{0}', ", VarTime.convertDateTimeFromServiceToSQLite(pfila["FECHA_VENCE"].ToString())));
            _sb.Append(string.Format("'{0}', ", SQL._Si));
            _sb.Append(string.Format("'{0}', ", SQL._No));
            _sb.Append(string.Format("'{0}', ", SQL._No));
            _sb.Append(string.Format("'{0}', ", SQL._NAF));
            _sb.Append(string.Format("{0}, ", pfila["M_ORIGINAL"]));
            _sb.Append(string.Format("'{0}', ", pfila["TIPO_DOC"]));
            _sb.Append(string.Format("'{0}', ", pfila["NUM_ESTABLECIMIENTO"]));
            _sb.Append(string.Format("'{0}'", VarTime.getDateTimeSQLiteComplete(DateTime.Now)));
            _sb.Append(")");

            return _sb;
        }
    }
}
