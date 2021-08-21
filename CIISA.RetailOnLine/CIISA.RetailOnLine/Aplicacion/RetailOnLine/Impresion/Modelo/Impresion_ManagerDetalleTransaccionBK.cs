using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo
{
    internal class Impresion_ManagerDetalleTransaccionBK
    {
        internal void buscarLineasDetalleImpresion(string pcodTransaction, string pcodTipoTransaccion, List<string> pprintingLinesList, Cliente pobjCliente, bool DevolucionFactura)
        {
            HelperDetalleTransaccionBK _helper = new HelperDetalleTransaccionBK();

            _helper.buscarLineasDetalleImpresion(pcodTransaction, pcodTipoTransaccion, pprintingLinesList, pobjCliente, DevolucionFactura);
        }

    }
}
