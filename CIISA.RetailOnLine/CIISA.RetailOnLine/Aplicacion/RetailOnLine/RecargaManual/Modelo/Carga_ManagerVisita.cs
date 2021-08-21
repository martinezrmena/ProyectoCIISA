using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Modelo
{
    public class Carga_ManagerVisita
    {

        public StringBuilder insertTablaVisita(DataRow pdr)
        {
            HelperVisita _helper = new HelperVisita();

            return _helper.insertTablaVisita(pdr);
        }

    }
}
