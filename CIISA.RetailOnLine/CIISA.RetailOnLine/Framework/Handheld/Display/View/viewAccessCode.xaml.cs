using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.IdentificarUsuario;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using CIISA.RetailOnLine.Framework.Handheld.Display.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Framework.Handheld.Display.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class viewAccessCode : ContentPage
    {
        private ctrlAccessCode controller = null;

        public bool v_correctAccessCode = false;

        SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public viewAccessCode(string paccessCode, string pmessage,ctrlMenu pctrlMenu,string ppantalla)
        {
            try
            {
                controller = new ctrlAccessCode(this);

                InitializeComponent();

                controller.screenInicialization(paccessCode, pmessage, pctrlMenu,ppantalla);
            }
            catch (Exception ex)
            {
                ExceptionHandled(ex);
            }
        }

        private async void ExceptionHandled(Exception ex)
        { 
            await ExceptionManager.ExceptionHandling(ex);
        }

        public viewAccessCode(string paccessCode, string pmessage, ctrlIdentificarUsuario pctrlIdentificarUsuario, string ppantalla)
        {
            try
            {
                controller = new ctrlAccessCode(this);

                InitializeComponent();

                controller.screenInicialization(paccessCode, pmessage, pctrlIdentificarUsuario, ppantalla);
            }
            catch (Exception ex)
            {
                ExceptionHandled(ex);
            }
        }

        public viewAccessCode(string paccessCode, string pmessage, LogicaIdentificarUsuario_Verificar pLIUV, string ppantalla)
        {
            try
            {
                controller = new ctrlAccessCode(this);

                InitializeComponent();

                controller.screenInicialization(paccessCode, pmessage, pLIUV, ppantalla);
            }
            catch (Exception ex)
            {
                ExceptionHandled(ex);
            }
        }

        public viewAccessCode()
        {
            InitializeComponent();
        }

        private async Task pnlSecurityCode_btnDelete_Clicked(object sender, EventArgs e)
        {
            try
            {
                controller.pnlSecurityCode_btnDelete_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlSecurityCode_btnClean_Clicked(object sender, EventArgs e)
        {
            try
            {
                controller.pnlSecurityCode_btnClean_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task menu_mniAccept_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlSecurityCode_stkAcceder);
                await controller.menu_mniAccept_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlSecurityCode_stkAcceder);

        }

        private async Task menu_mniClose_Clicked(object sender, EventArgs e)
        {
            try
            {
                await simulateClickGestures.SelectedStack(pnlSecurityCode_stkCerrar);
                controller.menu_mniClose_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

            await simulateClickGestures.NoSelectedStack(pnlSecurityCode_stkCerrar);
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();
            try
            {
                await controller.Cerrando();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            
        }
    }
}