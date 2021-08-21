using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerEncabezadoRazonesNV
    {
        public void guardarRazonNoVenta(string pcodCliente, string pcodTransaction)
        {
            HelperEncabezadoRazonesNV _helper = new HelperEncabezadoRazonesNV();

            _helper.guardarEncabezadoRazonesNV(pcodCliente, pcodTransaction);
        }
    }
}
