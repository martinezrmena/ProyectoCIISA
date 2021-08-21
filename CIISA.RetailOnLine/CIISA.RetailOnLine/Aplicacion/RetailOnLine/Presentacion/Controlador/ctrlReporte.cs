using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.VistaControlador;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.Controlador
{
    public class ctrlReporte
    {
        private vistaReporte view { get; set; }

        internal ctrlReporte(vistaReporte pview)
        {
            view = pview;
        }

        public void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlReporte").Id))
            {
                view.Title = "Reportes";
            }

            ppanel.IsVisible = true;
        }

        internal void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlReporte"));
        }

        internal async Task pnlReporte_btnIndicadores_Click()
        {
            await App.Current.MainPage.Navigation.PushAsync(new vistaVistaPrevia(TiposDeReportes.IndicadoresFacturacion));
        }

        internal async Task pnlReporte_btnVentasProducto_Click()
        {
            await App.Current.MainPage.Navigation.PushAsync(new vistaVistaPrevia(TiposDeReportes.VentasProducto));
        }

        internal async Task pnlReporte_btnFacturaCredito_Click()
        {
            await App.Current.MainPage.Navigation.PushAsync(new vistaVistaPrevia(TiposDeReportes.FacturaCredito));

        }

        internal async Task pnlReporte_btnTramite_Click()
        {
            await App.Current.MainPage.Navigation.PushAsync(new vistaVistaPrevia(TiposDeReportes.ReporteTramites));
        }

        internal async Task pnlReporte_btnAnulacion_Click()
        {
            await App.Current.MainPage.Navigation.PushAsync(new vistaVistaPrevia(TiposDeReportes.ReporteAnulaciones));
        }

        internal async Task pnlReporte_btnRecaudacion_Click()
        {
            await App.Current.MainPage.Navigation.PushAsync(new vistaVistaPrevia(TiposDeReportes.ReporteRecaudaciones));

        }

        internal async Task pnlReporte_btnRegalia_Click()
        {
            await App.Current.MainPage.Navigation.PushAsync(new vistaVistaPrevia(TiposDeReportes.ReporteRegalias));
        }

        internal async Task pnlReporte_btnDevolucion_Click()
        {
            await App.Current.MainPage.Navigation.PushAsync(new vistaVistaPrevia(TiposDeReportes.ReporteDevoluciones));
        }

        internal async Task pnlReporte_btnCotizacion_Click()
        {
            await App.Current.MainPage.Navigation.PushAsync(new vistaVistaPrevia(TiposDeReportes.ReporteCotizaciones));
        }

        internal async Task pnlReporte_btnOrdenesVenta_Click()
        {
            await App.Current.MainPage.Navigation.PushAsync(new vistaVistaPrevia(TiposDeReportes.ReporteOrdenesDeVenta));
        }

        internal async Task pnlReporte_btnRecibosDinero_Click()
        {
            await App.Current.MainPage.Navigation.PushAsync(new vistaVistaPrevia(TiposDeReportes.ReporteRecibosDinero));
        }

        internal async Task pnlReporte_btnFacturaContado_Click()
        {
            await App.Current.MainPage.Navigation.PushAsync(new vistaVistaPrevia(TiposDeReportes.ReporteFacturaContado));

        }

        internal async Task pnlReporte_btnPedidosSinAplicar_Click()
        {
            await App.Current.MainPage.Navigation.PushAsync(new vistaVistaPrevia(TiposDeReportes.ReportePedidosSinAplicar));
        }

        internal async Task pnlReporte_btnConsecutivos_Click()
        {
            await App.Current.MainPage.Navigation.PushAsync(new vistaVistaPrevia(TiposDeReportes.ReporteConsecutivoDocumentos));
        }

        internal async Task pnlReporte_btnCrediticioDeLaRuta_Click()
        {
            await App.Current.MainPage.Navigation.PushAsync(new vistaVistaPrevia(TiposDeReportes.ReporteCrediticioDeLaRuta));
            
        }

        internal async Task pnlReporte_btnPedidos_Click()
        {
            await App.Current.MainPage.Navigation.PushAsync(new vistaVistaPrevia(TiposDeReportes.ReportePedidosRuta));
        }

        internal async Task pnlReporte_btnCrediticioDelCliente_Click()
        {
            //if (await LogMessages._dialogResultYes("¿Desea imprimir el reporte crediticio del cliente?", "Impresión"))
            //{                
                ShowPresentacion _show = new ShowPresentacion();

                await _show.mostrarPantallaCliente(view, true);                
            //}
        }

        public async Task ReporteCrediticioDelCliente()
        {
            if (!view.v_objCliente.v_no_cliente.Equals(string.Empty))
            {
                await App.Current.MainPage.Navigation.PushAsync(new vistaVistaPrevia(TiposDeReportes.ReporteCrediticioDelCliente,view.v_objCliente));
            }
        }

        internal void menu_mniCerrar_Click()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
