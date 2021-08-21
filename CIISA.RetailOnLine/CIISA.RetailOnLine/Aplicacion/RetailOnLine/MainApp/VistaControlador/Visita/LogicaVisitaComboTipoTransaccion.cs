using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista.DevolucionFactura;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita.ComboTipoTransaccion;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using Rg.Plugins.Popup.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita
{
    internal class LogicaVisitaComboTipoTransaccion
    {
        private vistaVisita view = null;

        internal LogicaVisitaComboTipoTransaccion(vistaVisita pview)
        {
            view = pview;
        }

        internal string GetTipoDocumento() {

            return view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString();
        }

        internal async Task ReemplazarElementosListViewPorTipoDocumento()
        {
            LogicaVisitaConstructor _logica = new LogicaVisitaConstructor(view);

            view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource = new ObservableCollection<pnlTransacciones_ltvProductos>();

            await _logica.Constructores(false, false, true);
        }

        internal bool pnlTransacciones_Validar_TipoDocumento_Factura_Devolucion() {

            bool validar = false;

            if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._facturaContadoNombre)
                || view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._facturaCreditoNombre)
                || view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._devolucionNombre)) {

                validar = true;
            }

            return validar;

        }

        internal bool validarIndicadoresFacturaElectronica(ctrlVisita controlador) {

            if (controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_codProveedor)
            {
                return true;
            }

            if (controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_numOrden)
            {
                return true;
            }

            if (controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_fechaOrden)
            {
                return true;
            }

            if (controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_numRecibo)
            {
                return true;
            }

            if (controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_fechaRecibo)
            {
                return true;
            }

            if (controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_numReclamo)
            {
                return true;
            }

            if (controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_fechaReclamo)
            {
                return true;
            }

            return false;
        }

        internal string pnlTransacionTipoTransaccion() {

            return view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString();

        }

        internal async Task pnlTransacciones_cbxTipoTransaccion_SelectedIndexChanged(bool valid)
        {
            if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem != null)
            {
                view.controlador.v_DevolucionFactura = false;

                if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._reciboDineroNombre))
                {
                    LogicaVisitaComboTT_RJ _logicaReciboDinero = new LogicaVisitaComboTT_RJ(view);

                    await _logicaReciboDinero.reciboDinero_SelectedIndexChanged();
                }

                if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._recaudacionNombre))
                {
                    LogicaVisitaComboTT_RC _logicaRecaudacion = new LogicaVisitaComboTT_RC(view);

                    await _logicaRecaudacion.recaudacion_SelectedIndexChanged();
                }

                ValidateHH _validateHH = new ValidateHH();

                if (!_validateHH.emptyListView<pnlTransacciones_ltvProductos>(view.FindByName<ListView>("pnlTransacciones_ltvProductos")))
                {
                    LogicaVisitaVerificaciones _logicaVerificaciones = new LogicaVisitaVerificaciones(view);

                    await _logicaVerificaciones.verificarLineasDetalleCambioTipoTransaccion();
                }

                if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._regaliaNombre) && valid)
                {
                    LogicaVisitaComboTT_RG _logicaRegalia = new LogicaVisitaComboTT_RG(view);

                    await _logicaRegalia.regalia_SelectedIndexChanged();
                }

                if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._devolucionNombre))
                {
                    //Es necesario moverse a la pantalla que le permita al usuario decidir si es devolución normal o
                    //por factura
                    await PopupNavigation.Instance.PushAsync(new vistaTipoDevolucion(view, valid));

                }

                if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._tramiteNombre))
                {
                    LogicaVisitaComboTT_TR _logicaTramite = new LogicaVisitaComboTT_TR(view);
                    //OJO REDIRECCIONA
                    _logicaTramite.tramite_SelectedIndexChanged();
                }

                if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._ordenVentaNombre) && valid)
                {
                    LogicaVisitaComboTT_OV _logicaOrdenVenta = new LogicaVisitaComboTT_OV(view);

                    await _logicaOrdenVenta.ordenVenta_SelectedIndexChanged();
                }

                //Mover de sitio
                ComplementoItemSelected();

            }
        }

        public void ComplementoItemSelected()
        {
            LogicaVisitaActualizar _logicaActualizar = new LogicaVisitaActualizar(view);

            _logicaActualizar.actualizarColumnas();
            _logicaActualizar.actualizarTotal();

            RenderizarBotonesVisita(false);
            //Mover de sitio

            view.FindByName<ListView>("pnlTransacciones_ltvProductos").SelectedItem = null;
        }

        public void RenderizarBotonesVisita(bool render)
        {
            LogicaVisitaRender _logicaRender = new LogicaVisitaRender(view);

            _logicaRender.renderMenu(render);

            _logicaRender.renderBotonesProducto();
        }
    }
}
