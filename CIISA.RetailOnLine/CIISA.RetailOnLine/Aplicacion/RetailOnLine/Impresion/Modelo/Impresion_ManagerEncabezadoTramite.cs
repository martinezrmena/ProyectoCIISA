using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers;
using System;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo
{
    internal class Impresion_ManagerEncabezadoTramite
    {
        internal DateTime buscarLineasDetalle(string pcodTransaction)
        {
            HelperEncabezadoTramite _helper = new HelperEncabezadoTramite();

            return _helper.buscarLineasDetalle(
                pcodTransaction
                );
        }

        internal void buscarLineasReporteTramites(List<string> pprintingLinesList)
        {
            HelperEncabezadoTramite _helper = new HelperEncabezadoTramite();

            _helper.buscarLineasReporteTramites(
                pprintingLinesList
                );
        }
    }
}
