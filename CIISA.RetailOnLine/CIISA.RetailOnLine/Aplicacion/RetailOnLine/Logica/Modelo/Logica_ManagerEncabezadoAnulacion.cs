using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers;
using System;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo
{
    public class Logica_ManagerEncabezadoAnulacion
    {
        public void buscarListaDeDocumentosAnulados(ListView pltvAnulaciones)
        {
            HelperEncabezadoAnulacion _helper = new HelperEncabezadoAnulacion();

            _helper.buscarListaDeDocumentosAnulados(pltvAnulaciones);
        }

        public void guardarEncabezadoAnulacion(TransaccionEncabezado pobjTransaccion)
        {
            HelperEncabezadoAnulacion _helper = new HelperEncabezadoAnulacion();

            _helper.guardarEncabezadoAnulacion(pobjTransaccion);
        }

        public decimal calcularTotalFlujoEfectivo(string pcodTipoDocumento)
        {
            HelperEncabezadoAnulacion _helper = new HelperEncabezadoAnulacion();

            return _helper.calcularTotalFlujoEfectivo(pcodTipoDocumento);
        }

        public string buscarCodigoDocumentosAnulados()
        {
            HelperEncabezadoAnulacion _helper = new HelperEncabezadoAnulacion();

            return _helper.buscarCodigoDocumentosAnulados();
        }

        public DateTime buscarFechaHoraDocumento(string pcodTransaction, string pcodTipoTransaccion, string pcodCliente)
        {
            HelperEncabezadoAnulacion _helper = new HelperEncabezadoAnulacion();

            return _helper.buscarFechaHoraDocumento(pcodTransaction, pcodTipoTransaccion, pcodCliente);
        }
    }
}
