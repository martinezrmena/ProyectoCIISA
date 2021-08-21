using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo
{
    internal class Impresion_ManagerDetalleRecibo
    {
        internal string buscarLineasDetalle(string pcodTransaction, string pcodTipoTransaccion)
        {
            HelperDetalleRecibo _helper = new HelperDetalleRecibo();

            return _helper.buscarLineasDetalle(pcodTransaction, pcodTipoTransaccion);
        }
    }
}
