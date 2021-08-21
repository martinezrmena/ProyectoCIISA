using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerAnulacion
    {
        public DateTime buscarUltimaTransaccionRealizada()
        {
            HelperAnulacion _helper = new HelperAnulacion();

            return _helper.buscarUltimaTransaccionRealizada();
        }

        public void buscarListaTransaccionEncabezadosParaAnulaciones(ListView pltvTransacciones,DateTime pfechaDocumento)
        {
            HelperAnulacion _helper = new HelperAnulacion();

            _helper.buscarListaTransaccionEncabezadosParaAnulaciones(
                        pltvTransacciones,
                        pfechaDocumento
                        );
        }
    }
}
