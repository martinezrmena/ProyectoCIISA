using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Modelo
{
    public class Carga_ManagerEstablecimiento
    {

        public StringBuilder insertTablaEstablecimiento(DataRow pdr)
        {
            HelperEstablecimiento _helper = new HelperEstablecimiento();

            return _helper.insertTablaEstablecimiento(pdr);
        }

    }
}
