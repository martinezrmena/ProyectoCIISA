using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo
{
    internal class Impresion_ManagerDetalleTransaccion
    {
        internal void buscarLineasDetalleImpresion(string pcodTransaction,string pcodTipoTransaccion,List<string> pprintingLinesList,Cliente pobjCliente, bool DevolucionFactura = false)
        {
            HelperDetalleTransaccion _helper = new HelperDetalleTransaccion();

            _helper.buscarLineasDetalleImpresion(
                pcodTransaction,
                pcodTipoTransaccion,
                pprintingLinesList,
                pobjCliente,
                DevolucionFactura
                );
        }

    }
}
