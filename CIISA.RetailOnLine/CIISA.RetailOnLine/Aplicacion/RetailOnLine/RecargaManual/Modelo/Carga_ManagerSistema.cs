using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Modelo
{
    public class Carga_ManagerSistema
    {
        public StringBuilder insertTablaSistema(DataRow pdr)
        {
            HelperSistema _helper = new HelperSistema();

            return _helper.insertTablaSistema(pdr);
        }
    }
}
