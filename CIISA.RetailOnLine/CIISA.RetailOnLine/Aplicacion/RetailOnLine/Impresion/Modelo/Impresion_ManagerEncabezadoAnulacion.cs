using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo
{
    internal class Impresion_ManagerEncabezadoAnulacion
    {
        internal void buscarLineasReporteAnulaciones(List<string> pprintingLinesList)
        {
            HelperEncabezadoAnulacion _helper = new HelperEncabezadoAnulacion();

            _helper.buscarLineasReporteAnulaciones(
                pprintingLinesList
                );
        }
    }
}
