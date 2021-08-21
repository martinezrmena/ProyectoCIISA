using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System.Data;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerRazonesNV
    {
        public DataTable buscarRazonesNV()
        {
            HelperRazonesNV _helper = new HelperRazonesNV();

            return _helper.buscarRazonesNV();
        }

        public string obtenerCodigoTipoTransaccion(string pnomRazon)
        {
            HelperRazonesNV _helper = new HelperRazonesNV();

            return _helper.obtenerCodigoRazonesNV(pnomRazon);
        }

        public string obtenerDescripcionTipoTransaccion(string pcodRazonNV)
        {
            HelperRazonesNV _helper = new HelperRazonesNV();

            return _helper.obtenerDescripcionRazonesNV(pcodRazonNV);
        }
    }
}
