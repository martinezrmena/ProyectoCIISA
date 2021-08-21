using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador;
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
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.CargaDiaria.Modelo
{
    public class Carga_ManagerRecargaDiaria
    {
        private Carga_ManagerDatosTabla v_manager = null;

        public Carga_ManagerRecargaDiaria()
        {
            v_manager = new Carga_ManagerDatosTabla();
        }

        public async Task recargaDiariaTablaSistema(Log plog,Editor ptextBox,Label plabel)
        {
            await v_manager.cargaInformacionSistema(
                TablesROL._sistema,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaAgenteVendedor(Log plog,Editor ptextBox,Label plabel)
        {
            await v_manager.cargaInformacionAgenteVendedor(
                TablesROL._agenteVendedor,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaEncabezadoPedido(Log plog,Editor ptextBox,Label plabel)
        {
            await v_manager.cargaInformacionEncabezadoPedido(
                TablesROL._encabezadoPedido,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaDetallePedido(Log plog,Editor ptextBox,Label plabel)
        {
            await v_manager.cargaInformacionDetallePedido(
                TablesROL._detallePedido,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaClienteFacturacionFaltantes(string pcodCliente,Log plog,Editor ptextBox,Label plabel)
        {
            await v_manager.cargaInformacionClienteFacturacionFaltantes(
                TablesROL._cliente,
                ptextBox,
                plabel,
                pcodCliente,
                plog
                );
        }

        public async Task recargaDiariaTablaEstablecimientoFacturacionFaltantes(string pcodCliente,Log plog,Editor ptextBox,Label plabel)
        {
            await v_manager.cargaInformacionEstablecimientoFacturacionFaltantes(
                pcodCliente,
                TablesROL._establecimiento,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaVisitaFacturacionFaltantes(string pcodCliente,Log plog,Editor ptextBox,Label plabel)
        {
            await v_manager.cargaInformacionVisitaFacturacionFaltantes(
                pcodCliente,
                TablesROL._visitas,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaIndicadorFacturaFacturacionFaltantes(string pcodCliente,Log plog,Editor ptexbox,Label plabel)
        {
            await v_manager.cargaInformacionIndicadorFacturaFacturacionFaltantes(
                pcodCliente,
                TablesROL._indicadorFactura,
                ptexbox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaClave(Log plog,Editor ptextBox,Label plabel)
        {
            await v_manager.cargaInformacionClave(
                TablesROL._clave,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaAutorizadoFirmar(Log plog,Editor ptextBox,Label plabel)
        {
            await v_manager.cargaInformacionAutorizadoFirmar(
                TablesROL._autorizadoFirmar,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaCliente(Log plog,Editor ptextBox,Label plabel)
        {
            await v_manager.cargaInformacionCliente(
                TablesROL._cliente,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaDescuentos(Log plog,Editor ptextBox,Label plabel)
        {
            await v_manager.cargaInformacionDescuento(
                TablesROL._descuentos,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaDescuentosGeneral(Log plog, Editor ptextBox, Label plabel)
        {
            await v_manager.cargaInformacionDescuentoGeneral(
                TablesROL._descuentoGeneral,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaEmbalaje(Log plog,Editor ptextBox,Label plabel)
        {
            await v_manager.cargaInformacionEmbalaje(
                TablesROL._embalaje,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaEstablecimiento(Log plog,Editor ptextBox,Label plabel)
        {
            await v_manager.cargaInformacionEstablecimiento(
                TablesROL._establecimiento,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaFactura(Log plog,Editor ptextBox,Label plabel,bool pcondicionCarga,int ptipoCarga)
        {
            await v_manager.cargaInformacionFactura(
                TablesROL._factura,
                ptextBox,
                plabel,
                pcondicionCarga,
                ptipoCarga,
                plog
                );
        }

        public async Task recargaDiariaTablaIndicadorFactura(Log plog,Editor ptexbox,Label plabel)
        {
            await v_manager.cargaInformacionIndicadorFactura(
                TablesROL._indicadorFactura,
                ptexbox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaInventario(Log plog,Editor ptextBox,Label plabel,bool pcondicionCarga,int ptipoCarga)
        {
            await v_manager.cargaInformacionInventario(
                TablesROL._inventario,
                ptextBox,
                plabel,
                pcondicionCarga,
                ptipoCarga,
                plog
                );
        }

        public async Task recargaDiariaTablaImpresora(Log plog,Editor ptextBox,Label plabel)
        {
            await v_manager.cargaInformacionImpresora(
                TablesROL._impresora,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaListaPrecios(Log plog,Editor ptextBox,Label plabel)
        {
            await v_manager.cargaInformacionListaPrecios(
                TablesROL._listaPrecios,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaPrecioCliente(Log plog,Editor ptextBox,Label plabel)
        {
            await v_manager.cargaInformacionPrecioCliente(
                TablesROL._precioCliente,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaPrecioProducto(Log plog,Editor ptextBox,Label plabel)
        {
            await v_manager.cargaInformacionPrecioProducto(
                TablesROL._precioProducto,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaProducto(Log plog,Editor ptextBox,Label plabel)
        {
            await v_manager.cargaInformacionProducto(
                TablesROL._producto,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaSugerido(Log plog,Editor ptextBox,Label plabel)
        {
            await v_manager.cargaInformacionSugerido(
                TablesROL._sugerido,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaVisita(Log plog,Editor ptextBox,Label plabel)
        {
            await v_manager.cargaInformacionVisita(
                TablesROL._visitas,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaDetalleReses(Log plog, Editor ptextBox, Label plabel)
        {
            await v_manager.cargaInformacionDetalleReses(
                TablesROL._DetalleReses,
                ptextBox,
                plabel,
                plog
                );
        }

        public async Task recargaDiariaTablaMensajeFactura(Log plog, Editor ptextBox, Label plabel)
        {
            await v_manager.cargaInformacionMensajeFactura(
                TablesROL._MensajeFactura,
                ptextBox,
                plabel,
                plog
                );
        }

        public void renderComponentesTablas(List<string> Source) {
            Source.Add(TablesROL._autorizadoFirmar);
            Source.Add(TablesROL._clave);
            Source.Add(TablesROL._cliente);
            Source.Add(TablesROL._descuentos);
            Source.Add(TablesROL._descuentoGeneral);
            Source.Add(TablesROL._detallePedido);
            Source.Add(TablesROL._embalaje);
            Source.Add(TablesROL._encabezadoPedido);
            Source.Add(TablesROL._factura);
            Source.Add(TablesROL._indicadorFactura);
            Source.Add(TablesROL._inventario);
            Source.Add(TablesROL._impresora);
            Source.Add(TablesROL._listaPrecios);
            Source.Add(TablesROL._precioCliente);
            Source.Add(TablesROL._precioProducto);
            Source.Add(TablesROL._producto);
            Source.Add(TablesROL._sistema);
            Source.Add(TablesROL._sugerido);
            Source.Add(TablesROL._visitas);
            Source.Add(TablesROL._DetalleReses);
            Source.Add(TablesROL._MensajeFactura);
        }

        public async Task recargaDiaria(ctrlIdentificarUsuario controlador, bool paperturaNocturna,Editor ptextBox,Label plabel,Log plog, String Message)
        {
            List<string> Source = new List<string>();
            Logica_ManagerAgenteVendedor _managerAgenteVendedor = new Logica_ManagerAgenteVendedor();
            string _codCliente = _managerAgenteVendedor.obtenerCodigoClienteAgenteVendedor();
            var v_testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();
            MainApp.VistaControlador.GUIAvanceManual ControlAdvance = new MainApp.VistaControlador.GUIAvanceManual(controlador);
            var Notification = DependencyService.Get<IAndroidMethods>();
            LogMessageAttention _logMessageAttention = new LogMessageAttention();
            string Result = string.Empty;
            List<string> TablasConError = new List<string>();
            int i = 1;

            plog.addLine(ptextBox, "PROCESO DE RECARGA DIARIA DE INFORMACIÓN");

            renderComponentesTablas(Source);

            if (await v_testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
            {
                foreach (var _lvi in Source)
                {
                    try
                    {
                        await ControlAdvance.actualizarGUIdesdeHilo_progresoInsercion(i, Source.Count, _lvi);

                        if (_lvi.Equals(TablesROL._autorizadoFirmar))
                        {
                            await recargaDiariaTablaAutorizadoFirmar(plog, ptextBox, plabel);
                        }

                        if (_lvi.Equals(TablesROL._clave))
                        {
                            await recargaDiariaTablaClave(plog, ptextBox, plabel);
                        }

                        if (_lvi.Equals(TablesROL._cliente))
                        {
                            await recargaDiariaTablaCliente(plog, ptextBox, plabel);

                            await recargaDiariaTablaClienteFacturacionFaltantes(_codCliente, plog, ptextBox, plabel);
                        }

                        if (_lvi.Equals(TablesROL._descuentos))
                        {
                            await recargaDiariaTablaDescuentos(plog, ptextBox, plabel);
                        }

                        if (_lvi.Equals(TablesROL._descuentoGeneral))
                        {
                            await recargaDiariaTablaDescuentosGeneral(plog, ptextBox, plabel);
                        }

                        if (_lvi.Equals(TablesROL._detallePedido))
                        {
                            await recargaDiariaTablaDetallePedido(plog, ptextBox, plabel);
                        }

                        if (_lvi.Equals(TablesROL._embalaje))
                        {
                            await recargaDiariaTablaEmbalaje(plog, ptextBox, plabel);
                        }

                        if (_lvi.Equals(TablesROL._encabezadoPedido))
                        {
                            await recargaDiariaTablaEncabezadoPedido(plog, ptextBox, plabel);
                        }

                        if (_lvi.Equals(TablesROL._establecimiento))
                        {
                            await recargaDiariaTablaEstablecimiento(plog, ptextBox, plabel);

                            await recargaDiariaTablaEstablecimientoFacturacionFaltantes(_codCliente, plog, ptextBox, plabel);
                        }

                        if (_lvi.Equals(TablesROL._factura))
                        {
                            await recargaDiariaTablaFactura(plog, ptextBox, plabel, false, VariableCarga._recargaDiaria);
                        }

                        if (_lvi.Equals(TablesROL._indicadorFactura))
                        {
                            await recargaDiariaTablaIndicadorFactura(plog, ptextBox, plabel);

                            await recargaDiariaTablaIndicadorFacturaFacturacionFaltantes(_codCliente, plog, ptextBox, plabel);
                        }

                        if (_lvi.Equals(TablesROL._inventario))
                        {
                            await recargaDiariaTablaInventario(plog, ptextBox, plabel, false, VariableCarga._recargaDiaria);
                        }

                        if (_lvi.Equals(TablesROL._impresora))
                        {
                            await recargaDiariaTablaImpresora(plog, ptextBox, plabel);
                        }

                        if (_lvi.Equals(TablesROL._listaPrecios))
                        {
                            await recargaDiariaTablaListaPrecios(plog, ptextBox, plabel);
                        }

                        if (_lvi.Equals(TablesROL._precioCliente))
                        {
                            await recargaDiariaTablaPrecioCliente(plog, ptextBox, plabel);
                        }

                        if (_lvi.Equals(TablesROL._precioProducto))
                        {
                            await recargaDiariaTablaPrecioProducto(plog, ptextBox, plabel);
                        }

                        if (_lvi.Equals(TablesROL._producto))
                        {
                            await recargaDiariaTablaProducto(plog, ptextBox, plabel);
                        }

                        if (_lvi.Equals(TablesROL._sistema))
                        {
                            if (paperturaNocturna)
                            {
                                await recargaDiariaTablaSistema(plog, ptextBox, plabel);
                            }
                        }

                        if (_lvi.Equals(TablesROL._sugerido))
                        {
                            await recargaDiariaTablaSugerido(plog, ptextBox, plabel);
                        }

                        if (_lvi.Equals(TablesROL._visitas))
                        {
                            await recargaDiariaTablaVisita(plog, ptextBox, plabel);

                            await recargaDiariaTablaVisitaFacturacionFaltantes(_codCliente, plog, ptextBox, plabel);
                        }

                        if (_lvi.Equals(TablesROL._DetalleReses))
                        {
                            await recargaDiariaTablaDetalleReses(plog, ptextBox, plabel);
                        }

                        if (_lvi.Equals(TablesROL._MensajeFactura))
                        {
                            await recargaDiariaTablaMensajeFactura(plog, ptextBox, plabel);
                        }

                    }
                    catch (Exception)
                    {
                        TablasConError.Add(_lvi);
                    }

                    i++;

                }

            }

            plog.addLine(ptextBox,"FIN DEL PROCESO DE RECARGA INFORMACIÓN");

            if (TablasConError.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("Se finalizó la apertura nocturna." + 
                    Environment.NewLine +
                    "El proceso termino con errores en las siguientes tablas: ");

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
                Result = "El proceso termino exitosamente.";
            }

            Notification.PushNotification(Message, Result);
        }
    }
}
