using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo.Carniceria;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers
{
    internal class HelperDetalleTransaccion
    {
        internal void buscarLineasDetalleImpresion(string pcodTransaction,string pcodTipoTransaccion,List<string> pprintingLinesList,Cliente pobjCliente, bool DevolucionFactura = false)
        {
            if (!DevolucionFactura)
            {
                DataTable _dt = buscarDetalleImpresion(pcodTipoTransaccion, pcodTransaction);

                HelperGenerico _helper = new HelperGenerico();

                _helper.buscarLineasDetalleImpresion(
                    pcodTransaction,
                    pcodTipoTransaccion,
                    pprintingLinesList,
                    pobjCliente,
                    _dt
                    );
            }
            else
            {
                DataTable _dt = buscarDetalleImpresion(pcodTipoTransaccion, pcodTransaction);

                if (pobjCliente.v_objTransaccion.v_listaDetalles.Count == 0)
                {
                    foreach (DataRow _fila in _dt.Rows)
                    {
                        TransaccionDetalle transaccionDetalle = new TransaccionDetalle();

                        int numlinea = 0;
                        int.TryParse(_fila[TableDetalleDocumento._NUMLINEA].ToString(), out numlinea);
                        transaccionDetalle.v_numLinea = numlinea;

                        transaccionDetalle.v_objProducto = new Producto();
                        transaccionDetalle.v_objProducto.v_codProducto = _fila[TableDetalleDocumento._CODPRODUCTO].ToString();

                        pobjCliente.v_objTransaccion.v_listaDetalles.Add(transaccionDetalle);
                    }
                }

                Logica_ManagerCarniceria plogica = new Logica_ManagerCarniceria();
                //Buscar los detalles reses
                foreach (var detalle in pobjCliente.v_objTransaccion.v_listaDetalles)
                {
                    if (plogica.EsDetalleRes(detalle.v_objProducto.v_codProducto))
                    {
                        detalle.v_listaDetalleReses = plogica.buscarDetalleResesFactura(detalle.v_objProducto.v_codProducto, pobjCliente.v_no_cliente, pobjCliente.v_objTransaccion.v_codFactura);
                    }
                }

                HelperGenerico _helper = new HelperGenerico();

                _helper.buscarLineasDetalleImpresion(
                    pcodTransaction,
                    pcodTipoTransaccion,
                    pprintingLinesList,
                    pobjCliente,
                    _dt
                    );

            }

        }

        private DataTable buscarDetalleImpresion(string pcodTipoTransaccion,string pcodTransaction)
        {
            StringBuilder _sb = new StringBuilder();

            if (pcodTipoTransaccion.Equals(ROLTransactions._devolucionSigla)
                || pcodTipoTransaccion.Equals(ROLTransactions._regaliaSigla))
            {
                _sb = consultaNoDevolucionNoRegalia(pcodTransaction, pcodTipoTransaccion);
            }
            else
            {
                _sb = consultaDevolucionRegalia(pcodTransaction, pcodTipoTransaccion);
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        private StringBuilder consultaNoDevolucionNoRegalia(string pcodTransaction,string pcodTipoTransaccion)
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
            _sb.Append("DD." + TableDetalleDocumento._PORCENTAJE_IMP + ", ");
            _sb.Append("DD." + TableDetalleDocumento._PORCENTAJE_IMP_EXO + ", ");
            _sb.Append("DD." + TableDetalleDocumento._IMP_EXO + ", ");
            _sb.Append("DD." + TableDetalleDocumento._EXONERAID + ", ");
            _sb.Append("DD." + TableDetalleDocumento._TIPO_EXO + ", ");
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

        private StringBuilder consultaDevolucionRegalia(string pcodTransaction, string pcodTipoTransaccion)
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
            _sb.Append("DD." + TableDetalleDocumento._PORCENTAJE_IMP + ", ");
            _sb.Append("DD." + TableDetalleDocumento._PORCENTAJE_IMP_EXO + ", ");
            _sb.Append("DD." + TableDetalleDocumento._IMP_EXO + ", ");
            _sb.Append("DD." + TableDetalleDocumento._EXONERAID + ", ");
            _sb.Append("DD." + TableDetalleDocumento._TIPO_EXO + ", ");
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
    }
}
