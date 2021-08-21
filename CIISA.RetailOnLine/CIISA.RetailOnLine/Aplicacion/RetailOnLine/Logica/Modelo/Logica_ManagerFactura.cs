using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerFactura
    {
        public decimal calcularTotalFacturasPorPagarPorCliente(Cliente pobjCliente)
        {
            HelperFactura _helper = new HelperFactura();

            return _helper.calcularTotalFacturasPorPagarPorCliente(pobjCliente);
        }

        public void anularFactura(TransaccionEncabezado pobjTransaccion)
        {
            HelperFactura _helper = new HelperFactura();

            _helper.anularFactura(pobjTransaccion);
        }

        public bool ExisteFacturasVencidas(Cliente pobjCliente)
        {
            HelperFactura _helper = new HelperFactura();

            return _helper.ExisteFacturasVencidas(pobjCliente);
        }

        public List<string> facturasVencidas(Cliente pobjCliente)
        {
            HelperFactura _helper = new HelperFactura();

            return _helper.facturasVencidas(pobjCliente);
        }

        public decimal obtenerSaldoFactura(string pcodFactura)
        {
            HelperFactura _helper = new HelperFactura();

            return _helper.obtenerSaldoFactura(pcodFactura);
        }

        public void buscarListaFacturaTramite(ListView pltvFacturas, Cliente pobjCliente)
        {
            HelperFactura _helper = new HelperFactura();

            _helper.buscarListaFacturaTramite(pltvFacturas, pobjCliente);
        }

        public void buscarListaFacturaDevolucion(ListView pltvFacturas, Cliente pobjCliente)
        {
            HelperFactura _helper = new HelperFactura();

            _helper.buscarListaFacturaDevolucion(pltvFacturas, pobjCliente);
        }

        public void buscarListaFacturaRecibo(ListView pltvFacturas, Cliente pobjCliente)
        {
            HelperFactura _helper = new HelperFactura();

            _helper.buscarListaFacturaRecibo(pltvFacturas, pobjCliente);
        }

        public void guardarFactura(Cliente pobjCliente, decimal ptotal)
        {
            HelperFactura _helper = new HelperFactura();

            _helper.guardarFactura(pobjCliente, ptotal);
        }

        public void buscarListaFactura(ListView pnlAbono_ltvFacturas, Cliente pobjCliente, List<string> plistaFacturas)
        {
            HelperFactura _helper = new HelperFactura();

            _helper.buscarListaFactura(pnlAbono_ltvFacturas, pobjCliente, plistaFacturas);
        }
    }
}
