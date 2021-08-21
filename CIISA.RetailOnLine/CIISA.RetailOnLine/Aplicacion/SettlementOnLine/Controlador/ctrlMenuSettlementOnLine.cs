using CIISA.RetailOnLine.Aplicacion.AuditOnLine.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.CargaDiaria.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.VistaControlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Respaldo.Modelo;
using CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Modelo;
using CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Vista;
using CIISA.RetailOnLine.Aplicacion.SettlementOnLine.VistaController;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Feedback;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Handheld.Render;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Controlador
{
    internal class ctrlMenuSettlementOnLine
    {
        internal vistaMenuSettlementOnLine view { get; set; }

        internal ctrlMenuSettlementOnLine(vistaMenuSettlementOnLine pview)
        {
            view = pview;
        }

        private void renderPaneles(StackLayout ppanel)
        {
            RenderPanels.paintPanel(ppanel);

            if (ppanel.Id.Equals(view.FindByName<StackLayout>("pnlLiquidadores").Id))
            {
                view.Title = "Liquidadores";
            }

            ppanel.IsVisible = true;
        }

        private void actualizarEstadoSistema()
        {
            Logica_ManagerSistema _manager = new Logica_ManagerSistema();

            if (_manager.buscarEstado().Equals(SQL._close))
            {
                view.FindByName<Label>("pnlLiquidadores_lblCerradoAbierto").Text = "Cerrado";
            }
            else
            {
                view.FindByName<Label>("pnlLiquidadores_lblCerradoAbierto").Text = "Pendiente";
            };
        }

        internal void ScreenInicialization()
        {
            RenderWindows.paintWindow(view);

            renderPaneles(view.FindByName<StackLayout>("pnlLiquidadores"));

            actualizarEstadoSistema();
        }

        internal void menu_mniClose_Click()
        {
            //view.Close();
            Application.Current.MainPage.Navigation.PopAsync();
        }

        internal void pnlLiquidadores_btnDescarga_Click()
        {
            ShowDescarga _show = new ShowDescarga();

            _show.mostrarPantallaDescarga(PantallasSistema._pantallaMenuLiquidadores, TipoDescarga._normal, true);
        }

        internal void pnlLiquidadores_btnSincronizarRespaldo_Click()
        {
            ShowDescarga _show = new ShowDescarga();

            _show.mostrarPantallaDescarga(PantallasSistema._pantallaMenuLiquidadores, TipoDescarga._antiguedad, true);
        }

        internal void pnlLiquidadores_btnCarga_Click()
        {
            ShowRecargaManual _show = new ShowRecargaManual();

            _show.mostrarPantallaMenuCarga();
        }

        internal async Task pnlLiquidadores_btnCerradoManual_Click()
        {
            if (await LogMessages._dialogResultYes(
                "¿Desea cerrarla la máquina?",
                "Cierre Máquina"))
            {
                Logica_ManagerSistema _manager = new Logica_ManagerSistema();
                _manager.actualizarEstado(true);

                actualizarEstadoSistema();
            }
        }

        internal async Task pnlLiquidadores_btnAperturaManual_Click()
        {
            if (await LogMessages._dialogResultYes(
                "¿Desea abrir la máquina?",
                "Abrir Máquina"))
            {
                Logica_ManagerSistema _manager = new Logica_ManagerSistema();
                _manager.actualizarEstado(false);

                actualizarEstadoSistema();

                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention("La apertura de la máquina se realizó exitosamente.");
            }
        }

        internal async Task pnlLiquidadores_btnDescargaConsecutivos_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea descargar los consecutivos?", "Descarga Información"))
            {
                var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

                if (await _testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
                {
                    LogMessageAttention _logMessageAttention = new LogMessageAttention();
                    await _logMessageAttention.generalAttention("Finalizó la descarga de consecutivos.");
                }
            }
        }

        internal async Task pnlLiquidadores_btnCargaConsecutivos_Click()
        {
            var v_testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

            if (await v_testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
            {
                if (await LogMessages._dialogResultYes("¿Desea cargar los consecutivos?", "Carga Información"))
                {
                    view.FindByName<Editor>("pnlLiquidadores_txtDescarga").Text = string.Empty;

                    Log _log = new Log();

                    Carga_ManagerRecargaDiaria _manager = new Carga_ManagerRecargaDiaria();

                    await _manager.recargaDiariaTablaAgenteVendedor(
                        _log,
                        view.FindByName<Editor>("pnlLiquidadores_txtDescarga"),
                        view.FindByName<Label>("pnlLiquidadores_lblInsertando")
                        );

                    LogMessageAttention _logMessageAttention = new LogMessageAttention();
                    await _logMessageAttention.generalAttention("Finalizó la carga de consecutivos.");
                }
            }
        }

        internal async Task pnlLiquidadores_btnReporteConsecutivos_Click()
        {
            ProcesoImpresion _impresion = new ProcesoImpresion();

            bool hayImpresoras = false;

            hayImpresoras = await _impresion.HayImpresorasConectadas();

            if (hayImpresoras)
            {

                await _impresion.imprimirReporteConsecutivoDocumentos();

                await LogMessageSuccess.printed();
            }
        }

        internal async Task pnlLiquidadores_btnCargaPedidos_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea cargar los pedidos?", "Carga Información"))
            {
                view.FindByName<Editor>("pnlLiquidadores_txtDescarga").Text = string.Empty;

                Log _log = new Log();

                Carga_ManagerRecargaDiaria _manager = new Carga_ManagerRecargaDiaria();

                await _manager.recargaDiariaTablaEncabezadoPedido(
                    _log,
                    view.FindByName<Editor>("pnlLiquidadores_txtDescarga"),
                    view.FindByName<Label>("pnlLiquidadores_lblInsertando")
                    );

                _log = new Log();

                await _manager.recargaDiariaTablaDetallePedido(
                    _log,
                    view.FindByName<Editor>("pnlLiquidadores_txtDescarga"),
                    view.FindByName<Label>("pnlLiquidadores_lblInsertando")
                    );

                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention("Finalizó la carga de pedidos.");
            }
        }

        internal void pnlLiquidadores_btnReimpresion_Click()
        {
            ShowLiquidaciones _show = new ShowLiquidaciones();

            _show.mostrarPantallaLiquidadoresReimpresion();
        }

        internal void pnlLiquidadores_btnReimpresionAntiguedad_Click()
        {
            ShowLiquidaciones _show = new ShowLiquidaciones();

            _show.mostrarPantallaLiquidadoresReimpresionAntiguedad();
        }

        private async void recargaAgenteVendedorFacturacionFaltantes()
        {
            view.FindByName<Editor>("pnlLiquidadores_txtAgenteVendedor").Text = string.Empty;

            view.FindByName<Editor>("pnlLiquidadores_txtDescarga").Text = string.Empty;

            Log _log = new Log();

            Carga_ManagerRecargaDiaria _managerRecarga = new Carga_ManagerRecargaDiaria();

            await _managerRecarga.recargaDiariaTablaAgenteVendedor(
                _log,
                view.FindByName<Editor>("pnlLiquidadores_txtDescarga"),
                view.FindByName<Label>("pnlLiquidadores_lblInsertando")
                );

            Settlement_ManagerAgenteVendedor _managerAgenteVendedor = new Settlement_ManagerAgenteVendedor();

            string _codCliente = _managerAgenteVendedor.obtenerCodigoCliente();

            await _managerRecarga.recargaDiariaTablaClienteFacturacionFaltantes(
                _codCliente,
                _log,
                view.FindByName<Editor>("pnlLiquidadores_txtDescarga"),
                view.FindByName<Label>("pnlLiquidadores_lblInsertando")
                );

            await _managerRecarga.recargaDiariaTablaEstablecimientoFacturacionFaltantes(
                _codCliente,
                _log,
                view.FindByName<Editor>("pnlLiquidadores_txtDescarga"),
                view.FindByName<Label>("pnlLiquidadores_lblInsertando")
                );

            await _managerRecarga.recargaDiariaTablaVisitaFacturacionFaltantes(
                _codCliente,
                _log,
                view.FindByName<Editor>("pnlLiquidadores_txtDescarga"),
                view.FindByName<Label>("pnlLiquidadores_lblInsertando")
                );

            await _managerRecarga.recargaDiariaTablaIndicadorFacturaFacturacionFaltantes(
                _codCliente,
                _log,
                view.FindByName<Editor>("pnlLiquidadores_txtDescarga"),
                view.FindByName<Label>("pnlLiquidadores_lblInsertando")
                );
        }

        internal async Task pnlLiquidadores_btnCargarAgente_Click()
        {
            if (await LogMessages._dialogResultYes("¿Desea cargar el Agente?", "Carga Información"))
            {
                recargaAgenteVendedorFacturacionFaltantes();

                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention("Finalizó la carga de agente.");
            }
        }

        internal async Task pnlLiquidadores_btnConsultarAgente_Click()
        {
            view.FindByName<Editor>("pnlLiquidadores_txtAgenteVendedor").Text = string.Empty;

            Logica_ManagerAgenteVendedor _managerAgenteVendedor = new Logica_ManagerAgenteVendedor();
            DataTable _dt = _managerAgenteVendedor.buscarAgenteVendedor(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL)._codAgent);

            foreach (DataRow _fila in _dt.Rows)
            {
                view.FindByName<Editor>("pnlLiquidadores_txtAgenteVendedor").Text = _fila[TableAgenteVendedor._NOM_AGENTE].ToString();
            }
            LogMessageAttention logMessage = new LogMessageAttention();
            await logMessage.generalAttention("Verifique los consecutivos, despúes de cargar al Agente.");
        }

        internal void pnlLiquidadores_btnAuditoria_Click()
        {
            ShowAuditoria _show = new ShowAuditoria();

            _show.mostrarPantallaAuditoria();
        }

        internal async Task pnlLiquidadores_btnCargarClaves_Click()
        {
            var v_testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();

            if (await v_testConnectionSROL.testConnectionBoolean(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true))
            {
                if (await LogMessages._dialogResultYes("¿Desea cargar las claves?", "Carga Información"))
                {
                    view.FindByName<Editor>("pnlLiquidadores_txtDescarga").Text = string.Empty;

                    Log _log = new Log();

                    Carga_ManagerRecargaDiaria _manager = new Carga_ManagerRecargaDiaria();

                    await _manager.recargaDiariaTablaClave(
                        _log,
                        view.FindByName<Editor>("pnlLiquidadores_txtDescarga"),
                        view.FindByName<Label>("pnlLiquidadores_lblInsertando")
                        );

                    LogMessageAttention _logMessageAttention = new LogMessageAttention();
                    await _logMessageAttention.generalAttention("Finalizó la carga de claves.");
                }
            }
        }

        internal async Task pnlLiquidadores_btnMostrarClaves_Click()
        {
            Logica_ManagerClave _managerClave = new Logica_ManagerClave();

            string _claves = _managerClave.mostrarClaves();

            LogMessageAttention _logMessageAttention = new LogMessageAttention();
            await _logMessageAttention.generalAttention(_claves);
        }

        internal async Task pnlLiquidadores_btnRespaldo_Click()
        {

            if (await LogMessages._dialogResultYes("¿Desea respaldar la información?", "Respaldo Información"))
            {
                Respaldo_ManagerRespaldar _managerRespaldar = new Respaldo_ManagerRespaldar();

                Log _log = new Log();

                Editor _textBox = new Editor();

                _managerRespaldar.respaldarInformacion(_textBox, _log);

                LogMessageAttention _logMessageAttention = new LogMessageAttention();
                await _logMessageAttention.generalAttention("Finalizó el respaldo de la información.");
            }
        }

        internal async Task pnlLiquidadores_btnCargaClienteFaltantes_Click()
        {
            recargaAgenteVendedorFacturacionFaltantes();

            LogMessageAttention _logMessageAttention = new LogMessageAttention();
            await _logMessageAttention.generalAttention("Finalizó la carga cliente facturación faltantes.");
        }

        public async Task pnlMenu_btnFactFaltantes_Click()
        {
            ShowSROL _showVisitaFacturacionFaltantes = new ShowSROL();

            if (true)
            {
                Logica_ManagerAgenteVendedor _managerAgenteVendedor = new Logica_ManagerAgenteVendedor();

                string _codCliente = _managerAgenteVendedor.obtenerCodigoClienteAgenteVendedor();

                Cliente _objClienteFacturacionFaltantes = new Cliente { v_no_cliente = _codCliente };

                Logica_ManagerCliente _manager = new Logica_ManagerCliente();

                await _manager.buscarClientePorCodigoCliente(_objClienteFacturacionFaltantes);

                _showVisitaFacturacionFaltantes.v_objCliente = _objClienteFacturacionFaltantes;
            }

            if (true)
            {
                _showVisitaFacturacionFaltantes.mostrarPantallaVisitaDesdeFacturacionFaltantes();
            }
        }
    }
}
