using CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Helpers;

namespace CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Modelo
{
    internal class Settlement_ManagerAgenteVendedor
    {

        internal string obtenerCodigoCliente()
        {
            HelperAgenteVendedor _helper = new HelperAgenteVendedor();

            return _helper.obtenerCodigoCliente();
        }

    }
}
