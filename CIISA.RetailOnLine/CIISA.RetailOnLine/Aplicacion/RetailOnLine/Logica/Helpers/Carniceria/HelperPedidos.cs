using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.DevolucionFactura;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers.Carniceria
{
    public class HelperPedidos
    {
        internal void buscarListaPedidosBackOffice(ListView pltvPedidos, Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableEncabezadoPedido._NO_TRANSA + ", ");
            _sb.Append(TableEncabezadoPedido._NO_ESTABLECIMIENTO + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableEncabezadoPedido._FECHA_CREA + " ) " + TableEncabezadoPedido._FECHA_CREA + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableEncabezadoPedido._FECHA_ENTREGA + " ) " + TableEncabezadoPedido._FECHA_ENTREGA + ", ");
            _sb.Append(TableEncabezadoPedido._PEDIDO_PLANTA);
            _sb.Append(" FROM ");
            _sb.Append(TablesROL._encabezadoPedido);
            _sb.Append(" WHERE ");
            _sb.Append(TableEncabezadoPedido._NO_CLIENTE + " = ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoPedido._ENVIADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoPedido._APLICADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("PEDIDO_PLANTA = ");
            _sb.Append("'" + SQL._Si + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoPedido._NO_ESTABLECIMIENTO + " = ");
            _sb.Append("'" + pobjCliente.v_objEstablecimiento.v_codEstablecimiento + "' ");
            _sb.Append("ORDER BY ");
            _sb.Append(TableEncabezadoPedido._FECHA_CREA);

            var MultiGeneric = DependencyService.Get<Framework.Handheld.Connection.IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            ObservableCollection<pnlPedidos_ltvPedidos> Source = new ObservableCollection<pnlPedidos_ltvPedidos>();

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlPedidos_ltvPedidos _lvi = new pnlPedidos_ltvPedidos();

                _lvi.NO_TRANSA = _fila[TableEncabezadoPedido._NO_TRANSA].ToString();

                _lvi.NO_ESTABLECIMIENTO = _fila[TableEncabezadoPedido._NO_ESTABLECIMIENTO].ToString();

                _lvi.FECHA_CREA = _fila[TableEncabezadoPedido._FECHA_CREA].ToString();

                _lvi.FECHA_ENTREGA = _fila[TableEncabezadoPedido._FECHA_ENTREGA].ToString();

                _lvi.PEDIDO_PLANTA = _fila[TableEncabezadoPedido._PEDIDO_PLANTA].ToString();

                Source.Add(_lvi);
            }

            pltvPedidos.ItemsSource = Source;
        }
    }
}
