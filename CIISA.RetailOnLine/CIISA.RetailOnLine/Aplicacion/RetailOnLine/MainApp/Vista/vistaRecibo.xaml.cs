using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita.Guardar;
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
    public partial class vistaRecibo : ContentPage
    {
        public ctrlRecibo controlador = null;
        public string v_pantallaInvoca = string.Empty;

        public vistaRecibo(Cliente pobjCliente,string ppantallaInvoca, LogicaVisitaGuardar logica)
        {
            controlador = new ctrlRecibo(this);
            if (!ppantallaInvoca.Equals(string.Empty))
            {
                v_pantallaInvoca = ppantallaInvoca;
            }

            InitializeComponent();

            controlador.ScreenInicialization(pobjCliente,logica);
        }

        private async Task pnlAbono_btnBorrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlAbono_btnBorrar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }

        }

        private async Task pnlAbono_btnLimpiar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlAbono_btnLimpiar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task menu_mniPagar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.menu_mniPagar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task ctxMenu_abono_mniEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.ctxMenu_abono_mniEliminar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task ctxMenu_abono_mniEliminarTodos_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.ctxMenu_abono_mniEliminarTodos_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task ctxMenu_factura_mniEliminarTodas_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.ctxMenu_factura_mniEliminarTodas_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task ctxMenu_factura_mniEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.ctxMenu_factura_mniEliminar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void pnlAbono_btnPuntoDecimal_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlAbono_btnPuntoDecimal_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void menu_mniCliente_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.menu_mniCliente_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async void menu_mniAbonar_Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.menu_mniAbonar_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        public async void establecerVariablesCliente(Cliente pobjCliente)
        {
            try
            {
                controlador.establecerVariablesCliente(pobjCliente);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlAbono_ltvFacturas_Columna0Clicked(object sender, EventArgs e)
        {
            try
            {
                await controlador.pnlAbono_ltvFacturas_Columna0Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }

        private async Task pnlAbono_ltvFacturas_Columna1Clicked(object sender, EventArgs e)
        {
            try
            {
                controlador.pnlAbono_ltvFacturas_Columna1Click();
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
                await controlador.Cerrando();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
        }
    }
}