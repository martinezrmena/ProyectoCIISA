using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaVisitaDevolucion : ContentPage
    {
        private ctrlVisitaDevolucion controlador = null;
        public Producto v_objProducto = new Producto();
        SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaVisitaDevolucion(Producto pobjProducto, bool pmodificarProducto,vistaVisita pvisita)
        {
            controlador = new ctrlVisitaDevolucion(this,pvisita);

            InitializeComponent();

            controlador.ScreenInicialization(pobjProducto, pmodificarProducto);
        }

        public vistaVisitaDevolucion(Producto pobjProducto, bool pmodificarProducto, LogicaVisitaEventos plogicaEventos)
        {
            controlador = new ctrlVisitaDevolucion(this, plogicaEventos);

            InitializeComponent();

            controlador.ScreenInicialization(pobjProducto, pmodificarProducto);
        }

        private async Task pnlDevolucion_rdbBueno_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == CheckBox.CheckedProperty.PropertyName)
            {
                try
                {
                    controlador.pnlDevolucion_rdbBueno_CheckedChanged();
                }                
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
        }

        private async Task pnlDevolucion_rdbMalo_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == CheckBox.CheckedProperty.PropertyName)
            {
                try
                {
                    controlador.pnlDevolucion_rdbMalo_CheckedChanged();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
        }

        private async Task menu_mniAgregar_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await simulateClickGestures.SelectedStack(pnlDevolucion_stkAgregar);
                    controlador.menu_mniAgregar_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }

                await simulateClickGestures.NoSelectedStack(pnlDevolucion_stkAgregar);
            }
        }

        private async Task pnlDevolucion_btnLimpiarBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlDevolucion_btnLimpiarBuscar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task menu_mniCancelar_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await simulateClickGestures.SelectedStack(pnlDevolucion_stkCancelar);
                    controlador.menu_mniCancelar_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }

                await simulateClickGestures.NoSelectedStack(pnlDevolucion_stkCancelar);
            }
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