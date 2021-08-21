using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    internal class HelperRuta
    {

        internal StringBuilder insertTablaRuta(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._ruta);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableRuta._CODRUTA));
            _sb.Append(string.Format("{0}, ", TableRuta._DESCRUTA));
            _sb.Append(string.Format("{0}, ", TableRuta._CONSECUTIVODOCUMENTO));
            _sb.Append(TableRuta._CONSECUTIVORECIBO);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["COD_RUTA"]));
            _sb.Append(string.Format("'{0}', ", pfila["DESCRIPCION"]));
            _sb.Append(string.Format("'{0}', ", pfila["CONSECUTIVO_DOC"]));
            _sb.Append(string.Format("'{0}'", pfila["CONSECUTIVO_REC"]));
            _sb.Append(")");

            return _sb;
        }

    }
}
