using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Constantes;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Handheld.GPS.View;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    public class ctrlTipoImpresion
    {
        internal vistaTipoImpresion view { get; set; }
        private string v_viewTitle = "Tipo Impresión ";
        private HelperTipoImpresion helperTipoImpresion = new HelperTipoImpresion();
        private string TipoImpresion;
        private int i = 0;
        private LogMessageAttention logMessageAttetion = new LogMessageAttention();

        public ctrlTipoImpresion(vistaTipoImpresion pview)
        {
            view = pview;
        }

        public void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);

            renderPanels(view.FindByName<StackLayout>("pnlTipoImpresion"));

            TipoImpresion = helperTipoImpresion.GetValue();

            bool ActiveZPL = TipoImpresion.Equals(TipoImpresionConst.ZPL) ? true : false;

            bool ActiveESCPOS = TipoImpresion.Equals(TipoImpresionConst.ESCPOS) ? true : false;

            RenderBotones(ActiveZPL, ActiveESCPOS);

        }

        private void RenderBotones(bool isZPLActive, bool isESCPOSActive)
        {
            if (isZPLActive)
            {
                view.FindByName<Button>("pnlTipoImpresion_btnZPL").IsEnabled = false;
            }
            else
            {
                view.FindByName<Button>("pnlTipoImpresion_btnZPL").IsEnabled = true;
            }

            if (isESCPOSActive)
            {
                view.FindByName<Button>("pnlTipoImpresion_btnESCPOS").IsEnabled = false;
            }
            else
            {
                view.FindByName<Button>("pnlTipoImpresion_btnESCPOS").IsEnabled = true;
            }
        }

        private void renderPanels(StackLayout ppanel)
        {
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlTipoImpresion"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlTipoImpresion").Id))
            {
                view.Title = v_viewTitle;
            }

            ppanel.IsVisible = true;
        }

        internal async Task TipoImpresionZPL()
        {
            helperTipoImpresion.UpdateValue(TipoImpresionConst.ZPL);
            RenderBotones(true, false);
            await logMessageAttetion.generalAttention("Establecio el lenguaje ZPL por defecto para realizar las impresiones.");

        }

        internal async Task TipoImpresionESCPOS()
        {
            helperTipoImpresion.UpdateValue(TipoImpresionConst.ESCPOS);
            RenderBotones(false, true);
            await logMessageAttetion.generalAttention("Establecio el lenguaje Esc/Pos por defecto para realizar las impresiones.");
        }

        internal async Task menu_mniExit_Click()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
