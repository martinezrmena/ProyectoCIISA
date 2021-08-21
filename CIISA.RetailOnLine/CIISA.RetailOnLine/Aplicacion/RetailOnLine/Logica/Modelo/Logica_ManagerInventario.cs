using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System;
using System.Data;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerInventario
    {
        public DateTime buscarFechaTomaFisica()
        {
            HelperInventario _helper = new HelperInventario();

            return _helper.buscarFechaTomaFisica();
        }

        public int buscarNumeroDiaDeLaSemanaFechaTomaFisica()
        {
            HelperInventario _helper = new HelperInventario();

            return _helper.buscarNumeroDiaDeLaSemanaFechaTomaFisica();
        }

        public ProductoInventario buscarInventarioProducto(string pcodProducto)
        {
            HelperInventario _helper = new HelperInventario();

            return _helper.buscarInventarioProducto(pcodProducto);
        }

        public decimal buscarInventarioProductoDisponible(string pcodProducto)
        {
            HelperInventario _helper = new HelperInventario();

            return _helper.buscarInventarioProductoDisponible(pcodProducto);
        }

        public decimal buscarInventarioProductoVentas(string pcodProducto)
        {
            HelperInventario _helper = new HelperInventario();

            return _helper.buscarInventarioProductoVentas(pcodProducto);
        }

        public decimal buscarInventarioProductoRegalias(string pcodProducto)
        {
            HelperInventario _helper = new HelperInventario();

            return _helper.buscarInventarioProductoRegalias(pcodProducto);
        }

        public void recalcularProductoDisponibleEnInventario()
        {
            HelperInventario _helper = new HelperInventario();

            _helper.recalcularProductoDisponibleEnInventario();
        }

        public void actualizarInventarioAnulacion(TransaccionEncabezado pobjTransaccion)
        {
            HelperInventario _helper = new HelperInventario();

            _helper.actualizarInventarioAnulacion(pobjTransaccion);
        }

        public void actualizarVentasDocumentosAnuladosConVentas(TransaccionEncabezado pobjTransaccion)
        {
            HelperInventario _helper = new HelperInventario();

            _helper.actualizarVentasDocumentosAnuladosConVentas(pobjTransaccion);
        }

        public void actualizarRegaliasDocumentosAnuladosConRegalias(TransaccionEncabezado pobjTransaccion)
        {
            HelperInventario _helper = new HelperInventario();

            _helper.actualizarVentasDocumentosAnuladosConRegalias(pobjTransaccion);
        }

        public bool InventarioVacio()
        {
            HelperInventario _helper = new HelperInventario();

            return _helper.InventarioVacio();
        }

        public void actualizarInventarioTransaccion(Cliente pobjCliente)
        {
            HelperInventario _helper = new HelperInventario();

            _helper.actualizarInventarioTransaccion(pobjCliente);
        }

        public void buscarListaInventarioTeorico(ListView pltvProducto)
        {
            HelperInventario _helper = new HelperInventario();

            _helper.buscarListaInventarioTeorico(pltvProducto);
        }

        //public void buscarInventarioPedidos(TreeView ptrvInventarioPedidos, string pcodProducto)
        public void buscarInventarioPedidos(ListView ptrvInventarioPedidos, string pcodProducto)
        {
            HelperInventario _helper = new HelperInventario();

            _helper.buscarInventarioPedidos(ptrvInventarioPedidos, pcodProducto);
        }

        public string buscarEstadoConsolidado()
        {
            HelperInventario _helper = new HelperInventario();

            return _helper.buscarEstadoConsolidado();
        }

        public DateTime buscarFechaCreacion()
        {
            HelperInventario _helper = new HelperInventario();

            return _helper.buscarFechaCreacion();
        }

        public DateTime buscarFechaTomaFisicaConsolida()
        {
            HelperInventario _helper = new HelperInventario();

            return _helper.buscarFechaTomaFisicaConsolida();
        }

        public bool ExisteProducto(string pcodProducto)
        {
            HelperInventario _helper = new HelperInventario();

            return _helper.ExisteProducto(pcodProducto);
        }

        public void calcularCantidadDisponible(string pcodProducto)
        {
            HelperInventario _helper = new HelperInventario();

            _helper.calcularCantidadDisponible(pcodProducto);
        }

        public DataTable buscarFechaTomaFisicaActualizarEnviado()
        {
            HelperInventario _helper = new HelperInventario();

            return _helper.buscarFechaTomaFisicaActualizarEnviado();
        }

        public void consolidarInventarioAutomaticamente()
        {
            HelperInventario _helper = new HelperInventario();

            _helper.consolidarInventarioAutomaticamente();
        }
    }
}
