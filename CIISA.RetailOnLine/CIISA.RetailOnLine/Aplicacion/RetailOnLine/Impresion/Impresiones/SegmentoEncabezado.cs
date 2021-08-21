using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionDocumento;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.PosicionReporte;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.Render;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using System;
using System.Collections.Generic;
using System.Data;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones
{
    internal class SegmentoEncabezado
    {
        internal void encabezadoCuentasBancarias(List<string> pprintingLinesList)
        {
            string _lineaUno = string.Empty;
            string _lineaDos = string.Empty;

            Position _position = new Position();

            _lineaUno += _position.tabular(_lineaUno.Length, RepCuentasBancarias.sigla);
            _lineaUno += "Sigla";
            _lineaUno += _position.tabular(_lineaUno.Length, RepCuentasBancarias.banco);
            _lineaUno += "Nombre";

            _lineaDos += _position.tabular(_lineaDos.Length, RepCuentasBancarias.banco);
            _lineaDos += "Número de cuenta";

            Line _line = new Line();

            _line.printingLinesList(pprintingLinesList, _lineaUno, 1);
            _line.printingLinesList(pprintingLinesList, _lineaDos, 1);
        }

        internal void encabezadoInventarioTeorico(List<string> pprintingLinesList)
        {
            string _lineaUno = string.Empty;
            string _lineaDos = string.Empty;

            Position _position = new Position();

            _lineaUno += _position.tabular(_lineaUno.Length, InventarioTeorico.codigo);
            _lineaUno += "Código";
            _lineaUno += _position.tabular(_lineaUno.Length, InventarioTeorico.descripcion);
            _lineaUno += "Descripción";

            _lineaDos += _position.tabular(_lineaDos.Length, InventarioTeorico.cantidad);
            _lineaDos += "Cant";
            _lineaDos += _position.tabular(_lineaDos.Length, InventarioTeorico.ventas);
            _lineaDos += "Vent";
            _lineaDos += _position.tabular(_lineaDos.Length, InventarioTeorico.devoluciones);
            _lineaDos += "Devo";
            _lineaDos += _position.tabular(_lineaDos.Length, InventarioTeorico.regalias);
            _lineaDos += "Rega";
            _lineaDos += _position.tabular(_lineaDos.Length, InventarioTeorico.anulaciones);
            _lineaDos += "Anul";
            _lineaDos += _position.tabular(_lineaDos.Length, InventarioTeorico.disponible);
            _lineaDos += "Disp";

            Line _line = new Line();

            _line.printingLinesList(pprintingLinesList, _lineaUno, 1);
            _line.printingLinesList(pprintingLinesList, _lineaDos, 1);
        }

        internal void encabezadoReporteEnLineaTrasladosDiarios(List<string> pprintingLinesList, DataTable pdt)
        {

            string _documento = string.Empty;
            string _fechaCreacion = string.Empty;
            string _fechaListo = string.Empty;

            string _bodegaOrigen = string.Empty;
            string _bodegaDestino = string.Empty;

            foreach (DataRow _fila in pdt.Rows)
            {
                _documento = _fila["NUM_DOCTO"].ToString();

                _fechaCreacion = _fila["FEC_CRE"].ToString();
                _fechaListo = _fila["FECHA_LISTO"].ToString();

                _bodegaOrigen = _fila["BODEGA_ORIGEN"].ToString();
                _bodegaDestino = _fila["BODEGA_DESTINO"].ToString();
            }

            Line _line = new Line();

            _line.printingLinesList(pprintingLinesList, "No. Traslado:        " + _documento, 1);
            _line.printingLinesList(pprintingLinesList, "Fecha Creación:      " + _fechaCreacion, 1);
            _line.printingLinesList(pprintingLinesList, "Fecha Alisto:        " + _fechaListo, 2);

            string _linea = string.Empty;

            Position _position = new Position();

            _linea += _position.tabular(_linea.Length, PosicionTD.linea);
            _linea += "No.";
            _linea += _position.tabular(_linea.Length, PosicionTD.pedido);
            _linea += "Pedido";
            _linea += _position.tabular(_linea.Length, PosicionTD.articulo);
            _linea += "Artículo";
            _linea += _position.tabular(_linea.Length, PosicionTD.cantidad);
            _linea += "Cantidad";
            _linea += _position.tabular(_linea.Length, PosicionTD.estado);
            _linea += "Estado";

            _line.printingLinesList(pprintingLinesList, _linea, 1);
        }

        internal void subEncabezadoTransaccion(List<string> pprintingLinesList, string pcodTransaction, string pnomTipoTransaccion, int pcopy)
        {
            Line _line = new Line();

            if (pnomTipoTransaccion.Equals(ROLTransactions._facturaContadoNombre))
            {
                _line.printingLinesList(pprintingLinesList, "Condición pago: Contado", 1);
            }

            if (pnomTipoTransaccion.Equals(ROLTransactions._facturaCreditoNombre))
            {
                _line.printingLinesList(pprintingLinesList, "Condición pago: Crédito", 1);
            }
        }

        internal void encabezadoTransaccion(List<string> pprintingLinesList, string pcodTipoTransaccion)
        {
            string _lineaUno = string.Empty;
            string _lineaDos = string.Empty;

            Position _position = new Position();

            Line _line = new Line();

            if (pcodTipoTransaccion.Equals(ROLTransactions._recaudacionSigla)
                || pcodTipoTransaccion.Equals(ROLTransactions._reciboDineroSigla)
                || pcodTipoTransaccion.Equals(ROLTransactions._tramiteSigla)
                || pcodTipoTransaccion.Equals(ROLTransactions._anulacionSigla)
                )
            {

                if (pcodTipoTransaccion.Equals(ROLTransactions._reciboDineroSigla))
                {
                    _lineaUno += _position.tabular(_lineaUno.Length, PosicionRD.factura);
                    _lineaUno += "Factura";
                    _lineaUno += _position.tabular(_lineaUno.Length, PosicionRD.saldoFactura);
                    _lineaUno += "Saldo Pend.";
                    _lineaUno += _position.tabular(_lineaUno.Length, PosicionRD.abono);
                    _lineaUno += "Abono";
                    _lineaUno += _position.tabular(_lineaUno.Length, PosicionRD.saldoAbono);
                    _lineaUno += "Saldo Act.";
                }

                if (pcodTipoTransaccion.Equals(ROLTransactions._recaudacionSigla))
                {
                    _lineaUno += "Por concepto de:";
                    _lineaUno += _position.tabular(_lineaUno.Length, PosicionER.monto);
                    _lineaUno += "Monto";
                }

                if (pcodTipoTransaccion.Equals(ROLTransactions._tramiteSigla))
                {
                    _lineaUno += "Código Documento";
                    _lineaUno += _position.tabular(_lineaUno.Length, Tramite.monto);
                    _lineaUno += "Monto";
                    _lineaUno += _position.tabular(_lineaUno.Length, Tramite.fecha);
                    _lineaUno += "Fecha";
                }

                if (pcodTipoTransaccion.Equals(ROLTransactions._anulacionSigla))
                {
                    _lineaUno += "Información anulación";
                }

                _line.printingLinesList(pprintingLinesList, _lineaUno, 1);
            }
            else
            {
                _lineaUno += "Descripción";

                if (pcodTipoTransaccion.Equals(ROLTransactions._ordenVentaSigla))
                {
                    _lineaUno += "/Especificación/Embalaje:";
                }
                else
                {
                    if (pcodTipoTransaccion.Equals(ROLTransactions._regaliaSigla))
                    {
                        _lineaUno += "/Motivo (regalía):";
                    }
                    else
                    {
                        _lineaUno += ":";
                    }
                }

                _lineaDos += _position.tabular(_lineaDos.Length, PosicionET._et_Codigo);
                _lineaDos += "Cód";
                _lineaDos += _position.tabular(_lineaDos.Length, PosicionET._et_Cantidad);
                _lineaDos += "Cant";
                _lineaDos += _position.tabular(_lineaDos.Length, PosicionET._et_Unidad);
                _lineaDos += "Und";
                _lineaDos += _position.tabular(_lineaDos.Length, PosicionET._et_Exento);
                _lineaDos += "Exc";
                _lineaDos += _position.tabular(_lineaDos.Length, PosicionET._et_Precio);
                _lineaDos += "Precio";
                _lineaDos += _position.tabular(_lineaDos.Length, PosicionET._et_amount);
                _lineaDos += "Monto";

                if (pcodTipoTransaccion.Equals(ROLTransactions._devolucionSigla))
                {
                    _lineaDos += Environment.NewLine;
                    _lineaDos += "Estado/Motivo/Comentario";
                }

                _line.printingLinesList(pprintingLinesList, _lineaUno, 1);
                _line.printingLinesList(pprintingLinesList, _lineaDos, 1);
            }

        }

        internal void encabezadoFormaPago_contenido(List<string> pprintingLinesList)
        {
            string _linea = string.Empty;

            Position _position = new Position();

            _linea += _position.tabular(_linea.Length, PosicionFP.linea);
            _linea += "No";
            _linea += _position.tabular(_linea.Length, PosicionFP.formaPago);
            _linea += "Forma Pago";
            _linea += _position.tabular(_linea.Length, PosicionFP.banco);
            _linea += "Banco";
            _linea += _position.tabular(_linea.Length, PosicionFP.numero);
            _linea += "Número";
            _linea += _position.tabular(_linea.Length, PosicionFP.monto);
            _linea += "Monto";

            Line _line = new Line();

            _line.printingLinesList(pprintingLinesList, _linea, 1);

            _line.doubleHypenLine(pprintingLinesList);
        }

        internal void encabezadoFormaPago(List<string> pprintingLinesList, string pcodTipoTransaccion)
        {
            if (pcodTipoTransaccion.Equals(ROLTransactions._facturaContadoSigla))
            {
                encabezadoFormaPago_contenido(pprintingLinesList);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._reciboDineroSigla))
            {
                encabezadoFormaPago_contenido(pprintingLinesList);
            }

            if (pcodTipoTransaccion.Equals(ROLTransactions._recaudacionSigla))
            {
                encabezadoFormaPago_contenido(pprintingLinesList);
            }
        }

        internal void encabezadoReporteIndicadoresFacturacion(List<string> pprintingLinesList)
        {
            string _lineaUno = string.Empty;

            Position _position = new Position();

            _lineaUno += _position.tabular(_lineaUno.Length, RepIndicadores.noCliente);
            _lineaUno += IndFacturacionNS._codigo;
            _lineaUno += _position.tabular(_lineaUno.Length, RepIndicadores.pedido);
            _lineaUno += IndFacturacionNS._pedido;
            _lineaUno += _position.tabular(_lineaUno.Length, RepIndicadores.facturaContado);
            _lineaUno += IndFacturacionNS._facturaContado;
            _lineaUno += _position.tabular(_lineaUno.Length, RepIndicadores.facturaCredito);
            _lineaUno += IndFacturacionNS._facturaCredito;
            _lineaUno += _position.tabular(_lineaUno.Length, RepIndicadores.respetaLimite);
            _lineaUno += IndFacturacionNS._respetaLimite;
            _lineaUno += _position.tabular(_lineaUno.Length, RepIndicadores.cheque);
            _lineaUno += IndFacturacionNS._cheque;
            _lineaUno += _position.tabular(_lineaUno.Length, RepIndicadores.montoLimite);
            _lineaUno += IndFacturacionNS._montoLimite;
            _lineaUno += _position.tabular(_lineaUno.Length, RepIndicadores.vencimiento);
            _lineaUno += IndFacturacionNS._vencimiento;
            _lineaUno += _position.tabular(_lineaUno.Length, RepIndicadores.estado);
            _lineaUno += IndFacturacionNS._estado;
            _lineaUno += _position.tabular(_lineaUno.Length, RepIndicadores.cobrador);
            _lineaUno += IndFacturacionNS._cobrador;
            _lineaUno += _position.tabular(_lineaUno.Length, RepIndicadores.cobro);
            _lineaUno += IndFacturacionNS._cobro;

            Line _line = new Line();

            _line.printingLinesList(pprintingLinesList, _lineaUno, 1);

            _line.doubleHypenLine(pprintingLinesList);
        }

        internal void encabezadoReporteVentasPorProducto(List<string> pprintingLinesList)
        {
            string _lineaUno = string.Empty;

            Position _position = new Position();

            _lineaUno += _position.tabular(_lineaUno.Length, RepVentas.codigo);
            _lineaUno += "Código";
            _lineaUno += _position.tabular(_lineaUno.Length, RepVentas.descripcion);
            _lineaUno += "Descripción";
            _lineaUno += _position.tabular(_lineaUno.Length, RepVentas.ventas);
            _lineaUno += "Ventas";

            Line _line = new Line();

            _line.printingLinesList(pprintingLinesList, _lineaUno, 1);

            _line.doubleHypenLine(pprintingLinesList);
        }

        internal void encabezadoReporteDocumentosRealizados(List<string> pprintingLinesList)
        {
            string _lineaUno = string.Empty;
            string _lineaDos = string.Empty;

            Position _position = new Position();

            _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.codigo);
            _lineaUno += "No. Docu.";
            _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.tipoDocumento);
            _lineaUno += "T.D.";
            _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.tramite);
            _lineaUno += "Tr.";
            _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.anulado);
            _lineaUno += "An.";
            _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.codCliente);
            _lineaUno += "Cod. C.";
            _lineaUno += _position.tabular(_lineaUno.Length, RepFacturas.total);
            _lineaUno += "M. Original";

            _lineaDos += _position.tabular(_lineaDos.Length, RepFacturas.cliente);
            _lineaDos += "Local";

            Line _line = new Line();

            _line.printingLinesList(pprintingLinesList, _lineaUno, 1);

            _line.printingLinesList(pprintingLinesList, _lineaDos, 1);

            _line.doubleHypenLine(pprintingLinesList);
        }

        internal void encabezadoReportePedidosRuta(List<string> pprintingLinesList)
        {
            string _lineaUno = string.Empty;
            string _lineaDos = string.Empty;
            string _lineaTres = string.Empty;
            string _lineaCuatro = string.Empty;

            Position _position = new Position();

            _lineaUno += _position.tabular(_lineaUno.Length, RepPedidosRuta.codigoCliente);
            _lineaUno += "Código";
            _lineaUno += _position.tabular(_lineaUno.Length, RepPedidosRuta.local);
            _lineaUno += "Local";

            _lineaDos += _position.tabular(_lineaDos.Length, RepPedidosRuta.codigoPedido);
            _lineaDos += "Número Pedido";

            _lineaTres += _position.tabular(_lineaTres.Length, RepPedidosRuta.codigoDescripcion);
            _lineaTres += "Código - Descripción";

            _lineaCuatro += _position.tabular(_lineaCuatro.Length, RepPedidosRuta.cantidadEspecificacion);
            _lineaCuatro += "Cantidad / Descripción";

            Line _line = new Line();

            _line.printingLinesList(pprintingLinesList, _lineaUno, 1);

            _line.printingLinesList(pprintingLinesList, _lineaDos, 1);

            _line.printingLinesList(pprintingLinesList, _lineaTres, 1);

            _line.printingLinesList(pprintingLinesList, _lineaCuatro, 1);

            _line.doubleHypenLine(pprintingLinesList);
        }

        internal void encabezadoReporteConsecutivoDocumentos(List<string> pprintingLinesList)
        {
            string _lineaUno = string.Empty;

            Position _position = new Position();

            _lineaUno += _position.tabular(_lineaUno.Length, RepConsecutivo.codigo);
            _lineaUno += "Documento";
            _lineaUno += _position.tabular(_lineaUno.Length, RepConsecutivo.local);
            _lineaUno += "No. Consecutivo";

            Line _line = new Line();

            _line.printingLinesList(pprintingLinesList, _lineaUno, 1);

            _line.doubleHypenLine(pprintingLinesList);
        }

        internal void encabezadoReporteCrediticioDeLaRuta(List<string> pprintingLinesList)
        {
            string _lineaUno = string.Empty;
            string _lineaDos = string.Empty;
            string _lineaTres = string.Empty;

            Position _position = new Position();

            _lineaUno += _position.tabular(_lineaUno.Length, RepCrediticioRuta.codigo);
            _lineaUno += "Código";
            _lineaUno += _position.tabular(_lineaUno.Length, RepCrediticioRuta.local);
            _lineaUno += "Local";

            _lineaDos += _position.tabular(_lineaDos.Length, RepCrediticioRuta.fecha);
            _lineaDos += "Fecha";
            _lineaDos += _position.tabular(_lineaDos.Length, RepCrediticioRuta.documento);
            _lineaDos += "No. Docu";
            _lineaDos += _position.tabular(_lineaDos.Length, RepCrediticioRuta.monto);
            _lineaDos += "M. Original";
            _lineaDos += _position.tabular(_lineaDos.Length, RepCrediticioRuta.vencimiento);
            _lineaDos += "Fecha Venc";

            _lineaTres += _position.tabular(_lineaTres.Length, RepCrediticioRuta.saldo);
            _lineaTres += "Saldo";
            _lineaTres += _position.tabular(_lineaTres.Length, RepCrediticioRuta.diasVencido);
            _lineaTres += "Días Venc";

            Line _line = new Line();

            _line.printingLinesList(pprintingLinesList, _lineaUno, 1);

            _line.printingLinesList(pprintingLinesList, _lineaDos, 1);

            _line.printingLinesList(pprintingLinesList, _lineaTres, 1);

            _line.doubleHypenLine(pprintingLinesList);
        }

        internal void encabezadoReporteCrediticioDelCliente(List<string> pprintingLinesList)
        {
            string _lineaUno = string.Empty;
            string _lineaDos = string.Empty;

            Position _position = new Position();

            _lineaUno += _position.tabular(_lineaUno.Length, RepCrediticioCliente.fecha);
            _lineaUno += "Fecha";
            _lineaUno += _position.tabular(_lineaUno.Length, RepCrediticioCliente.documento);
            _lineaUno += "No. Docu";
            _lineaUno += _position.tabular(_lineaUno.Length, RepCrediticioCliente.monto);
            _lineaUno += "M. Original";
            _lineaUno += _position.tabular(_lineaUno.Length, RepCrediticioCliente.vencimiento);
            _lineaUno += "Fecha Venc";

            _lineaDos += _position.tabular(_lineaDos.Length, RepCrediticioCliente.saldo);
            _lineaDos += "Saldo";
            _lineaDos += _position.tabular(_lineaDos.Length, RepCrediticioCliente.diasVencido);
            _lineaDos += "Días Venc";

            Line _line = new Line();

            _line.printingLinesList(pprintingLinesList, _lineaUno, 1);

            _line.printingLinesList(pprintingLinesList, _lineaDos, 1);

            _line.doubleHypenLine(pprintingLinesList);
        }

        internal void encabezadoReporteEnLineaOrdenesVenta(List<string> pprintingLinesList)
        {
            string _lineaUno = string.Empty;
            string _lineaDos = string.Empty;
            string _lineaTres = string.Empty;

            Position _position = new Position();

            _lineaUno += "Hora y Fecha Sincronización";

            _lineaDos += "Documento";
            _lineaDos += "/";
            _lineaDos += "Cliente";

            _lineaTres += "Línea";
            _lineaTres += "/";
            _lineaTres += "Código";
            _lineaTres += "/";
            _lineaTres += "Cantidad";
            _lineaTres += "/";
            _lineaTres += "Especificación";
            _lineaTres += "/";
            _lineaTres += "Embalaje";

            Line _line = new Line();

            _line.printingLinesList(pprintingLinesList, _lineaUno, 1);
            _line.printingLinesList(pprintingLinesList, _lineaDos, 1);
            _line.printingLinesList(pprintingLinesList, _lineaTres, 1);
        }

        internal void encabezadoInventarioTomaFisica(List<string> pprintingLinesList)
        {
            string _lineaUno = string.Empty;
            string _lineaDos = string.Empty;

            Position _position = new Position();

            _lineaUno += _position.tabular(_lineaUno.Length, PosicionITF.codigo);
            _lineaUno += "Código";
            _lineaUno += _position.tabular(_lineaUno.Length, PosicionITF.descripcion);
            _lineaUno += "Descripción";

            _lineaDos += _position.tabular(_lineaDos.Length, PosicionITF.cantidad);
            _lineaDos += "Cantidad";
            _lineaDos += _position.tabular(_lineaDos.Length, PosicionITF.disponible);
            _lineaDos += "Disponible";
            _lineaDos += _position.tabular(_lineaDos.Length, PosicionITF.tomaFisica);
            _lineaDos += "Toma Física";
            _lineaDos += _position.tabular(_lineaDos.Length, PosicionITF.diferencia);
            _lineaDos += "Diferencia";

            Line _line = new Line();

            _line.printingLinesList(pprintingLinesList, _lineaUno, 1);
            _line.printingLinesList(pprintingLinesList, _lineaDos, 1);
        }

        internal void encabezadoInventarioAuditoria(List<string> pprintingLinesList)
        {
            string _lineaUno = string.Empty;
            string _lineaDos = string.Empty;

            Position _position = new Position();

            _lineaUno += _position.tabular(_lineaUno.Length, InventarioAuditoria.codigo);
            _lineaUno += "Código";
            _lineaUno += _position.tabular(_lineaUno.Length, InventarioAuditoria.descripcion);
            _lineaUno += "Descripción";

            _lineaDos += _position.tabular(_lineaDos.Length, InventarioAuditoria.cantidad);
            _lineaDos += "Cantidad";
            _lineaDos += _position.tabular(_lineaDos.Length, InventarioAuditoria.disponible);
            _lineaDos += "Disponible";
            _lineaDos += _position.tabular(_lineaDos.Length, InventarioAuditoria.auditado);
            _lineaDos += "Auditado";
            _lineaDos += _position.tabular(_lineaDos.Length, InventarioAuditoria.diferencia);
            _lineaDos += "Diferencia";

            Line _line = new Line();

            _line.printingLinesList(pprintingLinesList, _lineaUno, 1);
            _line.printingLinesList(pprintingLinesList, _lineaDos, 1);
        }

        //COORPO
        internal void SubEncabezadoTransaccionAdicionalesFE(
            List<string> pprintingLinesList,
            string pcodTransaction,
            string pnomTipoTransaccion,
            int pcopy,
            Cliente pCliente,
            FacturaElectronica facturaElectronica
            )
        {
            Line _line = new Line();

            if (pnomTipoTransaccion.Equals(ROLTransactions._facturaContadoNombre) || pnomTipoTransaccion.Equals(ROLTransactions._facturaCreditoNombre))
            {
                if (pCliente.v_objEstablecimiento.v_objIndicador.v_numOrden == true)
                {
                    _line.printingLinesList(pprintingLinesList, "Número Orden: " + facturaElectronica.v_numOrden, 1);
                }

                if (pCliente.v_objEstablecimiento.v_objIndicador.v_fechaOrden == true)
                {
                    _line.printingLinesList(pprintingLinesList, "Fecha Orden: " + covertStringToDateTimeNuleableWithoutTime(facturaElectronica.v_fechaOrden), 1);
                }

                if (pCliente.v_objEstablecimiento.v_objIndicador.v_numRecibo == true)
                {
                    _line.printingLinesList(pprintingLinesList, "Número Recibo: " + facturaElectronica.v_numRecibo, 1);

                }
                if (pCliente.v_objEstablecimiento.v_objIndicador.v_fechaRecibo == true)
                {
                    _line.printingLinesList(pprintingLinesList, "Fecha Recibo: " + covertStringToDateTimeNuleableWithoutTime(facturaElectronica.v_fechaRecibo), 1);

                }
                if (pCliente.v_objEstablecimiento.v_objIndicador.v_codProveedor == true)
                {
                    _line.printingLinesList(pprintingLinesList, "Cód Proveedor: " + facturaElectronica.v_codProveedor, 1);
                }
            }
            else if (pnomTipoTransaccion.Equals(ROLTransactions._devolucionNombre))
            {
                if (pCliente.v_objEstablecimiento.v_objIndicador.v_numReclamo == true)
                {
                    _line.printingLinesList(pprintingLinesList, "Número Reclamo: " + facturaElectronica.v_numReclamo, 1);
                }

                if (pCliente.v_objEstablecimiento.v_objIndicador.v_fechaReclamo == true)
                {
                    _line.printingLinesList(pprintingLinesList, "Fecha Reclamo: " + covertStringToDateTimeNuleableWithoutTime(facturaElectronica.v_fechaReclamo), 1);
                }

                if (pCliente.v_objEstablecimiento.v_objIndicador.v_codProveedor == true)
                {
                    _line.printingLinesList(pprintingLinesList, "Cód Proveedor: " + facturaElectronica.v_codProveedor, 1);
                }
            }
        }

        private string covertStringToDateTimeNuleableWithoutTime(DateTime? dt)
        {
            if (dt == null)
            {
                return "n/a";
            }
            else
            {
                return ((DateTime)dt).ToString("dd/MM/yyyy");
            }
        }

    }
}
