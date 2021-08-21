using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Droid.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Droid.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Droid.webServiceSROL_upload;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using Xamarin.Forms;

[assembly: Dependency(typeof(Service_WebServiceUpload))]
namespace CIISA.RetailOnLine.Droid.Aplicacion.Comunication.IWSSRol
{
    public class Service_WebServiceUpload : IService_WebServiceUpload
    {
        public string Get_Url()
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.Url;
        }
        public string Get_cargaExoneracion(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaExoneracion(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaTipoIva(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaTipoIva(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaAgenteVendedor(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaAgenteVendedor(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaSistema(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaSistema(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaCliente(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaCliente(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaEstablecimiento(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaEstablecimiento(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }    
        public string Get_cargaVisita(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaVisita(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaIndicadorFactura(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaIndicadorFactura(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaDescuento(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaDescuento(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaDescuentoGeneralCliente(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaDescuentoGeneralPorCliente(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaFactura_recarga_manualAgente(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaFactura_recarga_manualAgente(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaInventario_recarga_manualAgente(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaInventario_recarga_manualAgente(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }        
        public string Get_cargaEncabezadoPedido(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaEncabezadoPedido(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaDetallePedido(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaDetallePedido(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaListaPrecios(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaListaPrecios(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaPrecioProducto(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaPrecioProducto(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaProducto(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaProducto(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaImpresora(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaImpresora(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaClienteIndividual(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA,string pcodCliente)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaClienteIndividual(HomologateSystemCIISA_ROL.Upload(psystemCIISA),pcodCliente);
        }
        public string Get_cargaIndAnulacion(RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaIndAnulacion(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaEstablecimientoIndividual(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaEstablecimientoIndividual(HomologateSystemCIISA_ROL.Upload(psystemCIISA), pcodCliente);
        }
        public string Get_cargaVisitaIndividual(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaVisitaIndividual(HomologateSystemCIISA_ROL.Upload(psystemCIISA), pcodCliente);
        }
        public string Get_cargaIndicadorFacturaIndividual(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaIndicadorFacturaIndividual(HomologateSystemCIISA_ROL.Upload(psystemCIISA), pcodCliente);
        }
        public string Get_cargaDescuentoPorCodigoCliente(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaDescuentoPorCodigoCliente(HomologateSystemCIISA_ROL.Upload(psystemCIISA), pcodCliente);
        }
        public string Get_cargaClienteFacturacionFaltantes(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaClienteFacturacionFaltantes(HomologateSystemCIISA_ROL.Upload(psystemCIISA), pcodCliente);
        }
        public string Get_cargaEstablecimientoFacturacionFaltantes(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaEstablecimientoFacturacionFaltantes(HomologateSystemCIISA_ROL.Upload(psystemCIISA), pcodCliente);
        }
        public string Get_cargaVisitaFacturacionFaltantes(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaVisitaFacturacionFaltantes(HomologateSystemCIISA_ROL.Upload(psystemCIISA), pcodCliente);
        }
        public string Get_cargaIndicadorFacturaFacturacionFaltantes(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaIndicadorFacturaFacturacionFaltantes(HomologateSystemCIISA_ROL.Upload(psystemCIISA), pcodCliente);
        }
        public string Get_cargaClave(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaClave(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaAutorizadoFirmar(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaAutorizadoFirmar(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaEmbalaje(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaEmbalaje(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaFactura_inicial(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaFactura_inicial(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaFactura_seleccionTablas(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaFactura_seleccionTablas(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaFactura_recarga_diaria(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaFactura_recarga_diaria(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaInventario_inicial(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaInventario_inicial(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaInventario_seleccionTablas(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaInventario_seleccionTablas(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaInventario_recarga_diaria(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaInventario_recarga_diaria(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaInventario_sincronizacion(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaInventario_sincronizacion(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaPrecioCliente(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaPrecioCliente(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaSugerido(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaSugerido(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaBanco(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaBanco(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaCanton(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaCanton(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaClasificacionCliente(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaClasificacionCliente(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaCompannia(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaCompannia(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaCuentaCerrada(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaCuentaCerrada(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaDistrito(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaDistrito(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaEspecificacion(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaEspecificacion(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaFechaServidor(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaFechaServidor(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaFormaPago(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaFormaPago(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaInformacionRuta(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaInformacionRuta(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaMotivo(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaMotivo(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaPais(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaPais(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaProvincia(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaProvincia(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaRazonesNV(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaRazonesNV(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaRuta(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaRuta(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaSms(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaSms(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaTipoIdentificacion(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaTipoIdentificacion(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaTipoTransaccion(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaTipoTransaccion(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
        public string Get_cargaTituloComprobante(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaTituloComprobante(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }

        //Validacion 50mts
        public string cargaClienteFacturacionFaltantesGeo(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaClienteFacturacionFaltantesGeo(HomologateSystemCIISA_ROL.Upload(psystemCIISA), pcodCliente);
        }
        public string cargaClienteIndividualGeo(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaClienteIndividualGeo(HomologateSystemCIISA_ROL.Upload(psystemCIISA), pcodCliente);
        }
        public string cargaClienteGeo(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaClienteGeo(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }

        public string cargaIndicadorFacturaIndividualGeo(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaIndicadorFacturaIndividualGeo(HomologateSystemCIISA_ROL.Upload(psystemCIISA), pcodCliente);
        }
        public string cargaIndicadorFacturaGeo(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaIndicadorFacturaGeo(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }

        public string cargaIndicadorFacturaFacturacionFaltantesGeo(CIISA.RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA, string pcodCliente)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaIndicadorFacturaFacturacionFaltantesGeo(HomologateSystemCIISA_ROL.Upload(psystemCIISA), pcodCliente);
        }

        public string Get_cargaDetalleReses(RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaDetalleReses(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }

        public string Get_cargaMensajeFactura(RetailOnLine.Framework.Common.SystemInfo.SystemCIISA psystemCIISA)
        {
            VendedorCarga _vendedorCarga = new VendedorCarga();
            return _vendedorCarga.cargaMensajeFactura(HomologateSystemCIISA_ROL.Upload(psystemCIISA));
        }
    }
}