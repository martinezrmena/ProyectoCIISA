using CIISA.RetailOnLine.Framework.Common.SystemInfo;

namespace CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol
{
    public interface IService_WebServiceUpload
    {
        string Get_Url();
        string Get_cargaTipoIva(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaExoneracion(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaAgenteVendedor(Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaSistema(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaCliente(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaEstablecimiento(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaVisita(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaIndicadorFactura(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaDescuento(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaDescuentoGeneralCliente(SystemCIISA psystemCIISA);
        string Get_cargaFactura_recarga_manualAgente(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaInventario_recarga_manualAgente(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaEncabezadoPedido(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaDetallePedido(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaListaPrecios(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaPrecioProducto(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaProducto(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaIndAnulacion(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaImpresora(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaClienteIndividual(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente);
        string Get_cargaEstablecimientoIndividual(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente);
        string Get_cargaVisitaIndividual(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente);
        string Get_cargaIndicadorFacturaIndividual(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente);
        string Get_cargaDescuentoPorCodigoCliente(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente);
        string Get_cargaInventario_inicial(Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaClienteFacturacionFaltantes(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente);
        string Get_cargaEstablecimientoFacturacionFaltantes(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente);
        string Get_cargaVisitaFacturacionFaltantes(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente);
        string Get_cargaIndicadorFacturaFacturacionFaltantes(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente);
        string Get_cargaClave(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaAutorizadoFirmar(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaEmbalaje(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaFactura_inicial(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaFactura_seleccionTablas(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaFactura_recarga_diaria(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaInventario_seleccionTablas(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaInventario_recarga_diaria(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaInventario_sincronizacion(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaPrecioCliente(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaSugerido(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaBanco(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaCanton(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaClasificacionCliente(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaCompannia(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaCuentaCerrada(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaDistrito(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaEspecificacion(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaFechaServidor(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaFormaPago(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaInformacionRuta(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaMotivo(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaPais(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaProvincia(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaRazonesNV(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaRuta(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaSms(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaTipoIdentificacion(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaTipoTransaccion(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);
        string Get_cargaTituloComprobante(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);

        //Validacion 50mts
        string cargaClienteFacturacionFaltantesGeo(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente);
        string cargaClienteIndividualGeo(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente);
        string cargaClienteGeo(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);

        string cargaIndicadorFacturaIndividualGeo(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente);
        string cargaIndicadorFacturaGeo(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA);

        string cargaIndicadorFacturaFacturacionFaltantesGeo(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente);

        //Carniceria
        string Get_cargaDetalleReses(SystemCIISA psystemCIISA);
        string Get_cargaMensajeFactura(SystemCIISA psystemCIISA);
    }
}
