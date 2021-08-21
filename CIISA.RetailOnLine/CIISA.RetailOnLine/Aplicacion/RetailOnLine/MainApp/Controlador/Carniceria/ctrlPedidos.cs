using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.DevolucionFactura;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista.Carniceria;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador.Carniceria
{
    public class ctrlPedidos
    {
        private vistaPedidos view { get; set; }
        private vistaVisita Visita { get; set; }

        private Cliente v_objCliente = null;

        public ctrlPedidos(vistaPedidos pview, vistaVisita v_view)
        {
            view = pview;
            Visita = v_view;
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlPedidos").Id))
            {
                view.Title = "Pedidos BackOffice";
            }

            ppanel.IsVisible = true;
        }

        internal void ScreenInicialization(Cliente pobjCliente)
        {
            v_objCliente = pobjCliente;
            v_objCliente.v_listaFacturas.Clear();

            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlPedidos"));

            Logica_ManagerPedidos _manager = new Logica_ManagerPedidos();

            _manager.buscarListaPedidosBackOffice(view.FindByName<ListView>("pnlPedidos_ltvPedidos"), v_objCliente);
        }

        internal async Task menu_mniAgregar_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (!await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlPedidos_ltvPedidos")))
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention("No ha seleccionado ningún elemento.");
            }
            else
            {
                var Source = view.FindByName<ListView>("pnlPedidos_ltvPedidos").SelectedItem as pnlPedidos_ltvPedidos;

                await Visita.controlador.GenerarPedidosBackOffice(Source.NO_TRANSA);

                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

    }
}
