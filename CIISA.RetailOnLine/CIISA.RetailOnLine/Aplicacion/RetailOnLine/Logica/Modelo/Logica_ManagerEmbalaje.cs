using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System.Data;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerEmbalaje
    {
        public DataTable buscarEmbalajePorArticulo(string pcodArticulo)
        {
            HelperEmbalaje _helper = new HelperEmbalaje();

            return _helper.buscarEmbalajePorArticulo(pcodArticulo);
        }

        public string buscarEmbalajePorDefinicion(string pcodArticulo)
        {
            HelperEmbalaje _helper = new HelperEmbalaje();

            return _helper.buscarEmbalajePorDefinicion(pcodArticulo);
        }
    }
}
