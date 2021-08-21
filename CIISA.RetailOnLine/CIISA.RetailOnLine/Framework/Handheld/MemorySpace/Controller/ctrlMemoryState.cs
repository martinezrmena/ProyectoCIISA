using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.External.CustomProgressBar;
using CIISA.RetailOnLine.Framework.Handheld.MemorySpace.View;
using CIISA.RetailOnLine.Framework.Handheld.MemorySpace.ViewController;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Framework.Handheld.MemorySpace.Controller
{
    public class ctrlMemoryState
    {
        private viewMemorySpace view { get; set; }

        private SystemCIISA v_systemCIISA = null;

        private DeviceMemoryInfo _deviceMemoryInfo = null;

        public ctrlMemoryState(viewMemorySpace pview)
        {
            view = pview;

            _deviceMemoryInfo = new DeviceMemoryInfo();
        }

        public async Task screenInicialization(SystemCIISA psystemCIISA)
        {
            v_systemCIISA = psystemCIISA;

            RenderWindows.paintWindow(view);

            renderPanels(view.FindByName<StackLayout>("pnlMemory"));

            await displayMemoryStatus();

        }

        private void renderPanels(StackLayout ppanel)
        {
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlMemory"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlMemory").Id))
            {
                view.Title = "Estado Memoria";
            }

            ppanel.IsVisible = true;
        }

        private async Task displayMemoryStatus()
        {
            double _percentage = await _deviceMemoryInfo.percentajeFreeMemory();

            view.FindByName<Label>("pnlMemory_lblPercentajeFreeMemory").Text = "Porcentaje de Memoria Libre: " + _percentage + " %";

            double ProgressBarValue = Math.Round((100 - _percentage) / 100, 1);
            await view.FindByName<CustomProgressBar>("pnlMemory_pgbPercentaje").ProgressTo(ProgressBarValue, 900, Easing.Linear);

            double _totalMB = Convert.ToDouble(_deviceMemoryInfo.totalBytes / Numeric._megaByteInBytes);
            view.FindByName<Label>("pnlMemory_lblTotal").Text = "Espacio Total: " + _totalMB.ToString("N2") + " MB";


            double _freeMB = Convert.ToDouble(_deviceMemoryInfo.freeBytesAvailable / Numeric._megaByteInBytes);
            view.FindByName<Label>("pnlMemory_lblFree").Text = "Espacio Libre: " + _freeMB.ToString("N2") + " MB";


            long _usedKb = _deviceMemoryInfo.totalBytes - _deviceMemoryInfo.freeBytesAvailable;
            double _usedMB = Convert.ToDouble(_usedKb / Numeric._megaByteInBytes);
            view.FindByName<Label>("pnlMemory_lblUsed").Text = "Espacio Utilizado: " + _usedMB.ToString("N2") + " MB";

            if (await _deviceMemoryInfo.maintenanceRequired())
            {
                view.FindByName<Label>("pnlMemory_lblState").Text = "Estado: ATENCION, el equipo debe ir a mantenimiento si el porcentaje de memoria libre es inferior al 10%. Favor solicitelo al Departamento de Liquidaciones.";
                view.FindByName<Label>("pnlMemory_lblState").TextColor = Color.Red;
                view.FindByName<Label>("pnlMemory_lblState").BackgroundColor = Color.White;
                LogMessageAttention _logMessageAttention = new LogMessageAttention();
            }
            else
            {
                view.FindByName<Label>("pnlMemory_lblState").Text = "Estado: El equipo se encuentra en buenas condiciones de espacio de memoria.";
                view.FindByName<Label>("pnlMemory_lblState").TextColor = Color.White;
            }
        }

        internal async Task menu_mniClose_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea Salir?", "Salir"))
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

    }
}
