using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CIISA.RetailOnLine.Aplicacion.Comunication.ProxySrol;
using CIISA.RetailOnLine.Aplicacion.InventoryOnLine.ViewController;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Carga.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Consulta.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.InventoryOnLine.Controller;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.IdentificarUsuario;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita.Guardar;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Helpers;
using CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Controlador;
using CIISA.RetailOnLine.Droid.Aplicacion.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Framework.Common.Feedback.ExceptionHH;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using Xamarin.Forms;
using CIISA.RetailOnLine.Framework.Common.Feedback.Message;

[assembly: Xamarin.Forms.Dependency(typeof(TaskActivity))]
namespace CIISA.RetailOnLine.Droid.Aplicacion.Helpers
{

    /// <summary>
    /// Clase encargada de manipular hilos que disparan acciones secundarias en el sistema
    /// </summary>
    public class TaskActivity : ITaskActivity
    {
        Thread activity = null;
        public LogMessageAttention _logMessageAttention = new LogMessageAttention();

        #region Menu
        public void StartConsolidar_Cerrar()
        {
            UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
            Thread actividad = new Thread(new ThreadStart(Cerrar_Consolidar));
            actividad.Start();
            activity = actividad;

        }

        private async void Cerrar_Consolidar() {

            try { 

                CerradoHandheld _cerrado = new CerradoHandheld();

                await _cerrado.CerrarMaquina(true);

            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally {

                UserDialogs.Instance.HideLoading();
            }
        }

        #endregion

        #region Carga Menu

        public void StartNewTaskIndAnulacion(ctrlCarga_indAnulacion controlador)
        {
            Thread actividad = new Thread(new ThreadStart(controlador.informacionIndicadorAnulacionRecargar));
            actividad.Start();
            activity = actividad;
        }

        public void StartNewTaskInventario(ctrlCarga_inventario _controlador) {
            Thread actividad = new Thread(new ThreadStart(_controlador.informacionInventarioRecargar));
            actividad.Start();
            activity = actividad;
        }

        public void StartNewTaskClientes(ctrlCarga_cliente _controlador)
        {
            Thread actividad = new Thread(new ThreadStart(_controlador.informacionClienteRecargar));
            actividad.Start();
            activity = actividad;
        }

        public void StartNewTaskEstablecimientos(ctrlCarga_establecimiento _controlador)
        {
            Thread actividad = new Thread(new ThreadStart(_controlador.informacionEstablecimientoRecargar));
            actividad.Start();
            activity = actividad;
        }

        public void StartNewTaskDescuentos(ctrlCarga_descuentos _controlador)
        {
            Thread actividad = new Thread(new ThreadStart(_controlador.informacionDescuentoRecargar));
            actividad.Start();
            activity = actividad;
        }

        public void StartNewTaskDescuentosGeneral(ctrlCarga_descuentosGeneral _controlador)
        {
            Thread actividad = new Thread(new ThreadStart(_controlador.informacionDescuentoGeneralRecargar));
            actividad.Start();
            activity = actividad;
        }

        public void StartNewTaskFacturas(ctrlCarga_facturas _controlador)
        {
            Thread actividad = new Thread(new ThreadStart(_controlador.informacionFactura_recarga_manualAgente));
            actividad.Start();
            activity = actividad;
        }

        public void StartNewTaskIndicadorFacturas(ctrlCarga_indicadores _controlador)
        {
            Thread actividad = new Thread(new ThreadStart(_controlador.informacionIndicadoresFacturacionRecargar));
            actividad.Start();
            activity = actividad;
        }

        public void StartNewTaskListaPrecios(ctrlCarga_listaPrecios _controlador)
        {
            Thread actividad = new Thread(new ThreadStart(_controlador.informacionListaPreciosRecargar));
            actividad.Start();
            activity = actividad;
        }

        public void StartNewTaskPrecioProductos(ctrlCarga_precioProducto _controlador)
        {
            Thread actividad = new Thread(new ThreadStart(_controlador.informacionPrecioProductoRecargar));
            actividad.Start();
            activity = actividad;
        }

        public void StartNewTaskProductos(ctrlCarga_producto _controlador)
        {
            Thread actividad = new Thread(new ThreadStart(_controlador.informacionProductoRecargar));
            actividad.Start();
            activity = actividad;
        }

        public void StartNewTaskVisita(ctrlCarga_visita _controlador)
        {
            Thread actividad = new Thread(new ThreadStart(_controlador.informacionVisitaRecargar));
            actividad.Start();
            activity = actividad;
        }

        public void StartNewTaskImpresora(ctrlCarga_impresora _controlador)
        {
            Thread actividad = new Thread(new ThreadStart(_controlador.informacionImpresoraRecargar));
            actividad.Start();
            activity = actividad;
        }

        public void StartNewTaskPedidos(ctrlCarga_encabezadoPedido _controlador)
        {
            Thread actividad = new Thread(new ThreadStart(_controlador.informacionEncabezadoPedidoRecargar));
            actividad.Start();
            activity = actividad;
        }

        public void StartNewTaskinformacionClienteIndividualRecargarParte3(ctrlCarga_cliente _controlador)
        {
            Thread actividad = new Thread(new ThreadStart(_controlador.informacionClienteIndividualRecargarParte4));
            actividad.Start();
            activity = actividad;
        }

        public void informacionIndicadoresIndividualRecargarParte3(ctrlCarga_indicadores _controlador)
        {
            Thread actividad = new Thread(new ThreadStart(_controlador.informacionIndicadoresIndividualRecargarParte4));
            actividad.Start();
            activity = actividad;
        }

        public void informacionDescuentoIndividualRecargarParte3(ctrlCarga_descuentos _controlador)
        {
            Thread actividad = new Thread(new ThreadStart(_controlador.informacionDescuentoIndividualRecargarParte4));
            actividad.Start();
            activity = actividad;
        }

        public void StartNewTaskDetalleReses(ctrlCarga_DetalleReses controlador)
        {
            Thread actividad = new Thread(new ThreadStart(controlador.informacionDetalleResesRecargar));
            actividad.Start();
            activity = actividad;
        }

        public void StartNewTaskMensajeFactura(ctrlCarga_MensajeFactura controlador)
        {
            Thread actividad = new Thread(new ThreadStart(controlador.informacionMensajeFacturaRecargar));
            actividad.Start();
            activity = actividad;
        }

        #endregion

        #region Descarga

        public void StartNewTaskinformacionDescargar(ctrlDescarga _controlador)
        {
            Thread actividad = new Thread(new ThreadStart(_controlador.informacionDescargar));
            actividad.Start();
            activity = actividad;
        }

        #endregion

        #region TomaFisica

        public void StartScreenInicializationTomaFisica(ctrlTomaFisica _controlador)
        {
            UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
            Thread actividad = new Thread(new ThreadStart(_controlador.ScreenInicialization));
            actividad.Start();
            activity = actividad;

        }

        public void ConsolidarTomaFisica(ctrlTomaFisica _controlador) {
            UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
            Thread actividad = new Thread(new ThreadStart(_controlador.menu_mniConsolidar_Click));
            actividad.Start();
            activity = actividad;
        }

        public void pnlFinalizarSincronizarTomaFisica(ctrlTomaFisica _controlador) {
            UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
            Thread actividad = new Thread(new ThreadStart(_controlador.pnlFinalizar_btnSincronizar_Click));
            actividad.Start();
            activity = actividad;
        }

        public void pnlFinalizar_Imprimir(ctrlTomaFisica _controlador) {

            UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
            Thread actividad = new Thread(new ThreadStart(_controlador.pnlFinalizar_btnImprimir_Click));
            actividad.Start();
            activity = actividad;
        }

        public void pnlFinalizarCerrarMaquina(ctrlTomaFisica _controlador) {

            UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
            Thread actividad = new Thread(new ThreadStart(_controlador.pnlFinalizar_btnCerrarMaquina_Click));
            actividad.Start();
            activity = actividad;
        }

        public void pnlFinalizar_Continuar(ctrlTomaFisica _controlador) {

            UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
            Thread actividad = new Thread(new ThreadStart(_controlador.menu_mniContinuar_Click));
            actividad.Start();
            activity = actividad;
        }

        public void pnlProcedimiento(ctrlTomaFisica _controlador) {

            UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
            Thread actividad = new Thread(new ThreadStart(_controlador.menu_mniProcedimiento_Click));
            actividad.Start();
            activity = actividad;
        }

        #endregion

        #region IdentificarUsuario

        public void StartSendActivity(LogicaVisitaGuardar controlador) {
            UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
            Thread actividad = new Thread(new ThreadStart(controlador.informacionDescargar));
            actividad.Start();
            activity = actividad;
        }

        public void StartCargaInicial(ctrlIdentificarUsuario controlador) {
            UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
            LogicaIdentificarUsuario_Carga _liu_c = new LogicaIdentificarUsuario_Carga(controlador);
            Thread actividad = new Thread(new ThreadStart(_liu_c.cargaInicialDiaria));
            actividad.Start();
            activity = actividad;
        }

        public void StartSendBitacora(pnlBitacoraModel Bitacora)
        {
            LogicaVisitaGuardar logica = new LogicaVisitaGuardar();

            Thread actividad = new Thread(new ThreadStart(async ()=> 
            {
                try
                {
                    UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
                    logica.informacionDescargarBitacora(Bitacora);
                    await _logMessageAttention.generalAttention("El proceso de envio termino exitosamente.");
                }
                catch (Exception ex)
                {
                    await ExceptionManager.ExceptionHandling(ex);
                }
                finally
                {
                    UserDialogs.Instance.HideLoading();
                }
            }));
            actividad.Start();
            activity = actividad;
        }

        #endregion

        #region ConsultaReciboRecaudacion

        public void StartConsultaReciboRecaudacion(ctrlConsultaReciboRecaudacion controlador)
        {
            UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
            Thread actividad = new Thread(new ThreadStart(controlador.menu_mniConsultar_Click));
            actividad.Start();
            activity = actividad;

        }

        public void StartScreenInicializationReciboRecaudacion(ctrlConsultaReciboRecaudacion controlador)
        {
            UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
            Thread actividad = new Thread(new ThreadStart(controlador.ScreenInicialization));
            actividad.Start();
            activity = actividad;

        }
        #endregion

        #region ConsultarDocumentos

        public void StartConsultaDocumentos(ctrlConsultaDocumentos controlador) {
            UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
            Thread actividad = new Thread(new ThreadStart(controlador.menu_mniConsultar_Click));
            actividad.Start();
            activity = actividad;
        }

        public void StartScreenInicializationConsultarDocumentos(ctrlConsultaDocumentos controlador) {

            UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
            Thread actividad = new Thread(new ThreadStart(controlador.ScreenInicialization));
            actividad.Start();
            activity = actividad;
        }

        #endregion

        #region ConsultarOV

        public void StartConsultaOV(ctrlConsultaOV controlador) {

            UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
            Thread actividad = new Thread(new ThreadStart(controlador.menu_mniConsultar_Click));
            actividad.Start();
            activity = actividad;

        }

        public void StartScreenInicializationConsultarOV(ctrlConsultaOV controlador) {

            Device.BeginInvokeOnMainThread(() =>
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
            });
            
            Thread actividad = new Thread(new ThreadStart(controlador.ScreenInicialization));
            actividad.Start();
            activity = actividad;

        }

