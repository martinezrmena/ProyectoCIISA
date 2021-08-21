using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.DevolucionFactura;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Common.Utils;
using CIISA.RetailOnLine.Framework.External.CustomListview;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperFactura
    {
        internal decimal calcularTotalFacturasPorPagarPorCliente(Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("SUM(SALDO) AS TOTALFACTURAS ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._factura + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_CLIENTE = ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "' ");
            _sb.Append("AND ");
            _sb.Append("ANULADO = ");
            _sb.Append("'" + SQL._No + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readDecimal(_sb);
        }

        internal void anularFactura(TransaccionEncabezado pobjTransaccion)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._factura + " ");
            _sb.Append("SET ");
            _sb.Append("ANULADO = ");
            _sb.Append("'" + MiscUtils.getVariableStringSQLState(true) + "' ");
            _sb.Append("WHERE ");
            _sb.Append("NO_FISICO = ");
            _sb.Append("'" + pobjTransaccion.v_codDocumento + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);
        }

        internal bool ExisteFacturasVencidas(Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("(julianday(Date('Now'))- julianday(Date(FECHA_VENCE))) AS DIASVENCIDA ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._factura + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_CLIENTE = ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "' ");
            _sb.Append("AND ");
            _sb.Append("ANULADO = ");
            _sb.Append("'" + SQL._No + "'");


            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            bool _existeFacturaVencida = false;

            foreach (DataRow _fila in _dt.Rows)
            {
                string _dv = _fila["DIASVENCIDA"].ToString();
                decimal _diasVencida = FormatUtil.convertStringToDecimal(_dv);

                //if (_diasVencida >= 0)
                //{
                    _existeFacturaVencida = true;
                    //break;
                //}
            }

            return _existeFacturaVencida;
        }

        internal List<string> facturasVencidas(Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("NO_FISICO, ");
            //_sb.Append("DATEDIFF(D, FECHA_VENCE, GETDATE()) AS DIASVENCIDA ");
            _sb.Append("(julianday(Date('Now'))- julianday(Date(FECHA_VENCE))) AS DIASVENCIDA ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._factura + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_CLIENTE = ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "' ");
            _sb.Append("AND ");
            _sb.Append("ANULADO = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("ORDER BY ");
            _sb.Append("FECHA_DOCUMENTO ASC");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            List<string> _facturasVencidas = new List<string>();

            foreach (DataRow _fila in _dt.Rows)
            {

                string _dv = _fila["DIASVENCIDA"].ToString();
                decimal _diasVencida = FormatUtil.convertStringToDecimal(_dv);

                if (_diasVencida >= 0)
                {
                    _facturasVencidas.Add(_fila["NO_FISICO"].ToString());
                }
            }

            return _facturasVencidas;
        }

        internal decimal obtenerSaldoFactura(string pcodFactura)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("SALDO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._factura + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_FISICO = ");
            _sb.Append("'" + pcodFactura + "' ");
            _sb.Append("AND ");
            _sb.Append("ANULADO = ");
            _sb.Append("'" + SQL._No + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readDecimal(_sb);
        }

        internal void buscarListaFacturaTramite(ListView pltvFacturas, Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("NO_FISICO, ");
            _sb.Append("SALDO, ");
            _sb.Append("FECHA_DOCUMENTO, ");
            _sb.Append("strftime('%d/%m/%Y',FECHA_DOCUMENTO) FECHA_DOC, ");
            _sb.Append("strftime('%d/%m/%Y',FECHA_VENCE) FECHA_VENCE ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._factura + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_CLIENTE = ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "' ");
            _sb.Append("AND ");
            _sb.Append("ANULADO = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("TRAMITE = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("ORDER BY ");
            _sb.Append("FECHA_DOCUMENTO ASC");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            SelectableObservableCollection<pnlFacturas_ltvFacturas> Source = new SelectableObservableCollection<pnlFacturas_ltvFacturas>();

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlFacturas_ltvFacturas _lvi = new pnlFacturas_ltvFacturas();

                _lvi.NO_FISICO = _fila["NO_FISICO"].ToString();

                string _monto = _fila["SALDO"].ToString();
                decimal _montoFactura = FormatUtil.convertStringToDecimal(_monto);

                Logica_ManagerDetalleRecibo _manager = new Logica_ManagerDetalleRecibo();
                decimal _pagosFactura = _manager.obtenerPagosFactura(_fila["NO_FISICO"].ToString());

                decimal _montoConAbonos = _montoFactura - _pagosFactura;
                string _saldo = FormatUtil.applyCurrencyFormat(_montoConAbonos);

                _lvi.SALDO =_saldo;

                _monto = FormatUtil.applyCurrencyFormat(_montoFactura);

                _lvi.MONTO = _monto;

                string _pagos = FormatUtil.applyCurrencyFormat(_pagosFactura);

                _lvi.PAGOS = _pagos;

                _lvi.FECHA_DOC = _fila["FECHA_DOC"].ToString();

                string _fechaVence = _fila["FECHA_VENCE"].ToString();
                _lvi.FECHA_VENCE = _fechaVence;

                DateTime _fecha_vence = FormatUtil.covertStringToDateTimeWithoutTime(_fechaVence);

                TimeSpan _diasVencido = _fecha_vence.Subtract(VarTime.getNow());

                if (_diasVencido.Days <= 0)
                {
                    _lvi.ItemColorText = Color.Red;
                }

                _lvi.DIAS_VENCIDO = _diasVencido.Days + string.Empty;

                if (_montoConAbonos > 0)
                {
                    Source.Add(_lvi);
                }
            }

            pltvFacturas.ItemsSource = Source;
        }

        internal void buscarListaFacturaDevolucion(ListView pltvFacturas, Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableEncabezadoDocumento._CODDOCUMENTO + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableEncabezadoDocumento._FECHACREACION + " ) " + TableEncabezadoDocumento._FECHACREACION + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableEncabezadoDocumento._FECHAENTREGA + " ) " + TableEncabezadoDocumento._FECHAENTREGA + ", ");
            _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + ", ");
            _sb.Append(TableEncabezadoDocumento._CODPEDIDO);
            _sb.Append(" FROM ");
            _sb.Append(TablesROL._encabezadoDocumento);
            _sb.Append(" WHERE ");
            _sb.Append(TableEncabezadoDocumento._CODCLIENTE + " = ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoDocumento._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoDocumento._ENVIADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND (");
            _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("'" + ROLTransactions._facturaContadoSigla + "' ");
            _sb.Append("OR ");
            _sb.Append(TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("'" + ROLTransactions._facturaCreditoSigla + "') ");
            _sb.Append("AND ");
            _sb.Append(TableEncabezadoDocumento._CODDOCUMENTO);
            _sb.Append(" NOT IN (SELECT CODFACTURA FROM ENCABEZADODOCUMENTO WHERE CODFACTURA <> 'NULL') ");
            _sb.Append("ORDER BY ");
            _sb.Append(TableEncabezadoDocumento._FECHACREACION);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            ObservableCollection<pnlDevolucion_ltvTiposDevolucion> Source = new ObservableCollection<pnlDevolucion_ltvTiposDevolucion>();

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlDevolucion_ltvTiposDevolucion _lvi = new pnlDevolucion_ltvTiposDevolucion();

                _lvi.CODDOCUMENTO = _fila[TableEncabezadoDocumento._CODDOCUMENTO].ToString();

                _lvi.FECHACREACION = _fila[TableEncabezadoDocumento._FECHACREACION].ToString();

                _lvi.FECHAENTREGA = _fila[TableEncabezadoDocumento._FECHAENTREGA].ToString();

                _lvi.CODTIPODOCUMENTO = _fila[TableEncabezadoDocumento._CODTIPODOCUMENTO].ToString();

                _lvi.CODPEDIDO = _fila[TableEncabezadoDocumento._CODPEDIDO].ToString();

                Source.Add(_lvi);
            }

            pltvFacturas.ItemsSource = Source;
        }

        internal void buscarListaFacturaRecibo(ListView pltvFacturas, Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("F.NO_FISICO, ");
            _sb.Append("F.SALDO, ");
            _sb.Append("strftime('%d/%m/%Y',FECHA_DOCUMENTO) FECHA_DOC, ");          
            _sb.Append("strftime('%d/%m/%Y',FECHA_VENCE) FECHA_VENCE, ");
            _sb.Append("F.MONTOORIGINAL, ");
            _sb.Append("(julianday(Date('Now'))- julianday(Date(FECHA_VENCE))) AS DIASVENCIDA, ");
            _sb.Append("F.TIPO_DOC, ");
            _sb.Append("TT.DESCRIPCION ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._factura + " F, ");
            _sb.Append(TablesROL._tipoTransaccion + " TT ");
            _sb.Append("WHERE ");
            _sb.Append("F.NO_CLIENTE = ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "' ");
            _sb.Append("AND ");
            _sb.Append("F.ANULADO = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("F.TIPO_DOC = TT.TIPOTRANSACCION ");
            _sb.Append("ORDER BY ");
            _sb.Append("F.FECHA_DOCUMENTO, ");
            _sb.Append("F.NO_FISICO ASC");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            SelectableObservableCollection<pnlFacturas_ltvFacturas> Source = new SelectableObservableCollection<pnlFacturas_ltvFacturas>();

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlFacturas_ltvFacturas _lvi = new pnlFacturas_ltvFacturas();

                _lvi.NO_FISICO = _fila["NO_FISICO"].ToString();

                decimal _saldoFactura = FormatUtil.convertStringToDecimal(_fila["SALDO"].ToString());

                Logica_ManagerDetalleRecibo _manager = new Logica_ManagerDetalleRecibo();
                decimal _pagosFactura = _manager.obtenerPagosFactura(_fila["NO_FISICO"].ToString());
                decimal _montoConAbonos = _saldoFactura - _pagosFactura;
                _lvi.SALDO = FormatUtil.applyCurrencyFormat(_montoConAbonos);

                decimal _montoOriginal = FormatUtil.convertStringToDecimal(_fila["MONTOORIGINAL"].ToString());
                _lvi.MONTO = FormatUtil.applyCurrencyFormat(_montoOriginal);

                _lvi.PAGOS = FormatUtil.applyCurrencyFormat(_pagosFactura);

                _lvi.FECHA_DOC = _fila["FECHA_DOC"].ToString();

                _lvi.FECHA_VENCE = _fila["FECHA_VENCE"].ToString();

                _lvi.DIAS_VENCIDO = _fila["DIASVENCIDA"].ToString();

                _lvi.DESCRIPCION = _fila["DESCRIPCION"].ToString();

                _lvi.TIPO_DOC = _fila["TIPO_DOC"].ToString();

                decimal _diasVencido = FormatUtil.convertStringToDecimal(_fila["DIASVENCIDA"].ToString());

                if (_diasVencido >= 0)
                {
                    _lvi.ItemColorText = Color.Red;
                }

                if (_montoConAbonos > 0)
                {
                    Source.Add(_lvi);
                }
            }

            pltvFacturas.ItemsSource = Source;
        }

        internal void guardarFactura(Cliente pobjCliente, decimal ptotal)
        {
            string _fechaDocumento = VarTime.getSQLCEDate();
            string _fechaVence = VarTime.getDateExpiresSQLCE(pobjCliente.v_plazo);

            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._factura + " ");
            _sb.Append("(");
            _sb.Append("NO_FISICO, ");
            _sb.Append("NO_CLIENTE, ");
            _sb.Append("SALDO, ");
            _sb.Append("MONTOORIGINAL, ");
            _sb.Append("FECHA_DOCUMENTO, ");
            _sb.Append("FECHA_VENCE, ");
            _sb.Append("ENVIADO, ");
            _sb.Append("ANULADO, ");
            _sb.Append("CREADA, ");
            _sb.Append("TRAMITE, ");
            _sb.Append("TIPO_DOC, ");
            _sb.Append("NUM_ESTABLECIMIENTO ");
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_codDocumento + "', ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "', ");
            _sb.Append("REPLACE('" + ptotal + "',',',''), ");
            _sb.Append("REPLACE('" + ptotal + "',',',''), ");
            _sb.Append("'" + _fechaDocumento + "', ");
            _sb.Append("'" + _fechaVence + "', ");
            _sb.Append("'" + SQL._No + "', ");
            _sb.Append("'" + SQL._No + "', ");
            _sb.Append("'" + SQL._ROL + "', ");
            _sb.Append("'" + SQL._No + "', ");
            _sb.Append("'" + pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla() + "', ");
            _sb.Append("" + pobjCliente.v_objEstablecimiento.v_codEstablecimiento + " ");
            _sb.Append(")");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.insertRecord(_sb);
        }

        internal void buscarListaFactura(ListView pnlAbono_ltvFacturas, Cliente pobjCliente, List<string> plistaFacturas)
        {
            Utils _utils = new Utils();

            string _listaFacturas = _utils.recordListString(plistaFacturas);

            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("F.NO_FISICO, ");
            _sb.Append("F.SALDO, ");
            _sb.Append("strftime('%d/%m/%Y',FECHA_DOCUMENTO) FECHA_DOC, ");
            _sb.Append("strftime('%d/%m/%Y',FECHA_VENCE) FECHA_VENCE, ");
            _sb.Append("F.TIPO_DOC, ");
            _sb.Append("TT.DESCRIPCION ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._factura + " F, ");
            _sb.Append(TablesROL._tipoTransaccion + " TT ");
            _sb.Append("WHERE ");
            _sb.Append("F.NO_CLIENTE = ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "' ");
            _sb.Append("AND ");
            _sb.Append("F.NO_FISICO ");
            _sb.Append("IN ");
            _sb.Append("(");
            _sb.Append("" + _listaFacturas);
            _sb.Append(") ");
            _sb.Append("AND ");
            _sb.Append("F.ANULADO = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("F.TIPO_DOC = ");
            _sb.Append("TT.TIPOTRANSACCION ");
            _sb.Append("ORDER BY ");
            _sb.Append("F.FECHA_DOCUMENTO, ");
            _sb.Append("F.NO_FISICO ASC");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            var Source = new ObservableCollection<pnlAbono_ltvFacturas>();

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlAbono_ltvFacturas _lvi = new pnlAbono_ltvFacturas();
                _lvi.NoFisico = _fila["NO_FISICO"].ToString();

                decimal _montoFactura = FormatUtil.convertStringToDecimal(_fila["SALDO"].ToString());

                _lvi.Saldo = FormatUtil.applyCurrencyFormat(_montoFactura);

                Logica_ManagerDetalleRecibo _manager = new Logica_ManagerDetalleRecibo();
                decimal _pagosFactura = _manager.obtenerPagosFactura(_fila["NO_FISICO"].ToString());

                _lvi.MontoAbono = FormatUtil.applyCurrencyFormat(_pagosFactura);

                decimal _montoConAbonos = _montoFactura - _pagosFactura;
                _lvi.Monto = FormatUtil.applyCurrencyFormat(_montoConAbonos);

                _lvi.FechaDocumento = _fila["FECHA_DOC"].ToString();

                _lvi.FechaVencimiento = _fila["FECHA_VENCE"].ToString();

                _lvi.TipoDocumentoDescripcion = _fila["DESCRIPCION"].ToString();

                _lvi.TipoDocumento = _fila["TIPO_DOC"].ToString();

                Source.Add(_lvi);
                //pnlAbono_ltvFacturas.Items.Add(_lvi);

                //    pnlAbono_ltvFacturas.Update();
            }
            pnlAbono_ltvFacturas.ItemsSource = Source;
        }
    }
}
