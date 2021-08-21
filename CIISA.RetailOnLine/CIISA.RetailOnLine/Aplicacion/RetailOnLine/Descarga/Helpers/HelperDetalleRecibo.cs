using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.ValidateHH;
using CIISA.RetailOnLine.Framework.External.CustomTreeView;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Helpers
{
    internal class HelperDetalleRecibo
    {
        private void llenarTreeViewDocumentos(DataTable pdt, ListView ptrvDocumentos, Color color)
        {
            if (DataTableValidate.validateDataTable(pdt))
            {
                foreach (DataRow _fila in pdt.Rows)
                {
                    string _codDocumento = _fila[TableDetalleRecibo._NO_TRANSA].ToString();

                    string _codTipoDocumento = _fila[TableTipoTransaccion._DESCRIPCION].ToString();

                    string _codCliente = _fila[TableEncabezadoRecibo._NO_CLIENTE].ToString();

                    string _nomCliente = _fila[TableCliente._NOMBRE].ToString();

                    string _total = _fila["" + "MONTO_ENCABEZADO"].ToString();

                    decimal _total2 = FormatUtil.convertStringToDecimal(_total);

                    _total = FormatUtil.applyCurrencyFormat(_total2);

                    _codDocumento = _codDocumento + " " + _codTipoDocumento + " / " + _codCliente + " " + _nomCliente + " / " + _total;

                    Util _util = new Util();

                    _util.evaluateAddNode(ptrvDocumentos,_codDocumento, color);
                }

                var Source = ptrvDocumentos.ItemsSource as ObservableCollection<CollapsableItem>;

                //foreach (TreeNode _tn in ptrvDocumentos.Nodes)
                foreach (var _tn in Source)
                {
                    foreach (DataRow _fila in pdt.Rows)
                    {
                        string _codDocumento = _fila[TableDetalleRecibo._NO_TRANSA].ToString();

                        string _codTipoDocumento = _fila[TableTipoTransaccion._DESCRIPCION].ToString();

                        string _codCliente = _fila[TableEncabezadoRecibo._NO_CLIENTE].ToString();

                        string _nomCliente = _fila[TableCliente._NOMBRE].ToString();

                        string _total = _fila["" + "MONTO_ENCABEZADO"].ToString();

                        decimal _total2 = FormatUtil.convertStringToDecimal(_total);

                        _total = FormatUtil.applyCurrencyFormat(_total2);

                        _codDocumento = _codDocumento + " " + _codTipoDocumento + " / " + _codCliente + " " + _nomCliente + " / " + _total;

                        //if (_tn.Text.ToString() == _codDocumento)
                        if (_tn.Encabezado.ToString() == _codDocumento)
                        {
                            string _noLinea = _fila[TableDetalleRecibo._NO_LINEA].ToString();

                            string _noFactura = _fila[TableDetalleRecibo._NO_FACTURA].ToString();

                            string _monto = _fila["" + "MONTO_DETALLE"].ToString();

                            decimal _monto2 = FormatUtil.convertStringToDecimal(_monto);

                            _monto = FormatUtil.applyCurrencyFormat(_monto2);

                            string _enviado = _fila[TableDetalleDocumento._ENVIADO].ToString();

                            string _nodo = _noLinea
                                        + " / "
                                        + _noFactura
                                        + " / "
                                        + _monto
                                        + " / "
                                        + _enviado
                                        ;
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

        internal void consultaReciboDetalles(ListView ptrvDocumentos, Color color)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("DR." + TableDetalleRecibo._NO_TRANSA + ", ");
            _sb.Append("DR." + TableDetalleRecibo._NO_FACTURA + ", ");
            _sb.Append("REPLACE(ER." + TableEncabezadoRecibo._MONTO + ",',','.') " + "MONTO_ENCABEZADO" + ", ");
            _sb.Append("REPLACE(DR." + TableDetalleRecibo._MONTO + ",',','.') " + "MONTO_DETALLE" + ", ");
            _sb.Append("DR." + TableDetalleRecibo._NO_LINEA + ", ");
            _sb.Append("DR." + TableDetalleRecibo._ENVIADO + ", ");
            _sb.Append("TT." + TableTipoTransaccion._DESCRIPCION + ", ");
            _sb.Append("CL." + TableCliente._NOMBRE + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._NO_CLIENTE + ", ");
            _sb.Append("DR." + TableDetalleRecibo._FECHA_CREA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._detalleRecibo + " DR, ");
            _sb.Append(TablesROL._tipoTransaccion + " TT, ");
            _sb.Append(TablesROL._encabezadoRecibo + " ER, ");
            _sb.Append(TablesROL._cliente + " CL ");
            _sb.Append("WHERE ");
            _sb.Append("DR." + TableDetalleRecibo._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("DR." + TableDetalleRecibo._TIPO_DOC + " = ");
            _sb.Append("TT." + TableTipoTransaccion._TIPOTRANSACCION + " ");
            _sb.Append("AND ");
            _sb.Append("DR." + TableDetalleRecibo._NO_TRANSA + " = ");
            _sb.Append("ER." + TableEncabezadoRecibo._NO_TRANSA + " ");
            _sb.Append("AND ");
            _sb.Append("ER." + TableEncabezadoRecibo._NO_CLIENTE + " = ");
            _sb.Append("CL." + TableCliente._NO_CLIENTE + " ");


            _sb.Append("UNION ");

            _sb.Append("SELECT ");
            _sb.Append("ER." + TableEncabezadoRecibo._NO_TRANSA + ", ");
            _sb.Append("'' " + TableDetalleRecibo._NO_FACTURA + ", ");
            _sb.Append("REPLACE(ER." + TableEncabezadoRecibo._MONTO + ",',','.') " + "MONTO_ENCABEZADO" + ", ");
            _sb.Append("'' " + "MONTO_DETALLE" + ", ");
            _sb.Append("0 " + TableDetalleRecibo._NO_LINEA + ", ");
            _sb.Append("ER." + TableDetalleRecibo._ENVIADO + ", ");
            _sb.Append("TT." + TableTipoTransaccion._DESCRIPCION + ", ");
            _sb.Append("CL." + TableCliente._NOMBRE + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._NO_CLIENTE + ", ");
            _sb.Append("ER." + TableEncabezadoRecibo._FECHA_CREA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._tipoTransaccion + " TT, ");
            _sb.Append(TablesROL._encabezadoRecibo + " ER, ");
            _sb.Append(TablesROL._cliente + " CL ");
            _sb.Append("WHERE ");
            _sb.Append("ER." + TableEncabezadoRecibo._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("ER." + TableEncabezadoRecibo._TIPO_DOC + " = ");
            _sb.Append("TT." + TableTipoTransaccion._TIPOTRANSACCION + " ");
            _sb.Append("AND ");
            _sb.Append("ER." + TableEncabezadoRecibo._NO_CLIENTE + " = ");
            _sb.Append("CL." + TableCliente._NO_CLIENTE + " ");
            _sb.Append("AND ");
            _sb.Append("ER." + TableEncabezadoRecibo._TIPO_DOC + " != ");
            _sb.Append("'" + ROLTransactions._reciboDineroSigla + "' ");
            _sb.Append("ORDER BY ");
            _sb.Append(TableEncabezadoRecibo._FECHA_CREA);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            llenarTreeViewDocumentos(_dt, ptrvDocumentos, color);
        }

        private StringBuilder sentenciaBuscarDetallesRecibos()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableDetalleRecibo._NO_CIA + ", ");
            _sb.Append(TableDetalleRecibo._NO_TRANSA + ", ");
            _sb.Append(TableDetalleRecibo._NO_FACTURA + ", ");
            _sb.Append("REPLACE(" + TableDetalleRecibo._MONTO + ",',','.') " + TableDetalleRecibo._MONTO + ", ");
            _sb.Append(TableDetalleRecibo._TIPO_DOC + ", ");
            _sb.Append(TableDetalleRecibo._NO_LINEA + ", ");
            _sb.Append(TableDetalleRecibo._ENVIADO + ", ");
            _sb.Append(TableDetalleRecibo._ANULADO + ", ");
            _sb.Append(TableDetalleRecibo._FECHA_CREA + ", ");
            _sb.Append(TableDetalleRecibo._TIPO_TRANSA + " ");
            _sb.Append("FROM ");

            return _sb;
        }

        internal DataTable buscarDetallesRecibosSinEnviar(string ptipoDescarga, TransaccionEncabezado pobjTransaccionEncabezado)
        {
            StringBuilder _sb = sentenciaBuscarDetallesRecibos();

            if (ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append(TablesROL._detalleReciboBK + " ");
            }
            else
            {
                _sb.Append(TablesROL._detalleRecibo + " ");
            }

            _sb.Append("WHERE ");
            _sb.Append(TableDetalleRecibo._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");

            _sb.Append("AND ");
            _sb.Append(TableDetalleRecibo._ENVIADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append(" AND ");
            _sb.Append(TableDetalleRecibo._NO_TRANSA + " != ");
            _sb.Append("'" + pobjTransaccionEncabezado.v_codDocumento + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            var DTSpecialFormat = DependencyService.Get<IFillDataSet_Table>();

            return DTSpecialFormat.DataTable_Format(MultiGeneric.uploadDataTable(_sb));
        }

        internal DataTable buscarDetallesRecibosSinEnviar(string ptipoDescarga)
        {
            StringBuilder _sb = sentenciaBuscarDetallesRecibos();

            if (ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append(TablesROL._detalleReciboBK + " ");
            }
            else
            {
                _sb.Append(TablesROL._detalleRecibo + " ");
            }

            _sb.Append("WHERE ");
            _sb.Append(TableDetalleRecibo._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");

            if (!ptipoDescarga.Equals(TipoDescarga._antiguedad))
            {
                _sb.Append("AND ");
                _sb.Append(TableDetalleRecibo._ENVIADO + " = ");
                _sb.Append("'" + SQL._No + "' ");
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();
            var DTSpecialFormat = DependencyService.Get<IFillDataSet_Table>();

            return DTSpecialFormat.DataTable_Format(MultiGeneric.uploadDataTable(_sb));
        }
    }
}
