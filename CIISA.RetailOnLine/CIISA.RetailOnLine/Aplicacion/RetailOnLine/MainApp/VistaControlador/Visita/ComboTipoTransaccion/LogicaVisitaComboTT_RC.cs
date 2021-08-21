using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita.ComboTipoTransaccion
{
    internal class LogicaVisitaComboTT_RC
    {
        private vistaVisita view = null;

        internal LogicaVisitaComboTT_RC(vistaVisita pview)
        {
            view = pview;
        }

        internal async Task recaudacion_SelectedIndexChanged()
        {
            view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource = new ObservableCollection<pnlTransacciones_ltvProductos>();
            bool validar = view.controlador.TipoAgente.Equals(Agent._carniceroSigla) ? true : false;
            LogicaVisitaRender _logicaVisitaRender = new LogicaVisitaRender(view);

            if (!view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_esCobrador)
            {
                if (view.controlador.v_finalizoConstructor2)
                {
                    ShowSROL _showRecaudacion = new ShowSROL();

                    _showRecaudacion.mostrarPantallaVisitaRecaudacion(view.controlador.v_objCliente,view);
                }
            }
            else
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention("Cobro a otra ruta");

                if (await LogMessages._dialogResultYes("¿Desea realizar la recaudación?", "Cobro a otra ruta"))
                {
                    LogicaVisitaPedido _logicaVisitaPedido = new LogicaVisitaPedido(view);

                    _logicaVisitaPedido.guardarPedido(true, validar);

                    ShowSROL _showRecaudacion = new ShowSROL();

                    _showRecaudacion.mostrarPantallaVisitaRecaudacion(view.controlador.v_objCliente, view);
                }

                LogicaVisitaComboBox _logicaVisitaComboBox = new LogicaVisitaComboBox(view);

                await _logicaVisitaComboBox.filtrarComboBoxTipoTransaccionPorIndicadores();

                _logicaVisitaRender.renderComponentesPnlTransacciones(false);
            }

            _logicaVisitaRender.renderMenu(false);
        }

        internal async Task Respuesta(string _motivo)
        {
            LogicaVisitaRender _logicaVisitaRender = new LogicaVisitaRender(view);

            if (!view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_esCobrador)
            {
                LogicaVisitaPedido _logicaVisitaPedido = new LogicaVisitaPedido(view);

                if (!_motivo.Equals(string.Empty))
                {
                    LogicaVisitaMenu _logicaVisitaMenu = new LogicaVisitaMenu(view);

                    await _logicaVisitaMenu.menu_mniGuardar_Click();

                    _logicaVisitaRender.renderComponentesPnlTransacciones(false);

                    if (view.controlador.v_objCliente.v_objTransaccion.v_codDocumento.Equals(string.Empty))
                    {
                        await _logicaVisitaPedido.cargaPedidosRecuperar(false, string.Empty);

                        LogicaVisitaComboBox _logicaVisitaComboBox = new LogicaVisitaComboBox(view);

                        _logicaVisitaComboBox.ReiniciarPosicionComboTipoTransaccion();
                    }
                }
                else
                {
                    await _logicaVisitaPedido.cargaPedidosRecuperar(false, string.Empty);

                    LogicaVisitaComboBox _logicaVisitaComboBox = new LogicaVisitaComboBox(view);

                    _logicaVisitaComboBox.ReiniciarPosicionComboTipoTransaccion();
                }
            }
        }
    }
}
