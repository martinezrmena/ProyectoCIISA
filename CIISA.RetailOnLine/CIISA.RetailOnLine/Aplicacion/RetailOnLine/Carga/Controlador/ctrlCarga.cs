using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Vista;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.Character;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.External;
using CIISA.RetailOnLine.Framework.External.CustomListview;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.DataBase;
using CIISA.RetailOnLine.Framework.Handheld.Display.IMainLockScreen;
using CIISA.RetailOnLine.Framework.Handheld.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Power.Controller;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Controlador
{
    public class ctrlCarga
    {
        public vistaCarga view { get; set; }
        private bool v_condicionCarga = false;
        private int v_tipoCarga = Numeric._zeroInteger;
        private static CancellationTokenSource tokenSource = new CancellationTokenSource();
        private CancellationToken token = tokenSource.Token;
        private DateTime StartTime = DateTime.Now;

        public ctrlCarga(vistaCarga pview)
        {
            view = pview;
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlDetalle"));
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlParametros"));
            RenderPanels.paintPanel(view.FindByName<StackLayout>("pnlTablas"));

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlDetalle").Id))
            {
                view.Title = "Cargando "
                    + Simbol._squareBracketLeft
                    + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent
                    + Simbol._squareBracketRight;
            }

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlParametros").Id))
            {
                view.Title = "Parámetros";
            }

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlTablas").Id))
            {
                view.Title = "Carga Tablas";
            }

            ppanel.IsVisible = true;
        }

        private void cargaInformacionPanelBienvenida()
        {
            view.FindByName<Label>("pnlParametros_lblCodAgenteInfo").Text = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent;
            view.FindByName<Label>("pnlParametros_lblCodRutaInfo").Text = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codRute;

            view.FindByName<Label>("pnlParametros_lblNombreInfo").Text = Agent.getNombreAgente();

            string _tipoAgente = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._typeAgent;

            if (_tipoAgente.Equals(Agent._ruteroSigla))
            {
                view.FindByName<Label>("pnlParametros_lblTipoRuteroInfo").Text = Agent._ruteroNombre;
            }

            if (_tipoAgente.Equals(Agent._supermercadoSigla))
            {
                view.FindByName<Label>("pnlParametros_lblTipoRuteroInfo").Text = Agent._supermercadoNombre;
            }

            if (_tipoAgente.Equals(Agent._carniceroSigla))
            {
                view.FindByName<Label>("pnlParametros_lblTipoRuteroInfo").Text = Agent._carniceroNombre;
            }

            if (_tipoAgente.Equals(Agent._cobradorSigla))
            {
                view.FindByName<Label>("pnlParametros_lblTipoRuteroInfo").Text = Agent._cobradorNombre;
            }

            view.FindByName<Label>("pnlParametros_lblCodCompanniaInfo").Text = Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany;

            view.FindByName<Label>("pnlParametros_lblCodSuperInfo").Text = SellingGroup._supermarket;
            view.FindByName<Label>("pnlParametros_lblCodRuterosInfo").Text = SellingGroup._rutero;
            view.FindByName<Label>("pnlParametros_lblBaseDatosInfo").Text = DBCF._dataBaseName;
        }

        private void renderComponentesPnlTablas(bool pcarga)
        {
            SelectableObservableCollection<string> Source = new SelectableObservableCollection<string>();

            if (pcarga)
            {
                Source.Add(TablesROL._agenteVendedor);
            }

            if (pcarga)
            {
                Source.Add(TablesROL._autorizadoFirmar);
            }

            if (pcarga)
            {
                Source.Add(TablesROL._banco);
            }

            if (pcarga)
            {
                Source.Add(TablesROL._canton);
            }

            if (pcarga)
            {
                Source.Add(TablesROL._clasificacionCliente);
            }

            Source.Add(TablesROL._clave);

            Source.Add(TablesROL._cliente);

            if (pcarga)
            {
                Source.Add(TablesROL._compannia);
            }

            Source.Add(TablesROL._cuentaCerrada);

            Source.Add(TablesROL._descuentos);

            Source.Add(TablesROL._descuentoGeneral);

            Source.Add(TablesROL._detallePedido);

            if (pcarga)
            {
                Source.Add(TablesROL._distrito);
            }

            if (pcarga)
            {
                Source.Add(TablesROL._embalaje);
            }

            Source.Add(TablesROL._encabezadoPedido);

            if (pcarga)
            {
                Source.Add(TablesROL._especificacion);
            }

            Source.Add(TablesROL._establecimiento);

            Source.Add(TablesROL._factura);

            Source.Add(TablesROL._fechaServidor);

            if (pcarga)
            {
                Source.Add(TablesROL._formaPago);
            }

            Source.Add(TablesROL._impresora);

            Source.Add(TablesROL._indicadorFactura);

            if (pcarga)
            {
                Source.Add(TablesROL._informacionRuta);
            }

            Source.Add(TablesROL._inventario);

            Source.Add(TablesROL._listaPrecios);

            if (pcarga)
            {
                Source.Add(TablesROL._motivo);
            }

            if (pcarga)
            {
                Source.Add(TablesROL._pagoRecibo);
            }

            if (pcarga)
            {
                Source.Add(TablesROL._pagos);
            }

            if (pcarga)
            {
                Source.Add(TablesROL._pais);
            }

            Source.Add(TablesROL._precioCliente);

            Source.Add(TablesROL._precioProducto);

            Source.Add(TablesROL._producto);

            if (pcarga)
            {
                Source.Add(TablesROL._provincia);
            }

            if (pcarga)
            {
                Source.Add(TablesROL._razonesNV);
            }

            if (pcarga)
            {
                Source.Add(TablesROL._ruta);
            }

            if (pcarga)
            {
                Source.Add(TablesROL._sistema);
            }

            Source.Add(TablesROL._sms);

            if (pcarga)
            {
                Source.Add(TablesROL._sugerido);
            }

            if (pcarga)
            {
                Source.Add(TablesROL._tipoIdentificacion);
            }

            if (pcarga)
            {
                Source.Add(TablesROL._tipoTransaccion);
            }

            if (pcarga)
            {
                Source.Add(TablesROL._tituloComprobante);
            }

            Source.Add(TablesROL._visitas);

            Source.Add(TablesROL._DetalleReses);

            Source.Add(TablesROL._MensajeFactura);

            Source.Add(TablesROL._Exoneracion);

            Source.Add(TablesROL._TiposIVA);

            view.FindByName<ListView>("pnlTablas_ltvTablas").ItemsSource = Source;
        }

        private void renderMenu()
        {
            if (view.FindByName<StackLayout>("pnlDetalle").IsVisible)
            {
                view.ToolbarItems.Clear();
            }

            if (view.FindByName<StackLayout>("pnlTablas").IsVisible)
            {
                view.ToolbarItems.Clear();
                view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniSi"));
                view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniNo"));
            }

            if (view.FindByName<StackLayout>("pnlParametros").IsVisible)
            {
                view.ToolbarItems.Clear();
                view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniSi"));
                view.ToolbarItems.Add(view.FindByName<ToolbarItem>("menu_mniNo"));
            }

        }

        private void renderComponents(bool pcarga)
        {
            renderComponentesPnlTablas(pcarga);

            renderMenu();
        }

        private int cantidadTablasSeleccionadas()
        {
            int _cantTablasSeleccionadas = Numeric._zeroInteger;

            var Source = view.FindByName<ListView>("pnlTablas_ltvTablas").ItemsSource as SelectableObservableCollection<string>;

            foreach(var _lvi in Source)
            {
                if (_lvi.IsSelected)
                {
                    _cantTablasSeleccionadas++;
                }
            }

            return _cantTablasSeleccionadas;
        }

        private void parametrosDeCarga(Log plog)
        {
            plog.setDetail(Simbol._tab + "Tipo agente               : " + Agent._ruteroNombre);
            plog.setDetail(Simbol._tab + "Base de datos             : " + DBCF._dataBaseName);
            plog.setDetail(Simbol._tab + "Código grupo supermercados: " + SellingGroup._supermarket);
            plog.setDetail(Simbol._tab + "Código grupo ruteros      : " + SellingGroup._rutero);
            plog.setDetail(Simbol._tab + "Código de la ruta         : " + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codRute);
            plog.setDetail(Simbol._tab + "Código de la compañía     : " + Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codCompany);
        }

        private async Task cargaTablas(bool pcondicionCarga, int ptipoCarga)
        {
            Log _log = new Log();

            parametrosDeCarga(_log);

            _log.addLine(view.FindByName<KExtendedEditor>("pnlDetalle_txtCargaInformacion"), "==========================================================");
            _log.addLine(view.FindByName<KExtendedEditor>("pnlDetalle_txtCargaInformacion"), "3. CARGA DE INFORMACIÓN EN LAS TABLAS DE LA BASE DE DATOS.");
            _log.addLine(view.FindByName<KExtendedEditor>("pnlDetalle_txtCargaInformacion"), string.Empty);

            Carga_ManagerDatosTabla _manager = new Carga_ManagerDatosTabla();

            await _manager.cargaBorradoTablas(
                view.FindByName<ListView>("pnlTablas_ltvTablas"),
                pcondicionCarga,
                ptipoCarga,
                view.FindByName<KExtendedEditor>("pnlDetalle_txtCargaInformacion"),
                view.FindByName<Label>("pnlDetalle_lblInsertando"),
                _log,
                this
                );

            view.FindByName<KExtendedEditor>("pnlDetalle_txtCargaInformacion").IsVisible = true;
            view.FindByName<Label>("pnlDetalle_lblInsertando").Text = "Avance: 0\\0.";
            _log.addLine(view.FindByName<KExtendedEditor>("pnlDetalle_txtCargaInformacion"), string.Empty);
            _log.addLine(view.FindByName<KExtendedEditor>("pnlDetalle_txtCargaInformacion"), "FIN DEL PROCESO DE CARGA");
            _log.addLine(view.FindByName<KExtendedEditor>("pnlDetalle_txtCargaInformacion"), "REVISE LA BITÁCORA EN BUSCA DE ERRÓRES");
        }

        private void crearBaseDatos()
        {
            Log _log = new Log();

            parametrosDeCarga(_log);

            _log.addLine(view.FindByName<KExtendedEditor>("pnlDetalle_txtCargaInformacion"), "INICIO DEL PROCESO DE CARGA");

            _log.addLine(view.FindByName<KExtendedEditor>("pnlDetalle_txtCargaInformacion"), " ==========================================================");
            _log.addLine(view.FindByName<KExtendedEditor>("pnlDetalle_txtCargaInformacion"), "1. CREACIÓN DE LA BASE DE DATOS.");
            _log.addLine(view.FindByName<KExtendedEditor>("pnlDetalle_txtCargaInformacion"), string.Empty);

            var _establishDatabase = DependencyService.Get<IEstablishDatabase>();

            _establishDatabase.createDataBase(
                view.FindByName<KExtendedEditor>("pnlDetalle_txtCargaInformacion"),
                _log
                );
        }

        private void crearTablas()
        {
            Log _log = new Log();

            parametrosDeCarga(_log);

            _log.addLine(view.FindByName<KExtendedEditor>("pnlDetalle_txtCargaInformacion"), " ==========================================================");
            _log.addLine(view.FindByName<KExtendedEditor>("pnlDetalle_txtCargaInformacion"), "2. CREACIÓN DE LAS TABLAS DE LA BASE DE DATOS.");

            Carga_ManagerEstablecerTabla _manager = new Carga_ManagerEstablecerTabla(view.FindByName<Editor>("pnlDetalle_txtCargaInformacion"), _log);

        }

        private async Task iniciarMaquina(GUIAvanceManual ControlAdvance)
        {
            await ControlAdvance.actualizarGUIdesdeHilo_45_porciento();

            crearBaseDatos();

            await ControlAdvance.actualizarGUIdesdeHilo_65_porciento();

            crearTablas();

            await ControlAdvance.actualizarGUIdesdeHilo_85_porciento();

            await cargaTablas(v_condicionCarga, v_tipoCarga);

            await ControlAdvance.actualizarGUIdesdeHilo_95_porciento();

        }

        internal async Task menu_mniSi_Click()
        {
            var BatteryState = DependencyService.Get<IAndroidMethods>();

            if (!await BatteryState.ValueDisableSaveBattery())
            {
                if (view.FindByName<StackLayout>("pnlTablas").IsVisible)
                {
                    int _cantTablas = cantidadTablasSeleccionadas();

                    if (_cantTablas > 0)
                    {
                        await Task.Run(() =>
                        {
                            Device.BeginInvokeOnMainThread(() => {

                                renderMenu();

                                MiscUtils.deleteNoSelectedItemsListView<string>(view.FindByName<ListView>("pnlTablas_ltvTablas"));

                                renderPaneles(view.FindByName<StackLayout>("pnlDetalle"));

                                RenderDetalleAvance(true);
                            });

                        }).ConfigureAwait(true);

                        GUIAvanceManual ControlAdvance = new GUIAvanceManual(this);

                        StartTime = DateTime.Now;

                        await ControlAdvance.actualizarGUIdesdeHilo_25_porciento();

                        await cargaTablas(v_condicionCarga, v_tipoCarga);

                        await ControlAdvance.actualizarGUIdesdeHilo_85_porciento();

                        renderPaneles(view.FindByName<StackLayout>("pnlDetalle"));

                        await ControlAdvance.actualizarGUIdesdeHilo_100_porciento(StartTime);

                    }
                    else
                    {
                        LogMessageAttention _logMessageAttention = new LogMessageAttention();
                        await _logMessageAttention.generalAttention("Alerta: "
                            + Environment.NewLine
                            + Environment.NewLine
                            + "Debe seleccionar al menos una tabla");
                    }
                }

                if (view.FindByName<StackLayout>("pnlParametros").IsVisible)
                {
                    GUIAvanceManual ControlAdvance = new GUIAvanceManual(this);

                    await Task.Run(() =>
                    {
                        Device.BeginInvokeOnMainThread(()=> {
                            renderPaneles(view.FindByName<StackLayout>("pnlDetalle"));
                            renderMenu();
                            RenderDetalleAvance(true);
                        });

                    }).ConfigureAwait(true);
                    

                    await ControlAdvance.actualizarGUIdesdeHilo_0_porciento();

                    StartTime = DateTime.Now;

                    await iniciarMaquina(ControlAdvance);

                    await ControlAdvance.actualizarGUIdesdeHilo_100_porciento(StartTime);

                    renderPaneles(view.FindByName<StackLayout>("pnlDetalle"));
                }

                renderMenu();

            }            
        }

        internal async void ScreenInicialization(int ptipoCarga)
        {
            v_tipoCarga = ptipoCarga;

            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlParametros"));

            cargaInformacionPanelBienvenida();

            if (ptipoCarga == VariableCarga._inicial)
            {
                v_condicionCarga = true;
            }

            if (ptipoCarga == VariableCarga._seleccionTablas)
            {
                v_condicionCarga = true;
            }

            if (ptipoCarga == VariableCarga._recargaDiaria)
            {
                v_condicionCarga = false;
            }

            renderComponents(v_condicionCarga);

            if (ptipoCarga == VariableCarga._inicial)
            {

                renderPaneles(view.FindByName<StackLayout>("pnlParametros"));
            }

            if (ptipoCarga == VariableCarga._seleccionTablas)
            {
                renderPaneles(view.FindByName<StackLayout>("pnlTablas"));
            }

            if (ptipoCarga == VariableCarga._recargaDiaria)
            {
                var Source = view.FindByName<ListView>("pnlTablas_ltvTablas").ItemsSource as SelectableObservableCollection<string>;
                
                for (int i = 0; i < Source.Count; i++)
                {
                    Source[i].IsSelected = true;
                }
                view.FindByName<ListView>("pnlTablas_ltvTablas").ItemsSource = Source;

                await menu_mniSi_Click();
            }
        }

        internal void menu_mniNo_Click()
        {
            view.v_cerrado = true;

            Application.Current.MainPage.Navigation.PopAsync();
        }

        internal void pnlSeleccionTablas_chkTodas_CheckStateChanged()
        {
            var Source = view.FindByName<ListView>("pnlTablas_ltvTablas").ItemsSource as SelectableObservableCollection<string>;

            
            for (int i = 0; i < Source.Count; i++)
            {
                Source[i].IsSelected = view.FindByName<CheckBox>("pnlTablas_chkTodas").Checked;               
            }

            view.FindByName<ListView>("pnlTablas_ltvTablas").ItemsSource = Source;
        }

        internal void vistaCargaMaquina_Closing()
        {
            view.v_cerrado = true;

            Application.Current.MainPage.Navigation.PopAsync();

            var servicio = DependencyService.Get<IServicio_Aplicacion>();
            servicio.Exit();
        }

        public void RenderDetalleAvance(bool Disponible) {

            view.FindByName<StackLayout>("pnlAvance").IsVisible = Disponible;
        }
    }
}
