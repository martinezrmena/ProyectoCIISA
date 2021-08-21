using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Helpers;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita.Guardar
{
    public class LogicaVisitaGuardar
    {
        private vistaVisita view = null;

        private TransaccionEncabezado v_objTransaccionEncabezado = null;

        public bool Tramitar { get; set; } = false;

        internal LogicaVisitaGuardar(vistaVisita pview)
        {
            view = pview;
        }

        public LogicaVisitaGuardar()
        {

        }

        internal async Task<bool> EsTipoTransaccionCorrecta(bool pmodificarCantidad)
        {
            bool _guardar = false;

            LogicaVisitaRender _logicaRender = new LogicaVisitaRender(view);

            _logicaRender.renderMenu(true);

            if (!view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._reciboDineroNombre))
            {
                if (!view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._recaudacionNombre))
                {
                    bool _validarCantidadesDiferentesDeCero = LogicaVisitaValidaciones.validarCantidadesDiferentesDeCero(view);

                    if (!_validarCantidadesDiferentesDeCero)
                    {
                        if (view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_estado)
                        {
                            //Validacion 50mts
                            bool _validacion50Metros = await LogicaVisitaValidaciones.validar50mts(view);

                            if (_validacion50Metros)
                            {
                                string Titulo = "IMPORTANTE";
                                string Mensaje = "¿Seguro que la transacción es un(a) "
                                    + view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString()
                                    + "?"
                                    + Environment.NewLine
                                    + Environment.NewLine
                                    + "* Evite anular un documento.";

                                _guardar = await LogMessages._dialogResultYes(Mensaje, Titulo);
                            }
                            else
                            {
                                //Por petición del cliente siempre debe dejar facturar aunque
                                //se encuentre a 50 metros, por el momento solo interesa la notificación
                                _guardar = false;
                            }
                        }
                        else
                        {
                            _guardar = false;
                        }
                    }
                    else
                    {
                        pmodificarCantidad = true;

                        LogMessageAttention LMA = new LogMessageAttention();
                        await LMA.generalAttention("Modifique la cantidad de las líneas en rojo por un valor mayor que cero.");
                    }
                }
                else
                {
                    _guardar = true;
                }
            }
            else
            {
                _guardar = true;
            }

            return _guardar;
        }

        internal TransaccionEncabezado CargaInformacionParaGuardar()
        {
            var _objTransaccion = new TransaccionEncabezado();

            if (view.FindByName<StackLayout>("pnlTransacciones").IsVisible)
            {
                if (view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._tramiteNombre))
                {
                    string _facturaTramitar = view.controlador.v_objCliente.v_objTransaccion.v_facturaTramitar;

                    var _managerTransaccion = new Logica_ManagerTransaccion(view);

                    _objTransaccion = _managerTransaccion.buscarTransaccionParaAnulaciones(
                        _facturaTramitar,
                        ROLTransactions._facturaCreditoSigla
                        );

                    _objTransaccion.v_facturaTramitar = _facturaTramitar;
                    _objTransaccion.v_objTipoDocumento.SetSigla(ROLTransactions._tramiteSigla);
                    _objTransaccion.v_tramite = SQL._Si;

                    view.controlador.v_objCliente.v_objTransaccion = _objTransaccion;
                }
                else
                {
                    LogicaVisitaLevantarObjetos _logicaLevantarObjetos = new LogicaVisitaLevantarObjetos(view);

                    _objTransaccion = _logicaLevantarObjetos.levantarObjetoTransaccionEncabezado();

                    _logicaLevantarObjetos.levantarObjetoTransaccionDetalle(_objTransaccion);
                }
            }

            return _objTransaccion;
        }

        internal async Task ProcesamientoPorTipoDocumento(TransaccionEncabezado pobjTransaccion)
        {
            switch (pobjTransaccion.v_objTipoDocumento.GetSigla())
            {
                case ROLTransactions._facturaContadoSigla:

                    ShowSROL _showFormaPago = new ShowSROL();

                    _showFormaPago.mostrarPantallaFormaPagoFacturaContado(
                        view,
                        view.controlador.v_objCliente.v_objTransaccion.v_total,
                        PantallasSistema._pantallaVisita,
                        pobjTransaccion.v_objTipoDocumento.GetSigla(),
                        view.controlador.v_objCliente,
                        this
                        );

                    break;

                case ROLTransactions._facturaCreditoSigla:

                    bool _verificarCredito = false;
                    Logica_ManagerTransaccion _managerTransaccion = new Logica_ManagerTransaccion(view);

                    if (view.controlador.v_objCliente.v_listaAutorizadosFirmar.Count <= 0)
                    {
                        string _tipoAgente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._typeAgent;

                        if (_tipoAgente.Equals(Agent._ruteroSigla) ||
                            _tipoAgente.Equals(Agent._carniceroSigla) ||
                            _tipoAgente.Equals(Agent._supermercadoSigla) ||
                            _tipoAgente.Equals(Agent._cobradorSigla))
                        {
                            LogMessageAttention _logMessageAttention = new LogMessageAttention();
                            await _logMessageAttention.withoutAuthorizedToSign();
                        }
                    }

                    if (view.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_respetaLimiteCredito)
                    {
                        LogicaVisitaVerificaciones _logicaVerificaciones = new LogicaVisitaVerificaciones(view);

                        _verificarCredito = await _logicaVerificaciones.VerificarCreditoDisponible();
                    }

                    if (!_verificarCredito)
                    {
                        await _managerTransaccion.guardarTransaccion(null, view.controlador.v_objCliente);

                        pobjTransaccion.v_codDocumento = view.controlador.v_objCliente.v_objTransaccion.v_codDocumento;

                        Tramitar = _managerTransaccion.buscarIndicadorTramite();

                        if (Tramitar)
                        {
                            view.controlador.v_objCliente.v_objTransaccion.v_facturaTramitar = pobjTransaccion.v_codDocumento;
                        }
                    }

                    //Agregado
                    await SiGuardar(pobjTransaccion);

                    break;

                case ROLTransactions._devolucionSigla:

                    Logica_ManagerTransaccion _managerTransaccion2 = new Logica_ManagerTransaccion(view);

                    await _managerTransaccion2.guardarTransaccion(null, view.controlador.v_objCliente);

                    pobjTransaccion.v_codDocumento = view.controlador.v_objCliente.v_objTransaccion.v_codDocumento;

                    //Agregado
                    await SiGuardar(pobjTransaccion);

                    break;

                case ROLTransactions._regaliaSigla:

                    Logica_ManagerTransaccion _managerTransaccion3 = new Logica_ManagerTransaccion(view);

                    await _managerTransaccion3.guardarTransaccion(null, view.controlador.v_objCliente);

                    pobjTransaccion.v_codDocumento = view.controlador.v_objCliente.v_objTransaccion.v_codDocumento;

                    //Agregado
                    await SiGuardar(pobjTransaccion);

                    break;

                case ROLTransactions._cotizacionSigla:
                    Logica_ManagerTransaccion _managerTransaccion4 = new Logica_ManagerTransaccion(view);

                    await _managerTransaccion4.guardarTransaccion(null, view.controlador.v_objCliente);

                    pobjTransaccion.v_codDocumento = view.controlador.v_objCliente.v_objTransaccion.v_codDocumento;

                    //Agregado
                    await SiGuardar(pobjTransaccion);

                    break;

                case ROLTransactions._ordenVentaSigla:
                    Logica_ManagerTransaccion _managerTransaccion5 = new Logica_ManagerTransaccion(view);

                    await _managerTransaccion5.guardarTransaccion(null, view.controlador.v_objCliente);

                    pobjTransaccion.v_codDocumento = view.controlador.v_objCliente.v_objTransaccion.v_codDocumento;

                    //Agregado
                    await SiGuardar(pobjTransaccion);

                    break;

                case ROLTransactions._reciboDineroSigla:

                    bool _verificarFacturas = true;

                    if (_verificarFacturas)
                    {
                        ShowSROL _showRecibo = new ShowSROL();

                        _showRecibo.mostrarPantallaReciboDinero(view.controlador.v_objCliente, PantallasSistema._pantallaVisita, this);

                    }
                    else
                    {
                        LogMessageAttention logMessageAttention = new LogMessageAttention();
                        await logMessageAttention.generalAttention("El Cliente no posee facturas pendientes.");

                        //Agregado
                        await SiGuardar(pobjTransaccion);
                    }

                    break;

                case ROLTransactions._recaudacionSigla:

                    ShowSROL _showFormaPago2 = new ShowSROL();

                    _showFormaPago2.mostrarPantallaFormaPagoRecaudacion(
                                PantallasSistema._pantallaVisita,
                                view.controlador.v_objCliente,
                                view.controlador.v_motivoRecaudacion,
                                this
                                );

                    break;

                case ROLTransactions._tramiteSigla:

                    Logica_ManagerTramite _managerTramite = new Logica_ManagerTramite();

                    await _managerTramite.guardarTramite(view.controlador.v_objCliente);

                    pobjTransaccion.v_codDocumento = view.controlador.v_objCliente.v_objTransaccion.v_codDocumento;

                    //Agregado
                    await SiGuardar(pobjTransaccion);

                    break;

                default:
                    await LogMessageSuccess._generalSuccess(pobjTransaccion.v_objTipoDocumento.GetSigla());

                    //Agregado
                    await SiGuardar(pobjTransaccion);

                    break;
            }
        }

        public async Task SiGuardar(TransaccionEncabezado encabezado)
        {
            LogicaVisitaMenu logica = new LogicaVisitaMenu(view);

            await logica.EvaluarGuardadoDocumento(encabezado);
        }

        internal async Task GuardoDocumentoFallido()
        {
            if (!view.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._recaudacionNombre))
            {
                LogMessageAttention logMessageAttention = new LogMessageAttention();
                await logMessageAttention.generalAttention("No se guardó la transacción.");
            }
        }

        internal void setTransaccionEncabezado(TransaccionEncabezado pobjTransaccionEncabezado)
        {
            v_objTransaccionEncabezado = pobjTransaccionEncabezado;
        }

        public async void informacionDescargar()
        {
            try
            {
                var v_testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

                if (await v_testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
                {
                    Descarga_ManagerEnvio _manager = new Descarga_ManagerEnvio();

                    _manager.EnvioPaqueteInformacionPorWS(v_objTransaccionEncabezado);
                }
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        public async void informacionDescargarBitacora(pnlBitacoraModel pnlBitacora)
        {
            try
            {
                var v_testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

                if (await v_testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
                {
                    Descarga_ManagerEnvio _manager = new Descarga_ManagerEnvio();

                    _manager.EnvioPaqueteInformacionPorWS_Bitacora(pnlBitacora);
                }
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        internal async Task GuardoDocumentoExito(TransaccionEncabezado pobjTransaccion, string OrdenCompra)
        {
            Logica_ManagerTransaccion _managerTransaccion = new Logica_ManagerTransaccion(view);

            setTransaccionEncabezado(pobjTransaccion);

            if (pobjTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._facturaCreditoSigla))
            {
                Tramitar = _managerTransaccion.buscarIndicadorTramite();
            }

            var DPB = DependencyService.Get<ITaskActivity>();
            DPB.StartSendActivity(this);

            await LogMessageSuccess.successfulTransaction(pobjTransaccion.v_codDocumento);

            ProcesoImpresion _impresion = new ProcesoImpresion();

            _impresion.imprimirTransaccion(
                    view.controlador.v_objCliente,
                    pobjTransaccion.v_codDocumento,
                    pobjTransaccion.v_objTipoDocumento.GetSigla(),
                    pobjTransaccion.v_objTipoDocumento.GetNombre(),
                    false,
                    view.controlador.v_DevolucionFactura,
                    Tramitar
                    );
        }

        internal async Task RestauracionDeDatosYPantalla()
        {
            LogicaVisitaRender _logica = new LogicaVisitaRender(view);

            _logica.renderComponentesPnlTransacciones(false);

            LogicaVisitaComboBox _logicaVisitaComboBox = new LogicaVisitaComboBox(view);

            view.ConectarComboboxTipoTransaccion(false);

            _logicaVisitaComboBox.llenarComboBoxTipoTransaccion();

            view.ConectarComboboxTipoTransaccion(true);

            view.controlador.v_objCliente.v_objTransaccionTramite = new TransaccionEncabezado();

            await _logicaVisitaComboBox.filtrarComboBoxTipoTransaccionPorIndicadores();

            await view.controlador.pnlTransacciones_cbxTipoTransaccion_SelectedIndexChanged();

            view.controlador.COD_DOCUMENTO = string.Empty;

            view.controlador.COD_FACTURA = string.Empty;

            view.controlador.COD_PEDIDO = string.Empty;

            view.controlador.v_DevolucionFactura = false;

            view.controlador.v_PedidoBackOffice = false;

        }
    }
}
