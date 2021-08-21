using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita
{
    internal class LogicaVisitaActualizar
    {
        private vistaVisita view = null;

        internal LogicaVisitaActualizar(vistaVisita pview)
        {
            view = pview;
        }

        internal void actualizarColumnas()
        {
            MiscUtils _miscUtils = new MiscUtils();
            _miscUtils.quantityListViewItems<pnlTransacciones_ltvProductos>(
                view.FindByName<ListView>("pnlTransacciones_ltvProductos"),
                view.FindByName<Label>("pnlTransacciones_clhDescripcion"),
                "Descripción"
                );

            Util _util = new Util();

            _util.sumarItemsColumnaLista<pnlTransacciones_ltvProductos>(
                view.FindByName<ListView>("pnlTransacciones_ltvProductos"),
                view.FindByName<Label>("pnlTransacciones_clhPrecio"),
                3,
                "Precio"
                );

            _util.sumarItemsColumnaLista<pnlTransacciones_ltvProductos>(
                view.FindByName<ListView>("pnlTransacciones_ltvProductos"),
                view.FindByName<Label>("pnlTransacciones_clhImpuesto"),
                5,
                "Impuesto"
                );

        }

        internal void actualizarTotal()
        {
            decimal _montoTransaccion = Numeric._zeroDecimalInitialize;

            LogicaVisitaCalculos _logicaCalculos = new LogicaVisitaCalculos(view);

            _montoTransaccion = _logicaCalculos.calcularMontoTransaccion();

            if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._devolucionNombre))
            {
                view.FindByName<Label>("pnlTransacciones_lblCreditoDisponibleMonto").Text =
                    FormatUtil.applyCurrencyFormat(
                        view.controlador.v_objCliente.creditoDisponible() + _montoTransaccion
                        );
            }
            else
            {
                if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._facturaCreditoNombre)
                    || view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._ordenVentaNombre))
                {
                    view.FindByName<Label>("pnlTransacciones_lblCreditoDisponibleMonto").Text =
                        FormatUtil.applyCurrencyFormat(
                            view.controlador.v_objCliente.creditoDisponible() - _montoTransaccion
                            );
                }
                else
                {
                    view.FindByName<Label>("pnlTransacciones_lblCreditoDisponibleMonto").Text =
                        FormatUtil.applyCurrencyFormat(
                            view.controlador.v_objCliente.creditoDisponible()
                            );
                }
            }

            if (_montoTransaccion < 0)
            {
                _montoTransaccion = Numeric._zeroDecimalInitialize;
            }

            view.FindByName<Label>("pnlTransacciones_lblTotalMonto").Text = FormatUtil.applyCurrencyFormat(_montoTransaccion);

        }
    }
}
