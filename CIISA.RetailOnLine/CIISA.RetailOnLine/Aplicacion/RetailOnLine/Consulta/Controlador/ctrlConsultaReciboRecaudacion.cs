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
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
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
    public class ctrlConsultaReciboRecaudacion
    {
        private vistaConsultaReciboRecaudacion view { get; set; }
        private int Contador = 0;
        private DataTable v_dt = null;

        internal ctrlConsultaReciboRecaudacion(vistaConsultaReciboRecaudacion pview)
        {
            view = pview;

        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlConsultaDocumentos").Id))
            {
                view.Title = "Recibos/Recaudaciones Trásmitidas";
            }

            ppanel.IsVisible = true;
        }

        private void llenarTreeViewRecibosRecaudaciones()
        {
            if (DataTableValidate.validateDataTable(v_dt))
            {

                foreach (DataRow _fila in v_dt.Rows)
                {
                    string _tipoDoc = _fila[TableDetalleReciboNAF.TIPO_DOC].ToString();

                    string _codDocumento = _fila[TableDetalleReciboNAF.NO_TRANSA].ToString();

                    string _codCliente = _fila[TableEncabezadoReciboNAF.NO_CLIENTE].ToString();

                    Logica_ManagerCliente _manager = new Logica_ManagerCliente();

                    string _nombre = _manager.buscarNombreClientePorCodigoCliente(
                                _fila[TableEncabezadoReciboNAF.NO_CLIENTE].ToString()
                                );

                    _codDocumento = _tipoDoc + " / " + _codDocumento + " / " + _codCliente + " / " + _nombre;

                    Util _util = new Util();

                    _util.evaluateAddNode(
                        view.FindByName<ListView>("pnlConsultaDocumentos_trvDocumentos"),
                        _codDocumento
                        );

                }

                var Source = view.FindByName<ListView>("pnlConsultaDocumentos_trvDocumentos").ItemsSource as ObservableCollection<CollapsableItem>;

                if (Source != null) {

                    foreach (var _tn in Source)
                    {
                        foreach (DataRow _fila in v_dt.Rows)
                        {
                            string _tipoDoc = _fila[TableDetalleReciboNAF.TIPO_DOC].ToString();

                            string _codDocumento = _fila[TableDetalleReciboNAF.NO_TRANSA].ToString();

                            string _codCliente = _fila[TableEncabezadoReciboNAF.NO_CLIENTE].ToString();

                            Logica_ManagerCliente _manager = new Logica_ManagerCliente();

                            string _nombre = _manager.buscarNombreClientePorCodigoCliente(
                                        _fila[TableEncabezadoReciboNAF.NO_CLIENTE].ToString()
                                        );

                            _codDocumento = _tipoDoc + " / " + _codDocumento + " / " + _codCliente + " / " + _nombre;

                            if (_tn.Encabezado.ToString() == _codDocumento)
                            {
                                string _noFactura = _fila[TableDetalleReciboNAF.NO_FACTURA].ToString();

                                if (_noFactura.Equals(string.Empty))
                                {
                                    _noFactura = PaymentForm._notApplyInitials;
                                }

                                string _monto = _fila[TableDetalleReciboNAF.MONTO].ToString();
                                decimal _monto2 = FormatUtil.convertStringToDecimal(_monto);
                                _monto = FormatUtil.applyCurrencyFormat(_monto2);

                                string _saldo = _fila[TableEncabezadoReciboNAF.SALDO].ToString();
                                decimal _saldo2 = FormatUtil.convertStringToDecimal(_saldo);
                                _saldo = FormatUtil.applyCurrencyFormat(_saldo2);

                                string _tstamp = _fila[TableEncabezadoReciboNAF.TSTAMP].ToString();

                                _tn.Detalle = _tn.Detalle + "-> "
                                    + _noFactura
                                    + " / "
                                    + _monto
                                    + " / "
                                    + _saldo
                                    + " / "
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

        private async void cargarReciboRecaudacion()
        {
            try {

                var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

                if (await _testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
                {
                    var ServicioPassedOn = DependencyService.Get<IService_WebServicePassedOn>();

                    decimal valor = FormatUtil.convertStringToDecimal(view.FindByName<Label>("VisualStepper").Text);

                    string _memoryStream = ServicioPassedOn.consultaRecibosRecaudacionesTransmitidas(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), valor);

                    if (!_memoryStream.Equals(TypeEvents._errorWS))
                    {
                        var _memoryCompress = DependencyService.Get<IMC_HH>();

                        v_dt = _memoryCompress.Unzip_HH_DataTable("cargarReciboRecaudacion", TypeTransaction._select, _memoryStream, TablesSROLNAF._detalleRecibo);

                        ServicioPassedOn.Dispose();

                        Device.BeginInvokeOnMainThread(() =>
                            llenarTreeViewRecibosRecaudaciones()
                        );

                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                Device.BeginInvokeOnMainThread(() =>
                    view.FindByName<Label>("pnlConsultaDocumentos_lblCantidadOV").Text =
                    "Cantidad Documentos: "
                    + Contador
                );

                //+ view.pnlConsultaDocumentos_trvDocumentos.Nodes.Count;
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
            Device.BeginInvokeOnMainThread(()=>{

                RenderWindows.paintWindow(view);

                renderPaneles(view.FindByName<StackLayout>("pnlConsultaDocumentos"));
            });

            cargarReciboRecaudacion();
        }

        public void menu_mniConsultar_Click()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                view.FindByName<ListView>("pnlConsultaDocumentos_trvDocumentos").ItemsSource = new ObservableCollection<CollapsableItem>();

            });

            cargarReciboRecaudacion();
        }

        internal async Task menu_mniClose_Click()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        internal async Task pnlConsultaReciboRecaudacion_btnPruebaConexion_Click()
        {
            var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();
            await _testConnectionSROL.testConnectionString(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true);
        }

        internal async Task pnlConsultaReciboRecaudacion_btnImprimir_Click()
        {
            if (await DataTableValidate.validateDataTable(v_dt,"Sin información para imprimir"))
            {
                ProcesoImpresion _impresion = new ProcesoImpresion();

                await _impresion.imprimirReporteEnLineaReciboRecaudacion(v_dt);
            }
        }

        internal async Task pnlConsultaReciboRecaudacion_btnNomenclatura_Click()
        {
            LogMessageAttention _logMessageAttention = new LogMessageAttention();
            await _logMessageAttention.generalAttention(
                ROLTransactions._reciboDineroSigla + " = " + ROLTransactions._reciboDineroNombre
                + Environment.NewLine
                + ROLTransactions._recaudacionSigla + " = " + ROLTransactions._recaudacionNombre
                );
        }
    }
}
