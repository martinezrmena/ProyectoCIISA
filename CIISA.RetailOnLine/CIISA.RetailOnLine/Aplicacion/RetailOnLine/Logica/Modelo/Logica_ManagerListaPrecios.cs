using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System.Data;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerListaPrecios
    {
        public DataTable buscarListaPrecios()
        {
            HelperListaDePrecios _helper = new HelperListaDePrecios();

            return _helper.buscarListaPrecios();
        }

        public string buscarCodigoListaPrecios(string pnomDistrito)
        {
            HelperListaDePrecios _helper = new HelperListaDePrecios();

            return _helper.buscarCodigoListaPrecios(pnomDistrito);
        }
    }
}
