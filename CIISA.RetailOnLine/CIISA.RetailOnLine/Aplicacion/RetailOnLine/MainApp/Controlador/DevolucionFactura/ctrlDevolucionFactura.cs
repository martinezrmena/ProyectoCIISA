using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.DevolucionFactura;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista.DevolucionFactura;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.External.CustomListview;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador.DevolucionFactura
{
    public class ctrlDevolucionFactura
    {
        private vistaDevolucionFactura view { get; set; }
        private vistaVisita Visita { get; set; }

        private Cliente v_objCliente = null;

        internal ctrlDevolucionFactura(vistaDevolucionFactura pview, vistaVisita v_view)
        {
            view = pview;
            Visita = v_view;
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlDevolucionFactura").Id))
            {
                view.Title = "Devolución de facturas";
            }

            ppanel.IsVisible = true;
        }

        internal void ScreenInicialization(Cliente pobjCliente)
        {
            v_objCliente = pobjCliente;
            v_objCliente.v_listaFacturas.Clear();

            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlDevolucionFactura"));

            Logica_ManagerFactura _manager = new Logica_ManagerFactura();

            _manager.buscarListaFacturaDevolucion(view.FindByName<ListView>("pnlDevolucionFacturas_ltvFacturas"), v_objCliente);
        }

        internal async Task menu_mniAgregar_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (!await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlDevolucionFacturas_ltvFacturas")))
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention("No ha seleccionado ningún elemento.");
            }
            else
            {
                var Source = view.FindByName<ListView>("pnlDevolucionFacturas_ltvFacturas").SelectedItem as pnlDevolucion_ltvTiposDevolucion;

                Visita.controlador.COD_FACTURA = Source.CODDOCUMENTO;
                Visita.controlador.COD_PEDIDO = Source.CODPEDIDO;
                Visita.controlador.DevolucionFacturaInitialize();
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }


    }
}
