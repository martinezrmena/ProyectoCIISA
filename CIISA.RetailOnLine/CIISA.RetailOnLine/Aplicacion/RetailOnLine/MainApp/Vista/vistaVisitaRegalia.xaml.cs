using Acr.UserDialogs;
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
    public partial class vistaVisitaRegalia : ContentPage
    {
        private ctrlVisitaRegalia controlador = null;
        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public bool v_cancelo = false;

        public vistaVisitaRegalia(Producto pobjProducto,vistaVisita pviewVisita)
        {
            controlador = new ctrlVisitaRegalia(this,pviewVisita);

            InitializeComponent();

            controlador.ScreenInicialization(pobjProducto);
        }

        public vistaVisitaRegalia(Producto pobjProducto, LogicaVisitaEventos plogicaEventos)
        {
            controlador = new ctrlVisitaRegalia(this, plogicaEventos);

            InitializeComponent();

            controlador.ScreenInicialization(pobjProducto);
        }

        private async Task menu_mniAgregar_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await simulateClickGestures.SelectedStack(pnlRegalia_stkAgregar);
                    controlador.menu_mniAgregar_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }

                await simulateClickGestures.NoSelectedStack(pnlRegalia_stkAgregar);
            }
        }

        private async Task menu_mniCancelar_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    await simulateClickGestures.SelectedStack(pnlRegalia_stkCancelar);
                    controlador.menu_mniCancelar_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }

                await simulateClickGestures.NoSelectedStack(pnlRegalia_stkCancelar);
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