using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    public class HelperProducto
    {

        public StringBuilder insertTablaProducto(DataRow pfila)
        {
            StringBuilder _sb = new StringBuilder();

            string viscera = pfila["ES_VISCERA"].ToString();
            string iva = pfila.Table.Columns.Contains("PORCENTAJE_IVA") ? pfila["PORCENTAJE_IVA"].ToString() : Numeric._zeroInteger.ToString();
            string CAT = pfila.Table.Columns.Contains(TableProducto._CAT) ? pfila[TableProducto._CAT].ToString() : string.Empty;
            string SUBCAT = pfila.Table.Columns.Contains(TableProducto._SUBCAT) ? pfila[TableProducto._SUBCAT].ToString() : string.Empty;
            string CLAVE_IVA = pfila.Table.Columns.Contains(TableProducto._CLAVE_IVA) ? pfila[TableProducto._CLAVE_IVA].ToString() : string.Empty;

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._producto);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableProducto._CODPRODUCTO));
            _sb.Append(string.Format("{0}, ", TableProducto._DESCPRODUCTO));
            _sb.Append(string.Format("{0}, ", TableProducto._CODCLASIFICACION));
            _sb.Append(string.Format("{0}, ", TableProducto._PEDIRPAQUETERIA));
            _sb.Append(string.Format("{0}, ", TableProducto._EXENTO));

            if (!string.IsNullOrEmpty(viscera))
            {
                _sb.Append(string.Format("{0}, ", TableProducto._UNIDAD));
                _sb.Append(string.Format("{0} ", TableProducto._ES_VISCERA));
            }
            else
            {
                _sb.Append(string.Format("{0} ", TableProducto._UNIDAD));
            }

            if (!string.IsNullOrEmpty(iva))
            {
                _sb.Append(string.Format(" ,{0} ", TableProducto._PORCENTAJE_IVA));
            }

            if (!string.IsNullOrEmpty(CAT))
            {
                _sb.Append(string.Format(" ,{0} ", TableProducto._CAT));
            }

            if (!string.IsNullOrEmpty(SUBCAT))
            {
                _sb.Append(string.Format(" ,{0} ", TableProducto._SUBCAT));
            }

            if (!string.IsNullOrEmpty(CLAVE_IVA))
            {
                _sb.Append(string.Format(" ,{0} ", TableProducto._CLAVE_IVA));
            }

            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila["ARTICULO"]));
            _sb.Append(string.Format("'{0}', ", pfila["NOM_ARTICULO"]));
            _sb.Append(string.Format("'{0}', ", pfila["CATEGORIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["PAQUETERIA"]));
            _sb.Append(string.Format("'{0}', ", pfila["IMP"]));

            if (!string.IsNullOrEmpty(viscera))
            {
                _sb.Append(string.Format("'{0}', ", pfila["UNIDAD"]));
                _sb.Append(string.Format("'{0}'", pfila["ES_VISCERA"]));
            }
            else
            {
                _sb.Append(string.Format("'{0}' ", pfila["UNIDAD"]));
            }

            if (!string.IsNullOrEmpty(iva))
            {
                _sb.Append(string.Format(" ,{0} ", iva));
            }

            if (!string.IsNullOrEmpty(CAT))
            {
                _sb.Append(string.Format(" ,{0} ", CAT));
            }

            if (!string.IsNullOrEmpty(SUBCAT))
            {
                _sb.Append(string.Format(" ,{0} ", SUBCAT));
            }

            if (!string.IsNullOrEmpty(CLAVE_IVA))
            {
                _sb.Append(string.Format(" ,{0} ", CLAVE_IVA));
            }

            _sb.Append(")");

            return _sb;
        }

    }
}
