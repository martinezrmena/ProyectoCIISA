using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Carniceria;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista.Carniceria
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaDetalleReses : ContentPage
    {
        internal ctrlDetalleReses controlador = null;
        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        /// <summary>
        ///Pantalla encargada de definir cuales detalles reses se utilizarán
        ///para formar la linea del pedido desde visita
        /// </summary>
        /// return la cantidad de res en base a la cual se formará el descuento,
        ///impuesto, etc
        public vistaDetalleReses(
            Cliente _cliente,
            Producto _producto,
            LogicaCarniceriaEventos logica,
            string _pantalla,
            int NumLinea,
            string COD_DOCUMENTO,
            string CodPedido)
        {
            InitializeComponent();

            controlador = new ctrlDetalleReses(this, _cliente, _producto, logica, _pantalla, NumLinea, COD_DOCUMENTO, CodPedido);

            controlador.ScreenInicialization();

        }

        /// <summary>
        ///Pantalla encargada de definir cuales detalles reses se utilizarán
        ///para formar la linea del pedido desde producto
        /// </summary>
        /// return la cantidad de res en base a la cual se formará el descuento,
        ///impuesto, etc
        public vistaDetalleReses(
            Cliente _cliente,
            string codproducto,
            LogicaCarniceriaEventos logica,
            string _pantalla,
            ctrlProducto ctrl,
            string codpedido)
        {
            InitializeComponent();

            controlador = new ctrlDetalleReses(this, _cliente, codproducto, logica, _pantalla, ctrl, codpedido);

            controlador.ScreenInicialization();

        }

        private async Task menu_mniProcesar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlDetalleReses_stkProcesar);
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await controlador.Procesar();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }

            await simulateClickGestures.NoSelectedStack(pnlDetalleReses_stkProcesar);

        }

        private async Task menu_mniCancelar_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlDetalleReses_stkCancelar);
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controlador.Cerrar();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }

            await simulateClickGestures.NoSelectedStack(pnlDetalleReses_stkCancelar);
        }

        private async void pnlDetalleReses_chkTodas_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            try
            {
                controlador.pnlDetalleReses_chkTodas_Checked();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();
            try
            {
                await controlador.Closing();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            
        }

        private async void menu_mniSumar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlDetalleReses_ltvReses_ItemSelected();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
    }
}