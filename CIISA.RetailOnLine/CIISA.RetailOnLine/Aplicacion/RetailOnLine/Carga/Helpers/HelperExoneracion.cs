using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Exoneracion;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers
{
    /// <summary>
    /// Clase encargada de manejar la comunicación con BD de las exoneraciones.
    /// </summary>
    internal class HelperExoneracion
    {
        /// <summary>
        /// Permite insertar una exoneración
        /// </summary>
        /// <param name="pfila"></param>
        /// <returns></returns>
        internal StringBuilder insertTablaExoneracion(DataRow pfila)
        {
            string ARTICULO = string.IsNullOrEmpty(pfila[TableExoneracion._ARTICULO].ToString())? string.Empty : pfila[TableExoneracion._ARTICULO].ToString();
            string CAT = string.IsNullOrEmpty(pfila[TableExoneracion._CLASI_PR].ToString()) ? string.Empty: pfila[TableExoneracion._CLASI_PR].ToString();
            string SUBCAT = string.IsNullOrEmpty(pfila[TableExoneracion._SUBCLASI_PR].ToString()) ? string.Empty : pfila[TableExoneracion._SUBCLASI_PR].ToString();

            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._Exoneracion);
            _sb.Append("(");
            _sb.Append(string.Format("{0}, ", TableExoneracion._NO_CIA));
            _sb.Append(string.Format("{0}, ", TableExoneracion._GRUPO));
            _sb.Append(string.Format("{0}, ", TableExoneracion._NO_CLIENTE));
            _sb.Append(string.Format("{0}, ", TableExoneracion._FECHA_EXONERA));
            _sb.Append(string.Format("{0}, ", TableExoneracion._PORC_EXONERA));
            _sb.Append(string.Format("{0}, ", TableExoneracion._FECHA_FINALIZA));
            _sb.Append(string.Format("{0}, ", TableExoneracion._EXONERAID));
            if (!string.IsNullOrEmpty(ARTICULO))
            {
                _sb.Append(string.Format("{0}, ", TableExoneracion._ARTICULO));
            }
            if (!string.IsNullOrEmpty(CAT))
            {
                _sb.Append(string.Format("{0}, ", TableExoneracion._CLASI_PR));
            }
            if (!string.IsNullOrEmpty(SUBCAT))
            {
                _sb.Append(string.Format("{0}, ", TableExoneracion._SUBCLASI_PR));
            }
            _sb.Append(TableExoneracion._TIPO);
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append(string.Format("'{0}', ", pfila[TableExoneracion._NO_CIA]));
            _sb.Append(string.Format("'{0}', ", pfila[TableExoneracion._GRUPO]));
            _sb.Append(string.Format("'{0}', ", pfila[TableExoneracion._NO_CLIENTE]));
            _sb.Append(string.Format("'{0}', ", VarTime.convertDateTimeFromServiceToSQLitePlecaVersion(pfila[TableExoneracion._FECHA_EXONERA].ToString())));
            _sb.Append(string.Format("'{0}', ", pfila[TableExoneracion._PORC_EXONERA]));
            _sb.Append(string.Format("'{0}', ", VarTime.convertDateTimeFromServiceToSQLitePlecaVersion(pfila[TableExoneracion._FECHA_FINALIZA].ToString())));
            _sb.Append(string.Format("'{0}', ", pfila[TableExoneracion._EXONERAID]));
            if (!string.IsNullOrEmpty(ARTICULO))
            {
                _sb.Append(string.Format("'{0}', ", ARTICULO));
            }
            if (!string.IsNullOrEmpty(CAT))
            {
                _sb.Append(string.Format("'{0}', ", CAT));
            }
            if (!string.IsNullOrEmpty(SUBCAT))
            {
                _sb.Append(string.Format("'{0}', ", SUBCAT));
            }
            _sb.Append(string.Format("'{0}'", pfila[TableExoneracion._TIPO]));
            _sb.Append(")");

            return _sb;
        }

        /// <summary>
        /// Permite obtener por prioridad el elemento del porcentaje de exoneración asociado a un producto
        /// </summary>
        /// <param name="NO_CIA"></param>
        /// <param name="COD_ARTICULO"></param>
        /// <param name="CODCLIENTE"></param>
        /// <returns></returns>
        internal pnlExoneracion_ltvExoneracion consultarExoneracion(string NO_CIA, string COD_ARTICULO, string CODCLIENTE)
        {
            pnlExoneracion_ltvExoneracion Exoneracion = new pnlExoneracion_ltvExoneracion();
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("EX." + TableExoneracion._PORC_EXONERA + ", ");
            _sb.Append("EX." + TableExoneracion._EXONERAID + ", ");
            _sb.Append("EX." + TableExoneracion._TIPO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._Exoneracion + " EX, ");
            _sb.Append(TablesROL._producto + " INV ");
            _sb.Append("WHERE ");
            _sb.Append("EX." + TableExoneracion._NO_CIA + "=" + "'" + NO_CIA + "' ");
            _sb.Append("AND ");
            _sb.Append("INV." + TableProducto._CODPRODUCTO + " = ");
            _sb.Append("IFNULL(EX." + TableExoneracion._ARTICULO + ",'" + COD_ARTICULO + "') ");
            _sb.Append("AND ");
            _sb.Append("EX." + TableExoneracion._NO_CLIENTE + "= '" + CODCLIENTE + "' ");
            _sb.Append("AND ");
            _sb.Append("IFNULL(EX." + TableExoneracion._ARTICULO + ", '" + COD_ARTICULO + "') ='" + COD_ARTICULO + "' ");
            _sb.Append("AND ");
            _sb.Append("INV." + TableProducto._CAT + " = IFNULL(EX." + TableExoneracion._CLASI_PR + ", INV." + TableProducto._CAT + ") ");
            _sb.Append("AND ");
            _sb.Append("INV." + TableProducto._SUBCAT + "= IFNULL(EX." + TableExoneracion._SUBCLASI_PR + ", INV." + TableProducto._SUBCAT + ") ");
            _sb.Append("AND ");
            _sb.Append("DATE() BETWEEN EX." + TableExoneracion._FECHA_EXONERA + " AND IFNULL(EX." + TableExoneracion._FECHA_FINALIZA + ", DATE(DATE(), '+5 day')) ");
            _sb.Append("ORDER BY ex." + TableExoneracion._FECHA_EXONERA + " DESC LIMIT 1");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            foreach (DataRow _fila in _dt.Rows)
            {
                Exoneracion.PORC_EXONERA =  decimal.Parse(_fila[TableExoneracion._PORC_EXONERA].ToString());
                Exoneracion.TIPO = _fila[TableExoneracion._TIPO].ToString();
                Exoneracion.EXONERAID = _fila[TableExoneracion._EXONERAID].ToString();
            }

            return Exoneracion;
        }
    }
}





