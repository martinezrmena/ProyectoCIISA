using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo
{
    internal class Impresion_ManagerEncabezadoTransaccion
    {
        internal void buscarLineasReporteDocumentosRealizados(List<string> pprintingLinesList,string pcodTipoDocumento)
        {
            HelperEncabezadoTransaccion _helper = new HelperEncabezadoTransaccion();

            _helper.buscarLineasReporteDocumentosRealizados(
                pprintingLinesList,
                pcodTipoDocumento
                );
        }

    }
}
