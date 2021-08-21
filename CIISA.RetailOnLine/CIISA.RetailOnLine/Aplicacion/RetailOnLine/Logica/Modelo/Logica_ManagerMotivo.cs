using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System.Data;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerMotivo
    {
        public DataTable buscarMotivoPorCodigoTransaccion(string pcodTransaction)
        {
            HelperMotivo _helper = new HelperMotivo();

            return _helper.buscarMotivoPorCodigoTransaccion(pcodTransaction);
        }

        public string obtenerCodigoMotivo(string pcodTipoTransaccion, string pnomMotivo)
        {
            HelperMotivo _helper = new HelperMotivo();

            return _helper.obtenerCodigoMotivo(pcodTipoTransaccion, pnomMotivo);
        }

        public string obtenerDescripcionMotivo(string pcodTransaction, string pcodMotivo)
        {
            HelperMotivo _helper = new HelperMotivo();

            return _helper.obtenerDescripcionMotivo(pcodTransaction, pcodMotivo);
        }
    }
}
