using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Modelo
{
    public class Carga_ManagerCliente
    {
        public StringBuilder insertTablaCliente(DataRow pdr)
        {
            HelperCliente _helper = new HelperCliente();

            return _helper.insertTablaCliente(pdr);
        }

    }
}
