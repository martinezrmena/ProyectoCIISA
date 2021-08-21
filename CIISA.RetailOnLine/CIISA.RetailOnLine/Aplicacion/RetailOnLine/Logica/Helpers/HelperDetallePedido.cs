using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Entidades;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Modelo.Carniceria;
using CIISA.RetailOnLine.Aplicacion.RetailOnLine.Miscelaneos;
using CIISA.RetailOnLine.Framework.Common.DataBase.SROL.Tables;
using CIISA.RetailOnLine.Framework.Common.Misc;
using CIISA.RetailOnLine.Framework.Common.SystemInfo;
using CIISA.RetailOnLine.Framework.Common.Time;
using CIISA.RetailOnLine.Framework.Handheld.Connection;
using CIISA.RetailOnLine.Framework.Handheld.Format;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xamarin.Forms;

namespace CIISA.RetailOnLine.Aplicacion.RetailOnLine.Logica.Helpers
{
    internal class HelperDetallePedido
    {
        internal bool BuscarDetallePedido(string codpedido, string codarticulo, string Es_Factura) {

            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableDetallePedido._ARTI_DES + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._detallePedido + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableDetallePedido._ARTI_DES + " = ");
            _sb.Append("'" + codarticulo + "'");
            _sb.Append(" AND ");
            _sb.Append(TableDetallePedido._NO_TRANSA + " = ");
            _sb.Append("'" + codpedido + "'");
            _sb.Append(" AND ");
            _sb.Append(TableDetallePedido._ES_FACTURA + " = ");
            _sb.Append("'" + Es_Factura + "'");

            return OperationSQL.thereRecord(_sb, TableDetallePedido._ARTI_DES);
        }

        internal List<TransaccionDetalle> buscarDetallePedidoPorCodigoPedido(string pcodPedido, bool Validar, string Es_Factura, string codcliente)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("NO_CIA, ");
            _sb.Append("NO_TRANSA, ");
            _sb.Append("NO_LINEA, ");
            _sb.Append("ARTI_PED, ");
            _sb.Append("ARTI_DES, ");
            _sb.Append("CAN_PED, ");
            _sb.Append("CAN_DES, ");
            _sb.Append("COMENTARIO, ");
            _sb.Append("TOTAL_LIN, ");
            _sb.Append("TIPO_DOC, ");
            _sb.Append("TOTAL_IMP_LIN, ");
            _sb.Append("NO_AGENTE, ");
            _sb.Append("FECHA_CREA, ");
            _sb.Append("ENVIADO, ");
            _sb.Append("ES_VISCERA, ");
            _sb.Append("TIPO_PORCION, ");
            _sb.Append("CONSECUTIVODR ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._detallePedido + " ");
            _sb.Append("WHERE ");
            _sb.Append("NO_TRANSA = ");
            _sb.Append("'" + pcodPedido + "' ");
            _sb.Append("AND ");
            _sb.Append("APLICADO = ");
            _sb.Append("'" + SQL._No + "'");
            if (Validar)
            {
                _sb.Append(" AND ");
                _sb.Append("ES_FACTURA = ");
                _sb.Append("'" + Es_Factura + "'");
            }

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            List<TransaccionDetalle> _listaDetalles = new List<TransaccionDetalle>();

            Logica_ManagerCarniceria manager = new Logica_ManagerCarniceria();

            foreach (DataRow _fila in _dt.Rows)
            {
                TransaccionDetalle _objDetalleTransaccion = new TransaccionDetalle();

                _objDetalleTransaccion.v_codCia = _fila["NO_CIA"].ToString();
                _objDetalleTransaccion.v_codDocumento = _fila["NO_TRANSA"].ToString();

                _objDetalleTransaccion.v_numLinea = FormatUtil.convertStringToInt(_fila["NO_LINEA"].ToString());

                HelperProducto _helper = new HelperProducto();

                _objDetalleTransaccion.v_objProducto =
                _helper.buscarProductoPorCodigoProducto(_fila["ARTI_DES"].ToString());

                _objDetalleTransaccion.v_objProducto.v_codProductoPedido = _fila["ARTI_PED"].ToString();

                _objDetalleTransaccion.v_objProducto.v_cantPedido =
                FormatUtil.convertStringToDecimal(_fila["CAN_PED"].ToString());

                _objDetalleTransaccion.v_objProducto.v_cantTransaccion =
                FormatUtil.convertStringToDecimal(_fila["CAN_DES"].ToString());

                _objDetalleTransaccion.v_objProducto.v_especificacionOV = _fila["COMENTARIO"].ToString();

                _objDetalleTransaccion.v_totalLinea =
                FormatUtil.convertStringToDecimal(_fila["TOTAL_LIN"].ToString());

                _objDetalleTransaccion.v_objTipoDocumento.SetSigla(_fila["TIPO_DOC"].ToString());

                _objDetalleTransaccion.v_noAgente = _fila["NO_AGENTE"].ToString();
                _objDetalleTransaccion.v_fechaCrea = _fila["FECHA_CREA"].ToString();
                _objDetalleTransaccion.v_enviado = _fila["ENVIADO"].ToString();

                _objDetalleTransaccion.v_anulado = SQL._No;
                _objDetalleTransaccion.v_codMotivo = string.Empty;
                _objDetalleTransaccion.v_objProducto.v_estado = string.Empty;
                _objDetalleTransaccion.v_noFactura = string.Empty;
                _objDetalleTransaccion.v_Es_Factura = Es_Factura;

                string viscera = _fila["ES_VISCERA"].ToString();

                //Es víscera
                if (!string.IsNullOrEmpty(viscera))
                {
                    if (viscera.Equals(SQL._Si))
                    {
                        _objDetalleTransaccion.v_objProducto.EsViscera = true;
                    }
                    else if (viscera.Equals(SQL._No))
                    {
                        _objDetalleTransaccion.v_objProducto.EsViscera = false;
                    }
                }

                _objDetalleTransaccion.v_objProducto.TipoPorcion = _fila["TIPO_PORCION"].ToString();

                _objDetalleTransaccion.v_objProducto.ConsecutivoDReses = _fila["CONSECUTIVODR"].ToString();

                if (manager.buscarDetalleResExiste(_objDetalleTransaccion.v_objProducto.v_codProducto, _objDetalleTransaccion.v_codDocumento))
                {
                    _objDetalleTransaccion.v_listaDetalleReses = manager.buscarDetalleResesPedido(codcliente, _objDetalleTransaccion.v_codDocumento, _objDetalleTransaccion.v_objProducto.v_codProducto);
                }

                _listaDetalles.Add(_objDetalleTransaccion);
            }

            return _listaDetalles;
        }

