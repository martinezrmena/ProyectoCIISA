using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Respaldo.Helpers;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Respaldo.Modelo
{
    public class Respaldo_ManagerPagoRecibo
    {
        public void RespaldarPagosRecibo()
        {
            HelperPagoRecibo _helper = new HelperPagoRecibo();

            _helper.RespaldarPagosRecibo();
        }

    }
}
