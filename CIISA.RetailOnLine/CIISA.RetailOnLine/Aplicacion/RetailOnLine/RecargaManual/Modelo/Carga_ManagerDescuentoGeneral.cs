using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Modelo
{
    public class Carga_ManagerDescuentoGeneral
    {
        public DataTable obtenerRespaldoDescuentoGeneral()
        {
            HelperDescuentoGeneral _helper = new HelperDescuentoGeneral();

            return _helper.obtenerRespaldoDescuentoGeneral();
        }

        public StringBuilder insertTablaDescuentoGeneral(DataRow pdr)
        {
            HelperDescuentoGeneral _helper = new HelperDescuentoGeneral();

            return _helper.insertTablaDescuentoGeneral(pdr);
        }

    }
}
