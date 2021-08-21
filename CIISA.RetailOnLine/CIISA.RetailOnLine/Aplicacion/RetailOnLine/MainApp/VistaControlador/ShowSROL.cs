using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista.DevolucionFactura;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita.Guardar;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.Vista;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Display.View;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador
{
    public class ShowSROL
    {
        public Cliente v_objCliente = null;

        public void mostrarPantallaMenuPrincipal()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaMenu());
        }

        #region Rutero

        public void mostrarPantallaBitcora()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaBitacora());
        }

        public void mostrarPantallaRuta()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaRuta());
        }

        public void mostrarPantallaEntregaPedidos()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaEntregaPedidos());
        }

        public void mostrarPantallaCuentaCerrada()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaCuentaCerrada());
        }

        public void mostrarPantallaIndicadoresFacturacion()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaIndicadoresFacturacion());
        }

        public void mostrarPantallaCuentaBancaria()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaCuentaBancaria());
        }

        public void mostrarPantallaVisitaDesdeRuta()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaVisita(v_objCliente, false));
        }

        public void mostrarPantallaVisitaDesdeAnulacion()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaVisita(v_objCliente, true));
        }

        public void mostrarPantallaAnulaciones()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaAnulaciones());
        }

        public void mostrarPantallaVisita()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaVisita());
        }

        public void mostrarPantallaCliente(vistaCarniceria ppantalla)
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new vistaCliente(ppantalla, PantallasSistema._pantallaCarniceria)));
        }

        public void mostrarPantallaCliente(vistaVisita ppantalla)
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new vistaCliente(ppantalla, PantallasSistema._pantallaVisita)));
        }

        public void mostrarPantallaFactura(Cliente pobjCliente, string ppantallaInvoca, vistaRecibo pviewRecibo)
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaFacturaPendiente(pobjCliente, ppantallaInvoca, pviewRecibo));
        }

        public void mostrarPantallaVisitaSugerido(vistaVisita pviewVisita)
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new vistaVisitaSugerido(pviewVisita)));
        }

        public void MostrarPantallaVisitaEspecificacion(Producto pobjProducto, vistaVisita pviewVisita)
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new vistaVisitaEspecificacion(pobjProducto, pviewVisita)));
        }

        public void MostrarPantallaVisitaRegalia(Producto pobjProducto, vistaVisita pviewVista)
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new vistaVisitaRegalia(pobjProducto, pviewVista)));
        }

        public void MostrarPantallaVisitaRegalia(Producto pobjProducto, LogicaVisitaEventos pLogicaEventos)
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new vistaVisitaRegalia(pobjProducto, pLogicaEventos)));
        }

        public void mostrarPantallaVisitaDevolucion(Producto pobjProducto, bool pmodificarProducto, vistaVisita pviewVista)
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new vistaVisitaDevolucion(pobjProducto, pmodificarProducto, pviewVista)));
        }

        public void mostrarPantallaVisitaDevolucion(Producto pobjProducto, bool pmodificarProducto, LogicaVisitaEventos plogicaEventos)
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new vistaVisitaDevolucion(pobjProducto, pmodificarProducto, plogicaEventos)));
        }

        public void mostrarPantallaVisitaRecaudacion(Cliente pobjCliente, vistaVisita pviewVisita)
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaVisitaRecaudacion(pobjCliente, pviewVisita));
        }

        public void mostrarPantallaTramite(Cliente pobjCliente, vistaVisita pviewVisita)
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaTramite(pobjCliente, pviewVisita));
        }

        public void mostrarPantallaPedidos(Cliente pobjCliente, vistaVisita pviewVisita)
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaPedidos(pobjCliente, pviewVisita));
        }

        public void mostrarPantallaFormaPagoFacturaContado(vistaVisita vistaVisita, decimal ptotalSinIV, string ppantalla, string pcodTipoTransaccion, Cliente pobjCliente, LogicaVisitaGuardar logica)
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaFormaPago(vistaVisita ,ptotalSinIV, ppantalla, pcodTipoTransaccion, pobjCliente, logica));
        }

        public void mostrarPantallaReciboDinero(Cliente pobjCliente, string pantallaInvoca, LogicaVisitaGuardar logica)
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaRecibo(pobjCliente, pantallaInvoca, logica));
        }

        //public bool mostrarPantallaFormaPagoRecibo(string ppantalla, Cliente pobjCliente)
        public void mostrarPantallaFormaPagoRecibo(string ppantalla, Cliente pobjCliente, vistaRecibo pviewRecibo)
        {
            //bool _guardar = false;

            //using (vistaFormaPago _windowsForm = new vistaFormaPago(
            //                                            ppantalla,
            //                                            pobjCliente
            //                                            )
            //{ TopMost = true })
            //{
            //    _windowsForm.ShowDialog();
            //    _guardar = _windowsForm.controlador.v_guardar;
            //    _windowsForm.Close();
            //}

            Application.Current.MainPage.Navigation.PushAsync(new vistaFormaPago(ppantalla, pobjCliente, pviewRecibo));

            //return _guardar;
        }

        public void mostrarPantallaFormaPagoRecaudacion(string ppantalla, Cliente pobjCliente, string pmotivoRecaudacion, LogicaVisitaGuardar logica)
        {
            //using (vistaFormaPago _windowsForm = new vistaFormaPago(
            //                                            ppantalla,
            //                                            pobjCliente,
            //                                            pmotivoRecaudacion
            //                                            )
            //{ TopMost = true })
            //{
            //    _windowsForm.ShowDialog();
            //    _windowsForm.Close();
            //}
            Application.Current.MainPage.Navigation.PushAsync(new vistaFormaPago(ppantalla, pobjCliente, pmotivoRecaudacion, logica));
        }

        public void mostrarPantallaInventarioTeorico()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaInventarioTeorico());
        }

        public void mostrarPantallaInventarioPedidos(string pcodProducto)
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaInventarioPedidos(pcodProducto));
        }

        public void mostrarPantallaFlujoDinero()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaFlujoDinero());
        }

        public void mostrarPantallaClienteRecibo(vistaRecibo ppantalla)
        {
            //using (vistaCliente _windowsForm = new vistaCliente(PantallasSistema._pantallaRecibo) { TopMost = true })
            //{
            //    _windowsForm.ShowDialog();
            //    ppantalla.establecerVariablesCliente(_windowsForm.v_objCliente);
            //    _windowsForm.Close();
            //}

            Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new vistaCliente(PantallasSistema._pantallaRecibo, ppantalla)));
        }

        public void mostrarPantallaVisitaDesdeFacturacionFaltantes()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaVisita(v_objCliente, false, true));
        }

        #region Detalle Reses
        //Visita
        public void MostrarPantallaDetalleReses(Cliente cliente, Producto pobjProducto, LogicaCarniceriaEventos _logic, string pantalla, int NumLinea, string Cod_Doc, string CodPedido)
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaDetalleReses(cliente, pobjProducto, _logic, pantalla, NumLinea, Cod_Doc, CodPedido));
        }

        //Producto
        public void MostrarPantallaDetalleReses(Cliente cliente, string codProducto, LogicaCarniceriaEventos _logic, string pantalla, ctrlProducto ctrl, string codpedido)
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaDetalleReses(cliente, codProducto, _logic, pantalla, ctrl, codpedido));
        }
        #endregion

        public void MostrarPantallaVisitaCantidad(Producto pobjProducto, List<Producto> plistaProductoComprometido, LogicaVisitaEventos _logic)
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaVisitaCantidad(pobjProducto, plistaProductoComprometido,_logic));
        }

        public void mostrarPantallaPruebaPantalla()
        {
            Application.Current.MainPage.Navigation.PushAsync(new viewTestDisplay(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)));
        }

        public void mostrarPantallaTipoImpresion()
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaTipoImpresion());
        }

        public void mostrarPantallaProductoVisita(
            vistaVisita vista,
            string ppantallaInvoca,
            Cliente pobjCliente,
            string pnomTipoTransaccion,
            bool pshowControlBox,
            List<Producto> plistaProductoComprometido,
            string pcodigoUltimoProducto,
            LogicaVisitaEventos logicaVisitaEventos,
            List<string> ListaProductosVisita
            )
        {
            
            Application.Current.MainPage.Navigation
                .PushAsync(new vistaProducto(
                vista,
                ppantallaInvoca,
                pobjCliente,
                pnomTipoTransaccion,
                pshowControlBox,
                plistaProductoComprometido,
                pcodigoUltimoProducto,
                logicaVisitaEventos,
                ListaProductosVisita
            ));
        }

        public void mostrarPantallaDevolucionFactura(Cliente pobjCliente, vistaVisita pviewVisita)
        {
            Application.Current.MainPage.Navigation.PushAsync(new vistaDevolucionFactura(pobjCliente, pviewVisita));
        }

        #endregion
    }
}
