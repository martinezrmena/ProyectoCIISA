using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    public class ctrlProducto
    {
        private vistaProducto view { get; set; }

        private string v_pantallaInvoca = string.Empty;
        private string v_nomTipoTransaccion = string.Empty;
        private Cliente v_objCliente = null;
        private List<Producto> v_listaProductoComprometido = null;
        private LogicaVisitaEventos v_logivaVisitaEventos = null;
        public string v_tipoBusqueda = string.Empty;
        public string v_filtro = string.Empty;
        private bool cerrar = false;
        internal LogMessageAttention _logMessageAttention = new LogMessageAttention();
        public vistaVisita vistaVisita = null;
        public HelperDetallePedido helperDetallePedido = new HelperDetallePedido();
        public bool GuadarPedidoDR { get; set; }
        public string TipoAgente { get; set; }
        public List<string> ListaProductosVisita = new List<string>();
        public List<pnlTransacciones_ltvDetalleReses> ListaDetallesResesProductos { get; set; } = new List<pnlTransacciones_ltvDetalleReses>();

        internal ctrlProducto(vistaVisita _vista, vistaProducto pview)
        {
            vistaVisita = _vista;
            view = pview;
            GuadarPedidoDR = false;
        }

        internal ctrlProducto(vistaProducto pview)
        {
            view = pview;
        }

        public void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlProductos").Id))
            {
                view.Title = "Productos";
            }

            ppanel.IsVisible = true;
        }

        private void renderMenu()
        {
            TipoAgente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._typeAgent;

            if (!v_pantallaInvoca.Equals(PantallasSistema._pantallaVisita))
            {
                view.FindByName<StackLayout>("pnlProductos_stckBuscar").IsVisible = false;
            }
        }

        public void actualizarColumnasNumeroLineas()
        {
            MiscUtils _miscUtils = new MiscUtils();
            _miscUtils.quantityListViewItems<pnlProductos_ltvProductos>(view.FindByName<ListView>("pnlProductos_ltvProductos"), view.FindByName<Label>("pnlProductos_clhCodigo"), "Código");
        }

        private void llenarComboBoxEspecificacion(Picker pcomboBox)
        {
            Logica_ManagerEspecificacion _manager = new Logica_ManagerEspecificacion();

            DataTable _dt = _manager.buscarEspecificacion();

            Util _util = new Util();

            _util.fillComboBox(
                _dt,
                pcomboBox,
                "Descripcion"
                );
        }

        public void renderComponents()
        {
            actualizarColumnasNumeroLineas();

            Util _util = new Util();

            _util.fillComboBoxSearchForCodeAndDescription(view.FindByName<Picker>("pnlProductos_cbxTipoBusqueda"));

            llenarComboBoxEspecificacion(view.FindByName<Picker>("pnlProductos_cbxEspecificacion"));

            if (!v_nomTipoTransaccion.Equals(ROLTransactions._ordenVentaNombre))
            {
                view.FindByName<Picker>("pnlProductos_cbxEspecificacion").IsEnabled = false;
            }

            view.FindByName<ExtendedEntry>("pnlProductos_txtBuscar").Focus();

            if (!v_pantallaInvoca.Equals(PantallasSistema._pantallaVisita))
            {
                RenderHiden.hideLabel(view.FindByName<Label>("pnlProductos_lblEspecificacion"));
                RenderHiden.hideButton(view.FindByName<Picker>("pnlProductos_cbxEspecificacion"));
                RenderHiden.hideTextBox(view.FindByName<ExtendedEntry>("pnlProductos_txtEspecificacion"));

                RenderHiden.hideLabel(view.FindByName<Label>("pnlProductos_lblCantidadProducto"));
                RenderHiden.hideTextBox(view.FindByName<ExtendedEntry>("pnlProductos_txtCantidad"));
                RenderHiden.hideButton(view.FindByName<Button>("pnlProductos_btnPuntoDecimal"));
            }

            if (!v_pantallaInvoca.Equals(string.Empty))
            {
                view.FindByName<Picker>("pnlProductos_cbxEmbalaje").IsEnabled = false;
            }
        }

        private void constructor(bool pshowControlBox, string pcodigoUltimoProducto)
        {
            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlProductos"));

            renderComponents();

            view.v_objProducto = new Producto();

            view.FindByName<ExtendedEntry>("pnlProductos_txtBuscar").Focus();

            renderMenu();

            view.FindByName<ExtendedEntry>("pnlProductos_txtBuscar").Text = pcodigoUltimoProducto;
        }

        internal void ScreenInicialization(string ppantallaInvoca, Cliente pobjCliente, string pnomTipoTransaccion, bool pshowControlBox, List<Producto> plistaProductoComprometido, string pcodigoUltimoProducto,LogicaVisitaEventos plogicaVisitaEventos, List<string> ListaProductosVisita)
        {
            v_logivaVisitaEventos = plogicaVisitaEventos;
            v_nomTipoTransaccion = pnomTipoTransaccion;
            v_pantallaInvoca = ppantallaInvoca;
            v_objCliente = pobjCliente;
            v_listaProductoComprometido = plistaProductoComprometido;
            this.ListaProductosVisita = ListaProductosVisita;

            constructor(pshowControlBox, pcodigoUltimoProducto);
        }

        internal void ScreenInicialization(string ppantallaInvoca, bool pshowControlBox)
        {
            v_pantallaInvoca = ppantallaInvoca;

            constructor(pshowControlBox, string.Empty);
        }

        internal void pnlProductos_cbxEspecificacion_SelectedIndexChanged()
        {
            if (view.FindByName<Picker>("pnlProductos_cbxEspecificacion").SelectedItem.ToString().Equals(EspecificacionProducto._otroNombre))
            {
                view.FindByName<ExtendedEntry>("pnlProductos_txtEspecificacion").IsEnabled = true;
                RenderPaint.paintWhiteBackgroundTextBox(view.FindByName<ExtendedEntry>("pnlProductos_txtEspecificacion"));
                view.FindByName<ExtendedEntry>("pnlProductos_txtEspecificacion").Focus();
            }
            else
            {
                view.FindByName<ExtendedEntry>("pnlProductos_txtEspecificacion").IsEnabled = false;
                view.FindByName<ExtendedEntry>("pnlProductos_txtEspecificacion").Text = string.Empty;
                RenderPaint.paintGrayBackgroundTextBox(view.FindByName<ExtendedEntry>("pnlProductos_txtEspecificacion"));
            }
        }

        internal void pnlProductos_cbxTipoBusqueda_SelectedIndexChanged()
        {
            view.FindByName<ExtendedEntry>("pnlProductos_txtBuscar").Text = string.Empty;
            if (view.FindByName<Picker>("pnlProductos_cbxTipoBusqueda").SelectedIndex == 0)
            {
                view.FindByName<ExtendedEntry>("pnlProductos_txtBuscar").Keyboard = Keyboard.Numeric;
            }
            else
            {
                view.FindByName<ExtendedEntry>("pnlProductos_txtBuscar").Keyboard = Keyboard.Text;
            }
            view.FindByName<ExtendedEntry>("pnlProductos_txtBuscar").Focus();
        }

        private void limpiarPantalla()
        {
            view.FindByName<ListView>("pnlProductos_ltvProductos").ItemsSource = new ObservableCollection<pnlProductos_ltvProductos>();
        }

        private void llenarComboBoxEmbalaje(DataTable pdt, Picker pcomboBox)
        {
            Util _util = new Util();

            _util.fillComboBox(
                pdt,
                pcomboBox,
                "Embalaje"
                );
        }

        private void cargarEmbalajePosicionarCursorEnCantidad()
        {
            var _lvi = view.FindByName<ListView>("pnlProductos_ltvProductos").SelectedItem as pnlProductos_ltvProductos;

            if (_lvi != null)
            {
                string _codProducto = _lvi.Codigo;

                Logica_ManagerEmbalaje _manager = new Logica_ManagerEmbalaje();

                DataTable _dt = _manager.buscarEmbalajePorArticulo(_codProducto);

                if (_dt.Rows != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        view.FindByName<Picker>("pnlProductos_cbxEmbalaje").IsEnabled = true;
                        view.FindByName<Picker>("pnlProductos_cbxEmbalaje").IsVisible = true;
                        llenarComboBoxEmbalaje(_dt, view.FindByName<Picker>("pnlProductos_cbxEmbalaje"));
                    }
                    else
                    {
                        view.FindByName<Picker>("pnlProductos_cbxEmbalaje").IsEnabled = false;
                        view.FindByName<Picker>("pnlProductos_cbxEmbalaje").IsVisible = false;
                        view.FindByName<Picker>("pnlProductos_cbxEmbalaje").Items.Clear();
                    }
                }
            }

            view.FindByName<ExtendedEntry>("pnlProductos_txtCantidad").Focus();
        }

        internal void menu_mniBuscar_Click()
        {
            limpiarPantalla();

            v_tipoBusqueda = view.FindByName<Picker>("pnlProductos_cbxTipoBusqueda").SelectedItem.ToString();
            v_filtro = view.FindByName<ExtendedEntry>("pnlProductos_txtBuscar").Text;

            Logica_ManagerProducto _managerProducto = new Logica_ManagerProducto();

            _managerProducto.buscarListaProducto(
                    view.FindByName<ListView>("pnlProductos_ltvProductos"),
                    v_nomTipoTransaccion,
                    v_filtro,
                    v_tipoBusqueda,
                    v_listaProductoComprometido
                    );

            actualizarColumnasNumeroLineas();

            ValidateHH _validateHH = new ValidateHH();

            _validateHH.emptyListView<pnlProductos_ltvProductos>(view.FindByName<ListView>("pnlProductos_ltvProductos"));

            var Source = view.FindByName<ListView>("pnlProductos_ltvProductos").ItemsSource as ObservableCollection<pnlProductos_ltvProductos>;
            var Seleccionado = view.FindByName<ListView>("pnlProductos_ltvProductos").SelectedItem as pnlProductos_ltvProductos;

            if (Source.Count == 1)
            {
                foreach (var _lvi in Source)
                {
                    if (_lvi == Seleccionado)
                    {
                        cargarEmbalajePosicionarCursorEnCantidad();
                    }
                }
            }
        }

        private async Task<bool> validarFormulario()
        {
            bool _formulario = false;

            ValidateHH _validateHH = new ValidateHH();

            bool _ltvSeleccionado = await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlProductos_ltvProductos"));
            var Objeto = view.FindByName<ListView>("pnlProductos_ltvProductos").SelectedItem as pnlProductos_ltvProductos;

            if (_ltvSeleccionado)
            {
                bool _txtVacio = _validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlProductos_txtCantidad"));

                if (_txtVacio == false)
                {
                    bool _cantMayorQueCero = _validateHH.amountGreaterThanZero(view.FindByName<ExtendedEntry>("pnlProductos_txtCantidad"));
                    if (_cantMayorQueCero)
                    {
                        _formulario = true;
                    }
                }
            }

            if (ListaProductosVisita != null && Objeto != null)
            {
                if (ListaProductosVisita.Where(x => x == Objeto.Codigo).Count() > 0)
                {
                    //El elemento ya existe en la lista de productos
                    _formulario = false;
                    await _logMessageAttention.generalAttention("Estimado usuario, no es posible agregar el producto porque ya se encuentra dentro de la lista de visita.");
                }
            }

            return _formulario;
        }

        private void agregarEspecificacionComentario()
        {
            if (v_nomTipoTransaccion.Equals(ROLTransactions._ordenVentaNombre))
            {
                if (view.FindByName<Picker>("pnlProductos_cbxEspecificacion").SelectedItem.ToString().Equals(EspecificacionProducto._otroNombre))
                {
                    view.v_objProducto.v_especificacionOV =
                        view.FindByName<ExtendedEntry>("pnlProductos_txtEspecificacion").Text;
                }
                else
                {
                    view.v_objProducto.v_especificacionOV = view.FindByName<Picker>("pnlProductos_cbxEspecificacion").SelectedItem.ToString();
                }
            }
            else
            {
                view.v_objProducto.v_especificacionOV = string.Empty;
            }
        }

        /// <summary>
        /// Metodo encargado de realizar la gestión correspondiente a la facturación de visceras
        /// </summary>
        /// <param name="pobjproducto"></param>
        private async Task agregarProducto(Producto pobjproducto)
        {
            var Seleccionado = view.FindByName<ListView>("pnlProductos_ltvProductos").SelectedItem as pnlProductos_ltvProductos;

            view.v_objProducto = pobjproducto;

            view.v_objProducto.v_cantTransaccion = FormatUtil.convertStringToDecimal(view.FindByName<ExtendedEntry>("pnlProductos_txtCantidad").Text);

            view.v_objProducto.v_descripcion = Seleccionado.Descripcion;

            view.v_objProducto.v_descripcionConsultado = true;

            view.v_objProducto.v_exento = Seleccionado.Exento.Equals(Variable._true);

            view.v_objProducto.v_exentoConsultado = true;

            view.v_objProducto.v_objProductoInventario.v_disponible = FormatUtil.convertStringToDecimal(Seleccionado.Disponible);

            view.v_objProducto.v_objProductoInventario.v_disponibleConsultado = true;

            agregarEspecificacionComentario();

            if (view.FindByName<Picker>("pnlProductos_cbxEmbalaje").IsVisible)
            {
                view.v_objProducto.v_embalaje = FormatUtil.convertStringToDecimal(view.FindByName<Picker>("pnlProductos_cbxEmbalaje").SelectedItem.ToString());
            }
            else
            {
                view.v_objProducto.v_embalaje = Numeric._zeroDecimalInitialize;
            }

            //hay que validar unicamente a los productos que son visceras
            Logica_ManagerProducto _managerProducto = new Logica_ManagerProducto();

            if (_managerProducto.buscarEsViscera(view.v_objProducto.v_codProducto))
            {
                if (v_nomTipoTransaccion.Equals(ROLTransactions._facturaContadoNombre) ||
                    v_nomTipoTransaccion.Equals(ROLTransactions._facturaCreditoNombre))
                {
                    //Debe crearse un metodo que identifique que indicador de los
                    //detalles reses se apega
                    FacturacionVisceras facturacionVisceras = new FacturacionVisceras(vistaVisita, v_objCliente);

                    pnlFacturacionVisceras visceraSeleccionada = new pnlFacturacionVisceras();

                    bool continuar = true;

                    do
                    {

                        if (facturacionVisceras.CodProductosVisita.Count > 0)
                        {
                            visceraSeleccionada =
                                        await facturacionVisceras.RecorrerAsignacionesDR(
                                            facturacionVisceras.CodProductosVisita.First());
                        }
                        else
                        {
                            visceraSeleccionada = null;
                        }

                        if (facturacionVisceras.CodProductosVisita.Count == 0 ||
                            facturacionVisceras.detallesReses.Count > 0)
                        {
                            continuar = false;
                        }

                    } while (continuar);

                    agregarProductoParte2(visceraSeleccionada);
                }
                else
                {
                    agregarProductoParte2(null);
                }
            }
            else
            {
                agregarProductoParte2(null);
            }

        }

        public void agregarProductoParte2(pnlFacturacionVisceras visceras)
        {
            if (visceras != null)
            {
                view.v_objProducto.EsViscera = true;

                if (!string.IsNullOrEmpty(visceras.TIPOVICERAS))
                {
                    view.v_objProducto.TipoPorcion = visceras.TIPOVICERAS;
                    view.v_objProducto.ConsecutivoDReses = visceras.DETALLERES._vc_consecutivo;
                }
            }

            cerrar = true;
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task menu_mniAgregar_Click()
        {
            bool _formulario = await validarFormulario();

            if (_formulario)
            {
                var Seleccionado = view.FindByName<ListView>("pnlProductos_ltvProductos").SelectedItem as pnlProductos_ltvProductos;

                Producto _objProducto = new Producto();

                _objProducto.v_codProducto = Seleccionado.Codigo;

                _objProducto.CodCliente = v_objCliente.v_no_cliente;

                decimal _cantInventario = _objProducto.v_objProductoInventario.Disponible(_objProducto.v_codProducto);

                decimal _cantProductoComprometido = Numeric._zeroDecimalInitialize;

                UtilLogica _util = new UtilLogica();

                _cantProductoComprometido = _util.obtenerCantidadProductoComprometido(
                                                            _objProducto.v_codProducto,
                                                            v_listaProductoComprometido
                                                            );

                decimal _cantDisponible = _cantInventario - _cantProductoComprometido;

                bool _cantidad = false;

                if (!v_nomTipoTransaccion.Equals(ROLTransactions._ordenVentaNombre))
                {
                    if (!v_nomTipoTransaccion.Equals(ROLTransactions._cotizacionNombre))
                    {
                        if (!v_nomTipoTransaccion.Equals(ROLTransactions._devolucionNombre))
                        {
                            ValidateHH _validateHH = new ValidateHH();

                            _cantidad = await _validateHH.quantityFewerThanRange(
                                            view.FindByName<ExtendedEntry>("pnlProductos_txtCantidad"),
                                            _cantDisponible,
                                            v_nomTipoTransaccion
                                            );
                        }
                        else
                        {
                            _cantidad = true;
                        }
                    }
                    else
                    {
                        _cantidad = true;
                    }
                }
                else
                {
                    _cantidad = true;
                }

                bool _especificacion = false;

                if (v_nomTipoTransaccion.Equals(ROLTransactions._ordenVentaNombre))
                {
                    if (view.FindByName<Picker>("pnlProductos_cbxEspecificacion").IsVisible)
                    {
                        if (view.FindByName<Picker>("pnlProductos_cbxEspecificacion").SelectedItem.ToString().Equals(EspecificacionProducto._otroNombre))
                        {
                            ValidateHH _validateHH = new ValidateHH();

                            if (!_validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlProductos_txtEspecificacion")))
                            {
                                _especificacion = true;
                            }
                        }
                        else
                        {
                            _especificacion = true;
                        }
                    }
                    else
                    {
                        _especificacion = true;
                    }
                }
                else
                {
                    _especificacion = true;
                }

                if ((v_pantallaInvoca.Equals(PantallasSistema._pantallaVisita)) && (_cantidad) && !v_nomTipoTransaccion.Equals(ROLTransactions._ordenVentaNombre) && _especificacion)
                {
                    await agregarProducto(_objProducto);
                }

                if ((v_pantallaInvoca.Equals(PantallasSistema._pantallaVisita)) && v_nomTipoTransaccion.Equals(ROLTransactions._ordenVentaNombre) && _especificacion)
                {
                    await agregarProducto(_objProducto);
                }
            }
        }

        internal void pnlProductos_btnBorrarBuscar_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlProductos_txtBuscar"));
        }

        internal void pnlProductos_btnLimpiarBuscar_Click()
        {
            view.FindByName<ExtendedEntry>("pnlProductos_txtBuscar").Text = string.Empty;

            view.FindByName<ExtendedEntry>("pnlProductos_txtBuscar").Focus();
        }

        internal async Task pnlProductos_btnInventarioPedidos_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            bool _itemSeleccionado = await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlProductos_ltvProductos"));

            string _codProducto = string.Empty;

            if (_itemSeleccionado)
            {
                var Seleccionado = view.FindByName<ListView>("pnlProductos_ltvProductos").SelectedItem as pnlProductos_ltvProductos;
                _codProducto = Seleccionado.Codigo;
            }

            ShowSROL _show = new ShowSROL();

            _show.mostrarPantallaInventarioPedidos(_codProducto);
        }

        internal async Task pnlProductos_btnCopiarCantidad_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlProductos_ltvProductos")))
            {
                var Seleccionado = view.FindByName<ListView>("pnlProductos_ltvProductos").SelectedItem as pnlProductos_ltvProductos;

                view.FindByName<ExtendedEntry>("pnlProductos_txtCantidad").Text = Seleccionado.Disponible;
            }
        }

        internal void pnlProductos_btnBorrarCantidad_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlProductos_txtCantidad"));
        }

        internal void pnlProductos_btnPuntoDecimal_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.punto(view.FindByName<ExtendedEntry>("pnlProductos_txtCantidad"));
        }

        internal async Task pnlProductos_ctxtMnu_mitAyuda_Click()
        {
            await HelpFB.closeProductWindow();
        }

        private async Task seleccionarProducto()
        {
            if (!v_pantallaInvoca.Equals(PantallasSistema._pantallaVisita))
            {
                ValidateHH _validateHH = new ValidateHH();

                if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlProductos_ltvProductos")))
                {
                    var Seleccionado = view.FindByName<ListView>("pnlProductos_ltvProductos").SelectedItem as pnlProductos_ltvProductos;
                    view.v_objProducto.v_codProducto = Seleccionado.Codigo;
                    cerrar = true;
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
            }

            if (!v_pantallaInvoca.Equals(string.Empty))
            {
                cargarEmbalajePosicionarCursorEnCantidad();
            }
        }

        private async Task ComportamientoListaProductos()
        {
            await seleccionarProducto();
        }

        internal async Task pnlProductos_ltvBuscarProducto_ItemActivate()
        {
            await ComportamientoListaProductos();
        }

        internal void pnlProductos_txtBuscar_Focus(bool focus)
        {
            if (focus)
            {
                view.FindByName<ExtendedEntry>("pnlProductos_txtBuscar").Focus();
            }
            else
            {
                view.FindByName<ExtendedEntry>("pnlProductos_txtBuscar").Unfocus();
            }
        }

        internal void pnlProductos_txtCantidad_Focus(bool focus)
        {
            if (focus)
            {
                view.FindByName<ExtendedEntry>("pnlProductos_txtCantidad").Focus();
            }
            else
            {
                view.FindByName<ExtendedEntry>("pnlProductos_txtCantidad").Unfocus();
            }
        }

        /// <summary>
        /// Este metodo se dispará al seleccionar el campo que contiene la cantidad
        /// que se asignara al producto, en caso de que el documento corresponda a una
        /// factura será necesario realizar diversas validaciones relativas a detalles reses
        /// </summary>
        internal async Task FocusProducto() {

            //validar si el producto posee detalle reses disponibles que no esten reservados
            //ES NECESARIO VALIDAR EL TIPO DE DOCUMENTO
            if (v_nomTipoTransaccion.Equals(ROLTransactions._facturaContadoNombre) ||
                v_nomTipoTransaccion.Equals(ROLTransactions._facturaCreditoNombre))
            {
                ValidateHH _validateHH = new ValidateHH();

                if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlProductos_ltvProductos")))
                {
                    var Seleccionado = view.FindByName<ListView>("pnlProductos_ltvProductos").SelectedItem as pnlProductos_ltvProductos;

                    Logica_ManagerCarniceria logica = new Logica_ManagerCarniceria();

                    //ES NECESARIO VALIDAR SI EL PRODUCTO POSEE DETALLE RESES
                    if (logica.EsDetalleRes(Seleccionado.Codigo))
                    {
                        //ES NECESARIO VALIDAR SI POSEE DETALLE RESES PEDIENTES DE FACTURAR
                        if (!logica.buscarDetalleResComprometidoCliente(v_objCliente.v_no_cliente, Seleccionado.Codigo))
                        {
                            //SI POSEE DETALLERESES
                            LogicaCarniceriaEventos plogica = new LogicaCarniceriaEventos(view);

                            plogica.pnlProductos_ltvProductos(
                                v_objCliente,
                                Seleccionado.Codigo,
                                plogica,
                                PantallasSistema._pantallaProducto,
                                this,
                                vistaVisita.controlador.COD_PEDIDO);
                        }
                        else
                        {
                            //SI POSEE DETALLE RESES PENDIENTES DEBE CARGAR EL PEDIDO PRIMERO
                            await _logMessageAttention.generalAttention("El cliente seleccionado posee inventario comprometido a un pedido, complete primero ese pedido para continuar con esta acción.");
                            await Application.Current.MainPage.Navigation.PopAsync();
                        }

                    }
                }
                else
                {
                    pnlProductos_txtCantidad_Focus(false);
                }
            }
        }

        public async Task Cerrando()
        {
            if (cerrar)
            {
                if (v_logivaVisitaEventos != null)
                {
                    await v_logivaVisitaEventos.pnlTransacciones_ltvProductos_ColumnClickParte2(view.v_objProducto);

                    if (GuadarPedidoDR)
                    {
                        vistaVisita.controlador.vistaVisita_Closing(false, true, true, ListaDetallesResesProductos);
                    }
                }
            }
        }
    }
}
