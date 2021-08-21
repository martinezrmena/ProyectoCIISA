using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Modelo
{
    public class Carga_ManagerPrecioProducto
    {

        public StringBuilder insertTablaPrecioProducto(DataRow pdr)
        {
            HelperPrecioProducto _helper = new HelperPrecioProducto();

            return _helper.insertTablaPrecioProducto(pdr);
        }

    }
}
