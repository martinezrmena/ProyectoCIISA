using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo
{
    internal class Impresion_ManagerIndicadorFactura
    {
        internal void buscarLineasIndicadorFactura(List<string> pprintingLinesList)
        {
            HelperIndicadorFactura _helper = new HelperIndicadorFactura();

            _helper.buscarLineasIndicadorFactura(
                pprintingLinesList
                );
        }
    }
}
