using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerEncabezadoRecibo
    {
        public decimal calcularTotalRecibosPagosPorCliente(string pcodCliente)
        {
            HelperEncabezadoRecibo _helper = new HelperEncabezadoRecibo();

            return _helper.calcularTotalRecibosPagosPorCliente(pcodCliente);
        }

        public void actualizarAnulado(TransaccionEncabezado pobjTransaccion)
        {
            HelperEncabezadoRecibo _helper = new HelperEncabezadoRecibo();

            _helper.actualizarAnulado(
                pobjTransaccion
                );
        }

        public decimal calcularTotalFlujoEfectivo(string pcodTipoDocumento)
        {
            HelperEncabezadoRecibo _helper = new HelperEncabezadoRecibo();

            return _helper.calcularTotalFlujoEfectivo(pcodTipoDocumento);
        }

        public async Task guardarReciboEncabezado(Cliente pobjCliente)
        {
            HelperEncabezadoRecibo _helper = new HelperEncabezadoRecibo();

            await _helper.guardarReciboEncabezado(pobjCliente);
        }
    }
}
