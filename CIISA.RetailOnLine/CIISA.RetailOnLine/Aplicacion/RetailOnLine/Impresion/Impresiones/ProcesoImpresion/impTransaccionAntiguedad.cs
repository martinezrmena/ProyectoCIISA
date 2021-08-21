using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.ReportGenerator;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Print;
using System;
using System.Threading.Tasks;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion
{
    public class impTransaccionAntiguedad
    {
        private ProcesoImpresion v_procesoImpresion { get; set; }

        public impTransaccionAntiguedad(ProcesoImpresion pprocesoImpresion)
        {
            v_procesoImpresion = pprocesoImpresion;
        }

        internal async Task imprimirTransaccionAntiguedad(
            Cliente pobjCliente,
            string pcodTransaction,
            string pcodTipoTransaccion,
            string pnomTipoTransaccion,
            bool DevolucionFactura)
        {
            await v_procesoImpresion.v_impresora.connect();

            SegmentoTitulo _segmentoTitulo = new SegmentoTitulo();

            _segmentoTitulo.buscarTituloFacturaPorTipoDocumento(
                v_procesoImpresion.v_listaLineasImpresion,
                pcodTipoTransaccion
                );

            SegmentoEncabezado _segmentoEncabezado = new SegmentoEncabezado();

            _segmentoEncabezado.subEncabezadoTransaccion(
                v_procesoImpresion.v_listaLineasImpresion,
                pcodTransaction,
                pnomTipoTransaccion,
                100
                );

            SegmentoReimpresion _segmentoReimpresion = new SegmentoReimpresion();

            _segmentoReimpresion.tipoTransaccionCodigoTransaccion(
                v_procesoImpresion.v_listaLineasImpresion,
                pcodTransaction,
                pnomTipoTransaccion,
                true,
                pobjCliente.v_no_agente
                );

            if (pcodTipoTransaccion.Equals(
                ROLTransactions._facturaCreditoSigla)
                )
            {
                SegmentoPlazo _segmentoPlazo = new SegmentoPlazo();

                _segmentoPlazo.plazo(pobjCliente, v_procesoImpresion.v_listaLineasImpresion);
            }

            Logica_ManagerEncabezadoTransaccionBK _manager = new Logica_ManagerEncabezadoTransaccionBK();

            DateTime _fechaHoraDocumento = _manager.buscarFechaHoraDocumento(
                                            pcodTransaction,
                                            pcodTipoTransaccion
                                            );

            TimeDate _timeDate = new TimeDate();

            _timeDate.dateTimeTransaction(
                v_procesoImpresion.v_listaLineasImpresion,
                _fechaHoraDocumento,
                true
                );

            SegmentoDatos _segmentoDatos = new SegmentoDatos();

            _segmentoDatos.datosClienteTransaccion(
                v_procesoImpresion.v_listaLineasImpresion,
                pobjCliente,
                pcodTipoTransaccion
                );

            Line _line = new Line();

            _line.simpleHypenLine(
                v_procesoImpresion.v_listaLineasImpresion
                );


            _segmentoDatos.datosAgenteTransaccion(
                v_procesoImpresion.v_listaLineasImpresion,
                pcodTipoTransaccion
                );

            _segmentoEncabezado.encabezadoTransaccion(
                v_procesoImpresion.v_listaLineasImpresion,
                pcodTipoTransaccion
                );

            _line.doubleHypenLine(
                v_procesoImpresion.v_listaLineasImpresion
                );

            detallesTransaccionAntiguedad(
                pcodTransaction,
                pnomTipoTransaccion,
                pcodTipoTransaccion,
                pobjCliente,
                DevolucionFactura
                );

            SegmentoLeyenda _segmentoLeyenda = new SegmentoLeyenda();

            _segmentoLeyenda.pintarLeyendaProductosExentosIVA(
                v_procesoImpresion.v_listaLineasImpresion,
                pcodTipoTransaccion,
                pcodTransaction,
                pnomTipoTransaccion
                );

            SegmentoPie _segmentoPie = new SegmentoPie();

            _segmentoPie.pieTransaccion(
                v_procesoImpresion.v_listaLineasImpresion,
                pcodTipoTransaccion,
                pcodTransaction,
                pnomTipoTransaccion,
                true
                );
            SegmentoFirma _segmentoFirma = new SegmentoFirma();

            _segmentoFirma.firma(
                v_procesoImpresion.v_listaLineasImpresion,
                pcodTipoTransaccion
                );

            LeyendaCopia _leyendaCopia = new LeyendaCopia(v_procesoImpresion);

            await _leyendaCopia.enviarImprimirAgregarLeyendaCopia(
                pobjCliente,
                pcodTransaction,
                pcodTipoTransaccion
                );

            v_procesoImpresion.v_impresora.disconnect();

            Documents _documents = new Documents();

            _documents.printedDocumentsLog(
                v_procesoImpresion.v_listaLineasImpresion,
                Directorios.documentosReimpresosAntiguedad,
                Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent
                + " [" + pcodTipoTransaccion + "] "
                + pcodTransaction
                );
        }

        private void detallesTransaccionAntiguedad(
            string pcodTransaction,
            string pnomTipoTransaccion,
            string pcodTipoTransaccion,
            Cliente pobjCliente,
            bool DevolucionFactura)
        {
            Impresion_ManagerDetalleTransaccionBK _manager = new Impresion_ManagerDetalleTransaccionBK();

            _manager.buscarLineasDetalleImpresion(
               pcodTransaction,
               pcodTipoTransaccion,
               v_procesoImpresion.v_listaLineasImpresion,
               pobjCliente,
               DevolucionFactura
               );
        }


    }
}
