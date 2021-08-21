using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Simbologia;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers
{
    /// <summary>
    /// Clase encargada de manejar la comunicacion con la tabla de Tipos de iva
    /// </summary>
    public class HelperSimbologia
    {

        /// <summary>
        /// Metodo que permite obtener la lista de simbologias relativas a tipos de iva
        /// </summary>
        /// <returns></returns>
        internal List<pnlSimbologia_ltvTiposIva> buscarListaSimbologia()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableTiposIVA._CLAVE_IVA + ", ");
            _sb.Append(TableTiposIVA._PORCENTAJE + ", ");
            _sb.Append(TableTiposIVA._SIMBOLO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._TiposIVA + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            List<pnlSimbologia_ltvTiposIva> ListaTiposIVA = new List<pnlSimbologia_ltvTiposIva>();

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlSimbologia_ltvTiposIva _lvi = new pnlSimbologia_ltvTiposIva();

                int i = 0;

                int.TryParse(_fila[TableTiposIVA._CLAVE_IVA].ToString(), out i);

                _lvi.CLAVE_IVA = i.ToString();

                _lvi.PORCENTAJE = decimal.Parse(_fila[TableTiposIVA._PORCENTAJE].ToString());

                _lvi.SIMBOLO = _fila[TableTiposIVA._SIMBOLO].ToString();

                ListaTiposIVA.Add(_lvi);
            }

            return ListaTiposIVA;
        }

        /// <summary>
        /// Metodo que permite obtener el articulo de la tabla de productos
        /// </summary>
        /// <param name="Cod_Articulo"></param>
        /// <returns></returns>
        internal pnlTransacciones_ltvProductos buscarArticulo(string Cod_Articulo)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableProducto._CODPRODUCTO + ", ");
            _sb.Append(TableProducto._CLAVE_IVA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._producto + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableProducto._CODPRODUCTO + "= ");
            _sb.Append("'" + Cod_Articulo + "' ");
            _sb.Append("LIMIT 1");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            pnlTransacciones_ltvProductos Articulo = new pnlTransacciones_ltvProductos();

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlTransacciones_ltvProductos _lvi = new pnlTransacciones_ltvProductos();

                int i = 0;

                int.TryParse(_fila[TableProducto._CLAVE_IVA].ToString(), out i);

                _lvi.Clave_IVA = i.ToString();

                _lvi._pt_codigo = _fila[TableProducto._CODPRODUCTO].ToString();

                Articulo = _lvi;
            }

            return Articulo;
        }

        /// <summary>
        /// Se obtiene el simbolo que es relativo a un producto
        /// </summary>
        /// <param name="ListaSimbologia"></param>
        /// <param name="Articulo"></param>
        /// <returns></returns>
        internal string buscarArticuloSimbolo(List<pnlSimbologia_ltvTiposIva> ListaSimbologia, pnlTransacciones_ltvProductos Articulo)
        {
            string simbolo = string.Empty;

            if (ListaSimbologia.Count() > 0 && Articulo != null)
            {
                var objeto = ListaSimbologia.Where(x => x.CLAVE_IVA == Articulo.Clave_IVA).FirstOrDefault();
                if (objeto != null)
                {
                    simbolo = " " + objeto.SIMBOLO;
                }
            }

            return simbolo;
        }
    }
}
