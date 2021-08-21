using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita
{
    internal class LogicaVisitaCalculos
    {
        private vistaVisita view = null;

        internal LogicaVisitaCalculos(vistaVisita pview)
        {
            view = pview;
        }

        internal decimal calcularMontoTransaccion()
        {
            decimal _totalTransaccion = Numeric._zeroDecimalInitialize;

            ValidateHH _validateHH = new ValidateHH();

            if (!_validateHH.emptyListView<pnlTransacciones_ltvProductos>(view.FindByName<ListView>("pnlTransacciones_ltvProductos")))
            {
                var Source = view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

                if (Source != null)
                {
                    foreach (pnlTransacciones_ltvProductos _lvi in Source)
                    {
                        LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(view);

                        Producto _objProducto = _logica.levantarProductoSeleccionado(_lvi, true);

                        decimal _totalLinea = _objProducto.calcularMontoPrecioPorCantidadDeProducto(view.controlador.v_objCliente);

                        decimal _total_imp = _objProducto.calcularMontoImpuestoPorCantidadDeProducto(view.controlador.v_objCliente);

                        _totalLinea += _total_imp;

                        _totalTransaccion = _totalTransaccion + _totalLinea;
                    }
                }
            }

            return _totalTransaccion;
        }

        internal void calcularMontoTotal_Impuesto_DescuentoTransaccion(TransaccionEncabezado pobjTransaccionEncabezado)
        {
            ValidateHH _validateHH = new ValidateHH();

            if (!_validateHH.emptyListView<pnlTransacciones_ltvProductos>(view.FindByName<ListView>("pnlTransacciones_ltvProductos")))
            {
                decimal _totalTransaccion = Numeric._zeroDecimalInitialize;

                decimal _totalDescuento = Numeric._zeroDecimalInitialize;

                decimal _totalImpuesto = Numeric._zeroDecimalInitialize;

                var Source = view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

                foreach (var _lvi in Source)
                {
                    LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(view);

                    Producto _objProducto = _logica.levantarProductoSeleccionado(_lvi, true);

                    _objProducto.v_porcentajeDescuentoConsultado = false;
                    //DGPC
                    _objProducto.v_porcentajeDescuentoGeneralConsultado = false;
                    //
                    decimal _descuento = _objProducto.calcularMontoDescuentoPorCantidaDeProducto(view.controlador.v_objCliente);
                    _totalDescuento = _totalDescuento + _descuento;

                    decimal _totalLinea = _objProducto.calcularMontoLinea(view.controlador.v_objCliente);
                    _totalTransaccion = _totalTransaccion + _totalLinea;

                    decimal _impuestoLinea = _objProducto.calcularMontoImpuestoPorCantidadDeProducto(view.controlador.v_objCliente);
                    _totalImpuesto = _totalImpuesto + _impuestoLinea;
                }

                pobjTransaccionEncabezado.v_total = _totalTransaccion;
                pobjTransaccionEncabezado.v_totalImp = _totalImpuesto;

                pobjTransaccionEncabezado.v_montoDescuento = _totalDescuento;
            }

        }

        internal async Task calcularMontoLinea()
        {
            var _lvi = view.FindByName<ListView>("pnlTransacciones_ltvProductos").SelectedItem as pnlTransacciones_ltvProductos;

            Producto _objProducto = null;

            LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(view);

            _objProducto = _logica.levantarProductoSeleccionado(_lvi, true);

            StringBuilder _sb = new StringBuilder();

            _sb.Append(Simbol._bulletPoint);
            _sb.Append("Precio: ");
            _sb.Append(FormatUtil.applyCurrencyFormat(
                            _objProducto.precio(view.controlador.v_objCliente)
                            ));
            _sb.Append(Environment.NewLine);
            _sb.Append(Simbol._bulletPoint);
            _sb.Append("Impuesto: ");
            _sb.Append(FormatUtil.applyCurrencyFormat(
                            _objProducto.calcularMontoImpuestoUnaUnidad(view.controlador.v_objCliente)
                            ));
            _sb.Append(Environment.NewLine);
            _sb.Append(Simbol._bulletPoint);
            _sb.Append("Cantidad: ");
            _sb.Append(FormatUtil.applyCurrencyFormat(
                            _objProducto.v_cantTransaccion
                            ));
            _sb.Append(Environment.NewLine);
            _sb.Append(Environment.NewLine);
            _sb.Append(Simbol._bulletPoint);
            _sb.Append("% Descuento: ");
            _sb.Append(FormatUtil.applyCurrencyFormat(
                            _objProducto.porcentajeDescuento(view.controlador.v_objCliente)
                            ));
            _sb.Append("%");
            _sb.Append(Environment.NewLine);
            _sb.Append(Simbol._bulletPoint);
            _sb.Append("Descuento: ");
            _sb.Append(FormatUtil.applyCurrencyFormat(
                            _objProducto.calcularMontoDescuento(view.controlador.v_objCliente)
                            ));
            _sb.Append(Environment.NewLine);
            _sb.Append(Environment.NewLine);
            _sb.Append(Simbol._bulletPoint);
            _sb.Append("Inicio: ");
            _sb.Append(_objProducto.fechaInicioDescuento(view.controlador.v_objCliente));
            _sb.Append(Environment.NewLine);
            _sb.Append(Simbol._bulletPoint);
            _sb.Append("Vence: ");
            _sb.Append(_objProducto.fechaVenceDescuento(view.controlador.v_objCliente));
            _sb.Append(Environment.NewLine);
            _sb.Append(Environment.NewLine);
            _sb.Append(Simbol._bulletPoint);
            _sb.Append("TOTAL: ");
            _sb.Append(FormatUtil.applyCurrencyFormat(
                            _objProducto.calcularMontoLinea(view.controlador.v_objCliente)
                            ));
            _sb.Append(Environment.NewLine);
            _sb.Append(Environment.NewLine);
            _sb.Append("----");
            _sb.Append(Environment.NewLine);
            _sb.Append(Environment.NewLine);
            _sb.Append("Fecha y Hora (Máquina): ");
            _sb.Append(Environment.NewLine);
            _sb.Append(VarTime.getDateCR());
            _sb.Append(Simbol._hyphenWithSpaces);
            _sb.Append(VarTime.getCompleteDateTime());

            LogMessageAttention _logMessageAttention = new LogMessageAttention();
            await _logMessageAttention.generalAttention(_sb.ToString());
        }

        internal decimal calcularMontoTransaccionMenosUnaLinea(int pline)
        {
            decimal _totalTransaccion = Numeric._zeroDecimalInitialize;

            ValidateHH _validateHH = new ValidateHH();

            if (!_validateHH.emptyListView<pnlTransacciones_ltvProductos>(view.FindByName<ListView>("pnlTransacciones_ltvProductos")))
            {
                int i = 0;

                var source = view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

                foreach (var _lvi in source)
                {
                    LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(view);

                    Producto _objProducto = _logica.levantarProductoSeleccionado(_lvi, true);

                    if (i != pline)
                    {
                        decimal _totalLinea = _objProducto.calcularMontoLinea(view.controlador.v_objCliente);

                        _totalTransaccion = _totalTransaccion + _totalLinea;
                    }

                    i++;
                }
            }

            return _totalTransaccion;
        }
    }
}
