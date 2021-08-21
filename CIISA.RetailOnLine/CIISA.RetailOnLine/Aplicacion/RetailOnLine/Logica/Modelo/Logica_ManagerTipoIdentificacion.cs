using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System.Data;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerTipoIdentificacion
    {
        public DataTable buscarTipoIdentificacion()
        {
            HelperTipoIdentificacion _helper = new HelperTipoIdentificacion();

            return _helper.buscarTipoIdentificacion();
        }

        public string buscarCodigoTipoIdentificacion(string ptipoCedula)
        {
            HelperTipoIdentificacion _helper = new HelperTipoIdentificacion();

            return _helper.buscarCodigoTipoIdentificacion(ptipoCedula);
        }
    }
}
