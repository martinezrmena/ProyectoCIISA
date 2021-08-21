using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita.ComboTipoTransaccion
{
    internal class LogicaVisitaComboTT_TR
    {
        private vistaVisita view = null;

        internal LogicaVisitaComboTT_TR(vistaVisita pview)
        {
            view = pview;
        }

        internal void tramite_SelectedIndexChanged()
        {
            view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource = new ObservableCollection<pnlTransacciones_ltvProductos>();

            ShowSROL _showTramite = new ShowSROL();

            _showTramite.mostrarPantallaTramite(view.controlador.v_objCliente,view);
        }

        internal async Task Respuesta()
        {
            if (!view.controlador.v_objCliente.v_objTransaccion.v_facturaTramitar.Equals(string.Empty))
            {
                LogicaVisitaMenu _logica = new LogicaVisitaMenu(view);

                await _logica.menu_mniGuardar_Click();
            }
        }
    }
}
