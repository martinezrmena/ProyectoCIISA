using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Helpers;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Handheld.Display.IMainLockScreen;
using CIISA.RetailOnLine.Framework.Handheld.Display.ViewController;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Util;
using System;
using System.Data;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.IdentificarUsuario
{
    public class LogicaIdentificarUsuario_Verificar
    {
        public ctrlIdentificarUsuario controlador = null;

        internal LogicaIdentificarUsuario_Verificar(ctrlIdentificarUsuario pcontrolador)
        {
            controlador = pcontrolador;
        }

        internal async Task verificarCodigoSeguridad()
        {
            Logica_ManagerClave _managerClave = new Logica_ManagerClave();

            if (controlador.view.FindByName<ExtendedEntry>("pnlIdentificacion_txtRecarga").Text.Equals(_managerClave.obtenerCodigoPrincipal()))
            {
                LogicaIdentificarUsuario_IdentificarUsuario _liu_iu = new LogicaIdentificarUsuario_IdentificarUsuario(controlador);

                await _liu_iu.identificarUsuario();
            }
            else
            {
                await LogMessageError.incorrectCode();
            }
        }

        internal async Task verificarUsuario(DataTable pdt)
        {
            if (pdt.Rows != null)
            {
                if (pdt.Rows.Count == 0)
                {
                    await LogMessageError.generalError("Usuario incorrecto");
                }
                else
                {
                    string _codAgente = controlador.view.FindByName<ExtendedEntry>("pnlIdentificacion_txtIdentificacion").Text;

                    string _contrasenna = controlador.view.FindByName<ExtendedEntry>("pnlIdentificacion_txtContrasenna").Text;

                    bool _recargaTablas = controlador.view.FindByName<CheckBox>("pnlIdentificacion_chkRecarga").Checked;

                    foreach (DataRow _fila in pdt.Rows)
                    {
                        if (_codAgente.Equals(_fila[TableAgenteVendedor._NO_AGENTE].ToString())
                            && _contrasenna.Equals(_fila[TableAgenteVendedor._CONTRASENNA].ToString()))
                        {
                            LogicaIdentificarUsuario _liu = new LogicaIdentificarUsuario(controlador);
                            _liu.establecerVariableDeEntorno_Agente(pdt);

                            MainDateTime _mainDateTime = new MainDateTime();

                            bool _fechaCorrecta = await _mainDateTime.mainDateTimeHandheld(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL));

                            if (_fechaCorrecta)
                            {
                                LogicaIdentificarUsuario_Verificar _liv = new LogicaIdentificarUsuario_Verificar(controlador);
                                await _liv.verificarTipoCarga();
                            }
                            else
                            {
                                LanzadorMenu._mostrarMenuPrincipal = false;

                                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                                await _logMessageAttention.generalAttention(
                                    "Verifique la hora y fecha de la máquina"
                                    + Environment.NewLine
                                    + Environment.NewLine
                                    + VarTime.getDateCR()
                                    + Simbol._hyphenWithSpaces
                                    + VarTime.getCompleteDateTime()
                                    );
                            }
                        }
                        else
                        {
                            await LogMessageError.generalError("Usuario y/o contraseña inválida(s)");
                        }
                    }
                }
            }
        }

        internal async Task verificarTipoCarga()
        {
            if (Variable._thereDataBase)
            {
                if (controlador.view.FindByName<CheckBox>("pnlIdentificacion_chkRecarga").Checked)
                {
                    ShowCarga _show = new ShowCarga();
                    _show.mostrarPantallaCarga(VariableCarga._seleccionTablas);
                }
                else if (controlador.view.FindByName<CheckBox>("pnlIdentificacion_chkAperturaNocturna").Checked)
                {
                    var BatteryState = DependencyService.Get<IAndroidMethods>();

                    if (!await BatteryState.ValueDisableSaveBattery())
                    {
                        LogicaIdentificarUsuario_Carga _liu_c = new LogicaIdentificarUsuario_Carga(controlador);
                        await _liu_c.cargaAperturaNocturna();

                        LanzadorMenu._aperturaNocturna = true;
                    }
                    
                }
                else
                {
                    LogicaIdentificarUsuario_EstadoMaquina _liu_em = new LogicaIdentificarUsuario_EstadoMaquina(controlador);
                    await _liu_em.ProcesarCargarEstadoMaquina();

                    Logica_ManagerSistema _managerSistema = new Logica_ManagerSistema();

                    string _estadoMaquina = _managerSistema.buscarEstado();

                    if (_estadoMaquina.Equals(SQL._close))
                    {
                        ShowDisplay _showDisplay = new ShowDisplay();
                        {
                            Logica_ManagerClave _managerClave = new Logica_ManagerClave();

                            _showDisplay.showAccessCodeForm2(_managerClave.obtenerCodigoPrincipal(),
                                "La máquina se encuentra cerrada. Ingrese el código para continuar.",
                                this,
                                "LogicaIdentificarUsuario");
                        }
                    }
                    else if (_estadoMaquina.Equals(SQL._pending))
                    {
                        LanzadorMenu._mostrarMenuPrincipal = true;
                    }
                    else if (_estadoMaquina.Equals(string.Empty))
                    {

                        LogMessageAttention _logMessageAttention = new LogMessageAttention();
                        await _logMessageAttention.generalAttention(
                            "La ruta no ha sido cargada en los sistemas del Arreo."
                            + Environment.NewLine
                            + Environment.NewLine
                            + "* Comuníquese con el  Departamento de Liquidaciones");
                    }
                }
            }
            else
            {
                ShowCarga _show = new ShowCarga();
                _show.mostrarPantallaCarga(VariableCarga._inicial);

                LanzadorMenu._cargaDesdeCero = true;
            }

        }

        public async Task verificarTipoCargaParte2(bool _codigoCorrecto)
        {
            if (_codigoCorrecto)
            {
                //LogicaIdentificarUsuario_Carga _liu_c = new LogicaIdentificarUsuario_Carga(controlador);

                //await _liu_c.cargaInicialDiaria();

                var v_testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

                if (await v_testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
                {
                    var DPB = DependencyService.Get<ITaskActivity>();
                    DPB.StartCargaInicial(controlador);

                    LanzadorMenu._aperturaDiaria = true;

                }

            }
            else
            {
                LogMessageAttention _lma = new LogMessageAttention();

                await _lma.generalAttention("Debe ingresar el código correcto para ingresar");
            }
        }
    }
}
