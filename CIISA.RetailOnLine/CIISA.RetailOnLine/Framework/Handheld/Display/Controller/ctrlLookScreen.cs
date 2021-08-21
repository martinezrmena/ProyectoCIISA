using CIISA.RetailOnLine.Framework.Handheld.Display.View;
using CIISA.RetailOnLine.Framework.Handheld.Power.Controller;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.Display.Controller
{
    public class ctrlLookScreen
    {
        private viewLockScreen view { get; set; }

        public ctrlLookScreen(viewLockScreen pview)
        {
            view = pview;
        }

        internal void renderPanels(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlLockScreen").Id))
            {
                view.Title = "Bloqueo";
            }

            ppanel.IsVisible = true;
        }

        private void renderComponents()
        {
            view.FindByName<Button>("pnlLockScreen_btnRed").IsEnabled = false;
            view.FindByName<Button>("pnlLockScreen_btnYellow").IsEnabled = false;
            view.FindByName<Button>("pnlLockScreen_btnGreen").IsEnabled = false;
        }

        internal void screenInicialization()
        {
            RenderWindows.paintWindow(view);

            renderPanels(view.FindByName<StackLayout>("pnlLockScreen"));

            renderComponents();
        }

        internal void turnOff()
        {
            ctrlPower_TurnOffMode _systemEnergy = new ctrlPower_TurnOffMode();
            _systemEnergy.btnTurnOff_Click();
        }

        internal void menu_mniUnlock_Click()
        {
            view.FindByName<Button>("pnlLockScreen_btnRed").BackgroundColor = Color.Red;
            view.FindByName<Button>("pnlLockScreen_btnRed").IsEnabled = true;
            view.FindByName<Grid>("pnlLockScreen_grdOptions").IsVisible = false;
        }

        internal void pnlLockScreen_btnRed_Click()
        {
            view.FindByName<Button>("pnlLockScreen_btnYellow").BackgroundColor = Color.Yellow;
            view.FindByName<Button>("pnlLockScreen_btnYellow").IsEnabled = true;
            view.FindByName<Button>("pnlLockScreen_btnRed").IsVisible = false;
        }

        internal void pnlLockScreen_btnYellow_Click()
        {
            view.FindByName<Button>("pnlLockScreen_btnGreen").BackgroundColor = Color.Lime;
            view.FindByName<Button>("pnlLockScreen_btnGreen").IsEnabled = true;
            view.FindByName<Button>("pnlLockScreen_btnYellow").IsVisible = false;
        }

        internal void pnlLockScreen_btnGreen_Click()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
