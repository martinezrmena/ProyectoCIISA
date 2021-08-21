using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CIISA.RetailOnLine.Framework.Handheld.SystemHH.View;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.SystemHH.controller
{
    class ctrlAbout
    {
        //COMETARIO PRUEBA
        private viewAbout view { get; set; }
        private SystemCIISA v_systemCIISA = null;

        public ctrlAbout(viewAbout pview)
        {
            view = pview;
        }

        internal void screenInicialization(SystemCIISA psystemCIISA)
        {
            v_systemCIISA = psystemCIISA;

            view.FindByName<Label>("pnlAbout_lblSystem").Text = v_systemCIISA._name;
            view.FindByName<Label>("pnlAbout_lblVersionNumber").Text = v_systemCIISA._version;
            view.FindByName<Label>("pnlAbout_lblInitials").Text = v_systemCIISA._initials;

            RenderWindows.paintWindow(view);

            renderPanels(view.FindByName<StackLayout>("pnlAbout"));
        }

        internal void renderPanels(StackLayout ppanel)
        {
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlAbout"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlAbout").Id))
            {
                view.Title = "Acerca de";
            }

            ppanel.IsVisible = true;
        }

        internal void menu_mniClose_Click()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}
