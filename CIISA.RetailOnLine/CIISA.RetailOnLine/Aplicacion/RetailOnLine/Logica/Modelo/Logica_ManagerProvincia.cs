using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System.Data;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerProvincia
    {
        public DataTable buscarProvincias()
        {
            HelperProvincia _helper = new HelperProvincia();

            return _helper.buscarProvincias();
        }

        public string obtenerCodigoProvincia(string pnomProvincia)
        {
            HelperProvincia _helper = new HelperProvincia();

            return _helper.obtenerCodigoProvincia(pnomProvincia);
        }

        public string obtenerNombreProvincia(string pcodProvincia)
        {
            HelperProvincia _helper = new HelperProvincia();

            return _helper.obtenerNombreProvincia(pcodProvincia);
        }
    }
}
