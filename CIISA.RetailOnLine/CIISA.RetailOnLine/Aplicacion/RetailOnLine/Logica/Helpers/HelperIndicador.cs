using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperIndicador
    {
        internal void buscarListaIndicadoresFacturacion(ListView ppnlIndicadores_ltvIndicadores, string pcodCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("I.NO_CIA, ");
            _sb.Append("I.NO_CLIENTE, ");
            _sb.Append("I.IND_PED, ");
            _sb.Append("I.IND_FACCONT, ");
            _sb.Append("I.IND_FACCRED, ");
            _sb.Append("I.IND_RESPETA_LIMITE, ");
            _sb.Append("I.IND_CHEQUE, ");
            _sb.Append("I.MONTO_LIMITE, ");
            _sb.Append("I.IND_VENCIMIENTO, ");
            _sb.Append("I.IND_ESTADO, ");
            _sb.Append("I.NO_AGENTE, ");
            _sb.Append("I.COBRADOR, ");
            _sb.Append("I.NO_ESTABLECIMIENTO, ");
            _sb.Append("I.IND_GEO, ");
            _sb.Append("I.IND_NUM_ORDEN, ");
            _sb.Append("I.IND_FECHA_ORDEN, ");
            _sb.Append("I.IND_NUM_RECEPCION, ");
            _sb.Append("I.IND_FECHA_RECEPCION, ");
            _sb.Append("I.IND_NUM_RECLAMO, ");
            _sb.Append("I.IND_FECHA_RECLAMO, ");
            _sb.Append("I.IND_COD_PROVEEDOR, ");
            _sb.Append("C.NOMBRE ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._indicadorFactura + " I, ");
            _sb.Append(TablesROL._cliente + " C ");
            _sb.Append("WHERE ");
            _sb.Append("I.NO_CLIENTE = ");
            _sb.Append("C.NO_CLIENTE ");
            _sb.Append("AND ");
            _sb.Append("I.NO_CLIENTE LIKE '%" + pcodCliente + "%'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            var Source = ppnlIndicadores_ltvIndicadores.ItemsSource as ObservableCollection<pnlIndicadores_ltvIndicadores>;

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlIndicadores_ltvIndicadores _lvi = new pnlIndicadores_ltvIndicadores();

                _lvi.NO_CIA = _fila["NO_CIA"].ToString();

                _lvi.NO_CLIENTE = _fila["NO_CLIENTE"].ToString();
                _lvi.NOMBRE = _fila["NOMBRE"].ToString();
                _lvi.IND_PED = _fila["IND_PED"].ToString();
                _lvi.IND_FACCONT = _fila["IND_FACCONT"].ToString();
                _lvi.IND_FACCRED = _fila["IND_FACCRED"].ToString();
                _lvi.IND_RESPETA_LIMITE = _fila["IND_RESPETA_LIMITE"].ToString();
                _lvi.IND_CHEQUE = _fila["IND_CHEQUE"].ToString();

                decimal _monto = FormatUtil.convertStringToDecimal(_fila["MONTO_LIMITE"].ToString());

                _lvi.MONTO_LIMITE = FormatUtil.applyCurrencyFormat(_monto);

                _lvi.IND_VENCIMIENTO = _fila["IND_VENCIMIENTO"].ToString();
                _lvi.IND_ESTADO = _fila["IND_ESTADO"].ToString();

                _lvi.IND_GEO = _fila["IND_GEO"].ToString();

                if (_fila["IND_ESTADO"].ToString().Equals(SQL._No))
                {
                    _lvi.ItemTextColor = (Color)App.Current.Resources["RedColor"];
                }
                else
                {
                    _lvi.ItemTextColor = Color.Default;
                }

                _lvi.IND_NUM_ORDEN = _fila["IND_NUM_ORDEN"].ToString();
                _lvi.IND_FECHA_ORDEN = _fila["IND_FECHA_ORDEN"].ToString();
                _lvi.IND_NUM_RECEPCION = _fila["IND_NUM_RECEPCION"].ToString();
                _lvi.IND_FECHA_RECEPCION = _fila["IND_FECHA_RECEPCION"].ToString();
                _lvi.IND_NUM_RECLAMO = _fila["IND_NUM_RECLAMO"].ToString();
                _lvi.IND_FECHA_RECLAMO = _fila["IND_FECHA_RECLAMO"].ToString();
                _lvi.IND_COD_PROVEEDOR = _fila["IND_COD_PROVEEDOR"].ToString();

                _lvi.NO_AGENTE = _fila["NO_AGENTE"].ToString();
                _lvi.COBRADOR = _fila["COBRADOR"].ToString();

                Source.Add(_lvi);
            }

            ppnlIndicadores_ltvIndicadores.ItemsSource = Source;
        }

        internal async Task buscarIndicadoresEstablecimiento(Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("IND_PED, ");
            _sb.Append("IND_FACCONT, ");
            _sb.Append("IND_FACCRED, ");
            _sb.Append("IND_RESPETA_LIMITE, ");
            _sb.Append("IND_CHEQUE, ");
            _sb.Append("MONTO_LIMITE, ");
            _sb.Append("IND_VENCIMIENTO, ");
            _sb.Append("IND_ESTADO, ");
            _sb.Append("NO_AGENTE, ");
            _sb.Append("COBRADOR, ");
            _sb.Append("IND_COBRO, ");
            //_sb.Append("NO_ESTABLECIMIENTO ");
            //Validacion 50mts
            _sb.Append("NO_ESTABLECIMIENTO, ");
            _sb.Append("IND_GEO, ");
            //FACTURACION ELECTRONICA
            _sb.Append("IND_NUM_ORDEN, ");
            _sb.Append("IND_FECHA_ORDEN, ");
            _sb.Append("IND_NUM_RECEPCION, ");
            _sb.Append("IND_FECHA_RECEPCION, ");
            _sb.Append("IND_NUM_RECLAMO, ");
            _sb.Append("IND_FECHA_RECLAMO, ");
            _sb.Append("IND_COD_PROVEEDOR ");

            _sb.Append("FROM ");
            _sb.Append(TablesROL._indicadorFactura + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_CLIENTE != ");
            _sb.Append("COBRADOR ");
            _sb.Append("AND ");
            _sb.Append("NO_CLIENTE = ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "' ");
            _sb.Append("AND ");
            _sb.Append("NO_ESTABLECIMIENTO LIKE ");
            _sb.Append("'%" + pobjCliente.v_objEstablecimiento.v_codEstablecimiento + "%' ");
            _sb.Append("AND ");
            _sb.Append("NO_AGENTE LIKE ");
            _sb.Append("'%" + pobjCliente.v_objEstablecimiento.v_objIndicador.v_no_agente + "%' ");
            _sb.Append("AND ");
            _sb.Append("COBRADOR LIKE ");
            _sb.Append("'%" + pobjCliente.v_objEstablecimiento.v_objIndicador.v_cobrador + "%' ");
            _sb.Append("AND ");
            _sb.Append("IND_COBRO LIKE ");
            _sb.Append("'%" + MiscUtils.getVariableStringSQLState(pobjCliente.v_objEstablecimiento.v_objIndicador.v_esCobrador) + "%' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            if (_dt.Rows.Count > 0)
            {
                foreach (DataRow _fila in _dt.Rows)
                {
                    if (_fila["IND_COBRO"].ToString().Equals(SQL._No))
                    {
                        if (_fila["IND_PED"].ToString().Equals(Indicators._S))
                        {
                            pobjCliente.v_objEstablecimiento.v_objIndicador.v_pedido = true;
                        }
                        else
                        {
                            pobjCliente.v_objEstablecimiento.v_objIndicador.v_pedido = false;
                        }

                        if (_fila["IND_FACCONT"].ToString().Equals(Indicators._S))
                        {
                            pobjCliente.v_objEstablecimiento.v_objIndicador.v_facturaContado = true;
                        }
                        else
                        {
                            pobjCliente.v_objEstablecimiento.v_objIndicador.v_facturaContado = false;
                        }

                        if (_fila["IND_FACCRED"].ToString().Equals(Indicators._S))
                        {
                            pobjCliente.v_objEstablecimiento.v_objIndicador.v_facturaCredito = true;
                        }
                        else
                        {
                            pobjCliente.v_objEstablecimiento.v_objIndicador.v_facturaCredito = false;
                        }

                        if (_fila["IND_RESPETA_LIMITE"].ToString().Equals(Indicators._S))
                        {
                            pobjCliente.v_objEstablecimiento.v_objIndicador.v_respetaLimiteCredito = true;
                        }
                        else
                        {
                            pobjCliente.v_objEstablecimiento.v_objIndicador.v_respetaLimiteCredito = false;
                        }

                        if (_fila["IND_CHEQUE"].ToString().Equals(Indicators._S))
                        {
                            pobjCliente.v_objEstablecimiento.v_objIndicador.v_condicionCheque = true;
                        }
                        else
                        {
                            pobjCliente.v_objEstablecimiento.v_objIndicador.v_condicionCheque = false;
                        }

                        if (_fila["IND_VENCIMIENTO"].ToString().Equals(Indicators._S))
                        {
                            pobjCliente.v_objEstablecimiento.v_objIndicador.v_vencimiento = true;
                        }
                        else
                        {
                            pobjCliente.v_objEstablecimiento.v_objIndicador.v_vencimiento = false;
                        }

                        string _limiteCredito = _fila["MONTO_LIMITE"].ToString();
                        decimal _lc = FormatUtil.convertStringToDecimal(_limiteCredito);

                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_limiteCredito = _lc;

                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_esCobrador = false;
                    }
                    else
                    {
                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_pedido = false;

                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_facturaContado = false;

                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_facturaCredito = false;

                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_respetaLimiteCredito = false;

                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_condicionCheque = false;

                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_vencimiento = false;

                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_limiteCredito = Numeric._zeroDecimalInitialize;

                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_esCobrador = true;
                    }

                    if (_fila["IND_ESTADO"].ToString().Equals(Indicators._S))
                    {
                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_estado = true;
                    }
                    else
                    {
                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_estado = false;
                    }

                    if (_fila["IND_GEO"].ToString().Equals(Indicators._S))
                    {
                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_AplicaGeo = true;
                    }
                    else
                    {
                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_AplicaGeo = false;
                    }

                    #region Facturación Electrónica

                    if (_fila["IND_NUM_ORDEN"].ToString().Equals(Indicators._S))
                    {
                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_numOrden = true;
                    }
                    else
                    {
                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_numOrden = false;
                    }

                    if (_fila["IND_FECHA_ORDEN"].ToString().Equals(Indicators._S))
                    {
                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_fechaOrden = true;
                    }
                    else
                    {
                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_fechaOrden = false;
                    }

                    if (_fila["IND_NUM_RECEPCION"].ToString().Equals(Indicators._S))
                    {
                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_numRecibo = true;
                    }
                    else
                    {
                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_numRecibo = false;
                    }

                    if (_fila["IND_FECHA_RECEPCION"].ToString().Equals(Indicators._S))
                    {
                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_fechaRecibo = true;
                    }
                    else
                    {
                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_fechaRecibo = false;
                    }

                    if (_fila["IND_NUM_RECLAMO"].ToString().Equals(Indicators._S))
                    {
                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_numReclamo = true;
                    }
                    else
                    {
                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_numReclamo = false;
                    }

                    if (_fila["IND_FECHA_RECLAMO"].ToString().Equals(Indicators._S))
                    {
                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_fechaReclamo = true;
                    }
                    else
                    {
                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_fechaReclamo = false;
                    }

                    if (_fila["IND_COD_PROVEEDOR"].ToString().Equals(Indicators._S))
                    {
                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_codProveedor = true;
                    }
                    else
                    {
                        pobjCliente.v_objEstablecimiento.v_objIndicador.v_codProveedor = false;
                    }

                    #endregion

                    pobjCliente.v_objEstablecimiento.v_objIndicador.v_no_agente = _fila["NO_AGENTE"].ToString();

                    pobjCliente.v_objEstablecimiento.v_objIndicador.v_cobrador = _fila["COBRADOR"].ToString();
                }
            }
            else
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention(
                    "No hay indicadores para el Cliente."
                    + Environment.NewLine
                    + Environment.NewLine
                    + "Cargue indicadores para realizar la gestión de venta"
                    );
            }
        }

        internal bool ExisteIndicadoresCliente(string pcodCliente, string pcodEstablecimiento)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("IND_PED, ");
            _sb.Append("IND_FACCONT, ");
            _sb.Append("IND_FACCRED, ");
            _sb.Append("IND_RESPETA_LIMITE, ");
            _sb.Append("IND_CHEQUE, ");
            _sb.Append("MONTO_LIMITE, ");
            _sb.Append("IND_VENCIMIENTO, ");
            _sb.Append("IND_ESTADO, ");
            _sb.Append("NO_AGENTE, ");
            _sb.Append("COBRADOR, ");
            _sb.Append("IND_COBRO, ");
            _sb.Append("NO_ESTABLECIMIENTO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._indicadorFactura + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_CLIENTE = ");
            _sb.Append("'" + pcodCliente + "' ");
            _sb.Append("AND ");
            _sb.Append("NO_ESTABLECIMIENTO LIKE ");
            _sb.Append("'%" + pcodEstablecimiento + "%' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            if (_dt.Rows != null)
            {
                if (_dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        internal void nuevoIndicador(Cliente pobjCliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._indicadorFactura + " ");
            _sb.Append("(");
            _sb.Append("NO_CIA, ");
            _sb.Append("NO_CLIENTE, ");
            _sb.Append("IND_PED, ");
            _sb.Append("IND_FACCONT, ");
            _sb.Append("IND_FACCRED, ");
            _sb.Append("IND_RESPETA_LIMITE, ");
            _sb.Append("IND_CHEQUE, ");
            _sb.Append("MONTO_LIMITE, ");
            _sb.Append("IND_VENCIMIENTO, ");
            _sb.Append("IND_ESTADO, ");
            _sb.Append("NO_AGENTE, ");
            _sb.Append("COBRADOR, ");
            _sb.Append("IND_COBRO, ");
            _sb.Append("NO_ESTABLECIMIENTO, ");
            _sb.Append("IND_GEO, ");
            _sb.Append("IND_NUM_ORDEN, ");
            _sb.Append("IND_FECHA_ORDEN, ");
            _sb.Append("IND_NUM_RECEPCION, ");
            _sb.Append("IND_FECHA_RECEPCION, ");
            _sb.Append("IND_NUM_RECLAMO, ");
            _sb.Append("IND_FECHA_RECLAMO, ");
            _sb.Append("IND_COD_PROVEEDOR ");
            _sb.Append(") ");
            _sb.Append("VALUES ");
            _sb.Append("(");
            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany + "', ");
            _sb.Append("'" + pobjCliente.v_no_cliente + "', ");
            _sb.Append("'" + SQL._Si + "', ");
            _sb.Append("'" + SQL._Si + "', ");
            _sb.Append("'" + SQL._No + "', ");
            _sb.Append("'" + SQL._Si + "', ");

            _sb.Append("'" + SQL._No + "', ");
            _sb.Append("'" + Numeric._zeroInteger + "', ");
            _sb.Append("'" + SQL._Si + "', ");

            _sb.Append("'" + SQL._Si + "', ");
            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "', ");
            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "', ");
            _sb.Append("'" + SQL._No + "', ");
            _sb.Append("" + Agent._codigoEstablecimientoRuteros + ", ");

            if (pobjCliente.v_latitud != 0 && pobjCliente.v_longitud != 0)
            {
                _sb.Append("'" + SQL._Si + "', ");
            }
            else
            {
                _sb.Append("'" + SQL._No + "', ");
            }

            #region FACTURACIÓN ELECTRÓNICA
            _sb.Append("'" + SQL._No + "', ");
            _sb.Append("'" + SQL._No + "', ");
            _sb.Append("'" + SQL._No + "', ");
            _sb.Append("'" + SQL._No + "', ");
            _sb.Append("'" + SQL._No + "', ");
            _sb.Append("'" + SQL._No + "', ");
            _sb.Append("'" + SQL._No + "' ");
            #endregion

            _sb.Append(")");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.insertRecord(_sb);
        }

        internal bool buscarIndicadorTramite(string pcodCliente, int pcodEstablecimiento)
        {
            StringBuilder _sb = new StringBuilder();
            bool Tramitar = false;

            _sb.Append("SELECT ");
            _sb.Append("TRAMITE_FACT ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._indicadorFactura + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_CLIENTE = ");
            _sb.Append("'" + pcodCliente + "' ");
            _sb.Append("AND ");
            _sb.Append("NO_ESTABLECIMIENTO LIKE ");
            _sb.Append("'%" + pcodEstablecimiento + "%' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            if (_dt.Rows.Count > 0)
            {
                foreach (DataRow _fila in _dt.Rows)
                {
                    Tramitar = _fila["TRAMITE_FACT"].ToString().Equals(SQL._Si) ? true : false;
                }
            }

            return Tramitar;

        }
    }
}
