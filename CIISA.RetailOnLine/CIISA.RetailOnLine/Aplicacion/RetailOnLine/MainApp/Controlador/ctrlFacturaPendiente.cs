using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.External.CustomListview;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    internal class ctrlFacturaPendiente
    {
        private vistaFacturaPendiente view { get; set; }

        private Cliente v_objCliente = null;
        private string v_pantallaInvoca = string.Empty;
        private bool v_verificar = false;
        private vistaRecibo v_VistaRecibo { get; set; }
        private bool Cerrar = false;
        private int MaximoVerificar = 0;

        internal ctrlFacturaPendiente(vistaFacturaPendiente pview)
        {
            view = pview;
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlFacturas").Id))
            {
                view.Title = "Facturas";
            }

            ppanel.IsVisible = true;
        }

        private void renderComponentesPnlFacturas()
        {
            if (v_pantallaInvoca.Equals(PantallasSistema._pantallaRecibo)
                && v_objCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._reciboDineroSigla))
            {
                view.FindByName<Label>("pnlFacturas_lblLeyenda").IsVisible = true;
            }
            else
            {
                view.FindByName<Label>("pnlFacturas_lblLeyenda").IsVisible = false;
            }

        }

        private void renderMenu()
        {
            if (v_objCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._reciboDineroSigla))
            {
                view.ToolbarItems.Clear();
                view.FindByName<Grid>("pnlFacturaPendiente_Controls").IsVisible = true;
            }
            else
            {
                if (v_objCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._tramiteSigla))
                {
                    view.ToolbarItems.Clear();
                    view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniTramitar"));
                }
                else
                {
                    view.ToolbarItems.Clear();
                    view.FindByName<Grid>("pnlFacturaPendiente_Controls").IsVisible = true;
                    view.FindByName<StackLayout>("pnlFacturaPendiente_stkAgregar").IsVisible = false;
                    view.FindByName<StackLayout>("pnlFacturaPendiente_stckCheck").IsVisible = false;
                }
            }
        }

        private void actualizarColumnasMonto()
        {
            Util _util = new Util();

            _util.sumarItemsColumnaLista<pnlFacturas_ltvFacturas>(
                view.FindByName<ListView>("pnlFacturas_ltvFacturas"),
                view.FindByName<Label>("pnlFacturas_clhMonto"),
                2,
                "Monto"
                );
        }

        private void actualizarColumnasSaldo()
        {
            Util _util = new Util();

            _util.sumarItemsColumnaLista<pnlFacturas_ltvFacturas>(
                view.FindByName<ListView>("pnlFacturas_ltvFacturas"),
                view.FindByName<Label>("pnlFacturas_clhSaldo"),
                1,
                "Saldo"
                );
        }

        private void actualizarColumnasNumeroLineas()
        {
            MiscUtils _miscUtils = new MiscUtils();
            _miscUtils.quantityListViewItems<pnlFacturas_ltvFacturas>(view.FindByName<ListView>("pnlFacturas_ltvFacturas"), view.FindByName<Label>("pnlFacturas_clhNoFisico"), "Documento");
        }

        internal void ScreenInicialization(Cliente pobjCliente, string ppantallaInvoca,vistaRecibo pviewRecibo)
        {
            v_objCliente = pobjCliente;
            v_objCliente.v_listaFacturas.Clear();

            v_pantallaInvoca = ppantallaInvoca;

            if (ppantallaInvoca.Equals(PantallasSistema._pantallaRecibo))
            {
                v_VistaRecibo = pviewRecibo;
            }

            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlFacturas"));

            renderComponentesPnlFacturas();

            renderMenu();

            Logica_ManagerFactura _manager = new Logica_ManagerFactura();

            if (v_objCliente.v_objTransaccion.v_objTipoDocumento.GetSigla().Equals(ROLTransactions._tramiteSigla))
            {
                _manager.buscarListaFacturaTramite(view.FindByName<ListView>("pnlFacturas_ltvFacturas"), v_objCliente);
            }
            else
            {
                _manager.buscarListaFacturaRecibo(view.FindByName<ListView>("pnlFacturas_ltvFacturas"), v_objCliente);
            }

            actualizarColumnasMonto();
            actualizarColumnasSaldo();
            actualizarColumnasNumeroLineas();
        }

        private void desmarcarChequeDevuelto()
        {
            var Source = view.FindByName<ListView>("pnlFacturas_ltvFacturas").ItemsSource as SelectableObservableCollection<pnlFacturas_ltvFacturas>;

            foreach (var _lvi in Source)
            {                
                string _tipoDoc = _lvi.Data.TIPO_DOC;

                if (_tipoDoc.Equals(ROLTransactions._chequeDevueltoSigla))
                {
                    _lvi.IsSelected = false;
                }
            }

            view.FindByName<ListView>("pnlFacturas_ltvFacturas").ItemsSource = Source;
        }

        internal void menu_mniSumar_Click()
        {
            decimal _s = Numeric._zeroDecimalInitialize;

            var Source = view.FindByName<ListView>("pnlFacturas_ltvFacturas").ItemsSource as SelectableObservableCollection<pnlFacturas_ltvFacturas>;

            foreach (var _lvi in Source)
            {
                if (_lvi.IsSelected)
                {
                    string _saldo = _lvi.Data.SALDO;
                    _s = _s + FormatUtil.convertStringToDecimal(_saldo);

                    view.FindByName<Label>("pnlFacturas_lblRetroalimentacion").Text = "Saldo: " + FormatUtil.applyCurrencyFormat(_s);
                }
            }

            view.FindByName<ListView>("pnlFacturas_ltvFacturas").ItemsSource = Source;
        }

        internal void pnlFacturas_chkTodos_CheckStateChanged()
        {
            var Source = view.FindByName<ListView>("pnlFacturas_ltvFacturas").ItemsSource as SelectableObservableCollection<pnlFacturas_ltvFacturas>;

            foreach (var _lvi in Source)
            {
                _lvi.IsSelected = view.FindByName<CheckBox>("pnlFacturas_chkTodas").Checked;
            }

            view.FindByName<ListView>("pnlFacturas_ltvFacturas").ItemsSource = Source;

            desmarcarChequeDevuelto();

            if (view.FindByName<CheckBox>("pnlFacturas_chkTodas").Checked)
            {
                menu_mniSumar_Click();
            }
            else
            {
                view.FindByName<Label>("pnlFacturas_lblRetroalimentacion").Text = string.Empty;
            }
        }

        internal async Task menu_mniTramitar_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            bool _vacio = _validateHH.emptyListView<pnlFacturas_ltvFacturas>(view.FindByName<ListView>("pnlFacturas_ltvFacturas"));

            if (_vacio)
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention("Debe seleccionar almenos una factura");
            }
            else
            {
                var Source = view.FindByName<ListView>("pnlFacturas_ltvFacturas").ItemsSource as SelectableObservableCollection<pnlFacturas_ltvFacturas>;

                if (Source.Count > 0)
                {
                    for (int j = Source.Count - 1; j >= 0; j--)
                    {                        
                        if (Source[j].IsSelected == false)
                        {
                            Source.RemoveAt(j);
                        }
                    }

                    foreach (var _lvi in Source)
                    {                        
                        v_objCliente.v_listaFacturas.Add(_lvi.Data.NO_FISICO);
                    }

                    Cerrar = true;

                    await Application.Current.MainPage.Navigation.PopAsync();
                }
            }
        }

        internal async Task menu_mniVerificar_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            bool _chequeado = _validateHH.listViewChecked<pnlFacturas_ltvFacturas>(view.FindByName<ListView>("pnlFacturas_ltvFacturas"));

            if (_chequeado)
            {
                int _maximoIndice = 0;

                var Source = view.FindByName<ListView>("pnlFacturas_ltvFacturas").ItemsSource as SelectableObservableCollection<pnlFacturas_ltvFacturas>;

                foreach (var _lvi in Source)
                {                    
                    string _tipoDoc = _lvi.Data.TIPO_DOC;

                    if (_lvi.IsSelected)
                    {                        
                        _maximoIndice = Source.IndexOf(_lvi);
                    }
                }

                for (int i = 0; i < _maximoIndice; i++)
                {
                    Source[i].IsSelected = true;
                }

                desmarcarChequeDevuelto();

                menu_mniSumar_Click();

                MaximoVerificar = _maximoIndice;

                v_verificar = true;
            }
            else
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention("Debe seleccionar almenos un ítem");
            }
        }

        internal bool ValidarVerificacion()
        {
            int _maximoIndice = 0;

            var Source = view.FindByName<ListView>("pnlFacturas_ltvFacturas").ItemsSource as SelectableObservableCollection<pnlFacturas_ltvFacturas>;

            foreach (var _lvi in Source)
            {
                if (_lvi.IsSelected)
                {
                    _maximoIndice = Source.IndexOf(_lvi);
                }
            }

            if (MaximoVerificar != _maximoIndice)
            {
                return false;
            }

            for (int i = 0; i < _maximoIndice; i++)
            {
                if (i <= _maximoIndice && !Source[i].IsSelected)
                {
                    return false;
                }
            }

            return true;
        }

        internal async Task menu_mniAgregar_Click()
        {
            if (v_verificar)
            {
                if (!ValidarVerificacion())
                {
                    v_verificar = false;
                    await PreVerify();
                    return;
                }

                ValidateHH _validateHH = new ValidateHH();

                if (_validateHH.emptyListView<pnlFacturas_ltvFacturas>(view.FindByName<ListView>("pnlFacturas_ltvFacturas")))
                {
                    LogMessageAttention _logMessageAttention = new LogMessageAttention();
                    await _logMessageAttention.generalAttention("La lista esta vacía");
                }
                else
                {
                    v_objCliente.v_listaFacturas = new List<string>();

                    MiscUtils.deleteNoSelectedItemsListView<pnlFacturas_ltvFacturas>(view.FindByName<ListView>("pnlFacturas_ltvFacturas"));

                    var Source = view.FindByName<ListView>("pnlFacturas_ltvFacturas").ItemsSource as SelectableObservableCollection<pnlFacturas_ltvFacturas>;

                    foreach (var _lvi in Source)
                    {
                        v_objCliente.v_listaFacturas.Add(_lvi.Data.NO_FISICO);
                    }

                    Cerrar = true;

                    await Application.Current.MainPage.Navigation.PopAsync();
                }
            }
            else
            {
                await PreVerify();
            }
        }

        internal async Task PreVerify()
        {
            LogMessageAttention _logMessageAttention = new LogMessageAttention();
            await _logMessageAttention.generalAttention("Se verificaran las facturas antes de continuar"
                + Environment.NewLine
                + Environment.NewLine
                + "* Revise los resultados, si es correcto presione el botón Agregar");

            await menu_mniVerificar_Click();
        }

        internal void pnlFacturas_ltvFacturas_ItemCheck()
        {
            v_verificar = false;
        }

        public async Task Cerrando()
        {
            if (Cerrar)
            {
                if (v_pantallaInvoca.Equals(PantallasSistema._pantallaRecibo))
                {
                    v_VistaRecibo.controlador.v_objCliente = v_objCliente;

                    await v_VistaRecibo.controlador.RespuestaDeFacturasPendientes();
                }
            }
        }
    }
}
