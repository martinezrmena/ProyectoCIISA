using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita.ComboTipoTransaccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers.DevolucionFactura
{

    public class Helper_DevolucionFactura
    {
        public vistaVisita view { get; set; }

        public Helper_DevolucionFactura(vistaVisita v_view)
        {
            view = v_view;
        }


        internal async Task ProcesarTipoDevolucion()
        {
            if (view.controlador.v_DevolucionFactura)
            {
                LogicaVisitaComboTipoTransaccion logicaVisitaComboTipoTransaccion = new LogicaVisitaComboTipoTransaccion(view);

                logicaVisitaComboTipoTransaccion.ComplementoItemSelected();

                view.FindByName<ListView>("pnlTransacciones_ltvProductos").SelectedItem = null;

                LogicaVisitaComboTT_DF _logicaDevolucionFactura = new LogicaVisitaComboTT_DF(view);

                _logicaDevolucionFactura.devolucionFactura();
            }
            else if (view.controlador.valid)
            {
                LogicaVisitaComboTT_DV _logicaDevolucion = new LogicaVisitaComboTT_DV(view);

                await _logicaDevolucion.devolucion_SelectedIndexChanged();

            }

            LogicaVisitaRender _logicaRender = new LogicaVisitaRender(view);
            _logicaRender.renderMenuRutero(false);
            _logicaRender.renderBotonesProducto();
        }

    }
}
