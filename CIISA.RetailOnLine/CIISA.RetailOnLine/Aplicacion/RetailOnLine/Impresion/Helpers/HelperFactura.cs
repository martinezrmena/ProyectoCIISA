using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionReporte;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using Xamarin.Forms;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Text.RegularExpressions;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers
{
    internal class HelperFactura
    {
        internal void buscarLineasReporteCrediticioDelCliente(Cliente pobjCliente,List<string> pprintingLinesList)
        {
            #region REPORTE: Crediticio del cliente
            DataTable _dt = buscarInformacionCrediticiaDelCliente(
                                pobjCliente.v_no_cliente,
                                pobjCliente.v_objEstablecimiento.v_codEstablecimiento
                                );

            decimal _totalMontoOriginal = Numeric._zeroDecimalInitialize;
            decimal _totalSaldo = Numeric._zeroDecimalInitialize;

            string _lineaUno = string.Empty;
            string _lineaDos = string.Empty;
            string _lineaTres = string.Empty;
            string _lineaCuatro = string.Empty;

            int _numeroLinea = 0;

            Position _position = new Position();

            foreach (DataRow _fila in _dt.Rows)
            {
                _lineaUno = string.Empty;
                _lineaDos = string.Empty;

                _lineaUno += _position.tabular(_lineaUno.Length, RepCrediticioCliente.fecha);
                _lineaUno += _fila[TableFactura._FECHA_DOCUMENTO].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepCrediticioCliente.documento);
                _lineaUno += _fila[TableFactura._NO_FISICO].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepCrediticioCliente.monto);
                decimal _montoOriginal = FormatUtil.convertStringToDecimal(_fila[TableFactura._MONTOORIGINAL].ToString());

                _lineaUno += FormatUtil.applyCurrencyFormat(_montoOriginal);

                _lineaUno += _position.tabular(_lineaUno.Length, RepCrediticioCliente.vencimiento);
                _lineaUno += _fila[TableFactura._FECHA_VENCE].ToString();

                _lineaUno += Environment.NewLine;

                _lineaDos += _position.tabular(_lineaDos.Length, RepCrediticioRuta.saldo);
                decimal _saldo = FormatUtil.convertStringToDecimal(_fila[TableFactura._SALDO].ToString());

                Logica_ManagerDetalleRecibo _manager = new Logica_ManagerDetalleRecibo();

                decimal _pagosFactura = _manager.obtenerPagosFactura(_fila[TableFactura._NO_FISICO].ToString());
                _saldo = _saldo - _pagosFactura;
                _lineaDos += FormatUtil.applyCurrencyFormat(_saldo);

                _lineaDos += _position.tabular(_lineaDos.Length, RepCrediticioCliente.diasVencido);
                _lineaDos += _fila["DIASVENCIDA"].ToString();

                _lineaDos += Environment.NewLine;

                if (_saldo > 0)
                {
                    pprintingLinesList.Add(_lineaUno);
                    pprintingLinesList.Add(_lineaDos);

                    _totalMontoOriginal += _montoOriginal;
                    _totalSaldo += _saldo;

                    pprintingLinesList.Add(Environment.NewLine);

                    _numeroLinea++;
                }

            }

            _lineaUno = string.Empty;
            _lineaDos = string.Empty;

            _lineaUno = Environment.NewLine;
            _lineaDos = Environment.NewLine;

            _lineaUno += _position.tabular(_lineaUno.Length, RepCrediticioCliente.fecha);
            _lineaUno += "Total Monto Original: ";
            _lineaUno += _position.tabular(_lineaUno.Length, RepCrediticioCliente.monto);
            _lineaUno += FormatUtil.applyCurrencyFormat(_totalMontoOriginal);

            _lineaUno += Environment.NewLine;

            _lineaDos += _position.tabular(_lineaDos.Length, RepCrediticioCliente.fecha);
            _lineaDos += "Total Saldo: ";
            _lineaDos += _position.tabular(_lineaDos.Length, RepCrediticioCliente.monto);
            _lineaDos += FormatUtil.applyCurrencyFormat(_totalSaldo);

            _lineaDos += Environment.NewLine;
            _lineaDos += Environment.NewLine;

            _lineaTres += _position.tabular(_lineaTres.Length, RepCrediticioCliente.fecha);
            _lineaTres += "Límite de crédito: ";
            _lineaTres += _position.tabular(_lineaTres.Length, RepCrediticioCliente.monto);
            _lineaTres += FormatUtil.applyCurrencyFormat(
                                pobjCliente.v_objEstablecimiento.v_objIndicador.v_limiteCredito
                                );

            _lineaTres += Environment.NewLine;

            if (_totalSaldo > pobjCliente.v_objEstablecimiento.v_objIndicador.v_limiteCredito)
            {
                _lineaCuatro += _position.tabular(_lineaCuatro.Length, RepCrediticioCliente.fecha);
                _lineaCuatro += "* Sobregiro: ";
                _lineaCuatro += _position.tabular(_lineaCuatro.Length, RepCrediticioCliente.monto);
                _lineaCuatro += FormatUtil.applyCurrencyFormat(
                                    _totalSaldo - pobjCliente.v_objEstablecimiento.v_objIndicador.v_limiteCredito
                                    );

                _lineaCuatro += Environment.NewLine;
            }
            else
            {
                _lineaCuatro += _position.tabular(_lineaCuatro.Length, RepCrediticioCliente.fecha);
                _lineaCuatro += "* Disponible: ";
                _lineaCuatro += _position.tabular(_lineaCuatro.Length, RepCrediticioCliente.monto);
                _lineaCuatro += FormatUtil.applyCurrencyFormat(
                                    pobjCliente.v_objEstablecimiento.v_objIndicador.v_limiteCredito - _totalSaldo
                                    );

                _lineaCuatro += Environment.NewLine;
            }

            pprintingLinesList.Add(_lineaUno);
            pprintingLinesList.Add(_lineaDos);
            pprintingLinesList.Add(_lineaTres);
            pprintingLinesList.Add(_lineaCuatro);

            pprintingLinesList.Add(Environment.NewLine);
            pprintingLinesList.Add("No. Líneas: " + _numeroLinea);
            pprintingLinesList.Add(Environment.NewLine);
            #endregion
        }

        internal void buscarLineasReporteCrediticioDeLaRuta(List<string> pprintingLinesList)
        {
            #region REPORTES: Crediticio de la ruta
            DataTable _clientes = buscarClientesConFacturas();

            string _lineaUno = string.Empty;
            string _lineaDos = string.Empty;

            decimal _totalMontoOriginal = Numeric._zeroDecimalInitialize;
            decimal _totalSaldo = Numeric._zeroDecimalInitialize;

            int _noLinea = 0;

            int _numEstablecimiento = 0;

            Position _position = new Position();

            foreach (DataRow _filaClientes in _clientes.Rows)
            {
                _lineaUno = string.Empty;

                _lineaUno += _position.tabular(_lineaUno.Length, RepCrediticioRuta.codigo);
                _lineaUno += _filaClientes[TableFactura._NO_CLIENTE].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepCrediticioRuta.local);
                _lineaUno += _filaClientes[TableEstablecimiento._DESCESTABLECIMIENTO].ToString();
                _lineaUno += Environment.NewLine;

                pprintingLinesList.Add(_lineaUno);

                _numEstablecimiento = FormatUtil.convertStringToInt(_filaClientes[TableFactura._NUM_ESTABLECIMIENTO].ToString());

                DataTable _dt = buscarInformacionCrediticiaDelCliente(_filaClientes[TableFactura._NO_CLIENTE].ToString(), _numEstablecimiento);

                foreach (DataRow _fila in _dt.Rows)
                {
                    _lineaUno = string.Empty;
                    _lineaDos = string.Empty;

                    _lineaUno += _position.tabular(_lineaUno.Length, RepCrediticioRuta.fecha);
                    _lineaUno += _fila[TableFactura._FECHA_DOCUMENTO].ToString();

                    _lineaUno += _position.tabular(_lineaUno.Length, RepCrediticioRuta.documento);
                    _lineaUno += _fila[TableFactura._NO_FISICO].ToString();

                    _lineaUno += _position.tabular(_lineaUno.Length, RepCrediticioRuta.monto);
                    decimal _montoOriginal = FormatUtil.convertStringToDecimal(_fila[TableFactura._MONTOORIGINAL].ToString());

                    _lineaUno += FormatUtil.applyCurrencyFormat(_montoOriginal);

                    _lineaUno += _position.tabular(_lineaUno.Length, RepCrediticioRuta.vencimiento);
                    _lineaUno += _fila[TableFactura._FECHA_VENCE].ToString();

                    _lineaUno += Environment.NewLine;

                    _lineaDos += _position.tabular(_lineaDos.Length, RepCrediticioRuta.saldo);
                    decimal _saldo = FormatUtil.convertStringToDecimal(_fila[TableFactura._SALDO].ToString());

                    Logica_ManagerDetalleRecibo _manager = new Logica_ManagerDetalleRecibo();

                    decimal _pagosFactura = _manager.obtenerPagosFactura(_fila[TableFactura._NO_FISICO].ToString());
                    _saldo = _saldo - _pagosFactura;

                    _lineaDos += FormatUtil.applyCurrencyFormat(_saldo);

                    _lineaDos += _position.tabular(_lineaDos.Length, RepCrediticioRuta.diasVencido);
                    decimal _diasVencida = FormatUtil.convertStringToDecimal(_fila["DIASVENCIDA"].ToString());

                    _lineaDos += FormatUtil.applyCurrencyFormat(_diasVencida);

                    _lineaDos += Environment.NewLine;

                    if (_saldo > 0)
                    {
                        _totalMontoOriginal += _montoOriginal;
                        _totalSaldo += _saldo;

                        _noLinea++;
                    }

                    foreach (string singleline in Regex.Split(_lineaUno + _lineaDos, Environment.NewLine))
                    {
                        pprintingLinesList.Add(singleline + Environment.NewLine);
                    }
                }

                pprintingLinesList.Add(Environment.NewLine);
            }

            _lineaUno = string.Empty;
            _lineaDos = string.Empty;

            _lineaUno = Environment.NewLine;
            _lineaDos = Environment.NewLine;

            _lineaUno += _position.tabular(_lineaUno.Length, RepCrediticioRuta.fecha);
            _lineaUno += "Total Monto Original: ";
            _lineaUno += _position.tabular(_lineaUno.Length, RepCrediticioRuta.monto);
            _lineaUno += FormatUtil.applyCurrencyFormat(_totalMontoOriginal);

            _lineaUno += Environment.NewLine;

            _lineaDos += _position.tabular(_lineaDos.Length, RepCrediticioRuta.fecha);
            _lineaDos += "Total Saldo: ";
            _lineaDos += _position.tabular(_lineaDos.Length, RepCrediticioRuta.monto);
            _lineaDos += FormatUtil.applyCurrencyFormat(_totalSaldo);

            _lineaDos += Environment.NewLine;

            _lineaDos += Environment.NewLine;
            _lineaDos += "No. Líneas: " + _noLinea;
            _lineaDos += Environment.NewLine;


            foreach (string singleline in Regex.Split(_lineaUno + _lineaDos, Environment.NewLine))
            {
                pprintingLinesList.Add(singleline + Environment.NewLine);
            }
            #endregion
        }

        private DataTable buscarInformacionCrediticiaDelCliente(string pnoCliente,int pnoEstablecimiento)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableFactura._NO_FISICO + ", ");
            _sb.Append(TableFactura._SALDO + ", ");
            //_sb.Append("CONVERT(NCHAR(10), " + TableFactura._FECHA_DOCUMENTO + ", 103) " + TableFactura._FECHA_DOCUMENTO + ", ");
            //_sb.Append("CONVERT(NCHAR(10), " + TableFactura._FECHA_VENCE + ", 103) " + TableFactura._FECHA_VENCE + ", ");
            _sb.Append("strftime('%d/%m/%Y'," + TableFactura._FECHA_DOCUMENTO + ") " + TableFactura._FECHA_DOCUMENTO + ", ");
            _sb.Append("strftime('%d/%m/%Y'," + TableFactura._FECHA_VENCE + ") " + TableFactura._FECHA_VENCE + ", ");
            _sb.Append(TableFactura._MONTOORIGINAL + ", ");
            //_sb.Append("DATEDIFF(D, " + TableFactura._FECHA_VENCE + ", GETDATE()) AS DIASVENCIDA, ");
            _sb.Append("(julianday(Date('Now'))- julianday(Date("+ TableFactura._FECHA_VENCE + "))) AS DIASVENCIDA, ");
            _sb.Append(TableFactura._FECHA_DOCUMENTO + " FD, ");
            _sb.Append(TableFactura._NUM_ESTABLECIMIENTO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._factura + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableFactura._NO_CLIENTE + " = ");
            _sb.Append("'" + pnoCliente + "' ");
            _sb.Append("AND ");
            _sb.Append(TableFactura._NUM_ESTABLECIMIENTO + " = ");
            _sb.Append(pnoEstablecimiento + " ");
            _sb.Append("AND ");
            _sb.Append(TableFactura._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("ORDER BY " + TableFactura._NO_CLIENTE + " ASC, " + TableFactura._NUM_ESTABLECIMIENTO + " ASC, FD ASC");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        private DataTable buscarClientesConFacturas()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT DISTINCT ");
            _sb.Append("F." + TableFactura._NO_CLIENTE + ", ");
            _sb.Append("F." + TableFactura._NUM_ESTABLECIMIENTO + ", ");
            _sb.Append("E." + TableEstablecimiento._DESCESTABLECIMIENTO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._factura + " F, ");
            _sb.Append(TablesROL._establecimiento + " E ");
            _sb.Append("WHERE ");
            _sb.Append("F." + TableFactura._ANULADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("F." + TableFactura._NO_CLIENTE + " = ");
            _sb.Append("E." + TableEstablecimiento._CODCLIENTE + " ");
            _sb.Append("AND ");
            _sb.Append("F." + TableFactura._NUM_ESTABLECIMIENTO + " = ");
            _sb.Append("E." + TableEstablecimiento._CODESTABLECIMIENTO + " ");
            _sb.Append("ORDER BY F." + TableFactura._NO_CLIENTE + " DESC, ");
            _sb.Append("F." + TableFactura._NUM_ESTABLECIMIENTO + " DESC ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

    }
}
