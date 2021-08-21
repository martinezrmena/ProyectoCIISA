using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo
{
    internal class Impresion_ManagerFactura
    {
        internal void buscarLineasReporteCrediticioDelCliente(Cliente pobjCliente,List<string> pprintingLinesList)
        {
            HelperFactura _helper = new HelperFactura();

            _helper.buscarLineasReporteCrediticioDelCliente(
                pobjCliente,
                pprintingLinesList
                );
        }

        internal void buscarLineasReporteCrediticioDeLaRuta(List<string> pprintingLinesList)
        {
            HelperFactura _helper = new HelperFactura();

            _helper.buscarLineasReporteCrediticioDeLaRuta(
                pprintingLinesList
                );
        }

    }
}
