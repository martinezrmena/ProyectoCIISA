using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerSugerido
    {
        public void buscarListaSugeridos(ListView pltvSugeridos, Cliente pobjCliente)
        {
            HelperSugerido _helper = new HelperSugerido();

            _helper.buscarListaSugeridos(pltvSugeridos, pobjCliente);
        }
    }
}
