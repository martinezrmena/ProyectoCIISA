using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerIndicador
    {
        public void buscarListaIndicadoresFacturacion(ListView ppnlIndicadores_ltvIndicadores, string pcodCliente)
        {
            HelperIndicador _helper = new HelperIndicador();

            _helper.buscarListaIndicadoresFacturacion(ppnlIndicadores_ltvIndicadores, pcodCliente);
        }

        public async Task buscarIndicadoresCliente(Cliente pobjCliente)
        {
            HelperIndicador _helper = new HelperIndicador();

            await _helper.buscarIndicadoresEstablecimiento(pobjCliente);
        }

        public void nuevoIndicador(Cliente pobjCliente)
        {
            HelperIndicador _helper = new HelperIndicador();

            _helper.nuevoIndicador(pobjCliente);
        }

        public bool ExisteIndicadoresCliente(string pcodCliente, string pcodEstablecimiento)
        {
            HelperIndicador _helper = new HelperIndicador();

            return _helper.ExisteIndicadoresCliente(pcodCliente, pcodEstablecimiento);
        }
    }
}
