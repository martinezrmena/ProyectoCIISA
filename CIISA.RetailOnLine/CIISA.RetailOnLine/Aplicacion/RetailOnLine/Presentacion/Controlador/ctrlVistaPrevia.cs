using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.VistaControlador;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
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
    public  class ctrlVistaPrevia
    {
        private vistaVistaPrevia view { get; set; }

        internal ctrlVistaPrevia(vistaVistaPrevia pview)
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

            CargarReporte();
        }

        private void CargarReporte()
        {
            ProcesoImpresion _impresion = new ProcesoImpresion();

            string Data = string.Empty;

            switch (view.ReporteAVisualizar)
            {
                case TiposDeReportes.IndicadoresFacturacion:
                    Data = _impresion.VistaPreviaReporteIndicadoresFacturacion();
                    break;

                case TiposDeReportes.VentasProducto:
                    Data = _impresion.VistaPreviaReporteVentasPorProducto();
                    break;

                case TiposDeReportes.FacturaCredito:
                    Data = _impresion.VisualizarReporteDocumentosRealizados(ROLTransactions._facturaCreditoSigla);
                    break;

                case TiposDeReportes.ReporteTramites:
                    Data = _impresion.VisualizarReporteTramites();
                    break;

                case TiposDeReportes.ReporteAnulaciones:
                    Data = _impresion.VisualizarReporteAnulaciones();
                    break;

                case TiposDeReportes.ReporteRecaudaciones:
                    Data = _impresion.VisualizarReporteRecibosDinero(ROLTransactions._recaudacionSigla);
                    break;

                case TiposDeReportes.ReporteRegalias:
                    Data = _impresion.VisualizarReporteDocumentosRealizados(ROLTransactions._regaliaSigla);
                    break;

                case TiposDeReportes.ReporteDevoluciones:
                    Data = _impresion.VisualizarReporteDocumentosRealizados(ROLTransactions._devolucionSigla);
                    break;

                case TiposDeReportes.ReporteCotizaciones:
                    Data = _impresion.VisualizarReporteDocumentosRealizados(ROLTransactions._cotizacionSigla);
                    break;

                case TiposDeReportes.ReporteOrdenesDeVenta:
                    Data = _impresion.VisualizarReporteDocumentosRealizados(ROLTransactions._ordenVentaSigla);
                    break;

                case TiposDeReportes.ReporteRecibosDinero:
                    Data = _impresion.VisualizarReporteRecibosDinero(ROLTransactions._reciboDineroSigla);
                    break;

                case TiposDeReportes.ReporteFacturaContado:
                    Data = _impresion.VisualizarReporteDocumentosRealizados(ROLTransactions._facturaContadoSigla);
                    break;

                case TiposDeReportes.ReportePedidosSinAplicar:
                    Data = _impresion.VisualizarReportePedidosSinAplicar();
                    break;

                case TiposDeReportes.ReporteConsecutivoDocumentos:
                    Data = _impresion.VisualizarReporteConsecutivoDocumentos();
                    break;

                case TiposDeReportes.ReporteCrediticioDeLaRuta:
                    Data = _impresion.VisualizarReporteCrediticioDeLaRuta();
                    break;

                case TiposDeReportes.ReportePedidosRuta:
                    Data = _impresion.VisualizarReportePedidosRuta();
                    break;

                case TiposDeReportes.ReporteCrediticioDelCliente:
                    Data = _impresion.VisualizarReporteCrediticioDelCliente(view.v_objCliente);
                    break;

                default:
                    Data = string.Empty;
                    break;
            }

            view.FindByName<Label>("tbReporte").Text = Data;

            view.FindByName<Label>("lbTitulo").Text = view.ReporteAVisualizar.ToUpper();
        }

        public async void menu_mniImprimir_Click()
        {
            try
            {
                if (await LogMessages._dialogResultYes("¿Desea imprimir el " + view.ReporteAVisualizar + "?", "Impresión"))
                {
                    ProcesoImpresion _impresion = new ProcesoImpresion();

                    bool HayImpresoras = await _impresion.HayImpresorasConectadas();

                    if (HayImpresoras)
                    {
                        switch (view.ReporteAVisualizar)
                        {
                            case TiposDeReportes.IndicadoresFacturacion:
                                await _impresion.imprimirReporteIndicadoresFacturacion();
                                await LogMessageSuccess.Reporteprinted("indicadores de facturación");
                                break;

                            case TiposDeReportes.VentasProducto:
                                await _impresion.imprimirReporteVentasPorProducto();
                                await LogMessageSuccess.Reporteprinted("ventas por producto");
                                break;

                            case TiposDeReportes.FacturaCredito:
                                await _impresion.imprimirReporteDocumentosRealizados(ROLTransactions._facturaCreditoSigla);
                                await LogMessageSuccess.Reporteprinted("facturas de crédito");
                                break;

                            case TiposDeReportes.ReporteTramites:
                                await _impresion.imprimirReporteTramites();
                                await LogMessageSuccess.Reporteprinted("Trámites");
                                break;

                            case TiposDeReportes.ReporteAnulaciones:
                                await _impresion.imprimirReporteAnulaciones();
                                await LogMessageSuccess.Reporteprinted("anulaciones");
                                break;

                            case TiposDeReportes.ReporteRecaudaciones:
                                await _impresion.imprimirReporteRecibosDinero(ROLTransactions._recaudacionSigla);
                                await LogMessageSuccess.Reporteprinted("recaudaciones");
                                break;

                            case TiposDeReportes.ReporteRegalias:
                                await _impresion.imprimirReporteDocumentosRealizados(ROLTransactions._regaliaSigla);
                                await LogMessageSuccess.Reporteprinted("regalías");
                                break;

                            case TiposDeReportes.ReporteDevoluciones:
                                await _impresion.imprimirReporteDocumentosRealizados(ROLTransactions._devolucionSigla);
                                await LogMessageSuccess.Reporteprinted("devoluciones");
                                break;

                            case TiposDeReportes.ReporteCotizaciones:
                                await _impresion.imprimirReporteDocumentosRealizados(ROLTransactions._cotizacionSigla);
                                await LogMessageSuccess.Reporteprinted("cotizaciones");
                                break;

                            case TiposDeReportes.ReporteOrdenesDeVenta:
                                await _impresion.imprimirReporteDocumentosRealizados(ROLTransactions._ordenVentaSigla);
                                await LogMessageSuccess.Reporteprinted("ordenes de venta");
                                break;

                            case TiposDeReportes.ReporteRecibosDinero:
                                await _impresion.imprimirReporteRecibosDinero(ROLTransactions._reciboDineroSigla);
                                await LogMessageSuccess.Reporteprinted("recibos de dinero");
                                break;

                            case TiposDeReportes.ReporteFacturaContado:
                                await _impresion.imprimirReporteDocumentosRealizados(ROLTransactions._facturaContadoSigla);
                                await LogMessageSuccess.Reporteprinted("facturas de contado");
                                break;

                            case TiposDeReportes.ReportePedidosSinAplicar:
                                await _impresion.imprimirReportePedidosSinAplicar();
                                await LogMessageSuccess.Reporteprinted("pedidos sin aplicar");
                                break;

                            case TiposDeReportes.ReporteConsecutivoDocumentos:
                                await _impresion.imprimirReporteConsecutivoDocumentos();
                                await LogMessageSuccess.Reporteprinted("consecutivos");
                                break;

                            case TiposDeReportes.ReporteCrediticioDeLaRuta:
                                await _impresion.imprimirReporteCrediticioDeLaRuta();
                                await LogMessageSuccess.Reporteprinted("crediticio de la ruta");
                                break;

                            case TiposDeReportes.ReportePedidosRuta:
                                await _impresion.imprimirReportePedidosRuta();
                                await LogMessageSuccess.Reporteprinted("pedidos");
                                break;

                            case TiposDeReportes.ReporteCrediticioDelCliente:
                                await _impresion.imprimirReporteCrediticioDelCliente(view.v_objCliente);
                                await LogMessageSuccess.Reporteprinted("crediticio del cliente");
                                break;

                            default:
                                break;
                        }
                    }
                }
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
    }
}
