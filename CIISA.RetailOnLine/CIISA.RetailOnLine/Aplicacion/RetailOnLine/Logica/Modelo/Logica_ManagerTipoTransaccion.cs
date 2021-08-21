using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System.Data;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerTipoTransaccion
    {
        public DataTable buscarTipoTransaccion()
        {
            HelperTipoTransaccion _helper = new HelperTipoTransaccion();

            return _helper.buscarTiposTransacciones();
        }

        public string obtenerCodigoTipoTransaccion(string pnomTipoTransaccion)
        {
            HelperTipoTransaccion _helper = new HelperTipoTransaccion();

            return _helper.obtenerCodigoTipoTransaccion(pnomTipoTransaccion);
        }

        public string obtenerDescripcionTipoTransaccion(string pcodTipoTransaccion)
        {
            HelperTipoTransaccion _helper = new HelperTipoTransaccion();

            return _helper.obtenerDescripcionTipoTransaccion(pcodTipoTransaccion);
        }
    }
}
