using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    public class HelperMensajeFactura
    {
        public StringBuilder insertTablaMensajeFactura(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._MensajeFactura);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableMensajeFactura._NO_CIA));
            _sb.Append(string.Format("{0}, ", TableMensajeFactura._COMENTARIO));
            _sb.Append(string.Format("{0}, ", TableMensajeFactura._COMENTARIO1));
            _sb.Append(string.Format("{0}, ", TableMensajeFactura._COMENTARIO2));
            _sb.Append(string.Format("{0}, ", TableMensajeFactura._COMENTARIO3));
            _sb.Append(TableMensajeFactura._COMENTARIO4);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["" + TableMensajeFactura._NO_CIA]));
            _sb.Append(string.Format("'{0}', ", pfila["" + TableMensajeFactura._COMENTARIO]));
            _sb.Append(string.Format("'{0}', ", pfila["" + TableMensajeFactura._COMENTARIO1]));
            _sb.Append(string.Format("'{0}', ", pfila["" + TableMensajeFactura._COMENTARIO2]));
            _sb.Append(string.Format("'{0}', ", pfila["" + TableMensajeFactura._COMENTARIO3]));
            _sb.Append(string.Format("'{0}'", pfila["" + TableMensajeFactura._COMENTARIO4]));
            _sb.Append(")");

            return _sb;
        }
    }
}