        #endregion

        #region General

        public void PruebaConexion()
        {
            UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
            Thread actividad = new Thread(new ThreadStart(TestConnection));
            actividad.Start();
            activity = actividad;
        }

        public void AbortNewTask() {
            if (activity.IsAlive || activity.IsThreadPoolThread || activity.IsBackground)
            {
                activity.Interrupt();
                activity.Abort();
            }
        }

        internal async void TestConnection() {

            try
            {
                var _testConnectionSROL = DependencyService.Get<ITestConnectionSROL>();
                await _testConnectionSROL.testConnectionString(Sistema.establecerObjetoSystemCIISA(VersionROL._versionSROL), true);
            }
            catch (Exception ex)
            {
                await ExceptionManager.ExceptionHandling(ex);
            }
            finally {

                UserDialogs.Instance.HideLoading();
            }
        }

        public void SelectListViewItem(Xamarin.Forms.ListView listView, ObservableCollection<pnlCarniceria_Boxes> Source, int i)
        {
            Thread actividad = new Thread(new ThreadStart(() => {

                Device.BeginInvokeOnMainThread(() => {
                    listView.SelectedItem = Source.ElementAt(i);
                });

            }));

            actividad.Start();            
            activity = actividad;
        }

        #endregion

        #region Print

        public void ImprimirTransaccion(impTransaccion _impresion) {

            Device.BeginInvokeOnMainThread(() =>
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
            });

            Thread actividad = new Thread(new ThreadStart(_impresion.ImprimirProceso));
            actividad.Start();
            activity = actividad;

        }

        public void ImprimirReporte(ctrlVistaPrevia controlador)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                UserDialogs.Instance.ShowLoading("Procesando...", MaskType.Black);
            });

            Thread actividad = new Thread(new ThreadStart(controlador.menu_mniImprimir_Click));
            actividad.Start();
            activity = actividad;
        }

        #endregion

    }
}