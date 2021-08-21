using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.Controlador;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador.Carniceria
{
    /// <summary>
    /// Clase encargada de direccionar a los usuarios a una pantalla secundaria
    /// en caso de que un cliente posea vinculación con más de un establecimiento
    /// </summary>
    public class ctrlAgregarEstablecimiento
    {
        public vistaAgregarEstablecimiento view { get; set; }
        public Cliente ClienteSelected { get; set; }
        public pnlEstablecimiento_ltvEstablecimientos Establecimiento;
        public bool Cerrar { get; set; }
        private ctrlCliente MainController { get; set; }

        internal ctrlAgregarEstablecimiento(vistaAgregarEstablecimiento p_view)
        {
            view = p_view;

            Cerrar = false;
        }

        internal void ScreenInicialization()
        {
            renderPaneles(view.FindByName<StackLayout>("pnlEstablecimiento"));
        }

        internal void ScreenInicialization(ctrlCliente ctrl,Cliente pobjCliente)
        {
            MainController = ctrl;

            renderPaneles(view.FindByName<StackLayout>("pnlEstablecimiento"));

            ClienteSelected = pobjCliente;

            renderMenu();
        }

        public void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlEstablecimiento"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlEstablecimiento").Id))
            {
                view.FindByName<Label>("lblTitle").Text = "Seleccionar Establecimiento";
            }

            ppanel.IsVisible = true;
        }

        private void renderMenu() {

            view.FindByName<Label>("pnlEstablecimiento_lblNombreCliente").Text = ClienteSelected.v_nombre;

            view.FindByName<ListView>("pnlEstablecimiento_ltvEstablecimientos").ItemsSource = new ObservableCollection<pnlEstablecimiento_ltvEstablecimientos>();

            Logica_ManagerEstablecimiento _manager = new Logica_ManagerEstablecimiento();

            _manager.buscarEstablecimientosPorCodigoCliente(
                view.FindByName<ListView>("pnlEstablecimiento_ltvEstablecimientos"),
                ClienteSelected
                );
        }

        /// <summary>
        ///Metodo para validar que se haya seleccionado un establecimiento
        ///antes de regresar de la pantalla
        /// </summary>
        internal async Task SeleccionarEstablecimiento()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlEstablecimiento_ltvEstablecimientos")))
            {
                if (await LogMessages._dialogResultYes("¿Desea proceder con el establecimiento seleccionado?", "Alerta"))
                {
                    Establecimiento = view.FindByName<ListView>("pnlEstablecimiento_ltvEstablecimientos").SelectedItem as pnlEstablecimiento_ltvEstablecimientos;

                    Cerrar = true;

                    await Cerrando();

                    //Permite cerrar el pop up
                    await PopupNavigation.Instance.PopAsync(true);

                }
            }
        }

        private async Task Cerrando()
        {
            if (Cerrar)
            {
                await MainController.seleccionarClienteParte2(Establecimiento);
            }
        }
    }
}
