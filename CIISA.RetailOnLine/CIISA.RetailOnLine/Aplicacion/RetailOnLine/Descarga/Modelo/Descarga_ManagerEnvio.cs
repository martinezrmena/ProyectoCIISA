using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Compress;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Server.WS;
using System;
using System.Data;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Modelo
{
    public class Descarga_ManagerEnvio
    {
        private async void EnvioPaqueteInformacionPorWS_automatico(DataSet pds)
        {
            if (pds.Tables.Count > 0)
            {
                var _memoryCompress = DependencyService.Get<IMC_HH>();

                string _memoryStream = _memoryCompress.Zip_HH_DataSet("EnvioPaqueteInformacionPorWS", TypeTransaction._download, pds);

                if (_memoryStream.Equals(string.Empty))
                {
                    throw new Exception();
                }
                else
                {
                    var _servicio = DependencyService.Get<IService_WebServiceTotalDownload>();

                    string _tipoAgente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._typeAgent;

                    string _resultado = _servicio.Get_TotalSend_Automatic(
                                            _memoryStream,
                                            _tipoAgente,
                                            false,
                                            Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL),
                                            true
                                            );

                    if (_resultado.Equals(TypeEvents._errorWS))
                    {
                        throw new Exception();
                    }
                }
            }
            else
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();

                await _logMessageAttention.generalAttention("No hay información que enviar.");
            }

        }

        private void MarcarDatosComoEnviados_Documentos(DataSet pds, TransaccionEncabezado pobjTransaccionEncabezado, string TipoTransaccion)
        {
            Descarga_ManagerGenerico _manager = new Descarga_ManagerGenerico();

            foreach (DataTable _dt in pds.Tables)
            {
                string _table = _dt.TableName;

                if (!_table.Equals(TablesROL._agenteVendedor))
                {
                    if (_table.Equals(TablesROL._encabezadoTramite))
                    {
                        _manager.marcarDatosComoEnviados_Tramites(_table, pobjTransaccionEncabezado);
                    }
                    else if (_table.Equals(TablesROL._detalleTramite))
                    {
                        _manager.marcarDatosComoEnviados_DetalleTramites(_table, pobjTransaccionEncabezado);
                    }
                    else if (_table.Equals(TablesROL._encabezadoRecibo))
                    {
                        _manager.marcarDatosComoEnviados_Recibos(_table, pobjTransaccionEncabezado);
                    }
                    else if (_table.Equals(TablesROL._detalleRecibo))
                    {
                        _manager.marcarDatosComoEnviados_DetallesRecibos(_table, pobjTransaccionEncabezado);
                    }
                    else if (_table.Equals(TablesROL._pagos))
                    {
                        _manager.marcarDatosComoEnviados_Pagos(_table, pobjTransaccionEncabezado);
                    }
                    else if (_table.Equals(TablesROL._pagoRecibo))
                    {
                        _manager.marcarDatosComoEnviados_PagosRecibos(_table, pobjTransaccionEncabezado);
                    }
                    else
                    {
                        _manager.marcarDatosComoEnviados_Documentos(_table, pobjTransaccionEncabezado, TipoTransaccion);
                    }
                }
            }
        }

        public void EnvioPaqueteInformacionPorWS(TransaccionEncabezado pobjTransaccionEncabezado)
        {
            DataSet _ds = new DataSet();

            Descarga_ManagerDSEnvio _managerDSEnvio = new Descarga_ManagerDSEnvio();

            switch (pobjTransaccionEncabezado.v_objTipoDocumento.GetSigla())
            {
                case ROLTransactions._facturaContadoSigla:

                    _managerDSEnvio.Paquete_Documentos_Enviar(pobjTransaccionEncabezado, _ds, ROLTransactions._facturaContadoSigla);

                    _managerDSEnvio.Paquete_Pagos_Enviar(pobjTransaccionEncabezado, _ds);

                    EnvioPaqueteInformacionPorWS_automatico(_ds);

                    MarcarDatosComoEnviados_Documentos(_ds, pobjTransaccionEncabezado, ROLTransactions._facturaContadoSigla);

                    break;

                case ROLTransactions._facturaCreditoSigla:

                    _managerDSEnvio.Paquete_Documentos_Enviar(pobjTransaccionEncabezado, _ds, ROLTransactions._facturaCreditoSigla);

                    if (Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._typeAgent.Equals(Agent._carniceroSigla))
                    {
                        _managerDSEnvio.Paquete_Tramites_EnviarTotal(_ds);
                    }

                    EnvioPaqueteInformacionPorWS_automatico(_ds);

                    MarcarDatosComoEnviados_Documentos(_ds, pobjTransaccionEncabezado, ROLTransactions._facturaCreditoSigla);

                    break;

                case ROLTransactions._devolucionSigla:

                    _managerDSEnvio.Paquete_Documentos_Enviar(pobjTransaccionEncabezado, _ds, ROLTransactions._devolucionSigla);

                    EnvioPaqueteInformacionPorWS_automatico(_ds);

                    MarcarDatosComoEnviados_Documentos(_ds, pobjTransaccionEncabezado, ROLTransactions._devolucionSigla);

                    break;

                case ROLTransactions._regaliaSigla:

                    _managerDSEnvio.Paquete_Documentos_Enviar(pobjTransaccionEncabezado, _ds, ROLTransactions._regaliaSigla);

                    EnvioPaqueteInformacionPorWS_automatico(_ds);

                    MarcarDatosComoEnviados_Documentos(_ds, pobjTransaccionEncabezado, ROLTransactions._regaliaSigla);
                    break;

                case ROLTransactions._cotizacionSigla:

                    _managerDSEnvio.Paquete_Documentos_Enviar(pobjTransaccionEncabezado, _ds, ROLTransactions._cotizacionSigla);

                    EnvioPaqueteInformacionPorWS_automatico(_ds);

                    MarcarDatosComoEnviados_Documentos(_ds, pobjTransaccionEncabezado, ROLTransactions._cotizacionSigla);

                    break;

                case ROLTransactions._ordenVentaSigla:

                    _managerDSEnvio.Paquete_Documentos_Enviar(pobjTransaccionEncabezado, _ds, ROLTransactions._ordenVentaSigla);

                    EnvioPaqueteInformacionPorWS_automatico(_ds);

                    MarcarDatosComoEnviados_Documentos(_ds, pobjTransaccionEncabezado, ROLTransactions._ordenVentaSigla);

                    break;

                case ROLTransactions._reciboDineroSigla:

                    _managerDSEnvio.Paquete_Pagos_Enviar(pobjTransaccionEncabezado, _ds);

                    _managerDSEnvio.Paquete_Pagos_Recibo_Enviar(pobjTransaccionEncabezado, _ds);

                    EnvioPaqueteInformacionPorWS_automatico(_ds);

                    MarcarDatosComoEnviados_Documentos(_ds, pobjTransaccionEncabezado, ROLTransactions._reciboDineroSigla);
                    break;

                case ROLTransactions._recaudacionSigla:

                    _managerDSEnvio.Paquete_Pagos_Recibo_Enviar(pobjTransaccionEncabezado, _ds);

                    EnvioPaqueteInformacionPorWS_automatico(_ds);

                    MarcarDatosComoEnviados_Documentos(_ds, pobjTransaccionEncabezado, ROLTransactions._recaudacionSigla);
                    break;

                case ROLTransactions._tramiteSigla:

                    _managerDSEnvio.Paquete_Tramites_Enviar(pobjTransaccionEncabezado, _ds);

                    EnvioPaqueteInformacionPorWS_automatico(_ds);

                    MarcarDatosComoEnviados_Documentos(_ds, pobjTransaccionEncabezado, ROLTransactions._tramiteSigla);

                    break;

                default:

                    break;
            }
        }

        public void EnvioPaqueteInformacionPorWS_Bitacora(pnlBitacoraModel pnlBitacora)
        {
            DataSet _ds = new DataSet();

            Descarga_ManagerDSEnvio _managerDSEnvio = new Descarga_ManagerDSEnvio();

            _managerDSEnvio.Paquete_Bitacora(pnlBitacora, _ds);

            EnvioPaqueteInformacionPorWS_automatico(_ds);
        }

        private async Task EnvioPaqueteInformacionPorWS(DataSet pds, bool ptomaFisica)
        {
            if (pds.Tables.Count > 0)
            {
                var _memoryCompress = DependencyService.Get<IMC_HH>();

                string _memoryStream = _memoryCompress.Zip_HH_DataSet("EnvioPaqueteInformacionPorWS", TypeTransaction._download, pds);

                if (_memoryStream.Equals(string.Empty))
                {
                    throw new Exception();
                }
                else
                {
                    string _tipoAgente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._typeAgent;

                    var servicioTotalDownload = DependencyService.Get<IService_WebServiceTotalDownload>();

                    string _resultado = servicioTotalDownload.Get_TotalSend(_memoryStream, _tipoAgente, ptomaFisica, Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true);

                    if (_resultado.Equals(TypeEvents._errorWS))
                    {
                        throw new Exception();
                    }
                }
            }
            else
            {
                LogMessageAttention _logMessageAttention = new LogMessageAttention();

                await _logMessageAttention.generalAttention("No hay información que enviar.");
            }

        }

        private void MarcarDatosComoEnviados_Total(DataSet pds)
        {
            foreach (DataTable _dt in pds.Tables)
            {
                string _table = _dt.TableName;

                if (!_table.Equals(TablesROL._agenteVendedor))
                {
                    Descarga_ManagerGenerico _manager = new Descarga_ManagerGenerico();

                    _manager.marcarDatosComoEnviados(_table);
                }
            }
        }

        public async Task EnvioPaqueteInformacionPorWS_Total()
        {
            DataSet _ds = new DataSet();

            Descarga_ManagerDSEnvio _managerDSEnvio = new Descarga_ManagerDSEnvio();

            _managerDSEnvio.Paquete_Total_Enviar(_ds, TipoDescarga._normal);

            await EnvioPaqueteInformacionPorWS(_ds, false);

            MarcarDatosComoEnviados_Total(_ds);
        }

        public async Task EnvioPaqueteInformacionPorWS_TomaFisica()
        {
            DataSet _ds = new DataSet();

            Descarga_ManagerDSEnvio _managerDSEnvio = new Descarga_ManagerDSEnvio();

            _managerDSEnvio.Paquete_TomaFisica_Enviar(_ds);

            await EnvioPaqueteInformacionPorWS(_ds, true);
        }
    }
}
