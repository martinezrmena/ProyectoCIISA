using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.External.CustomListview;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador.Carniceria
{
    public class ctrlDetalleReses
    {
        internal vistaDetalleReses view = null;
        internal Cliente cliente = null;
        internal Producto producto = null;
        internal string pantalla = null;
        public Decimal PESOTOTAL { get; set; }
        internal bool cerrar = false;
        internal LogicaCarniceriaEventos logicaVisitaEventos = null;
        internal int NumLinea { get; set; }
        internal string COD_DOCUMENTO { get; set; }
        internal string COD_PEDIDO { get; set; }
        internal ctrlProducto ctrlProd = null;
        internal string CodProducto = null;
        internal LogMessageAttention _logMessageAttention = new LogMessageAttention();

        //Desde visita
        public ctrlDetalleReses(
            vistaDetalleReses pview,
            Cliente _cliente,
            Producto _producto,
            LogicaCarniceriaEventos logica,
            string _pantalla,
            int _NumLinea,
            string Cod_doc,
            string CodPedido) {

            view = pview;
            cliente = _cliente;
            producto = _producto;
            pantalla = _pantalla;
            logicaVisitaEventos = logica;
            NumLinea = _NumLinea;
            COD_DOCUMENTO = Cod_doc;
            COD_PEDIDO = CodPedido;

        }

        //Desde producto
        public ctrlDetalleReses(
            vistaDetalleReses pview,
            Cliente _cliente,
            string codproducto,
            LogicaCarniceriaEventos logica,
            string _pantalla,
            ctrlProducto ctrl,
            string codpedido)
        {

            view = pview;
            cliente = _cliente;
            CodProducto = codproducto;
            pantalla = _pantalla;
            logicaVisitaEventos = logica;
            ctrlProd = ctrl;
            this.COD_PEDIDO = codpedido;

        }

        internal void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlDetalleReses"));

            RenderListView();

            menu_mniSumar_Click();
        }

        public void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlDetalleReses"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlDetalleReses").Id))
            {
                view.Title = "Detalle Reses";
            }

            ppanel.IsVisible = true;
        }

        /// <summary>
        /// Metodo encargado de establecer los datos en el listview principal de la vista
        /// la misma se dispara de diferente manera dependiendo de la pantalla que inicialmente
        /// mostros estas funcionalidades
        /// </summary>
        public async void RenderListView() {

            view.FindByName<ListView>("pnlDetalleReses_ltvReses").ItemsSource = new SelectableObservableCollection<pnlTransacciones_ltvDetalleReses>();

            Logica_ManagerCarniceria manager = new Logica_ManagerCarniceria();

            if (pantalla == PantallasSistema._pantallaVisita)
            {
                if (!manager.buscarDetallesResesProducto(
                            view.FindByName<ListView>("pnlDetalleReses_ltvReses"),
                            producto.v_codProducto,
                            cliente.v_no_cliente,
                            this.COD_PEDIDO))
                {
                    //SI NO HAY NADA EN LA LISTA MOSTRAMOS EL MENSAJE Y CERRAMOS LA PANTALLA
                    await _logMessageAttention.generalAttention("El inventario disponible para este artículo se encuentra comprometido.");
                    Cerrar();
                }
            }
            else if(pantalla == PantallasSistema._pantallaProducto) {

                if (!manager.buscarDetalleResesProductoDisponible(
                            view.FindByName<ListView>("pnlDetalleReses_ltvReses"), CodProducto, cliente.v_no_cliente))
                {
                    //SI NO HAY NADA EN LA LISTA MOSTRAMOS EL MENSAJE Y CERRAMOS LA PANTALLA
                    await _logMessageAttention.generalAttention("El inventario disponible para este artículo se encuentra comprometido .");
                    Cerrar();
                }
            }
            

        }

        /// <summary>
        /// Metodo encargado de sumar todos los pesos de los detalles seleccionados
        /// para poder visualizar el total que se procesará
        /// </summary>
        internal void menu_mniSumar_Click()
        {
            decimal _s = Numeric._zeroDecimalInitialize;

            var Source = view.FindByName<ListView>("pnlDetalleReses_ltvReses").ItemsSource as SelectableObservableCollection<pnlTransacciones_ltvDetalleReses>;

            foreach (var _lvi in Source)
            {
                if (_lvi.IsSelected)
                {
                    _s = _s + _lvi.Data._vc_peso;
                }
            }

            PESOTOTAL = _s;

            view.FindByName<Label>("pnlDetalleReses_lblSuma").Text = "Peso total: " + FormatUtil.applyCurrencyFormat(_s);

            view.FindByName<ListView>("pnlDetalleReses_ltvReses").ItemsSource = Source;
        }

        /// <summary>
        /// Metodo encargado de marcar o desmarcar todos los detalles de la lista
        /// dependiendo de la propia marcación del checkbox que posee este metodo
        /// </summary>
        public void pnlDetalleReses_chkTodas_Checked()
        {
            var Source = view.FindByName<ListView>("pnlDetalleReses_ltvReses").ItemsSource as SelectableObservableCollection<pnlTransacciones_ltvDetalleReses>;

            foreach (var _lvi in Source)
            {
                _lvi.IsSelected = view.FindByName<CheckBox>("pnlDetalleReses_chkTodas").Checked;
            }

            view.FindByName<ListView>("pnlDetalleReses_ltvReses").ItemsSource = Source;

            menu_mniSumar_Click();

        }

        internal void pnlDetalleReses_ltvReses_ItemSelected() {

            menu_mniSumar_Click();

        }

        /// <summary>
        /// Metodo principal que dispara la reasignación de peso para el detalle seleccionado
        /// desde visita, con la finalidad de establecer el peso total que se haya establecido
        /// en esta pantalla
        /// </summary>
        /// <returns></returns>
        internal async Task Procesar() {

            if (await LogMessages._dialogResultYes("¿Desea proceder a guardar la información actual?", "Alerta"))
            {
                if (pantalla == PantallasSistema._pantallaVisita)
                {
                    var Source = view.FindByName<ListView>("pnlDetalleReses_ltvReses").ItemsSource as SelectableObservableCollection<pnlTransacciones_ltvDetalleReses>;

                    foreach (var _lvi in Source)
                    {
                        bool acu = false;

                        //Extraemos el valor desde la consulta para saber si se modifico
                        if (_lvi.Data._vc_comprometido.Equals(SQL._Si))
                        {
                            acu = true;
                        }

                        //comparamos con el checkbox en caso de que se modificara
                        if (_lvi.IsSelected != acu)
                        {
                            //Si se modifico entonces actualizar desde la BD
                            Logica_ManagerCarniceria logica = new Logica_ManagerCarniceria();
                            logica.CambiarReservado(_lvi.Data._vc_consecutivo, _lvi.IsSelected);
                        }
                    }

                    view.FindByName<ListView>("pnlDetalleReses_ltvReses").ItemsSource = Source;

                }

                menu_mniSumar_Click();

                cerrar = true;

                Cerrar();
            }
        }

        internal void Cerrar()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        /// <summary>
        /// Metodo que se encarga de controlar las acciones especificas que se dispararan al cerrar
        /// la pantalla, dependiendo de la pantalla que haya realizado su llamado inicialmente
        /// </summary>
        /// <returns></returns>
        internal async Task Closing()
        {
            if (cerrar)
            {
                //El flujo puede venir de visita, producto o anulacion
                if (pantalla == PantallasSistema._pantallaVisita)
                {
                    producto.v_cantTransaccion = PESOTOTAL;
                    await logicaVisitaEventos.pnlTransacciones_ltvProductos_ItemActivateParte2(true, producto);
                    Logica_ManagerDetallePedido logica = new Logica_ManagerDetallePedido();
                    logica.actualizarDetallePedido(producto, COD_DOCUMENTO);
                }
                else if (pantalla == PantallasSistema._pantallaProducto)
                {
                    await logicaVisitaEventos.pnlProductos_ltvProductos_Parte2(PESOTOTAL, ctrlProd);
                    //Es necesario reasignar el estado del detalle res a este nuevo cliente
                    //Comprometido si
                    ReasignarProductoSeleccionado();

                    //Es necesario guardar ese pedido en visita
                    logicaVisitaEventos.viewP.controlador.GuadarPedidoDR = true;
                }

            }
        }

        /// <summary>
        /// Se encarga de reasignar el producto seleccionado a otro cliente diferente al original establecido
        /// por web services, este proceso se dispara unicamente desde la pantalla producto
        /// </summary>
        internal void ReasignarProductoSeleccionado() {

            var Source = view.FindByName<ListView>("pnlDetalleReses_ltvReses").ItemsSource as SelectableObservableCollection<pnlTransacciones_ltvDetalleReses>;

            Logica_ManagerCarniceria logica = new Logica_ManagerCarniceria();

            foreach (var _lvi in Source)
            {
                //comparamos con el checkbox en caso de que se modificara
                if (_lvi.IsSelected)
                {
                    if (pantalla == PantallasSistema._pantallaProducto)
                    {
                        //Es necesario agregar el detalle res al producto para reasignarlo posteriormente al pedido
                        logicaVisitaEventos.viewP.controlador.ListaDetallesResesProductos.Add(_lvi.Data);
                    }

                    //Si se modifico entonces actualizar desde la BD                    
                    logica.ReasignarDetalleRes(_lvi.Data._vc_consecutivo, cliente.v_no_cliente);
                }
            }
        }

    }
}
