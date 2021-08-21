using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using System.Data;
using System.Text;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Modelo
{
    public class Carga_ManagerImpresora
    {

        public StringBuilder insertTablaImpresora(DataRow pdr)
        {
            HelperImpresora _helper = new HelperImpresora();

            return _helper.insertTablaImpresora(pdr);
        }

    }
}
