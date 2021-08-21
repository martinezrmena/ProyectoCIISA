using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita
{
    internal class LogicaVisitaBotones
    {
        private vistaVisita view = null;

        internal LogicaVisitaBotones(vistaVisita pview)
        {
            view = pview;
        }

        internal async Task pnlTransacciones_btnEliminar_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (!_validateHH.emptyListView<pnlTransacciones_ltvProductos>(view.FindByName<ListView>("pnlTransacciones_ltvProductos")))
            {
                Util _util = new Util();

                await _util.deleteElementListView<pnlTransacciones_ltvProductos>(view.FindByName<ListView>("pnlTransacciones_ltvProductos"));

                LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(view);

                _logica.actualizarProductoInventario();

                LogicaVisitaActualizar _logicaActualizar = new LogicaVisitaActualizar(view);

                _logicaActualizar.actualizarColumnas();
                _logicaActualizar.actualizarTotal();

                LogicaVisitaRender _logicaRender = new LogicaVisitaRender(view);

                _logicaRender.renderMenu(false);
            }
        }

        internal async Task pnlTransacciones_btnEliminarTodos_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (!_validateHH.emptyListView<pnlTransacciones_ltvProductos>(view.FindByName<ListView>("pnlTransacciones_ltvProductos")))
            {
                if (await LogMessages._dialogResultYes("¿Desea eliminar todas las líneas?", "Eliminar"))
                {

                    view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource = new ObservableCollection<pnlTransacciones_ltvProductos>();

                    LogicaVisitaActualizar _logicaActualizar = new LogicaVisitaActualizar(view);
                    _logicaActualizar.actualizarColumnas();
                    _logicaActualizar.actualizarTotal();

                    LogicaVisitaRender _logicaRender = new LogicaVisitaRender(view);

                    _logicaRender.renderMenu(false);
                }
            }
        }

        internal async Task pnlTransacciones_btnCalcularMontoLinea_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (!_validateHH.emptyListView<pnlTransacciones_ltvProductos>(view.FindByName<ListView>("pnlTransacciones_ltvProductos")))
            {
                bool _elementoSeleccionado = await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlTransacciones_ltvProductos"));

                if (_elementoSeleccionado)
                {
                    LogicaVisitaCalculos _logicaCalculos = new LogicaVisitaCalculos(view);

                    await _logicaCalculos.calcularMontoLinea();
                }
            }
        }

        internal async Task pnlTransacciones_btnEspecificacion_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (!_validateHH.emptyListView<pnlTransacciones_ltvProductos>(view.FindByName<ListView>("pnlTransacciones_ltvProductos")))
            {
                if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlTransacciones_ltvProductos")))
                {
                    var _lvi = view.FindByName<ListView>("pnlTransacciones_ltvProductos").SelectedItem as pnlTransacciones_ltvProductos;

                    LogicaVisitaLtvProducto _logicaVisitaProducto = new LogicaVisitaLtvProducto(view);

                    Producto _objProducto = _logicaVisitaProducto.levantarProductoSeleccionado(_lvi, true);

                    ShowSROL _show = new ShowSROL();
                    _show.MostrarPantallaVisitaEspecificacion(_objProducto,view);
                }
            }
        }

        internal async Task pnlTransacciones_btnMotivo_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (!_validateHH.emptyListView<pnlTransacciones_ltvProductos>(view.FindByName<ListView>("pnlTransacciones_ltvProductos")))
            {
                if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlTransacciones_ltvProductos")))
                {
                    var _lvi = view.FindByName<ListView>("pnlTransacciones_ltvProductos").SelectedItem as pnlTransacciones_ltvProductos;

                    LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(view);

                    Producto _objProducto = _logica.levantarProductoSeleccionado(_lvi, true);

                    if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._regaliaNombre))
                    {
                        ShowSROL _show = new ShowSROL();

                        _show.MostrarPantallaVisitaRegalia(_objProducto,view);

                    }

                    if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._devolucionNombre))
                    {
                        ShowSROL _show = new ShowSROL();

                        _show.mostrarPantallaVisitaDevolucion(_objProducto, true,view);
                    }
                }
            }

        }
    }
}
