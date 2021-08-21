using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo
{
    internal class Impresion_ManagerPago
    {
        internal string buscarLineasFormaPago(string pcodTransaction)
        {
            HelperPago _helper = new HelperPago();

            return _helper.buscarLineasFormaPago(pcodTransaction);
        }

        internal bool buscarTipoFormaPago(string pcodTransaction)
        {
            HelperPago _helper = new HelperPago();

            return _helper.buscarTipoFormaPago(pcodTransaction);
        }
    }
}
