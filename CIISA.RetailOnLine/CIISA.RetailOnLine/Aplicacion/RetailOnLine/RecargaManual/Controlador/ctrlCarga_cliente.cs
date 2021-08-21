using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.VistaControlador;
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
using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Controlador
{
    public class ctrlCarga_cliente
    {
        public ctrlCargaMenu controlador { get; set; }
        public string pcodCliente = string.Empty;

        internal ctrlCarga_cliente(ctrlCargaMenu pcontrolador)
        {
            controlador = pcontrolador;
        }

        public async void informacionClienteRecargar()
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

                //string _memoryStream = ServicioUpload.Get_cargaCliente(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));
                //Validacion 50mts
                string _memoryStream = ServicioUpload.cargaClienteGeo(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));

                if (!_memoryStream.Equals(TypeEvents._errorWS))
                {
                    _log.addLineSuccessWSDownload(_textBox, TablesROL._cliente);

                    var _memoryCompress = DependencyService.Get<IMC_HH>();

                    DataTable _dt = _memoryCompress.Unzip_HH_DataTable("informacionClienteRecargar", TypeTransaction._upload, _memoryStream, TablesROL._cliente);

                    _guiCarga.actualizarGUIdesdeHilo_45_porciento();

                    if (_dt.Rows == null)
                    {
                        _log.addErrorLineDataTableNull(_textBox, TablesROL._cliente);

                        _guiCarga.actualizarGUIdesdeHiloError();
                    }
                    else
                    {
                        if (_dt.Rows.Count != 0)
                        {
                            OperationSQL.deleteTableFeedbackTextBox(
                                TablesROL._cliente,
                                _textBox,
                                _log
                                );

                            _guiCarga.actualizarGUIdesdeHilo_65_porciento();

                            int _inserciones = Numeric._zeroInteger;

                            foreach (DataRow _fila in _dt.Rows)
                            {
                                Carga_ManagerCliente _manager = new Carga_ManagerCliente();

                                StringBuilder _sb = _manager.insertTablaCliente(_fila);

                                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                                _inserciones += MultiGeneric.uploadGenericTable(_sb);

                                _guiCarga.actualizarGUIdesdeHilo_progresoInsercion(_inserciones, _dt.Rows.Count);
                            }

                            _log.registrationNumberInserts(_textBox, _inserciones, _dt.Rows.Count, TablesROL._cliente);

                            _guiCarga.actualizarGUIdesdeHilo_85_porciento();
                        }
                        else
                        {
                            _log.addAlertLine(_textBox, "la consulta a la tabla " + TablesROL._cliente + " no devolvio resultados.");

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

            ctrlCarga_establecimiento _controladorEstablecimiento = new ctrlCarga_establecimiento(controlador);

            _controladorEstablecimiento.informacionEstablecimientoRecargar();

            ctrlCarga_visita _controladorVisita = new ctrlCarga_visita(controlador);

            _controladorVisita.informacionVisitaRecargar();

            ctrlCarga_indicadores _controladorIndicadores = new ctrlCarga_indicadores(controlador);

            _controladorIndicadores.informacionIndicadoresFacturacionRecargar();

            _guiCarga.actualizarGUIdesdeHiloBoton(true, false, true);

            controlador.v_hiloCorriendo = false;
        }

        //Pedir el nombre
        internal void informacionClienteIndividualRecargarParte1()
        {
            ShowCarga _show = new ShowCarga();
            _show.mostrarPantallaClienteInactivo(this);            
        }

        //llamar accion en ctrlCargaMenu
        public void informacionClienteIndividualRecargarParte2()
        {
            controlador.informacionClienteIndividualRecargarParte3(this);
        }

        //Hilo
        public async void informacionClienteIndividualRecargarParte4()
        {
            GUIRecargaManual _guiCarga = new GUIRecargaManual(controlador);

            _guiCarga.actualizarGUIdesdeHilo_0_porciento();

            _guiCarga.actualizarGUIdesdeHiloResultadoCarga(string.Empty, Color.White);

            Editor _textBox = new Editor();

            string _codCliente = string.Empty;

            var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

            if (await _testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
            {
                _guiCarga.actualizarGUIdesdeHilo("AVANCE - 25%"
                    + Environment.NewLine
                    + "Solicitando Información", Color.FromRgb(10, 108, 3));

                _codCliente = pcodCliente;

                if (!_codCliente.Equals(string.Empty))
                {
                    Log _log = new Log();

                    var ServicioUpload = DependencyService.Get<IService_WebServiceUpload>();

                    //string _memoryStream = ServicioUpload.Get_cargaClienteIndividual(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), _codCliente);
                    //Validacion 50mts
                    string _memoryStream = ServicioUpload.cargaClienteIndividualGeo(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), _codCliente);

                    if (!_memoryStream.Equals(TypeEvents._errorWS))
                    {
                        _log.addLineSuccessWSDownload(_textBox, TablesROL._cliente + SubjectTagEmail._individual);

                        var _memoryCompress = DependencyService.Get<IMC_HH>();

                        DataTable _dt = _memoryCompress.Unzip_HH_DataTable("informacionClienteIndividualRecargar", TypeTransaction._upload, _memoryStream, TablesROL._cliente);

                        _guiCarga.actualizarGUIdesdeHilo_45_porciento();

                        if (_dt.Rows == null)
                        {
                            _log.addErrorLineDataTableNull(_textBox, TablesROL._cliente + SubjectTagEmail._individual);

                            _guiCarga.actualizarGUIdesdeHiloError();
                        }
                        else
                        {
                            if (_dt.Rows.Count != 0)
                            {
                                OperationSQL.deleteSpecificTable(
                                    TablesROL._cliente,
                                    TableCliente._NO_CLIENTE,
                                    _codCliente,
                                    _textBox,
                                    _log
                                    );

                                _guiCarga.actualizarGUIdesdeHilo_65_porciento();

                                int _inserciones = Numeric._zeroInteger;

                                foreach (DataRow _fila in _dt.Rows)
                                {
                                    Carga_ManagerCliente _manager = new Carga_ManagerCliente();

                                    StringBuilder _sb = _manager.insertTablaCliente(_fila);

                                    var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                                    _inserciones += MultiGeneric.uploadGenericTable(_sb);

                                    _guiCarga.actualizarGUIdesdeHilo_progresoInsercion(_inserciones, _dt.Rows.Count);
                                }

                                _log.registrationNumberInserts(_textBox, _inserciones, _dt.Rows.Count, TablesROL._cliente);

                                _guiCarga.actualizarGUIdesdeHilo_85_porciento();
                            }
                            else
                            {
                                _log.addAlertLine(_textBox, "la consulta a la tabla " + TablesROL._cliente + " no devolvio resultados.");

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

            }
            else
            {
                _guiCarga.actualizarGUIdesdeHiloError();
            }

            _guiCarga.actualizarGUIdesdeHiloResultadoCarga(_textBox.Text, Color.White);

            if (!_codCliente.Equals(string.Empty))
            {
                ctrlCarga_establecimiento _controladorEstablecimiento = new ctrlCarga_establecimiento(controlador);

                await _controladorEstablecimiento.informacionEstablecimientoIndividualRecargar(_codCliente);

                ctrlCarga_visita _controladorVisita = new ctrlCarga_visita(controlador);

                await _controladorVisita.informacionVisitaIndividualRecargar(_codCliente);

                ctrlCarga_indicadores _controladorIndicador = new ctrlCarga_indicadores(controlador);

                await _controladorIndicador.informacionIndicadoresIndividualRecargar(_codCliente);
            }

            _guiCarga.actualizarGUIdesdeHiloBoton(true, false, true);

            controlador.v_hiloCorriendo = false;
        }        
    }
}
