using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Carniceria
{
    public class LogicaCarniceriaEventos
    {
        public vistaVisita view = null;
        public vistaProducto viewP = null;

        internal LogicaCarniceriaEventos(vistaVisita pview)
        {
            view = pview;
        }

        internal LogicaCarniceriaEventos(vistaProducto pview)
        {
            viewP = pview;
        }

        internal async Task pnlTransacciones_ltvProductos_ItemActivate(
            pnlTransacciones_ltvProductos _lvi,
            LogicaVisitaLtvProducto _logica,
            Producto _objProducto,
            int numLinea,
            string COD_DOCUMENTO
            )
        {
            ValidateHH _validateHH = new ValidateHH();

            if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlTransacciones_ltvProductos")))
            {
                ShowSROL _show = new ShowSROL();

                _show.MostrarPantallaDetalleReses(
                    view.controlador.v_objCliente,
                    _objProducto,
                    this,
                    PantallasSistema._pantallaVisita,
                    numLinea,
                    COD_DOCUMENTO,
                    view?.controlador?.COD_PEDIDO);
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

        public void pnlProductos_ltvProductos(
            Cliente cliente, 
            string codProducto, 
            LogicaCarniceriaEventos _logic, 
            string pantalla, 
            ctrlProducto ctrl,
            string codpedido) {

            ShowSROL _show = new ShowSROL();

            _show.MostrarPantallaDetalleReses(
                cliente,
                codProducto,
                this,
                pantalla,
                ctrl,
                codpedido
                );

        }

        public async Task pnlProductos_ltvProductos_Parte2(decimal cantidad, ctrlProducto ctrl) {

            viewP.FindByName<ExtendedEntry>("pnlProductos_txtCantidad").Text = cantidad.ToString();

            await ctrl.menu_mniAgregar_Click();
        }

    }
}
