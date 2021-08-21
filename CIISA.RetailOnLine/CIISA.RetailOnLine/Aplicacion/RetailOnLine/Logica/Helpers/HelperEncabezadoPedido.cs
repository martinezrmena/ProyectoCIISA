using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperEncabezadoPedido
    {
        internal void buscarListaEncabezadosPedidos(ListView pltvPedidos)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("NO_CIA, ");
            _sb.Append("NO_TRANSA, ");
            _sb.Append("NO_CLIENTE, ");
            _sb.Append("NO_ESTABLECIMIENTO, ");
            _sb.Append("NO_AGENTE, ");
            _sb.Append("STRFTIME('%d/%m/%Y',FECHA_CREA) FECHA_CREA, ");
            _sb.Append("STRFTIME('%d/%m/%Y',FECHA_ENTREGA) FECHA_ENTREGA, ");
            _sb.Append("TOTAL, ");
            _sb.Append("TOTAL_IMP, ");
            _sb.Append("IND_AUTOMATICO, ");
            _sb.Append("ENVIADO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoPedido + " ");
            _sb.Append("ORDER BY ");
            _sb.Append("NO_CLIENTE");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            ObservableCollection<pnlTransacciones_ltvDocumentos> Source = new ObservableCollection<pnlTransacciones_ltvDocumentos>();

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlTransacciones_ltvDocumentos _lvi = new pnlTransacciones_ltvDocumentos();
                _lvi.NO_CIA = _fila["NO_CIA"].ToString();
                _lvi.NO_TRANSA = _fila["NO_TRANSA"].ToString();
                _lvi.NO_CLIENTE = _fila["NO_CLIENTE"].ToString();
                _lvi.NO_ESTABLECIMIENTO = _fila["NO_ESTABLECIMIENTO"].ToString();
                _lvi.NO_AGENTE = _fila["NO_AGENTE"].ToString();
                _lvi.FECHA_CREA = _fila["FECHA_CREA"].ToString();
                _lvi.FECHA_ENTREGA = _fila["FECHA_ENTREGA"].ToString();

                decimal _total = FormatUtil.convertStringToDecimal(_fila["TOTAL"].ToString());

                _lvi.TOTAL = FormatUtil.applyCurrencyFormat(_total);

                decimal _total_imp = FormatUtil.convertStringToDecimal(_fila["TOTAL_IMP"].ToString());

                _lvi.TOTAL_IMP = FormatUtil.applyCurrencyFormat(_total_imp);

                Source.Add(_lvi);
            }

            pltvPedidos.ItemsSource = Source;
        }

        internal bool ExistePedidoClienteBackOffice(Cliente pobjCliente)
        {
            int codestablecimiento = pobjCliente.v_objEstablecimiento.v_codEstablecimiento;

            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("NO_CLIENTE ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoPedido + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_CLIENTE = ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "' ");
            _sb.Append("AND ");
            _sb.Append("APLICADO = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("NO_ESTABLECIMIENTO = ");
            _sb.Append("'" + codestablecimiento + "' ");
            _sb.Append("AND ");
            _sb.Append("PEDIDO_PLANTA = ");
            _sb.Append("'" + SQL._Si + "' ");

            return OperationSQL.thereRecord(_sb, "NO_CLIENTE");
        }

        internal bool ExistePedidoCliente(Cliente pobjCliente, bool validar)
        {
            int codestablecimiento = pobjCliente.v_objEstablecimiento.v_codEstablecimiento;

            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("NO_CLIENTE ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoPedido + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_CLIENTE = ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "' ");
            _sb.Append("AND ");
            _sb.Append("APLICADO = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("NO_ESTABLECIMIENTO = ");
            _sb.Append("'" + codestablecimiento + "'");

            if (validar)
            {
                _sb.Append("AND ");
                _sb.Append("PEDIDO_PLANTA = ");
                _sb.Append("'" + SQL._No + "'");
            }


            return OperationSQL.thereRecord(_sb, "NO_CLIENTE");
        }

        private TransaccionEncabezado buscarPedidoPorCodigoPedido(string pcodPedido, Cliente cliente, bool Validar, string Es_Factura)
        {
            string pcodCliente = cliente.v_no_cliente;
            int codestablecimiento = cliente.v_objEstablecimiento.v_codEstablecimiento;
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("NO_CIA, ");
            _sb.Append("NO_TRANSA, ");
            _sb.Append("NO_CLIENTE, ");
            _sb.Append("NO_ESTABLECIMIENTO, ");
            _sb.Append("NO_AGENTE, ");
            _sb.Append("DATETIME(FECHA_CREA) FECHA_CREA, ");
            _sb.Append("DATETIME(FECHA_ENTREGA) FECHA_ENTREGA, ");
            _sb.Append("TOTAL, ");
            _sb.Append("TOTAL_IMP, ");
            _sb.Append("IND_AUTOMATICO, ");
            _sb.Append("ENVIADO, ");
            _sb.Append("PEDIDO_PLANTA ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoPedido + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_TRANSA = ");
            _sb.Append("'" + pcodPedido + "' ");
            _sb.Append("AND ");
            _sb.Append("NO_CLIENTE = ");
            _sb.Append("'" + pcodCliente + "' ");
            _sb.Append("AND ");
            _sb.Append("APLICADO = ");
            _sb.Append("'" + SQL._No + "'");
            _sb.Append(" AND ");
            _sb.Append("NO_ESTABLECIMIENTO = ");
            _sb.Append("'" + codestablecimiento + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            TransaccionEncabezado _objTransaccion = null;

            foreach (DataRow _fila in _dt.Rows)
            {
                _objTransaccion = new TransaccionEncabezado();

                _objTransaccion.v_codCia = _fila["NO_CIA"].ToString();
                _objTransaccion.v_codDocumento = _fila["NO_TRANSA"].ToString();
                _objTransaccion.v_codCliente = _fila["NO_CLIENTE"].ToString();

                _objTransaccion.v_objEstablecimiento.v_codEstablecimiento =
                    FormatUtil.convertStringToInt(_fila["NO_ESTABLECIMIENTO"].ToString());

                _objTransaccion.v_codAgente = _fila["NO_AGENTE"].ToString();

                _objTransaccion.v_fechaCreacion =
                    FormatUtil.convertStringToDateTimeWithTime(_fila["FECHA_CREA"].ToString());

                _objTransaccion.v_fechaEntrega =
                    FormatUtil.convertStringToDateTimeWithTime(_fila["FECHA_ENTREGA"].ToString());

                _objTransaccion.v_total =
                    FormatUtil.convertStringToDecimal(_fila["TOTAL"].ToString());

                _objTransaccion.v_totalImp =
                    FormatUtil.convertStringToDecimal(_fila["TOTAL_IMP"].ToString());

                _objTransaccion.v_indAutomatico = _fila["IND_AUTOMATICO"].ToString();
                _objTransaccion.v_enviado = _fila["ENVIADO"].ToString();

                _objTransaccion.v_objTipoDocumento.SetSigla(ROLTransactions._pedidoSigla);
                _objTransaccion.v_observacion = string.Empty;
                _objTransaccion.v_tramite = SQL._No;
                _objTransaccion.v_anulado = SQL._No;
                _objTransaccion.v_codClienteGenerico = string.Empty;
                _objTransaccion.v_motivoAnulacion = string.Empty;
                _objTransaccion.v_Pedido_Planta = _fila["PEDIDO_PLANTA"].ToString();

                Logica_ManagerDetallePedido _manager = new Logica_ManagerDetallePedido();
                _objTransaccion.v_listaDetalles = _manager.buscarDetallePedidoPorCodigoPedido(pcodPedido, Validar, Es_Factura, _objTransaccion.v_codCliente);
            }

            return _objTransaccion;
        }

        internal TransaccionEncabezado buscarPedidoPorCodPedido(string CodPedido, Cliente cliente, bool Validar, string Es_Factura)
        {
            TransaccionEncabezado _Pedido = buscarPedidoPorCodigoPedido(CodPedido, cliente, Validar, Es_Factura);
            
            return _Pedido;
        }

        internal List<TransaccionEncabezado> buscarPedidosPorCliente(Cliente cliente, bool Validar, string Es_Factura)
        {
            string pcodCliente = cliente.v_no_cliente;
            int codestablecimiento = cliente.v_objEstablecimiento.v_codEstablecimiento;

            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("NO_TRANSA ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoPedido + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_CLIENTE = ");
            _sb.Append("'" + pcodCliente + "' ");
            _sb.Append("AND ");
            _sb.Append("APLICADO = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("NO_ESTABLECIMIENTO = ");
            _sb.Append("'" + codestablecimiento + "'");
            if (Validar)
            {
                _sb.Append(" AND ");
                _sb.Append("PEDIDO_PLANTA = ");
                _sb.Append("'" + SQL._No + "'");
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            List<TransaccionEncabezado> _listaPedidos = new List<TransaccionEncabezado>();

            foreach (DataRow _fila in _dt.Rows)
            {
                string _codPedido = _fila["NO_TRANSA"].ToString();

                _listaPedidos.Add(buscarPedidoPorCodigoPedido(_fila["NO_TRANSA"].ToString(), cliente, Validar, Es_Factura));
            }

            return _listaPedidos;
        }

        internal void actualizarAplicado(Cliente pobjCliente, bool Validar)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._encabezadoPedido + " ");
            _sb.Append("SET ");
            _sb.Append("APLICADO = ");
            _sb.Append("'" + SQL._Si + "' ");
            _sb.Append("WHERE ");
            _sb.Append("NO_CLIENTE = ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "' ");
            _sb.Append(" AND ");
            _sb.Append(" NO_ESTABLECIMIENTO = ");
            _sb.Append("'" + pobjCliente.v_objEstablecimiento.v_codEstablecimiento + "' ");
            if (Validar)
            {
                _sb.Append(" AND ");
                _sb.Append(" PEDIDO_PLANTA = ");
                _sb.Append("'" + SQL._No + "' ");
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);
        }

        internal void actualizarAplicadoPedidoBackOffice(string CodPedido, Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._encabezadoPedido + " ");
            _sb.Append("SET ");
            _sb.Append("APLICADO = ");
            _sb.Append("'" + SQL._Si + "' ");
            _sb.Append("WHERE ");
            _sb.Append("NO_CLIENTE = ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "' ");
            _sb.Append(" AND ");
            _sb.Append(" NO_ESTABLECIMIENTO = ");
            _sb.Append("'" + pobjCliente.v_objEstablecimiento.v_codEstablecimiento + "' ");
            _sb.Append(" AND ");
            _sb.Append(" PEDIDO_PLANTA = ");
            _sb.Append("'" + SQL._Si + "' ");
            _sb.Append(" AND ");
            _sb.Append(" NO_TRANSA = ");
            _sb.Append("'" + CodPedido + "' ");


            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);
        }

        internal void guardarEncabezadoPedido(Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._encabezadoPedido + " ");
            _sb.Append("(");
            _sb.Append("NO_CIA, ");
            _sb.Append("NO_TRANSA, ");
            _sb.Append("NO_CLIENTE, ");
            _sb.Append("NO_ESTABLECIMIENTO, ");
            _sb.Append("NO_AGENTE, ");
            _sb.Append("FECHA_CREA, ");
            _sb.Append("FECHA_ENTREGA, ");
            _sb.Append("TOTAL, ");
            _sb.Append("TOTAL_IMP, ");
            _sb.Append("IND_AUTOMATICO, ");
            _sb.Append("ENVIADO, ");
            _sb.Append("APLICADO, ");
            _sb.Append("PEDIDO_PLANTA");
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append("'" + pobjCliente.v_no_cia + "', ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_codDocumento + "', ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "', ");
            _sb.Append("" + pobjCliente.v_objEstablecimiento.v_codEstablecimiento + ", ");
            _sb.Append("'" + pobjCliente.v_no_agente + "', ");
            _sb.Append(SQL.SQLITEDATE + ", ");
            _sb.Append("'" + VarTime.getDateTimeSqlite(pobjCliente.v_objTransaccion.v_fechaEntrega) + "', ");
            _sb.Append("" + pobjCliente.v_objTransaccion.v_total + ", ");
            _sb.Append("" + pobjCliente.v_objTransaccion.v_totalImp + ", ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_indAutomatico + "', ");
            _sb.Append("'" + SQL._No + "', ");
            _sb.Append("'" + SQL._No + "', ");
            _sb.Append("'" + SQL._No + "' ");

            _sb.Append(")");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.insertRecord(_sb);
        }

        internal bool EncabezadoPedido_Vacio()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableEncabezadoPedido._NO_CIA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoPedido + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            if (_dt.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
