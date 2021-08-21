using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Consulta.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Impresion.Impresiones.ProcesoImpresion;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.InventoryOnLine.Controller;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.ListviewModels.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.IdentificarUsuario;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.VistaControlador.Visita.Guardar;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Presentacion.Controlador;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Controlador;
using CIISA.RetailOnLine.Aplicacion.SettlementOnLine.Controlador;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.RecargaManual.Helpers
{
    public interface ITaskActivity
    {

        #region Menu
            void StartConsolidar_Cerrar();
        #endregion

        #region Carga Menu

        void StartNewTaskIndAnulacion(ctrlCarga_indAnulacion controlador);

        void StartNewTaskInventario(ctrlCarga_inventario _controlador);

        void StartNewTaskClientes(ctrlCarga_cliente _controlador);

        void StartNewTaskEstablecimientos(ctrlCarga_establecimiento _controlador);

        void StartNewTaskDescuentos(ctrlCarga_descuentos _controlador);

        void StartNewTaskDescuentosGeneral(ctrlCarga_descuentosGeneral _controlador);

        void StartNewTaskFacturas(ctrlCarga_facturas _controlador);

        void StartNewTaskIndicadorFacturas(ctrlCarga_indicadores _controlador);

        void StartNewTaskListaPrecios(ctrlCarga_listaPrecios _controlador);

        void StartNewTaskPrecioProductos(ctrlCarga_precioProducto _controlador);

        void StartNewTaskProductos(ctrlCarga_producto _controlador);

        void StartNewTaskVisita(ctrlCarga_visita _controlador);

        void StartNewTaskImpresora(ctrlCarga_impresora _controlador);

        void StartNewTaskPedidos(ctrlCarga_encabezadoPedido _controlador);

        void StartNewTaskinformacionClienteIndividualRecargarParte3(ctrlCarga_cliente _controlador);

        void informacionIndicadoresIndividualRecargarParte3(ctrlCarga_indicadores _controlador);

        void informacionDescuentoIndividualRecargarParte3(ctrlCarga_descuentos _controlador);

        void StartNewTaskDetalleReses(ctrlCarga_DetalleReses controlador);

        void StartNewTaskMensajeFactura(ctrlCarga_MensajeFactura controlador);

        #endregion

        #region Descarga

        void StartNewTaskinformacionDescargar(ctrlDescarga _controlador);

        #endregion

        #region TomaFisica

        void StartScreenInicializationTomaFisica(ctrlTomaFisica _controlador);

        void ConsolidarTomaFisica(ctrlTomaFisica _controlador);

        void pnlFinalizarSincronizarTomaFisica(ctrlTomaFisica controlador);

        void pnlFinalizar_Imprimir(ctrlTomaFisica controlador);

        void pnlFinalizarCerrarMaquina(ctrlTomaFisica controlador);

        void pnlFinalizar_Continuar(ctrlTomaFisica controlador);

        void pnlProcedimiento(ctrlTomaFisica controlador);

        #endregion

        #region IdentificarUsuario

        void StartSendActivity(LogicaVisitaGuardar controlador);

        void StartCargaInicial(ctrlIdentificarUsuario controlador);

        void StartSendBitacora(pnlBitacoraModel Bitacora);

        #endregion

        #region ConsultaReciboRecaudacion

        void StartConsultaReciboRecaudacion(ctrlConsultaReciboRecaudacion controlador);

        void StartScreenInicializationReciboRecaudacion(ctrlConsultaReciboRecaudacion controlador);

        #endregion

        #region ConsultarDocumentos

        void StartConsultaDocumentos(ctrlConsultaDocumentos controlador);

        void StartScreenInicializationConsultarDocumentos(ctrlConsultaDocumentos controlador);

        #endregion

        #region ConsultarOV

        void StartConsultaOV(ctrlConsultaOV controlador);

        void StartScreenInicializationConsultarOV(ctrlConsultaOV controlador);

        #endregion

        #region General
        void PruebaConexion();

        void SelectListViewItem(ListView listView, ObservableCollection<pnlCarniceria_Boxes> Source, int i);

        void AbortNewTask();
        #endregion

        #region Print

        void ImprimirTransaccion(impTransaccion _impresion);

        void ImprimirReporte(ctrlVistaPrevia controlador);

        #endregion

    }
}
