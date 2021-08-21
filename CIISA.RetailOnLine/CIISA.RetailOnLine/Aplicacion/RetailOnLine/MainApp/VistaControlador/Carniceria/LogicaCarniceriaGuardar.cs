using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Vista.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Carniceria
{
    /// <summary>
    ///Clase encargada de realizar redirecciones a pantallas relacionadas con
    ///carniceria para tomar parametros
    /// </summary>
    public class LogicaCarniceriaGuardar
    {
        private ctrlVisita controlador = null;

        internal LogicaCarniceriaGuardar(ctrlVisita ctrl)
        {
            controlador = ctrl;
        }

        /// <summary>
        ///Metodo encargado de tomar los elementos de un listview para convertirlos en una lista
        ///que se pueda modificar desde CodeBehind
        /// </summary>
        /// return la lista de productos que existe en el listview
        public List<pnlTransacciones_ltvProductos> convertListView(ListView list) {

            List<pnlTransacciones_ltvProductos> productos = new List<pnlTransacciones_ltvProductos>();

            var Source = list.ItemsSource as ObservableCollection<pnlTransacciones_ltvProductos>;

            foreach (var item in Source)
            {
                //Validacion para verificar el codigo de cliente que conincida con los productos
                productos.Add(item);
            }

            return productos;

        }

        /// <summary>
        ///Metodo encargado de redireccionar a la pantalla encargada de escanear las cajas
        ///junto con el objeto del cliente y la lista de items del listview,
        ///dentro de su procedimiento modificará los elementos del listview que sean necesario
        /// </summary>
        public async Task AbrirCarniceriaEscaneoCajas(List<pnlTransacciones_ltvProductos> productos) {

            await Application.Current.MainPage.Navigation.PushAsync(
                new vistaCarniceria(controlador.v_objCliente, productos));

        }

        /// <summary>
        ///Metodo encargado de encontrar el detalle de res del producto
        /// </summary>
        public List<pnlTransacciones_ltvDetalleReses> BuscarDetalleRes(string codproducto, Cliente cliente, string codpedido)
        {
            Logica_ManagerCarniceria logica = new Logica_ManagerCarniceria();

            return logica.buscarDetalleResesProducto(codproducto, cliente.v_no_cliente, codpedido);

        }
    }
}
