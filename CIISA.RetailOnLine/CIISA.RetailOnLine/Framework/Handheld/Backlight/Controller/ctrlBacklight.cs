using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.External.DeviceInformation;
using CIISA.RetailOnLine.Framework.Handheld.Backlight.View;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Framework.Handheld.Backlight.Controller
{
    public class ctrlBacklight
    {
        private viewBackLight view { get; set; }

        public ctrlBacklight(viewBackLight pview)
        {
            view = pview;
        }

        public void screenInicialization()
        {
            RenderWindows.paintWindow(view);
            renderPanels(view.FindByName<StackLayout>("pnlBacklight"));
            Onload();
            renderComponents();
        }

        private void renderComponents()
        {
            updateActualValue();
        }

        private void updateActualValue()
        {
            view.FindByName<Label>("pnlBacklight_lblActualValue").Text = Math.Round((view.FindByName<Slider>("pnlBacklight_trbBacklightLevel").Value)/10,0).ToString();
        }

        private void renderPanels(StackLayout ppanel)
        {
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlBacklight"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlBacklight").Id))
            {
                view.Title = "Iluminación";
            }

            ppanel.IsVisible = true;
        }

        internal async Task menu_mniClose_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea Salir?", "Salir"))
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        internal void trbBacklightLevel_ValueChanged()
        {
            var brightnessService = DependencyService.Get<IBrightnessService>();

            if (view.FindByName<CheckBox>("pnlBacklight_chkBacklight").Checked && view.FindByName<Slider>("pnlBacklight_trbBacklightLevel").Value != 100)
            {
                view.FindByName<CheckBox>("pnlBacklight_chkBacklight").Checked = false;
            }

            float value = ((float)view.FindByName<Slider>("pnlBacklight_trbBacklightLevel").Value/100);
            brightnessService.SetBrightness(value);
            updateActualValue();
        }

        internal void chkBacklight_CheckStateChanged()
        {
            var brightnessService = DependencyService.Get<IBrightnessService>();

            if (view.FindByName<CheckBox>("pnlBacklight_chkBacklight").Checked)
            {
                view.FindByName<Slider>("pnlBacklight_trbBacklightLevel").Value = 100;
                brightnessService.SetBrightness(1);
            }
            else
            {
                view.FindByName<Slider>("pnlBacklight_trbBacklightLevel").Value = 0;
                brightnessService.SetBrightness(0);
            }
        }

        internal void Onload() {
            var brightnessService = DependencyService.Get<IBrightnessService>();
            var Value = brightnessService.GetBrightness();

            if ((int)Value == -1 || Value == 0)
            {
                //la primera vez, por defecto del sistema
                view.FindByName<Slider>("pnlBacklight_trbBacklightLevel").Value = 0.1;
            }
            else {

                if ((int)Value == 1)
                {
                    view.FindByName<CheckBox>("pnlBacklight_chkBacklight").Checked = true;
                    view.FindByName<Slider>("pnlBacklight_trbBacklightLevel").Value = 100;
                }
                else
                {
                    view.FindByName<Slider>("pnlBacklight_trbBacklightLevel").Value = Math.Round((double.Parse(Value.ToString())) * 100, 0);
                }
            }
        }

    }
}
