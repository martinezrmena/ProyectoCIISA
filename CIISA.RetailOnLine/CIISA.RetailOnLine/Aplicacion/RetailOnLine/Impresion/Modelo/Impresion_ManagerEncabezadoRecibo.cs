using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers;
using System.Collections.Generic;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo
{
    internal class Impresion_ManagerEncabezadoRecibo
    {
        internal void buscarLineasReporteRecibosDeDinero(string pcodTipoDocumento,List<string> pprintingLinesList)
        {
            HelperEncabezadoRecibo _helper = new HelperEncabezadoRecibo();

            _helper.buscarLineasReporteRecibosDeDinero(
                pcodTipoDocumento,
                pprintingLinesList
                );
        }

        internal string buscarLineasEncabezado(string pcodTransaction,string pcodTipoTransaccion)
        {
            HelperEncabezadoRecibo _helper = new HelperEncabezadoRecibo();

            return _helper.buscarLineasEncabezado(
                pcodTransaction,
                pcodTipoTransaccion
                );
        }
    }
}
