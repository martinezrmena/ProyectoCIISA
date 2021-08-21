using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System.Data;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerDistrito
    {
        public DataTable buscarDistritos(string pcodProvincia, string pcodCanton)
        {
            HelperDistrito _helper = new HelperDistrito();

            return _helper.buscarDistritos(pcodProvincia, pcodCanton);
        }

        public string buscarCodigoDistrito(string pcodProvincia, string pcodCanton, string pnomDistrito)
        {
            HelperDistrito _helper = new HelperDistrito();

            return _helper.buscarCodigoDistrito(pcodProvincia, pcodCanton, pnomDistrito);
        }
        public string buscarNombreDistrito(string pcodProvincia, string pcodCanton, string pcodDistrito)
        {
            HelperDistrito _helper = new HelperDistrito();

            return _helper.buscarNombreDistrito(pcodProvincia, pcodCanton, pcodDistrito);
        }
    }
}
