using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerDetalleAnulacion
    {
        public void guardarDetalleAnulacion(TransaccionEncabezado pobjTransaccion)
        {
            HelperDetalleAnulacion _helper = new HelperDetalleAnulacion();

            _helper.guardarDetalleAnulacionDeTransacciones(pobjTransaccion);
        }
    }
}
