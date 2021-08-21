using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionReporte;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers
{
    internal class HelperEncabezadoPedido
    {
        internal void buscarEncabezadosPedidosRuta(List<string> pprintingLinesList)
        {
            #region REPORTES: Pedidos
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("EP." + TableEncabezadoPedido._NO_CIA + ", ");
            _sb.Append("EP." + TableEncabezadoPedido._NO_TRANSA + ", ");
            _sb.Append("EP." + TableEncabezadoPedido._NO_CLIENTE + ", ");
            _sb.Append("EP." + TableEncabezadoPedido._NO_ESTABLECIMIENTO + ", ");
            _sb.Append("EP." + TableEncabezadoPedido._NO_AGENTE + ", ");
            _sb.Append("C." + TableCliente._NOMBRE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoPedido + " EP, ");
            _sb.Append(TablesROL._cliente + " C ");
            _sb.Append("WHERE ");
            _sb.Append("EP." + TableEncabezadoPedido._NO_CLIENTE + " = ");
            _sb.Append("C." + TableCliente._NO_CLIENTE + " ");
            _sb.Append("ORDER BY ");
            _sb.Append("EP." + TableEncabezadoPedido._NO_CLIENTE);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            foreach (DataRow _fila in _dt.Rows)
            {
                Position _position = new Position();

                string _lineaUno = string.Empty;
                _lineaUno += _position.tabular(_lineaUno.Length, RepPedidosRuta.codigoCliente);
                _lineaUno += _fila[TableEncabezadoPedido._NO_CLIENTE].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepPedidosRuta.local);
                _lineaUno += _fila[TableCliente._NOMBRE].ToString();
                _lineaUno += Environment.NewLine;

                _lineaUno += _position.tabular(_lineaUno.Length, RepPedidosRuta.codigoPedido);
                _lineaUno += "No. Pedido: ";
                _lineaUno += _fila[TableEncabezadoPedido._NO_TRANSA].ToString();
                _lineaUno += Environment.NewLine;

                Impresion_ManagerDetallePedido _manager = new Impresion_ManagerDetallePedido();

                _lineaUno += _manager.buscarDetallesPedidosRutaImpresion(
                                _fila[TableEncabezadoPedido._NO_TRANSA].ToString()
                                );
                _lineaUno += Environment.NewLine;


                foreach (string singleline in Regex.Split(_lineaUno, Environment.NewLine))
                {
                    pprintingLinesList.Add(singleline + Environment.NewLine);
                }
            }

            pprintingLinesList.Add("No. pedidos: " + _dt.Rows.Count + Environment.NewLine);
            #endregion
        }

        internal void buscarEncabezadosPedidosSinAplicar(List<string> pprintingLinesList)
        {
            #region REPORTES: Pedidos sin aplicar
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("EP." + TableEncabezadoPedido._NO_CIA + ", ");
            _sb.Append("EP." + TableEncabezadoPedido._NO_TRANSA + ", ");
            _sb.Append("EP." + TableEncabezadoPedido._NO_CLIENTE + ", ");
            _sb.Append("EP." + TableEncabezadoPedido._NO_ESTABLECIMIENTO + ", ");
            _sb.Append("EP." + TableEncabezadoPedido._NO_AGENTE + ", ");
            _sb.Append("C." + TableCliente._NOMBRE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoPedido + " EP, ");
            _sb.Append(TablesROL._cliente + " C ");
            _sb.Append("WHERE ");
            _sb.Append("EP." + TableEncabezadoPedido._APLICADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("EP." + TableEncabezadoPedido._NO_CLIENTE + " = ");
            _sb.Append("C." + TableCliente._NO_CLIENTE + " ");
            _sb.Append("ORDER BY ");
            _sb.Append("EP." + TableEncabezadoPedido._NO_CLIENTE + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            foreach (DataRow _fila in _dt.Rows)
            {
                Position _position = new Position();

                string _lineaUno = string.Empty;
                _lineaUno += _position.tabular(_lineaUno.Length, RepPedidosRuta.codigoCliente);
                _lineaUno += _fila[TableEncabezadoPedido._NO_CLIENTE].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepPedidosRuta.local);
                _lineaUno += _fila[TableCliente._NOMBRE].ToString();
                _lineaUno += Environment.NewLine;

                _lineaUno += _position.tabular(_lineaUno.Length, RepPedidosRuta.codigoPedido);
                _lineaUno += "No. Pedido: ";
                _lineaUno += _fila[TableEncabezadoPedido._NO_TRANSA].ToString();
                _lineaUno += Environment.NewLine;

                Impresion_ManagerDetallePedido _manager = new Impresion_ManagerDetallePedido();

                _lineaUno += _manager.buscarDetallesPedidosRutaImpresion(
                                _fila[TableEncabezadoPedido._NO_TRANSA].ToString()
                                );

                _lineaUno += Environment.NewLine;

                foreach (string singleline in Regex.Split(_lineaUno, Environment.NewLine))
                {
                    pprintingLinesList.Add(singleline +Environment.NewLine);
                }
            }

            pprintingLinesList.Add("No. pedidos: " + _dt.Rows.Count + Environment.NewLine);
            #endregion
        }

    }
}
