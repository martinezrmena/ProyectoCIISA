using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Carniceria;
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
    public partial class vistaVisitaCantidad : ContentPage
    {
        private ctrlVisitaCantidad controlador = null;

        public Producto v_objProducto = new Producto();
        public bool v_guardar = false;
        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaVisitaCantidad(Producto pobjProducto, List<Producto> plistaProductoComprometido, LogicaVisitaEventos logica)
        {
            controlador = new ctrlVisitaCantidad(this);

            InitializeComponent();

            controlador.ScreenInicialization(pobjProducto, plistaProductoComprometido,logica);
        }

        public vistaVisitaCantidad(Producto pobjProducto, List<Producto> plistaProductoComprometido, LogicaCarniceriaEventos logica)
        {
            controlador = new ctrlVisitaCantidad(this);

            InitializeComponent();

            controlador.ScreenInicialization(pobjProducto, plistaProductoComprometido, logica);
        }

        private async Task pnlModificarCantidad_btnBorrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlModificarCantidad_btnBorrar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlModificarCantidad_btnPuntoDecimal_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlModificarCantidad_btnPuntoDecimal_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlModificarCantidad_btnLimpiar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlModificarCantidad_btnLimpiar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlModificarCantidad_btnAgregarReal_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlModificarCantidad_btnAgregarReal_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task menu_mniCancelar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlModificarCantidad_stkCancelar);
                controlador.menu_mniCancelar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlModificarCantidad_stkCancelar);
        }

        private async Task menu_mniModificar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlModificarCantidad_stkModificar);
                controlador.menu_mniModificar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlModificarCantidad_stkModificar);
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            try
            {
                await controlador.Cerrando();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
    }
}