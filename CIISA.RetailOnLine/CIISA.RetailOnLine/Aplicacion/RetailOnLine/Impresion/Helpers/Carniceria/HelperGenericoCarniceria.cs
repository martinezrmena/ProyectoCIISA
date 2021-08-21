using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionDocumento;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers.Carniceria
{
    internal class HelperGenericoCarniceria
    {
        internal void buscarLineasDetalleImpresion(string pcodTransaction, string pcodTipoTransaccion, List<string> pprintingLinesList, Cliente pobjCliente, DataTable pdt)
        {
            string _lineasImpresion = string.Empty;

            decimal _totalLinea = Numeric._zeroDecimalInitialize;
            decimal _total_imp_lin = Numeric._zeroDecimalInitialize;
            decimal _totalDescuento = Numeric._zeroDecimalInitialize;

            decimal _totalCantidades = Numeric._zeroDecimalInitialize;

            string _lineaUno = string.Empty;

            Position _position = new Position();

            foreach (DataRow _fila in pdt.Rows)
            {
                _lineasImpresion = string.Empty;

                string _descProducto = string.Empty;
                _lineaUno = string.Empty;
                _descProducto += _fila[TableProducto._DESCPRODUCTO].ToString();

                if (pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla))
                {
                    _descProducto += Simbol._slash;
                    _descProducto += _fila[TableDetalleDocumento._COMENTARIO].ToString();
                    _descProducto += Simbol._slash;

                    string _embalaje = _fila[TableDetalleDocumento._EMBALAJE].ToString();

                    if (_embalaje.Equals(Numeric._zeroDecimal))
                    {
                        _descProducto += string.Empty;
                    }
                    else
                    {
                        _descProducto += _embalaje;
                    }
                }

                if (pcodTipoTransaccion.Equals(ROLTransactions._regaliaSigla))
                {
                    _descProducto += Simbol._slash;

                    _descProducto += _fila[TableProducto._DESCPRODUCTO].ToString();
                }

                _lineaUno += _position.tabular(_lineaUno.Length, PosicionET._et_Codigo);
                _lineaUno += _fila[TableDetalleDocumento._CODPRODUCTO].ToString();

                _lineaUno += _position.tabular(_lineaUno.Length, PosicionET._et_Cantidad);
                decimal _cantidad = FormatUtil.convertStringToDecimal(_fila[TableDetalleDocumento._CANTIDAD].ToString());

                _totalCantidades += _cantidad;

                _lineaUno += FormatUtil.applyCurrencyFormat(_cantidad);

                _lineaUno += _position.tabular(_lineaUno.Length, PosicionET._et_Unidad);
                _lineaUno += _fila[TableProducto._UNIDAD].ToString();

                bool _exento = _fila[TableProducto._EXENTO].ToString().Equals(Variable._true);

                if (_exento)
                {
                    _lineaUno += _position.tabular(_lineaUno.Length, PosicionET._et_Exento);
                    _lineaUno += " ";
                }
                else
                {
                    _lineaUno += _position.tabular(_lineaUno.Length, PosicionET._et_Exento);
                    _lineaUno += Simbol._asterisk;
                }

                _lineaUno += _position.tabular(_lineaUno.Length, PosicionET._et_Precio);
                decimal _precio = FormatUtil.convertStringToDecimal(_fila[TableDetalleDocumento._PRECIO_UNI].ToString());

                if (pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla))
                {
                    _precio = Numeric._zeroDecimalInitialize;
                }

                if (pcodTipoTransaccion.Equals(ROLTransactions._regaliaSigla))
                {
                    _precio = Numeric._zeroDecimalInitialize;
                }

                _lineaUno += FormatUtil.applyCurrencyFormat(_precio);

                _lineaUno += _position.tabular(_lineaUno.Length, PosicionET._et_amount);

                _lineaUno += FormatUtil.applyCurrencyFormat(_precio * _cantidad);

                _lineaUno += Environment.NewLine;

                if (pcodTipoTransaccion.Equals(ROLTransactions._devolucionSigla))
                {
                    _lineaUno += _fila[TableDetalleDocumento._ESTADODEVOLUCION].ToString();
                    _lineaUno += Simbol._slash;

                    _lineaUno += _fila[TableMotivo._DESCRIPCION].ToString();

                    _lineaUno += Simbol._slash;
                    _lineaUno += _fila[TableDetalleDocumento._COMENTARIO].ToString();

                    _lineaUno += Environment.NewLine;
                }

                if (pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla))
                {
                    _totalLinea += Numeric._zeroDecimalInitialize;
                }
                else
                {
                    _totalLinea += FormatUtil.convertStringToDecimal(_fila[TableDetalleDocumento._TOTALLINEA].ToString());
                }

                if (pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla))
                {
                    _total_imp_lin = Numeric._zeroDecimalInitialize;
                }
                else
                {
                    if (pobjCliente.v_exento_imp)
                    {
                        _total_imp_lin = Numeric._zeroDecimalInitialize;
                    }
                    else
                    {
                        _total_imp_lin += FormatUtil.convertStringToDecimal(_fila[TableDetalleDocumento._TOTAL_IMP_LIN].ToString());
                    }
                }

                decimal _descuento = Numeric._zeroDecimalInitialize;

                if (pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla))
                {
                    _descuento = Numeric._zeroDecimalInitialize;
                }
                else
                {
                    _descuento = FormatUtil.convertStringToDecimal(_fila[TableDetalleDocumento._MONTO_DESCUENTO].ToString());
                }

                _totalDescuento += _descuento * _cantidad;

                _lineasImpresion += _descProducto;
                _lineasImpresion += Environment.NewLine;
                _lineasImpresion += _lineaUno;


                foreach (string singleline in Regex.Split(_lineasImpresion, Environment.NewLine))
                {
                    pprintingLinesList.Add(singleline + Environment.NewLine);
                }
            }

            _lineasImpresion = string.Empty;

            string _lineaDos = string.Empty;

            _lineaDos += _position.tabular(_lineaDos.Length, PosicionGeneral._total);
            _lineaDos += "SubTotal: ";
            _lineaDos += _position.tabular(_lineaDos.Length, PosicionGeneral._totalMonto);
            _lineaDos += FormatUtil.applyCurrencyFormat(_totalLinea + _totalDescuento);

            _lineaDos += Environment.NewLine;

            if (!pcodTipoTransaccion.Equals(ROLTransactions._regaliaSigla)
                && !pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla))
            {
                _lineasImpresion += _lineaDos;
            }

            string _lineaTres = string.Empty;
            _lineaTres += _position.tabular(_lineaTres.Length, PosicionGeneral._total);
            _lineaTres += "Imp. Venta: ";
            _lineaTres += _position.tabular(_lineaTres.Length, PosicionGeneral._totalMonto);
            _lineaTres += FormatUtil.applyCurrencyFormat(_totalDescuento);

            _lineaTres += Environment.NewLine;

            if (!pcodTipoTransaccion.Equals(ROLTransactions._regaliaSigla)
                && !pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla))
            {
                _lineasImpresion += _lineaTres;
            }

            string _lineaCuatro = string.Empty;
            _lineaCuatro += _position.tabular(_lineaCuatro.Length, PosicionGeneral._total);
            _lineaCuatro += "I.V.A.: ";
            _lineaCuatro += _position.tabular(_lineaCuatro.Length, PosicionGeneral._totalMonto);
            _lineaCuatro += FormatUtil.applyCurrencyFormat(_total_imp_lin);

            _lineaCuatro += Environment.NewLine;

            if (!pcodTipoTransaccion.Equals(ROLTransactions._regaliaSigla)
                && !pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla))
            {
                _lineasImpresion += _lineaCuatro;
            }

            string _lineaCinco = string.Empty;
            _lineaCinco += _position.tabular(_lineaCinco.Length, PosicionGeneral._total);
            _lineaCinco += "TOTAL: ";
            _lineaCinco += _position.tabular(_lineaCinco.Length, PosicionGeneral._totalMonto);
            _lineaCinco += FormatUtil.applyCurrencyFormat(_totalLinea + _total_imp_lin);

            _lineaCinco += Environment.NewLine;

            if (!pcodTipoTransaccion.Equals(ROLTransactions._regaliaSigla)
                && !pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla))
            {
                _lineasImpresion += _lineaCinco;
            }

            _lineasImpresion += Environment.NewLine;

            _lineasImpresion += "TOTAL DE LÍNEAS: " + pdt.Rows.Count;

            _lineasImpresion += Environment.NewLine;

            _lineasImpresion += "TOTAL DE CANTIDADES: " + FormatUtil.applyCurrencyFormat(_totalCantidades);

            _lineasImpresion += Environment.NewLine;

            foreach (string singleline in Regex.Split(_lineasImpresion, Environment.NewLine))
            {
                pprintingLinesList.Add(singleline + Environment.NewLine);
            }
        }
    }
}
