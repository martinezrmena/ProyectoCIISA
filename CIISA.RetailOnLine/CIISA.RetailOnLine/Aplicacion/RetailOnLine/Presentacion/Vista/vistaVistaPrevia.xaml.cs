using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Helpers;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaVistaPrevia : ContentPage
    {
        public ctrlVistaPrevia controlador = null;

        public Cliente v_objCliente = null;

        public string ReporteAVisualizar = string.Empty;

        internal SimulateClickGestures simulateClickGestures = new SimulateClickGestures();

        public vistaVistaPrevia(string Reporte)
        {
            controlador = new ctrlVistaPrevia(this);

            ReporteAVisualizar = Reporte;

            InitializeComponent();

            controlador.ScreenInicialization();
        }

        public vistaVistaPrevia(string Reporte, Cliente pcliente)
        {
            v_objCliente = pcliente;

            controlador = new ctrlVistaPrevia(this);

            ReporteAVisualizar = Reporte;

            InitializeComponent();

            controlador.ScreenInicialization();
        }

        private async void menu_mniImprimir_Clicked(object sender, EventArgs e)
        {
            await simulateClickGestures.SelectedStack(pnlReporte_stkImprimir);
            var DPB = DependencyService.Get<ITaskActivity>();
            DPB.ImprimirReporte(controlador);
            await simulateClickGestures.NoSelectedStack(pnlReporte_stkImprimir);
        }
    }
}