        internal List<TransaccionDetalle> actualizarAplicadoListaDetallesPedido(List<TransaccionDetalle> _listaDocumentos, bool Validar, string Es_Factura, List<string> Cod_Documento, string Codcliente)
        {
            List<TransaccionDetalle> DetallesParaNuevoPedido = new List<TransaccionDetalle>();

            if (Cod_Documento.Count > 0)
            {
                string Not_Factura = Es_Factura;

                if (Validar)
                {
                    Not_Factura = Not_Factura.Equals(SQL._Si) ? SQL._No : SQL._Si;
                }

                foreach (string pedido in Cod_Documento)
                {
                    DetallesParaNuevoPedido.AddRange(buscarDetallePedidoPorCodigoPedido(pedido, Validar, Not_Factura, Codcliente));

                    StringBuilder _sb = new StringBuilder();
                    _sb.Append("UPDATE ");
                    _sb.Append(TablesROL._detallePedido + " ");
                    _sb.Append("SET ");
                    _sb.Append("APLICADO = ");
                    _sb.Append("'" + SQL._Si + "' ");
                    _sb.Append("WHERE ");
                    _sb.Append("NO_TRANSA = ");
                    _sb.Append("'" + pedido + "' ");

                    var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                    MultiGeneric.updateRecord(_sb);

                }
            }

            return DetallesParaNuevoPedido;
        }

