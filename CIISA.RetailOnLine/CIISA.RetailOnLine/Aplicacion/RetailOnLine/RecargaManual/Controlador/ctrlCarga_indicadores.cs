using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.VistaControlador;
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
    public class ctrlCarga_indicadores
    {
        private ctrlCargaMenu controlador { get; set; }
        public Cliente v_objCliente = null;

        internal ctrlCarga_indicadores(ctrlCargaMenu pcontrolador)
        {
            controlador = pcontrolador;
        }

        public async void informacionIndicadoresFacturacionRecargar()
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

                //string _memoryStream = ServicioUpload.Get_cargaIndicadorFactura(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));
                //Validacion 50mts
                string _memoryStream = ServicioUpload.cargaIndicadorFacturaGeo(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));

                if (!_memoryStream.Equals(TypeEvents._errorWS))
                {
                    _log.addLineSuccessWSDownload(_textBox, TablesROL._indicadorFactura);

                    var _memoryCompress = DependencyService.Get<IMC_HH>();

                    DataTable _dt = _memoryCompress.Unzip_HH_DataTable("informacionIndicadoresFacturacionRecargar", TypeTransaction._upload, _memoryStream, TablesROL._indicadorFactura);

                    _guiCarga.actualizarGUIdesdeHilo_45_porciento();

                    if (_dt.Rows == null)
                    {
                        _log.addErrorLineDataTableNull(_textBox, TablesROL._indicadorFactura);

                        _guiCarga.actualizarGUIdesdeHiloError();
                    }
                    else
                    {
                        if (_dt.Rows.Count != 0)
                        {
                            OperationSQL.deleteTableFeedbackTextBox(
                                TablesROL._indicadorFactura,
                                _textBox,
                                _log
                                );

                            _guiCarga.actualizarGUIdesdeHilo_65_porciento();

                            int _inserciones = Numeric._zeroInteger;

                            foreach (DataRow _fila in _dt.Rows)
                            {
                                Carga_ManagerIndicadorFactura _manager = new Carga_ManagerIndicadorFactura();

                                StringBuilder _sb = _manager.insertTablaIndicadorFactura(_fila);
                                var MultiGeneric = DependencyService.Get<IMultiGeneric>();
                                _inserciones += MultiGeneric.uploadGenericTable(_sb);

                                _guiCarga.actualizarGUIdesdeHilo_progresoInsercion(_inserciones, _dt.Rows.Count);
                            }

                            _log.registrationNumberInserts(_textBox, _inserciones, _dt.Rows.Count, TablesROL._indicadorFactura);

                            _guiCarga.actualizarGUIdesdeHilo_85_porciento();
                        }
                        else
                        {
                            _log.addAlertLine(_textBox, "la consulta a la tabla " + TablesROL._indicadorFactura + " no devolvio resultados.");

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

        internal async Task informacionIndicadoresIndividualRecargar(string pcodCliente)
        {
            GUIRecargaManual _guiCarga = new GUIRecargaManual(controlador);

            _guiCarga.actualizarGUIdesdeHilo_0_porciento();

            _guiCarga.actualizarGUIdesdeHiloResultadoCarga(string.Empty, Color.White);

            string _resultadoCarga = string.Empty;

            Editor _textBox = new Editor();

            var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

            if (await _testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
            {
                _guiCarga.actualizarGUIdesdeHilo("AVANCE - 25%"
                    + Environment.NewLine
                    + "Solicitando Información", Color.GreenYellow);

                Log _log = new Log();

                var ServicioUpload = DependencyService.Get<IService_WebServiceUpload>();

                //string _memoryStream = ServicioUpload.Get_cargaIndicadorFacturaIndividual(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), pcodCliente);
                //Validacion 50mts
                string _memoryStream = ServicioUpload.cargaIndicadorFacturaIndividualGeo(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), pcodCliente);

                if (!_memoryStream.Equals(TypeEvents._errorWS))
                {
                    _log.addLineSuccessWSDownload(_textBox, TablesROL._indicadorFactura + SubjectTagEmail._individual);

                    var _memoryCompress = DependencyService.Get<IMC_HH>();

                    DataTable _dt = _memoryCompress.Unzip_HH_DataTable("informacionIndicadoresIndividualRecargar", TypeTransaction._upload, _memoryStream, TablesROL._indicadorFactura);

                    _guiCarga.actualizarGUIdesdeHilo_45_porciento();

                    if (_dt.Rows == null)
                    {
                        _log.addErrorLineDataTableNull(_textBox, TablesROL._indicadorFactura + SubjectTagEmail._individual + SubjectTagEmail._individual);

                        _guiCarga.actualizarGUIdesdeHiloError();
                    }
                    else
                    {
                        if (_dt.Rows.Count != 0)
                        {
                            OperationSQL.deleteSpecificTable(
                                TablesROL._indicadorFactura,
                                TableIndicadorFactura._NO_CLIENTE,
                                pcodCliente,
                                _textBox,
                                _log
                                );

                            _guiCarga.actualizarGUIdesdeHilo_65_porciento();

                            int _inserciones = Numeric._zeroInteger;

                            foreach (DataRow _fila in _dt.Rows)
                            {
                                Carga_ManagerIndicadorFactura _manager = new Carga_ManagerIndicadorFactura();

                                StringBuilder _sb = _manager.insertTablaIndicadorFactura(_fila);

                                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                                _inserciones += MultiGeneric.uploadGenericTable(_sb);

                                _guiCarga.actualizarGUIdesdeHilo_progresoInsercion(_inserciones, _dt.Rows.Count);
                            }

                            _log.registrationNumberInserts(_textBox, _inserciones, _dt.Rows.Count, TablesROL._indicadorFactura);

                            _guiCarga.actualizarGUIdesdeHilo_85_porciento();
                        }
                        else
                        {
                            _log.addAlertLine(_textBox, "la consulta a la tabla " + TablesROL._indicadorFactura + " no devolvio resultados.");

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

        //Pedir el nombre
        internal async Task informacionIndicadoresIndividualRecargarParte1()
        {
            ShowPresentacion _show = new ShowPresentacion();

            await _show.mostrarPantallaCliente(this,PantallasSistema._pantallaPromedioVentas);
        }

        //llamar accion en ctrlCargaMenu
        public void informacionIndicadoresIndividualRecargarParte2()
        {
            controlador.informacionIndicadoresIndividualRecargarParte3(this);
        }

        //Hilo
        public async void informacionIndicadoresIndividualRecargarParte4()
        {
            GUIRecargaManual _guiCarga = new GUIRecargaManual(controlador);

            _guiCarga.actualizarGUIdesdeHilo_0_porciento();

            _guiCarga.actualizarGUIdesdeHiloResultadoCarga(string.Empty, Color.White);

            string _resultadoCarga = string.Empty;

            Editor _textBox = new Editor();

            var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

            if (await _testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
            {
                _guiCarga.actualizarGUIdesdeHilo("AVANCE - 25%"
                    + Environment.NewLine
                    + "Solicitando Información", Color.GreenYellow);

                Cliente _objCliente = v_objCliente;

                //ShowPresentacion _show = new ShowPresentacion();

                //_objCliente = _show.mostrarPantallaCliente(PantallasSistema._pantallaPromedioVentas, false);

                Log _log = new Log();

                var ServicioUpload = DependencyService.Get<IService_WebServiceUpload>();

                //string _memoryStream = ServicioUpload.Get_cargaIndicadorFacturaIndividual(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), _objCliente.v_no_cliente);
                //Validacion 50mts
                string _memoryStream = ServicioUpload.cargaIndicadorFacturaIndividualGeo(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), _objCliente.v_no_cliente);

                if (!_memoryStream.Equals(TypeEvents._errorWS))
                {
                    _log.addLineSuccessWSDownload(_textBox, TablesROL._indicadorFactura + SubjectTagEmail._individual);

                    var _memoryCompress = DependencyService.Get<IMC_HH>();

                    DataTable _dt = _memoryCompress.Unzip_HH_DataTable("informacionIndicadoresIndividualRecargar", TypeTransaction._upload, _memoryStream, TablesROL._indicadorFactura);

                    _guiCarga.actualizarGUIdesdeHilo_45_porciento();

                    if (_dt.Rows == null)
                    {
                        _log.addErrorLineDataTableNull(_textBox, TablesROL._indicadorFactura + SubjectTagEmail._individual);

                        _guiCarga.actualizarGUIdesdeHiloError();
                    }
                    else
                    {
                        if (_dt.Rows.Count != 0)
                        {
                            OperationSQL.deleteSpecificTable(
                                TablesROL._indicadorFactura,
                                TableIndicadorFactura._NO_CLIENTE,
                                _objCliente.v_no_cliente,
                                _textBox,
                                _log
                                );

                            _guiCarga.actualizarGUIdesdeHilo_65_porciento();

                            int _inserciones = Numeric._zeroInteger;

                            foreach (DataRow _fila in _dt.Rows)
                            {
                                Carga_ManagerIndicadorFactura _manager = new Carga_ManagerIndicadorFactura();

                                StringBuilder _sb = _manager.insertTablaIndicadorFactura(_fila);

                                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                                _inserciones += MultiGeneric.uploadGenericTable(_sb);

                                _guiCarga.actualizarGUIdesdeHilo_progresoInsercion(_inserciones, _dt.Rows.Count);

                            }

                            _log.registrationNumberInserts(_textBox, _inserciones, _dt.Rows.Count, TablesROL._indicadorFactura);

                            _guiCarga.actualizarGUIdesdeHilo_85_porciento();
                        }
                        else
                        {
                            _log.addAlertLine(_textBox, "la consulta a la tabla " + TablesROL._indicadorFactura + " no devolvio resultados.");

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
