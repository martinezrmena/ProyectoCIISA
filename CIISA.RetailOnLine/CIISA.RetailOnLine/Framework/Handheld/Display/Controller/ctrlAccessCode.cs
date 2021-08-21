using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.IdentificarUsuario;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Handheld.Display.View;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Framework.Handheld.Display.Controller
{
    internal class ctrlAccessCode
    {
        private viewAccessCode view { get; set; }
        public bool Cerrar = false;
        private string v_accessCode = string.Empty;
        private ctrlMenu v_ctrlMenu = null;
        private ctrlIdentificarUsuario v_ctrolIdentificar = null;
        private LogicaIdentificarUsuario_Verificar v_LIUV = null;
        public string v_pantalla = string.Empty;

        internal ctrlAccessCode(viewAccessCode pview)
        {
            view = pview;
        }

        private void renderComponentes()
        {
            view.FindByName<ExtendedEntry>("pnlSecurityCode_txtCode").IsEnabled = false;
            view.FindByName<Button>("pnlSecurityCode_btnClean").IsEnabled = false;
            view.FindByName<Button>("pnlSecurityCode_btnDelete").IsEnabled = false;
            //view.FindByName<ToolbarItem>("menu_mniAccept").IsEnabled = false;
            view.FindByName<Grid>("pnlSecurityCode_stkAcceder").IsVisible = false;

        }

        internal void renderPanels(StackLayout ppanel)
        {
            RenderWindows.paintWindow(view);

            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlSecurityCode").Id))
            {
                view.Title = "Código Acceso";
            }

            ppanel.IsVisible = true;
        }

        internal async void screenInicialization(string paccessCode, string pmessage, ctrlMenu pctrlMenu,string ppantalla)
        {
            v_ctrlMenu = pctrlMenu;
            v_pantalla = ppantalla;

            if (paccessCode.Equals(string.Empty))
            {
                await UserDialogs.Instance.AlertAsync(LogMessages._operationNoCompleted
                    + Environment.NewLine
                    + Environment.NewLine
                    + "(Sin comunicación con el servidor).", "Atención", "Aceptar");

                renderComponentes();
            }

            v_accessCode = paccessCode;

            view.FindByName<Label>("pnlSecurityCode_lblMessage").Text = pmessage;

            renderPanels(view.FindByName<StackLayout>("pnlSecurityCode"));
        }

        internal async void screenInicialization(string paccessCode, string pmessage, ctrlIdentificarUsuario pctrolIdentificar, string ppantalla)
        {
            v_ctrolIdentificar = pctrolIdentificar;
            v_pantalla = ppantalla;

            if (paccessCode.Equals(string.Empty))
            {
                await UserDialogs.Instance.AlertAsync(LogMessages._operationNoCompleted
                    + Environment.NewLine
                    + Environment.NewLine
                    + "(Sin comunicación con el servidor).", "Atención", "Aceptar");

                renderComponentes();
            }

            v_accessCode = paccessCode;

            view.FindByName<Label>("pnlSecurityCode_lblMessage").Text = pmessage;

            renderPanels(view.FindByName<StackLayout>("pnlSecurityCode"));
        }

        internal async void screenInicialization(string paccessCode, string pmessage, LogicaIdentificarUsuario_Verificar PLIUV, string ppantalla)
        {
            v_LIUV = PLIUV;
            v_pantalla = ppantalla;

            if (paccessCode.Equals(string.Empty))
            {
                await UserDialogs.Instance.AlertAsync(LogMessages._operationNoCompleted
                    + Environment.NewLine
                    + Environment.NewLine
                    + "(Sin comunicación con el servidor).", "Atención", "Aceptar");

                renderComponentes();
            }

            v_accessCode = paccessCode;

            view.FindByName<Label>("pnlSecurityCode_lblMessage").Text = pmessage;

            renderPanels(view.FindByName<StackLayout>("pnlSecurityCode"));
        }

        internal void pnlSecurityCode_btnDelete_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlSecurityCode_txtCode"));
        }

        internal void pnlSecurityCode_btnClean_Click()
        {
            view.FindByName<ExtendedEntry>("pnlSecurityCode_txtCode").Text = string.Empty;

            view.FindByName<ExtendedEntry>("pnlSecurityCode_txtCode").Focus();
        }

        internal async Task menu_mniAccept_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (!_validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlSecurityCode_txtCode")))
            {
                if (v_accessCode.Equals(view.FindByName<ExtendedEntry>("pnlSecurityCode_txtCode").Text))
                {
                    view.v_correctAccessCode = true;
                    //view.Close();
                    Cerrar = true;
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
                else
                {
                    view.v_correctAccessCode = false;
                    RenderPaint.paintRedBackgroundTextBox(view.FindByName<ExtendedEntry>("pnlSecurityCode_txtCode"));
                    view.FindByName<ExtendedEntry>("pnlSecurityCode_txtCode").Text = string.Empty;
                }
            }
        }

        internal void menu_mniClose_Click()
        {
            //view.Close();
            Cerrar = true;
            Application.Current.MainPage.Navigation.PopModalAsync();
        }

        public async Task Cerrando()
        {
            if (Cerrar)
            {
                if (v_pantalla.Equals("FacturacionFaltantes"))
                {
                    if (v_ctrlMenu != null)
                    {
                        await v_ctrlMenu.pnlMenu_btnFactFaltantes_ClickParte2(view.v_correctAccessCode);
                    }
                }
                if (v_pantalla.Equals("ConsolidarCerrar"))
                {
                    if(v_ctrlMenu != null)
                    {
                        v_ctrlMenu.pnlMenu_btnConsolidarCerrar_ClickParte2(view.v_correctAccessCode);
                    }
                }
                if (v_pantalla.Equals("Liquidadores"))
                {
                    if (v_ctrlMenu != null)
                    {
                        v_ctrlMenu.pnlMenu_btnLiquidadores_ClickParte2(view.v_correctAccessCode);
                    }
                }
                if (v_pantalla.Equals("IdentificarUsuario"))
                {
                    if (v_ctrolIdentificar != null)
                    {
                        v_ctrolIdentificar.menu_mniLiquidadores_ClickParte2(view.v_correctAccessCode);
                    }
                }
                if (v_pantalla.Equals("LogicaIdentificarUsuario"))
                {
                    if (v_LIUV != null)
                    {
                        await v_LIUV.verificarTipoCargaParte2(view.v_correctAccessCode);
                    }
                }
                if (v_pantalla.Equals("TomaFisica"))
                {
                    if (v_ctrlMenu != null)
                    {
                        await v_ctrlMenu.pnlMenu_btnTomaFisica_ClickParte2(view.v_correctAccessCode);
                    }
                }
            }
        }
    }
}
