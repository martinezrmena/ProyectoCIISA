using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita.ComboTipoTransaccion;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador
{
    internal class ctrlTramite
    {
        private vistaTramite view { get; set; }
        private vistaVisita pviewVista { get; set; }
        private Cliente v_objCliente = null;
        private bool Cerrar = false;

        internal ctrlTramite(vistaTramite pview,vistaVisita pvista)
        {
            view = pview;
            pviewVista = pvista;
        }

        internal void ScreenInicialization(Cliente pobjCliente)
        {
            ValidateHH _validateHH = new ValidateHH();

            v_objCliente = pobjCliente;

            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlTramite"));

            Logica_ManagerEncabezadoTransaccion _manager = new Logica_ManagerEncabezadoTransaccion();

            _manager.buscarListaTransaccionEncabezadosParaTramite(
                view.FindByName<ListView>("pnlTramite_ltvDocumentos"),
                pobjCliente
                );

            if (_validateHH.emptyListView<pnlTramite_ltvDocumentos>(view.FindByName<ListView>("pnlTramite_ltvDocumentos")))
            {
                //view.FindByName<ToolbarItem>("menu_mniTramitar").IsEnabled = false;
                renderPanelTramite(false);
            }
        }

        internal void renderPanelTramite(bool render) {

            view.FindByName<Grid>("pnlTramite_grdOptions").IsVisible = render;
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlTramite").Id))
            {
                view.Title = "Trámite";
            }

            ppanel.IsVisible = true;
        }

        internal async Task menu_mniTramitar_Click()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (_validateHH.emptyListView<pnlTramite_ltvDocumentos>(view.FindByName<ListView>("pnlTramite_ltvDocumentos")))
            {
                throw new Exception("La lista esta vacía.");
            }
            else
            {
                if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlTramite_ltvDocumentos")))
                {
                    var Seleccionado = view.FindByName<ListView>("pnlTramite_ltvDocumentos").SelectedItem as pnlTramite_ltvDocumentos;

                    v_objCliente.v_objTransaccion.v_facturaTramitar = Seleccionado.Documento;

                    Cerrar = true;
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
            }
        }

        internal async Task pnlTramite_ltvDocumentos_SelectedIndexChanged()
        {
            ValidateHH _validateHH = new ValidateHH();

            await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlTramite_ltvDocumentos"));
        }

        internal async Task Cerrando()
        {
            if (Cerrar)
            {
                LogicaVisitaComboTT_TR logicaVisitaComboTT_TR = new LogicaVisitaComboTT_TR(pviewVista);
                await logicaVisitaComboTT_TR.Respuesta();
            }
        }

    }
}
