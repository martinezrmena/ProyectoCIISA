using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers.Carniceria
{
    internal class HelperDetalleTransaccion
    {
        internal void buscarLineasDetalleImpresion(string pcodTransaction, string pcodTipoTransaccion, List<string> pprintingLinesList, Cliente pobjCliente)
        {             
            DataTable _dt = buscarDetalleImpresion(pcodTipoTransaccion, pcodTransaction);

            HelperGenericoCarniceria _helper = new HelperGenericoCarniceria();

            _helper.buscarLineasDetalleImpresion(
                pcodTransaction,
                pcodTipoTransaccion,
                pprintingLinesList,
                pobjCliente,
                _dt
                );
        }


        //*************************************************************************************************
        //Reemplazar por nueva consulta al tener nuevas tablas

        #region Reemplazar (Deprecated)
        private DataTable buscarDetalleImpresion(string pcodTipoTransaccion, string pcodTransaction)
        {
            StringBuilder _sb = new StringBuilder();

            if (pcodTipoTransaccion.Equals(ROLTransactions._devolucionSigla))
            {
                _sb = consultaNoDevolucion(pcodTransaction, pcodTipoTransaccion);
            }
            else
            {
                _sb = consultaDevolucion(pcodTransaction, pcodTipoTransaccion);
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        private StringBuilder consultaNoDevolucion(string pcodTransaction, string pcodTipoTransaccion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("DD." + TableDetalleDocumento._CODPRODUCTO + ", ");
            _sb.Append("DD." + TableDetalleDocumento._CANTIDAD + ", ");
            _sb.Append("DD." + TableDetalleDocumento._TOTALLINEA + ", ");
            _sb.Append("DD." + TableDetalleDocumento._TOTAL_IMP_LIN + ", ");
            _sb.Append("DD." + TableDetalleDocumento._PRECIO_UNI + ", ");
            _sb.Append("DD." + TableDetalleDocumento._MONTO_DESCUENTO + ", ");
            _sb.Append("DD." + TableDetalleDocumento._COMENTARIO + ", ");
            _sb.Append("DD." + TableDetalleDocumento._NUMLINEA + ", ");
            _sb.Append("DD." + TableDetalleDocumento._ESTADODEVOLUCION + ", ");
            _sb.Append("DD." + TableDetalleDocumento._COD_MOTIVO + ", ");
            _sb.Append("DD." + TableDetalleDocumento._EMBALAJE + ", ");
            _sb.Append("M." + TableMotivo._DESCRIPCION + ", ");
            _sb.Append("P." + TableProducto._DESCPRODUCTO + ", ");
            _sb.Append("P." + TableProducto._UNIDAD + ", ");
            _sb.Append("P." + TableProducto._EXENTO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._detalleDocumento + " DD, ");
            _sb.Append(TablesROL._motivo + " M, ");
            _sb.Append(TablesROL._producto + " P ");
            _sb.Append("WHERE ");
            _sb.Append("DD." + TableDetalleDocumento._CODDOCUMENTO + " = ");
            _sb.Append("'" + pcodTransaction + "' ");
            _sb.Append("AND ");
            _sb.Append("DD." + TableDetalleDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("'" + pcodTipoTransaccion + "' ");
            _sb.Append("AND ");
            _sb.Append("DD." + TableDetalleDocumento._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("DD." + TableDetalleDocumento._COD_MOTIVO + " = ");
            _sb.Append("M." + TableMotivo._CODIGO + " ");
            _sb.Append("AND ");
            _sb.Append("DD." + TableDetalleDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("M." + TableMotivo._TIPO_DOC + " ");
            _sb.Append("AND ");
            _sb.Append("DD." + TableDetalleDocumento._CODPRODUCTO + " = ");
            _sb.Append("P." + TableProducto._CODPRODUCTO + " ");

            return _sb;
        }

        private StringBuilder consultaDevolucion(string pcodTransaction, string pcodTipoTransaccion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("DD." + TableDetalleDocumento._CODPRODUCTO + ", ");
            _sb.Append("DD." + TableDetalleDocumento._CANTIDAD + ", ");
            _sb.Append("DD." + TableDetalleDocumento._TOTALLINEA + ", ");
            _sb.Append("DD." + TableDetalleDocumento._TOTAL_IMP_LIN + ", ");
            _sb.Append("DD." + TableDetalleDocumento._PRECIO_UNI + ", ");
            _sb.Append("DD." + TableDetalleDocumento._MONTO_DESCUENTO + ", ");
            _sb.Append("DD." + TableDetalleDocumento._COMENTARIO + ", ");
            _sb.Append("DD." + TableDetalleDocumento._NUMLINEA + ", ");
            _sb.Append("DD." + TableDetalleDocumento._ESTADODEVOLUCION + ", ");
            _sb.Append("DD." + TableDetalleDocumento._COD_MOTIVO + ", ");
            _sb.Append("DD." + TableDetalleDocumento._EMBALAJE + ", ");
            _sb.Append("P." + TableProducto._DESCPRODUCTO + ", ");
            _sb.Append("P." + TableProducto._UNIDAD + ", ");
            _sb.Append("P." + TableProducto._EXENTO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._detalleDocumento + " DD, ");
            _sb.Append(TablesROL._producto + " P ");
            _sb.Append("WHERE ");
            _sb.Append("DD." + TableDetalleDocumento._CODDOCUMENTO + " = ");
            _sb.Append("'" + pcodTransaction + "' ");
            _sb.Append("AND ");
            _sb.Append("DD." + TableDetalleDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("'" + pcodTipoTransaccion + "' ");
            _sb.Append("AND ");
            _sb.Append("DD." + TableDetalleDocumento._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("DD." + TableDetalleDocumento._CODPRODUCTO + " = ");
            _sb.Append("P." + TableProducto._CODPRODUCTO + " ");

            return _sb;
        }
        #endregion

        //*************************************************************************************************
    }
}
