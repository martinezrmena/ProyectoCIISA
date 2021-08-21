using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
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
    public class ctrlCarga_inventario
    {
        private ctrlCargaMenu controlador { get; set; }

        internal ctrlCarga_inventario(ctrlCargaMenu pcontrolador)
        {
            controlador = pcontrolador;
        }

        public async void informacionInventarioRecargar()
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

                string _memoryStream = ServicioUpload.Get_cargaInventario_recarga_manualAgente(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));

                if (!_memoryStream.Equals(TypeEvents._errorWS))
                {
                    _log.addLineSuccessWSDownload(_textBox, TablesROL._inventario);

                    var _memoryCompress = DependencyService.Get<IMC_HH>();

                    DataTable _dt = _memoryCompress.Unzip_HH_DataTable("informacionInventarioRecargar", TypeTransaction._upload, _memoryStream, TablesROL._inventario);

                    _guiCarga.actualizarGUIdesdeHilo_45_porciento();

                    if (_dt.Rows == null)
                    {
                        _log.addErrorLineDataTableNull(_textBox, TablesROL._inventario);

                        _guiCarga.actualizarGUIdesdeHiloError();
                    }
                    else
                    {
                        if (_dt.Rows.Count != 0)
                        {
                            _guiCarga.actualizarGUIdesdeHilo_65_porciento();

                            int _inserciones = Numeric._zeroInteger;

                            foreach (DataRow _fila in _dt.Rows)
                            {
                                StringBuilder _sb = new StringBuilder();

                                Carga_ManagerInventario _managerCargaInventario = new Carga_ManagerInventario();

                                Logica_ManagerInventario _managerLogicaInventario = new Logica_ManagerInventario();

                                bool _existeProducto = _managerLogicaInventario.ExisteProducto(_fila["NO_ARTI"].ToString());

                                if (_existeProducto)
                                {
                                    _sb = _managerCargaInventario.updateTablaInventario(_fila);

                                    var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                                    _inserciones += MultiGeneric.uploadGenericTable(_sb);

                                    _guiCarga.actualizarGUIdesdeHilo_progresoInsercion(_inserciones, _dt.Rows.Count);
                                }
                                else
                                {
                                    _sb = _managerCargaInventario.insertTablaInventario(_fila);

                                    var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                                    _inserciones += MultiGeneric.uploadGenericTable(_sb);

                                    _guiCarga.actualizarGUIdesdeHilo_progresoInsercion(_inserciones, _dt.Rows.Count);
                                }

                                _managerLogicaInventario.calcularCantidadDisponible(_fila["NO_ARTI"].ToString());

                                _log.setDetailSentence(_sb.ToString());
                            }

                            _log.registrationNumberInserts(_textBox, _inserciones, _dt.Rows.Count, TablesROL._inventario);

                            _guiCarga.actualizarGUIdesdeHilo_85_porciento();
                        }
                        else
                        {
                            _log.addAlertLine(_textBox, "la consulta a la tabla " + TablesROL._inventario + " no devolvio resultados.");

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

            if (controlador.v_cargaSistema)
            {
                ctrlCarga_sistema _controladorSistema = new ctrlCarga_sistema(controlador);
                await _controladorSistema.informacionSistemaRecargar();
            }

            if (controlador.v_cargaPedido)
            {
                ctrlCarga_encabezadoPedido _controladorEP = new ctrlCarga_encabezadoPedido(controlador);
                _controladorEP.informacionEncabezadoPedidoRecargar();

                ctrlCarga_detallePedido _controladorDP = new ctrlCarga_detallePedido(controlador);
                await _controladorDP.informacionDetallePedidoRecargar();
            }

            _guiCarga.actualizarGUIdesdeHiloBoton(true, false, true);

            controlador.v_hiloCorriendo = false;

            controlador.v_cargaSistema = false;
            controlador.v_cargaPedido = false;
        }
    }
}
