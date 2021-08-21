using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo
{
    internal class Impresion_ManagerPagoRecibo
    {
        internal string buscarLineasFormaPago(string pcodTransaction, string pcodTipoTransaccion)
        {
            HelperPagoRecibo _helper = new HelperPagoRecibo();

            return _helper.buscarLineasFormaPago(pcodTransaction, pcodTipoTransaccion);
        }

        internal bool buscarTipoFormaPago(string pcodTransaction)
        {
            HelperPagoRecibo _helper = new HelperPagoRecibo();

            return _helper.buscarTipoFormaPago(pcodTransaction);
        }
    }
}
