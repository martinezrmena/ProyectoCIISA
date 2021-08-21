using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerCuentaCerrada
    {
        public void buscarListaCuentasCerradas(ListView pltvClientes,string pcodCliente)
        {
            HelperCuentaCerrada _helper = new HelperCuentaCerrada();

            _helper.buscarListaCuentasCerradas(
                pltvClientes,
                pcodCliente
                );
        }
    }
}
