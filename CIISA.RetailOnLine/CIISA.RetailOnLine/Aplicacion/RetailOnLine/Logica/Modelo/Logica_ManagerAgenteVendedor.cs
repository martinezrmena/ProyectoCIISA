using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System.Data;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerAgenteVendedor
    {
        public async Task<bool> verificarConsecutivoRepetido(Cliente pobjCliente)
        {
            HelperAgenteVendedor _helper = new HelperAgenteVendedor();

            return await _helper.verificarConsecutivoRepetido(pobjCliente);
        }

        public DataTable buscarAgenteVendedor(string pcodAgente)
        {
            HelperAgenteVendedor _helper = new HelperAgenteVendedor();

            return _helper.buscarAgenteVendedor(pcodAgente);
        }

        public string obtenerCodigoAgente()
        {
            HelperAgenteVendedor _helper = new HelperAgenteVendedor();

            return _helper.obtenerCodigoAgente();
        }

        public string buscarConsecutivoCliente()
        {
            HelperAgenteVendedor _helper = new HelperAgenteVendedor();

            return _helper.buscarConsecutivoCliente();
        }

        public void aumentarConsecutivoNuevoCliente()
        {
            HelperAgenteVendedor _helper = new HelperAgenteVendedor();

            _helper.aumentarConsecutivoNuevoCliente();
        }

        public void BuscarConsecutivo_Pedido(Cliente pobjCliente)
        {
            HelperAgenteVendedor _helper = new HelperAgenteVendedor();

            _helper.BuscarConsecutivo_Pedido(pobjCliente);
        }

        public void AumentarConsecutivo_Pedido()
        {
            HelperAgenteVendedor _helper = new HelperAgenteVendedor();

            _helper.AumentarConsecutivo_Pedido();
        }

        public void buscarConsecutivoTransaccion(Cliente pobjCliente)
        {
            HelperAgenteVendedor _helper = new HelperAgenteVendedor();

            _helper.buscarConsecutivoTransaccion(pobjCliente);
        }

        public void actualizarConsecutivoTransaccion(Cliente pobjCliente)
        {
            HelperAgenteVendedor _helper = new HelperAgenteVendedor();

            _helper.actualizarConsecutivoTransaccion(pobjCliente);
        }

        public void buscarConsecutivoTramite(Cliente pobjCliente)
        {
            HelperAgenteVendedor _helper = new HelperAgenteVendedor();

            _helper.buscarConsecutivoTramite(pobjCliente);
        }

        public void actualizarConsecutivoTramite(Cliente pobjCliente)
        {
            HelperAgenteVendedor _helper = new HelperAgenteVendedor();

            _helper.actualizarConsecutivoTramite(pobjCliente);
        }

        public string obtenerCodigoClienteGenerico(string pcodAgente)
        {
            HelperAgenteVendedor _helper = new HelperAgenteVendedor();

            return _helper.obtenerCodigoAgenteGenerico(pcodAgente);
        }

        public string obtenerCodigoClienteAgenteVendedor()
        {
            HelperAgenteVendedor _helper = new HelperAgenteVendedor();

            return _helper.obtenerCodigoClienteAgenteVendedor();
        }

        public string buscarTipoAgente()
        {
            HelperAgenteVendedor _helper = new HelperAgenteVendedor();

            return _helper.obtenerTipoAgente();
        }

        public void actualizarConsecutivoRecaudacion()
        {
            HelperAgenteVendedor _helper = new HelperAgenteVendedor();

            _helper.actualizarConsecutivoRecaudacion();
        }

        public void actualizarConsecutivoRecibo()
        {
            HelperAgenteVendedor _helper = new HelperAgenteVendedor();

            _helper.actualizarConsecutivoRecibo();
        }
    }
}
