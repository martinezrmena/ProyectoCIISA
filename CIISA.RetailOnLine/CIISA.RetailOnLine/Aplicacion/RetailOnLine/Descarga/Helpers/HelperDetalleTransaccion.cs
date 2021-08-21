using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.ValidateHH;
using CIISA.RetailOnLine.Framework.External.CustomTreeView;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Helpers
{
    internal class HelperDetalleTransaccion
    {
        private StringBuilder sentenciaBuscarDetallesDocumentos()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableDetalleDocumento._CODDOCUMENTO + ", ");
            _sb.Append(TableDetalleDocumento._CODTIPODOCUMENTO + ", ");
            _sb.Append(TableDetalleDocumento._NUMLINEA + ", ");
            _sb.Append(TableDetalleDocumento._CODPRODUCTO + ", ");
            _sb.Append("REPLACE(" + TableDetalleDocumento._CANTIDAD + ",',','.') " + TableDetalleDocumento._CANTIDAD + ", ");
            _sb.Append(TableDetalleDocumento._COMENTARIO + ", ");
            _sb.Append("REPLACE(" + TableDetalleDocumento._TOTALLINEA + ",',','.') " + TableDetalleDocumento._TOTALLINEA + ", ");
            _sb.Append("REPLACE(" + TableDetalleDocumento._TOTAL_IMP_LIN + ",',','.') " + TableDetalleDocumento._TOTAL_IMP_LIN + ", ");
            _sb.Append(TableDetalleDocumento._NO_AGENTE + ", ");
            _sb.Append(TableDetalleDocumento._ENVIADO + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableDetalleDocumento._FECHA_CREA + " ) " + TableDetalleDocumento._FECHA_CREA + ", ");
            _sb.Append("REPLACE(" + TableDetalleDocumento._PRECIO_UNI + ",',','.') " + TableDetalleDocumento._PRECIO_UNI + ", ");
            _sb.Append("REPLACE(" + TableDetalleDocumento._PORC_DEC + ",',','.') " + TableDetalleDocumento._PORC_DEC + ", ");
            _sb.Append("REPLACE(" + TableDetalleDocumento._MONTO_DESCUENTO + ",',','.') " + TableDetalleDocumento._MONTO_DESCUENTO + ", ");
            _sb.Append("REPLACE(PRECIO_UNI - MONTO_DESCUENTO,',','.') " + TableDetalleDocumento.k_PRECIO_SIN_DESCUENTO + ", ");
            _sb.Append("REPLACE(" + TableDetalleDocumento._EMBALAJE + ",',','.') " + TableDetalleDocumento._EMBALAJE + ", ");
            _sb.Append(TableDetalleDocumento._ESTADODEVOLUCION + " ");
            _sb.Append("FROM ");

            return _sb;
        }

        internal DataTable buscarDetallesDocumentosSinEnviar_SincronizacionAutomatica(TransaccionEncabezado pobjTransaccion, string TipoTransaccion)
        {
            StringBuilder _sb = sentenciaBuscarDetallesDocumentos();

            _sb.Append(TablesROL._detalleDocumento + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableEncabezadoDocumento._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoDocumento._ENVIADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoDocumento._CODDOCUMENTO + " != ");
            _sb.Append("'" + pobjTransaccion.v_codDocumento + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("'" + TipoTransaccion + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            var DTSpecialFormat = DependencyService.Get<IFillDataSet_Table>();

            return DTSpecialFormat.DataTable_Format(MultiGeneric.uploadDataTable(_sb));
        }

        private void llenarTreeViewDocumentos(DataTable pdt, ListView ptrvDocumentos, Color color)
        {
            if (DataTableValidate.validateDataTable(pdt))
            {
                foreach (DataRow _fila in pdt.Rows)
                {
                    string _codDocumento = _fila[TableDetalleDocumento._CODDOCUMENTO].ToString();

                    string _codTipoDocumento = _fila[TableTipoTransaccion._DESCRIPCION].ToString();

                    string _codCliente = _fila[TableEncabezadoDocumento._CODCLIENTE].ToString();

                    string _nomCliente = _fila[TableCliente._NOMBRE].ToString();

                    string _total = _fila[TableEncabezadoDocumento._TOTAL].ToString();

                    decimal _total2 = FormatUtil.convertStringToDecimal(_total);

                    _total = FormatUtil.applyCurrencyFormat(_total2);

                    _codDocumento = _codDocumento + " " + _codTipoDocumento + " / " + _codCliente + " " + _nomCliente + " / " + _total;

                    Util _util = new Util();

                    _util.evaluateAddNode(ptrvDocumentos,_codDocumento, color);
                }

                var Source = ptrvDocumentos.ItemsSource as ObservableCollection<CollapsableItem>;

                foreach (var _tn in Source)
                {
                    foreach (DataRow _fila in pdt.Rows)
                    {
                        string _codDocumento = _fila[TableDetalleDocumento._CODDOCUMENTO].ToString();

                        string _codTipoDocumento = _fila[TableTipoTransaccion._DESCRIPCION].ToString();

                        string _codCliente = _fila[TableEncabezadoDocumento._CODCLIENTE].ToString();

                        string _nomCliente = _fila[TableCliente._NOMBRE].ToString();

                        string _total = _fila[TableEncabezadoDocumento._TOTAL].ToString();

                        decimal _total2 = FormatUtil.convertStringToDecimal(_total);

                        _total = FormatUtil.applyCurrencyFormat(_total2);

                        _codDocumento = _codDocumento + " " + _codTipoDocumento + " / " + _codCliente + " " + _nomCliente + " / " + _total;

                        if (_tn.Encabezado.ToString() == _codDocumento)
                        {
                            string _linea = _fila[TableDetalleDocumento._NUMLINEA].ToString();
                            string _codProducto = _fila[TableDetalleDocumento._CODPRODUCTO].ToString();

                            string _cantidad = _fila[TableDetalleDocumento._CANTIDAD].ToString();
                            decimal _cantidadTemp = FormatUtil.convertStringToDecimal(_cantidad);
                            _cantidad = FormatUtil.applyCurrencyFormat(_cantidadTemp);

                            string _especificacion = _fila[TableDetalleDocumento._COMENTARIO].ToString();

                            if (_especificacion.Equals(string.Empty))
                            {
                                _especificacion = PaymentForm._notApplyInitials;
                            }

                            string _embalaje = _fila[TableDetalleDocumento._EMBALAJE].ToString();

                            if (_embalaje.Equals(string.Empty))
                            {
                                _embalaje = PaymentForm._notApplyInitials;
                            }

                            string _enviado = _fila[TableDetalleDocumento._ENVIADO].ToString();

                            string _nodo = _linea + " / "
                                        + _codProducto + " / "
                                        + _cantidad + " / "
                                        + _especificacion + " / "
                                        + _embalaje + " / "
                                        + _enviado;

                            //if (_enviado.Equals(SQL._No))
                            //{
                            //    _tn.Nodes.Add(_nodo).ForeColor = Color.Red;
                            //}
                            //else
                            //{
                            //    _tn.Nodes.Add(_nodo);
                            //}
                            _tn.Detalle = _tn.Detalle + "-> " + _nodo + Environment.NewLine;
                        }
                    }
                }
            }
        }

        internal void consultaTransaccionDetalles(ListView ptrvDocumentos, Color color)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("DD." + TableDetalleDocumento._CODCIA + ", ");
            _sb.Append("DD." + TableDetalleDocumento._CODDOCUMENTO + ", ");
            _sb.Append("DD." + TableDetalleDocumento._CODTIPODOCUMENTO + ", ");
            _sb.Append("DD." + TableDetalleDocumento._NUMLINEA + ", ");
            _sb.Append("DD." + TableDetalleDocumento._CODPRODUCTO + ", ");
            _sb.Append("DD." + TableDetalleDocumento._CANTIDAD + ", ");
            _sb.Append("DD." + TableDetalleDocumento._COMENTARIO + ", ");
            _sb.Append("DD." + TableDetalleDocumento._TOTALLINEA + ", ");
            _sb.Append("DD." + TableDetalleDocumento._TOTAL_IMP_LIN + ", ");
            _sb.Append("DD." + TableDetalleDocumento._NO_AGENTE + ", ");
            _sb.Append("DD." + TableDetalleDocumento._ENVIADO + ", ");
            _sb.Append("DD." + TableDetalleDocumento._FECHA_CREA + ", ");
            _sb.Append("DD." + TableDetalleDocumento._PRECIO_UNI + ", ");
            //_sb.Append("DD." + TableDetalleDocumento._ENVIADO + ", ");
            _sb.Append("DD." + TableDetalleDocumento._EMBALAJE + ", ");
            _sb.Append("TT." + TableTipoTransaccion._DESCRIPCION + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODCLIENTE + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._TOTAL + ", ");
            _sb.Append("CL." + TableCliente._NOMBRE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._detalleDocumento + " DD, ");
            _sb.Append(TablesROL._tipoTransaccion + " TT, ");
            _sb.Append(TablesROL._encabezadoDocumento + " ED, ");
            _sb.Append(TablesROL._cliente + " CL ");
            _sb.Append("WHERE ");
            _sb.Append("DD." + TableDetalleDocumento._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("DD." + TableDetalleDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("TT." + TableTipoTransaccion._TIPOTRANSACCION + " ");
            _sb.Append("AND ");
            _sb.Append("DD." + TableDetalleDocumento._CODCIA + " = ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODCIA + " ");
            _sb.Append("AND ");
            _sb.Append("DD." + TableDetalleDocumento._CODDOCUMENTO + " = ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODDOCUMENTO + " ");
            _sb.Append("AND ");
            _sb.Append("DD." + TableDetalleDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODTIPODOCUMENTO + " ");
            _sb.Append("AND ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODCIA + " = ");
            _sb.Append("CL." + TableCliente._NO_CIA + " ");
            _sb.Append("AND ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODCLIENTE + " = ");
            _sb.Append("CL." + TableCliente._NO_CLIENTE);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            llenarTreeViewDocumentos(_dt, ptrvDocumentos, color);
        }

        internal DataTable buscarDetallesDocumentosSinEnviar(bool ptodosDocumentos, string ptipoDescarga)
        {
            StringBuilder _sb = sentenciaBuscarDetallesDocumentos();

            if (ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append(TablesROL._detalleDocumentoBK + " ");
            }
            else
            {
                _sb.Append(TablesROL._detalleDocumento + " ");
            }

            _sb.Append("WHERE ");
            _sb.Append(TableDetalleDocumento._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");

            if (!ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append("AND ");
                _sb.Append(TableDetalleDocumento._ENVIADO + " = ");
                _sb.Append("'" + SQL._No + "' ");

                if (!ptodosDocumentos)
                {
                    _sb.Append("AND ");
                    _sb.Append(TableDetalleDocumento._CODTIPODOCUMENTO + " = ");
                    _sb.Append("'" + ROLTransactions._ordenVentaSigla + "'");
                }
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            var DTSpecialFormat = DependencyService.Get<IFillDataSet_Table>();

            return DTSpecialFormat.DataTable_Format(MultiGeneric.uploadDataTable(_sb));
        }
    }
}
