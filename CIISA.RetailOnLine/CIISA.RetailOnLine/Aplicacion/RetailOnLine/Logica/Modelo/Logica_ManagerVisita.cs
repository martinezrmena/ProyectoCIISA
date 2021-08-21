using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerVisita
    {
        public void buscarListaClienteRuteros(ListView pltvClientes, string ptipoBusquedaSegmento, string ptipoBusquedaCliente, string pfiltro)
        {
            HelperVisita _helper = new HelperVisita();

            _helper.buscarSegmentoRuta(pltvClientes, ptipoBusquedaSegmento, ptipoBusquedaCliente, pfiltro);
        }

        public int calcularTotalClientesSegmento()
        {
            HelperVisita _helper = new HelperVisita();

            return _helper.calcularTotalClientesSegmento();
        }

        public void nuevaVisita(Cliente pobjCliente)
        {
            HelperVisita _helper = new HelperVisita();

            _helper.nuevaVisita(pobjCliente);
        }

        public string buscarOrdenVisita(string pcodCliente, int pday)
        {
            HelperVisita _helper = new HelperVisita();

            return _helper.buscarOrdenVisita(pcodCliente, pday);
        }

        public bool ExisteVisita(string pcodCliente)
        {
            HelperVisita _helper = new HelperVisita();

            return _helper.ExisteVisita(pcodCliente);
        }
    }
}
