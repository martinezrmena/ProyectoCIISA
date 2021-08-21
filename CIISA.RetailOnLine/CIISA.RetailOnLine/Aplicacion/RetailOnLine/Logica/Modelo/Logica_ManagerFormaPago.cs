using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System.Data;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerFormaPago
    {
        public string obtenerCodigoFormaPago(string pnomFormaPago)
        {
            HelperFormaPago _helper = new HelperFormaPago();

            return _helper.obtenerCodigoFormaPago(pnomFormaPago);
        }

        public DataTable buscarFormaPago()
        {
            HelperFormaPago _helper = new HelperFormaPago();

            return _helper.buscarFormaPago();
        }

        public string buscarDescripcion(string pcodFormaPago)
        {
            HelperFormaPago _helper = new HelperFormaPago();

            return _helper.buscarDescripcion(pcodFormaPago);
        }
    }
}
