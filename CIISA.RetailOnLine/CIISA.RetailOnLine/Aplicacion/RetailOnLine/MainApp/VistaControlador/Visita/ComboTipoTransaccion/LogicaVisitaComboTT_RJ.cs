using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita.ComboTipoTransaccion
{
    internal class LogicaVisitaComboTT_RJ
    {
        private vistaVisita view = null;

        internal LogicaVisitaComboTT_RJ(vistaVisita pview)
        {
            view = pview;
        }

        private async Task ReciboDineroSeleccionado()
        {
            view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource = new ObservableCollection<pnlTransacciones_ltvProductos>();

            if (view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_estado)
            {
                LogicaVisitaActualizar _logicaVisitaActualizar = new LogicaVisitaActualizar(view);

                _logicaVisitaActualizar.actualizarTotal();

                LogicaVisitaMenu _logicaVisitaMenu = new LogicaVisitaMenu(view);

                await _logicaVisitaMenu.menu_mniGuardar_Click();
            }
            else
            {
                if (view.controlador.v_objCliente.montoPendienteDePago() > 0)
                {
                    if (await LogMessages._dialogResultYes("¿Desea cancelar la(s) factura(s) pendiente(s) de pago?", "Facturas"))
                    {
                        LogicaVisitaMenu _logicaVisitaMenu = new LogicaVisitaMenu(view);

                        await _logicaVisitaMenu.menu_mniGuardar_Click();
                    }
                }
                else
                {
                    LogMessageAttention _logMessageAttention = new LogMessageAttention();
                    await _logMessageAttention.generalAttention("El Cliente no posee facturas pendientes de pago."
                        + Environment.NewLine
                        + Environment.NewLine
                        + "La cuenta del Cliente se encuentra cerrada."
                        + Environment.NewLine
                        + Environment.NewLine
                        + "A continuación se muestra el estado de indicadores para el Cliente");

                    LogicaVisitaContextMenu _logica = new LogicaVisitaContextMenu(view);

                    await _logica.indicadores();
                }
            }

            //Mover de sitio

            //LogicaVisitaPedido _logicaVisitaPedido = new LogicaVisitaPedido(view);

            //await _logicaVisitaPedido.cargaPedidosRecuperar();
            //Mover de sitio
        }

        public void CargarPedidosRecuperar()
        {
            //LogicaVisitaPedido _logicaVisitaPedido = new LogicaVisitaPedido(view);

            //await _logicaVisitaPedido.cargaPedidosRecuperar();
            //Mover de sitio
        }

        internal async Task reciboDinero_SelectedIndexChanged()
        {
            if (!view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_esCobrador)
            {
                await ReciboDineroSeleccionado();
            }
            else
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention("Cobro a otra ruta");

                if (await LogMessages._dialogResultYes("¿Desea realizar el recibo dinero?", "Cobro a otra ruta"))
                {

                    await ReciboDineroSeleccionado();
                }

            }
        }
    }
}
