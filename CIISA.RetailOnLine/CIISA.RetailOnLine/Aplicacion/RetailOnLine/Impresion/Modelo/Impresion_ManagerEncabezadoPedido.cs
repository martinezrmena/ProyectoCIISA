using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo
{
    internal class Impresion_ManagerEncabezadoPedido
    {
        internal void buscarEncabezadosPedidosRuta(List<string> pprintingLinesList)
        {
            HelperEncabezadoPedido _helper = new HelperEncabezadoPedido();

            _helper.buscarEncabezadosPedidosRuta(
                pprintingLinesList
                );
        }

        internal void buscarEncabezadosPedidosSinAplicar(List<string> pprintingLinesList)
        {
            HelperEncabezadoPedido _helper = new HelperEncabezadoPedido();

            _helper.buscarEncabezadosPedidosSinAplicar(
                pprintingLinesList
                );
        }
    }
}
