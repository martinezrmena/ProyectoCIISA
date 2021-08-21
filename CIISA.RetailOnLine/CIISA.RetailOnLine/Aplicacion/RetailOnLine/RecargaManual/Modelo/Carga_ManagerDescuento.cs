using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Modelo
{
    public class Carga_ManagerDescuento
    {
        public DataTable obtenerRespaldoDescuento()
        {
            HelperDescuento _helper = new HelperDescuento();

            return _helper.obtenerRespaldoDescuento();
        }

        public StringBuilder insertTablaDescuento(DataRow pdr)
        {
            HelperDescuento _helper = new HelperDescuento();

            return _helper.insertTablaDescuento(pdr);
        }

        public StringBuilder insertTablaDescuentoRespaldo(DataRow pdr)
        {
            HelperDescuento _helper = new HelperDescuento();

            return _helper.insertTablaDescuentoRespaldo(pdr);
        }
    }
}
