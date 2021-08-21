using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerEncabezadoTransaccion
    {
        public int calcularTotalClientesFacturadosParaHoy()
        {
            HelperEncabezadoTransaccion _helper = new HelperEncabezadoTransaccion();

            return _helper.calcularTotalClientesFacturadosParaHoy();
        }

        public void actualizarAnulado(TransaccionEncabezado pobjTransaccion)
        {
            HelperEncabezadoTransaccion _helper = new HelperEncabezadoTransaccion();

            _helper.actualizarAnulado(pobjTransaccion);
        }

        public async Task guardarTransaccionEncabezado(Cliente pobjCliente)
        {
            HelperEncabezadoTransaccion _helper = new HelperEncabezadoTransaccion();

            await _helper.guardarEncabezadoTransaccion(pobjCliente);
        }

        public void actualizarTramitado(Cliente pobjCliente)
        {
            HelperEncabezadoTransaccion _helper = new HelperEncabezadoTransaccion();

            _helper.actualizarTramitado(pobjCliente);
        }

        public void buscarListaTransaccionEncabezadosParaTramite(ListView pltvTransacciones, Cliente pobjCliente)
        {
            HelperEncabezadoTransaccion _helper = new HelperEncabezadoTransaccion();

            _helper.buscarListaTransaccionEncabezadosParaTramite(pltvTransacciones,pobjCliente);
        }

        public decimal calcularTotalFlujoEfectivo(string pcodTipoDocumento)
        {
            HelperEncabezadoTransaccion _helper = new HelperEncabezadoTransaccion();

            return _helper.calcularTotalFlujoEfectivo(pcodTipoDocumento);
        }

        public DateTime buscarFechaHoraDocumento(string pcodTransaction, string pcodTipoTransaccion)
        {
            HelperEncabezadoTransaccion _helper = new HelperEncabezadoTransaccion();

            return _helper.buscarFechaHoraDocumento(pcodTransaction, pcodTipoTransaccion);
        }
    }
}
