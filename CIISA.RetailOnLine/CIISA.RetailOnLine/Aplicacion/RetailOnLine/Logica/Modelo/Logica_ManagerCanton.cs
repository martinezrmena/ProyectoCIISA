using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System.Data;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerCanton
    {
        public DataTable buscarCantones(string pcodProvincia)
        {
            HelperCanton _helper = new HelperCanton();

            return _helper.buscarCantones(pcodProvincia);
        }

        public string obtenerCodigoCanton(string pcodProvincia,string pnomCanton)
        {
            HelperCanton _helper = new HelperCanton();

            return _helper.obtenerCodigoCanton(pcodProvincia,pnomCanton);
        }

        public string obtenerNombreCanton(string pcodProvincia,string pcodCanton)
        {
            HelperCanton _helper = new HelperCanton();

            return _helper.obtenerNombreCanton(pcodProvincia,pcodCanton);
        }
    }
}
