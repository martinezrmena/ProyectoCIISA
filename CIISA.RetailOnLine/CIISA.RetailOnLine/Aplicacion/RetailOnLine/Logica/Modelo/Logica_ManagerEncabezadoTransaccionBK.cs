using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerEncabezadoTransaccionBK
    {

        public DateTime buscarFechaHoraDocumento(string pcodTransaction,string pcodTipoTransaccion)
        {
            HelperEncabezadoTransaccionBK _manager = new HelperEncabezadoTransaccionBK();

            return _manager.buscarFechaHoraDocumento(pcodTransaction,pcodTipoTransaccion);
        }

    }
}
