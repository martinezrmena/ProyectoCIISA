using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.External.CustomTreeView;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperInventario
    {
        internal DateTime buscarFechaTomaFisica()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableInventario._FECHATOMA + " ) " + TableInventario._FECHATOMA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._inventario + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            string _fechaToma = MultiGeneric.readStringText(_sb);

            if (_fechaToma.Equals(String.Empty))
            {
                throw new Exception("Sin información de fecha toma física (debe cargar el inventario");
            }
            else
            {
                return FormatUtil.covertStringToDateTimeWithoutTime(_fechaToma);
            }
        }

        internal int buscarNumeroDiaDeLaSemanaFechaTomaFisica()
        {
            DateTime _fechaToma = buscarFechaTomaFisica();

            return VarTime.getDayOfWeek(_fechaToma);
        }

        internal ProductoInventario buscarInventarioProducto(string pcodProducto)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableInventario._CODCIA + ", ");
            _sb.Append(TableInventario._CODAGENTE + ", ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableInventario._FECHATOMA + " ) " + TableInventario._FECHATOMA + ", ");
            _sb.Append(TableInventario._CODPRODUCTO + ", ");
            _sb.Append(TableInventario._CANTIDAD + ", ");
            _sb.Append("ROUND(" + TableInventario._VENTAS + ", 2) "+ TableInventario._VENTAS + ", ");
            _sb.Append(TableInventario._DEVOLUCIONESBUENAS + ", ");
            _sb.Append(TableInventario._DEVOLUCIONESMALAS + ", ");
            _sb.Append(TableInventario._REGALIAS + ", ");
            _sb.Append(TableInventario._ANULACIONES + ", ");
            _sb.Append(TableInventario._ANULACIONESBUENAS + ", ");
            _sb.Append(TableInventario._ANULACIONESMALAS + ", ");
            _sb.Append(TableInventario._DISPONIBLE + ", ");
            _sb.Append(TableInventario._FECHACREA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._inventario + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableInventario._CODPRODUCTO + " = ");
            _sb.Append("'" + pcodProducto + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            ProductoInventario _objProductoInventario = new ProductoInventario();

            foreach (DataRow _fila in _dt.Rows)
            {
                _objProductoInventario = new ProductoInventario();

                _objProductoInventario.v_codCia = _fila[TableInventario._CODCIA].ToString();
                _objProductoInventario.v_codAgente = _fila[TableInventario._CODAGENTE].ToString();

                try
                {
                    _objProductoInventario.v_fechaToma =
                        FormatUtil.covertStringToDateTimeWithoutTime(_fila[TableInventario._FECHATOMA].ToString());
                }
                catch(Exception ex) {
                    throw ex;
                }
                _objProductoInventario.v_codProducto = _fila[TableInventario._CODPRODUCTO].ToString();

                _objProductoInventario.v_cantidad =
                    FormatUtil.convertStringToDecimal(_fila[TableInventario._CANTIDAD].ToString());

                _objProductoInventario.v_ventas =
                    FormatUtil.convertStringToDecimal(_fila[TableInventario._VENTAS].ToString());

                _objProductoInventario.v_devolucionesBuenas =
                    FormatUtil.convertStringToDecimal(_fila[TableInventario._DEVOLUCIONESBUENAS].ToString());

                _objProductoInventario.v_devolucionesMalas =
                    FormatUtil.convertStringToDecimal(_fila[TableInventario._DEVOLUCIONESMALAS].ToString());

                _objProductoInventario.v_regalias =
                    FormatUtil.convertStringToDecimal(_fila[TableInventario._REGALIAS].ToString());

                _objProductoInventario.v_anulaciones =
                    FormatUtil.convertStringToDecimal(_fila[TableInventario._ANULACIONES].ToString());

                _objProductoInventario.v_anulacionesBuenas =
                    FormatUtil.convertStringToDecimal(_fila[TableInventario._ANULACIONESBUENAS].ToString());

                _objProductoInventario.v_anulacionesMalas =
                    FormatUtil.convertStringToDecimal(_fila[TableInventario._ANULACIONESMALAS].ToString());

                _objProductoInventario.v_disponible =
                    FormatUtil.convertStringToDecimal(_fila[TableInventario._DISPONIBLE].ToString());

                _objProductoInventario.v_disponibleConsultado = true;

                _objProductoInventario.v_fechaCrea = _fila[TableInventario._FECHACREA].ToString();
            }

            return _objProductoInventario;
        }

        internal decimal buscarInventarioProductoDisponible(string pcodProducto)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableInventario._DISPONIBLE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._inventario + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableInventario._CODPRODUCTO + " = ");
            _sb.Append("'" + pcodProducto + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readDecimal(_sb);
        }

        internal decimal buscarInventarioProductoVentas(string pcodProducto)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableInventario._VENTAS + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._inventario + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableInventario._CODPRODUCTO + " = ");
            _sb.Append("'" + pcodProducto + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readDecimal(_sb);
        }

        internal decimal buscarInventarioProductoRegalias(string pcodProducto)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableInventario._REGALIAS + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._inventario + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableInventario._CODPRODUCTO + " = ");
            _sb.Append("'" + pcodProducto + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readDecimal(_sb);
        }

        internal void actualizarDisponible(decimal pdisponible, string pcodProducto)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._inventario + " ");
            _sb.Append("SET ");
            _sb.Append(TableInventario._DISPONIBLE + " = ");
            _sb.Append("REPLACE('" + pdisponible + "',',','') ");
            _sb.Append("WHERE ");
            _sb.Append(TableInventario._CODPRODUCTO + " = ");
            _sb.Append("'" + pcodProducto + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);
        }

        internal void calcularCantidadDisponible(string pcodProducto)
        {
            ProductoInventario _objProductoInventario = buscarInventarioProducto(pcodProducto);

            decimal _cantInicial = _objProductoInventario.v_cantidad;
            decimal _vent = _objProductoInventario.v_ventas;
            decimal _devoB = _objProductoInventario.v_devolucionesBuenas;
            decimal _devoM = _objProductoInventario.v_devolucionesMalas;
            decimal _rega = _objProductoInventario.v_regalias;
            decimal _anula = _objProductoInventario.v_anulaciones;
            decimal _anulaBuena = _objProductoInventario.v_anulacionesBuenas;
            decimal _anulaMala = _objProductoInventario.v_anulacionesMalas;
            decimal _disponible = Numeric._zeroDecimalInitialize;

            _disponible = _cantInicial - _vent + _devoB - _rega - _anulaBuena;

            if (_cantInicial == Numeric._zeroInteger)
            {
                _disponible -= _anula;
            }

            actualizarDisponible(_disponible, pcodProducto);
        }

        internal void recalcularProductoDisponibleEnInventario()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableInventario._CODPRODUCTO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._inventario);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            foreach (DataRow _fila in _dt.Rows)
            {
                calcularCantidadDisponible(
                    _fila[TableInventario._CODPRODUCTO].ToString()
                    );
            }
        }

        internal void actualizarInventarioAnulacion(TransaccionEncabezado pobjTransaccion)
        {
            foreach (TransaccionDetalle _transaccionDetalle in pobjTransaccion.v_listaDetalles)
            {
                StringBuilder _sb = new StringBuilder();

                if (_transaccionDetalle.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaCreditoSigla)
                    || _transaccionDetalle.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaContadoSigla)
                    || _transaccionDetalle.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._regaliaSigla))
                {
                    _sb = new StringBuilder();

                    _sb.Append("UPDATE ");
                    _sb.Append(TablesROL._inventario + " ");
                    _sb.Append("SET ");
                    _sb.Append(TableInventario._ANULACIONES + " = ");
                    _sb.Append(TableInventario._ANULACIONES + " + ");
                    _sb.Append("REPLACE('" + _transaccionDetalle.v_objProducto.v_cantTransaccion + "',',','') ");
                    _sb.Append("WHERE ");
                    _sb.Append(TableInventario._CODPRODUCTO + " = ");
                    _sb.Append("'" + _transaccionDetalle.v_objProducto.v_codProducto + "'");
                }

                if (_transaccionDetalle.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._devolucionSigla))
                {
                    if (_transaccionDetalle.v_objProducto.v_estado.Equals(Pedido._devolucionBuena))
                    {
                        _sb = new StringBuilder();

                        _sb.Append("UPDATE ");
                        _sb.Append(TablesROL._inventario + " ");
                        _sb.Append("SET ");
                        _sb.Append(TableInventario._ANULACIONESBUENAS + " = ");
                        _sb.Append(TableInventario._ANULACIONESBUENAS + " + ");
                        _sb.Append("REPLACE('" + _transaccionDetalle.v_objProducto.v_cantTransaccion + "',',','') ");
                        _sb.Append("WHERE ");
                        _sb.Append(TableInventario._CODPRODUCTO + " = ");
                        _sb.Append("'" + _transaccionDetalle.v_objProducto.v_codProducto + "'");
                    }
                    else
                    {
                        _sb = new StringBuilder();

                        _sb.Append("UPDATE ");
                        _sb.Append(TablesROL._inventario + " ");
                        _sb.Append("SET ");
                        _sb.Append(TableInventario._ANULACIONESMALAS + " = ");
                        _sb.Append(TableInventario._ANULACIONESMALAS + " + ");
                        _sb.Append("REPLACE('" + _transaccionDetalle.v_objProducto.v_cantTransaccion + "',',','') ");
                        _sb.Append("WHERE ");
                        _sb.Append(TableInventario._CODPRODUCTO + " = ");
                        _sb.Append("'" + _transaccionDetalle.v_objProducto.v_codProducto + "'");
                    }
                }

                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                MultiGeneric.updateRecord(_sb);

                calcularCantidadDisponible(_transaccionDetalle.v_objProducto.v_codProducto);
            }
        }

        internal void actualizarVentasDocumentosAnuladosConRegalias(TransaccionEncabezado pobjTransaccion)
        {
            foreach (TransaccionDetalle _objDetalle in pobjTransaccion.v_listaDetalles)
            {
                StringBuilder _sb = new StringBuilder();

                if (_objDetalle.v_objProducto.inventarioRegalias() > 0)
                {
                    _sb.Append("UPDATE ");
                    _sb.Append(TablesROL._inventario + " ");
                    _sb.Append("SET ");
                    _sb.Append(TableInventario._REGALIAS + " = ");
                    _sb.Append(TableInventario._REGALIAS + " - ");
                    _sb.Append("REPLACE('" + _objDetalle.v_objProducto.v_cantTransaccion + "',',','') ");
                    _sb.Append("WHERE ");
                    _sb.Append(TableInventario._CODPRODUCTO + " = ");
                    _sb.Append("'" + _objDetalle.v_objProducto.v_codProducto + "'");

                    var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                    MultiGeneric.updateRecord(_sb);
                }
                else
                {
                }
            }
        }

        internal void actualizarVentasDocumentosAnuladosConVentas(TransaccionEncabezado pobjTransaccion)
        {
            foreach (TransaccionDetalle _objDetalle in pobjTransaccion.v_listaDetalles)
            {
                StringBuilder _sb = new StringBuilder();

                if (_objDetalle.v_objProducto.inventarioVentas() > 0)
                {
                    _sb.Append("UPDATE ");
                    _sb.Append(TablesROL._inventario + " ");
                    _sb.Append("SET ");
                    _sb.Append(TableInventario._VENTAS + " = ");
                    _sb.Append(TableInventario._VENTAS + " - ");
                    _sb.Append("REPLACE('" + _objDetalle.v_objProducto.v_cantTransaccion + "',',','') ");
                    _sb.Append("WHERE ");
                    _sb.Append(TableInventario._CODPRODUCTO + " = ");
                    _sb.Append("'" + _objDetalle.v_objProducto.v_codProducto + "'");

                    var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                    MultiGeneric.updateRecord(_sb);
                }
                else
                {
                }
            }
        }

        internal bool InventarioVacio()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableInventario._CODCIA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._inventario + " ");

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

        internal bool ExisteProducto(string pcodProducto)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableInventario._CODPRODUCTO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._inventario + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableInventario._CODPRODUCTO + " = ");
            _sb.Append("'" + pcodProducto + "'");

            return OperationSQL.thereRecord(_sb, TableInventario._CODPRODUCTO);
        }

        private StringBuilder sentenciaInsertarActualizarInventario()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("INSERT ");
            _sb.Append("INTO ");
            _sb.Append(TablesROL._inventario + " ");
            _sb.Append("(");
            _sb.Append(TableInventario._CODCIA + ", ");
            _sb.Append(TableInventario._CODAGENTE + ", ");
            _sb.Append(TableInventario._FECHATOMA + ", ");
            _sb.Append(TableInventario._FECHATOMACONSOLIDA + ", ");
            _sb.Append(TableInventario._CODPRODUCTO + ", ");
            _sb.Append(TableInventario._CANTIDAD + ", ");
            _sb.Append(TableInventario._VENTAS + ", ");
            _sb.Append(TableInventario._DEVOLUCIONESBUENAS + ", ");
            _sb.Append(TableInventario._DEVOLUCIONESMALAS + ", ");
            _sb.Append(TableInventario._REGALIAS + ", ");
            _sb.Append(TableInventario._ANULACIONES + ", ");
            _sb.Append(TableInventario._ANULACIONESBUENAS + ", ");
            _sb.Append(TableInventario._ANULACIONESMALAS + ", ");
            _sb.Append(TableInventario._DISPONIBLE + ", ");
            _sb.Append(TableInventario._FECHACREA + ", ");
            _sb.Append(TableInventario._TOMA_FISICA + ", ");
            _sb.Append(TableInventario._CONSOLIDADO + ", ");
            _sb.Append(TableInventario._ENVIADO + ", ");
            _sb.Append(TableInventario._AUDITORIA);
            _sb.Append(") ");

            return _sb;
        }

        internal void actualizarInventarioTransaccion(Cliente pobjCliente)
        {
            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            foreach (TransaccionDetalle _objDetalle in pobjCliente.v_objTransaccion.v_listaDetalles)
            {
                bool _existeProducto = ExisteProducto(_objDetalle.v_objProducto.v_codProducto);

                StringBuilder _sb = new StringBuilder();

                bool _insertar = false;

                if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._devolucionSigla))
                {
                    if (_objDetalle.v_objProducto.v_estado.Equals(Pedido._devolucionBuena))
                    {
                        if (_existeProducto)
                        {
                            _sb.Append("UPDATE ");
                            _sb.Append(TablesROL._inventario + " ");
                            _sb.Append("SET ");
                            _sb.Append(TableInventario._DEVOLUCIONESBUENAS + " = ");
                            _sb.Append(TableInventario._DEVOLUCIONESBUENAS + " + ");
                            _sb.Append("REPLACE('" + _objDetalle.v_objProducto.v_cantTransaccion + "',',','') ");
                            _sb.Append("WHERE ");
                            _sb.Append(TableInventario._CODPRODUCTO + " = ");
                            _sb.Append("'" + _objDetalle.v_objProducto.v_codProducto + "'");
                        }
                        else
                        {
                            _sb = sentenciaInsertarActualizarInventario();
                            _sb.Append("VALUES ");
                            _sb.Append("(");
                            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany + "', ");
                            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "', ");
                            _sb.Append("'" + buscarFechaTomaFisica().ToString("yyyy-MM-dd HH:mm:ss") + "', ");
                            _sb.Append("DATE('NOW'), ");
                            _sb.Append("'" + _objDetalle.v_objProducto.v_codProducto + "', ");
                            _sb.Append("0, ");
                            _sb.Append("0, ");
                            _sb.Append("REPLACE('" + _objDetalle.v_objProducto.v_cantTransaccion + "',',',''), ");
                            _sb.Append("0, ");
                            _sb.Append("0, ");
                            _sb.Append("0, ");
                            _sb.Append("0, ");
                            _sb.Append("0, ");
                            _sb.Append("0, ");
                            _sb.Append("DATE('NOW'), ");
                            _sb.Append("0, ");
                            _sb.Append("'" + SQL._No + "', ");
                            _sb.Append("'" + SQL._No + "', ");
                            _sb.Append("0");
                            _sb.Append(")");

                            _insertar = true;
                        }
                    }

                    if (_objDetalle.v_objProducto.v_estado.Equals(Pedido._devolucionMala))
                    {
                        if (_existeProducto)
                        {
                            _sb.Append("UPDATE ");
                            _sb.Append(TablesROL._inventario + " ");
                            _sb.Append("SET ");
                            _sb.Append(TableInventario._DEVOLUCIONESMALAS + " = ");
                            _sb.Append(TableInventario._DEVOLUCIONESMALAS + " + ");
                            _sb.Append("REPLACE('" + _objDetalle.v_objProducto.v_cantTransaccion + "',',','') ");
                            _sb.Append("WHERE ");
                            _sb.Append(TableInventario._CODPRODUCTO + " = ");
                            _sb.Append("'" + _objDetalle.v_objProducto.v_codProducto + "'");
                        }
                        else
                        {
                            _sb = sentenciaInsertarActualizarInventario();
                            _sb.Append("VALUES ");
                            _sb.Append("(");
                            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany + "', ");
                            _sb.Append("'" + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent + "', ");
                            _sb.Append("'" + buscarFechaTomaFisica().ToString("yyyy-MM-dd HH:mm:ss") + "', ");
                            _sb.Append("DATE('NOW'), ");
                            _sb.Append("'" + _objDetalle.v_objProducto.v_codProducto + "', ");
                            _sb.Append("0, ");
                            _sb.Append("0, ");
                            _sb.Append("0, ");
                            _sb.Append("REPLACE('" + _objDetalle.v_objProducto.v_cantTransaccion + "',',',''), ");
                            _sb.Append("0, ");
                            _sb.Append("0, ");
                            _sb.Append("0, ");
                            _sb.Append("0, ");
                            _sb.Append("0, ");
                            _sb.Append("DATE('NOW'), ");
                            _sb.Append("0, ");
                            _sb.Append("'" + SQL._No + "', ");
                            _sb.Append("'" + SQL._No + "', ");
                            _sb.Append("0");
                            _sb.Append(")");

                            _insertar = true;
                        }
                    }
                }

                if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaContadoSigla))
                {
                    _sb.Append("UPDATE ");
                    _sb.Append(TablesROL._inventario + " ");
                    _sb.Append("SET ");
                    _sb.Append(TableInventario._VENTAS + " = ");
                    _sb.Append(TableInventario._VENTAS + " + ");
                    _sb.Append("REPLACE('" + _objDetalle.v_objProducto.v_cantTransaccion + "',',','') ");
                    _sb.Append("WHERE ");
                    _sb.Append(TableInventario._CODPRODUCTO + " = ");
                    _sb.Append("'" + _objDetalle.v_objProducto.v_codProducto + "'");
                }

                if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaCreditoSigla))
                {
                    _sb.Append("UPDATE ");
                    _sb.Append(TablesROL._inventario + " ");
                    _sb.Append("SET ");
                    _sb.Append(TableInventario._VENTAS + " = ");
                    _sb.Append(TableInventario._VENTAS + " + ");
                    _sb.Append("REPLACE('" + _objDetalle.v_objProducto.v_cantTransaccion + "',',','') ");
                    _sb.Append("WHERE ");
                    _sb.Append(TableInventario._CODPRODUCTO + " = ");
                    _sb.Append("'" + _objDetalle.v_objProducto.v_codProducto + "'");
                }


                if (pobjCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._regaliaSigla))
                {
                    _sb.Append("UPDATE ");
                    _sb.Append(TablesROL._inventario + " ");
                    _sb.Append("SET ");
                    _sb.Append(TableInventario._REGALIAS + " = ");
                    _sb.Append(TableInventario._REGALIAS + " + ");
                    _sb.Append("REPLACE('" + _objDetalle.v_objProducto.v_cantTransaccion + "',',','') ");
                    _sb.Append("WHERE ");
                    _sb.Append(TableInventario._CODPRODUCTO + " = ");
                    _sb.Append("'" + _objDetalle.v_objProducto.v_codProducto + "'");
                }

                if (_insertar)
                {
                    MultiGeneric.insertRecord(_sb);
                }
                else
                {
                    MultiGeneric.updateRecord(_sb);
                }

                calcularCantidadDisponible(_objDetalle.v_objProducto.v_codProducto);
            }
        }

        internal void buscarListaInventarioTeorico(ListView pltvProducto)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableInventario._CODCIA + ", ");
            _sb.Append(TableInventario._CODAGENTE + ", ");
            _sb.Append(TableInventario._FECHATOMA + ", ");
            _sb.Append(TableInventario._CODPRODUCTO + ", ");
            _sb.Append(TableInventario._CANTIDAD + ", ");
            _sb.Append(TableInventario._VENTAS + ", ");
            _sb.Append(TableInventario._DEVOLUCIONESBUENAS + ", ");
            _sb.Append(TableInventario._DEVOLUCIONESMALAS + ", ");
            _sb.Append(TableInventario._REGALIAS + ", ");
            _sb.Append(TableInventario._ANULACIONES + ", ");
            _sb.Append(TableInventario._ANULACIONESBUENAS + ", ");
            _sb.Append(TableInventario._ANULACIONESMALAS + ", ");
            _sb.Append(TableInventario._DISPONIBLE + ", ");
            _sb.Append(TableInventario._FECHACREA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._inventario + " ");
            _sb.Append("ORDER BY ");
            _sb.Append(TableInventario._CODPRODUCTO + " ASC");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            ObservableCollection<pnlInventario_ltvProducto> Source = new ObservableCollection<pnlInventario_ltvProducto>();

            foreach (DataRow _fila in _dt.Rows)
            {
                pnlInventario_ltvProducto _lvi = new pnlInventario_ltvProducto();
                _lvi.Compannia = _fila[TableInventario._CODCIA].ToString();
                _lvi.Agente = _fila[TableInventario._CODAGENTE].ToString();
                _lvi.FechaToma = _fila[TableInventario._FECHATOMA].ToString();
                _lvi.CodigoProducto = _fila[TableInventario._CODPRODUCTO].ToString();
                _lvi.Cantidad = double.Parse(_fila[TableInventario._CANTIDAD].ToString()).ToString("N2");
                _lvi.Ventas = _fila[TableInventario._VENTAS].ToString();
                _lvi.DevolucionesBuenas = _fila[TableInventario._DEVOLUCIONESBUENAS].ToString();
                _lvi.DevolucionesMalas = _fila[TableInventario._DEVOLUCIONESMALAS].ToString();
                _lvi.Regalias = _fila[TableInventario._REGALIAS].ToString();
                _lvi.Anulaciones = _fila[TableInventario._ANULACIONES].ToString();
                _lvi.AnulacionesBuenas = _fila[TableInventario._ANULACIONESBUENAS].ToString();
                _lvi.AnulacionesMalas = _fila[TableInventario._ANULACIONESMALAS].ToString();
                _lvi.Disponible = double.Parse(_fila[TableInventario._DISPONIBLE].ToString()).ToString("N2");
                _lvi.FechaCreación = _fila[TableInventario._FECHACREA].ToString();

                Source.Add(_lvi);
            }

            pltvProducto.ItemsSource = Source;
        }

        //internal void buscarInventarioPedidos(TreeView ptrvInventarioPedidos, string pcodProducto)
        internal void buscarInventarioPedidos(ListView ptrvInventarioPedidos, string pcodProducto)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("DP." + TableDetallePedido._ARTI_DES + ", ");
            _sb.Append("DP." + TableDetallePedido._CAN_DES + ", ");
            _sb.Append("I." + TableInventario._DISPONIBLE + ", ");
            _sb.Append("P." + TableProducto._DESCPRODUCTO + ", ");
            _sb.Append("C." + TableCliente._NO_CLIENTE + ", ");
            _sb.Append("C." + TableCliente._NOMBRE + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoPedido + " EP, ");
            _sb.Append(TablesROL._detallePedido + " DP, ");
            _sb.Append(TablesROL._cliente + " C, ");
            _sb.Append(TablesROL._producto + " P, ");
            _sb.Append(TablesROL._inventario + " I ");
            _sb.Append("WHERE ");
            _sb.Append("DP." + TableDetallePedido._ARTI_DES + " ");
            _sb.Append("LIKE ");
            _sb.Append("'%" + pcodProducto + "%' ");
            _sb.Append("AND ");
            _sb.Append("DP." + TableDetallePedido._APLICADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append("AND ");
            _sb.Append("DP." + TableDetallePedido._NO_TRANSA + " = ");
            _sb.Append("EP." + TableEncabezadoPedido._NO_TRANSA + " ");
            _sb.Append("AND ");
            _sb.Append("EP." + TableEncabezadoPedido._NO_CLIENTE + " = ");
            _sb.Append("C." + TableCliente._NO_CLIENTE + " ");
            _sb.Append("AND ");
            _sb.Append("DP." + TableDetallePedido._ARTI_DES + " = ");
            _sb.Append("P." + TableProducto._CODPRODUCTO + " ");
            _sb.Append("AND ");
            _sb.Append("DP." + TableDetallePedido._ARTI_DES + " = ");
            _sb.Append("I." + TableInventario._CODPRODUCTO + " ");
            _sb.Append("ORDER BY ");
            _sb.Append("DP."+TableDetallePedido._ARTI_DES + ", ");
            _sb.Append("C."+TableCliente._NO_CLIENTE + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            ptrvInventarioPedidos.ItemsSource = new ObservableCollection<CollapsableItem>();

            cargarTreeViewInventarioPedidos(_dt, ptrvInventarioPedidos);

        }

        //private void cargarTreeViewInventarioPedidos(DataTable pdt, TreeView ptreeView)
        private void cargarTreeViewInventarioPedidos(DataTable pdt, ListView ptreeView)
        {
            var Source = ptreeView.ItemsSource as ObservableCollection<CollapsableItem>;

            foreach (DataRow _fila in pdt.Rows)
            {
                string _raizNodo = _fila[TableDetallePedido._ARTI_DES].ToString();

                string _disponible = _fila[TableInventario._DISPONIBLE].ToString();

                decimal _cantDisponible = FormatUtil.convertStringToDecimal(_disponible);

                _disponible = FormatUtil.applyCurrencyFormat(_cantDisponible);

                string _producto = _fila[TableProducto._DESCPRODUCTO].ToString();

                _raizNodo = _raizNodo + " / " + _disponible + " / " + _producto;

                Util _util = new Util();

                _util.evaluateAddNode(ptreeView,_raizNodo);
            }            

            //foreach (TreeNode _tn in ptreeView.Nodes)
            foreach (var _tn in Source)
            {
                foreach (DataRow _fila in pdt.Rows)
                {
                    string _raizNodo = _fila[TableDetallePedido._ARTI_DES].ToString();

                    string _disponible = _fila[TableInventario._DISPONIBLE].ToString();

                    decimal _cantDisponible = FormatUtil.convertStringToDecimal(_disponible);

                    _disponible = FormatUtil.applyCurrencyFormat(_cantDisponible);

                    string _producto = _fila[TableProducto._DESCPRODUCTO].ToString();

                    _raizNodo = _raizNodo + " / " + _disponible + " / " + _producto;

                    //if (_tn.Text.ToString() == _raizNodo)
                    if(_tn.Encabezado.ToString() == _raizNodo)
                    {
                        string _despachado = _fila[TableDetallePedido._CAN_DES].ToString();

                        decimal _cantDespachado = FormatUtil.convertStringToDecimal(_despachado);

                        _despachado = FormatUtil.applyCurrencyFormat(_cantDespachado);

                        string _codCliente = _fila[TableCliente._NO_CLIENTE].ToString();

                        string _nombreCliente = _fila[TableCliente._NOMBRE].ToString();

                        _tn.Detalle = _tn.Detalle+"-> " + _despachado + " / " + _codCliente + " - " + _nombreCliente + Environment.NewLine;
                    }
                }
            }
        }

        internal void consolidarInventarioAutomaticamente()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._inventario + " ");
            _sb.Append("SET ");
            _sb.Append(TableInventario._TOMA_FISICA + " = ");
            _sb.Append(TableInventario._DISPONIBLE);

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.insertRecord(_sb);
        }

        internal DateTime buscarFechaCreacion()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableInventario._FECHACREA + " ) " + TableInventario._FECHACREA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._inventario + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            string _fechaCreacion = MultiGeneric.readStringText(_sb);

            return FormatUtil.covertStringToDateTimeWithoutTime(_fechaCreacion);
        }

        internal DataTable buscarFechaTomaFisicaActualizarEnviado()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("STRFTIME('%d/%m/%Y'," + TableInventario._FECHATOMA + " ) " + TableInventario._FECHATOMA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._inventario + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.uploadDataTable(_sb);
        }

        internal DateTime buscarFechaTomaFisicaConsolida()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableInventario._FECHATOMACONSOLIDA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._inventario + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            string _fechaTomaConsolida = MultiGeneric.readStringText(_sb);

            return FormatUtil.convertStringToDateTimeWithTime(_fechaTomaConsolida);
        }

        internal string buscarEstadoConsolidado()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableInventario._CONSOLIDADO + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._inventario + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readStringText(_sb);
        }
    }
}
