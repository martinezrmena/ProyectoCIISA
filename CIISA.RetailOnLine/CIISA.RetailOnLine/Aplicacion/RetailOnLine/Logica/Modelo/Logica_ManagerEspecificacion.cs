using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System.Data;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerEspecificacion
    {
        public DataTable buscarEspecificacion()
        {
            HelperEspecificacion _helper = new HelperEspecificacion();

            return _helper.buscarEspecificacion();
        }

        public bool BuscarEspecificacionPorDescripcion(string pdescripcion)
        {
            HelperEspecificacion _helper = new HelperEspecificacion();

            return _helper.buscarEspecificacionPorDescripcion(pdescripcion);
        }

        public string obtenerEspecificacionMotivo(string pcodEspecificacion)
        {
            HelperEspecificacion _helper = new HelperEspecificacion();

            return _helper.obtenerEspecificacionMotivo(pcodEspecificacion);
        }
    }
}
