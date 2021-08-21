using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita
{
    internal class LogicaVisitaRender
    {
        private vistaVisita view = null;

        internal LogicaVisitaRender(vistaVisita pview)
        {
            view = pview;
        }

        internal void clearBottomMenuBar() {

            view.FindByName<StackLayout>("pnlTransacciones_stkLock").IsVisible = false;
            view.FindByName<StackLayout>("pnlTransacciones_stkGuardar").IsVisible = false;
        }

        internal void renderMenuRutero(bool pinactivarBotonGuardar)
        {
            view.ToolbarItems.Clear();
            clearBottomMenuBar();

            if (view.controlador.v_cambiarCliente)
            {
                view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniCambiarCliente"));
            }


            if (view.controlador.v_PedidoBackOffice && view.controlador.TipoAgente.Equals(Agent._carniceroSigla))
            {
                view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniFacturaManual"));
            }

            if (!view.controlador.v_DevolucionFactura)
            {
                //Si no es devolucion de factura entonces el panel estará con todas las opciones
                view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniSugerido"));
                view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniFactura"));
                view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniPruebaImpresion"));
                view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniCargaPedido"));

                //Si es devolucion aun asi tendra la opcion de cambiar de tipo de devolución
                LogicaVisitaComboBox _logicaVisitaComboBox = new LogicaVisitaComboBox(view);

                if (_logicaVisitaComboBox.getTipoDocumento().Equals(ROLTransactions._devolucionNombre))
                {
                    view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniTipoDevolucion"));
                }

            }
            else
            {
                view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniTipoDevolucion"));
                view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniDevolucionFactura"));
            }

            view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniCoordenadas"));
            view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_GoogleMaps"));
            view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_Waze"));

            ValidateHH _validateHH = new ValidateHH();

            if (_validateHH.emptyListView<pnlTransacciones_ltvProductos>(view.FindByName<ListView>("pnlTransacciones_ltvProductos")))
            {
                //view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniBloquear"));
                view.FindByName<StackLayout>("pnlTransacciones_stkLock").IsVisible = true;
            }
            else
            {
                if (!pinactivarBotonGuardar)
                {
                    //view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniGuardar"));
                    view.FindByName<StackLayout>("pnlTransacciones_stkGuardar").IsVisible = true;
                }
            }
        }

        internal void renderMenuFacturacionFaltante()
        {
            view.ToolbarItems.Clear();
            clearBottomMenuBar();

            if (view.controlador.v_FacturacionFaltante)
            {
                view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniFactura"));
                view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniPruebaImpresion"));
                view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniCoordenadas"));
                //view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniGuardar"));
                view.FindByName<StackLayout>("pnlTransacciones_stkGuardar").IsVisible = true;
            }
            else
            {
                if (view.controlador.v_cambiarCliente)
                {
                    view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniCambiarCliente"));
                }
                view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniSugerido"));
                view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniCargaPedido"));
            }          

            ValidateHH _validateHH = new ValidateHH();

            if (_validateHH.emptyListView<pnlTransacciones_ltvProductos>(view.FindByName<ListView>("pnlTransacciones_ltvProductos")))
            {
                //view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniBloquear"));
                view.FindByName<StackLayout>("pnlTransacciones_stkLock").IsVisible = true;
            }
        }

        internal void renderMenu(bool pinactivarBotonGuardar)
        {
            if (view.FindByName<StackLayout>("pnlTransacciones").IsVisible)
            {
                if (view.controlador.v_FacturacionFaltante)
                {
                    renderMenuFacturacionFaltante();
                }
                else
                {
                    renderMenuRutero(pinactivarBotonGuardar);
                }
            }
        }

        internal void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlTransacciones"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlTransacciones").Id))
            {
                view.Title = "Transacción";
            }

            ppanel.IsVisible = true;

            renderMenu(false);
        }

        internal void renderComponentesPnlTransacciones(bool pcarga)
        {
            if (pcarga)
            {
                LogicaVisitaComboBox _logicaVisitaComboBox = new LogicaVisitaComboBox(view);
                _logicaVisitaComboBox.llenarComboBoxTipoTransaccion();

                view.FindByName<Label>("pnlTransacciones_lblTotalMonto").Text = Numeric._zeroDecimal;
                view.FindByName<Label>("pnlTransacciones_lblCreditoDisponibleMonto").Text = Numeric._zeroDecimal; ;
            }

            view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource = new ObservableCollection<pnlTransacciones_ltvProductos>();

            LogicaVisitaActualizar _logica = new LogicaVisitaActualizar(view);

            _logica.actualizarColumnas();
            _logica.actualizarTotal();
        }

        internal void RenderVisitaProductos()
        {
            view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource = new ObservableCollection<pnlTransacciones_ltvProductos>();

            LogicaVisitaActualizar _logica = new LogicaVisitaActualizar(view);

            _logica.actualizarColumnas();
            _logica.actualizarTotal();
        }

        internal void renderBotonesProducto()
        {
            string _nomTipoTransaccion = view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString();

            if (_nomTipoTransaccion.Equals(ROLTransactions._ordenVentaNombre))
            {
                view.FindByName<Button>("pnlTransacciones_btnEliminar").IsEnabled = true;
                view.FindByName<Button>("pnlTransacciones_btnEliminarTodos").IsEnabled = true;
                view.FindByName<Button>("pnlTransacciones_btnCalcularMontoLinea").IsEnabled = true;

                view.FindByName<Button>("pnlTransacciones_btnMotivo").IsEnabled = false;
                view.FindByName<Button>("pnlTransacciones_btnEspecificacion").IsEnabled = true;
                view.FindByName<Button>("pnlTransacciones_btnDetalleReses").IsEnabled = false;

                view.FindByName<Label>("pnlTransacciones_clhCodigo").IsEnabled = true;
            }

            if (_nomTipoTransaccion.Equals(ROLTransactions._facturaContadoNombre))
            {
                view.FindByName<Button>("pnlTransacciones_btnEliminar").IsEnabled = true;
                view.FindByName<Button>("pnlTransacciones_btnEliminarTodos").IsEnabled = true;
                view.FindByName<Button>("pnlTransacciones_btnCalcularMontoLinea").IsEnabled = true;

                view.FindByName<Button>("pnlTransacciones_btnMotivo").IsEnabled = false;
                view.FindByName<Button>("pnlTransacciones_btnEspecificacion").IsEnabled = false;
                view.FindByName<Button>("pnlTransacciones_btnDetalleReses").IsEnabled = true;

                view.FindByName<Label>("pnlTransacciones_clhCodigo").IsEnabled = true;
            }

            if (_nomTipoTransaccion.Equals(ROLTransactions._facturaCreditoNombre))
            {
                view.FindByName<Button>("pnlTransacciones_btnEliminar").IsEnabled = true;
                view.FindByName<Button>("pnlTransacciones_btnEliminarTodos").IsEnabled = true;
                view.FindByName<Button>("pnlTransacciones_btnCalcularMontoLinea").IsEnabled = true;

                view.FindByName<Button>("pnlTransacciones_btnMotivo").IsEnabled = false;
                view.FindByName<Button>("pnlTransacciones_btnEspecificacion").IsEnabled = false;
                view.FindByName<Button>("pnlTransacciones_btnDetalleReses").IsEnabled = true;

                view.FindByName<Label>("pnlTransacciones_clhCodigo").IsEnabled = true;
            }

            if (_nomTipoTransaccion.Equals(ROLTransactions._cotizacionNombre))
            {
                view.FindByName<Button>("pnlTransacciones_btnEliminar").IsEnabled = true;
                view.FindByName<Button>("pnlTransacciones_btnEliminarTodos").IsEnabled = true;
                view.FindByName<Button>("pnlTransacciones_btnCalcularMontoLinea").IsEnabled = true;

                view.FindByName<Button>("pnlTransacciones_btnMotivo").IsEnabled = false;
                view.FindByName<Button>("pnlTransacciones_btnEspecificacion").IsEnabled = false;
                view.FindByName<Button>("pnlTransacciones_btnDetalleReses").IsEnabled = false;

                view.FindByName<Label>("pnlTransacciones_clhCodigo").IsEnabled = true;
            }

            if (_nomTipoTransaccion.Equals(ROLTransactions._devolucionNombre))
            {
                view.FindByName<Button>("pnlTransacciones_btnEliminar").IsEnabled = true;
                view.FindByName<Button>("pnlTransacciones_btnEliminarTodos").IsEnabled = true;
                view.FindByName<Button>("pnlTransacciones_btnCalcularMontoLinea").IsEnabled = true;

                view.FindByName<Button>("pnlTransacciones_btnMotivo").IsEnabled = true;
                view.FindByName<Button>("pnlTransacciones_btnEspecificacion").IsEnabled = false;
                view.FindByName<Button>("pnlTransacciones_btnDetalleReses").IsEnabled = false;

                view.FindByName<Label>("pnlTransacciones_clhCodigo").IsEnabled = true;
            }

            if (_nomTipoTransaccion.Equals(ROLTransactions._regaliaNombre))
            {
                view.FindByName<Button>("pnlTransacciones_btnEliminar").IsEnabled = true;
                view.FindByName<Button>("pnlTransacciones_btnEliminarTodos").IsEnabled = true;
                view.FindByName<Button>("pnlTransacciones_btnCalcularMontoLinea").IsEnabled = true;

                view.FindByName<Button>("pnlTransacciones_btnMotivo").IsEnabled = true;
                view.FindByName<Button>("pnlTransacciones_btnEspecificacion").IsEnabled = false;
                view.FindByName<Button>("pnlTransacciones_btnDetalleReses").IsEnabled = false;

                view.FindByName<Label>("pnlTransacciones_clhCodigo").IsEnabled = true;
            }

            if (_nomTipoTransaccion.Equals(ROLTransactions._recaudacionNombre))
            {
                view.FindByName<Button>("pnlTransacciones_btnEliminar").IsEnabled = false;
                view.FindByName<Button>("pnlTransacciones_btnEliminarTodos").IsEnabled = false;
                view.FindByName<Button>("pnlTransacciones_btnCalcularMontoLinea").IsEnabled = false;

                view.FindByName<Button>("pnlTransacciones_btnMotivo").IsEnabled = false;
                view.FindByName<Button>("pnlTransacciones_btnEspecificacion").IsEnabled = false;
                view.FindByName<Button>("pnlTransacciones_btnDetalleReses").IsEnabled = false;

                view.FindByName<Label>("pnlTransacciones_clhCodigo").IsEnabled = true;
            }

            if (_nomTipoTransaccion.Equals(ROLTransactions._reciboDineroNombre))
            {
                view.FindByName<Button>("pnlTransacciones_btnEliminar").IsEnabled = false;
                view.FindByName<Button>("pnlTransacciones_btnEliminarTodos").IsEnabled = false;
                view.FindByName<Button>("pnlTransacciones_btnCalcularMontoLinea").IsEnabled = false;

                view.FindByName<Button>("pnlTransacciones_btnMotivo").IsEnabled = false;
                view.FindByName<Button>("pnlTransacciones_btnEspecificacion").IsEnabled = false;
                view.FindByName<Button>("pnlTransacciones_btnDetalleReses").IsEnabled = false;

                view.FindByName<Label>("pnlTransacciones_clhCodigo").IsEnabled = true;
            }

            if (view.controlador.v_DevolucionFactura)
            {
                view.FindByName<Button>("pnlTransacciones_btnEliminar").IsEnabled = false;
                view.FindByName<Button>("pnlTransacciones_btnEliminarTodos").IsEnabled = false;
                view.FindByName<Button>("pnlTransacciones_btnCalcularMontoLinea").IsEnabled = false;

                view.FindByName<Button>("pnlTransacciones_btnMotivo").IsEnabled = true;
                view.FindByName<Button>("pnlTransacciones_btnEspecificacion").IsEnabled = false;
                view.FindByName<Button>("pnlTransacciones_btnDetalleReses").IsEnabled = false;

                view.FindByName<Label>("pnlTransacciones_clhCodigo").IsEnabled = false;
            }


            //Restriccion adicional en caso de que haya un pedido desde BackOffice y
            //se trate de un agente carnicero
            if (view.controlador.v_PedidoBackOffice && view.controlador.TipoAgente.Equals(Agent._carniceroSigla))
            {
                view.FindByName<Label>("pnlTransacciones_clhCodigo").IsEnabled = false;
            }


        }
    }
}
