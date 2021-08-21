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
    public class LogicaVisitaComboTT_DF
    {
        private vistaVisita view = null;

        internal LogicaVisitaComboTT_DF(vistaVisita pview)
        {
            view = pview;
        }

        internal void devolucionFactura()
        {
            view.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource = new ObservableCollection<pnlTransacciones_ltvProductos>();

            ShowSROL _showTramite = new ShowSROL();

            _showTramite.mostrarPantallaDevolucionFactura(view.controlador.v_objCliente, view);
        }
    }
}
