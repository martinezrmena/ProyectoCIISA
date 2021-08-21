using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Modelo
{
    public class Carga_ManagerFactura
    {
        public DataTable buscarFacturas()
        {
            HelperFactura _helper = new HelperFactura();

            return _helper.buscarFacturas();
        }

        public StringBuilder insertTablaFactura(DataRow pdr)
        {
            HelperFactura _helper = new HelperFactura();

            return _helper.insertTablaFactura(pdr);
        }
    }
}
