using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Modelo
{
    public class Carga_ManagerListaPrecios
    {

        public StringBuilder insertTablaListaPrecios(DataRow pdr)
        {
            HelperListaPrecios _helper = new HelperListaPrecios();

            return _helper.insertTablaListaPrecios(pdr);
        }

    }
}
