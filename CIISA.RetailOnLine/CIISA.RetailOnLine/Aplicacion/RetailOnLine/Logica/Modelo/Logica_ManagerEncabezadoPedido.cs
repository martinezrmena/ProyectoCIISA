using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerEncabezadoPedido
    {
        public void buscarListaEncabezadosPedidos(ListView plvtPedidos)
        {
            HelperEncabezadoPedido _helper = new HelperEncabezadoPedido();

            _helper.buscarListaEncabezadosPedidos(plvtPedidos);
        }

        private List<TransaccionEncabezado> buscarPedidosPorCliente(Cliente cliente, bool Validar, string EsFactura)
        {
            HelperEncabezadoPedido _helper = new HelperEncabezadoPedido();

            return _helper.buscarPedidosPorCliente(cliente, Validar, EsFactura);
        }

        private TransaccionEncabezado buscarPedidosPorCodPedido(string CodPedido, Cliente cliente, bool Validar, string EsFactura)
        {
            HelperEncabezadoPedido _helper = new HelperEncabezadoPedido();

            return _helper.buscarPedidoPorCodPedido(CodPedido, cliente, Validar, EsFactura);
        }

        public bool ExistePedidoBackOffice(Cliente pobjCliente)
        {
            HelperEncabezadoPedido _helper = new HelperEncabezadoPedido();

            bool _existePedido = _helper.ExistePedidoClienteBackOffice(pobjCliente);

            return _existePedido;
        }

        public bool ExistePedidoClienteBackOffice(string CodPedido, Cliente pobjCliente, bool validar, string Es_Factura)
        {
            HelperEncabezadoPedido _helper = new HelperEncabezadoPedido();

            bool _existePedido = _helper.ExistePedidoClienteBackOffice(pobjCliente);

            if (_existePedido)
            {
                var pedido = buscarPedidosPorCodPedido(CodPedido, pobjCliente, validar, Es_Factura);

                if (pedido != null)
                {
                    pobjCliente.v_objListaPedidos.Add(pedido);
                }
            }

            return _existePedido;
        }

        public bool ExistePedidoCliente(Cliente pobjCliente, bool validar, string Es_Factura)
        {
            HelperEncabezadoPedido _helper = new HelperEncabezadoPedido();

            bool _existePedido = _helper.ExistePedidoCliente(pobjCliente, validar);

            if (_existePedido)
            {
                pobjCliente.v_objListaPedidos = buscarPedidosPorCliente(pobjCliente, validar, Es_Factura);
            }

            return _existePedido;
        }

        public void actualizarAplicado(Cliente pobjCliente, bool Validar)
        {
            HelperEncabezadoPedido _helper = new HelperEncabezadoPedido();

            _helper.actualizarAplicado(pobjCliente, Validar);
        }

        public void actualizarAplicadoPedidoBackOffice(string CodPedido, Cliente pobjCliente)
        {
            HelperEncabezadoPedido _helper = new HelperEncabezadoPedido();

            _helper.actualizarAplicadoPedidoBackOffice(CodPedido, pobjCliente);
        }

        public void guardarEncabezadoPedido(Cliente pobjCliente)
        {
            HelperEncabezadoPedido _helper = new HelperEncabezadoPedido();

            _helper.guardarEncabezadoPedido(pobjCliente);
        }

        public bool EncabezadoPedido_Vacio()
        {
            HelperEncabezadoPedido _helper = new HelperEncabezadoPedido();

            return _helper.EncabezadoPedido_Vacio();
        }
    }
}
