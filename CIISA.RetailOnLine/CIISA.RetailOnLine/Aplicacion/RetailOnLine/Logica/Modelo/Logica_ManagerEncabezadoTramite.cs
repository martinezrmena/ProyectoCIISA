using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerEncabezadoTramite
    {
        public decimal calcularTotalFlujoEfectivo()
        {
            HelperEncabezadoTramite _helper = new HelperEncabezadoTramite();

            return _helper.calcularTotalFlujoEfectivo();
        }
    }
}
