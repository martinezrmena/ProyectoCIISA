using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Modelo
{
    public class Carga_ManagerProducto
    {

        public StringBuilder insertTablaProducto(DataRow pdr)
        {
            HelperProducto _helper = new HelperProducto();

            return _helper.insertTablaProducto(pdr);
        }

    }
}
