using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita
{
    internal class LogicaVisitaVerificaciones
    {
        private vistaVisita view = null;

        private string v_nomTipoTransaccion = null;

        internal LogicaVisitaVerificaciones(vistaVisita pview)
        {
            view = pview;
            v_nomTipoTransaccion = view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString();
        }

        internal async Task<bool> VerificarCreditoDisponible()
        {
            bool _excedido = false;

            decimal _montoTransaccion = Numeric._zeroDecimalInitialize;

            LogicaVisitaCalculos _logicaCalculos = new LogicaVisitaCalculos(view);

            _montoTransaccion = _logicaCalculos.calcularMontoTransaccion();

            decimal _montoExcedido = view.controlador.v_objCliente.creditoDisponible() - _montoTransaccion;

            if (_montoExcedido < 0)
            {
                _montoExcedido = _montoExcedido * -1;

                _excedido = true;

                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention("El monto se encuentra excedido por: "
                    + Environment.NewLine
                    + Environment.NewLine
                    + FormatUtil.applyCurrencyFormat(_montoExcedido)
                    + Environment.NewLine
                    + Environment.NewLine
                    + "* Realice el ajuste manual"
                    );
            }

            return _excedido;
        }

        internal async Task verificarLineasDetalleCambioTipoTransaccion()
        {
            LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(view);

            Producto _objProducto = new Producto();
            pnlTransacciones_ltvProductos _lvi = new pnlTransacciones_ltvProductos();

            var Source = view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

            if (Source != null)
            {
                int totalitems = (Source.Count);

                for (int i = Numeric._zeroInteger; i < totalitems ; i++)
                {
                    _lvi = Source[i];

                    _objProducto = _logica.levantarProductoSeleccionado(_lvi, true);

                    #region Vísceras
                    //hay que validar unicamente a los productos que son visceras
                    Logica_ManagerProducto _managerProducto = new Logica_ManagerProducto();

                    if (v_nomTipoTransaccion.Equals(ROLTransactions._facturaContadoNombre) ||
                        v_nomTipoTransaccion.Equals(ROLTransactions._facturaCreditoNombre))
                    {
                        //Verificar que el producto sea viscera y que no haya sido definido ya como viscera
                        if (_managerProducto.buscarEsViscera(_objProducto.v_codProducto) &&
                            !_objProducto.EsViscera)
                        {
                            //Debe crearse un metodo que identifique que indicador de los
                            //detalles reses se apega
                            FacturacionVisceras facturacionVisceras =
                                new FacturacionVisceras(
                                                        view,
                                                        view.controlador.v_objCliente);

                            pnlFacturacionVisceras visceraSeleccionada = new pnlFacturacionVisceras();

                            bool continuar = true;

                            do
                            {

                                if (facturacionVisceras.CodProductosVisita.Count > 0)
                                {
                                    visceraSeleccionada =
                                                await facturacionVisceras.RecorrerAsignacionesDR(
                                                    facturacionVisceras.CodProductosVisita.First());
                                }
                                else
                                {
                                    visceraSeleccionada = null;
                                }

                                if (facturacionVisceras.CodProductosVisita.Count == 0 ||
                                        facturacionVisceras.detallesReses.Count > 0)
                                {
                                    continuar = false;
                                }

                            } while (continuar);

                            if (visceraSeleccionada != null)
                            {
                                _objProducto.EsViscera = true;

                                if (!string.IsNullOrEmpty(visceraSeleccionada.TIPOVICERAS))
                                {
                                    _objProducto.TipoPorcion = visceraSeleccionada.TIPOVICERAS;
                                    _objProducto.ConsecutivoDReses = visceraSeleccionada.DETALLERES._vc_consecutivo;
                                    _objProducto.v_precioConsultado = false;
                                    _objProducto.v_porcentajeDescuentoConsultado = false;
                                    _objProducto.v_porcentajeDescuentoGeneralConsultado = false;
                                    _objProducto.v_exentoConsultado = false;
                                    _objProducto.v_unidadConsultado = false;
                                    _objProducto.v_descripcionConsultado = false;
                                    _objProducto.v_unidadConsultado = false;
                                }
                            }

                        }
                    }
                    #endregion

                    await _logica.verificarPrecio(_objProducto, i, true);

                    if (totalitems != Source.Count)
                    {
                        i--;
                        totalitems = Source.Count;
                    }

                }
            }

            ValidateHH _validateHH = new ValidateHH();

            if (!_validateHH.emptyListView<pnlTransacciones_ltvProductos>(view.FindByName<ListView>("pnlTransacciones_ltvProductos")))
            {
                _logica.actualizarProductoInventario();
            }
        }
    }
}
