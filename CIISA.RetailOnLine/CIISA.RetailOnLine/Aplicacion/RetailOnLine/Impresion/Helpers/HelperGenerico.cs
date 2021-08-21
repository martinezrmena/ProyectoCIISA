using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo.Exoneracion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionDocumento;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Constantes;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Simbologia;
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
using System.Text.RegularExpressions;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers
{
    internal class HelperGenerico
    {
        internal void buscarLineasDetalleImpresion(string pcodTransaction, string pcodTipoTransaccion, List<string> pprintingLinesList, Cliente pobjCliente, DataTable pdt)
        {
            #region Variables
            #region Heredadas
            string _lineasImpresion = string.Empty;
            decimal _totalLinea = Numeric._zeroDecimalInitialize;
            decimal _total_imp_lin = Numeric._zeroDecimalInitialize;
            decimal _totalDescuento = Numeric._zeroDecimalInitialize;
            decimal _totalCantidades = Numeric._zeroDecimalInitialize;
            string _lineaUno = string.Empty;
            Position _position = new Position();
            #endregion

            #region Detalles Reses
            string _lineaDetallesReses = string.Empty;
            int NumeroLinea = 0;
            int i = 0;
            decimal totalDetalleReses = 0;
            #endregion

            #region Exoneracion
            List<Result_Exoneracion> ListResultadosExoneracion = new List<Result_Exoneracion>();
            decimal _totalExonerado = Numeric._zeroDecimalInitialize;
            decimal _total_iva_exonerado = Numeric._zeroDecimalInitialize;

            decimal _total_IVA_INICIAL = Numeric._zeroDecimalInitialize; //Es el iva inicial
            decimal _total_IVA_EXO = Numeric._zeroDecimalInitialize; //Es el iva que se termina cobrando despues de aplicar el exonerado
            decimal _total_LINEA = Numeric._zeroDecimalInitialize; //Es el precio de la linea
            decimal _total_IVA_LINEA_EXO = Numeric._zeroDecimalInitialize; //Es el iva exonerado en esa linea

            #endregion

            #region Simbolos
            HelperSimbologia helperSimbologia = new HelperSimbologia();
            List<pnlSimbologia_ltvTiposIva> ListaSimbolos = helperSimbologia.buscarListaSimbologia();
            #endregion
            #endregion

            foreach (DataRow _fila in pdt.Rows)
            {
                _lineasImpresion = string.Empty;
                string _ImpuestoTotal = string.Empty;
                string _descProducto = string.Empty;
                _lineaUno = string.Empty;
                _descProducto += _fila[TableProducto._DESCPRODUCTO].ToString();

                #region Tipo Exoneracion por linea
                if (!pcodTipoTransaccion.Equals(ROLTransactions._regaliaSigla) && !pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla))
                {
                    ///Se verifica el tipo de exoneracion de la linea
                    string tipo = _fila[TableDetalleDocumento._TIPO_EXO].ToString();
                    Result_Exoneracion result1 = new Result_Exoneracion();
                    
                    var impuesto = FormatUtil.convertStringToDecimal(_fila[TableDetalleDocumento._PORCENTAJE_IMP_EXO].ToString());

                    if (!string.IsNullOrEmpty(tipo))
                    {
                        result1 = ListResultadosExoneracion.Where(x => x.Impuesto == impuesto && x.Tipo == tipo).FirstOrDefault();
                    }
                    else
                    {
                        result1 = ListResultadosExoneracion.Where(x => x.Impuesto == impuesto).FirstOrDefault();
                    }

                    Result_Exoneracion _Exoneracion = new Result_Exoneracion();

                    _Exoneracion.Impuesto = impuesto;
                    _Exoneracion.Tipo = tipo;
                    _Exoneracion.TotalImpuesto = FormatUtil.convertStringToDecimal(_fila[TableDetalleDocumento._PORCENTAJE_IMP_EXO].ToString());

                    if (_Exoneracion.TotalImpuesto > 0)
                    {
                        _Exoneracion.Gravado = FormatUtil.convertStringToDecimal(_fila[TableDetalleDocumento._TOTALLINEA].ToString());
                        _Exoneracion.TotalImpuesto = ((_Exoneracion.TotalImpuesto / 100) * _Exoneracion.Gravado);

                        if (result1 == null)
                        {
                            //No existe entonces agregamos esa categoria
                            ListResultadosExoneracion.Add(_Exoneracion);
                        }
                        else
                        {
                            //ya existe entonces actualizamos esa categoria
                            ListResultadosExoneracion.Where(x => x.Impuesto == impuesto).FirstOrDefault().Gravado += _Exoneracion.Gravado;
                            ListResultadosExoneracion.Where(x => x.Impuesto == impuesto).FirstOrDefault().TotalImpuesto += _Exoneracion.TotalImpuesto;
                        }
                    }
                }
                #endregion

                ///Se obtiene el impuesto total cobrado sobre la linea del producto
                _ImpuestoTotal = _fila[TableDetalleDocumento._PORCENTAJE_IMP].ToString();

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
                    _total_imp_lin += FormatUtil.convertStringToDecimal(_fila[TableDetalleDocumento._TOTAL_IMP_LIN].ToString());

                    //Es necesario verificar que en realidad si se este trabajando con exonerados
                    string EXONERA_ID = _fila[TableDetalleDocumento._EXONERAID].ToString();

                    if (!string.IsNullOrEmpty(EXONERA_ID))
                    {

                        _total_IVA_INICIAL = FormatUtil.convertStringToDecimal(_fila[TableDetalleDocumento._PORCENTAJE_IMP].ToString());
                        _total_IVA_EXO = FormatUtil.convertStringToDecimal(_fila[TableDetalleDocumento._PORCENTAJE_IMP_EXO].ToString());
                        _total_LINEA = FormatUtil.convertStringToDecimal(_fila[TableDetalleDocumento._TOTALLINEA].ToString());
                        _total_IVA_LINEA_EXO = _total_IVA_INICIAL - _total_IVA_EXO;

                        //El total de la linea
                        _totalExonerado += _total_LINEA;

                        //El total iva exonerado sera el resultado del iva que no se utilizo a causa del exonerado (IVA_INICIAL - IVATOTALEXONERADO)
                        //multiplicado con el total de la linea
                        _total_iva_exonerado += (_total_LINEA * (_total_IVA_LINEA_EXO / 100));
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

                //Es necesario consultar en la BD el simbolo para el articulo
                var articulo = helperSimbologia.buscarArticulo(_fila[TableDetalleDocumento._CODPRODUCTO].ToString());
                _lineasImpresion += helperSimbologia.buscarArticuloSimbolo(ListaSimbolos, articulo);

                foreach (string singleline in Regex.Split(_lineasImpresion, Environment.NewLine))
                {
                    if (!string.IsNullOrEmpty(singleline))
                    {
                        pprintingLinesList.Add(singleline + Environment.NewLine);
                    }
                    
                }

                #region Detalles reses
                if (pobjCliente.v_objTransaccion.v_listaDetalles.Count > 0)
                {
                    NumeroLinea = 0;

                    int.TryParse(_fila[TableDetalleDocumento._NUMLINEA].ToString(), out NumeroLinea);

                    var DetallesReses = pobjCliente.v_objTransaccion.v_listaDetalles.Where(x => x.v_numLinea == NumeroLinea).FirstOrDefault().v_listaDetalleReses;

                    if (DetallesReses.Count > 0)
                    {
                        _lineaDetallesReses = Environment.NewLine;

                        _lineaDetallesReses = Simbol._simpletab;
                        _lineaDetallesReses += _position.tabular(_lineaDetallesReses.Length, PosicionET._et_Articulo);
                        _lineaDetallesReses += "N.";
                        _lineaDetallesReses += _position.tabular(_lineaDetallesReses.Length, PosicionET._et_Peso);
                        _lineaDetallesReses += "Peso";
                        _lineaDetallesReses += _position.tabular(_lineaDetallesReses.Length, PosicionET._et_Matanza);
                        _lineaDetallesReses += "F. Matanza";
                        _lineaDetallesReses += Environment.NewLine;

                        i = 1;
                        totalDetalleReses = 0;

                        foreach (var detalle_reses in DetallesReses)
                        {
                            string lineaDR = Simbol._simpletab;

                            lineaDR += _position.tabular(lineaDR.Length, PosicionET._et_Articulo);
                            lineaDR += (i + Simbol._point);
                            lineaDR += _position.tabular(lineaDR.Length, PosicionET._et_Peso);
                            lineaDR += (detalle_reses._vc_peso + ".KGS");
                            lineaDR += _position.tabular(lineaDR.Length, PosicionET._et_Matanza);

                            try
                            {
                                detalle_reses._vc_fechamatanza = Regex.Split(detalle_reses._vc_fechamatanza, " ").FirstOrDefault();
                            }
                            catch (Exception)
                            {
                                detalle_reses._vc_fechamatanza = string.Empty;
                            }

                            lineaDR += detalle_reses._vc_fechamatanza;
                            lineaDR += Environment.NewLine;

                            totalDetalleReses += detalle_reses._vc_peso;

                            _lineaDetallesReses += lineaDR;

                            i++;

                        }

                        _lineaDetallesReses += Simbol._simpletab;
                        _lineaDetallesReses += "TOTAL PESO: " + totalDetalleReses + ".KGS";
                        _lineaDetallesReses += Environment.NewLine;
                        _lineaDetallesReses += Environment.NewLine;

                        foreach (string singleline in Regex.Split(_lineaDetallesReses, Environment.NewLine))
                        {
                            if (!string.IsNullOrEmpty(singleline))
                            {
                                pprintingLinesList.Add(singleline + Environment.NewLine);
                            }

                        }

                    }
                }
                #endregion

            }

            #region Sub total
            _lineasImpresion = string.Empty;

            string _lineaDos = string.Empty;

            _lineaDos += _position.tabular(_lineaDos.Length, PosicionGeneral._total);
            _lineaDos += "SubTotal:";
            _lineaDos += _position.tabular(_lineaDos.Length, PosicionGeneral._totalMonto);
            _lineaDos += FormatUtil.applyCurrencyFormat(_totalLinea + _totalDescuento);

            _lineaDos += Environment.NewLine;

            if (!pcodTipoTransaccion.Equals(ROLTransactions._regaliaSigla)
                && !pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla))
            {
                _lineasImpresion += _lineaDos;
            }
            #endregion

            #region Descuentos
            string _lineaTres = string.Empty;
            _lineaTres += _position.tabular(_lineaTres.Length, PosicionGeneral._total);
            _lineaTres += "Total Descuento:";
            _lineaTres += _position.tabular(_lineaTres.Length, PosicionGeneral._totalMonto);
            _lineaTres += FormatUtil.applyCurrencyFormat(_totalDescuento);

            _lineaTres += Environment.NewLine;

            if (!pcodTipoTransaccion.Equals(ROLTransactions._regaliaSigla)
                && !pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla))
            {
                _lineasImpresion += _lineaTres;
            }
            #endregion

            #region Categorias de exoneracion en los productos

            if (!pcodTipoTransaccion.Equals(ROLTransactions._regaliaSigla) && !pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla))
            {
                string _lineaCategoriasExo = string.Empty;
                string _lineaIndividualImpuesto = string.Empty;
                string _lineaIndividualGravado = string.Empty;

                foreach (var CatExoneracion in ListResultadosExoneracion ?? new List<Result_Exoneracion>())
                {
                    _lineaIndividualImpuesto = string.Empty;
                    _lineaIndividualGravado = string.Empty;

                    //Gravado
                    _lineaIndividualGravado += _position.tabular(_lineaIndividualGravado.Length, PosicionGeneral._total);
                    _lineaIndividualGravado += ("Gravado al " + CatExoneracion.Impuesto + Simbol._percentaje);
                    _lineaIndividualGravado += _position.tabular(_lineaIndividualGravado.Length, PosicionGeneral._totalMonto);
                    _lineaIndividualGravado += FormatUtil.applyCurrencyFormat(CatExoneracion.Gravado);
                    _lineaIndividualGravado += Environment.NewLine;

                    //Impuesto
                    _lineaIndividualImpuesto += _position.tabular(_lineaIndividualImpuesto.Length, PosicionGeneral._total);
                    _lineaIndividualImpuesto += ("Impuesto al " + CatExoneracion.Impuesto + Simbol._percentaje);
                    _lineaIndividualImpuesto += _position.tabular(_lineaIndividualImpuesto.Length, PosicionGeneral._totalMonto);
                    _lineaIndividualImpuesto += FormatUtil.applyCurrencyFormat(CatExoneracion.TotalImpuesto);
                    _lineaIndividualImpuesto += Environment.NewLine;

                    _lineaCategoriasExo += _lineaIndividualGravado;
                    _lineaCategoriasExo += _lineaIndividualImpuesto;
                }

                _lineasImpresion += _lineaCategoriasExo;
            }
            #endregion

            #region Totales de exoneracion
            //Se agrega la funcionalidad para exoneraciones
            string _lineaTotalExonerado = string.Empty;
            _lineaTotalExonerado += _position.tabular(_lineaTotalExonerado.Length, PosicionGeneral._total);
            _lineaTotalExonerado += "Exonerado:";
            _lineaTotalExonerado += _position.tabular(_lineaTotalExonerado.Length, PosicionGeneral._totalMonto);
            _lineaTotalExonerado += FormatUtil.applyCurrencyFormat(_totalExonerado);

            _lineaTotalExonerado += Environment.NewLine;

            if (!pcodTipoTransaccion.Equals(ROLTransactions._regaliaSigla)
                && !pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla))
            {
                _lineasImpresion += _lineaTotalExonerado;
            }

            //Se agrega el total iva exonerado
            string _lineaIvaExonerado = string.Empty;
            _lineaIvaExonerado += _position.tabular(_lineaIvaExonerado.Length, PosicionGeneral._total);
            _lineaIvaExonerado += "I.V.A. Exonerado:";
            _lineaIvaExonerado += _position.tabular(_lineaIvaExonerado.Length, PosicionGeneral._totalMonto);
            _lineaIvaExonerado += FormatUtil.applyCurrencyFormat(_total_iva_exonerado);

            _lineaIvaExonerado += Environment.NewLine;

            if (!pcodTipoTransaccion.Equals(ROLTransactions._regaliaSigla)
                && !pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla))
            {
                _lineasImpresion += _lineaIvaExonerado;
            }
            #endregion

            #region IVA
            string _lineaCuatro = string.Empty;
            _lineaCuatro += _position.tabular(_lineaCuatro.Length, PosicionGeneral._total);
            _lineaCuatro += "I.V.A.:";
            _lineaCuatro += _position.tabular(_lineaCuatro.Length, PosicionGeneral._totalMonto);
            _lineaCuatro += FormatUtil.applyCurrencyFormat(_total_imp_lin);

            _lineaCuatro += Environment.NewLine;

            if (!pcodTipoTransaccion.Equals(ROLTransactions._regaliaSigla)
                && !pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla))
            {
                _lineasImpresion += _lineaCuatro;
            }
            #endregion

            #region Total Factura
            string _lineaCinco = string.Empty;
            _lineaCinco += _position.tabular(_lineaCinco.Length, PosicionGeneral._total);
            _lineaCinco += "TOTAL FACTURA:";
            _lineaCinco += _position.tabular(_lineaCinco.Length, PosicionGeneral._totalMonto);
            _lineaCinco += FormatUtil.applyCurrencyFormat(_totalLinea + _total_imp_lin);

            _lineaCinco += Environment.NewLine;

            if (!pcodTipoTransaccion.Equals(ROLTransactions._regaliaSigla)
                && !pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla))
            {
                _lineasImpresion += _lineaCinco;
            }
            #endregion

            #region Totales Finales
            _lineasImpresion += Environment.NewLine;

            _lineasImpresion += "TOTAL DE LÍNEAS: " + pdt.Rows.Count;

            _lineasImpresion += Environment.NewLine;

            _lineasImpresion += "TOTAL DE CANTIDADES: " + FormatUtil.applyCurrencyFormat(_totalCantidades);

            _lineasImpresion += Environment.NewLine;

            _lineasImpresion += Environment.NewLine;

            if (ListaSimbolos != null)
            {
                string _lineasSimbolo = string.Empty;

                if (ListaSimbolos.Count > 0)
                {
                    _lineasSimbolo = "El simbolo al final indica: ";
                    _lineasSimbolo = (_position.center(_lineasSimbolo.Length) + _lineasSimbolo);
                    _lineasSimbolo += Environment.NewLine;
                    _lineasImpresion += _lineasSimbolo;
                    _lineasSimbolo = string.Empty;
                }

                foreach (var Simbolo in ListaSimbolos ?? new List<pnlSimbologia_ltvTiposIva>())
                {
                    if (!string.IsNullOrEmpty(Simbolo.SIMBOLO))
                    {
                        _lineasSimbolo = Simbolo.SIMBOLO;
                        _lineasSimbolo += " IVA ";
                        _lineasSimbolo += Simbolo.PORCENTAJE;
                        _lineasSimbolo += Simbol._percentaje;
                        _lineasSimbolo = (_position.center(_lineasSimbolo.Length) + _lineasSimbolo);
                        _lineasSimbolo += Environment.NewLine;

                        _lineasImpresion += _lineasSimbolo;
                    }
                }
            }

            foreach (string singleline in Regex.Split(_lineasImpresion, Environment.NewLine))
            {
                pprintingLinesList.Add(singleline + Environment.NewLine);
            }
            #endregion

        }
    }
}
