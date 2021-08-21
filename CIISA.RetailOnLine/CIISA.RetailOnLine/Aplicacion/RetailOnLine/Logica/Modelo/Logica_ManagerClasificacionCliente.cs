using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System.Data;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerClasificacionCliente
    {
        public DataTable buscarClasificacionCliente()
        {
            HelperClasificacionCliente _helper = new HelperClasificacionCliente();

            return _helper.buscarClasificacionCliente();
        }

        public string buscarCodigoClasificacion(string pnombreClasificacion)
        {
            HelperClasificacionCliente _helper = new HelperClasificacionCliente();

            return _helper.buscarCodigoClasificacion(
                pnombreClasificacion
                );
        }
    }
}
