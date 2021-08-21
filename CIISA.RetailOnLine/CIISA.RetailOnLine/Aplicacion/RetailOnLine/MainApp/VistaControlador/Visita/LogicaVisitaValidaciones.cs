using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.GPS.ViewController;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita
{
    internal static class LogicaVisitaValidaciones
    {
        internal static bool validarCantidadesDiferentesDeCero(vistaVisita pviewVisita)
        {
            bool _cantidadMenorCero = false;

            var Source = pviewVisita.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

            foreach (var _lvi in Source)
            {
                LogicaVisitaLtvProducto _logica = new LogicaVisitaLtvProducto(pviewVisita);

                Producto _objProducto = _logica.levantarProductoSeleccionado(_lvi, true);

                if (_objProducto.v_cantTransaccion <= 0)
                {
                    _cantidadMenorCero = true;

                    _lvi.ItemTextColor = Color.Red;
                }
                else
                {
                    _lvi.ItemTextColor = Color.Default;
                }
            }

            pviewVisita.FindByName<ListView>("pnlTransacciones_ltvProductos").ItemsSource = Source;

            return _cantidadMenorCero;
        }

        internal static async Task<bool> validar50mts(vistaVisita pviewVisita)
        {
            bool indicadorRutaGeo = false;
            bool _estaA50mts = false;
            HelperAgenteVendedor helperAgente = new HelperAgenteVendedor();

            indicadorRutaGeo = helperAgente.ConsultarIndRutaGeo();

            //Validar si la transacciones es factura credito o factura contado
            if (pviewVisita.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._facturaCreditoNombre) ||
                pviewVisita.FindByName<Picker>("pnlTransacciones_cbxTipoTransaccion").SelectedItem.ToString().Equals(ROLTransactions._facturaContadoNombre))
            {
                //1)Ver si tiene indicador de geolocalizacion, sino return true
                if (pviewVisita.controlador.v_objCliente.v_objEstablecimiento.v_objIndicador.v_AplicaGeo)
                {
                    double latitud = pviewVisita.controlador.v_objCliente.v_latitud;
                    double longitud = pviewVisita.controlador.v_objCliente.v_longitud;

                    //2)Si lo tiene coger las coordenadas del cliente
                    if (pviewVisita.controlador.v_objCliente.v_latitud!=0 && pviewVisita.controlador.v_objCliente.v_longitud != 0)
                    {
                        //3)coordenadas del cliente a menos de 50 metros, sino return false
                        var ServicioGeo = DependencyService.Get<IServicioDistancia>();

                        _estaA50mts = await ServicioGeo.ValidacionGeolocalizacion(pviewVisita.controlador.v_objCliente.v_longitud, pviewVisita.controlador.v_objCliente.v_latitud);
                    }
                    else
                    {
                        LogMessageAttention LMA = new LogMessageAttention();
                        await LMA.generalAttention("Estimado usuario, el cliente no posee el formato correcto en sus coordenadas, por favor comuniquese con BackOffice.");
                        _estaA50mts = true;
                        //mensaje
                    }
                }
                else
                {
                    _estaA50mts = true;
                }
            }
            else
            {
                _estaA50mts = true;
            }

            return _estaA50mts;
        }
    }
}
