using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo
{
    internal class Impresion_ManagerBanco
    {
        internal void buscarLineasCuentasBancarias(List<string> pprintingLinesList)
        {
            HelperBanco _helper = new HelperBanco();

            _helper.buscarLineasCuentasBancarias(pprintingLinesList);
        }

    }
}
