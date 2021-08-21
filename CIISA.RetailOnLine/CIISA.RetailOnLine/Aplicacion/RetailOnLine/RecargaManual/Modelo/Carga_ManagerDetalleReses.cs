using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Modelo
{
    public class Carga_ManagerDetalleReses
    {
        public StringBuilder insertTablaDetalleReses(DataRow pdr)
        {
            HelperDetalleReses _helper = new HelperDetalleReses();

            return _helper.insertTablaDetalleReses(pdr);
        }
    }
}
