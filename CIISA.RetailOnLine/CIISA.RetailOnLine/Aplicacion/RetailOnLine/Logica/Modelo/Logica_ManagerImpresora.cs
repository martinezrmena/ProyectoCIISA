using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerImpresora
    {
        public string obtenerPuertoImpresora()
        {
            HelperImpresora _helper = new HelperImpresora();

            return _helper.obtenerPuertoImpresora();
        }

    }
}
