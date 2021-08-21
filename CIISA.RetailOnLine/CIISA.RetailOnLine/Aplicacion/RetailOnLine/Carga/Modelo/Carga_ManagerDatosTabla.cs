using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.ControladorCargaDatos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.HelperCargaDatos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.External.CustomListview;
using CIISA.RetailOnLine.Framework.Handheld.Display.IMainLockScreen;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Modelo
{
    public class Carga_ManagerDatosTabla
    {
        internal ITestConnectionSROL v_testConnectionSROL = null;
        internal HelperCargaDatosTabla v_helperCargaDatosTabla = null;

        public Carga_ManagerDatosTabla()
        {
            v_helperCargaDatosTabla = new HelperCargaDatosTabla();
            v_testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();
        }

        public async Task cargaInformacionSistema(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaSistema _carga = new ctrlCarga_DatosTablaSistema(this);

            await _carga.cargaInformacionSistema(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task cargaInformacionAgenteVendedor(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaAgenteVendedor _carga = new ctrlCarga_DatosTablaAgenteVendedor(this);
            await _carga.cargaInformacionAgenteVendedor(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }

        private async Task cargaInformacionBanco(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaBanco _carga = new ctrlCarga_DatosTablaBanco(this);
            await _carga.cargaInformacionBanco(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }

        private async Task cargaInformacionCanton(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaCanton _carga = new ctrlCarga_DatosTablaCanton(this);
            await _carga.cargaInformacionCanton(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task cargaBorradoTablas(ListView plistView,bool pcondicionCarga,int ptipoCarga, Editor ptextBox,Label plabel,Log plog, ctrlCarga controlador)
        {
            var v_testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();
            GUIAvanceManual ControlAdvance = new GUIAvanceManual(controlador);
            var Notification = DependencyService.Get<IAndroidMethods>();
            LogMessageAttention _logMessageAttention = new LogMessageAttention();
            String Message = String.Empty;
            string Result = string.Empty;
            List<string> TablasConError = new List<string>();

            if (await v_testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
            {
                var Source = plistView.ItemsSource as SelectableObservableCollection<string>;
                int i = 1;

                foreach (var _lvi in Source)
                {
                    await ControlAdvance.actualizarGUIdesdeHilo_progresoInsercion(i, Source.Count, _lvi.Data);

                    string _table = _lvi.Data;

                    try
                    {
                        if (_lvi.Data.Equals(TablesROL._agenteVendedor))
                        {
                            await cargaInformacionAgenteVendedor(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._autorizadoFirmar))
                        {
                            await cargaInformacionAutorizadoFirmar(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._banco))
                        {
                            await cargaInformacionBanco(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._canton))
                        {
                            await cargaInformacionCanton(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._clasificacionCliente))
                        {
                            await cargaInformacionClasificacionCliente(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._clave))
                        {
                            await cargaInformacionClave(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._cliente))
                        {
                            await cargaInformacionCliente(_table, ptextBox, plabel, plog);

                            Logica_ManagerAgenteVendedor _managerAgenteVendedor = new Logica_ManagerAgenteVendedor();

                            await cargaInformacionClienteFacturacionFaltantes(
                                _table,
                                ptextBox,
                                plabel,
                                _managerAgenteVendedor.obtenerCodigoClienteAgenteVendedor(),
                                plog
                                );
                        }
                        if (_lvi.Data.Equals(TablesROL._compannia))
                        {
                            await cargaInformacionCompannia(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._cuentaCerrada))
                        {
                            await cargaInformacionCuentaCerrada(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._descuentos))
                        {
                            await cargaInformacionDescuento(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._descuentoGeneral))
                        {
                            await cargaInformacionDescuentoGeneral(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._detallePedido))
                        {
                            await cargaInformacionDetallePedido(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._distrito))
                        {
                            await cargaInformacionDistrito(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._embalaje))
                        {
                            await cargaInformacionEmbalaje(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._encabezadoPedido))
                        {
                            await cargaInformacionEncabezadoPedido(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._especificacion))
                        {
                            await cargaInformacionEspecificacion(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._establecimiento))
                        {
                            await cargaInformacionEstablecimiento(_table, ptextBox, plabel, plog);

                            Logica_ManagerAgenteVendedor _managerAgenteVendedor = new Logica_ManagerAgenteVendedor();

                            await cargaInformacionEstablecimientoFacturacionFaltantes(
                                _managerAgenteVendedor.obtenerCodigoClienteAgenteVendedor(),
                                _table,
                                ptextBox,
                                plabel,
                                plog
                                );
                        }
                        if (_lvi.Data.Equals(TablesROL._factura))
                        {
                            await cargaInformacionFactura(_table, ptextBox, plabel, pcondicionCarga, ptipoCarga, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._fechaServidor))
                        {
                            await cargaInformacionFechaServidor(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._formaPago))
                        {
                            await cargaInformacionFormaPago(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._impresora))
                        {
                            await cargaInformacionImpresora(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._indicadorFactura))
                        {
                            await cargaInformacionIndicadorFactura(_table, ptextBox, plabel, plog);

                            Logica_ManagerAgenteVendedor _managerAgenteVendedor = new Logica_ManagerAgenteVendedor();

                            await cargaInformacionIndicadorFacturaFacturacionFaltantes(
                                _managerAgenteVendedor.obtenerCodigoClienteAgenteVendedor(),
                                _table,
                                ptextBox,
                                plabel,
                                plog
                                );
                        }
                        if (_lvi.Data.Equals(TablesROL._informacionRuta))
                        {
                            await cargaInformacionInformacionRuta(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._inventario))
                        {
                            await cargaInformacionInventario(_table, ptextBox, plabel, pcondicionCarga, ptipoCarga, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._listaPrecios))
                        {
                            await cargaInformacionListaPrecios(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._motivo))
                        {
                            await cargaInformacionMotivo(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._pais))
                        {
                            await cargaInformacionPais(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._precioCliente))
                        {
                            await cargaInformacionPrecioCliente(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._precioProducto))
                        {
                            await cargaInformacionPrecioProducto(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._producto))
                        {
                            await cargaInformacionProducto(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._provincia))
                        {
                            await cargaInformacionProvincia(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._razonesNV))
                        {
                            await cargaInformacionRazonesNV(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._ruta))
                        {
                            await cargaInformacionRuta(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._sistema))
                        {
                            await cargaInformacionSistema(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._sms))
                        {
                            await cargaInformacionSms(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._sugerido))
                        {
                            await cargaInformacionSugerido(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._tipoIdentificacion))
                        {
                            await cargaInformacionTipoIdentificacion(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._tipoTransaccion))
                        {
                            await cargaInformacionTipoTransaccion(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._tituloComprobante))
                        {
                            await cargaInformacionTituloComprobante(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._visitas))
                        {
                            await cargaInformacionVisita(_table, ptextBox, plabel, plog);

                            Logica_ManagerAgenteVendedor _managerAgenteVendedor = new Logica_ManagerAgenteVendedor();

                            await cargaInformacionVisitaFacturacionFaltantes(
                                _managerAgenteVendedor.obtenerCodigoClienteAgenteVendedor(),
                                _table,
                                ptextBox,
                                plabel,
                                plog
                                );
                        }
                        if (_lvi.Data.Equals(TablesROL._DetalleReses))
                        {
                            await cargaInformacionDetalleReses(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._MensajeFactura))
                        {
                            await cargaInformacionMensajeFactura(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._Exoneracion))
                        {
                            await cargaExoneraciones(_table, ptextBox, plabel, plog);
                        }
                        if (_lvi.Data.Equals(TablesROL._TiposIVA))
                        {
                            await cargaTiposIVA(_table, ptextBox, plabel, plog);
                        }
                    }
                    catch (Exception ex)
                    {
                        TablasConError.Add(_lvi.Data);
                    }
                    
                    i++;

                }

                if(TablasConError.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("El proceso termino con errores en las siguientes tablas: ");

                    foreach (var item in TablasConError)
                    {
                        sb.AppendLine(" ■ " + item);
                    }

                    sb.AppendLine("Se recomienda hacer el proceso de recarga de dichas tablas.");
                    Message = sb.ToString();
                    Result = "El proceso termino con errores.";                  
                }
                else
                {
                    Message = "El proceso termino exitosamente.";
                    Result = "El proceso termino exitosamente.";
                }

                Notification.PushNotification(Message, Result);
            }

        }
        public async Task cargaInformacionEncabezadoPedido(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaEncabezadoPedido _carga = new ctrlCarga_DatosTablaEncabezadoPedido(this);
            await _carga.cargaInformacionEncabezadoPedido(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionDetallePedido(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaDetallePedido _carga = new ctrlCarga_DatosTablaDetallePedido(this);
            await _carga.cargaInformacionDetallePedido(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionClienteFacturacionFaltantes(string ptable,Editor ptextBox,Label plabel,string pcodCliente,Log plog)
        {
            ctrlCarga_DatosTablaCliente _carga = new ctrlCarga_DatosTablaCliente(this);
            await _carga.cargaInformacionClienteFacturacionFaltantes(
                ptable,
                ptextBox,
                plabel,
                pcodCliente,
                plog
                );
        }
        public async Task cargaInformacionEstablecimientoFacturacionFaltantes(string pcodCliente,string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaEstablecimiento _carga = new ctrlCarga_DatosTablaEstablecimiento(this);
            await _carga.cargaInformacionEstablecimientoFacturacionFaltantes(
                pcodCliente,
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionVisitaFacturacionFaltantes(string pcodCliente,string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaVisita _carga = new ctrlCarga_DatosTablaVisita(this);
            await _carga.cargaInformacionVisitaFacturacionFaltantes(
                pcodCliente,
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionIndicadorFacturaFacturacionFaltantes(string pcodCliente,string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaIndicadorFactura _carga = new ctrlCarga_DatosTablaIndicadorFactura(this);
            await _carga.cargaInformacionIndicadorFacturaFacturacionFaltantes(
                pcodCliente,
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionClave(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaClave _carga = new ctrlCarga_DatosTablaClave(this);
            await _carga.cargaInformacionClave(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionAutorizadoFirmar(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosAutorizadoFirmar _carga = new ctrlCarga_DatosAutorizadoFirmar(this);
            await _carga.cargaInformacionAutorizadoFirmar(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionCliente(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaCliente _carga = new ctrlCarga_DatosTablaCliente(this);
            await _carga.cargaInformacionCliente(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionDescuento(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaDescuento _carga = new ctrlCarga_DatosTablaDescuento(this);
            await _carga.cargaInformacionDescuento(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionDescuentoGeneral(string ptable, Editor ptextBox, Label plabel, Log plog)
        {
            ctrlCarga_DatosTablaDescuentoGeneral _carga = new ctrlCarga_DatosTablaDescuentoGeneral(this);
            await _carga.cargaInformacionDescuentoGeneral(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionDetalleReses(string ptable, Editor ptextBox, Label plabel, Log plog)
        {
            ctrlCarga_DatosTablaDetalleReses _carga = new ctrlCarga_DatosTablaDetalleReses(this);
            await _carga.cargaInformacionDetalleReses(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionMensajeFactura(string ptable, Editor ptextBox, Label plabel, Log plog)
        {
            ctrlCarga_DatosTablaMensajeFactura _carga = new ctrlCarga_DatosTablaMensajeFactura(this);
            await _carga.cargaInformacionMensajeFactura(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaExoneraciones(string ptable, Editor ptextBox, Label plabel, Log plog)
        {
            ctrlCarga_DatosTablaExoneracion _carga = new ctrlCarga_DatosTablaExoneracion(this);
            await _carga.cargaInformacionExoneracion(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaTiposIVA(string ptable, Editor ptextBox, Label plabel, Log plog)
        {
            ctrlCarga_DatosTablaTipoIva _carga = new ctrlCarga_DatosTablaTipoIva(this);
            await _carga.cargaInformacionTipoIVA(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionEmbalaje(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaEmbalaje _carga = new ctrlCarga_DatosTablaEmbalaje(this);
            await _carga.cargaInformacionEmbalaje(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionEstablecimiento(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaEstablecimiento _carga = new ctrlCarga_DatosTablaEstablecimiento(this);
            await _carga.cargaInformacionEstablecimiento(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionFactura(string ptable,Editor ptextBox,Label plabel,bool pcondicionCarga,int ptipoCarga,Log plog)
        {
            ctrlCarga_DatosTablaFactura _carga = new ctrlCarga_DatosTablaFactura(this);
            await _carga.cargaInformacionFactura(
                ptable,
                ptextBox,
                plabel,
                ptipoCarga,
                plog
                );
        }
        public async Task cargaInformacionIndicadorFactura(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaIndicadorFactura _carga = new ctrlCarga_DatosTablaIndicadorFactura(this);
            await _carga.cargaInformacionIndicadorFactura(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionInventario(string ptable,Editor ptextBox,Label plabel,bool pcondicionCarga,int ptipoCarga,Log plog)
        {
            ctrlCarga_DatosTablaInventario _carga = new ctrlCarga_DatosTablaInventario(this);
            await _carga.cargaInformacionInventario(
                ptable,
                ptextBox,
                plabel,
                pcondicionCarga,
                plog
                );
        }
        public async Task cargaInformacionImpresora(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaImpresora _carga = new ctrlCarga_DatosTablaImpresora(this);
            await _carga.cargaInformacionImpresora(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionListaPrecios(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaListaPrecios _carga = new ctrlCarga_DatosTablaListaPrecios(this);
            await _carga.cargaInformacionListaPrecios(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionPrecioCliente(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaPrecioCliente _carga = new ctrlCarga_DatosTablaPrecioCliente(this);
            await _carga.cargaInformacionPrecioCliente(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionPrecioProducto(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaPrecioProducto _carga = new ctrlCarga_DatosTablaPrecioProducto(this);
            await _carga.cargaInformacionPrecioProducto(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionProducto(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaProducto _carga = new ctrlCarga_DatosTablaProducto(this);
            await _carga.cargaInformacionProducto(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionSugerido(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaSugerido _carga = new ctrlCarga_DatosTablaSugerido(this);
            await _carga.cargaInformacionSugerido(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionVisita(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaVisita _carga = new ctrlCarga_DatosTablaVisita(this);
            await _carga.cargaInformacionVisita(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        private async Task cargaInformacionPais(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaPais _carga = new ctrlCarga_DatosTablaPais(this);
            await _carga.cargaInformacionPais(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        private async Task cargaInformacionProvincia(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaProvincia _carga = new ctrlCarga_DatosTablaProvincia(this);
            await _carga.cargaInformacionProvincia(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        private async Task cargaInformacionDistrito(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaDistrito _carga = new ctrlCarga_DatosTablaDistrito(this);
            await _carga.cargaInformacionDistrito(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        private async Task cargaInformacionTipoIdentificacion(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaTipoIdentificacion _carga = new ctrlCarga_DatosTablaTipoIdentificacion(this);
            await _carga.cargaInformacionTipoIdentificacion(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        private async Task cargaInformacionTipoTransaccion(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaTipoTransaccion _carga = new ctrlCarga_DatosTablaTipoTransaccion(this);
            await _carga.cargaInformacionTipoTransaccion(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        private async Task cargaInformacionTituloComprobante(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaTituloComprobante _carga = new ctrlCarga_DatosTablaTituloComprobante(this);
            await _carga.cargaInformacionTituloComprobante(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionFechaServidor(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaFechaServidor _carga = new ctrlCarga_DatosTablaFechaServidor(this);
            await _carga.cargaInformacionFechaServidor(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        private async Task cargaInformacionFormaPago(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaFormaPago _carga = new ctrlCarga_DatosTablaFormaPago(this);
            await _carga.cargaInformacionFormaPago(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionInformacionRuta(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaInformacionRuta _carga = new ctrlCarga_DatosTablaInformacionRuta(this);
            await _carga.cargaInformacionInformacionRuta(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        private async Task cargaInformacionClasificacionCliente(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaClasificacionCliente _carga = new ctrlCarga_DatosTablaClasificacionCliente(this);
            await _carga.cargaInformacionClasificacionCliente(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        private async Task cargaInformacionCompannia(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaCompannia _carga = new ctrlCarga_DatosTablaCompannia(this);
            await _carga.cargaInformacionCompannia(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        private async Task cargaInformacionCuentaCerrada(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaCuentaCerrada _carga = new ctrlCarga_DatosTablaCuentaCerrada(this);
            await _carga.cargaInformacionCuentaCerrada(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        private async Task cargaInformacionEspecificacion(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaEspecificacion _carga = new ctrlCarga_DatosTablaEspecificacion(this);
            await _carga.cargaInformacionEspecificacion(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        private async Task cargaInformacionMotivo(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaMotivo _carga = new ctrlCarga_DatosTablaMotivo(this);
            await _carga.cargaInformacionMotivo(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        private async Task cargaInformacionRazonesNV(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaRazonesNV _carga = new ctrlCarga_DatosTablaRazonesNV(this);
            await _carga.cargaInformacionRazonesNV(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        private async Task cargaInformacionRuta(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaRuta _carga = new ctrlCarga_DatosTablaRuta(this);
            await _carga.cargaInformacionRuta(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
        public async Task cargaInformacionSms(string ptable,Editor ptextBox,Label plabel,Log plog)
        {
            ctrlCarga_DatosTablaSms _carga = new ctrlCarga_DatosTablaSms(this);
            await _carga.cargaInformacionSms(
                ptable,
                ptextBox,
                plabel,
                plog
                );
        }
    }
}
