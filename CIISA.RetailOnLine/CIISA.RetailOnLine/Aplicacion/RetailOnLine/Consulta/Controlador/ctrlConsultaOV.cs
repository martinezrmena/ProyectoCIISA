using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Consulta.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Compress;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.TablesNAF;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Common.ValidateHH;
using CIISA.RetailOnLine.Framework.External.CustomTreeView;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using CIISA.RetailOnLine.Framework.Server.WS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Consulta.Controlador
{
    public class ctrlConsultaOV
    {
        private vistaConsultaOV view { get; set; }
        private int Contador = 0;
        private DataTable v_dt = null;

        internal ctrlConsultaOV(vistaConsultaOV pview)
        {
            view = pview;

        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlConsultaOV").Id))
            {
                view.Title = "Ordenes Venta Tránsmitidas";
            }

            ppanel.IsVisible = true;
        }

        private void llenarTreeViewOrdenesDeVenta()
        {
            if (DataTableValidate.validateDataTable(v_dt))
            {
                foreach (DataRow _fila in v_dt.Rows)
                {
                    string _codDocumento = _fila[TableDetalleDocumentoNAF.NO_TRANSA].ToString();

                    string _codCliente = _fila[TableEncabezadoDocumentoNAF.NO_CLIENTE].ToString();

                    Logica_ManagerCliente _manager = new Logica_ManagerCliente();

                    string _nombre = _manager.buscarNombreClientePorCodigoCliente(
                                        _fila[TableEncabezadoDocumentoNAF.NO_CLIENTE].ToString()
                                        );

                    _codDocumento = _codDocumento + " / " + _codCliente + " / " + _nombre;

                    Util _util = new Util();

                    _util.evaluateAddNode(
                        view.FindByName<ListView>("pnlConsultaOV_trvOrdenesVenta"),
                        _codDocumento
                        );
                }

                var Source = view.FindByName<ListView>("pnlConsultaOV_trvOrdenesVenta").ItemsSource as ObservableCollection<CollapsableItem>;

                if (Source != null) {

                    foreach (var _tn in Source)
                    {
                        foreach (DataRow _fila in v_dt.Rows)
                        {
                            string _codDocumento = _fila[TableDetalleDocumentoNAF.NO_TRANSA].ToString();

                            string _codCliente = _fila[TableEncabezadoDocumentoNAF.NO_CLIENTE].ToString();

                            Logica_ManagerCliente _manager = new Logica_ManagerCliente();

                            string _nombre = _manager.buscarNombreClientePorCodigoCliente(
                                        _fila[TableEncabezadoDocumentoNAF.NO_CLIENTE].ToString()
                                        );

                            _codDocumento = _codDocumento + " / " + _codCliente + " / " + _nombre;

                            //if (_tn.Text.ToString() == _codDocumento)
                            if (_tn.Encabezado.ToString() == _codDocumento)
                            {
                                string _linea = _fila[TableDetalleDocumentoNAF.NO_LINEA].ToString();
                                string _codProducto = _fila[TableDetalleDocumentoNAF.NO_ARTI].ToString();

                                string _cantidad = _fila[TableDetalleDocumentoNAF.CANTIDAD].ToString();
                                decimal _cantidadTemp = FormatUtil.convertStringToDecimal(_cantidad);
                                _cantidad = FormatUtil.applyCurrencyFormat(_cantidadTemp);

                                string _especificacion = _fila[TableDetalleDocumentoNAF.COMENTARIO].ToString();

                                string _embalaje = _fila[TableDetalleDocumentoNAF.EMBALAGE].ToString();

                                if (_embalaje.Equals("0"))
                                {
                                    _embalaje = PaymentForm._notApplyInitials;
                                }

                                string _tstamp = _fila[TableDetalleDocumentoNAF.TSTAMP].ToString();

                                _tn.Detalle = _tn.Detalle + "-> "
                                    + _linea + " / "
                                    + _codProducto + " / "
                                    + _cantidad + " / "
                                    + _especificacion + " / "
                                    + _embalaje + " / "
                                    + Simbol._squareBracketLeft
                                    + _tstamp
                                    + Simbol._squareBracketRight
                                    + Environment.NewLine;

                                Contador = Contador + 1;
                            }
                        }
                    }
                }
                
            }
        }

        private async void cargarOrdenesDeVenta()
        {
            var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

            try {

                if (await _testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
                {
                    var ServicioPassedOn = DependencyService.Get<IService_WebServicePassedOn>();

                    decimal valor = FormatUtil.convertStringToDecimal(view.FindByName<Label>("VisualStepper").Text);

                    string _memoryStream = ServicioPassedOn.consultaOrdenesVentaTransmitidas(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), valor);

                    if (!_memoryStream.Equals(TypeEvents._errorWS))
                    {
                        var _memoryCompress = DependencyService.Get<IMC_HH>();

                        v_dt = _memoryCompress.Unzip_HH_DataTable("cargarOrdenesDeVenta", TypeTransaction._select, _memoryStream, TablesSROLNAF._detalleDocumento);

                        ServicioPassedOn.Dispose();

                        Device.BeginInvokeOnMainThread(() =>
                            llenarTreeViewOrdenesDeVenta()
                        );
                        
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                Device.BeginInvokeOnMainThread(() => {
                    view.FindByName<Label>("pnlConsultaOV_lblCantidadOV").Text =
                    "Cantidad Ordenes de Venta: "
                    + Contador;
                });
                
                //+ view.pnlConsultaOV_trvOrdenesVenta.Nodes.Count;
                Contador = 0;

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

        public void ScreenInicialization()
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                RenderWindows.paintWindow(view);

                renderPaneles(view.FindByName<StackLayout>("pnlConsultaOV"));
            });

            cargarOrdenesDeVenta();
        }

        public void menu_mniConsultar_Click()
        {
            Device.BeginInvokeOnMainThread(() =>
                
                view.FindByName<ListView>("pnlConsultaOV_trvOrdenesVenta").ItemsSource = new ObservableCollection<CollapsableItem>()

            );

            cargarOrdenesDeVenta();
        }

        internal void menu_mniClose_Click()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        internal async Task pnlConsultaOV_btnImprimir_Click()
        {
            if (await DataTableValidate.validateDataTable(v_dt,"Sin información para imprimir"))
            {
                ProcesoImpresion _impresion = new ProcesoImpresion();

                await _impresion.imprimirReporteEnLineaOrdenesVenta(v_dt);
            }
        }

        internal async Task pnlConsultaOV_btnPruebaConexion_Click()
        {
            var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

            await _testConnectionSROL.testConnectionString(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true);
        }
    }
}
