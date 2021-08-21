using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerPrecioProducto
    {
        public decimal buscarPrecio(string pcodListaPrecios, string pcodProducto)
        {
            HelperPrecioProducto _helper = new HelperPrecioProducto();

            return _helper.buscarPrecio(pcodListaPrecios, pcodProducto);
        }
    }
}
