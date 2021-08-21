using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.DevolucionFactura;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista.DevolucionFactura;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using CIISA.RetailOnLine.Framework.Handheld.Validate;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador.DevolucionFactura
{
    public class ctrlTipoDevolucion
    {
        public vistaTipoDevolucion view { get; set; }

        public bool Cerrar { get; set; }

        public vistaVisita Visita { get; set; }

        public bool valid { get; set; }

        public ctrlTipoDevolucion(vistaTipoDevolucion v_view, vistaVisita vistaVisita, bool v_valid)
        {
            view = v_view;
            Visita = vistaVisita;
            valid = v_valid;
        }

        internal void ScreenInicialization()
        {
            renderPaneles(view.FindByName<StackLayout>("pnlTipoDevolucion"));
            renderMenu();
        }

        public void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlTipoDevolucion"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlTipoDevolucion").Id))
            {
                view.FindByName<Label>("lblTitle").Text = "Seleccionar tipo de devolución";
            }

            ppanel.IsVisible = true;
        }

        internal void renderMenu()
        {
            var Source = new ObservableCollection<pnlDevolucion_ltvTiposDevolucion>();

            List<pnlDevolucion_ltvTiposDevolucion> Elementos = new List<pnlDevolucion_ltvTiposDevolucion>();

            Elementos.Add(new pnlDevolucion_ltvTiposDevolucion(ROLTransactions._devolucionNombre + " normal"));
            Elementos.Add(new pnlDevolucion_ltvTiposDevolucion(ROLTransactions._devolucionFacturaNombre));

            foreach (var item in Elementos)
            {
                Source.Add(item);
            }

            view.FindByName<ListView>("pnlDevolucion_ltvTipoDevolucion").ItemsSource = Source;
        }

        /// <summary>
        ///Metodo para validar que se haya seleccionado un tipo de devolución
        ///antes de regresar de la pantalla
        /// </summary>
        internal async Task SeleccionarTipoDevolucion()
        {
            ValidateHH _validateHH = new ValidateHH();

            if (await _validateHH.listViewItemSelected(view.FindByName<ListView>("pnlDevolucion_ltvTipoDevolucion")))
            {
                if (await LogMessages._dialogResultYes("¿Desea proceder con el tipo de devolución seleccionada?", "Alerta"))
                {
                    var selectedItem = view.FindByName<ListView>("pnlDevolucion_ltvTipoDevolucion").SelectedItem as pnlDevolucion_ltvTiposDevolucion;

                    if (selectedItem.TipoDevolucion == ROLTransactions._devolucionFacturaNombre)
                    {
                        Visita.controlador.v_DevolucionFactura = true;
                    }
                    else
                    {
                        Visita.controlador.COD_FACTURA = string.Empty;
                        Visita.controlador.COD_PEDIDO = string.Empty;
                        Visita.controlador.v_objCliente.v_objTransaccion.v_codFactura = string.Empty;
                        Visita.controlador.v_DevolucionFactura = false;
                        Visita.controlador.COD_FACTURA = string.Empty;
                        Visita.controlador.valid = valid;
                    }

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
                await Visita.controlador.seleccionarTipoDevolucionParte2();
            }
        }

    }
}
