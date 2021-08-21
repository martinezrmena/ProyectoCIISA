using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.InventoryOnLine._View;
using CIISA.RetailOnLine.Aplicacion.InventoryOnLine.Modelo;
using CIISA.RetailOnLine.Aplicacion.InventoryOnLine.ViewController;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.InventoryOnLine.ListViewMoldels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Sincronizacion;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;
using CIISA.RetailOnLine.Framework.Handheld.Calculator.ViewController;
using CIISA.RetailOnLine.Framework.External.ResizableColumnsListView;
using Acr.UserDialogs;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.InventoryOnLine.Controller
{
    public class ctrlTomaFisica
    {
        private vistaTomaFisica view { get; set; }
        private int v_fila = Numeric._zeroInteger;
        private TemplateStyles Style = new TemplateStyles();
        private bool HiloCorriendo { get; set; }

        internal ctrlTomaFisica(vistaTomaFisica pview)
        {
            view = pview;
            HiloCorriendo = true;
        }

        public async void ScreenInicialization()
        {
            if (HiloCorriendo)
            {
                view.FindByName<StackLayout>("pnlProcedimiento").IsVisible = false;
                view.FindByName<StackLayout>("pnlTomaFisica").IsVisible = false;
                view.FindByName<StackLayout>("pnlFinalizar").IsVisible = false;
            }

            var v_testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

            if (await v_testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
            {
                Descarga_ManagerGenerico _managerGenerico = new Descarga_ManagerGenerico();

                _managerGenerico.marcarDatosComoNoEnvidos();

                SincronizacionInformacion _sincronizacion = new SincronizacionInformacion();

                await _sincronizacion.informacionSincronizar();
            }

            Device.BeginInvokeOnMainThread(async () => {

                Logica_ManagerInventario _managerInventarioLogica = new Logica_ManagerInventario();

                string _estadoInventario = _managerInventarioLogica.buscarEstadoConsolidado();

                if (_estadoInventario.Equals(SQL._Si))
                {
                    renderPaneles(view.FindByName<StackLayout>("pnlFinalizar"));
                }
                else
                {
                    _managerInventarioLogica.recalcularProductoDisponibleEnInventario();

                    renderPaneles(view.FindByName<StackLayout>("pnlProcedimiento"));

                    Inventario_ManagerInventario _managerInventarioInventario = new Inventario_ManagerInventario();

                    _managerInventarioInventario.buscarInventarioTomaFisica(view.FindByName<ListView>("pnlTomaFisica_ltvInventario"));
                }

                renderMenu(true, _estadoInventario);
                renderComponents(_estadoInventario);
                try
                {
                    ProcesoImpresion _impresion = new ProcesoImpresion();
                    await _impresion.imprimirReporteInventarioConExistencias("INVENTARIO CON EXISTENCIAS [TF]");
                }
                catch (Exception)
                {
                    LogMessageAttention _logMessageAttention = new LogMessageAttention();
                    await _logMessageAttention.generalAttention("No se imprimió el inventario con existencias.");

                }
                finally
                {
                    UserDialogs.Instance.HideLoading();
                }
            }
            );

            HiloCorriendo = false;
        }

        private void renderComponents(string pestadoInventario)
        {
            if (pestadoInventario.Equals(SQL._No))
            {
                actualizarColumnasCodigoProducto();
            }

            view.FindByName<ExtendedEntry>("pnlTomaFisica_txtCantidad").Text = string.Empty;
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlTomaFisica"));
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlProcedimiento"));
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlFinalizar"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlTomaFisica").Id))
            {
                view.Title = "Toma Física";
            }

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlProcedimiento").Id))
            {
                view.Title = "Procedimiento";
                view.FindByName<StackLayout>("pnlProcedimiento").IsVisible = true;
            }

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlFinalizar").Id))
            {
                view.Title = "Finalizar";
            }

            ppanel.IsVisible = true;
        }

        private void renderMenu(bool pprimerIngreso, string pconsolidado)
        {            
            view.ToolbarItems.Clear();

            if (!pprimerIngreso)
            {
                if (view.FindByName<StackLayout>("pnlTomaFisica").IsVisible)
                {
                    view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniConsolidar"));
                    view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniAgregarCantidad"));
                    view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniProcedimiento"));
                    view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniRecalcularInventario"));
                    view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniClose"));
                    view.FindByName<CheckBox>("pnlTomaFisica_chkAjustarColumnas").Checked = true;
                }
                else
                {
                    if (view.FindByName<StackLayout>("pnlProcedimiento").IsVisible)
                    {
                        view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniContinuar"));
                        view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniClose"));
                    }

                    if (view.FindByName<StackLayout>("pnlFinalizar").IsVisible)
                    {
                        view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniClose"));
                    }
                }
            }
            else
            {
                if (pconsolidado.Equals(SQL._Si))
                {
                    view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniClose"));
                }
                else
                {

                    view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniContinuar"));
                    view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniClose"));
                }
            }
        }

        private void actualizarColumnasCodigoProducto()
        {
            MiscUtils _miscUtils = new MiscUtils();
            _miscUtils.quantityListViewItems<pnlTomaFisica_ltvInventario>(view.FindByName<ListView>("pnlTomaFisica_ltvInventario"), view.FindByName<Label>("pnlTomaFisica_clhCodigo"), "Código");
        }

        internal void pnlTomaFisica_ltvInventario_ItemActivate()
        {
            var Source = view.FindByName<ListView>("pnlTomaFisica_ltvInventario").ItemsSource as ObservableCollection<pnlTomaFisica_ltvInventario>;
            var Seleccionado = view.FindByName<ListView>("pnlTomaFisica_ltvInventario").SelectedItem as pnlTomaFisica_ltvInventario;
            v_fila = Source.IndexOf(Seleccionado);
            //view.FindByName<ExtendedEntry>("pnlTomaFisica_txtCantidad").Focus();
        }

        #region btnTeclado

        internal void pnlTomaFisica_btnPuntoDecimal_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.punto(view.FindByName<ExtendedEntry>("pnlTomaFisica_txtCantidad"));
        }

        internal void pnlTomaFisica_btnZero_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.zero(view.FindByName<ExtendedEntry>("pnlTomaFisica_txtCantidad"));
        }

        internal void pnlTomaFisica_btnOne_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.one(view.FindByName<ExtendedEntry>("pnlTomaFisica_txtCantidad"));
        }

        internal void pnlTomaFisica_btnTwo_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.two(view.FindByName<ExtendedEntry>("pnlTomaFisica_txtCantidad"));
        }

        internal void pnlTomaFisica_btnThree_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.three(view.FindByName<ExtendedEntry>("pnlTomaFisica_txtCantidad"));
        }

        internal void pnlTomaFisica_btnFour_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.four(view.FindByName<ExtendedEntry>("pnlTomaFisica_txtCantidad"));
        }

        internal void pnlTomaFisica_btnFive_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.five(view.FindByName<ExtendedEntry>("pnlTomaFisica_txtCantidad"));
        }

        internal void pnlTomaFisica_btnSix_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.six(view.FindByName<ExtendedEntry>("pnlTomaFisica_txtCantidad"));
        }

        internal void pnlTomaFisica_btnSeven_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.seven(view.FindByName<ExtendedEntry>("pnlTomaFisica_txtCantidad"));
        }

        internal void pnlTomaFisica_btnEight_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.eight(view.FindByName<ExtendedEntry>("pnlTomaFisica_txtCantidad"));
        }

        internal void pnlTomaFisica_btnNine_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.nine(view.FindByName<ExtendedEntry>("pnlTomaFisica_txtCantidad"));
        }

        internal void pnlTomaFisica_btnBorrar_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlTomaFisica_txtCantidad"));
        }
        #endregion

        internal async Task mniAgregarCantidad_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (!_validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlTomaFisica_txtCantidad")))
            {
                if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlTomaFisica_ltvInventario")))
                {
                    pnlTomaFisica_ltvInventario _lvi = view.FindByName<ListView>("pnlTomaFisica_ltvInventario").SelectedItem as pnlTomaFisica_ltvInventario;

                    decimal _cantidad = FormatUtil.convertStringToDecimal(_lvi.Cantidad);
                    decimal _tomaFisica = FormatUtil.convertStringToDecimal(view.FindByName<ExtendedEntry>("pnlTomaFisica_txtCantidad").Text);

                    _lvi.TomaFisica = FormatUtil.applyCurrencyFormat(_tomaFisica);
                    _lvi.Diferencia = FormatUtil.applyCurrencyFormat(_tomaFisica - _cantidad);
                    _lvi.ItemTextColor = (Color)App.Current.Resources["RedColor"];

                    if (_cantidad >= 500)
                    {
                        LogMessageAttention _logMessageAttention = new LogMessageAttention();
                        await _logMessageAttention.generalAttention("Esta ingresando la cantidad de: " + _cantidad + ".");
                    }


                    var Source = view.FindByName<ListView>("pnlTomaFisica_ltvInventario").ItemsSource as ObservableCollection<pnlTomaFisica_ltvInventario>;
                    var Seleccionado = view.FindByName<ListView>("pnlTomaFisica_ltvInventario").SelectedItem as pnlTomaFisica_ltvInventario;
                    v_fila = Source.IndexOf(Seleccionado);

                    Source.RemoveAt(v_fila);
                    Source.Insert(v_fila, _lvi);

                    view.FindByName<ExtendedEntry>("pnlTomaFisica_txtCantidad").Text = string.Empty;
                    view.FindByName<ListView>("pnlTomaFisica_ltvInventario").ItemsSource = Source;
                    view.FindByName<ListView>("pnlTomaFisica_ltvInventario").Focus();
                }
            }
        }

        public async void menu_mniContinuar_Click()
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    renderPaneles(view.FindByName<StackLayout>("pnlTomaFisica"));
                    renderMenu(false, SQL._No);
                });
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

        public async void menu_mniProcedimiento_Click()
        {
            try
            {
                Device.BeginInvokeOnMainThread(()=> {
                    renderPaneles(view.FindByName<StackLayout>("pnlProcedimiento"));
                    renderMenu(false, SQL._No);
                });
                
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

        internal async Task menu_mniRecalcularInventario_Click()
        {
            if (await LogMessages._dialogResultYes("Se perderán las cantidades ingresadas, ¿Desea Continuar?", "Alerta"))
            {
                Logica_ManagerInventario _managerLogicaInventario = new Logica_ManagerInventario();

                _managerLogicaInventario.recalcularProductoDisponibleEnInventario();

                Inventario_ManagerInventario _managerInventarioInventario = new Inventario_ManagerInventario();

                _managerInventarioInventario.buscarInventarioTomaFisica(view.FindByName<ListView>("pnlTomaFisica_ltvInventario"));

                view.FindByName<ListView>("pnlTomaFisica_ltvInventario").SelectedItem = null;

                view.FindByName<CheckBox>("pnlTomaFisica_chkAjustarColumnas").Checked = false;
            }
        }

        internal async Task pnlTomaFisica_btnCalculadora_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlTomaFisica_ltvInventario")))
            {
                pnlTomaFisica_ltvInventario _lvi = view.FindByName<ListView>("pnlTomaFisica_ltvInventario").SelectedItem as pnlTomaFisica_ltvInventario;
                
                string _cantidadUno = _lvi.Cantidad;
                decimal _cantidadDos = FormatUtil.convertStringToDecimal(_cantidadUno);

                ShowCalculator _show = new ShowCalculator();
                _show.showCalculatorForm(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), _cantidadDos, this);
            }
        }

        internal async Task pnlTomaFisica_btnCalculadora_ClickParte2(decimal _cantidad)
        {
            ValidateHH _validateHH = new ValidateHH();

            if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlTomaFisica_ltvInventario"))) {

                var _lvi = view.FindByName<ListView>("pnlTomaFisica_ltvInventario").SelectedItem as pnlTomaFisica_ltvInventario;
                _lvi.Diferencia = FormatUtil.applyCurrencyFormat(Convert.ToDecimal(_lvi.TomaFisica) - _cantidad);
                _lvi.Cantidad = FormatUtil.applyCurrencyFormat(_cantidad);

                var Source = view.FindByName<ListView>("pnlTomaFisica_ltvInventario").ItemsSource as ObservableCollection<pnlTomaFisica_ltvInventario>;
                var Seleccionado = view.FindByName<ListView>("pnlTomaFisica_ltvInventario").SelectedItem as pnlTomaFisica_ltvInventario;
                v_fila = Source.IndexOf(Seleccionado);

                Source.RemoveAt(v_fila);
                Source.Insert(v_fila, _lvi);

                view.FindByName<ExtendedEntry>("pnlTomaFisica_txtCantidad").Text = string.Empty;
                view.FindByName<ListView>("pnlTomaFisica_ltvInventario").ItemsSource = Source;
            }
        }

        internal async Task menu_mniClose_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea Salir?", "Salir"))
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        internal void pnlTomaFisica_btnLimpiar_Click()
        {
            view.FindByName<ExtendedEntry>("pnlTomaFisica_txtCantidad").Text = string.Empty;
        }

        internal void pnlTomaFisica_chkAjustarColumnas_CheckStateChanged()
        {
            if (view.FindByName<CheckBox>("pnlTomaFisica_chkAjustarColumnas").Checked)
            {
                view.FindByName<Grid>("pnlTomaFisicaGridDefinitions").ColumnDefinitions[0].Width = 40;
                view.FindByName<Grid>("pnlTomaFisicaGridDefinitions").ColumnDefinitions[1].Width = 0;
                view.FindByName<Grid>("pnlTomaFisicaGridDefinitions").ColumnDefinitions[2].Width = 80;
                view.FindByName<Grid>("pnlTomaFisicaGridDefinitions").ColumnDefinitions[3].Width = 80;
                view.FindByName<Grid>("pnlTomaFisicaGridDefinitions").ColumnDefinitions[4].Width = 80;
                AjustarColumnas(true);
            }
            else
            {
                view.FindByName<Grid>("pnlTomaFisicaGridDefinitions").ColumnDefinitions[0].Width = 60;
                view.FindByName<Grid>("pnlTomaFisicaGridDefinitions").ColumnDefinitions[1].Width = 270;
                view.FindByName<Grid>("pnlTomaFisicaGridDefinitions").ColumnDefinitions[2].Width = 75;
                view.FindByName<Grid>("pnlTomaFisicaGridDefinitions").ColumnDefinitions[3].Width = 140;
                view.FindByName<Grid>("pnlTomaFisicaGridDefinitions").ColumnDefinitions[4].Width = 85;
                AjustarColumnas(false);
            }
        }

        internal void AjustarColumnas(bool value) {
            CustomDataTemplateSelectorTF FormatListView = new CustomDataTemplateSelectorTF();
            ObservableCollection<pnlTomaFisica_ltvInventario> Collection = new ObservableCollection<pnlTomaFisica_ltvInventario>();

            if (value)
            {
                Collection = FormatListView.ChangeStyleListViewTF(view.FindByName<ListView>("pnlTomaFisica_ltvInventario"), Style.ContractedStyle);
            }
            else {
                Collection = FormatListView.ChangeStyleListViewTF(view.FindByName<ListView>("pnlTomaFisica_ltvInventario"), Style.ExpandedStyle);
            }

            view.FindByName<ListView>("pnlTomaFisica_ltvInventario").ItemsSource = Collection;
        }

        public async void pnlFinalizar_btnImprimir_Click()
        {
            try
            {

                Logica_ManagerInventario _manager = new Logica_ManagerInventario();

                string _state = _manager.buscarEstadoConsolidado();

                if (_state.Equals(SQL._Si))
                {
                    ProcesoImpresion _impresion = new ProcesoImpresion();

                    await _impresion.imprimirReporteInventarioTomaFisica();

                }
                else
                {
                    LogMessageAttention _logMessageAttention = new LogMessageAttention();
                    await _logMessageAttention.generalAttention("Debe consolidar primero la toma física");
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

        internal async void pnlFinalizar_btnPruebaConexion_Click()
        {
            var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();
            await _testConnectionSROL.testConnectionString(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true);
        }

        public async void pnlFinalizar_btnSincronizar_Click()
        {
            try
            {
                if (await LogMessages._dialogResultYes("¿Desea sincronizar la Toma Física?", "Toma Física"))
                {
                    var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();
                    Descarga_ManagerEnvio _manager = new Descarga_ManagerEnvio();
                    await _manager.EnvioPaqueteInformacionPorWS_TomaFisica();

                    LogMessageAttention _lma = new LogMessageAttention();
                    await _lma.generalAttention("Sincronizó la toma física");
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

        public async void pnlFinalizar_btnCerrarMaquina_Click()
        {
            try {

                CerradoHandheld _cerrado = new CerradoHandheld();

                await _cerrado.CerrarMaquina(false);
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

        public async void menu_mniConsolidar_Click()
        {
            try
            {
                if (await LogMessages._dialogResultYes("¿Desea consolidar la Toma Física?", "Toma Física"))
                {
                    Inventario_ManagerInventario _manager = new Inventario_ManagerInventario();

                    _manager.actualizarInventarioTomaFisica(view.FindByName<ListView>("pnlTomaFisica_ltvInventario"));

                    _manager.actualizarConsolidado();

                    await LogMessageSuccess._generalSuccess("Consolidó la toma física");

                    Device.BeginInvokeOnMainThread(()=> {
                        renderPaneles(view.FindByName<StackLayout>("pnlFinalizar"));
                        renderMenu(false, SQL._No);
                    });
                    
                    ProcesoImpresion _impresion = new ProcesoImpresion();

                    await _impresion.imprimirReporteInventarioTomaFisica();
                }
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally {

                UserDialogs.Instance.HideLoading();
            }
        }
        
    }
}