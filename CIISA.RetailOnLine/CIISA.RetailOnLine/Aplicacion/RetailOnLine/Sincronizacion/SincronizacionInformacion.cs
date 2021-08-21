using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.CargaDiaria.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Modelo;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Sincronizacion
{
    class SincronizacionInformacion
    {
        public async Task informacionSincronizar()
        {
            informacionCargar();
            await informacionDescargarEnvioTotal();            
        }

        private void informacionCargar()
        {
            Log _log = new Log();

            Carga_ManagerRecargaDiaria _manager = new Carga_ManagerRecargaDiaria();

#pragma warning disable CS4014 // Como esta llamada no es 'awaited', la ejecución del método actual continuará antes de que se complete la llamada. Puede aplicar el operador 'await' al resultado de la llamada.
            _manager.recargaDiariaTablaInventario(
                _log,
                new Editor(),
                new Label(),
                false,
                VariableCarga._sincronizacion
                );
#pragma warning restore CS4014 // Como esta llamada no es 'awaited', la ejecución del método actual continuará antes de que se complete la llamada. Puede aplicar el operador 'await' al resultado de la llamada.
        }

        public async Task informacionDescargarEnvioTotal()
        {
            Descarga_ManagerEnvio _manager = new Descarga_ManagerEnvio();

            await _manager.EnvioPaqueteInformacionPorWS_Total();
        }
    }
}
