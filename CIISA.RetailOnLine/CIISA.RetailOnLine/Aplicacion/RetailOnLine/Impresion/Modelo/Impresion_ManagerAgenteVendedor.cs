using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo
{
    internal class Impresion_ManagerAgenteVendedor
    {
        internal string buscarLineasReporteConsecutivoDocumentos(string pcodAgente)
        {
            HelperAgenteVendedor _helper = new HelperAgenteVendedor();

            return _helper.buscarLineasReporteConsecutivoDocumentos(pcodAgente);
        }

    }
}
