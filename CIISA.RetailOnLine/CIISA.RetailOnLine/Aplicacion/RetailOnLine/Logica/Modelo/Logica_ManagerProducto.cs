using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Exoneracion;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerProducto
    {
        public bool buscarEsViscera(string pcodProducto)
        {
            HelperProducto _helper = new HelperProducto();

            return _helper.buscarEsViscera(pcodProducto);
        }

        public string buscarDescripcion(string pcodProducto)
        {
            HelperProducto _helper = new HelperProducto();

            return _helper.buscarDescripcion(pcodProducto);
        }

        public string buscarUnidad(string pcodProducto)
        {
            HelperProducto _helper = new HelperProducto();

            return _helper.buscarUnidad(pcodProducto);
        }

        public bool BuscarExento(string pcodProducto)
        {
            HelperProducto _helper = new HelperProducto();

            return _helper.BuscarExento(pcodProducto);
        }

        public decimal BuscarProcentajeIVA(string pcodProducto)
        {
            HelperProducto _helper = new HelperProducto();

            return _helper.BuscarProcentajeIVA(pcodProducto);
        }

        public pnlExoneracion_ltvExoneracion BuscarExoneracion(string NO_CIA, string COD_ARTICULO, string CODCLIENTE)
        {
            Carga.Helpers.HelperExoneracion _helper = new Carga.Helpers.HelperExoneracion();

            return _helper.consultarExoneracion(NO_CIA, COD_ARTICULO, CODCLIENTE);
        }

        public Producto buscarProductoPorCodigoProducto(string pcodProducto)
        {
            HelperProducto _helper = new HelperProducto();

            return _helper.buscarProductoPorCodigoProducto(pcodProducto);
        }

        public void buscarListaProducto(ListView pltvProductos, string pnomTipoTransaccion, string pfiltro, string ptipoBusqueda, List<Producto> plistaProductoComprometido)
        {
            HelperProducto _helper = new HelperProducto();

            _helper.buscarListaProducto(pltvProductos, pnomTipoTransaccion, ptipoBusqueda, pfiltro, plistaProductoComprometido);
        }
    }
}
