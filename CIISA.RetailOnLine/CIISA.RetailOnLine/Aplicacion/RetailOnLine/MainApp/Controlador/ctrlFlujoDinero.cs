using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    public class ctrlFlujoDinero
    {
        private vistaFlujoDinero view { get; set; }

        internal ctrlFlujoDinero(vistaFlujoDinero pview)
        {
            view = pview;

        }

        public void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlFlujoDinero").Id))
            {
                view.Title = "Flujo Dinero";
            }

            ppanel.IsVisible = true;
        }

        internal void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);
            renderPaneles(view.FindByName<StackLayout>("pnlFlujoDinero"));

            calcularTotalFlujoEfectivo();
        }

        private void calcularTotalFlujoEfectivo()
        {
            Logica_ManagerEncabezadoTransaccion _managerEncabezadoTransaccion = new Logica_ManagerEncabezadoTransaccion();

            decimal _totalOrdenVenta = _managerEncabezadoTransaccion.calcularTotalFlujoEfectivo(ROLTransactions._ordenVentaSigla);
            view.FindByName<Label>("pnlFlujoDinero_lblOrdenVentaMonto").Text = FormatUtil.applyCurrencyFormat(_totalOrdenVenta);

            decimal _totalFacturaContado = _managerEncabezadoTransaccion.calcularTotalFlujoEfectivo(ROLTransactions._facturaContadoSigla);
            view.FindByName<Label>("pnlFlujoDinero_lblFacturaContadoMonto").Text = FormatUtil.applyCurrencyFormat(_totalFacturaContado);

            decimal _totalFacturaCredito = _managerEncabezadoTransaccion.calcularTotalFlujoEfectivo(ROLTransactions._facturaCreditoSigla);
            view.FindByName<Label>("pnlFlujoDinero_lblFacturaCreditoMonto").Text = FormatUtil.applyCurrencyFormat(_totalFacturaCredito);

            decimal _totalCotizacion = _managerEncabezadoTransaccion.calcularTotalFlujoEfectivo(ROLTransactions._cotizacionSigla);
            view.FindByName<Label>("pnlFlujoDinero_lblCotizacionMonto").Text = FormatUtil.applyCurrencyFormat(_totalCotizacion);

            decimal _totalDevolucion = _managerEncabezadoTransaccion.calcularTotalFlujoEfectivo(ROLTransactions._devolucionSigla);
            view.FindByName<Label>("pnlFlujoDinero_lblDevolucionMonto").Text = FormatUtil.applyCurrencyFormat(_totalDevolucion);

            decimal _totalRegalia = _managerEncabezadoTransaccion.calcularTotalFlujoEfectivo(ROLTransactions._regaliaSigla);
            view.FindByName<Label>("pnlFlujoDinero_lblRegaliaMonto").Text = FormatUtil.applyCurrencyFormat(_totalRegalia);

            Logica_ManagerEncabezadoRecibo _managerEncabezadoRecibo = new Logica_ManagerEncabezadoRecibo();
            decimal _totalRecaudacion = _managerEncabezadoRecibo.calcularTotalFlujoEfectivo(ROLTransactions._recaudacionSigla);
            view.FindByName<Label>("pnlFlujoDinero_lblRecaudacionMonto").Text = FormatUtil.applyCurrencyFormat(_totalRecaudacion);

            Logica_ManagerEncabezadoAnulacion _managerEncabezadoAnulacion = new Logica_ManagerEncabezadoAnulacion();
            decimal _totalAnulacion = _managerEncabezadoAnulacion.calcularTotalFlujoEfectivo(ROLTransactions._anulacionSigla);
            view.FindByName<Label>("pnlFlujoDinero_lblAnulacionMonto").Text = FormatUtil.applyCurrencyFormat(_totalAnulacion);

            decimal _totalReciboDinero = _managerEncabezadoRecibo.calcularTotalFlujoEfectivo(ROLTransactions._reciboDineroSigla);
            view.FindByName<Label>("pnlFlujoDinero_lblReciboDineroMonto").Text = FormatUtil.applyCurrencyFormat(_totalReciboDinero);

            Logica_ManagerEncabezadoTramite _managerEncabezadoTramite = new Logica_ManagerEncabezadoTramite();
            decimal _totalTramite = _managerEncabezadoTramite.calcularTotalFlujoEfectivo();
            view.FindByName<Label>("pnlFlujoDinero_lblTramiteMonto").Text = FormatUtil.applyCurrencyFormat(_totalTramite);

            Logica_ManagerPago _managerPago = new Logica_ManagerPago();
            decimal _totalEfectivo = _managerPago.obtenerMontoPorTipoPago(PaymentForm._cashInitials);
            Logica_ManagerPagoRecibo _managerPagoRecibo = new Logica_ManagerPagoRecibo();
            _totalEfectivo += _managerPagoRecibo.obtenerMontoPorTipoPago(PaymentForm._cashInitials);
            view.FindByName<Label>("pnlFlujoDinero_lblTotalEfectivoMonto").Text = FormatUtil.applyCurrencyFormat(_totalEfectivo);

            decimal _totalCheque = _managerPago.obtenerMontoPorTipoPago(PaymentForm._checkInitials);
            _totalCheque += _managerPagoRecibo.obtenerMontoPorTipoPago(PaymentForm._checkInitials);
            view.FindByName<Label>("pnlFlujoDinero_lblTotalChequeMonto").Text = FormatUtil.applyCurrencyFormat(_totalCheque);

            decimal _totalTransferencia = _managerPago.obtenerMontoPorTipoPago(PaymentForm._transferInitials);
            _totalTransferencia += _managerPagoRecibo.obtenerMontoPorTipoPago(PaymentForm._transferInitials);
            view.FindByName<Label>("pnlFlujoDinero_lblTotalTransferenciaMonto").Text = FormatUtil.applyCurrencyFormat(_totalTransferencia);

            decimal _totalDeposito = _managerPago.obtenerMontoPorTipoPago(PaymentForm._depositInitials);
            _totalDeposito += _managerPagoRecibo.obtenerMontoPorTipoPago(PaymentForm._depositInitials);
            view.FindByName<Label>("pnlFlujoDinero_lblTotalDepositoMonto").Text = FormatUtil.applyCurrencyFormat(_totalDeposito);

            decimal _totalADepositar = _totalFacturaContado + _totalRecaudacion + _totalReciboDinero;
            view.FindByName<Label>("pnlFlujoDinero_lblTotalADepositarMonto").Text = FormatUtil.applyCurrencyFormat(_totalADepositar);
        }

        internal async Task menu_mniImprimir_Click()
        {
            ProcesoImpresion _impresion = new ProcesoImpresion();

            await _impresion.imprimirReporteFlujoDinero(
                    view.FindByName<Label>("pnlFlujoDinero_lblOrdenVentaMonto").Text,
                    view.FindByName<Label>("pnlFlujoDinero_lblFacturaContadoMonto").Text,
                    view.FindByName<Label>("pnlFlujoDinero_lblFacturaCreditoMonto").Text,
                    view.FindByName<Label>("pnlFlujoDinero_lblCotizacionMonto").Text,
                    view.FindByName<Label>("pnlFlujoDinero_lblDevolucionMonto").Text,
                    view.FindByName<Label>("pnlFlujoDinero_lblRegaliaMonto").Text,
                    view.FindByName<Label>("pnlFlujoDinero_lblRecaudacionMonto").Text,
                    view.FindByName<Label>("pnlFlujoDinero_lblAnulacionMonto").Text,
                    view.FindByName<Label>("pnlFlujoDinero_lblReciboDineroMonto").Text,
                    view.FindByName<Label>("pnlFlujoDinero_lblTramiteMonto").Text,
                    view.FindByName<Label>("pnlFlujoDinero_lblTotalEfectivoMonto").Text,
                    view.FindByName<Label>("pnlFlujoDinero_lblTotalChequeMonto").Text,
                    view.FindByName<Label>("pnlFlujoDinero_lblTotalTransferenciaMonto").Text,
                    view.FindByName<Label>("pnlFlujoDinero_lblTotalDepositoMonto").Text,
                    view.FindByName<Label>("pnlFlujoDinero_lblTotalADepositarMonto").Text
                    );
        }

        internal void menu_mniClose_Click()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
