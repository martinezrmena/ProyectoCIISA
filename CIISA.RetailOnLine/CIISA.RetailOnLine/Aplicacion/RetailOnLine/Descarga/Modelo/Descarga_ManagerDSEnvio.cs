using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Helpers.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using System.Data;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Modelo
{
    internal class Descarga_ManagerDSEnvio
    {
        IFillDataSet_Table Fill = DependencyService.Get<IFillDataSet_Table>();

        internal void Paquete_Documentos_Enviar(TransaccionEncabezado pobjTransaccionEncabezado, DataSet pds, string TipoTransaccion)
        {
            HelperEncabezadoTransaccion _helperEncDoc = new HelperEncabezadoTransaccion();
            DataTable _dtEncDoc = _helperEncDoc.buscarEncabezadosDocumentosSinEnviar_SincronizacionAutomatica(pobjTransaccionEncabezado, TipoTransaccion);
            _dtEncDoc.TableName = TablesROL._encabezadoDocumento;
            Fill.FillTable(pds, _dtEncDoc);

            HelperDetalleTransaccion _helperDetDoc = new HelperDetalleTransaccion();
            DataTable _dtDetDoc = _helperDetDoc.buscarDetallesDocumentosSinEnviar_SincronizacionAutomatica(pobjTransaccionEncabezado, TipoTransaccion);
            _dtDetDoc.TableName = TablesROL._detalleDocumento;
            Fill.FillTable(pds, _dtDetDoc);
        }

        internal void Paquete_Pagos_Enviar(TransaccionEncabezado pobjTransaccionEncabezado, DataSet pds)
        {
            //Pagos
            HelperFormaPagoTransaccion _helperFPT = new HelperFormaPagoTransaccion();
            DataTable dtForDoc = _helperFPT.buscarPagosTransaccionesSinEnviar(TipoDescarga._normal, pobjTransaccionEncabezado);
            dtForDoc.TableName = TablesROL._pagos;
            Fill.FillTable(pds, dtForDoc);
        }

        internal void Paquete_Tramites_Enviar(TransaccionEncabezado pobjTransaccionEncabezado, DataSet pds)
        {
            HelperEncabezadoTramite _helperET = new HelperEncabezadoTramite();
            DataTable _dtEncTra = _helperET.buscarEncabezadosTramitesSinEnviar(TipoDescarga._normal, pobjTransaccionEncabezado);
            _dtEncTra.TableName = TablesROL._encabezadoTramite;
            Fill.FillTable(pds, _dtEncTra);

            HelperDetalleTramite _helperDT = new HelperDetalleTramite();
            DataTable _dtDetTra = _helperDT.buscarDetallesTramitesinEnviar(TipoDescarga._normal, pobjTransaccionEncabezado);
            _dtDetTra.TableName = TablesROL._detalleTramite;
            Fill.FillTable(pds, _dtDetTra);
        }

        internal void Paquete_Tramites_EnviarTotal(DataSet pds)
        {
            HelperEncabezadoTramite _helperET = new HelperEncabezadoTramite();
            DataTable _dtEncTra = _helperET.buscarEncabezadosTramitesSinEnviar(TipoDescarga._normal);
            _dtEncTra.TableName = TablesROL._encabezadoTramite;
            Fill.FillTable(pds, _dtEncTra);

            HelperDetalleTramite _helperDT = new HelperDetalleTramite();
            DataTable _dtDetTra = _helperDT.buscarDetallesTramitesinEnviar(TipoDescarga._normal);
            _dtDetTra.TableName = TablesROL._detalleTramite;
            Fill.FillTable(pds, _dtDetTra);
        }

        internal void Paquete_Pagos_Recibo_Enviar(TransaccionEncabezado pobjTransaccionEncabezado, DataSet pds)
        {

            HelperEncabezadoRecibo _helperEncRec = new HelperEncabezadoRecibo();
            DataTable _dtEncRec = _helperEncRec.buscarEncabezadosRecibosSinEnviarAutomatico(TipoDescarga._normal, pobjTransaccionEncabezado);
            _dtEncRec.TableName = TablesROL._encabezadoRecibo;
            Fill.FillTable(pds, _dtEncRec);

            HelperDetalleRecibo _helperDetRec = new HelperDetalleRecibo();
            DataTable _dtDetRec = _helperDetRec.buscarDetallesRecibosSinEnviar(TipoDescarga._normal, pobjTransaccionEncabezado);
            _dtDetRec.TableName = TablesROL._detalleRecibo;
            Fill.FillTable(pds, _dtDetRec);

            HelperFormaPagoRecibo _helperFPR = new HelperFormaPagoRecibo();
            DataTable dtForRec = _helperFPR.buscarPagosRecibosSinEnviar(TipoDescarga._normal, pobjTransaccionEncabezado);
            dtForRec.TableName = TablesROL._pagoRecibo;
            Fill.FillTable(pds, dtForRec);
        }

        /// <summary>
        /// Metodo que permite enviar una bitacora con datos recolectados por el agente rutero
        /// </summary>
        /// <param name="pnlBitacora"></param>
        /// <param name="pds"></param>
        internal void Paquete_Bitacora(pnlBitacoraModel pnlBitacora, DataSet pds)
        {
            HelperEncabezadoTransaccion _helperEncDoc = new HelperEncabezadoTransaccion();
            DataTable _dtEncDoc = Fill.DataTable_FormatBitacora(pnlBitacora);
            _dtEncDoc.TableName = TablesROL._bitacora;
            Fill.FillTable(pds, _dtEncDoc);
        }

        internal void Paquete_Total_Enviar(DataSet pds, string ptipoDescarga)
        {
            #region Documentos
            HelperEncabezadoTransaccion _helperEncDoc = new HelperEncabezadoTransaccion();
            DataTable _dtEncDoc = _helperEncDoc.buscarEncabezadosDocumentosSinEnviar(true, ptipoDescarga);
            _dtEncDoc.TableName = TablesROL._encabezadoDocumento;
            Fill.FillTable(pds, _dtEncDoc);

            HelperDetalleTransaccion _helperDetDoc = new HelperDetalleTransaccion();
            DataTable _dtDetDoc = _helperDetDoc.buscarDetallesDocumentosSinEnviar(true, ptipoDescarga);
            _dtDetDoc.TableName = TablesROL._detalleDocumento;
            Fill.FillTable(pds, _dtDetDoc);
            #endregion

            #region Pagos
            HelperFormaPagoTransaccion _helperFPT = new HelperFormaPagoTransaccion();
            DataTable dtForDoc = _helperFPT.buscarPagosTransaccionesSinEnviar(ptipoDescarga);
            dtForDoc.TableName = TablesROL._pagos;
            Fill.FillTable(pds, dtForDoc);
            #endregion

            #region Recibos
            HelperEncabezadoRecibo _helperEncRec = new HelperEncabezadoRecibo();
            DataTable _dtEncRec = _helperEncRec.buscarEncabezadosRecibosSinEnviar(ptipoDescarga);
            _dtEncRec.TableName = TablesROL._encabezadoRecibo;
            Fill.FillTable(pds, _dtEncRec);

            HelperDetalleRecibo _helperDetRec = new HelperDetalleRecibo();
            DataTable _dtDetRec = _helperDetRec.buscarDetallesRecibosSinEnviar(ptipoDescarga);
            _dtDetRec.TableName = TablesROL._detalleRecibo;
            Fill.FillTable(pds, _dtDetRec);
            #endregion

            #region Pagos Recibos
            HelperFormaPagoRecibo _helperFPR = new HelperFormaPagoRecibo();
            DataTable dtForRec = _helperFPR.buscarPagosRecibosSinEnviar(ptipoDescarga);
            dtForRec.TableName = TablesROL._pagoRecibo;
            Fill.FillTable(pds, dtForRec);
            #endregion

            #region Tramites
            HelperEncabezadoTramite _helperET = new HelperEncabezadoTramite();
            DataTable _dtEncTra = _helperET.buscarEncabezadosTramitesSinEnviar(ptipoDescarga);
            _dtEncTra.TableName = TablesROL._encabezadoTramite;
            Fill.FillTable(pds, _dtEncTra);

            HelperDetalleTramite _helperDT = new HelperDetalleTramite();
            DataTable _dtDetTra = _helperDT.buscarDetallesTramitesinEnviar(ptipoDescarga);
            _dtDetTra.TableName = TablesROL._detalleTramite;
            Fill.FillTable(pds, _dtDetTra);
            #endregion

            #region Anulaciones
            HelperEncabezadoAnulacion _helperEA = new HelperEncabezadoAnulacion();
            DataTable _dtEncAnu = _helperEA.buscarEncabezadosAnulacionesSinEnviar(ptipoDescarga);
            _dtEncAnu.TableName = TablesROL._encabezadoAnulacion;
            Fill.FillTable(pds, _dtEncAnu);

            HelperDetalleAnulacion _helperDA = new HelperDetalleAnulacion();
            DataTable _dtDetAnu = _helperDA.buscarDetallesAnulacionesSinEnviar(ptipoDescarga);
            _dtDetAnu.TableName = TablesROL._detalleAnulacion;
            Fill.FillTable(pds, _dtDetAnu);
            #endregion

            HelperEncabezadoRazonesNV _helperERNV = new HelperEncabezadoRazonesNV();
            DataTable _dtEncRNV = _helperERNV.buscarEncabezadosRazonesNVSinEnviar(ptipoDescarga);
            _dtEncRNV.TableName = TablesROL._encabezadoRazonesNV;
            Fill.FillTable(pds, _dtEncRNV);

            HelperCliente _helperC = new HelperCliente();
            DataTable _dtDetRNV = _helperC.buscarClienteSinEnviar();
            _dtDetRNV.TableName = TablesROL._cliente;
            Fill.FillTable(pds, _dtDetRNV);

            Paquete_TomaFisica_Enviar(pds);

            HelperAgenteVendedor _helperAV = new HelperAgenteVendedor();
            DataTable _dtAngVen = _helperAV.buscarConsecutivosDocumentos();
            _dtAngVen.TableName = TablesROL._agenteVendedor;
            Fill.FillTable(pds, _dtAngVen);

            //Detalle Reses
            HelperDetalleReses _helperDR = new HelperDetalleReses();
            DataTable _dtDelRes = _helperDR.buscarDetallesResesSinEnviar();
            _dtDelRes.TableName = TablesROL._DetalleReses;
            Fill.FillTable(pds, _dtDelRes);

            //Bitacora
            HelperBitacora _helperBi = new HelperBitacora();
            DataTable _dtBitacora = _helperBi.buscarBitacora();
            _dtBitacora.TableName = TablesROL._bitacora;
            Fill.FillTable(pds, _dtBitacora);

        }

        internal void Paquete_TomaFisica_Enviar(DataSet pds)
        {
            HelperInventario _helperI = new HelperInventario();
            DataTable _dtInv = _helperI.BuscarInventario();
            _dtInv.TableName = TablesROL._inventario;
            Fill.FillTable(pds, _dtInv);
        }
    }
}
