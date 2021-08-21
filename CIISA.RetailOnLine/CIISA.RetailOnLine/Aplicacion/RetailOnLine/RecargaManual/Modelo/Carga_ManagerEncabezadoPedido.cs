using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Modelo
{
    public class Carga_ManagerEncabezadoPedido
    {
        public StringBuilder insertTablaEncabezadoPedido(DataRow pdr)
        {
            HelperEncabezadoPedido _helper = new HelperEncabezadoPedido();

            return _helper.insertTablaEncabezadoPedido(pdr);
        }
    }
}
