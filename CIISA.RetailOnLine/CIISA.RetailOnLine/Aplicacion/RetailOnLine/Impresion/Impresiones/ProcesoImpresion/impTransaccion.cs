using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Print;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion
{
    public class impTransaccion
    {
        #region Variables
        private ProcesoImpresion v_procesoImpresion { get; set; }
        public Cliente v_objCliente { get; set; }
        public string v_codTransaction { get; set; }
        string v_codTipoTransaccion { get; set; } 
        public string v_nomTipoTransaccion { get; set; }
        public bool v_preprint { get; set; }
        public FacturaElectronica v_facturaElectronica { get; set; }
        public bool DevolucionFactura { get; set; }
        public bool Tramitar { get; set; }
        private Cliente Cliente_Temporal = new Cliente();
        #endregion

        public impTransaccion(ProcesoImpresion v_procesoImpresion, Cliente v_objCliente, string v_codTransaction, string v_codTipoTransaccion, string v_nomTipoTransaccion, bool v_preprint, bool DevolucionFactura, bool Tramitar)
        {
            this.v_procesoImpresion = v_procesoImpresion;
            this.v_objCliente = v_objCliente;
            this.v_codTransaction = v_codTransaction;
            this.v_codTipoTransaccion = v_codTipoTransaccion;
            this.v_nomTipoTransaccion = v_nomTipoTransaccion;
            this.v_preprint = v_preprint;
            this.DevolucionFactura = DevolucionFactura;
            this.v_facturaElectronica = v_objCliente.v_objTransaccion.v_objFacturaElectronica;
            this.Tramitar = Tramitar;
        }

        public async void ImprimirProceso()
        {
            await imprimirTransaccion();

            if (Tramitar)
            {
                await CrearTramite();
            }
        }

        public async Task imprimirTransaccion()
        {
            
            int intentos = 1;
            LogMessageAttention _logMessageAttention = new LogMessageAttention();

            while (intentos <= 3)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                });

                try
                {
                    await v_procesoImpresion.v_impresora.connect();

                    if (v_procesoImpresion.ImpresoraFunciona())
                    {
                        intentos = 0;
                        break;
                    }
                    else
                    {
                        await _logMessageAttention.printerConnectionError(intentos);
                        intentos++;
                    }
                }
                catch (Exception)
                {
                    await _logMessageAttention.printerConnectionError(intentos);
                    intentos++;
                }
                finally
                {
                    UserDialogs.Instance.HideLoading();
                }
            }

            try
            {
                if (intentos > 3)
                {
                    await _logMessageAttention.generalAttention("Error con la impresora. No se puede realizar la impresión del documento.");
                }
                else
                {
                    SegmentoSupermercados _segmentoSupermercados = new SegmentoSupermercados();

                    _segmentoSupermercados.facturaOriginalParaCobro(
                        v_procesoImpresion.v_listaLineasImpresion,
                        v_codTipoTransaccion
                        );

                    SegmentoTitulo _segmentoTitulo = new SegmentoTitulo();

                    _segmentoTitulo.buscarTituloFacturaPorTipoDocumento(
                        v_procesoImpresion.v_listaLineasImpresion,
                        v_codTipoTransaccion
                        );

                    SegmentoEncabezado _segmentoEncabezado = new SegmentoEncabezado();

                    _segmentoEncabezado.subEncabezadoTransaccion(
                        v_procesoImpresion.v_listaLineasImpresion,
                        v_codTransaction,
                        v_nomTipoTransaccion,
                        100
                        );

                    SegmentoReimpresion _segmentoReimpresion = new SegmentoReimpresion();

                    _segmentoReimpresion.tipoTransaccionCodigoTransaccion(
                        v_procesoImpresion.v_listaLineasImpresion,
                        v_codTransaction,
                        v_nomTipoTransaccion,
                        v_preprint,
                        v_objCliente.v_no_agente
                        );

                    //COORPO
                    _segmentoEncabezado.SubEncabezadoTransaccionAdicionalesFE(
                        v_procesoImpresion.v_listaLineasImpresion,
                        v_codTransaction,
                        v_nomTipoTransaccion,
                        100,
                        v_objCliente,
                        v_facturaElectronica
                        );

                    if (v_codTipoTransaccion.Equals(ROLTransactions._facturaCreditoSigla))
                    {
                        SegmentoPlazo _segmentoPlazo = new SegmentoPlazo();

                        _segmentoPlazo.plazo(v_objCliente, v_procesoImpresion.v_listaLineasImpresion);
                    }

                    Logica_ManagerEncabezadoTransaccion _manager = new Logica_ManagerEncabezadoTransaccion();

                    DateTime _fechaHoraDocumento = _manager.buscarFechaHoraDocumento(
                                                        v_codTransaction,
                                                        v_codTipoTransaccion
                                                        );

                    TimeDate _timeDate = new TimeDate();

                    _timeDate.dateTimeTransaction(
                        v_procesoImpresion.v_listaLineasImpresion,
                        _fechaHoraDocumento,
                        v_preprint
                        );

                    SegmentoDatos _segmentoDatos = new SegmentoDatos();

                    _segmentoDatos.datosClienteTransaccion(
                        v_procesoImpresion.v_listaLineasImpresion,
                        v_objCliente,
                        v_codTipoTransaccion
                        );

                    Line _line = new Line();

                    _line.simpleHypenLine(
                        v_procesoImpresion.v_listaLineasImpresion
                        );

                    _segmentoDatos.datosAgenteTransaccion(
                        v_procesoImpresion.v_listaLineasImpresion,
                        v_codTipoTransaccion
                        );

                    _segmentoEncabezado.encabezadoTransaccion(
                        v_procesoImpresion.v_listaLineasImpresion,
                        v_codTipoTransaccion
                        );

                    _line.doubleHypenLine(
                        v_procesoImpresion.v_listaLineasImpresion
                        );

                    detallesTransaccion(
                        v_codTransaction,
                        v_nomTipoTransaccion,
                        v_codTipoTransaccion,
                        v_objCliente,
                        DevolucionFactura
                        );

                    SegmentoLeyenda _segmentoLeyenda = new SegmentoLeyenda();

                    //_segmentoLeyenda.pintarLeyendaProductosExentosIVA(
                    //    v_procesoImpresion.v_listaLineasImpresion,
                    //    v_codTipoTransaccion,
                    //    v_codTransaction,
                    //    v_nomTipoTransaccion
                    //    );

                    _segmentoSupermercados.facturaOriginalParaCobro(
                        v_procesoImpresion.v_listaLineasImpresion,
                        v_codTipoTransaccion
                        );

                    _segmentoLeyenda.pintarLeyendaRevisaProducto(
                        v_procesoImpresion.v_listaLineasImpresion,
                        v_codTipoTransaccion,
                        v_codTransaction,
                        v_nomTipoTransaccion
                        );

                    _segmentoEncabezado.encabezadoFormaPago(
                        v_procesoImpresion.v_listaLineasImpresion,
                        v_codTipoTransaccion
                        );

                    SegmentoFormaPago _segmentoFormaPago = new SegmentoFormaPago();

                    _segmentoFormaPago.formaPago(
                        v_procesoImpresion.v_listaLineasImpresion,
                        v_codTransaction,
                        v_codTipoTransaccion
                        );

                    SegmentoPie _segmentoPie = new SegmentoPie();

                    _segmentoPie.pieTransaccion(
                        v_procesoImpresion.v_listaLineasImpresion,
                        v_codTipoTransaccion,
                        v_codTransaction,
                        v_nomTipoTransaccion,
                        v_preprint
                        );

                    SegmentoFirma _segmentoFirma = new SegmentoFirma();

                    _segmentoFirma.firma(
                        v_procesoImpresion.v_listaLineasImpresion,
                        v_codTipoTransaccion
                        );

                    _segmentoSupermercados.atencionCambiosPrecios(
                        v_procesoImpresion.v_listaLineasImpresion,
                        v_codTipoTransaccion
                        );

                    _segmentoSupermercados.tipoTransaccionCodigoTransaccion(
                        v_procesoImpresion.v_listaLineasImpresion,
                        v_codTransaction,
                        v_codTipoTransaccion,
                        v_nomTipoTransaccion
                        );

                    LeyendaCopia _leyendaCopia = new LeyendaCopia(v_procesoImpresion);

                    await _leyendaCopia.enviarImprimirAgregarLeyendaCopia(
                        v_objCliente,
                        v_codTransaction,
                        v_codTipoTransaccion
                        );

                    v_procesoImpresion.v_impresora.disconnect();

                    Documents _documents = new Documents();

                    if (v_preprint)
                    {
                        _documents.printedDocumentsLog(
                            v_procesoImpresion.v_listaLineasImpresion,
                            Directorios.documentosReimpresos,
                            Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent
                            + " [" + v_codTipoTransaccion + "] "
                            + v_codTransaction
                            );
                    }
                    else
                    {
                        _documents.printedDocumentsLog(
                            v_procesoImpresion.v_listaLineasImpresion,
                            Directorios.documentosImpresos,
                            Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent
                            + " [" + v_codTipoTransaccion + "] "
                            + v_codTransaction
                            );
                    }

                    await LogMessageSuccess.printed();
                }
            }
            catch (Exception ex)
            {
                await _logMessageAttention.generalAttention("Ocurrio un error en el proceso de impresión: " + ex.Message);
            }
        }

        private void detallesTransaccion(string pcodTransaction,string pnomTipoTransaccion,string pcodTipoTransaccion,Cliente pobjCliente, bool DevolucionFactura)
        {
            if (pnomTipoTransaccion.Equals(ROLTransactions._ordenVentaNombre))
            {
                Impresion_ManagerDetalleTransaccion _manager = new Impresion_ManagerDetalleTransaccion();

                _manager.buscarLineasDetalleImpresion(
                   pcodTransaction,
                   pcodTipoTransaccion,
                   v_procesoImpresion.v_listaLineasImpresion,
                   pobjCliente
                   );
            }

            if (pnomTipoTransaccion.Equals(ROLTransactions._facturaContadoNombre))
            {
                Impresion_ManagerDetalleTransaccion _manager = new Impresion_ManagerDetalleTransaccion();

                _manager.buscarLineasDetalleImpresion(
                    pcodTransaction,
                    pcodTipoTransaccion,
                    v_procesoImpresion.v_listaLineasImpresion,
                    pobjCliente
                    );
            }

            if (pnomTipoTransaccion.Equals(ROLTransactions._facturaCreditoNombre))
            {
                Impresion_ManagerDetalleTransaccion _manager = new Impresion_ManagerDetalleTransaccion();

                _manager.buscarLineasDetalleImpresion(
                   pcodTransaction,
                   pcodTipoTransaccion,
                   v_procesoImpresion.v_listaLineasImpresion,
                   pobjCliente
                   );
            }

            if (pnomTipoTransaccion.Equals(ROLTransactions._cotizacionNombre))
            {
                Impresion_ManagerDetalleTransaccion _manager = new Impresion_ManagerDetalleTransaccion();

                _manager.buscarLineasDetalleImpresion(
                   pcodTransaction,
                   pcodTipoTransaccion,
                   v_procesoImpresion.v_listaLineasImpresion,
                   pobjCliente
                   );
            }

            if (pnomTipoTransaccion.Equals(ROLTransactions._devolucionNombre))
            {
                Impresion_ManagerDetalleTransaccion _manager = new Impresion_ManagerDetalleTransaccion();

                _manager.buscarLineasDetalleImpresion(
                   pcodTransaction,
                   pcodTipoTransaccion,
                   v_procesoImpresion.v_listaLineasImpresion,
                   pobjCliente,
                   DevolucionFactura
                   );
            }

            if (pnomTipoTransaccion.Equals(ROLTransactions._regaliaNombre))
            {
                Impresion_ManagerDetalleTransaccion _manager = new Impresion_ManagerDetalleTransaccion();

                _manager.buscarLineasDetalleImpresion(
                   pcodTransaction,
                   pcodTipoTransaccion,
                   v_procesoImpresion.v_listaLineasImpresion,
                   pobjCliente
                   );
            }

            Line _line = new Line();

            if (pnomTipoTransaccion.Equals(ROLTransactions._recaudacionNombre))
            {
                Impresion_ManagerEncabezadoRecibo _manager = new Impresion_ManagerEncabezadoRecibo();

                _line.printingLinesList(
                    v_procesoImpresion.v_listaLineasImpresion,
                    _manager.buscarLineasEncabezado(pcodTransaction, pcodTipoTransaccion),
                    1
                    );
            }

            if (pnomTipoTransaccion.Equals(ROLTransactions._reciboDineroNombre))
            {
                Impresion_ManagerDetalleRecibo _manager = new Impresion_ManagerDetalleRecibo();

                _line.printingLinesList(
                    v_procesoImpresion.v_listaLineasImpresion,
                    _manager.buscarLineasDetalle(pcodTransaction, pcodTipoTransaccion),
                    0
                    );
            }

            if (pnomTipoTransaccion.Equals(ROLTransactions._tramiteNombre))
            {
                Impresion_ManagerEncabezadoTramite _managerEncabezado = new Impresion_ManagerEncabezadoTramite();

                DateTime _fecha = _managerEncabezado.buscarLineasDetalle(
                    pcodTransaction
                    );

                Impresion_ManagerDetalleTramite _managerDetalle = new Impresion_ManagerDetalleTramite();

                _line.printingLinesList(
                    v_procesoImpresion.v_listaLineasImpresion,
                    _managerDetalle.buscarLineasDetalle(pcodTransaction, _fecha),
                    1
                    );
            }

        }

        /// <summary>
        /// Metodo que permite reinicializar las lineas de impresión a su vez que genera un tramite
        /// </summary>
        /// <returns></returns>
        private async Task CrearTramite()
        {
            v_procesoImpresion.v_listaLineasImpresion = new List<string>();
            Logica_ManagerTramite _managerTramite2 = new Logica_ManagerTramite();
            Cliente_Temporal = new Cliente(v_objCliente);

            string _facturaTramitar = v_objCliente.v_objTransaccion.v_facturaTramitar;
            Cliente_Temporal.v_objTransaccion = new TransaccionEncabezado(v_objCliente.v_objTransaccion);
            Cliente_Temporal.v_objTransaccion.v_facturaTramitar = _facturaTramitar;
            Cliente_Temporal.v_objTransaccion.v_objTipoDocumento.SetSigla(ROLTransactions._tramiteSigla);
            Cliente_Temporal.v_objTransaccion.v_tramite = SQL._Si;

            await _managerTramite2.guardarTramite(Cliente_Temporal);

            Cliente_Temporal.v_objTransaccion.v_codDocumento = Cliente_Temporal.v_objTransaccion.v_codDocumento;
            this.v_codTipoTransaccion = Cliente_Temporal.v_objTransaccion.v_objTipoDocumento.GetSigla();
            this.v_codTransaction = Cliente_Temporal.v_objTransaccion.v_codDocumento;
            this.v_nomTipoTransaccion = Cliente_Temporal.v_objTransaccion.v_objTipoDocumento.GetNombre();
            await imprimirTransaccion();
        }

    }
}
