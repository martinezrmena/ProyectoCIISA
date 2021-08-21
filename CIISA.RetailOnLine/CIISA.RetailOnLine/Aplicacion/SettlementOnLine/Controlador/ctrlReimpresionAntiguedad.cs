using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.SettlementOnLine.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Modelo;
using CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Vista;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Controlador
{
    internal class ctrlReimpresionAntiguedad
    {
        private vistaReimpresionAntiguedad view { get; set; }

        internal ctrlReimpresionAntiguedad(vistaReimpresionAntiguedad pview)
        {
            view = pview;
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlTransacciones").Id))
            {
                view.Title = "Reimpresión Antigúedad";
            }

            ppanel.IsVisible = true;
        }

        private void actualizarColumnasNumeroLineasEncabezadoTransaccion()
        {
            MiscUtils _miscUtils = new MiscUtils();
            _miscUtils.quantityListViewItems<pnlTransacciones_ltvTransacciones>(
                view.FindByName<ListView>("pnlTransacciones_ltvTransacciones"),
                view.FindByName<Label>("pnlTransacciones_clhCodDocumento"),
                "Código"
                );
        }

        private void actualizarColumnasNumeroLineas()
        {
            actualizarColumnasNumeroLineasEncabezadoTransaccion();
        }

        private void renderComponents()
        {
            view.FindByName<ListView>("pnlTransacciones_ltvTransacciones").ItemsSource = new ObservableCollection<pnlTransacciones_ltvTransacciones>();
            actualizarColumnasNumeroLineas();
        }

        private void renderMenu()
        {
            //view.ToolbarItems.Clear();
            RenderMenuBottom(false);

            if (view.FindByName<StackLayout>("pnlTransacciones").IsVisible)
            {
                //view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniReimprimir"));
                //view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniClose"));
                RenderMenuBottom(true);
            }
        }

        private void RenderMenuBottom(bool render)
        {
            view.FindByName<Grid>("pnlTransacciones_grdOptions").IsVisible = render;
        }

        private void buscarEncabezadoDocumentos()
        {
            Settlement_ManagerTransaccionBK _manager = new Settlement_ManagerTransaccionBK();

            _manager.consultarTransaccionEncabezadosBK(view.FindByName<ListView>("pnlTransacciones_ltvTransacciones"));
        }

        private void actualizarColumnasMontoTotal()
        {
            Util _util = new Util();

            _util.sumarItemsColumnaLista<pnlTransacciones_ltvTransacciones>(
                view.FindByName<ListView>("pnlTransacciones_ltvTransacciones"),
                view.FindByName<Label>("pnlTransacciones_clhTotal"),
                8,
                "Total"
                );
        }

        private void refrescarPantalla()
        {
            renderPaneles(view.FindByName<StackLayout>("pnlTransacciones"));
            renderComponents();
            renderMenu();

            buscarEncabezadoDocumentos();

            actualizarColumnasNumeroLineasEncabezadoTransaccion();
            actualizarColumnasMontoTotal();
        }

        internal void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);

            refrescarPantalla();
        }

        internal async Task menu_mniReimprimir_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlTransacciones_ltvTransacciones")))
            {
                var Seleccionado = view.FindByName<ListView>("pnlTransacciones_ltvTransacciones").SelectedItem as pnlTransacciones_ltvTransacciones;

                string _noCliente = Seleccionado.Cliente;
                string _noEstablecimiento = Seleccionado.Establecimiento;
                int _codEstablecimiento = FormatUtil.convertStringToInt(_noEstablecimiento);

                string _codTransaccion = Seleccionado.Documento;
                string _nomTipoTransaccion = Seleccionado.TipoDocumento;

                bool DevolucionFactura = string.IsNullOrEmpty(Seleccionado.CodFactura) ? false : true;

                Logica_ManagerTipoTransaccion _managerTipoTransaccion = new Logica_ManagerTipoTransaccion();

                string _codTipoTransaccion = _managerTipoTransaccion.obtenerCodigoTipoTransaccion(_nomTipoTransaccion);

                Cliente _objCliente = new Cliente();

                _objCliente.v_no_cliente = _noCliente;
                _objCliente.v_objEstablecimiento.v_codEstablecimiento = _codEstablecimiento;

                Logica_ManagerCliente _manager = new Logica_ManagerCliente();

                await _manager.buscarClientePorCodigoCliente(_objCliente);

                ProcesoImpresion _impresion = new ProcesoImpresion();
                await _impresion.imprimirTransaccionAntiguedad(
                    _objCliente,
                    _codTransaccion,
                    _codTipoTransaccion,
                    _nomTipoTransaccion,
                    DevolucionFactura
                    );

                await LogMessageSuccess.printed();
            }
        }

        internal void menu_mniClose_Click()
        {
            //view.Close();
            Application.Current.MainPage.Navigation.PopAsync();
        }

        internal async Task pnlTransacciones_ltvTransacciones_ItemActivate()
        {
            await menu_mniReimprimir_Click();
        }
    }
}
