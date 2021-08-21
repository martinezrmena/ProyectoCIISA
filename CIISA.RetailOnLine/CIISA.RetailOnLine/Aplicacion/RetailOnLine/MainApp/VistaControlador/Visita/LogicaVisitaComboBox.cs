using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita
{
    internal class LogicaVisitaComboBox
    {
        private vistaVisita view = null;

        internal LogicaVisitaComboBox(vistaVisita pview)
        {
            view = pview;
        }

        internal void llenarComboBoxTipoTransaccion()
        {
            view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").Items.Clear();

            Logica_ManagerTipoTransaccion _manager = new Logica_ManagerTipoTransaccion();

            DataTable _dt = _manager.buscarTipoTransaccion();

            Util _util = new Util();

            _util.fillComboBox(
                _dt,
                view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                "Descripcion"
                );
        }

        private void filtrarComboBoxTipoTransaccionClienteSoloCobro()
        {
            Util _util = new Util();

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._ordenVentaNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._facturaContadoNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._facturaCreditoNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._cotizacionNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._devolucionNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._regaliaNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._tramiteNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._anulacionNombre
                    );
        }

        private void filtrarComboBoxTipoTransaccionClienteNuevo()
        {
            Util _util = new Util();

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._facturaCreditoNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._tramiteNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._anulacionNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._devolucionNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._reciboDineroNombre
                    );
        }

        private async Task evaluarFacturasVencidasFiltrarReciboDinero()
        {
            Util _util = new Util();

            decimal _monto = view.controlador.v_objCliente.montoPendienteDePago();

            if (_monto > 0)
            {
                if (await LogMessages._dialogResultYes(
                    "El Cliente posee facturas pendientes por un monto de: "
                    + Environment.NewLine
                    + Environment.NewLine
                    + FormatUtil.applyCurrencyFormat(_monto)
                    + Simbol._point
                    + Environment.NewLine
                    + Environment.NewLine
                    + "¿Desea ver las facturas?"
                    , "Facturas Pendientes"))
                {
                    Logica_ManagerTipoTransaccion _manager = new Logica_ManagerTipoTransaccion();

                    string _codTipoTransaccion = _manager.obtenerCodigoTipoTransaccion(
                                                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString()
                                                    );

                    view.controlador.v_objCliente.v_objTransaccion.v_objTipoDocumento.SetSigla(_codTipoTransaccion);

                    ShowSROL _show = new ShowSROL();

                    _show.mostrarPantallaFactura(view.controlador.v_objCliente, PantallasSistema._pantallaVisita,null);
                }
            }
            else
            {
                _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._reciboDineroNombre
                    );
            }
        }

        private async Task filtrarComboBoxTipoTransaccionClienteExistente()
        {
            Util _util = new Util();

            if (view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_pedido == false)
            {
                _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._ordenVentaNombre
                    );
            }

            if (view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_vencimiento)
            {
                Logica_ManagerFactura _manager = new Logica_ManagerFactura();

                if (_manager.ExisteFacturasVencidas(view.controlador.v_objCliente))
                {
                    Logica_ManagerFactura _managerFactura = new Logica_ManagerFactura();

                    List<string> _facturasVencidas = _managerFactura.facturasVencidas(view.controlador.v_objCliente);

                    decimal _saldoFacturasVencidas = Numeric._zeroDecimalInitialize;

                    foreach (string pcodFactura in _facturasVencidas)
                    {
                        Logica_ManagerDetalleRecibo _managerDetalleRecibo = new Logica_ManagerDetalleRecibo();

                        decimal _saldoFactura = _managerFactura.obtenerSaldoFactura(pcodFactura);
                        decimal _pagosFacturas = _managerDetalleRecibo.obtenerPagosFactura(pcodFactura);

                        _saldoFacturasVencidas += (_saldoFactura - _pagosFacturas);
                    }

                    if (_saldoFacturasVencidas > 0)
                    {
                        _util.deleteItemComboBox(
                            view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                            ROLTransactions._facturaContadoNombre
                            );

                        _util.deleteItemComboBox(
                            view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                            ROLTransactions._facturaCreditoNombre
                            );
                    }
                    else if (view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_limiteCredito == 0)
                    {
                        _util.deleteItemComboBox(
                            view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                            ROLTransactions._facturaCreditoNombre
                            );
                    }

                    await evaluarFacturasVencidasFiltrarReciboDinero();
                }
                else
                {

                    if (view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_facturaContado == false)
                    {
                        _util.deleteItemComboBox(
                            view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                            ROLTransactions._facturaContadoNombre
                            );
                    }
                    else
                    {

                        await evaluarFacturasVencidasFiltrarReciboDinero();
                    }

                    if (view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_facturaCredito == false
                        || view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_facturaContado == true)
                    {
                        if (view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_respetaLimiteCredito)
                        {
                            if (view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_limiteCredito == 0)
                            {
                                _util.deleteItemComboBox(
                                view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                                ROLTransactions._facturaCreditoNombre
                                );
                            }
                        }
                    }
                }
            }
            else if (view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_respetaLimiteCredito)
            {
                if (view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_limiteCredito == 0)
                {
                    _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._facturaCreditoNombre
                    );
                }
            }
        }

        private async Task filtrarComboBoxTipoTransaccionClienteActivo()
        {
            if (view.controlador.v_objCliente.v_nuevoCliente)
            {
                filtrarComboBoxTipoTransaccionClienteNuevo();
            }
            else
            {
                await filtrarComboBoxTipoTransaccionClienteExistente();
            }
        }

        private void filtrarComboBoxTipoTransaccionClienteInactivo()
        {
            Util _util = new Util();

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._ordenVentaNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._facturaContadoNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._facturaCreditoNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._cotizacionNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._devolucionNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._regaliaNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._tramiteNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._anulacionNombre
                    );
        }

        private void filtrarComboBoxTipoTransaccionFacturacionFaltantes()
        {
            Util _util = new Util();

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._ordenVentaNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._facturaContadoNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._cotizacionNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._devolucionNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._regaliaNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._tramiteNombre
                    );

            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._anulacionNombre
                    );
            _util.deleteItemComboBox(
                    view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                    ROLTransactions._recaudacionNombre
                    );
        }

        internal async Task filtrarComboBoxTipoTransaccionPorIndicadores()
        {
            Util _util = new Util();

            _util.deleteItemComboBox(
                view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                Pedido._mnv_noVentaNombre
                );

            _util.deleteItemComboBox(
                view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                ROLTransactions._anulacionNombre
                );

            _util.deleteItemComboBox(
                view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                ROLTransactions._recibosCobradoresNombre
                );

            _util.deleteItemComboBox(
                view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                ROLTransactions._facturaNombre
                );

            _util.deleteItemComboBox(
                view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                ROLTransactions._facturaDistribucionNombre
                );

            _util.deleteItemComboBox(
                view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                ROLTransactions._notaCreditoNombre
                );

            _util.deleteItemComboBox(
                view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                ROLTransactions._notaDebitoNombre
                );

            _util.deleteItemComboBox(
                view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion"),
                ROLTransactions._chequeDevueltoNombre
                );

            if (view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_estado)
            {
                if (view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_esCobrador)
                {
                    filtrarComboBoxTipoTransaccionClienteSoloCobro();
                }
                else
                {
                    await filtrarComboBoxTipoTransaccionClienteActivo();
                }
            }
            else
            {

                if (view.controlador.v_FacturacionFaltante)
                {
                    filtrarComboBoxTipoTransaccionFacturacionFaltantes();
                }
                else
                {
                    LogMessageAttention _logMessageAttention = new LogMessageAttention();
                    await _logMessageAttention.generalAttention(
                        "Este cliente está cerrado."
                        + Environment.NewLine
                        + Environment.NewLine
                        + "Únicamente puede realizar: "
                        + Environment.NewLine
                        + Simbol._bulletPoint
                        + "Recibos de Dinero (si tiene facturas pendientes)."
                        + Environment.NewLine
                        + Simbol._bulletPoint
                        + "Recaudaciónes"
                        );

                    filtrarComboBoxTipoTransaccionClienteInactivo();
                }

                await evaluarFacturasVencidasFiltrarReciboDinero();
            }

            view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedIndex = 0;
        }

        public string getTipoDocumento()
        {
            string value = string.Empty;

            if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem != null)
            {
                value = view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString();
            }

            return value;
        }

        internal void ReiniciarPosicionComboTipoTransaccion()
        {
            view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedIndexChanged -= view.pnlTransacciones_cbxTipoTransaccion_SelectedIndexChanged;

            view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedIndex = 0;

            view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedIndexChanged += view.pnlTransacciones_cbxTipoTransaccion_SelectedIndexChanged;
        }
    }
}
