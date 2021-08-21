using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Framework.Common.Misc;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion
{
    public class LeyendaCopia
    {
        private ProcesoImpresion v_procesoImpresion { get; set; }

        public LeyendaCopia(ProcesoImpresion pprocesoImpresion)
        {
            v_procesoImpresion = pprocesoImpresion;
        }

        internal async Task enviarImprimirAgregarLeyendaCopia(Cliente pobjCliente,string pcodTransaction,string pcodTipoTransaccion)
        {
            originalCopias _originalCopias = new originalCopias(v_procesoImpresion);

            if (pcodTipoTransaccion.Equals(ROLTransactions._facturaContadoSigla))
            {
                int _copias = NumeroCopiasImpresion.facturaContado;

                Impresion_ManagerPago _manager = new Impresion_ManagerPago();

                if (_manager.buscarTipoFormaPago(pcodTransaction))
                {
                    _copias++;
                }

                await _originalCopias.imprimirOriginalYCopias(_copias, pcodTipoTransaccion, pcodTransaction, pobjCliente);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._facturaCreditoSigla))
            {
                await _originalCopias.imprimirOriginalYCopias(pobjCliente.v_copias_fac, pcodTipoTransaccion, pcodTransaction, pobjCliente);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._reciboDineroSigla))
            {
                int _copias = NumeroCopiasImpresion.reciboDinero;

                Impresion_ManagerPagoRecibo _manager = new Impresion_ManagerPagoRecibo();

                if (_manager.buscarTipoFormaPago(pcodTransaction))
                {
                    _copias++;
                }

                await _originalCopias.imprimirOriginalYCopias(_copias, pcodTipoTransaccion, pcodTransaction, pobjCliente);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla))
            {
                await _originalCopias.imprimirOriginalYCopias(NumeroCopiasImpresion.ordenVenta, pcodTipoTransaccion, pcodTransaction, pobjCliente);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._cotizacionSigla))
            {
                await _originalCopias.imprimirOriginalYCopias(NumeroCopiasImpresion.cotizacion, pcodTipoTransaccion, pcodTransaction, pobjCliente);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._devolucionSigla))
            {
                await _originalCopias.imprimirOriginalYCopias(NumeroCopiasImpresion.devolucion, pcodTipoTransaccion, pcodTransaction, pobjCliente);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._regaliaSigla))
            {
                await _originalCopias.imprimirOriginalYCopias(NumeroCopiasImpresion.regalia, pcodTipoTransaccion, pcodTransaction, pobjCliente);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._recaudacionSigla))
            {
                int _copias = NumeroCopiasImpresion.recaudacion;

                Impresion_ManagerPagoRecibo _manager = new Impresion_ManagerPagoRecibo();

                if (_manager.buscarTipoFormaPago(pcodTransaction))
                {
                    _copias++;
                }

                await _originalCopias.imprimirOriginalYCopias(_copias, pcodTipoTransaccion, pcodTransaction, pobjCliente);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._tramiteSigla))
            {
                await _originalCopias.imprimirOriginalYCopias(NumeroCopiasImpresion.tramite, pcodTipoTransaccion, pcodTransaction, pobjCliente);
            }
        }
    }
}
