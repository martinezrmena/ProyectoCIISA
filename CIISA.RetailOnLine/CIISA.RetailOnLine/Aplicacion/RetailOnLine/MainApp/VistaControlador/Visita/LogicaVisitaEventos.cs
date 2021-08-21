using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using Xamarin.Forms;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using System.Collections.ObjectModel;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita
{
    public class LogicaVisitaEventos
    {
        private vistaVisita view = null;

        internal LogicaVisitaEventos(vistaVisita pview)
        {
            view = pview;
        }

        internal async Task pnlTransacciones_ltvProductos_ItemActivate()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlTransacciones_ltvProductos")))
            {
                pnlTransacciones_ltvProductos _lvi = view.FindByName<ListView>("pnlTransacciones_ltvProductos").SelectedItem as pnlTransacciones_ltvProductos;

                LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(view);

                Producto _objProducto = _logica.levantarProductoSeleccionado(_lvi, false);

                List<Producto> _listaProductoComprometido = _logica.productoComprometidoTransaccion();

                ShowSROL _show = new ShowSROL();

                _show.MostrarPantallaVisitaCantidad(_objProducto, _listaProductoComprometido,this);
            }
        }

        public async Task pnlTransacciones_ltvProductos_ItemActivateParte2(bool _guardar, Producto _objProducto)
        {
            pnlTransacciones_ltvProductos _lvi = view.FindByName<ListView>("pnlTransacciones_ltvProductos").SelectedItem as pnlTransacciones_ltvProductos;
            LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(view);

            if (_guardar)
            {
                var Source = view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

                await _logica.verificarPrecio(
                    _objProducto,
                    Source.IndexOf(_lvi),
                    true
                    );

                LogicaVisitaActualizar _logicaActualizar = new LogicaVisitaActualizar(view);

                _logicaActualizar.actualizarTotal();
            }

            _logica.actualizarProductoInventario();
        }

        internal async Task pnlTransacciones_ltvProductos_ColumnClick()
        {
            if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem != null)
            {
                if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._recaudacionNombre)
                || view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._reciboDineroNombre))
                {
                    LogMessageAttention _logMessageAttention = new LogMessageAttention();
                    await _logMessageAttention.generalAttention("No puede agregar líneas a una "
                        + view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString());
                }
                else
                {
                    List<Producto> _listaProductoComprometido = null;

                    LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(view);

                    _listaProductoComprometido = _logica.productoComprometidoTransaccion();

                    string _codigoUltimoProducto = string.Empty;
                    List<string> CodigosProductos = new List<string>();

                    var Source = view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

                    foreach (pnlTransacciones_ltvProductos _lvi in Source)
                    {
                        _codigoUltimoProducto = _lvi._pt_codigo;
                        CodigosProductos.Add(_lvi._pt_codigo);
                    }

                    ShowSROL _showProducto = new ShowSROL();

                    _showProducto.mostrarPantallaProductoVisita(
                                                            view,
                                                            PantallasSistema._pantallaVisita,
                                                            view.controlador.v_objCliente,
                                                            view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString(),
                                                            true,
                                                            _listaProductoComprometido,
                                                            _codigoUltimoProducto,
                                                            this,
                                                            CodigosProductos
                                                            );
                }
            }
            
        }

        internal async Task pnlTransacciones_ltvProductos_ColumnClickParte2(Producto _objProducto)
        {
            if (_objProducto.v_codProducto != string.Empty)
            {
                bool _devolucion =
                   view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._devolucionNombre);

                bool _regalia =
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._regaliaNombre);

                bool _cancelar = false;

                if (_devolucion)
                {
                    ShowSROL _showVisitaDevolucion = new ShowSROL();

                    _showVisitaDevolucion.mostrarPantallaVisitaDevolucion(_objProducto, false,this);
                }

                if (_regalia)
                {
                    ShowSROL _showVisitaRegalia = new ShowSROL();

                    _showVisitaRegalia.MostrarPantallaVisitaRegalia(_objProducto,this);

                }

                if(!_devolucion && !_regalia)
                {
                    await pnlTransacciones_ltvProductos_ColumnClickParte3(_objProducto, _cancelar);
                }
            }
        }

        internal async Task pnlTransacciones_ltvProductos_ColumnClickParte3(Producto _objProducto,bool _cancelar)
        {
            LogicaVisitaLtvProducto _logicaVisitaLtvProducto = new LogicaVisitaLtvProducto(view);

            if (!_cancelar)
            {
                var Source = view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

                await _logicaVisitaLtvProducto.verificarPrecio(
                        _objProducto,
                        Source.Count - 1,
                        false
                        );
            }

            _logicaVisitaLtvProducto.actualizarProductoInventario();

            LogicaVisitaRender _logicaRender = new LogicaVisitaRender(view);

            _logicaRender.renderMenu(false);

            LogicaVisitaActualizar _logicaActualizar = new LogicaVisitaActualizar(view);

            _logicaActualizar.actualizarColumnas();
            _logicaActualizar.actualizarTotal();
        }

        internal void vistaVisita_Closing(bool renderList, bool CambiarNumPedidoDR = false, bool pantallaProducto = false, List<pnlTransacciones_ltvDetalleReses> ListaDetallesResesProductos = null)
        {
            ValidateHH _validateHH = new ValidateHH();
            bool validar = view.controlador.TipoAgente.Equals(Agent._carniceroSigla) ? true : false;
            bool _vacio = _validateHH.emptyListView<pnlTransacciones_ltvProductos>(view.FindByName<ListView>("pnlTransacciones_ltvProductos"));
            LogicaVisitaPedido _logica = new LogicaVisitaPedido(view);
            LogicaVisitaComboBox _logicaVisitaComboBox = new LogicaVisitaComboBox(view);
            LogicaVisitaPedido _logicaVisitaPedido = new LogicaVisitaPedido(view);
            string Es_Factura = _logicaVisitaComboBox.getTipoDocumento().Contains(ROLTransactions._facturaNombre) ? SQL._Si : SQL._No;

            if (!_vacio && !view.controlador.v_DevolucionFactura)
            {
                bool EsPedidoBackOffice = view.controlador.TipoAgente.Equals(Agent._carniceroSigla) && Es_Factura.Equals(SQL._Si) && !view.controlador.v_PedidoManual ? _logicaVisitaPedido.ExistePedidoBackOffice() : false;
                //Si es carnicero, hay pedidos backOffice y es factura entonces no se debe poder guardar el pedido de respaldo
                if (!EsPedidoBackOffice)
                {
                    CambiarNumPedidoDR = true;
                    //CambiarNumPedidoDR = Es_Factura.Equals(SQL._Si) ? true : _logicaVisitaPedido.ExistePedidoBackOffice() ? false : true;
                    _logica.guardarPedido(renderList, validar, CambiarNumPedidoDR, pantallaProducto, ListaDetallesResesProductos);
                }
            }
        }
    }
}
