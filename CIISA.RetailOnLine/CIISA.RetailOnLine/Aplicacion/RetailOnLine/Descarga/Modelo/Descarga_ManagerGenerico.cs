using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Helpers;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Handheld.Connection;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Descarga.Modelo
{
    public class Descarga_ManagerGenerico
    {
        public void marcarDatosComoEnviados(string ptabla)
        {
            HelperGenerico _helper = new HelperGenerico();

            _helper.MarcarComoEnviado(ptabla);
        }

        public void marcarDatosComoEnviados_Documentos(string ptabla, TransaccionEncabezado pobjTransaccionEncabezado, string TipoTransaccion)
        {
            HelperGenerico _helper = new HelperGenerico();

            _helper.MarcarComoEnviado_Documentos(ptabla, pobjTransaccionEncabezado, TipoTransaccion);
        }

        public void marcarDatosComoEnviados_Tramites(string ptabla, TransaccionEncabezado pobjTransaccionEncabezado)
        {
            HelperGenerico _helper = new HelperGenerico();

            _helper.MarcarComoEnviado_Tramites(ptabla, pobjTransaccionEncabezado);
        }

        public void marcarDatosComoEnviados_DetalleTramites(string ptabla, TransaccionEncabezado pobjTransaccionEncabezado)
        {
            HelperGenerico _helper = new HelperGenerico();

            _helper.MarcarComoEnviado_DetallesTramites(ptabla, pobjTransaccionEncabezado);
        }

        public void marcarDatosComoEnviados_Recibos(string ptabla, TransaccionEncabezado pobjTransaccionEncabezado)
        {
            HelperGenerico _helper = new HelperGenerico();

            _helper.MarcarComoEnviado_Recibos(ptabla, pobjTransaccionEncabezado);
        }

        public void marcarDatosComoEnviados_DetallesRecibos(string ptabla, TransaccionEncabezado pobjTransaccionEncabezado)
        {
            HelperGenerico _helper = new HelperGenerico();

            _helper.MarcarComoEnviado_DetallesRecibos(ptabla, pobjTransaccionEncabezado);
        }

        public void marcarDatosComoEnviados_Pagos(string ptabla, TransaccionEncabezado pobjTransaccionEncabezado)
        {
            HelperGenerico _helper = new HelperGenerico();

            _helper.MarcarComoEnviado_Pagos(ptabla, pobjTransaccionEncabezado);
        }

        public void marcarDatosComoEnviados_PagosRecibos(string ptabla, TransaccionEncabezado pobjTransaccionEncabezado)
        {
            HelperGenerico _helper = new HelperGenerico();

            _helper.MarcarComoEnviado_PagosRecibos(ptabla, pobjTransaccionEncabezado);
        }

        public void marcarDatosComoNoEnvidos()
        {
            Logica_ManagerEncabezadoAnulacion _manager = new Logica_ManagerEncabezadoAnulacion();

            string _listaFacturas = _manager.buscarCodigoDocumentosAnulados();

            if (_listaFacturas.Equals(string.Empty))
            {
                OperationSQL.updateAsNoSent(
                    GetType().Name,
                    "marcarDatosComoNoEnvidos",
                    TablesROL._encabezadoDocumento
                    );

                OperationSQL.updateAsNoSent(
                    GetType().Name,
                    "marcarDatosComoNoEnvidos",
                    TablesROL._detalleDocumento
                    );

                OperationSQL.updateAsNoSent(
                    GetType().Name,
                    "marcarDatosComoNoEnvidos",
                    TablesROL._pagos
                    );
            }
            else
            {
                OperationSQL.updateAsNoSentSpecificTable(
                    TablesROL._encabezadoDocumento,
                    TableEncabezadoDocumento._CODDOCUMENTO,
                    _listaFacturas
                    );

                OperationSQL.updateAsNoSentSpecificTable(
                    TablesROL._detalleDocumento,
                    TableDetalleDocumento._CODDOCUMENTO,
                    _listaFacturas
                    );

                OperationSQL.updateAsNoSentSpecificTable(
                    TablesROL._pagos,
                    TablePagos._NO_TRANSA,
                    _listaFacturas
                    );
            }

            OperationSQL.updateAsNoSent(
                GetType().Name,
                "marcarDatosComoNoEnvidos",
                TablesROL._encabezadoRecibo
                );

            OperationSQL.updateAsNoSent(
                GetType().Name,
                "marcarDatosComoNoEnvidos",
                TablesROL._detalleRecibo
                );

            OperationSQL.updateAsNoSent(
                GetType().Name,
                "marcarDatosComoNoEnvidos",
                TablesROL._pagoRecibo
                );

            OperationSQL.updateAsNoSent(
                GetType().Name,
                "marcarDatosComoNoEnvidos",
                TablesROL._encabezadoTramite
                );

            OperationSQL.updateAsNoSent(
                GetType().Name,
                "marcarDatosComoNoEnvidos",
                TablesROL._detalleTramite
                );

            OperationSQL.updateAsNoSent(
                GetType().Name,
                "marcarDatosComoNoEnvidos",
                TablesROL._encabezadoAnulacion
                );

            OperationSQL.updateAsNoSent(
                GetType().Name,
                "marcarDatosComoNoEnvidos",
                TablesROL._detalleAnulacion
                );

            OperationSQL.updateAsNoSent(
                GetType().Name,
                "marcarDatosComoNoEnvidos",
                TablesROL._inventario
                );

            OperationSQL.updateAsNoSent(
                GetType().Name,
                "marcarDatosComoNoEnvidos",
                TablesROL._cliente
                );

            OperationSQL.updateAsNoSent(
                GetType().Name,
                "marcarDatosComoNoEnvidos",
                TablesROL._encabezadoRazonesNV
                );

        }
    }
}
