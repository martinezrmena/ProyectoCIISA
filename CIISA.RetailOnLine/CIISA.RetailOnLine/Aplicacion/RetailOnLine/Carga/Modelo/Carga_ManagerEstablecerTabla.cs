using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Helpers;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Modelo
{
    public class Carga_ManagerEstablecerTabla
    {
        public Carga_ManagerEstablecerTabla(Editor ptextBox, Log plog)
        {
            crearTablas(ptextBox, plog);
        }

        public Carga_ManagerEstablecerTabla(string Table)
        {
            if (Table.Equals(TablesROL._indAnulacion))
            {
                crearTablaIndicadorAnulacion();
            }
        }

        #region Metodos SROL heredados
        public void crearTablaIndicadorAnulacion(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createIndicadorAnulacion.sql",
                TablesROL._indAnulacion,
                ptextBox,
                plog
                );
        }

        public void crearTablaIndicadorAnulacion()
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createIndicadorAnulacion.sql",
                TablesROL._indAnulacion
                );
        }

        public void crearTablaAgenteVendedor(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createAgenteVendedor.sql",
                TablesROL._agenteVendedor,
                ptextBox,
                plog
                );
        }

        public void crearTablaBitacora(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createBitacora.sql",
                TablesROL._bitacora,
                ptextBox,
                plog
                );
        }

        public void crearTablaBitacoraBK(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createBitacoraBK.sql",
                TablesROL._bitacorabk,
                ptextBox,
                plog
                );
        }

        public void crearTablaAutorizadoFirmar(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(

                "createAutorizadoFirmar.sql",
                TablesROL._autorizadoFirmar,
                ptextBox,
                plog
                );
        }

        public void crearTablaBanco(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createBanco.sql",
                TablesROL._banco,
                ptextBox,
                plog
                );
        }

        public void crearTablaCanton(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createCanton.sql",
                TablesROL._canton,
                ptextBox,
                plog
                );
        }

        public void crearTablaClasificacionCliente(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createClasificacionCliente.sql",
                TablesROL._cliente,
                ptextBox,
                plog
                );
        }

        public void crearTablaClave(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createClave.sql",
                TablesROL._clave,
                ptextBox,
                plog
                );
        }

        public void crearTablaCliente(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createCliente.sql",
                TablesROL._cliente,
                ptextBox,
                plog
                );
        }

        public void crearTablaCompannia(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createCompannia.sql",
                TablesROL._compannia,
                ptextBox,
                plog
                );
        }

        public void crearTablaCuentaCerrada(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createCuentaCerrada.sql",
                TablesROL._cuentaCerrada,
                ptextBox,
                plog
                );
        }

        public void crearTablaDescuentos(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createDescuentos.sql",
                TablesROL._descuentos,
                ptextBox,
                plog
                );
        }

        public void crearTablaDescuentoGeneral(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createDescuentoGeneral.sql",
                TablesROL._descuentoGeneral,
                ptextBox,
                plog
                );
        }

        public void crearTablaDetalleAnulacion(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createDetalleAnulacion.sql",
                TablesROL._detalleAnulacion,
                ptextBox,
                plog
                );
        }

        public void crearTablaDetalleAnulacionBK(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createDetalleAnulacionBK.sql",
                TablesROL._detalleAnulacionBK,
                ptextBox,
                plog
                );
        }

        public void crearTablaDetalleDocumento(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createDetalleDocumento.sql",
                TablesROL._detalleDocumento,
                ptextBox,
                plog
                );
        }

        public void crearTablaDetalleDocumentoBK(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createDetalleDocumentoBK.sql",
                TablesROL._detalleDocumentoBK,
                ptextBox,
                plog
                );
        }

        public void crearTablaDetallePedido(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createDetallePedido.sql",
                TablesROL._detallePedido,
                ptextBox,
                plog
                );
        }

        public void crearTablaDetalleRecibo(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createDetalleRecibo.sql",
                TablesROL._detalleRecibo,
                ptextBox,
                plog
                );
        }

        public void crearTablaDetalleReciboBK(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createDetalleReciboBK.sql",
                TablesROL._detalleReciboBK,
                ptextBox,
                plog
                );
        }

        public void crearTablaDetalleTramite(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createDetalleTramite.sql",
                TablesROL._detalleTramite,
                ptextBox,
                plog
                );
        }

        public void crearTablaDetalleTramiteBK(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createDetalleTramiteBK.sql",
                TablesROL._detalleTramiteBK,
                ptextBox,
                plog
                );
        }

        public void crearTablaDistrito(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createDistrito.sql",
                TablesROL._distrito,
                ptextBox,
                plog
                );
        }

        public void crearTablaEmbalaje(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createEmbalaje.sql",
                TablesROL._embalaje,
                ptextBox,
                plog
                );
        }

        public void crearTablaEncabezadoAnulacion(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createEncabezadoAnulacion.sql",
                TablesROL._encabezadoAnulacion,
                ptextBox,
                plog
                );
        }

        public void crearTablaEncabezadoAnulacionBK(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createEncabezadoAnulacionBK.sql",
                TablesROL._encabezadoAnulacionBK,
                ptextBox,
                plog
                );
        }

        public void crearTablaEncabezadoDocumento(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createEncabezadoDocumento.sql",
                TablesROL._encabezadoDocumento,
                ptextBox,
                plog
                );
        }

        public void crearTablaEncabezadoDocumentoBK(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createEncabezadoDocumentoBK.sql",
                TablesROL._encabezadoDocumentoBK,
                ptextBox,
                plog
                );
        }

        public void crearTablaEncabezadoPedido(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createEncabezadoPedido.sql",
                TablesROL._encabezadoPedido,
                ptextBox,
                plog
                );
        }

        public void crearTablaEncabezadoRecibo(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createEncabezadoRecibo.sql",
                TablesROL._encabezadoRecibo,
                ptextBox,
                plog
                );
        }

        public void crearTablaEncabezadoRazonesNV(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createEncabezadoRazonesNV.sql",
                TablesROL._encabezadoRazonesNV,
                ptextBox,
                plog
                );
        }

        public void crearTablaEncabezadoRazonesNVBK(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createEncabezadoRazonesNVBK.sql",
                TablesROL._encabezadoRazonesNVBK,
                ptextBox,
                plog
                );
        }

        public void crearTablaEncabezadoReciboBK(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createEncabezadoReciboBK.sql",
                TablesROL._encabezadoReciboBK,
                ptextBox,
                plog
                );
        }

        public void crearTablaEncabezadoTramite(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createEncabezadoTramite.sql",
                TablesROL._encabezadoTramite,
                ptextBox,
                plog
                );
        }

        public void crearTablaEncabezadoTramiteBK(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createEncabezadoTramiteBK.sql",
                TablesROL._encabezadoTramiteBK,
                ptextBox,
                plog
                );
        }

        public void crearTablaEspecificacion(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createEspecificacion.sql",
                TablesROL._especificacion,
                ptextBox,
                plog
                );
        }

        public void crearTablaEstablecimiento(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createEstablecimiento.sql",
                TablesROL._establecimiento,
                ptextBox,
                plog
                );
        }

        public void crearTablaFactura(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createFactura.sql",
                TablesROL._factura,
                ptextBox,
                plog
                );
        }

        public void crearTablaFechaServidor(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createFechaServidor.sql",
                TablesROL._fechaServidor,
                ptextBox,
                plog
                );
        }

        public void crearTablaFormaPago(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createFormaPago.sql",
                TablesROL._formaPago,
                ptextBox,
                plog
                );
        }

        public void crearTablaIndicadorFactura(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createIndicadorFactura.sql",
                TablesROL._indicadorFactura,
                ptextBox,
                plog
                );
        }

        public void crearTablaInformacionRuta(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createInformacionRuta.sql",
                TablesROL._informacionRuta,
                ptextBox,
                plog
                );
        }

        public void crearTablaInventario(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createInventario.sql",
                TablesROL._inventario,
                ptextBox,
                plog
                );
        }

        public void crearTablaInventarioBK(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createInventarioBK.sql",
                TablesROL._inventarioBK,
                ptextBox,
                plog
                );
        }

        public void crearTablaImpresora(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createImpresora.sql",
                TablesROL._impresora,
                ptextBox,
                plog
                );
        }

        public void crearTablaListaPrecios(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createListaPrecios.sql",
                TablesROL._listaPrecios,
                ptextBox,
                plog
                );
        }

        public void crearTablaPagoRecibo(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createPagoRecibo.sql",
                TablesROL._pagoRecibo,
                ptextBox,
                plog
                );
        }

        public void crearTablaPagoReciboBK(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createPagoReciboBK.sql",
                TablesROL._pagoReciboBK,
                ptextBox,
                plog
                );
        }

        public void crearTablaPagos(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createPagos.sql",
                TablesROL._pagos,
                ptextBox,
                plog
                );
        }

        public void crearTablaPagosBK(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createPagosBK.sql",
                TablesROL._pagosBK,
                ptextBox,
                plog
                );
        }

        public void crearTablaPais(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createPais.sql",
                TablesROL._pais,
                ptextBox,
                plog
                );
        }

        public void crearTablaPrecioCliente(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createPrecioCliente.sql",
                TablesROL._precioProducto,
                ptextBox,
                plog
                );
        }

        public void crearTablaPrecioProducto(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createPrecioProducto.sql",
                TablesROL._precioProducto,
                ptextBox,
                plog
                );
        }

        public void crearTablaProducto(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createProducto.sql",
                TablesROL._producto,
                ptextBox,
                plog
                );
        }

        public void crearTablaProvincia(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createProvincia.sql",
                TablesROL._provincia,
                ptextBox,
                plog
                );
        }

        public void crearTablaRazonesNV(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createRazonesNV.sql",
                TablesROL._razonesNV,
                ptextBox,
                plog
                );
        }

        public void crearTablaRuta(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createRuta.sql",
                TablesROL._ruta,
                ptextBox,
                plog
                );
        }

        public void crearTablaSistema(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createSistema.sql",
                TablesROL._sistema,
                ptextBox,
                plog
                );
        }

        public void crearTablaSms(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createSms.sql",
                TablesROL._sms,
                ptextBox,
                plog
                );
        }

        public void crearTablaSugerido(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createSugerido.sql",
                TablesROL._sugerido,
                ptextBox,
                plog
                );
        }

        public void crearTablaTipoIdentificacion(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createTipoIdentificacion.sql",
                TablesROL._tipoIdentificacion,
                ptextBox,
                plog
                );
        }

        public void crearTablaTipoTransaccion(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createTipoTransaccion.sql",
                TablesROL._tipoTransaccion,
                ptextBox,
                plog
                );
        }

        public void crearTablaTituloComprobante(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createTituloComprobante.sql",
                TablesROL._tituloComprobante,
                ptextBox,
                plog
                );
        }

        public void crearTablaMotivos(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createMotivo.sql",
                TablesROL._motivo,
                ptextBox,
                plog
                );
        }

        public void crearTablaVisitas(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createVisitas.sql",
                TablesROL._visitas,
                ptextBox,
                plog
                );
        }


        #endregion

        #region Carniceros

        public void crearTablaDetalleReses(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createDetalleReses.sql",
                TablesROL._DetalleReses,
                ptextBox,
                plog
                );
        }

        public void crearTablaDetalleResesBK(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createDetalleResesBK.sql",
                TablesROL._DetalleResesBK,
                ptextBox,
                plog
                );
        }

        public void crearTablaMensajeFactura(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createMensajeFactura.sql",
                TablesROL._MensajeFactura,
                ptextBox,
                plog
                );
        }
        #endregion

        #region Tipo Impresora
        public void crearTablaTipoImpresora(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createTipoImpresora.sql",
                TablesROL._TipoImpresora,
                ptextBox,
                plog
                );

            //Es necesario inicializar esta tabla
            HelperTipoImpresion helperTipoImpresion = new HelperTipoImpresion();
            helperTipoImpresion.InsertDefaultValue();
        }
        #endregion

        #region Exoneracion
        public void crearTablaExoneracion(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createExoneraciones.sql",
                TablesROL._Exoneracion,
                ptextBox,
                plog
                );
        }
        #endregion

        #region Simbologia Impresión
        public void crearTablaTiposIVA(Editor ptextBox, Log plog)
        {
            EstablishTable _establishTable = new EstablishTable();

            _establishTable.createTable(
                "createTiposIVA.sql",
                TablesROL._TiposIVA,
                ptextBox,
                plog
                );
        }
        #endregion

        public void crearTablas(Editor ptextBox, Log plog)
        {
            crearTablaIndicadorAnulacion(ptextBox, plog);

            crearTablaAgenteVendedor(ptextBox, plog);

            crearTablaBitacora(ptextBox, plog);

            crearTablaBitacoraBK(ptextBox, plog);

            crearTablaAutorizadoFirmar(ptextBox, plog);

            crearTablaBanco(ptextBox, plog);

            crearTablaCanton(ptextBox, plog);

            crearTablaClasificacionCliente(ptextBox, plog);

            crearTablaClave(ptextBox, plog);

            crearTablaCliente(ptextBox, plog);

            crearTablaCompannia(ptextBox, plog);

            crearTablaCuentaCerrada(ptextBox, plog);

            crearTablaDescuentos(ptextBox, plog);

            crearTablaDescuentoGeneral(ptextBox, plog);

            crearTablaDetalleAnulacion(ptextBox, plog);

            crearTablaDetalleAnulacionBK(ptextBox, plog);

            crearTablaDetalleDocumento(ptextBox, plog);

            crearTablaDetalleDocumentoBK(ptextBox, plog);

            crearTablaDetallePedido(ptextBox, plog);

            crearTablaDetalleRecibo(ptextBox, plog);

            crearTablaDetalleReciboBK(ptextBox, plog);

            crearTablaDetalleTramite(ptextBox, plog);

            crearTablaDetalleTramiteBK(ptextBox, plog);

            crearTablaDistrito(ptextBox, plog);

            crearTablaEmbalaje(ptextBox, plog);

            crearTablaEncabezadoAnulacion(ptextBox, plog);

            crearTablaEncabezadoAnulacionBK(ptextBox, plog);

            crearTablaEncabezadoDocumento(ptextBox, plog);

            crearTablaEncabezadoDocumentoBK(ptextBox, plog);

            crearTablaEncabezadoPedido(ptextBox, plog);

            crearTablaEncabezadoRecibo(ptextBox, plog);

            crearTablaEncabezadoRazonesNV(ptextBox, plog);

            crearTablaEncabezadoRazonesNVBK(ptextBox, plog);

            crearTablaEncabezadoReciboBK(ptextBox, plog);

            crearTablaEncabezadoTramite(ptextBox, plog);

            crearTablaEncabezadoTramiteBK(ptextBox, plog);

            crearTablaEspecificacion(ptextBox, plog);

            crearTablaEstablecimiento(ptextBox, plog);

            crearTablaFactura(ptextBox, plog);

            crearTablaFechaServidor(ptextBox, plog);

            crearTablaFormaPago(ptextBox, plog);

            crearTablaIndicadorFactura(ptextBox, plog);

            crearTablaInformacionRuta(ptextBox, plog);

            crearTablaInventario(ptextBox, plog);

            crearTablaInventarioBK(ptextBox, plog);

            crearTablaImpresora(ptextBox, plog);

            crearTablaListaPrecios(ptextBox, plog);

            crearTablaPagoRecibo(ptextBox, plog);

            crearTablaPagoReciboBK(ptextBox, plog);

            crearTablaPagos(ptextBox, plog);

            crearTablaPagosBK(ptextBox, plog);

            crearTablaPais(ptextBox, plog);

            crearTablaPrecioCliente(ptextBox, plog);

            crearTablaPrecioProducto(ptextBox, plog);

            crearTablaProducto(ptextBox, plog);

            crearTablaProvincia(ptextBox, plog);

            crearTablaRazonesNV(ptextBox, plog);

            crearTablaRuta(ptextBox, plog);

            crearTablaSistema(ptextBox, plog);

            crearTablaSms(ptextBox, plog);

            crearTablaSugerido(ptextBox, plog);

            crearTablaTipoIdentificacion(ptextBox, plog);

            crearTablaTipoTransaccion(ptextBox, plog);

            crearTablaTituloComprobante(ptextBox, plog);

            crearTablaMotivos(ptextBox, plog);

            crearTablaVisitas(ptextBox, plog);

            #region Carniceros

            crearTablaDetalleReses(ptextBox, plog);

            crearTablaDetalleResesBK(ptextBox, plog);

            crearTablaMensajeFactura(ptextBox, plog);

            #endregion

            #region TipoImpresora
            crearTablaTipoImpresora(ptextBox, plog);
            #endregion

            #region Exoneracion
            crearTablaExoneracion(ptextBox, plog);
            #endregion

            #region Simbologia Impresión
            crearTablaTiposIVA(ptextBox, plog);
            #endregion

        }
    }
}
