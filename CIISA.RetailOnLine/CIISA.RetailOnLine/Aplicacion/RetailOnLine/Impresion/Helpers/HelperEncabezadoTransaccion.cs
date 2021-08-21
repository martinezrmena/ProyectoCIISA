using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionReporte;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers
{
    internal class HelperEncabezadoTransaccion
    {
        internal void buscarLineasReporteDocumentosRealizados(List<string> pprintingLinesList,string pcodTipoDocumento)
        {
            #region REPORTES: 
            //Facturacion Crédito, Facturación Contado, 
            //Regalias, Devoluciones, Cotización, Ordenes de venta

            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODDOCUMENTO + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODCLIENTE + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._TOTAL + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODTIPODOCUMENTO + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._ANULADO + ", ");
            _sb.Append("ED." + TableEncabezadoDocumento._TRAMITE + ", ");
            _sb.Append("C." + TableCliente._NOMBRE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoDocumento + " ED, ");
            _sb.Append(TablesROL._cliente + " C ");
            _sb.Append("WHERE ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODTIPODOCUMENTO + " = ");
            _sb.Append("'" + pcodTipoDocumento + "' ");
            _sb.Append("AND ");
            _sb.Append("ED." + TableEncabezadoDocumento._CODCLIENTE + " = ");
            _sb.Append("C." + TableCliente._NO_CLIENTE + " ");
            _sb.Append("ORDER BY ED." + TableEncabezadoDocumento._CODDOCUMENTO + " ASC");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            int _noLinea = Numeric._zeroInteger;

            decimal _totalGeneral = Numeric._zeroDecimalInitialize;

            Position _position = new Position();

            foreach (DataRow _fila in _dt.Rows)
            {
                string _lineaUno = string.Empty;
                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.codigo);
                _lineaUno += _fila[TableEncabezadoDocumento._CODDOCUMENTO].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.tipoDocumento);
                _lineaUno += pcodTipoDocumento;

                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.tramite);
                string _estado = _fila[TableEncabezadoDocumento._TRAMITE].ToString();

                if (pcodTipoDocumento.Equals(ROLTransactions._facturaCreditoSigla))
                {
                    if (_estado.Equals(Indicators._S))
                    {
                        _lineaUno += Indicators._Si;
                    }
                    else
                    {
                        _lineaUno += Indicators._No;
                    }
                }
                else
                {
                    _lineaUno += PaymentForm._notApplyInitials;
                }

                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.anulado);
                _estado = _fila[TableEncabezadoDocumento._ANULADO].ToString();

                if (pcodTipoDocumento.Equals(ROLTransactions._devolucionSigla))
                {
                    _lineaUno += PaymentForm._notApplyInitials;
                }
                else
                {
                    if (_estado.Equals(Indicators._S))
                    {
                        _lineaUno += Indicators._Si;
                    }
                    else
                    {
                        _lineaUno += Indicators._No;
                    }
                }

                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.codCliente);
                _lineaUno += _fila[TableEncabezadoDocumento._CODCLIENTE].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.total);
                decimal _total = FormatUtil.convertStringToDecimal(_fila[TableEncabezadoDocumento._TOTAL].ToString());
                _totalGeneral = _totalGeneral + _total;

                if (pcodTipoDocumento.Equals(ROLTransactions._ordenVentaSigla))
                {
                    _lineaUno += PaymentForm._notApplyInitials;
                }
                else
                {
                    _lineaUno += FormatUtil.applyCurrencyFormat(_total);
                }

                _lineaUno += Environment.NewLine;

                string _lineaDos = string.Empty;
                _lineaDos += _position.tabular(_lineaDos.Length, RepFacturas.cliente);
                _lineaDos += _fila[TableCliente._NOMBRE].ToString();

                _lineaDos += Environment.NewLine;

                _noLinea++;

                foreach (string singleline in Regex.Split(_lineaUno + _lineaDos, Environment.NewLine))
                {
                    pprintingLinesList.Add(singleline + Environment.NewLine);
                }
            }

            if (!pcodTipoDocumento.Equals(ROLTransactions._ordenVentaSigla))
            {
                string _lineaTres = string.Empty;

                _lineaTres += Environment.NewLine;
                _lineaTres += _position.tabular(_lineaTres.Length, RepFacturas.tipoDocumento);
                _lineaTres += "Total: ";
                _lineaTres += _position.tabular(_lineaTres.Length, RepFacturas.codCliente);
                _lineaTres += FormatUtil.applyCurrencyFormat(_totalGeneral);
                _lineaTres += Environment.NewLine;

                pprintingLinesList.Add(_lineaTres);
            }

            pprintingLinesList.Add(Environment.NewLine);

            string _lineaCuatro = string.Empty;

            _lineaCuatro += "No. Líneas: ";
            _lineaCuatro += _noLinea;
            _lineaCuatro += Simbol._point;
            _lineaCuatro += Environment.NewLine;

            pprintingLinesList.Add(_lineaCuatro);
            #endregion
        }

    }
}