        internal void guardarDetallePedido(Cliente pobjCliente, string TipoAgente)
        {
            foreach (TransaccionDetalle _objTransaccionDetalle in pobjCliente.v_objTransaccion.v_listaDetalles)
            {
                StringBuilder _sb = new StringBuilder();

                _sb.Append("INSERT ");
                _sb.Append("INTO ");
                _sb.Append(TablesROL._detallePedido + " ");
                _sb.Append("(");
                _sb.Append(TableDetallePedido._NO_CIA + ", ");
                _sb.Append(TableDetallePedido._NO_TRANSA + ", ");
                _sb.Append(TableDetallePedido._NO_LINEA + ", ");
                _sb.Append(TableDetallePedido._ARTI_PED + ", ");
                _sb.Append(TableDetallePedido._ARTI_DES + ", ");
                _sb.Append(TableDetallePedido._CAN_PED + ", ");
                _sb.Append(TableDetallePedido._CAN_DES + ", ");
                _sb.Append(TableDetallePedido._COMENTARIO + ", ");
                _sb.Append(TableDetallePedido._TOTAL_LIN + ", ");
                _sb.Append(TableDetallePedido._TIPO_DOC + ", ");
                _sb.Append(TableDetallePedido._TOTAL_IMP_LIN + ", ");
                _sb.Append(TableDetallePedido._NO_AGENTE + ", ");
                _sb.Append(TableDetallePedido._FECHA_CREA + ", ");
                _sb.Append(TableDetallePedido._USUARIO_CREA + ", ");
                _sb.Append(TableDetallePedido._ENVIADO + ", ");
                _sb.Append(TableDetallePedido._APLICADO + ", ");
                _sb.Append(TableDetallePedido._ES_VISCERA + ", ");
                _sb.Append(TableDetallePedido._TIPO_PORCION + ", ");
                _sb.Append(TableDetallePedido._CONSECUTIVODR + ", ");
                _sb.Append(TableDetallePedido._ES_FACTURA);
                _sb.Append(") ");
                _sb.Append("VALUES ");
                _sb.Append("(");
                _sb.Append("'" + pobjCliente.v_no_cia + "', ");
                _sb.Append("'" + pobjCliente.v_objTransaccion.v_codDocumento + "', ");
                _sb.Append("'" + _objTransaccionDetalle.v_numLinea + "', ");
                _sb.Append("'" + _objTransaccionDetalle.v_objProducto.v_codProductoPedido + "', ");
                _sb.Append("'" + _objTransaccionDetalle.v_objProducto.v_codProducto + "', ");
                _sb.Append("" + _objTransaccionDetalle.v_objProducto.v_cantPedido + ", ");
                _sb.Append("" + _objTransaccionDetalle.v_objProducto.v_cantTransaccion + ", ");
                _sb.Append("'" + _objTransaccionDetalle.v_objProducto.v_especificacionOV + "', ");
                _sb.Append("REPLACE('" + _objTransaccionDetalle.v_totalLinea + "',',',''), ");

                if (_objTransaccionDetalle.v_Es_Factura.Equals(SQL._Si))
                {
                    _sb.Append("'" + ROLTransactions._Factura + "', ");
                }
                else
                {
                    _sb.Append("'" + ROLTransactions._Otro + "', ");
                }

                _sb.Append("REPLACE('" + _objTransaccionDetalle.v_objProducto.calcularMontoImpuestoPorCantidadDeProducto(pobjCliente) + "',',',''), ");
                _sb.Append("'" + pobjCliente.v_no_agente + "', ");
                _sb.Append("'" + VarTime.getDateTimeSQLiteComplete(pobjCliente.v_objTransaccion.v_fechaCreacion) + "', ");
                _sb.Append("'" + string.Empty + "', ");
                _sb.Append("'" + SQL._No + "', ");
                _sb.Append("'" + SQL._No + "', ");

                if (_objTransaccionDetalle.v_objProducto.EsViscera)
                {
                    _sb.Append("'" + SQL._Si + "', ");
                }
                else
                {
                    _sb.Append("'" + SQL._No + "', ");
                }
                
                _sb.Append("'" + _objTransaccionDetalle.v_objProducto.TipoPorcion + "', ");

                _sb.Append("'" + _objTransaccionDetalle.v_objProducto.ConsecutivoDReses + "', ");

                if (TipoAgente.Equals(Agent._carniceroSigla))
                {
                    _sb.Append("'" + _objTransaccionDetalle.v_Es_Factura + "'");
                }
                else
                {
                    _sb.Append("'" + SQL._No + "'");
                }

                _sb.Append(")");

                var MultiGeneric = DependencyService.Get<IMultiGeneric>();

                MultiGeneric.insertRecord(_sb);
            }
        }

        internal void actualizarDetallePedido(Producto v_objProducto, string Cod_Document)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("UPDATE ");
            _sb.Append(TablesROL._detallePedido + " ");
            _sb.Append("SET ");
            _sb.Append(TableDetallePedido._CAN_DES + " = '" + v_objProducto.v_cantTransaccion + "' ");
            _sb.Append("WHERE ");
            _sb.Append(TableDetallePedido._NO_TRANSA + "= '" + Cod_Document + "'");
            _sb.Append(" AND ");
            _sb.Append(TableDetallePedido._ARTI_DES + "= '" + v_objProducto.v_codProducto + "'");
            _sb.Append(" AND ");
            _sb.Append(TableDetallePedido._ES_FACTURA + "= '" + SQL._Si + "'");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            MultiGeneric.updateRecord(_sb);
        }

        internal bool DetallePedido_Vacio()
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableDetallePedido._NO_CIA + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._detallePedido + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            DataTable _dt = MultiGeneric.uploadDataTable(_sb);

            if (_dt.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        internal bool existeProductoDespachado(string pcodProducto)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append(TableDetallePedido._ARTI_DES + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._detallePedido + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableDetallePedido._ARTI_DES + " = ");
            _sb.Append("'" + pcodProducto + "'");

            return OperationSQL.thereRecord(_sb, TableDetallePedido._ARTI_DES);
        }

        internal decimal obtenerInventarioReservadoPedidos(string pcodProducto)
        {
            StringBuilder _sb = new StringBuilder();

            _sb.Append("SELECT ");
            _sb.Append("SUM(" + TableDetallePedido._CAN_DES + ") " + TableDetallePedido._CAN_DES + " ");
            _sb.Append("FROM ");
            _sb.Append(TablesROL._detallePedido + " ");
            _sb.Append("WHERE ");
            _sb.Append(TableDetallePedido._ARTI_DES + " = ");
            _sb.Append("'" + pcodProducto + "' ");
            _sb.Append("AND ");
            _sb.Append(TableDetallePedido._APLICADO + " = ");
            _sb.Append("'" + SQL._No + "' ");
            _sb.Append(" AND ");
            _sb.Append("ENVIADO = '" + SQL._No + "'");
            _sb.Append(" AND ");
            _sb.Append("ES_FACTURA = '" + SQL._Si + "' ");
            _sb.Append("GROUP BY ");
            _sb.Append(TableDetallePedido._ARTI_DES + " ");

            var MultiGeneric = DependencyService.Get<IMultiGeneric>();

            return MultiGeneric.readDecimal(_sb);
        }
    }
}
