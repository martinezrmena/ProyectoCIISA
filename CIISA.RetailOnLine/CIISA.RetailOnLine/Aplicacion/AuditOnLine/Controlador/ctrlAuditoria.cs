using CIISA.RetailOnLine.Aplicacion.AuditOnLine.ListViewModels;
using CIISA.RetailOnLine.Aplicacion.AuditOnLine.Modelo;
using CIISA.RetailOnLine.Aplicacion.AuditOnLine.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.External.ResizableColumnsListView;
using CIISA.RetailOnLine.Framework.Handheld.Calculator.ViewController;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.AuditOnLine.Controlador
{
    public class ctrlAuditoria
    {
        private vistaAuditoria view { get; set; }
        public bool cerrar = false;
        private int v_fila = Numeric._zeroInteger;
        private TemplateStyles Style = new TemplateStyles();

        internal ctrlAuditoria(vistaAuditoria pview)
        {
            view = pview;
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlInventario").Id))
            {
                view.Title = "Auditoría";
            }

            ppanel.IsVisible = true;
        }

        public void pnlInventario_chkAjustarColumnas_Click()
        {
            if (view.FindByName<CheckBox>("pnlInventario_chkAjustarColumnas").Checked)
            {
                view.FindByName<Grid>("pnlInventarioGridDefinitions").ColumnDefinitions[0].Width = 40;
                view.FindByName<Grid>("pnlInventarioGridDefinitions").ColumnDefinitions[1].Width = 0;
                view.FindByName<Grid>("pnlInventarioGridDefinitions").ColumnDefinitions[2].Width = 65;
                view.FindByName<Grid>("pnlInventarioGridDefinitions").ColumnDefinitions[3].Width = 70;
                view.FindByName<Grid>("pnlInventarioGridDefinitions").ColumnDefinitions[4].Width = 65;
                view.FindByName<Grid>("pnlInventarioGridDefinitions").ColumnDefinitions[5].Width = 65;
                AjustarColumnas(true);
            }
            else
            {
                view.FindByName<Grid>("pnlInventarioGridDefinitions").ColumnDefinitions[0].Width = 80;
                view.FindByName<Grid>("pnlInventarioGridDefinitions").ColumnDefinitions[1].Width = 270;
                view.FindByName<Grid>("pnlInventarioGridDefinitions").ColumnDefinitions[2].Width = 80;
                view.FindByName<Grid>("pnlInventarioGridDefinitions").ColumnDefinitions[3].Width = 110;
                view.FindByName<Grid>("pnlInventarioGridDefinitions").ColumnDefinitions[4].Width = 80;
                view.FindByName<Grid>("pnlInventarioGridDefinitions").ColumnDefinitions[5].Width = 90;
                AjustarColumnas(false);
            }
        }

        internal void AjustarColumnas(bool value)
        {
            CustomDataTemplateSelectorAN FormatListView = new CustomDataTemplateSelectorAN();
            ObservableCollection<pnlInventario_ltvInventario> Collection = new ObservableCollection<pnlInventario_ltvInventario>();

            if (value)
            {
                Collection = FormatListView.ChangeStyleListViewTF(view.FindByName<ListView>("pnlInventario_ltvInventario"), Style.ContractedStyle);
            }
            else
            {
                Collection = FormatListView.ChangeStyleListViewTF(view.FindByName<ListView>("pnlInventario_ltvInventario"), Style.ExpandedStyle);
            }

            view.FindByName<ListView>("pnlInventario_ltvInventario").ItemsSource = Collection;
        }

        private void actualizarColumnasCodigoProducto()
        {
            MiscUtils _miscUtils = new MiscUtils();
            _miscUtils.quantityListViewItems<pnlInventario_ltvInventario>(view.FindByName<ListView>("pnlInventario_ltvInventario"), view.FindByName<Label>("pnlInventario_clhCodigo"), "Código");
        }

        private void renderComponents()
        {
            pnlInventario_chkAjustarColumnas_Click();
            actualizarColumnasCodigoProducto();
        }

        internal void ScreenInicialization()
        {
            Logica_ManagerInventario _managerInventarioLogica = new Logica_ManagerInventario();

            _managerInventarioLogica.recalcularProductoDisponibleEnInventario();

            renderPaneles(view.FindByName<StackLayout>("pnlInventario"));

            Auditoria_ManagerInventario _managerInventarioAuditorio = new Auditoria_ManagerInventario();

            _managerInventarioAuditorio.buscarInventarioAuditoria(view.FindByName<ListView>("pnlInventario_ltvInventario"));

            renderComponents();

            view.FindByName<CheckBox>("pnlInventario_chkAjustarColumnas").Checked = true;

            view.FindByName<ExtendedEntry>("pnlInventario_txtCantidad").Text = string.Empty;
        }

        internal async Task menu_mniLimpiar_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea limpiar la Auditoría?", "Auditoría"))
            {
                var Source = view.FindByName<ListView>("pnlInventario_ltvInventario").ItemsSource as ObservableCollection<pnlInventario_ltvInventario>;

                for (int i = 0; i < Source.Count; i++)
                {
                    Source[i].Auditado = Numeric._zeroDecimal;
                }

                view.FindByName<ListView>("pnlInventario_ltvInventario").ItemsSource = Source;
            }
        }

        private async Task actualizarInventarioAuditoria()
        {
            Auditoria_ManagerInventario _manager = new Auditoria_ManagerInventario();

            _manager.actualizarInventarioAuditoria(view.FindByName<ListView>("pnlInventario_ltvInventario"));

            await LogMessageSuccess._generalSuccess("Actualizó la auditoría");
        }

        internal async Task menu_mniImprimir_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea imprimir la Auditoría?", "Auditoría"))
            {
                await actualizarInventarioAuditoria();

                ProcesoImpresion _impresion = new ProcesoImpresion();

                await _impresion.imprimirReporteInventarioAuditoria();

                await LogMessageSuccess.printed();
            }
        }

        internal async Task menu_mniConsolidar_Click()
        {

            if (await LogMessages._dialogResultYes("¿Desea actualizar la Auditoría?", "Auditoría"))
            {
                await actualizarInventarioAuditoria();

                await LogMessageSuccess._generalSuccess("Actualizó la auditoría");
            }
        }

        private void agregarCantidad(decimal pcantidadIngresada)
        {
            var Source = view.FindByName<ListView>("pnlInventario_ltvInventario").ItemsSource as ObservableCollection<pnlInventario_ltvInventario>;            
            var Seleccionado = view.FindByName<ListView>("pnlInventario_ltvInventario").SelectedItem as pnlInventario_ltvInventario;
            int IndiceSeleccionado = Source.IndexOf(Seleccionado);
            string _disponibleS = Seleccionado.Disponible;
            decimal _disponibleD = FormatUtil.convertStringToDecimal(_disponibleS);

            Seleccionado.Auditado = FormatUtil.applyCurrencyFormat(pcantidadIngresada);
            Seleccionado.Diferencia = FormatUtil.applyCurrencyFormat(pcantidadIngresada - _disponibleD);
            view.FindByName<ExtendedEntry>("pnlInventario_txtCantidad").Text = string.Empty;
            Source.RemoveAt(IndiceSeleccionado);
            Source.Insert(IndiceSeleccionado, Seleccionado);
            view.FindByName<ListView>("pnlInventario_ltvInventario").ItemsSource = Source;
        }

        internal async Task mniAgregarCantidad_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (!_validateHH.emptyTextBox(view.FindByName<ExtendedEntry>("pnlInventario_txtCantidad")))
            {
                if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlInventario_ltvInventario")))
                {
                    decimal _cantidadIngresada = FormatUtil.convertStringToDecimal(view.FindByName<ExtendedEntry>("pnlInventario_txtCantidad").Text);

                    agregarCantidad(_cantidadIngresada);
                }
            }
        }

        internal void pnlInventario_btnBorrar_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.backSpace(view.FindByName<ExtendedEntry>("pnlInventario_txtCantidad"));
        }

        internal void pnlInventario_btnLimpiar_Click()
        {
            view.FindByName<ExtendedEntry>("pnlInventario_txtCantidad").Text = string.Empty;
        }

        internal void pnlInventario_btnPuntoDecimal_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.punto(view.FindByName<ExtendedEntry>("pnlInventario_txtCantidad"));
        }

        internal void pnlInventario_btnZero_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.zero(view.FindByName<ExtendedEntry>("pnlInventario_txtCantidad"));
        }

        internal void pnlInventario_btnOne_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.one(view.FindByName<ExtendedEntry>("pnlInventario_txtCantidad"));
        }

        internal void pnlInventario_btnTwo_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.two(view.FindByName<ExtendedEntry>("pnlInventario_txtCantidad"));
        }

        internal void pnlInventario_btnThree_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.three(view.FindByName<ExtendedEntry>("pnlInventario_txtCantidad"));
        }

        internal void pnlInventario_btnFour_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.four(view.FindByName<ExtendedEntry>("pnlInventario_txtCantidad"));
        }

        internal void pnlInventario_btnFive_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.five(view.FindByName<ExtendedEntry>("pnlInventario_txtCantidad"));
        }

        internal void pnlInventario_btnSix_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.six(view.FindByName<ExtendedEntry>("pnlInventario_txtCantidad"));
        }

        internal void pnlInventario_btnSeven_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.seven(view.FindByName<ExtendedEntry>("pnlInventario_txtCantidad"));
        }

        internal void pnlInventario_btnEight_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.eight(view.FindByName<ExtendedEntry>("pnlInventario_txtCantidad"));
        }

        internal void pnlInventario_btnNine_Click()
        {
            ValidateHH _validateHH = new ValidateHH();
            _validateHH.nine(view.FindByName<ExtendedEntry>("pnlInventario_txtCantidad"));
        }

        internal async Task pnlInventario_btnCalculadora_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlInventario_ltvInventario")))
            {
                var Seleccionado = view.FindByName<ListView>("pnlInventario_ltvInventario").SelectedItem as pnlInventario_ltvInventario;
                string _auditadoS = Seleccionado.Auditado;

                decimal _auditadoD = FormatUtil.convertStringToDecimal(_auditadoS);

                ShowCalculator _show = new ShowCalculator();

                _show.showCalculatorForm(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL),
                    _auditadoD,this);
            }
        }

        public void pnlInventario_btnCalculadora_ClickParte2(decimal _cantidadIngresada)
        {
            agregarCantidad(_cantidadIngresada);
        }

        internal void pnlInventario_ltvInventario_ItemActivate()
        {
            var Source = view.FindByName<ListView>("pnlInventario_ltvInventario").ItemsSource as ObservableCollection<pnlInventario_ltvInventario>;
            var Seleccionado = view.FindByName<ListView>("pnlInventario_ltvInventario").SelectedItem as pnlInventario_ltvInventario;
            v_fila = Source.IndexOf(Seleccionado);
        }

        internal async Task vistaAuditoria_Closed()
        {
            if (await LogMessages._dialogResultYes("¿Desea salir de la Auditoría?", "Auditoría"))
            {
                await actualizarInventarioAuditoria();
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}
