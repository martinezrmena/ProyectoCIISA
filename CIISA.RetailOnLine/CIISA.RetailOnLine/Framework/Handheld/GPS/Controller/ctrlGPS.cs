using CIISA.RetailOnLine.Framework.Handheld.GPS.View;
using CIISA.RetailOnLine.Framework.Handheld.GPS.ViewController;
using CIISA.RetailOnLine.Framework.Handheld.Render;

using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.GPS.Controller
{
    public class ctrlGPS
    {
        internal viewGPS view { get; set; }
        private string v_viewTitle = "GPS ";
        private int i = 0;

        public ctrlGPS(viewGPS pview)
        {
            view = pview;
        }

        private void RenderBotones(bool pactiveGps)
        {
            if (pactiveGps)
            {
                view.FindByName<Button>("pnlGps_btnGpsStart").IsEnabled = false;
                view.FindByName<Button>("pnlGps_btnGpsStop").IsEnabled = true;
                view.FindByName<StackLayout>("pnlGps_stkCoordenadas").IsVisible = true;
            }
            else
            {
                view.FindByName<Button>("pnlGps_btnGpsStart").IsEnabled = true;
                view.FindByName<Button>("pnlGps_btnGpsStop").IsEnabled = false;
                view.FindByName<StackLayout>("pnlGps_stkCoordenadas").IsVisible = false;
            }
        }

        public void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);

            renderPanels(view.FindByName<StackLayout>("pnlGps"));

            RenderBotones(false);
        }

        private void renderPanels(StackLayout ppanel)
        {
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlGps"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlGps").Id))
            {
                view.Title = v_viewTitle;
            }

            ppanel.IsVisible = true;
        }

        internal async Task menu_mniExit_Click()
        {
            pnlGps_btnGpsStop_Click();
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        internal void viewGPS_Closed()
        {
           GPS_Info.v_gpsDevice.Closed();
        }

        internal void pnlGps_btnGpsStop_Click()
        {
            GPS_Info.v_gpsDevice.Closed();
            RenderBotones(false);
            setTimerState(false);
        }

        internal async Task pnlGps_btnGpsStart_Click()
        {
            await GPS_Info.v_gpsDevice.StartGps();
            RenderBotones(true);
            setTimerState(true);
        }

        private void setTimerState(bool penable)
        {
            //SE PUEDE ENVIAR DIRECTO EL VALOR QUE EQUIVALE A 1 SEGUNDO DESDE EL CODEBEHIND
            //view.tmrCounter.Interval = 1000;

            //INDICAREMOS SI SE ACTIVA O NO EL TIMER DESDE EL CODEBEHIND DE LA VISTA
            //view.tmrCounter.Enabled = penable;

            i = 0;

            if (penable)
            {
                view.FindByName<Label>("pnlGps_lblStatus").Text = string.Empty;
            }
            else
            {
                view.Title = v_viewTitle;
            }

        }

        internal void tmrCounter_Tick()
        {
            progress();
            string _data = GPS_Info.v_gpsDevice.GetData();
            GUI_status _status = new GUI_status(this);
            _status.actualizarGUIdesdeHilo(_data, Color.White);
        }

        private void progress()
        {
            if (i == 0)
            {
                i++;
                view.Title = v_viewTitle + "  |";
            }
            else
            {
                if (i == 1)
                {
                    i++;
                    view.Title = v_viewTitle + "  /";
                }
                else
                {
                    if (i == 2)
                    {
                        i++;
                        view.Title = v_viewTitle + "  -";
                    }
                    else
                    {
                        if (i == 3)
                        {
                            i = 0;
                            view.Title = v_viewTitle + "  \\";
                        }
                    }
                }
            }
        }

        internal async Task menu_mniCoordinates_Click()
        {
            await GPS_Info.v_gpsDevice.ShowCoordinates();
        }

        private void UpdateData()
        {
            string _value = string.Empty;

            _value = GPS_Info.v_gpsDevice.GetData();
        }

    }
}
