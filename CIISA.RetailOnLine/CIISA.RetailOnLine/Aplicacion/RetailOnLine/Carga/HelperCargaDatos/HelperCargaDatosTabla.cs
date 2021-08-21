using CIISA.RetailOnLine.Framework.Common.Feedback;
using System.Data;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.HelperCargaDatos
{
    public class HelperCargaDatosTabla
    {
        //private BacklightSymbol v_backlight = null;

        public HelperCargaDatosTabla()
        {
            //v_backlight = new BacklightSymbol();

            //v_backlight.backlight_Load();
        }

        internal async Task cargaTablaExoneracion(
            string ptable,
            DataTable pdt,
            Editor ptextBox,
            Label plabel,
            Log plog)
        {
            HelperCargaDatosTablaExoneracion _helper = new HelperCargaDatosTablaExoneracion();

            await _helper.cargaTablaExoneracion(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }

        internal async Task cargaTablaClasificacionCliente(
            string ptable,
            DataTable pdt,
            Editor ptextBox,
            Label plabel,
            Log plog)
        {
            HelperCargaDatosTablaClasificacionCliente _helper = new HelperCargaDatosTablaClasificacionCliente();

            await _helper.cargaTablaClasificacionCliente(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }

        internal async Task cargaTablaCompannia(
            string ptable,
            DataTable pdt,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            HelperCargaDatosTablaCompannia _helper = new HelperCargaDatosTablaCompannia();

            await _helper.cargaTablaCompannia(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }

        internal async Task cargaTablaCuentaCerrada(
            string ptable,
            DataTable pdt,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            HelperCargaDatosTablaCuentaCerrada _helper = new HelperCargaDatosTablaCuentaCerrada();

            await _helper.cargaTablaCuentaCerrada(
                //v_backlight,
                ptable, pdt, ptextBox, plabel, plog);
        }

        internal async Task cargaTablaDistrito(
            string ptable,
            DataTable pdt,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            HelperCargaDatosTablaDistrito _helper = new HelperCargaDatosTablaDistrito();

            await _helper.cargaTablaDistrito(
                //v_backlight,
                ptable, pdt, ptextBox, plabel, plog);
        }

        internal async Task cargaTablaEspecificacion(
            string ptable,
            DataTable pdt,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            HelperCargaDatosTablaEspecificacion _helper = new HelperCargaDatosTablaEspecificacion();

            await _helper.cargaTablaEspecificacion(
                //v_backlight,
                ptable, pdt, ptextBox, plabel, plog);
        }

        public async Task cargaTablaEstablecimientoFacturacionFaltantes(
            string ptable,
            DataTable pdt,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            HelperCargaDatosTablaEstablecimiento _helper = new HelperCargaDatosTablaEstablecimiento();

            await _helper.cargaTablaEstablecimientoFacturacionFaltantes(
                //v_backlight,
                ptable, pdt, ptextBox, plabel, plog);
        }

        internal async Task cargaTablaFechaServidor(
            string ptable,
            DataTable pdt,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            HelperCargaDatosTablaFechaServidor _helper = new HelperCargaDatosTablaFechaServidor();

            await _helper.cargaTablaFechaServidor(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }

        internal async Task cargaTablaFormaPago(
            string ptable,
            DataTable pdt,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            HelperCargaDatosTablaFormaPago _helper = new HelperCargaDatosTablaFormaPago();

            await _helper.cargaTablaFormaPago(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }

        internal async Task cargaTablaInformacionRuta(
            string ptable,
            DataTable pdt,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            HelperCargaDatosTablaInformacionRuta _helper = new HelperCargaDatosTablaInformacionRuta();

            await _helper.cargaTablaInformacionRuta(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }

        internal async Task cargaTablaMotivo(
            string ptable,
            DataTable pdt,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            HelperCargaDatosTablaMotivo _helper = new HelperCargaDatosTablaMotivo();

            await _helper.cargaTablaMotivo(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }

        internal async Task cargaTablaPais(
            string ptable,
            DataTable pdt,
            Editor ptextBox,
            Label plabel,
            Log plog
            )
        {
            HelperCargaDatosTablaPais _helper = new HelperCargaDatosTablaPais();

            await _helper.cargaTablaPais(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }


        internal async Task cargaTablaAgenteVendedor(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaAgenteVendedor _helper = new HelperCargaDatosTablaAgenteVendedor();

            await _helper.cargaTablaAgenteVendedor(
                //v_backlight, 
                ptable, 
                pdt, 
                ptextBox, 
                plabel, 
                plog);
        }
        public async Task cargaTablaEncabezadoPedido(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaEncabezadoPedido _helper = new HelperCargaDatosTablaEncabezadoPedido();

            await _helper.cargaTablaEncabezadoPedido(
                //v_backlight, 
                ptable, 
                pdt, 
                ptextBox, 
                plabel, 
                plog);
        }
        public async Task cargaTablaDetallePedido(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaDetallePedido _helper = new HelperCargaDatosTablaDetallePedido();

            await _helper.cargaTablaDetallePedido(
                //v_backlight, 
                ptable, 
                pdt, 
                ptextBox, 
                plabel, 
                plog);
        }
        public async Task cargaTablaSistema(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaSistema _helper = new HelperCargaDatosTablaSistema();

            await _helper.cargaTablaSistema(
                //v_backlight, 
                ptable, 
                pdt, 
                ptextBox, 
                plabel, 
                plog);
        }
        public async Task cargaTablaClienteFacturacionFaltantes(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaCliente _helper = new HelperCargaDatosTablaCliente();

            await _helper.cargaTablaClienteFacturacionFaltantes(
                //v_backlight, 
                ptable, 
                pdt, 
                ptextBox, 
                plabel, 
                plog);
        }
        public async Task cargaTablaEstablecimiento(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaEstablecimiento _helper = new HelperCargaDatosTablaEstablecimiento();

            await _helper.cargaTablaEstablecimientoTodos(
                //v_backlight, 
                ptable, 
                pdt, 
                ptextBox, 
                plabel, 
                plog);
        }
        public async Task cargaTablaVisitaFacturacionFaltantes(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaVisita _helper = new HelperCargaDatosTablaVisita();

            await _helper.cargaTablaVisitaFacturacionFaltantes(
                //v_backlight, 
                ptable, 
                pdt, 
                ptextBox, 
                plabel, 
                plog);
        }
        public async Task cargaTablaIndicadorFacturaFacturacionFaltantes(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaIndicadorFactura _helper = new HelperCargaDatosTablaIndicadorFactura();

            await _helper.cargaTablaIndicadorFacturaFacturacionFaltantes(
                //v_backlight, 
                ptable, 
                pdt, 
                ptextBox, 
                plabel, 
                plog);
        }
        public async Task cargaTablaClave(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaClave _helper = new HelperCargaDatosTablaClave();

            await _helper.cargaTablaClave(
                //v_backlight, 
                ptable, 
                pdt, 
                ptextBox, 
                plabel, 
                plog);
        }
        public async Task cargaTablaAutorizadoFirmar(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaAutorizadoFirmar _helper = new HelperCargaDatosTablaAutorizadoFirmar();

            await _helper.cargaTablaAutorizadoFirmar(
                //v_backlight, 
                ptable, 
                pdt, 
                ptextBox, 
                plabel, 
                plog);
        }
        public async Task cargaTablaCliente(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaCliente _helper = new HelperCargaDatosTablaCliente();

            await _helper.cargaTablaClienteTodos(
                //v_backlight, 
                ptable, 
                pdt, 
                ptextBox, 
                plabel, 
                plog);
        }
        public async Task cargaTablaDescuento(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaDescuento _helper = new HelperCargaDatosTablaDescuento();

            await _helper.cargaTablaDescuento(
                //v_backlight, 
                ptable, 
                pdt, 
                ptextBox, 
                plabel, 
                plog);
        }
        public async Task cargaTablaDescuentoGeneral(string ptable, DataTable pdt, Editor ptextBox, Label plabel, Log plog)
        {
            HelperCargaDatosTablaDescuentoGeneral _helper = new HelperCargaDatosTablaDescuentoGeneral();

            await _helper.cargaTablaDescuentoGeneral(
                ptable,
                pdt,
                ptextBox,
                plabel,
                plog);
        }
        public async Task cargaTablaDetalleReses(string ptable, DataTable pdt, Editor ptextBox, Label plabel, Log plog)
        {
            HelperCargaDatosTablaDetalleReses _helper = new HelperCargaDatosTablaDetalleReses();

            await _helper.cargaTablaDetalleReses(
                ptable,
                pdt,
                ptextBox,
                plabel,
                plog);
        }

        public async Task cargaTablaMensajeFactura(string ptable, DataTable pdt, Editor ptextBox, Label plabel, Log plog)
        {
            HelperCargaDatosTablaMensajeFactura _helper = new HelperCargaDatosTablaMensajeFactura();

            await _helper.cargaTablaMensajeFactura(
                ptable,
                pdt,
                ptextBox,
                plabel,
                plog);
        }
        public async Task cargaTablaEmbalaje(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaEmbalaje _helper = new HelperCargaDatosTablaEmbalaje();

            await _helper.cargaTablaEmbalaje(
                //v_backlight, 
                ptable, 
                pdt, 
                ptextBox, 
                plabel, 
                plog);
        }
        public async Task cargaTablaFactura(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaFactura _helper = new HelperCargaDatosTablaFactura();

            await _helper.cargaTablaFactura(
                //v_backlight, 
                ptable, 
                pdt, 
                ptextBox, 
                plabel, 
                plog);
        }
        public async Task cargaTablaIndicadorFactura(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaIndicadorFactura _helper = new HelperCargaDatosTablaIndicadorFactura();

            await _helper.cargaTablaIndicadorFacturaTodos(
                //v_backlight, 
                ptable, 
                pdt, 
                ptextBox, 
                plabel, 
                plog);
        }
        public async Task cargaTablaInventario(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaInventario _helper = new HelperCargaDatosTablaInventario();

            await _helper.cargaTablaInventario(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }
        internal async Task cargaTablaImpresora(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaImpresora _helper = new HelperCargaDatosTablaImpresora();

            await _helper.cargaTablaImpresora(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }
        public async Task cargaTablaListaPrecios(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaListaPrecios _helper = new HelperCargaDatosTablaListaPrecios();

            await _helper.cargaTablaListaPrecios(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }
        public async Task cargaTablaPrecioCliente(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaPrecioCliente _helper = new HelperCargaDatosTablaPrecioCliente();

            await _helper.cargaTablaPrecioCliente(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }
        public async Task cargaTablaPrecioProducto(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaPrecioProducto _helper = new HelperCargaDatosTablaPrecioProducto();

            await _helper.cargaTablaPrecioProducto(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }
        public async Task cargaTablaProducto(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaProducto _helper = new HelperCargaDatosTablaProducto();

            await _helper.cargaTablaProducto(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }
        public async Task cargaTablaTipoIVA(string ptable, DataTable pdt, Editor ptextBox, Label plabel, Log plog)
        {
            HelperCargaDatosTablaTipoIVA _helper = new HelperCargaDatosTablaTipoIVA();

            await _helper.cargaTablaTipoIVA(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }
        public async Task cargaTablaSugerido(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaSugerido _helper = new HelperCargaDatosTablaSugerido();

            await _helper.cargaTablaSugerido(
                //v_backlight,
                ptable, pdt, ptextBox, plabel, plog);
        }
        public async Task cargaTablaVisita(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaVisita _helper = new HelperCargaDatosTablaVisita();

            await _helper.cargaTablaVisitaTodos(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }
        internal async Task cargaTablaBanco(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaBanco _helper = new HelperCargaDatosTablaBanco();

            await _helper.cargaTablaBanco(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }
        internal async Task cargaTablaCanton(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaCanton _helper = new HelperCargaDatosTablaCanton();

            await _helper.cargaTablaCanton(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }
        internal async Task cargaTablaTipoIdentificacion(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaTipoIdentificacion _helper = new HelperCargaDatosTablaTipoIdentificacion();

            await _helper.cargaTablaTipoIdentificacion(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }
        internal async Task cargaTablaTipoTransaccion(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaTipoTransaccion _helper = new HelperCargaDatosTablaTipoTransaccion();

            await _helper.cargaTablaTipoTransaccion(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }
        internal async Task cargaTablaTituloComprobante(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaTituloComprobante _helper = new HelperCargaDatosTablaTituloComprobante();

            await _helper.cargaTablaTituloComprobante(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }
        internal async Task cargaTablaProvincia(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaProvincia _helper = new HelperCargaDatosTablaProvincia();

            await _helper.cargaTablaProvincia(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }
        internal async Task cargaTablaRazonesNV(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaRazonesNV _helper = new HelperCargaDatosTablaRazonesNV();

            await _helper.cargaTablaRazonesNV(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }
        internal async Task cargaTablaRuta(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaRuta _helper = new HelperCargaDatosTablaRuta();

            await _helper.cargaTablaRuta(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }
        public async Task cargaTablaSms(string ptable,DataTable pdt,Editor ptextBox,Label plabel,Log plog)
        {
            HelperCargaDatosTablaSms _helper = new HelperCargaDatosTablaSms();

            await _helper.cargaTablaSms(
                //v_backlight, 
                ptable, pdt, ptextBox, plabel, plog);
        }
    }
}
