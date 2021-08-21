using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Modelo
{
    public class Carga_ManagerIndicadorFactura
    {

        public StringBuilder insertTablaIndicadorFactura(DataRow pdr)
        {
            HelperIndicadorFactura _helper = new HelperIndicadorFactura();

            return _helper.insertTablaIndicadorFactura(pdr);
        }

    }
}
