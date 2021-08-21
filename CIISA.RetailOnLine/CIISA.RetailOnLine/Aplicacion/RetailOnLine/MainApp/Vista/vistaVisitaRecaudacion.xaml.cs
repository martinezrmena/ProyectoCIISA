using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
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
    public partial class vistaVisitaRecaudacion : ContentPage
    {
        private ctrlVisitaRecaudacion controlador = null;
        public string v_motivo = string.Empty;
        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaVisitaRecaudacion(Cliente pobjCliente, vistaVisita pviewVisita)
        {        
            controlador = new ctrlVisitaRecaudacion(this, pviewVisita);

            InitializeComponent();

            controlador.ScreenInicialization(pobjCliente);
        }

        private async Task menu_mniAgregar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlRecaudacion_stkAgregar);
                controlador.menu_mniAgregar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlRecaudacion_stkAgregar);
        }

        private async Task menu_mniCancelar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlRecaudacion_stkCancelar);
                controlador.menu_mniCancelar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlRecaudacion_stkCancelar);
        }

        protected override async void OnDisappearing()
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