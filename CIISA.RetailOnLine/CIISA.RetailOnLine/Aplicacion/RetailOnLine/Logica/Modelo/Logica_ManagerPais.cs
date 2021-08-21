using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System.Data;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerPais
    {
        public DataTable buscarPaises()
        {
            HelperPais _helper = new HelperPais();

            return _helper.buscarPaises();
        }

        public string obtenerCodigoPais(string pnomPais)
        {
            HelperPais _helper = new HelperPais();

            return _helper.obtenerCodigoPais(pnomPais);
        }
    }
}
