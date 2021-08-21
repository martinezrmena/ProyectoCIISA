using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Modelo
{
    public class Carga_ManagerIndicadorAnulacion
    {
        public StringBuilder insertTablaIndicadorAnulacion(DataRow pdr)
        {
            HelperIndicadorAnulacion _helper = new HelperIndicadorAnulacion();

            return _helper.insertTablaIndicadorAnulacion(pdr);
        }
    }
}
