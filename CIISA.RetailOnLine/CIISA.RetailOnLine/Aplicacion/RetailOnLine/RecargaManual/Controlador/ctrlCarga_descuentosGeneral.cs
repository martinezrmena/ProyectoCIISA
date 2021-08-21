using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Data;
using System.Text;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Server.WS;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Compress;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Modelo;
using CIISA.RetailOnLine.Framework.Common.Character;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Controlador
{
    public class ctrlCarga_descuentosGeneral
    {
        private ctrlCargaMenu controlador { get; set; }
        public Cliente v_objCliente = null;
        private DataTable v_dtRespaldo = new DataTable();

        internal ctrlCarga_descuentosGeneral(ctrlCargaMenu pcontrolador)
        {
            controlador = pcontrolador;
        }

        private void obtenerRespaldoDescuentoGeneral(Editor ptextBox, Log plog)
        {
            Carga_ManagerDescuentoGeneral _manager = new Carga_ManagerDescuentoGeneral();

            v_dtRespaldo = _manager.obtenerRespaldoDescuentoGeneral();

            if (v_dtRespaldo.Rows != null)
            {
                plog.addSuccessLine(
                    ptextBox,
                    "respaldo " + v_dtRespaldo.Rows.Count + " registros de " + TablesROL._descuentoGeneral + Simbol._point,
                    1);

                v_dtRespaldo.TableName = TablesROL._descuentoGeneral;

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

        public async void informacionDescuentoGeneralRecargar()
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

                string _memoryStream = ServicioUpload.Get_cargaDescuentoGeneralCliente(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));

                if (!_memoryStream.Equals(TypeEvents._errorWS))
                {
                    _log.addLineSuccessWSDownload(_textBox, TablesROL._descuentoGeneral);

                    var _memoryCompress = DependencyService.Get<IMC_HH>();

                    DataTable _dt = _memoryCompress.Unzip_HH_DataTable("informacionDescuentoGeneralRecargar", TypeTransaction._upload, _memoryStream, TablesROL._descuentoGeneral);

                    _guiCarga.actualizarGUIdesdeHilo_45_porciento();

                    if (_dt.Rows == null)
                    {
                        _log.addErrorLineDataTableNull(_textBox, TablesROL._descuentoGeneral);

                        _guiCarga.actualizarGUIdesdeHiloError();
                    }
                    else
                    {
                        if (_dt.Rows.Count != 0)
                        {
                            _guiCarga.actualizarGUIdesdeHilo("AVANCE - 55%" + Environment.NewLine + "Respaldo Tabla", Color.GreenYellow);

                            obtenerRespaldoDescuentoGeneral(_textBox, _log);

                            OperationSQL.deleteTableFeedbackTextBox(
                                TablesROL._descuentoGeneral,
                                _textBox,
                                _log
                                );

                            _guiCarga.actualizarGUIdesdeHilo_65_porciento();

                            int _inserciones = Numeric._zeroInteger;

                            foreach (DataRow _fila in _dt.Rows)
                            {
                                Carga_ManagerDescuentoGeneral _manager = new Carga_ManagerDescuentoGeneral();

                                StringBuilder _sb = _manager.insertTablaDescuentoGeneral(_fila);

                                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                                _inserciones += MultiGeneric.uploadGenericTable(_sb);

                                _guiCarga.actualizarGUIdesdeHilo_progresoInsercion(_inserciones, _dt.Rows.Count);
                            }

                            _log.registrationNumberInserts(_textBox, _inserciones, _dt.Rows.Count, TablesROL._descuentos);

                            _guiCarga.actualizarGUIdesdeHilo_85_porciento();
                        }
                        else
                        {
                            _log.addAlertLine(_textBox, "la consulta a la tabla " + TablesROL._descuentoGeneral + " no devolvio resultados.");

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
