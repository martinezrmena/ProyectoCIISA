using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    public class ctrlEntregaPedidos
    {
        private vistaEntregaPedidos view { get; set; }

        internal ctrlEntregaPedidos(vistaEntregaPedidos pview)
        {
            view = pview;
        }

        internal void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);
            renderPaneles(view.FindByName<StackLayout>("pnlTransacciones"));

            Logica_ManagerEncabezadoPedido _manager = new Logica_ManagerEncabezadoPedido();

            _manager.buscarListaEncabezadosPedidos(view.FindByName<ListView>("pnlTransacciones_ltvDocumentos"));
        }

        public void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlTransacciones"));
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlTransaccionDetalle"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlTransacciones").Id))
            {
                view.Title = "Pedidos";
            }

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlTransaccionDetalle").Id))
            {
                view.Title = "Detalle Pedido";
            }

            ppanel.IsVisible = true;
        }

        internal void pnlTransaccionDetalles_SelectedIndexChanged()
        {
            renderPaneles(view.FindByName<StackLayout>("pnlTransaccionDetalle"));
        }
    }
}
