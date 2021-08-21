using CIISA.RetailOnLine.Framework.Handheld.Power.View;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.Power.Controller
{
    public class ctrlPower
    {
        private viewPower view { get; set; }

        private ctrlPower_TurnOffMode v_ctrlPower_TurnOffMode;
        private ctrlPower_EnergyState v_ctrlPower_EnergyState;

        public ctrlPower(viewPower pview)
        {
            view = pview;

            v_ctrlPower_TurnOffMode = new ctrlPower_TurnOffMode();
            v_ctrlPower_EnergyState = new ctrlPower_EnergyState();
        }

        internal void screenInicialization()
        {
            RenderWindows.paintWindow(view);

            renderPanels(view.FindByName<StackLayout>("pnlPower"));
        }

        internal void btnSuspend_Click()
        {
            v_ctrlPower_TurnOffMode.btnSuspend_Click();
        }

        internal void pnlPower_btnFullOn_Click()
        {
            v_ctrlPower_EnergyState.pnlPower_btnFullOn_Click();
        }

        internal void menu_mniConsultarEstado_Click()
        {
            if (v_ctrlPower_EnergyState.v_error == null)
            {
                view.FindByName<Label>("pnlPower_lblEnergyState").Text = "Estado: " + "Error " + v_ctrlPower_EnergyState.v_error + ".";
            }
            else
            {
                view.FindByName<Label>("pnlPower_lblEnergyState").Text = "Estado: " + v_ctrlPower_EnergyState.v_error;
            }
        }

        internal void renderPanels(StackLayout ppanel)
        {
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlPower"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlPower").Id))
            {
                view.Title = "Energía Administración";
            }

            ppanel.IsVisible = true;
        }

        internal async Task Close() {

            await Application.Current.MainPage.Navigation.PopAsync();
        }


    }
}
