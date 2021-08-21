using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita
{
    internal class LogicaVisitaLtvProducto
    {
        private vistaVisita view = null;

        internal LogicaVisitaLtvProducto(vistaVisita pview)
        {
            view = pview;
        }

        internal Producto levantarProductoSeleccionado(pnlTransacciones_ltvProductos plistViewItem, bool pdescuento)
        {
            Producto _objProducto = new Producto();

            _objProducto.v_codProducto = plistViewItem._pt_codigo;

            _objProducto.CodCliente = view.controlador.v_objCliente.v_no_cliente;

            _objProducto.v_cantTransaccion = FormatUtil.convertStringToDecimal(plistViewItem._pt_cantidad);

            _objProducto.v_especificacionOV = plistViewItem._pt_comentario;

            _objProducto.v_motivo = plistViewItem._pt_motivo;

            _objProducto.v_estado = plistViewItem._pt_estado;

            _objProducto.v_embalaje = FormatUtil.convertStringToDecimal(plistViewItem._pt_embalaje);

            _objProducto.v_precio = FormatUtil.convertStringToDecimal(plistViewItem._pt_precio);

            _objProducto.v_precioConsultado = true;

            if (pdescuento)
            {
                _objProducto.v_porcentajeDescuento = FormatUtil.convertStringToDecimal(plistViewItem._pt_porcDescuento);

                _objProducto.v_porcentajeDescuentoConsultado = true;

                //DGPC
                _objProducto.v_porcentajeDescuentoGeneral = Numeric._zeroDecimalInitialize;

                _objProducto.v_porcentajeDescuentoGeneralConsultado = true;

                _objProducto.v_MontoDescuento = FormatUtil.convertStringToDecimal(plistViewItem._pt_montDescuento);

                _objProducto.ValoresListView = true;
                //
            }

            _objProducto.v_descripcion = plistViewItem._pt_descripcion;
            _objProducto.v_descripcionConsultado = true;

            _objProducto.v_exento = plistViewItem._pt_exento.Equals(string.Empty);
            _objProducto.v_exentoConsultado = true;

            //Visceras
            if (!string.IsNullOrEmpty(plistViewItem._pt_viceras))
            {
                _objProducto.EsViscera = true;
                _objProducto.TipoPorcion = plistViewItem._pt_tipoporcion;
                _objProducto.ConsecutivoDReses = plistViewItem.consecutivo_DReses;
            }

            return _objProducto;
        }

        private pnlTransacciones_ltvProductos establecerValoresListViewItemProducto(string pclassInvoke,string pmethodInvoke,Producto pobjProducto)
        {
            pnlTransacciones_ltvProductos _lvi = new pnlTransacciones_ltvProductos();

            _lvi._pt_codigo = pobjProducto.v_codProducto;

            pobjProducto.CodCliente = view.controlador.v_objCliente.v_no_cliente;

            if (pobjProducto.v_cantTransaccion <= 0)
            {
                decimal _cantidadErronea = pobjProducto.v_cantTransaccion;

                pobjProducto.v_cantTransaccion = Numeric._zeroDecimalInitialize;
            }

            _lvi._pt_cantidad = FormatUtil.applyCurrencyFormat(pobjProducto.v_cantTransaccion);

            if (pobjProducto.v_cantTransaccion <= 0)
            {
                _lvi.ItemTextColor = Color.Red;
            }

            _lvi._pt_descripcion = pobjProducto.descripcion();

            _lvi._pt_precio = FormatUtil.applyCurrencyFormat(
                                    pobjProducto.precio(view.controlador.v_objCliente)
                                    );

            _lvi._pt_exento = pobjProducto.exento(view.controlador.v_objCliente);

            _lvi.MontoImpuestoUnaUnidad = FormatUtil.applyCurrencyFormat(
                                    pobjProducto.calcularMontoImpuestoUnaUnidad(view.controlador.v_objCliente)
                                    );

            decimal _montoDescuento = Numeric._zeroDecimalInitialize;

            Logica_ManagerTipoTransaccion _managerTipoTransaccion = new Logica_ManagerTipoTransaccion();

            view.controlador.v_objCliente.v_objTransaccion.v_objTipoDocumento.SetSigla(
                _managerTipoTransaccion.obtenerCodigoTipoTransaccion(view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString()));

            _montoDescuento = pobjProducto.calcularMontoDescuento(view.controlador.v_objCliente);

            _lvi._montoDescuento = FormatUtil.applyCurrencyFormat(_montoDescuento);

            //Calculos descuentos por porcentaje y por monto
            decimal _porcentajeDescuento = pobjProducto.porcentajeGlobalDescuento(view.controlador.v_objCliente);

            decimal _mntDescuento = pobjProducto.montoGlobalDescuento(view.controlador.v_objCliente);

            _lvi._pt_porcDescuento = FormatUtil.applyCurrencyFormat(_porcentajeDescuento);

            _lvi._pt_montDescuento = FormatUtil.applyCurrencyFormat(_mntDescuento);
            //

            _lvi._pt_comentario = pobjProducto.v_especificacionOV;

            _lvi._pt_motivo = pobjProducto.v_motivo;

            _lvi._pt_estado = pobjProducto.v_estado;

            _lvi.inventarioDisponible = FormatUtil.applyCurrencyFormat(pobjProducto.inventarioDisponible());

            _lvi._pt_embalaje = FormatUtil.applyCurrencyFormat(pobjProducto.v_embalaje);

            //Viscera
            if (pobjProducto.EsViscera)
            {
                _lvi._pt_viceras = Simbol._asterisk;
            }

            _lvi._pt_tipoporcion = pobjProducto.TipoPorcion;

            _lvi.consecutivo_DReses = pobjProducto.ConsecutivoDReses;

            if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Contains("Factura"))
            {
                _lvi.Es_Factura = SQL._Si;
            }
            else
            {
                _lvi.Es_Factura = SQL._No;
            }

            return _lvi;
        }

        internal void agregarProductoALaListaTransaccion(Producto pobjProducto,int pindice,bool pmodificar)
        {
            pnlTransacciones_ltvProductos _lvi = establecerValoresListViewItemProducto(
                                    GetType().Name,
                                    "agregarProductoALaListaTransaccion",
                                    pobjProducto
                                    );

            var Source = view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

            if (pmodificar)
            {
                //view.pnlTransacciones_ltvProductos.Items.RemoveAt(pindice);
                Source.RemoveAt(pindice);

                //view.pnlTransacciones_ltvProductos.Items.Insert(pindice, _lvi);
                Source.Insert(pindice, _lvi);
            }
            else
            {
                //view.pnlTransacciones_ltvProductos.Items.Add(_lvi);
                Source.Add(_lvi);
            }
        }

        internal List<Producto> productoComprometidoTransaccion()
        {
            List<Producto> _listaProducto = new List<Producto>();

            var Source = view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

            foreach (var _lvi in Source)
            {
                LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(view);

                Producto _objProducto = _logica.levantarProductoSeleccionado(_lvi, true);

                if (_listaProducto.Count == 0)
                {
                    _listaProducto.Add(_objProducto);
                }
                else
                {
                    bool _existe = false;

                    foreach (Producto _objP in _listaProducto)
                    {
                        if (_objP.v_codProducto.Equals(_objProducto.v_codProducto))
                        {
                            _existe = true;
                            _objP.v_cantTransaccion += _objProducto.v_cantTransaccion;
                        }
                    }

                    if (!_existe)
                    {
                        _listaProducto.Add(_objProducto);
                    }
                }
            }

            return _listaProducto;
        }

        private decimal productoComprometidoTransaccionActual(string pcodProducto, int pnumLinea)
        {
            decimal _cantTransaccionAcumulado = Numeric._zeroDecimalInitialize;
            decimal _cantTransaccionUltima = Numeric._zeroDecimalInitialize;
            pnlTransacciones_ltvProductos _lvi = new pnlTransacciones_ltvProductos();
            Producto _objProducto = new Producto();

            LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(view);

            var Source = view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

            for (int i = Numeric._zeroInteger; i <= pnumLinea; i++)
            {
                _lvi = Source[i];

                _objProducto = _logica.levantarProductoSeleccionado(_lvi, true);

                if (pcodProducto.Equals(_objProducto.v_codProducto))
                {
                    _cantTransaccionAcumulado += _objProducto.v_cantTransaccion;
                    _cantTransaccionUltima = _objProducto.v_cantTransaccion;
                }
            }

            _cantTransaccionAcumulado -= _cantTransaccionUltima;

            if (_cantTransaccionAcumulado < Numeric._zeroInteger)
            {
                _cantTransaccionAcumulado = _cantTransaccionAcumulado * -1;
            }

            return _cantTransaccionAcumulado;
        }

        private async Task verificarRespetaExistenciaEnInventario(Producto pobjProducto, int pindice, bool pmodificar)
        {
            decimal _existenciaEnInventario = pobjProducto.inventarioDisponible();

            LogicaCarniceriaActualizar logicaCarniceriaActualizar = new LogicaCarniceriaActualizar(view);

            Util _util = new Util();

            List<Producto> _listaProducto = productoComprometidoTransaccion();

            decimal _productoComprometidoActual = productoComprometidoTransaccionActual(
                                                        pobjProducto.v_codProducto,
                                                        pindice
                                                        );

            decimal _existenciaReal = Numeric._zeroDecimalInitialize;

            if (_existenciaEnInventario >= _productoComprometidoActual)
            {
                _existenciaReal = _existenciaEnInventario - _productoComprometidoActual;
            }
            else
            {
                if (_existenciaEnInventario > _productoComprometidoActual)
                {
                    _existenciaReal = _existenciaEnInventario - _productoComprometidoActual;
                }
                else
                {
                    _existenciaReal = _existenciaEnInventario;
                }
            }

            if (_existenciaReal > 0)
            {
                if (_existenciaReal >= pobjProducto.v_cantTransaccion)
                {
                    if (await logicaCarniceriaActualizar.actualizarFilasDetallesReses(pobjProducto))
                    {
                        agregarProductoALaListaTransaccion(
                            pobjProducto,
                            pindice,
                            pmodificar
                            );
                    }
                    else
                    {
                        if (_util.VerificaExisteDR(
                        view.FindByName<ListView>("pnlTransacciones_ltvProductos"),
                        pindice,
                        pobjProducto))
                        {
                            _util.deleteElementListView<pnlTransacciones_ltvProductos>(
                                view.FindByName<ListView>("pnlTransacciones_ltvProductos"),
                                pindice
                                );
                        }
                    }

                }
                else
                {
                    LogMessageAttention _logMessageAttention = new LogMessageAttention();
                    await _logMessageAttention.generalAttention("Se ajustó la línea:"
                        + Environment.NewLine
                        + Environment.NewLine
                        + pobjProducto.v_codProducto
                        + Simbol._hyphenWithSpaces
                        + pobjProducto.descripcion()
                        + Environment.NewLine
                        + Environment.NewLine
                        + "por falta de inventario,"
                        + Environment.NewLine
                        + Environment.NewLine
                        + "de "
                        + pobjProducto.v_cantTransaccion
                        + " a "
                        + _existenciaReal
                        + " "
                        +
                        pobjProducto.unidad()
                        );

                    pobjProducto.v_cantTransaccion = _existenciaReal;

                    pobjProducto.v_descripcionConsultado = false;

                    if (await logicaCarniceriaActualizar.actualizarFilasDetallesReses(pobjProducto))
                    {
                        agregarProductoALaListaTransaccion(
                            pobjProducto,
                            pindice,
                            pmodificar
                            );
                    }
                    else
                    {
                        if (_util.VerificaExisteDR(
                        view.FindByName<ListView>("pnlTransacciones_ltvProductos"),
                        pindice,
                        pobjProducto))
                        {
                            _util.deleteElementListView<pnlTransacciones_ltvProductos>(
                                view.FindByName<ListView>("pnlTransacciones_ltvProductos"),
                                pindice
                                );
                        }
                    }
                }
            }
            else
            {
                if (_util.VerificaExiste(
                        view.FindByName<ListView>("pnlTransacciones_ltvProductos"),
                        pindice, 
                        pobjProducto))
                {
                    _util.deleteElementListView<pnlTransacciones_ltvProductos>(
                        view.FindByName<ListView>("pnlTransacciones_ltvProductos"),
                        pindice
                        );
                }

                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention("Se eliminó la línea:"
                    + Environment.NewLine
                    + Environment.NewLine
                    + pobjProducto.v_codProducto
                    + Simbol._hyphenWithSpaces
                    + pobjProducto.descripcion()
                    + Environment.NewLine
                    + Environment.NewLine
                    + "por falta de inventario"
                    );
            }
        }

        private async Task verificarRespetaLimiteCredito(Producto pobjProducto,int pindice,bool pmodificar)
        {

            if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._ordenVentaNombre))
            {
                agregarProductoALaListaTransaccion(
                    pobjProducto,
                    pindice,
                    pmodificar
                    );
            }

            if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._recaudacionNombre))
            {
                agregarProductoALaListaTransaccion(
                    pobjProducto,
                    pindice,
                    pmodificar
                    );
            }

            if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._facturaContadoNombre))
            {
                await verificarRespetaExistenciaEnInventario(
                    pobjProducto,
                    pindice,
                    pmodificar
                    );
            }

            if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._facturaCreditoNombre))
            {
                await verificarRespetaExistenciaEnInventario(
                    pobjProducto,
                    pindice,
                    pmodificar
                    );
            }

            if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._cotizacionNombre))
            {
                agregarProductoALaListaTransaccion(
                    pobjProducto,
                    pindice,
                    pmodificar
                    );
            }

            if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._devolucionNombre))
            {
                agregarProductoALaListaTransaccion(
                    pobjProducto,
                    pindice,
                    pmodificar
                    );
            }

            if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._regaliaNombre))
            {
                await verificarRespetaExistenciaEnInventario(
                    pobjProducto,
                    pindice,
                    pmodificar
                    );
            }
        }

        internal async Task verificarPrecio(Producto pobjProducto,int pindice,bool pmodificar)
        {

            decimal _precio = pobjProducto.precio(view.controlador.v_objCliente);

            if (_precio > 0)
            {
                await verificarRespetaLimiteCredito(
                    pobjProducto,
                    pindice,
                    pmodificar
                    );
            }
            else
            {
                if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._ordenVentaNombre))
                {
                    agregarProductoALaListaTransaccion(
                        pobjProducto,
                        pindice,
                        pmodificar
                        );

                    if (!pobjProducto.TipoPorcion.Equals(TypeSlice.PorcionCompletaSigla))
                    {
                        LogMessageAttention _logMessageAttention = new LogMessageAttention();
                        await _logMessageAttention.generalAttention(
                            "No hay precio para el producto"
                            + Environment.NewLine
                            + Environment.NewLine
                            + pobjProducto.v_codProducto
                            + Simbol._hyphenWithSpaces
                            + pobjProducto.descripcion());
                    }

                }
                else
                {
                    //verificar si es carincero y si el producto es viscera completa
                    //en cuyo caso es valido que el precio sea 0
                    if (pobjProducto.TipoPorcion.Equals(TypeSlice.PorcionCompletaSigla))
                    {
                        await verificarRespetaLimiteCredito(
                            pobjProducto,
                            pindice,
                            pmodificar
                            );
                    }
                    else {

                        LogMessageAttention _logMessageAttention = new LogMessageAttention();
                        await _logMessageAttention.generalAttention(
                            "No hay precio para agregar el producto"
                            + Environment.NewLine
                            + Environment.NewLine
                            + pobjProducto.v_codProducto
                            + Simbol._hyphenWithSpaces
                            + pobjProducto.descripcion()
                            + Environment.NewLine
                            + Environment.NewLine
                            + "* Solicite el precio al Departamento de Liquidaciones.");
                    }
                   
                }
            }

        }

        internal void actualizarProductoInventario()
        {
            var Source = view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

            if (Source != null)
            {
                if (Source.Count > 0)
                {
                    LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(view);

                    List<Producto> _listaProductos = _logica.productoComprometidoTransaccion();

                    foreach (var _lvi in Source)
                    {
                        Producto _objProducto = levantarProductoSeleccionado(_lvi, true);

                        decimal _cantProductoDisponible = _objProducto.inventarioDisponible();

                        UtilLogica _util = new UtilLogica();

                        decimal _cantProductoComprometido = _util.obtenerCantidadProductoComprometido(
                                                        _objProducto.v_codProducto,
                                                        _listaProductos
                                                        );

                        if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._ordenVentaNombre))
                        {
                            _lvi.inventarioDisponible = FormatUtil.applyCurrencyFormat(_cantProductoDisponible);
                        }

                        if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._facturaContadoNombre))
                        {
                            _lvi.inventarioDisponible = FormatUtil.applyCurrencyFormat(_cantProductoDisponible - _cantProductoComprometido);
                        }

                        if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._facturaCreditoNombre))
                        {
                            _lvi.inventarioDisponible = FormatUtil.applyCurrencyFormat(_cantProductoDisponible - _cantProductoComprometido);
                        }

                        if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._cotizacionNombre))
                        {
                            _lvi.inventarioDisponible = FormatUtil.applyCurrencyFormat(_cantProductoDisponible);
                        }

                        if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._devolucionNombre))
                        {
                            if (_objProducto.v_estado.Equals(Pedido._devolucionBuena))
                            {
                                _lvi.inventarioDisponible = FormatUtil.applyCurrencyFormat(_cantProductoDisponible + _cantProductoComprometido);
                            }
                            else
                            {
                                _lvi.inventarioDisponible = FormatUtil.applyCurrencyFormat(_cantProductoDisponible);
                            }
                        }

                        if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._regaliaNombre))
                        {
                            _lvi.inventarioDisponible = FormatUtil.applyCurrencyFormat(_cantProductoDisponible - _cantProductoComprometido);
                        }

                    }
                }
            }

        }
    }
}
