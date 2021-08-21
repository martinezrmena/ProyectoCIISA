using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Consulta.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RadioTelefonico.ListviewModels;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Helpers;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.External;
using CIISA.RetailOnLine.Framework.External.CustomTreeView;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;


namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Controlador
{
    public class ctrlDescarga
    {

        internal vistaDescarga view { get; set; }
        private bool v_esUsuarioLiquidador = false;
        private DateTime v_startTime = VarTime.getNow();
        private bool v_hiloCorriendo = false;
        private static CancellationTokenSource tokenSource = new CancellationTokenSource();
        private CancellationToken token = tokenSource.Token;
        internal ITaskActivity DPB = DependencyService.Get<ITaskActivity>();

        internal ctrlDescarga(vistaDescarga pview, bool pesUsuarioLiquidador)
        {
            view = pview;
            v_esUsuarioLiquidador = pesUsuarioLiquidador;
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlTransacciones").Id))
            {
                view.Title = "Sincronización/Consulta";
            }

            ppanel.IsVisible = true;
        }

        private void renderComponents()
        {
            view.FindByName<Label>("pnlTransacciones_lblInfoDetalles").Text = string.Empty;

            view.FindByName<ListView>("pnlTransacciones_trvTransacciones").ItemsSource = new ObservableCollection<CollapsableItem>();
        }

        internal void consultaDocumentos(Color color)
        {
            renderComponents();

            Descarga_ManagerTransaccion _managerTransaccion = new Descarga_ManagerTransaccion();

            _managerTransaccion.consultaTransaccionDetalles(view.FindByName<ListView>("pnlTransacciones_trvTransacciones"), color);

            Descarga_ManagerRecibo _managerRecibo = new Descarga_ManagerRecibo();

            _managerRecibo.consultaReciboDetalles(view.FindByName<ListView>("pnlTransacciones_trvTransacciones"), color);

            Descarga_ManagerTramite _managerTramite = new Descarga_ManagerTramite();

            _managerTramite.consultaTramiteDetalles(view.FindByName<ListView>("pnlTransacciones_trvTransacciones"), color);

            view.FindByName<CheckBox>("pnlTransacciones_chkExpandirLineas").Checked = true;

            ExpandAndCollapseAll(true);

        }

        private void refrescarPantalla(Color color)
        {
            renderComponents();

            consultaDocumentos(color);

            Logica_ManagerDetalleTransaccion _manager = new Logica_ManagerDetalleTransaccion();

            if (!_manager.BuscarDocumentosSinEnviar())
            {
                view.FindByName<Label>("pnlTransacciones_lblInfoDetalles").Text = "* Existen detalles SIN enviar.";
            }

        }

        public async void informacionDescargar()
        {
            GUIDescarga _guiDescarga = new GUIDescarga(this);

            _guiDescarga.actualizarGUIdesdeHilo_0_porciento_iniciando();

            var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

            if (await _testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
            {
                Descarga_ManagerEnvio _manager = new Descarga_ManagerEnvio();

                await _manager.EnvioPaqueteInformacionPorWS_Total();

                _guiDescarga.actualizarGUIdesdeHilo_100_porciento(v_startTime);
            }
            else
            {
                _guiDescarga.actualizarGUIdesdeHiloError();
            }

            _guiDescarga.actualizarGUIdesdeHiloBoton(true, false);

            v_hiloCorriendo = false;
        }

        private void ExpandAndCollapseAll(bool ExpandirTodas)
        {
            var Source = view.FindByName<ListView>("pnlTransacciones_trvTransacciones").ItemsSource as ObservableCollection<CollapsableItem>;
            foreach (var item in Source)
            {
                item.IsCollapsed = ExpandirTodas;
            }
            view.FindByName<ListView>("pnlTransacciones_trvTransacciones").ItemsSource = Source;
        }
        
        internal void cargarConfiguracionSistema(bool pavanzado)
        {
            var Info = DependencyService.Get<IServicio_Phone>();

            view.FindByName<ListView>("pnlTransacciones_ltvConfiguracion").ItemsSource = new ObservableCollection<pnlTelefono_ltvConfiguracion>();

            ObservableCollection<pnlTelefono_ltvConfiguracion> Source = new ObservableCollection<pnlTelefono_ltvConfiguracion>();

            pnlTelefono_ltvConfiguracion _lvi01 = new pnlTelefono_ltvConfiguracion();
            _lvi01.Configuracion = "Tipo Sistema";            

            _lvi01.Valor = Info.PhoneType();

            if (pavanzado)
            {
                Source.Add(_lvi01);
            }

            pnlTelefono_ltvConfiguracion _lvi02 = new pnlTelefono_ltvConfiguracion();
            _lvi02.Configuracion = "Intensidad Señal";
            _lvi02.Valor = Info.GetSignalStrength();

            int Numero = 0;

            bool IsNumeric = int.TryParse(_lvi02.Valor, out Numero);

            if (IsNumeric)
            {
                if (Numero <= 74)
                {
                    _lvi02.TextColor = Color.Red;
                }
                if (Numero >= 75)
                {
                    _lvi02.TextColor = Color.Blue;
                }
            }
            else
            {
                _lvi02.TextColor = Color.Red;
            }

            Source.Add(_lvi02);

            pnlTelefono_ltvConfiguracion _lvi03 = new pnlTelefono_ltvConfiguracion();
            _lvi03.Configuracion = "Estado Red";
            _lvi03.Valor = Info.GetNetworkStatus();
            if (_lvi03.Valor.Equals(string.Empty))
            {
                _lvi03.Valor = "Sin Red";
            }
            if (pavanzado)
            {
                Source.Add(_lvi03);
            }

            pnlTelefono_ltvConfiguracion _lvi04 = new pnlTelefono_ltvConfiguracion();
            _lvi04.Configuracion = "Operador";
            _lvi04.Valor = Info.GetNetworkOperator();
            if (pavanzado)
            {
                Source.Add(_lvi04);
            }

            pnlTelefono_ltvConfiguracion _lvi05 = new pnlTelefono_ltvConfiguracion();
            _lvi05.Configuracion = "Radio Mfg";
            _lvi05.Valor = Info.GetManufacturer();
            if (pavanzado)
            {
                Source.Add(_lvi05);
            }

            pnlTelefono_ltvConfiguracion _lvi06 = new pnlTelefono_ltvConfiguracion();
            _lvi06.Configuracion = "Radio FW";
            _lvi06.Valor = Info.GetRevision();
            if (pavanzado)
            {
                Source.Add(_lvi06);
            }

            pnlTelefono_ltvConfiguracion _lvi07 = new pnlTelefono_ltvConfiguracion();
            _lvi07.Configuracion = "Radio Model";
            _lvi07.Valor = Info.GetModel();
            if (pavanzado)
            {
                Source.Add(_lvi07);
            }

            pnlTelefono_ltvConfiguracion _lvi08 = new pnlTelefono_ltvConfiguracion();
            _lvi08.Configuracion = "IMEI";
            _lvi08.Valor = Info.GetIMEI();
            if (pavanzado)
            {
                Source.Add(_lvi08);
            }

            pnlTelefono_ltvConfiguracion _lvi09 = new pnlTelefono_ltvConfiguracion();
            _lvi09.Configuracion = "SIM ID";
            _lvi09.Valor = Info.GetSIMID();

            if (pavanzado)
            {
                Source.Add(_lvi09);
            }

            pnlTelefono_ltvConfiguracion _lvi10 = new pnlTelefono_ltvConfiguracion();
            _lvi10.Configuracion = "Estado SIM";
            _lvi10.Valor = Info.GetSimStatus();
            if (_lvi10.Valor.Equals(string.Empty))
            {
                _lvi10.Valor = "Apagado";
                _lvi10.TextColor = Color.Red;
            }
            Source.Add(_lvi10);

            view.FindByName<ListView>("pnlTransacciones_ltvConfiguracion").ItemsSource = Source;
        }

        internal async void ScreenInicialization(string ppantallaInvoca)
        {
            RenderWindows.paintWindow(view);
            renderPaneles(view.FindByName<StackLayout>("pnlTransacciones"));

            refrescarPantalla(Color.Black);

            if (ppantallaInvoca.Equals(PantallasSistema._pantallaVisita))
            {
                await pnlTransacciones_btnEnviar_Click();
            }

            cargarConfiguracionSistema(false);

            //El Telefono siempre estara activo
            bool _activar = true;

            GUIDescarga _guiDescarga = new GUIDescarga(this);

            _guiDescarga.actualizarGUIdesdeHiloBoton(_activar, _activar);

            view.FindByName<CheckBox>("pnlTransacciones_chkExpandirLineas").Checked = true;

            await menu_mniMenu_mniMarcarComoNoEnviados_Click();
        }

        internal void menu_mniClose_Click()
        {
            if (v_hiloCorriendo)
            {
                DPB.AbortNewTask();
                v_hiloCorriendo = false;
            }

            Application.Current.MainPage.Navigation.PopAsync();
        }

        internal void renderBotones(bool pbtnSend, bool pbtnAbort)
        {
            view.FindByName<Button>("pnlTransacciones_btnEnviar").IsEnabled = pbtnSend;

            view.FindByName<Button>("pnlTransacciones_btnAbortar").IsEnabled = pbtnAbort;

            view.FindByName<Button>("pnlTransacciones_btnRefrescar").IsEnabled = pbtnSend;

            view.FindByName<StackLayout>("pnlTransacciones_btnPruebaConexion").IsVisible = pbtnSend;

            view.FindByName<Button>("pnlTransacciones_btnMarcarNoEnviados").IsEnabled = pbtnSend;

            view.FindByName<StackLayout>("pnlTransacciones_stkCerrar").IsVisible = true;
        }

        private void marcarDatosComoNoEnvidos()
        {
            Descarga_ManagerGenerico _manager = new Descarga_ManagerGenerico();

            _manager.marcarDatosComoNoEnvidos();

            refrescarPantalla(Color.Red);
        }
        
        #region Botones
        internal async Task pnlTransacciones_btnEnviar_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea enviar la información?", "Enviar"))
            {
                view.FindByName<ListView>("pnlTransacciones_ltvConfiguracion").Focus();

                ctrlConsultaBitaRecarga _consulta = new ctrlConsultaBitaRecarga();

                DataTable _dt = await _consulta.ConsultaBitaRecarga();

                /*
                 * EVARGAS 
                 * Si existen resultados quiere decir que hay recargas generadas 
                 * en backofice que deben ser cargadas antes de sincronizar,
                 * de lo contrario,
                 * no existen recargas y se puede enviar la informacion
                 */

                if (_dt.Rows.Count > 0)
                {
                    string fecha_toma = null;
                    string fecha_recarga_hh = null;
                    string fecha_recarga_naf = null;

                    /*Debido a que la consulta se hace por dia y agente, solo se devuelve un resultado*/
                    foreach (DataRow _fila in _dt.Rows)
                    {
                        fecha_toma = _fila["FECHA_TOMA"].ToString();
                        fecha_recarga_hh = _fila["FECHA_RECARGA_HH"].ToString();
                        fecha_recarga_naf = _fila["FECHA_RECARGA_NAF"].ToString();
                    }

                    /* Si la fecha_recarga_hh (fecha de la ultima regarga en HH) es null quiere decir que desde la hh 
                     * aun no ha realizado ninguna recarga de inventario, por lo tanto:
                     *   -> Debe informarse al usuario que hay recargas pendientes de aplicar en la hh
                     *   -> El proceso de "Cargar => Inventario" se encarga de llenar la informacion del campo fecha_recarga_hh 
                     *   
                     * Si fecha_recarga_hh ya no es null debemos asegurarnos que la ultima fecha_recarga_hh fue posterior a la ultima fecha_recarga_naf
                     *   -> Si lo anterior no se cumple quiere decir que se generaron recargas en naf que aun no son aplicadas en la hh
                     *   -> Por lo que debe informarse al usuario que hay recargas pendientes de aplicar en la hh
                     */
                    if (string.IsNullOrEmpty(fecha_recarga_hh) || (Convert.ToDateTime(fecha_recarga_hh) <= Convert.ToDateTime(fecha_recarga_naf)))
                    {
                        LogMessageAttention _lma = new LogMessageAttention();
                        await _lma.generalAttention("Existen en su ruta recargas que aún no han sido aplicadas en el inventario de la máquina. Es necesario que cargue antes el inventario para poder enviar la información.");
                    }
                    else
                    {
                        Enviar_Informacion();
                    }
                }
                else
                {
                    Enviar_Informacion();

                }
            }

        }

        internal void Enviar_Informacion()
        {

            GUIDescarga _guiDescarga = new GUIDescarga(this);

            _guiDescarga.actualizarGUIdesdeHiloBoton(false, true);

            v_hiloCorriendo = true;

            v_startTime = VarTime.getNow();

            DPB.StartNewTaskinformacionDescargar(this);
        }

        internal async Task pnlTransacciones_btnAbortar_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea abortar el envio de información?", "Abortar"))
            {
                view.FindByName<ListView>("pnlTransacciones_ltvConfiguracion").Focus();

                if (v_hiloCorriendo)
                {
                    DPB.AbortNewTask();
                }

                await menu_mniMenu_mniMarcarComoNoEnviados_Click();

                GUIDescarga _guiDescarga = new GUIDescarga(this);

                _guiDescarga.actualizarGUIdesdeHilo("CANCELO LA DESCARGA - 0%", Color.FromRgb(158, 4, 4));

                Log _log = new Log();

                Editor _textBox = new Editor();

                _log.addAlertLine(
                    _textBox,
                    "Abortó el proceso de descarga de información.");

                view.FindByName<Button>("pnlTransacciones_btnEnviar").IsEnabled = true;

                _guiDescarga.actualizarGUIdesdeHiloBoton(true, false);

                v_hiloCorriendo = false;
            }
        }

        internal void pnlTransacciones_btnRefrescar_Click()
        {
            refrescarPantalla(Color.Black);
        }

        internal async Task menu_mniMenu_mniPruebaConexion_Click()
        {
            var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

            await _testConnectionSROL.testConnectionString(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true);
        }

        internal async Task menu_mniMenu_mniMarcarComoNoEnviados_Click()
        {
            if (v_esUsuarioLiquidador)
            {
                marcarDatosComoNoEnvidos();
            }
            else
            {
                if (await LogMessages._dialogResultYes("¿Desea marcar todos los documentos como no enviados?", "Marcar como no enviados"))
                {
                    marcarDatosComoNoEnvidos();
                }
            }

        }
        #endregion

        #region CheckBoxes
        internal void pnlTransacciones_chkExpandirLineas_CheckStateChanged()
        {
            if (view.FindByName<CheckBox>("pnlTransacciones_chkExpandirLineas").Checked)
            {
                ExpandAndCollapseAll(true);
            }
            else
            {
                ExpandAndCollapseAll(false);
            }
        }
        #endregion
    }
}
