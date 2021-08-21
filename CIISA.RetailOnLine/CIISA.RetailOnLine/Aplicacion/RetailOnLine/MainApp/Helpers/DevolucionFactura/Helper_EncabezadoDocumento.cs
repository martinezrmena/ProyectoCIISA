using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.MainApp.Helpers.DevolucionFactura
{
    public class Helper_EncabezadoDocumento
    {
        #region Devolucion de factura
        internal bool ExisteFacturaDocumentoCliente(string COD_FACTURA)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("E.CODDOCUMENTO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoDocumento + " E ");
            _sb.Append("WHERE ");
            _sb.Append("E.ANULADO = ");
            _sb.Append("'" + SQL._No + "' ");


            return OperationSQL.thereRecord(_sb, "CODDOCUMENTO");
        }

        private TransaccionEncabezado buscarProductosPorCodigoFactura(string pCodFactura, Cliente cliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("CODCIA, ");
            _sb.Append("CODDOCUMENTO, ");
            _sb.Append("CODAGENTE, ");
            _sb.Append("DATETIME(FECHACREACION) FECHACREACION, ");
            _sb.Append("DATETIME(FECHAENTREGA) FECHAENTREGA, ");
            _sb.Append("TOTAL, ");
            _sb.Append("TOTAL_IMP, ");
            _sb.Append("ENVIADO ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoDocumento + " ");
            _sb.Append("WHERE ");
            _sb.Append("CODDOCUMENTO = ");
            _sb.Append("'" + pCodFactura + "' ");
            _sb.Append("AND ");
            _sb.Append("ANULADO = ");
            _sb.Append("'" + SQL._No + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            TransaccionEncabezado _objTransaccion = null;

            foreach (DataRow _fila in _dt.Rows)
            {
                _objTransaccion = new TransaccionEncabezado();

                _objTransaccion.v_codCia = _fila["CODCIA"].ToString();
                _objTransaccion.v_codDocumento = _fila["CODDOCUMENTO"].ToString();
                _objTransaccion.v_codCliente = cliente.v_no_cliente;

                _objTransaccion.v_objEstablecimiento.v_codEstablecimiento =
                    cliente.v_objEstablecimiento.v_codEstablecimiento;

                _objTransaccion.v_codAgente = _fila["CODAGENTE"].ToString();

                _objTransaccion.v_fechaCreacion =
                    FormatUtil.convertStringToDateTimeWithTime(_fila["FECHACREACION"].ToString());

                _objTransaccion.v_fechaEntrega =
                    FormatUtil.convertStringToDateTimeWithTime(_fila["FECHAENTREGA"].ToString());

                _objTransaccion.v_total =
                    FormatUtil.convertStringToDecimal(_fila["TOTAL"].ToString());

                _objTransaccion.v_totalImp =
                    FormatUtil.convertStringToDecimal(_fila["TOTAL_IMP"].ToString());

                _objTransaccion.v_indAutomatico = SQL._No;
                _objTransaccion.v_enviado = _fila["ENVIADO"].ToString();

                _objTransaccion.v_objTipoDocumento.SetSigla(ROLTransactions._devolucionFacturaSigla);
                _objTransaccion.v_observacion = string.Empty;
                _objTransaccion.v_tramite = SQL._No;
                _objTransaccion.v_anulado = SQL._No;
                _objTransaccion.v_codClienteGenerico = string.Empty;
                _objTransaccion.v_motivoAnulacion = string.Empty;

                HelperDetalleDocumento _manager = new HelperDetalleDocumento();
                _objTransaccion.v_listaDetalles = _manager.buscarDetalleDocumentoPorCodigoFactura(pCodFactura, _objTransaccion.v_codAgente);
            }

            return _objTransaccion;
        }

        internal List<TransaccionEncabezado> buscarProductosPorFactura(Cliente cliente, string COD_FACTURA)
        {

            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("CODDOCUMENTO, ");
            _sb.Append("CODAGENTE ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._encabezadoDocumento + " ");
            _sb.Append("WHERE ");
            _sb.Append("CODDOCUMENTO = ");
            _sb.Append("'" + COD_FACTURA + "' ");
            _sb.Append("AND ");
            _sb.Append("ANULADO = ");
            _sb.Append("'" + SQL._No + "' ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            List<TransaccionEncabezado> _listaProductos = new List<TransaccionEncabezado>();

            foreach (DataRow _fila in _dt.Rows)
            {
                _listaProductos.Add(buscarProductosPorCodigoFactura(COD_FACTURA, cliente));
            }

            return _listaProductos;
        }

        #endregion
    }
}
