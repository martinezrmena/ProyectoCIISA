﻿using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.VistaControlador;
using CIISA.RetailOnLine.Framework.Common.Compress;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Server.WS;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Controlador
{
    public class ctrlCarga_visita
    {
        private ctrlCargaMenu controlador { get; set; }

        internal ctrlCarga_visita(ctrlCargaMenu pcontrolador)
        {
            controlador = pcontrolador;
        }

        public async void informacionVisitaRecargar()
        {
            GUIRecargaManual _guiCarga = new GUIRecargaManual(controlador);

            _guiCarga.actualizarGUIdesdeHilo_0_porciento();

            _guiCarga.actualizarGUIdesdeHiloResultadoCarga(string.Empty, Color.White);

            string _resultadoCarga = string.Empty;

            Editor _textBox = new Editor();

            var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

            if (await _testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
            {
                _guiCarga.actualizarGUIdesdeHilo_25_porciento();

                Log _log = new Log();

                var ServicioUpload = DependencyService.Get<IService_WebServiceUpload>();

                string _memoryStream = ServicioUpload.Get_cargaVisita(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));

                if (!_memoryStream.Equals(TypeEvents._errorWS))
                {
                    _log.addLineSuccessWSDownload(_textBox, TablesROL._visitas);

                    var _memoryCompress = DependencyService.Get<IMC_HH>();

                    DataTable _dt = _memoryCompress.Unzip_HH_DataTable("informacionVisitaRecargar", TypeTransaction._upload, _memoryStream, TablesROL._visitas);

                    _guiCarga.actualizarGUIdesdeHilo_45_porciento();

                    if (_dt.Rows == null)
                    {
                        _log.addErrorLineDataTableNull(_textBox, TablesROL._visitas);

                        _guiCarga.actualizarGUIdesdeHiloError();
                    }
                    else
                    {
                        if (_dt.Rows.Count != 0)
                        {
                            OperationSQL.deleteTableFeedbackTextBox(
                                TablesROL._visitas,
                                _textBox,
                                _log
                                );

                            _guiCarga.actualizarGUIdesdeHilo_65_porciento();

                            int _inserciones = Numeric._zeroInteger;

                            foreach (DataRow _fila in _dt.Rows)
                            {
                                Carga_ManagerVisita _manager = new Carga_ManagerVisita();

                                StringBuilder _sb = _manager.insertTablaVisita(_fila);
                                var MultiGeneric = DependencyService.Get<IMultiGeneric>();
                                _inserciones += MultiGeneric.uploadGenericTable(_sb);

                                _guiCarga.actualizarGUIdesdeHilo_progresoInsercion(_inserciones, _dt.Rows.Count);
                            }

                            _log.registrationNumberInserts(_textBox, _inserciones, _dt.Rows.Count, TablesROL._visitas);

                            _guiCarga.actualizarGUIdesdeHilo_85_porciento();
                        }
                        else
                        {
                            _log.addAlertLine(_textBox, "la consulta a la tabla " + TablesROL._visitas + " no devolvio resultados.");

                            _guiCarga.actualizarGUIdesdeHiloError();
                        }
                    }
                }
                else
                {
                    _guiCarga.actualizarGUIdesdeHiloError();
                }

                _guiCarga.actualizarGUIdesdeHilo_95_porciento();

                _guiCarga.actualizarGUIdesdeHilo_100_porciento(controlador.v_startTime);
            }
            else
            {
                _guiCarga.actualizarGUIdesdeHiloError();
            }

            _guiCarga.actualizarGUIdesdeHiloResultadoCarga(_textBox.Text, Color.White);

            _guiCarga.actualizarGUIdesdeHiloBoton(true, false, true);

            controlador.v_hiloCorriendo = false;
        }

        internal async Task informacionVisitaIndividualRecargar(string pcodCliente)
        {
            GUIRecargaManual _guiCarga = new GUIRecargaManual(controlador);

            _guiCarga.actualizarGUIdesdeHilo_0_porciento();

            _guiCarga.actualizarGUIdesdeHiloResultadoCarga(string.Empty, Color.White);

            string _resultadoCarga = string.Empty;

            Editor _textBox = new Editor();

            var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

            if (await _testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
            {
                _guiCarga.actualizarGUIdesdeHilo_25_porciento();

                Log _log = new Log();

                var ServicioUpload = DependencyService.Get<IService_WebServiceUpload>();

                string _memoryStream = ServicioUpload.Get_cargaVisitaIndividual(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), pcodCliente);

                if (!_memoryStream.Equals(TypeEvents._errorWS))
                {
                    _log.addLineSuccessWSDownload(_textBox, TablesROL._visitas + SubjectTagEmail._individual);

                    var _memoryCompress = DependencyService.Get<IMC_HH>();

                    DataTable _dt = _memoryCompress.Unzip_HH_DataTable("informacionVisitaIndividualRecargar", TypeTransaction._upload, _memoryStream, TablesROL._visitas);

                    _guiCarga.actualizarGUIdesdeHilo_45_porciento();

                    if (_dt.Rows == null)
                    {
                        _log.addErrorLineDataTableNull(_textBox, TablesROL._visitas);

                        _guiCarga.actualizarGUIdesdeHiloError();
                    }
                    else
                    {
                        if (_dt.Rows.Count != 0)
                        {
                            OperationSQL.deleteSpecificTable(
                                TablesROL._visitas,
                                TableVisitas._CODCLIENTE,
                                pcodCliente,
                                _textBox,
                                _log
                                );

                            _guiCarga.actualizarGUIdesdeHilo_65_porciento();

                            int _inserciones = Numeric._zeroInteger;

                            foreach (DataRow _fila in _dt.Rows)
                            {
                                Carga_ManagerVisita _manager = new Carga_ManagerVisita();

                                StringBuilder _sb = _manager.insertTablaVisita(_fila);

                                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                                _inserciones += MultiGeneric.uploadGenericTable(_sb);

                                _guiCarga.actualizarGUIdesdeHilo_progresoInsercion(_inserciones, _dt.Rows.Count);
                            }

                            _log.registrationNumberInserts(_textBox, _inserciones, _dt.Rows.Count, TablesROL._visitas);

                            _guiCarga.actualizarGUIdesdeHilo_85_porciento();
                        }
                        else
                        {
                            _log.addAlertLine(_textBox, "la consulta a la tabla " + TablesROL._visitas + " no devolvio resultados.");

                            _guiCarga.actualizarGUIdesdeHiloError();
                        }
                    }
                }
                else
                {
                    _guiCarga.actualizarGUIdesdeHiloError();
                }

                _guiCarga.actualizarGUIdesdeHilo_95_porciento();

                _guiCarga.actualizarGUIdesdeHilo_100_porciento(controlador.v_startTime);
            }
            else
            {
                _guiCarga.actualizarGUIdesdeHiloError();
            }

            _guiCarga.actualizarGUIdesdeHiloResultadoCarga(_textBox.Text, Color.White);

            _guiCarga.actualizarGUIdesdeHiloBoton(true, false, true);

            controlador.v_hiloCorriendo = false;
        }
    }
}