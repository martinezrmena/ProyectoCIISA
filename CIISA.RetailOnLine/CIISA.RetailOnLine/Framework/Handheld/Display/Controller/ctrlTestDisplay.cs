using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Display.View;
using CIISA.RetailOnLine.Framework.Handheld.Display.ViewController;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.Display.Controller
{
    public class ctrlTestDisplay
    {
        private viewTestDisplay view { get; set; }
        private SystemCIISA v_systemCIISA = null;

        public ctrlTestDisplay(viewTestDisplay pview)
        {
            view = pview;
        }

        internal void screenInicialization(SystemCIISA psystemCIISA)
        {
            v_systemCIISA = psystemCIISA;

            RenderWindows.paintWindow(view);

            renderPanels(view.FindByName<StackLayout>("pnlTestDisplay"));
        }

        private void renderPanels(StackLayout ppanel)
        {
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlTestDisplay"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlTestDisplay").Id))
            {
                view.Title = "Prueba Táctil";
            }

            view.FindByName<SignaturePadView>("pnlTestDisplay_pbxTestBox").SignatureLine.IsVisible = false;
            view.FindByName<SignaturePadView>("pnlTestDisplay_pbxTestBox").CaptionText = "";
            view.FindByName<SignaturePadView>("pnlTestDisplay_pbxTestBox").PromptText = "";
            view.FindByName<SignaturePadView>("pnlTestDisplay_pbxTestBox").ClearText = "Limpiar";

            ppanel.IsVisible = true;
        }

        internal void constructor()
        {
            view.FindByName<SignaturePadView>("pnlTestDisplay_pbxTestBox").Clear();
            view.FindByName<SignaturePadView>("pnlTestDisplay_pbxTestBox").StrokeColor = Color.Black;
        }

        internal void menu_mniClean_Click()
        {
            constructor();
        }

        internal async Task menu_mniClose_Click()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}
