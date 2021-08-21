using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo
{
    internal class Impresion_ManagerTituloComprobante
    {
        internal void buscarTituloFacturaPorTipoDocumento(string pcodTipoTransaccion, List<string> pprintingLinesList)
        {
            HelperTituloComprobante _helper = new HelperTituloComprobante();

            _helper.buscarTituloFacturaPorTipoDocumento(pcodTipoTransaccion, pprintingLinesList);
        }
    }
}
