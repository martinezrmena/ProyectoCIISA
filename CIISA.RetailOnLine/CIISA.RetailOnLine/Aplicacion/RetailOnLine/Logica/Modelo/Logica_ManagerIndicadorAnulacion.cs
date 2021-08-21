using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerIndicadorAnulacion
    {
        public bool ValidarAnulacion(string cod_agente, string cod_transaccion)
        {
            HelperAnulacion _helper = new HelperAnulacion();

            return _helper.validarAnulacion(cod_agente, cod_transaccion);
        }

        public void eliminarIndicadorAnulacion(string cod_agente, string cod_transaccion)
        {
            HelperAnulacion _helper = new HelperAnulacion();

            _helper.eliminarIndicadorAnulacion(cod_agente, cod_transaccion);
        }
    }
}
