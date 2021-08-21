using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Modelo
{
    public class Carga_ManagerInventario
    {
        public StringBuilder insertTablaInventario(DataRow pdr)
        {
            HelperInventario _helper = new HelperInventario();

            return _helper.insertTablaInventario(pdr);
        }

        public StringBuilder updateTablaInventario(DataRow pdr)
        {
            HelperInventario _helper = new HelperInventario();

            return _helper.updateTablaInventario(pdr);
        }
    }
}
