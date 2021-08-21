using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using CIISA.RetailOnLine.Framework.Handheld.GPS.Controller;
using Geolocator.Plugin;
using System;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Framework.Handheld.GPS.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class viewGPS : ContentPage
	{
        private ctrlGPS controller = null;
        private readonly TimeSpan timespan = TimeSpan.FromSeconds(1);
        private CancellationTokenSource cancellation = new CancellationTokenSource();
        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public viewGPS()
        {
            controller = new ctrlGPS(this);

            InitializeComponent();

            controller.ScreenInicialization();
        }

        private async void menu_mniExit_Click(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await simulateClickGestures.SelectedStack(pnlGps_stkCerrar);
                await controller.menu_mniExit_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }

            await simulateClickGestures.NoSelectedStack(pnlGps_stkCerrar);
        }

        private async void viewGPS_Closed(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                controller.viewGPS_Closed();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        private async void pnlGps_btnGpsStop_Click(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                Stop();
                controller.pnlGps_btnGpsStop_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        private async void pnlGps_btnGpsStart_Click(object sender, EventArgs e)
        {
            
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await controller.pnlGps_btnGpsStart_Click();
                Start();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        private async Task menu_mniCoordinates_Click(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                await simulateClickGestures.SelectedStack(pnlGps_stkCoordenadas);
                await controller.menu_mniCoordinates_Click();
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }

            await simulateClickGestures.NoSelectedStack(pnlGps_stkCoordenadas);
        }

        #region TIMER
        public void Start()
        {
            CancellationTokenSource cts = this.cancellation; // safe copy
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            
            Device.StartTimer(this.timespan,
                () => {
                    if (cts.IsCancellationRequested)
                    {
                        return false;
                    }
                    else
                    {
                        if (locator.IsGeolocationEnabled)
                        {
                            controller.tmrCounter_Tick();
                        }
                        else {
                            controller.tmrCounter_Tick();
                            Stop();
                            controller.pnlGps_btnGpsStop_Click();
                        }
                        return true; //true for periodic behavior
                    }

                });
        }

        public void Stop()
        {
            Interlocked.Exchange(ref this.cancellation, new CancellationTokenSource()).Cancel();
        }
        #endregion

    }
}