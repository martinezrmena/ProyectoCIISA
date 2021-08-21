using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaVisitaEspecificacion : ContentPage
    {
        private ctrlVisitaEspecificacion controlador = null;

        public bool v_guardar = false;

        public vistaVisitaEspecificacion(Producto pobjProducto, vistaVisita pviewVisita)
        {        
            InitializeComponent();

            controlador = new ctrlVisitaEspecificacion(this,pviewVisita);

            pnlEspecificacion_cbxEspecificacion.SelectedIndexChanged -= pnlEspecificacion_cbxEspecificacion_SelectedIndexChanged;

            controlador.ScreenInicialization(pobjProducto);

            pnlEspecificacion_cbxEspecificacion.SelectedIndexChanged += pnlEspecificacion_cbxEspecificacion_SelectedIndexChanged;

            controlador.pnlEspecificacion_cbxEspecificacion_SelectedIndexChanged();
        }

        private void pnlEspecificacion_cbxEspecificacion_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                controlador.pnlEspecificacion_cbxEspecificacion_SelectedIndexChanged();
            }
            catch (Exception ex)
            {
#pragma warning disable CS4014 // Como esta llamada no es 'awaited', la ejecución del método actual continuará antes de que se complete la llamada. Puede aplicar el operador 'await' al resultado de la llamada.
                ExceptionManager.ExceptionHandling(ex);
#pragma warning restore CS4014 // Como esta llamada no es 'awaited', la ejecución del método actual continuará antes de que se complete la llamada. Puede aplicar el operador 'await' al resultado de la llamada.
            }

        }

        private async Task menu_mniAgregar_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    controlador.menu_mniAgregar_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
        }

        private async Task menu_mniCancelar_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Procesando...", null, null, true, MaskType.Black))
            {
                try
                {
                    controlador.menu_mniCancelar_Click();
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
#pragma warning disable CS4014 // Como esta llamada no es 'awaited', la ejecución del método actual continuará antes de que se complete la llamada. Puede aplicar el operador 'await' al resultado de la llamada.
            controlador.Cerrando();
#pragma warning restore CS4014 // Como esta llamada no es 'awaited', la ejecución del método actual continuará antes de que se complete la llamada. Puede aplicar el operador 'await' al resultado de la llamada.
        }
    }
}