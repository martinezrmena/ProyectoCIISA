using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Modelo
{
    public class Carga_ManagerDetallePedido
    {

        public StringBuilder insertTablaDetallePedido(DataRow pdr)
        {
            HelperDetallePedido _helper = new HelperDetallePedido();

            return _helper.insertTablaDetallePedido(pdr);
        }

    }
}
