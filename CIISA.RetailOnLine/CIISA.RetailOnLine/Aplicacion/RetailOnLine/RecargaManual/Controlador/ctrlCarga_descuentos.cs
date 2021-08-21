using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.VistaControlador;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.Compress;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using CIISA.RetailOnLine.Framework.Server.WS;
using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Controlador
{
    public class ctrlCarga_descuentos
    {
        private ctrlCargaMenu controlador { get; set; }
        public Cliente v_objCliente = null;
        private DataTable v_dtRespaldo = new DataTable();

        internal ctrlCarga_descuentos(ctrlCargaMenu pcontrolador)
        {
            controlador = pcontrolador;
        }

        private void obtenerRespaldoDescuento(Editor ptextBox, Log plog)
        {
            Carga_ManagerDescuento _manager = new Carga_ManagerDescuento();

            v_dtRespaldo = _manager.obtenerRespaldoDescuento();

            if (v_dtRespaldo.Rows != null)
            {
                plog.addSuccessLine(
                    ptextBox,
                    "respaldo " + v_dtRespaldo.Rows.Count + " registros de " + TablesROL._descuentos + Simbol._point,
                    1);

                if (v_dtRespaldo.Rows.Count != 0)
                {
                    plog.setDetailDataTableFilled(v_dtRespaldo.TableName);
                }
                else
                {
                    plog.setDetailDataTableEmpty(v_dtRespaldo.TableName);
                }
            }
            else
            {
                plog.setDetailDataTableNull();
            }
        }

        public async void informacionDescuentoRecargar()
        {
            GUIRecargaManual _guiCarga = new GUIRecargaManual(controlador);

            _guiCarga.actualizarGUIdesdeHilo_0_porciento();

            _guiCarga.actualizarGUIdesdeHiloResultadoCarga(string.Empty, Color.White);

            Editor _textBox = new Editor();

            var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

            if (await _testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
            {
                _guiCarga.actualizarGUIdesdeHilo_25_porciento();

                Log _log = new Log();

                var ServicioUpload = DependencyService.Get<IService_WebServiceUpload>();

                string _memoryStream = ServicioUpload.Get_cargaDescuento(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));               

                if (!_memoryStream.Equals(TypeEvents._errorWS))
                {
                    _log.addLineSuccessWSDownload(_textBox, TablesROL._descuentos);

                    var _memoryCompress = DependencyService.Get<IMC_HH>();

                    DataTable _dt = _memoryCompress.Unzip_HH_DataTable("informacionDescuentoRecargar", TypeTransaction._upload, _memoryStream, TablesROL._descuentos);

                    _guiCarga.actualizarGUIdesdeHilo_45_porciento();

                    if (_dt.Rows == null)
                    {
                        _log.addErrorLineDataTableNull(_textBox, TablesROL._descuentos);

                        _guiCarga.actualizarGUIdesdeHiloError();
                    }
                    else
                    {
                        if (_dt.Rows.Count != 0)
                        {
                            _guiCarga.actualizarGUIdesdeHilo("AVANCE - 55%" + Environment.NewLine + "Respaldo Tabla", Color.GreenYellow);

                            obtenerRespaldoDescuento(_textBox, _log);

                            OperationSQL.deleteTableFeedbackTextBox(
                                TablesROL._descuentos,
                                _textBox,
                                _log
                                );

                            _guiCarga.actualizarGUIdesdeHilo_65_porciento();

                            int _inserciones = Numeric._zeroInteger;

                            foreach (DataRow _fila in _dt.Rows)
                            {
                                Carga_ManagerDescuento _manager = new Carga_ManagerDescuento();

                                StringBuilder _sb = _manager.insertTablaDescuento(_fila);

                                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                                _inserciones += MultiGeneric.uploadGenericTable(_sb);

                                _guiCarga.actualizarGUIdesdeHilo_progresoInsercion(_inserciones, _dt.Rows.Count);
                            }

                            _log.registrationNumberInserts(_textBox, _inserciones, _dt.Rows.Count, TablesROL._descuentos);

                            _guiCarga.actualizarGUIdesdeHilo_85_porciento();
                        }
                        else
                        {
                            _log.addAlertLine(_textBox, "la consulta a la tabla " + TablesROL._descuentos + " no devolvio resultados.");

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
        internal async Task informacionDescuentoIndividualRecargarParte1()
        {
            ShowPresentacion _show = new ShowPresentacion();

            await _show.mostrarPantallaCliente(this, PantallasSistema._pantallaPromedioVentas);
        }

        //llamar accion en ctrlCargaMenu
        public void informacionDescuentoIndividualRecargarParte2()
        {
            controlador.informacionDescuentoIndividualRecargarParte3(this);
        }

        //Hilo
        public async void informacionDescuentoIndividualRecargarParte4()
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

                Cliente _objCliente = v_objCliente;

                Log _log = new Log();

                var ServicioUpload = DependencyService.Get<IService_WebServiceUpload>();

                string _memoryStream = ServicioUpload.Get_cargaDescuentoPorCodigoCliente(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), _objCliente.v_no_cliente);

                if (!_memoryStream.Equals(TypeEvents._errorWS))
                {
                    _log.addLineSuccessWSDownload(_textBox, TablesROL._descuentos + SubjectTagEmail._individual);

                    var _memoryCompress = DependencyService.Get<IMC_HH>();

                    DataTable _dt = _memoryCompress.Unzip_HH_DataTable("informacionDescuentoIndividualRecargar", TypeTransaction._upload, _memoryStream, TablesROL._descuentos);

                    _guiCarga.actualizarGUIdesdeHilo_45_porciento();

                    if (_dt.Rows == null)
                    {
                        _log.addErrorLineDataTableNull(_textBox, TablesROL._descuentos + SubjectTagEmail._individual);

                        _guiCarga.actualizarGUIdesdeHiloError();
                    }
                    else
                    {
                        if (_dt.Rows.Count != 0)
                        {
                            OperationSQL.deleteSpecificTable(
                                TablesROL._descuentos,
                                TableDescuentos._CODCLIENTE,
                                _objCliente.v_no_cliente,
                                _textBox,
                                _log
                                );

                            _guiCarga.actualizarGUIdesdeHilo_65_porciento();

                            int _inserciones = Numeric._zeroInteger;

                            foreach (DataRow _fila in _dt.Rows)
                            {
                                string _fechaInicia = FormatUtil.convertDateOracleToSQLCompact_yyMMdd(
                                    _fila["FECHA_INICIA"].ToString()
                                    );

                                string _fechaVence = FormatUtil.convertDateOracleToSQLCompact_yyMMdd(
                                    _fila["FECHA_VENCE"].ToString()
                                    );

                                if (_fechaInicia.Equals(string.Empty))
                                {
                                    _fechaInicia = VarTime.getSQLCEDate();
                                }

                                if (_fechaVence.Equals(string.Empty))
                                {
                                    _fechaVence = VarTime.getSQLCEDate();
                                }

                                Carga_ManagerDescuento _manager = new Carga_ManagerDescuento();

                                StringBuilder _sb = _manager.insertTablaDescuento(_fila);

                                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                                _inserciones += MultiGeneric.uploadGenericTable(_sb);

                                _guiCarga.actualizarGUIdesdeHilo_progresoInsercion(_inserciones, _dt.Rows.Count);
                            }

                            _log.registrationNumberInserts(_textBox, _inserciones, _dt.Rows.Count, TablesROL._descuentos);

                            _guiCarga.actualizarGUIdesdeHilo_85_porciento();
                        }
                        else
                        {
                            _log.addAlertLine(_textBox, "la consulta a la tabla " + TablesROL._descuentos + " no devolvio resultados.");

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

        internal void informacionDescuentoRespaldo(Editor ptextBox, Log plog)
        {
            int _inserciones = Numeric._zeroInteger;

            foreach (DataRow _fila in v_dtRespaldo.Rows)
            {
                Carga_ManagerDescuento _manager = new Carga_ManagerDescuento();

                StringBuilder _sb = _manager.insertTablaDescuentoRespaldo(_fila);

                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                _inserciones += MultiGeneric.uploadGenericTable(_sb);

                plog.setDetailSentence(_sb.ToString());
            }

            plog.registrationNumberInserts(ptextBox, _inserciones, v_dtRespaldo.Rows.Count, TablesROL._descuentos);
        }
    }
}
