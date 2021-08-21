using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerEstablecimiento
    {
        public int buscarCantidadEstablecimientosPorCodigoCliente(string CODCLIENTE)
        {
            HelperEstablecimiento _helper = new HelperEstablecimiento();

            return _helper.CantidadEstablecimientosCliente(CODCLIENTE);
        }

        public void buscarEstablecimientosPorCodigoCliente(ListView list, Cliente pobjCliente)
        {
            HelperEstablecimiento _helper = new HelperEstablecimiento();

            _helper.buscarEstablecimientosCliente(list, pobjCliente);
        }

        public async Task buscarEstablecimientoPorCodigoEstablecimiento(Cliente pobjCliente)
        {
            HelperEstablecimiento _helper = new HelperEstablecimiento();

            await _helper.buscarEstablecimientoPorCodigoEstablecimiento(pobjCliente);
        }

        public void nuevoEstablecimiento(Cliente pobjCliente)
        {
            HelperEstablecimiento _helper = new HelperEstablecimiento();

            _helper.nuevoEstablecimiento(pobjCliente);
        }

        public bool ExisteEstablecimiento(string pcodCliente)
        {
            HelperEstablecimiento _helper = new HelperEstablecimiento();

            return _helper.ExisteEstablecimientoCliente(pcodCliente);
        }
    }
}
