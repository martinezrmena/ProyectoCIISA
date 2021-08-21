using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita
{
    internal class LogicaVisitaConstructor
    {
        private vistaVisita view = null;

        private LogicaVisitaComboBox _logicaVisitaComboBox = null;

        internal LogicaVisitaConstructor(vistaVisita pview)
        {
            view = pview;

            _logicaVisitaComboBox = new LogicaVisitaComboBox(view);
        }

        internal void constructor()
        {
            RenderWindows.paintWindow(view);

            LogicaVisitaRender _logica = new LogicaVisitaRender(view);

            _logica.renderPaneles(view.FindByName<StackLayout>("pnlTransacciones"));
        }

        internal async Task constructor2()
        {
            LogicaVisitaRender _logicaVisitaRender = new LogicaVisitaRender(view);

            _logicaVisitaRender.renderComponentesPnlTransacciones(true);

            _logicaVisitaRender.renderMenu(false);

            await _logicaVisitaComboBox.filtrarComboBoxTipoTransaccionPorIndicadores();
        }

        /// <summary>
        /// Constructor encargado de renderizar pedidos segun el tipo de documento
        /// El metodo se dispara al ingresar a la pantalla, al cambiar de documento, al cambiar de cliente
        /// </summary>
        internal async Task constructor3()
        {
            LogicaVisitaPedido _logicaVisitaPedido = new LogicaVisitaPedido(view);
            string Es_Factura = _logicaVisitaComboBox.getTipoDocumento().Contains(ROLTransactions._facturaNombre) ? SQL._Si : SQL._No;

            #region Limpiar campos
            view.controlador.v_objCliente.v_objListaPedidos.Clear();
            view.controlador.COD_DOCUMENTO = string.Empty;
            view.controlador.COD_FACTURA = string.Empty;
            view.controlador.COD_PEDIDO = string.Empty;
            view.controlador.v_DevolucionFactura = false;
            #endregion

            //Es necesario validar si hay pedidos back office y si el agente es carnicero
            view.controlador.v_PedidoBackOffice = view.controlador.TipoAgente.Equals(Agent._carniceroSigla) && Es_Factura.Equals(SQL._Si) ? _logicaVisitaPedido.ExistePedidoBackOffice() : false;

            if (view.controlador.v_PedidoBackOffice && !view.controlador.v_objCliente.v_objTransaccion.v_recuperarDocumento)
            {
                view.FindByName<Label>("pnlTransacciones_clhCodigo").IsEnabled = false;

                //En cuyo caso el usuario deberá seleccionar el pedido que requiere antes de poder respaldar cualquier linea
                ShowSROL _showTramite = new ShowSROL();

                _showTramite.mostrarPantallaPedidos(view.controlador.v_objCliente, view);

                LogicaVisitaRender _logicaRender = new LogicaVisitaRender(view);
                _logicaRender.renderMenu(false);
            }
            else
            {
                await ConstructorPedidosManual(_logicaVisitaPedido);
            }
        }


        public async Task ConstructorPedidosManual(LogicaVisitaPedido _logicaVisitaPedido)
        {
            string Es_Factura = _logicaVisitaComboBox.getTipoDocumento().Contains(ROLTransactions._facturaNombre) ? SQL._Si : SQL._No;

            bool validar = view.controlador.TipoAgente.Equals(Agent._carniceroSigla) ? true : false;

            await _logicaVisitaPedido.cargaPedidosRecuperar(validar, Es_Factura);

            LogicaVisitaLtvProducto _logicaVisitaProducto = new LogicaVisitaLtvProducto(view);

            _logicaVisitaProducto.actualizarProductoInventario();

            LogicaVisitaActualizar _logicaActualizar = new LogicaVisitaActualizar(view);

            _logicaActualizar.actualizarColumnas();
            _logicaActualizar.actualizarTotal();

            LogicaVisitaRender _logicaRender = new LogicaVisitaRender(view);
            _logicaRender.renderMenu(false);

            view.controlador.v_finalizoConstructor2 = true;

            Logica_ManagerInventario _manager = new Logica_ManagerInventario();

            if (_manager.InventarioVacio())
            {
                LogMessageAttention _lma = new LogMessageAttention();
                await _lma.generalAttention(
                    "El inventario se encuentra vacío."
                    + Environment.NewLine
                    + Environment.NewLine
                    + "* Debe cargar el inventario");
            }

            if (Es_Factura.Contains(SQL._Si))
            {
                view.controlador.ScreenInicializationVisceras();
            }
        }

        /// <summary>
        /// Consutructor encargado de manejar los pedidos desde BackOffice
        /// luego de que el usuario seleccione el elemento en la pantalla de pedidos
        /// </summary>
        /// <param name="CodPedido">Codigo del pedido seleccionado</param>
        /// <returns></returns>
        public async Task ConstructorPedidosBackOffice(string CodPedido)
        {
            view.controlador.v_objCliente.v_objListaPedidos.Clear();
            string Es_Factura = _logicaVisitaComboBox.getTipoDocumento().Contains(ROLTransactions._facturaNombre) ? SQL._Si : SQL._No;
            bool validar = view.controlador.TipoAgente.Equals(Agent._carniceroSigla) ? true : false;

            LogicaVisitaPedido _logicaVisitaPedido = new LogicaVisitaPedido(view);

            await _logicaVisitaPedido.cargaPedidoRecuperarBackOffice(CodPedido,validar, Es_Factura);

            LogicaVisitaLtvProducto _logicaVisitaProducto = new LogicaVisitaLtvProducto(view);

            _logicaVisitaProducto.actualizarProductoInventario();

            LogicaVisitaActualizar _logicaActualizar = new LogicaVisitaActualizar(view);

            _logicaActualizar.actualizarColumnas();
            _logicaActualizar.actualizarTotal();

            LogicaVisitaRender _logicaRender = new LogicaVisitaRender(view);
            _logicaRender.renderMenu(false);

            view.controlador.v_finalizoConstructor2 = true;

            Logica_ManagerInventario _manager = new Logica_ManagerInventario();

            if (_manager.InventarioVacio())
            {
                LogMessageAttention _lma = new LogMessageAttention();
                await _lma.generalAttention(
                    "El inventario se encuentra vacío."
                    + Environment.NewLine
                    + Environment.NewLine
                    + "* Debe cargar el inventario");
            }

            if (Es_Factura.Contains(SQL._Si))
            {
                view.controlador.ScreenInicializationVisceras();
            }
        }

        /// <summary>
        /// Metodo encargado de gestionar los constructores de visita
        /// </summary>
        /// <param name="cons1"></param>
        /// <param name="cons2"></param>
        /// <param name="cons3"></param>
        public async Task Constructores(bool cons1, bool cons2, bool cons3)
        {
            if (cons1){ constructor();}

            if (cons2){ await constructor2();}

            if (cons3){ await constructor3();}
        }

    }
}
