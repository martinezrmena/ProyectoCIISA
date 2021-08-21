using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerCliente
    {

        public FacturaElectronica buscarFacturaElectronicaPorCodigoDocumento(FacturaElectronica facturaElectronica, string cod_transaccion) {

            HelperCliente _helper = new HelperCliente();

            return _helper.buscarFacturaElectronicaPorCodigoDocumento(facturaElectronica, cod_transaccion);
        }

        public async Task buscarClientePorCodigoCliente(Cliente pobjCliente)
        {
            HelperCliente _helper = new HelperCliente();

            await _helper.buscarClientePorCodigoCliente(pobjCliente);
        }

        public void nuevoCliente(Cliente pobjCliente)
        {
            HelperCliente _helper = new HelperCliente();

            _helper.nuevoCliente(pobjCliente);
        }

        public void buscarListaClienteRuteros(ListView ppnlClientes_ltvClientes, string ptipoBusqueda, string pfiltro)
        {
            HelperCliente _helper = new HelperCliente();

            _helper.buscarListaClienteRuteros(ppnlClientes_ltvClientes, ptipoBusqueda, pfiltro);
        }

        public string buscarNombreClientePorCodigoCliente(string pcodCliente)
        {
            HelperCliente _helper = new HelperCliente();

            return _helper.buscarNombreClientePorCodigoCliente(pcodCliente);
        }

        public bool ExisteCliente(string pcodCliente)
        {
            HelperCliente _helper = new HelperCliente();

            return _helper.ExisteCliente(pcodCliente);
        }

        public string buscarRutaCobro_Cliente(string pcodCliente)
        {
            HelperCliente _helper = new HelperCliente();

            return _helper.buscarRutaCobro_Cliente(pcodCliente);
        }
    }
}
