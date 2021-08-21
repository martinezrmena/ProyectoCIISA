using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaProducto : ContentPage
    {
        public ctrlProducto controlador = null;
        public Producto v_objProducto = null;
        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaProducto(vistaVisita vista, string ppantallaInvoca, Cliente pobjCliente, string pnomTipoTransaccion, bool pshowControlBox, List<Producto> plistaProductoComprometido, string pcodigoUltimoProducto,LogicaVisitaEventos logicaVisitaEventos, List<string> ListaProductosVisita)
        {
            controlador = new ctrlProducto(vista, this);

            InitializeComponent();

            controlador.ScreenInicialization(ppantallaInvoca, pobjCliente, pnomTipoTransaccion, pshowControlBox, plistaProductoComprometido, pcodigoUltimoProducto,logicaVisitaEventos, ListaProductosVisita);
        }

        public vistaProducto(string ppantallaInvoca, bool pshowControlBox)
        {
            controlador = new ctrlProducto(this);

            InitializeComponent();

            controlador.ScreenInicialization(ppantallaInvoca, pshowControlBox);
        }

        private async Task pnlProductos_cbxEspecificacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlProductos_cbxEspecificacion_SelectedIndexChanged();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlProductos_cbxTipoBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlProductos_cbxTipoBusqueda_SelectedIndexChanged();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlProductos_stkBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {

                await simulateClickGestures.SelectedStack(pnlProductos_stkBuscar);

                pnlProductos_ltvProductos.ItemSelected -= pnlProductos_ltvProductos_ItemSelected;

                controlador.menu_mniBuscar_Click();

                pnlProductos_ltvProductos.ItemSelected += pnlProductos_ltvProductos_ItemSelected;
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlProductos_stkBuscar);
        }

        private async Task pnlProductos_stkSeleccionar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlProductos_stkSeleccionar);
                await controlador.menu_mniAgregar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }


            await simulateClickGestures.NoSelectedStack(pnlProductos_stkSeleccionar);
        }

        private async Task pnlProductos_btnBorrarBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlProductos_btnBorrarBuscar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlProductos_btnLimpiarBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlProductos_btnLimpiarBuscar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlProductos_btnInventarioPedidos_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlProductos_btnInventarioPedidos_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlProductos_btnCopiarCantidad_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlProductos_btnCopiarCantidad_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlProductos_btnBorrarCantidad_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlProductos_btnBorrarCantidad_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlProductos_btnPuntoDecimal_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlProductos_btnPuntoDecimal_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlProductos_ctxtMnu_mitAyuda_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlProductos_ctxtMnu_mitAyuda_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlProductos_ctxtMnu_Seleccionar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlProductos_ltvBuscarProducto_ItemActivate();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();
            pnlProductos_cbxTipoBusqueda.PropertyChanged -= pnlProductos_cbxTipoBusqueda_PropertyChanged;
            await controlador.Cerrando();
        }

        private async void pnlProductos_ltvProductos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                await controlador.pnlProductos_ltvBuscarProducto_ItemActivate();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void pnlProductos_cbxTipoBusqueda_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                controlador.pnlProductos_txtBuscar_Focus(true);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void pnlProductos_txtCantidad_Focused(object sender, FocusEventArgs e)
        {
            try
            {
                await controlador.FocusProducto();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

        }
    }
}