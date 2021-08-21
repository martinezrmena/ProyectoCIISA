using Acr.UserDialogs;
using CIISA.RetailOnLine.Aplicacion.Comunication.IWSSRol;
using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.CargaDiaria.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Respaldo.Modelo;
using CIISA.RetailOnLine.Framework.Common.Compress;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Common.ValidateHH;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Display.IMainLockScreen;
using CIISA.RetailOnLine.Framework.Server.WS;
using System;
using System.Data;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.IdentificarUsuario
{
    public class LogicaIdentificarUsuario_Carga
    {
        private ctrlIdentificarUsuario controlador = null;

        public LogicaIdentificarUsuario_Carga(ctrlIdentificarUsuario pcontrolador)
        {
            controlador = pcontrolador;
        }

        public async void cargaInicialDiaria()
        {
            GenerarRutaServidor();
            UserDialogs.Instance.HideLoading();

            DataTable _dt = await ObtenerDatosServidorTablaSistema();

            if (await DataTableValidate.validateDataTable(_dt, "La máquina no ha sido asignada en NAF"))
            {
                foreach (DataRow _fila in _dt.Rows)
                {
                    string _indCierre = _fila[TableSistema._IND_CIERRE].ToString();

                    if (_indCierre.Equals(SQL._pending))
                    {
                        //string _fecha = VarTime.getSQLCEDate();
                        string _fecha = VarTime.getDataTableDate();

                        Logica_ManagerSistema _manager = new Logica_ManagerSistema();

                        if (_fecha.Equals(_fila[TableSistema._FECHA].ToString()))
                        {
                            _manager.actualizarEstado(false);
                        }
                        else
                        {
                            LogMessageAttention _logMessageAttention = new LogMessageAttention();
                            await _logMessageAttention.generalAttention("Verifique la fecha/hora del equipo");
                        }

                        if (_manager.buscarEstado().Equals(SQL._pending))
                        {
                            String Message = "El proceso termino de manera exitosa.";
                            Device.BeginInvokeOnMainThread(async ()=>
                                await abrirMaquina(Message)
                            );
                            
                        }
                        else
                        {
                            LogMessageAttention _logMessageAttention = new LogMessageAttention();
                            await _logMessageAttention.generalAttention(
                                "La ruta se encuentra cerrada en los sistemas del Arreo [1]."
                                + Environment.NewLine
                                + Environment.NewLine
                                + "* Consulte al Departamento de Liquidaciones");
                        }
                    }
                    else
                    {
                        LogMessageAttention _logMessageAttention = new LogMessageAttention();
                        await _logMessageAttention.generalAttention(
                            "La ruta se encuentra cerrada en los sistemas del Arreo [2]."
                            + Environment.NewLine
                            + Environment.NewLine
                            + "* Consulte al Departamento de Liquidaciones");
                    }
                }
            }
        }

        private void GenerarRutaServidor()
        {
            var servicio = DependencyService.Get<IService_WebServiceRute>();

            string _memoryStream = servicio.Get_generarRutaEInventario(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));

            if (_memoryStream.Equals(TypeEvents._errorWS))
            {
                Exception ex = new Exception();

                throw new Exception("Error solicitando acciones al servidor");
            }
        }

        private async Task<DataTable> ObtenerDatosServidorTablaSistema()
        {
            DataTable _dt = null;

            var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

            if (await _testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
            {
                var servicio = DependencyService.Get<IService_WebServiceUpload>();
                string _memoryStream = servicio.Get_cargaSistema(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));

                if (!_memoryStream.Equals(TypeEvents._errorWS))
                {
                    var _memoryCompress = DependencyService.Get<IMC_HH>();

                    _dt = _memoryCompress.Unzip_HH_DataTable("ObtenerDatosServidorTablaSistema", TypeTransaction._upload, _memoryStream, TablesROL._sistema);
                }
            }

            return _dt;
        }

        private async Task abrirMaquina(String Message)
        {
            DateTime StartTime = DateTime.Now;

            UserDialogs.Instance.HideLoading();
            GUIAvanceManual ControlAdvance = new GUIAvanceManual(controlador);

            LogMessageAttention _logMessageAttention = new LogMessageAttention();
            await _logMessageAttention.generalAttention("Se abrió la máquina");

            LogicaIdentificarUsuario_IURender _liu_iur = new LogicaIdentificarUsuario_IURender(controlador);

            await Task.Run(() =>

                Device.BeginInvokeOnMainThread(() =>
                {
                    RenderAvancePanel(true);
                    _liu_iur.renderPaneles(controlador.view.FindByName<StackLayout>("pnlRecargaDiaria"));
                }
                )
            ).ConfigureAwait(true);

            controlador.view.ToolbarItems.Clear();
            _liu_iur.RenderMenuBottomIdentificacion(false);
            _liu_iur.RenderMenuBottomRecargaDiaria(false);

            Log _log = new Log();

            await ControlAdvance.actualizarGUIdesdeHilo_0_porciento();

            Respaldo_ManagerRespaldar _managerRespaldar = new Respaldo_ManagerRespaldar();

            await ControlAdvance.actualizarGUIdesdeHilo_25_porciento();

            _managerRespaldar.respaldarInformacion(controlador.view.FindByName<Editor>("pnlRecargaDiaria_txtBitacora"), _log);

            Carga_ManagerRecargaDiaria _managerRecargaDiaria = new Carga_ManagerRecargaDiaria();

            await ControlAdvance.actualizarGUIdesdeHilo_65_porciento();

            await _managerRecargaDiaria.recargaDiaria(
                controlador,
                controlador.view.FindByName<CheckBox>("pnlIdentificacion_chkAperturaNocturna").Checked,
                controlador.view.FindByName<Editor>("pnlRecargaDiaria_txtBitacora"),
                controlador.view.FindByName<Label>("pnlRecargaDiaria_lblInsertando"),
                _log,
                Message
                );

            await ControlAdvance.actualizarGUIdesdeHilo_95_porciento();

            controlador.view.FindByName<Label>("pnlRecargaDiaria_lblInsertando").Text = "Finalizó.";

            var _establishDatabase = DependencyService.Get<IEstablishDatabase>();

            _establishDatabase.thereDataBase();

            await ControlAdvance.actualizarGUIdesdeHilo_100_porciento(StartTime);

            _liu_iur.renderMenu();
        }

        public async Task cargaAperturaNocturna()
        {
            String Message = String.Empty;

            Message = "Se finalizó la apertura nocturna."
                    + Environment.NewLine
                    + Environment.NewLine
                    + " * Se borró el inventario y la máquina esta abierta";

            Logica_ManagerSistema _managerSistema = new Logica_ManagerSistema();

            _managerSistema.actualizarEstado(false);

            EliminarInventarioServidor();

            await abrirMaquina(Message);

        }

        private void EliminarInventarioServidor()
        {
            var servicio = DependencyService.Get<IService_WebServiceRute>();

            string _memoryStream = servicio.Get_eliminarInventario(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));
            if (_memoryStream.Equals(TypeEvents._errorWS))
            {
                throw new Exception("Error solicitando acciones al servidor");
            }
        }

        internal void RenderAvancePanel(bool visible) {
            if (visible)
            {
                controlador.view.FindByName<StackLayout>("pnlAvance").IsVisible = true;
            }
            else {
                controlador.view.FindByName<StackLayout>("pnlAvance").IsVisible = false;
            }
            
        }
    }
}
