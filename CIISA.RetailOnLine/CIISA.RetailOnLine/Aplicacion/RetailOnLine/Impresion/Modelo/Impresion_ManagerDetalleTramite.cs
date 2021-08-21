using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers;
using System;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo
{
    internal class Impresion_ManagerDetalleTramite
    {
        internal string buscarLineasDetalle(string pcodTransaction, DateTime pdate)
        {
            HelperDetalleTramite _helper = new HelperDetalleTramite();

            return _helper.buscarLineasDetalle(pcodTransaction, pdate);
        }
    }
}
